using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace test
{
    class ConsoleLogger : Logger
    {
        protected override void trace(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("TRACE [{0}] - {1}", _categoray, _message));
        }
        protected override void debug(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("DEBUG [{0}] - {1}", _categoray, _message));
        }
        protected override void info(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("INFO [{0}] - {1}", _categoray, _message));
        }
        protected override void warning(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("WARNING [{0}] - {1}", _categoray, _message));
        }
        protected override void error(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("ERROR [{0}] - {1}", _categoray, _message));
        }
        protected override void exception(System.Exception _exp)
        {
            Console.WriteLine(string.Format("EXCEPTION [{0}] - {1}", "EXP", _exp.Message));
        }
    }
    class SimpleModel : Model
    {

        public class SimpleStatus : Status
        {
            public string uuid { get; set; }
        }

        private SimpleController controller { get; set; }
        private SimpleStatus status
        {
            get
            {
                return status_ as SimpleStatus;
            }
        }

        public SimpleModel(string _uid) : base(_uid)
        {
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<SimpleStatus>("SimpleStatus", out err);
            controller = findController("SimpleController") as SimpleController;
        }

        protected override void setup()
        {
            getLogger().Info("setup SimpleModel");
        }

        protected override void postDismantle()
        {
            Error err;
            killStatus("SimpleStatus", out err);
        }

        public void UpdateSignin(Model.Status _reply, string _uuid)
        {
            controller.Signin(_reply, status, _uuid);
        }
    }

    class SimpleView : View
    {

        private SimpleService service { get; set; }

        public SimpleView(string _uid) : base(_uid)
        {
        }

        protected override void preSetup()
        {
            service = findService("SimpleService") as SimpleService;
        }

        protected override void setup()
        {
            getLogger().Info("setup SimpleView");
        }

        public void OnSigninClicked()
        {
            service.PostSignin("admin", "11223344");
        }

        public void PrintError(string _error)
        {
            getLogger().Error(_error);
        }

        public void PrintUUID(SimpleModel.SimpleStatus _status)
        {
            getLogger().Info(string.Format("uuid is {0}", _status.uuid));
        }
    }

    class SimpleController : Controller
    {
        private SimpleView view { get; set; }

        public SimpleController(string _uid) : base(_uid)
        {
        }

        protected override void preSetup()
        {
            view = findView("SimpleView") as SimpleView;
        }
        protected override void setup()
        {
            getLogger().Info("setup SimpleController");
        }
        public void Signin(Model.Status _reply, SimpleModel.SimpleStatus _status, string _uuid)
        {
            if (_reply.getCode() != 0)
            {
                view.PrintError(_reply.getMessage());
                return;
            }
            _status.uuid = _uuid;
            view.PrintUUID(_status);
        }
    }

    class HttpSerivce : Service
    {
        public Action<string, Dictionary<string, Any>, Options, OnReplyDelegate, OnErrorDelegate> MockProcessor { get; set; }
        public HttpSerivce(string _uid) : base(_uid)
        {
        }

        protected void post(string _url, Dictionary<string, Any> _parameter, Options _options, OnReplyDelegate _onReply, OnErrorDelegate _onError)
        {
            if (_options.useMock)
            {
                MockProcessor(_url, _parameter, _options, _onReply, _onError);
            }
            else
            {
                _onError(Error.NewException(new NotImplementedException()));
            }
        }
    }


    class SimpleService : HttpSerivce
    {
        public delegate void SendRequestDelegate(string _url, Dictionary<string, Any> _parameter, Options _options, Action<string> _onReply, Action<Error> _onError);
        public SendRequestDelegate Mock { get; set; }

        private SimpleModel model { get; set; }

        public SimpleService(string _uid) : base(_uid)
        {
        }

        protected override void preSetup()
        {
            model = findModel("SimpleModel") as SimpleModel;
        }

        protected override void setup()
        {
            getLogger().Info("setup SimpleService");
        }

        public void PostSignin(string _username, string _password)
        {
            Dictionary<string, Any> parameter = new Dictionary<string, Any>();
            parameter["username"] = Any.FromString(_username);
            parameter["password"] = Any.FromString(_password);
            Options options = new Options();
            options.useMock = true;
            post("http://localhost/signin", parameter, options, (_reply) =>
            {
                Model.Status reply = Model.Status.New<Model.Status>(0, "");
                string uuid = _reply as string;
                model.UpdateSignin(reply, uuid);
            }, (_err) =>
            {
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateSignin(reply, "");
            });
        }
    }




    class Program
    {
        class SimpleMock
        {
            public static void MockProcessor(string _url, Dictionary<string, Any> _params, Service.Options _options, Service.OnReplyDelegate _onReply, Service.OnErrorDelegate _onError)
            {
                if (!_url.Equals("http://localhost/signin"))
                {
                    _onError(Error.NewAccessErr("404 not found"));
                    return;
                }
                if (_params["username"].AsString().Equals("admin") && _params["password"].AsString().Equals("11223344"))
                    _onReply("00000000001");
                else
                    _onError(Error.NewAccessErr("password is not matched"));
            }
        }

        static void Main(string[] args)
        {
            //testAny1();
            Framework framework = new Framework();
            framework.setLogger(new ConsoleLogger());

            framework.Initialize();

            var simpleModel = new SimpleModel("SimpleModel");
            framework.getStaticPipe().RegisterModel(simpleModel);
            SimpleView simpleView = new SimpleView("SimpleView");
            framework.getStaticPipe().RegisterView(simpleView);
            var simpleController = new SimpleController("SimpleController");
            framework.getStaticPipe().RegisterController(simpleController);
            SimpleService simpleService = new SimpleService("SimpleService");
            framework.getStaticPipe().RegisterService(simpleService);
            simpleService.MockProcessor = SimpleMock.MockProcessor;

            framework.Setup();

            // 主循环
            // 模拟登录按钮被点击时
            {
                simpleView.OnSigninClicked();
            }

            framework.Dismantle();
            framework.getStaticPipe().CancelService(simpleService);
            framework.getStaticPipe().CancelController(simpleController);
            framework.getStaticPipe().CancelView(simpleView);
            framework.getStaticPipe().CancelModel(simpleModel);

            framework.Release();
        }

        static void testAny1()
        {

            Console.WriteLine("int32 ------------------------------------------------");
            Any int32 = Any.FromInt32(10);
            print(int32);
            Console.WriteLine("int64 ------------------------------------------------");
            Any int64 = Any.FromInt64(99999999999999);
            print(int64);
            Console.WriteLine("float32 ------------------------------------------------");
            Any float32 = Any.FromFloat32(10.1f);
            print(float32);
            Console.WriteLine("float64 ------------------------------------------------");
            Any float64 = Any.FromFloat64(99999999999.9);
            print(float64);
            Console.WriteLine("bool ------------------------------------------------");
            Any boolean = Any.FromBool(true);
            print(boolean);
            Console.WriteLine("string ------------------------------------------------");
            Any str = Any.FromString("hello");
            print(str);
            Console.WriteLine("int32ary ------------------------------------------------");
            Any int32ary = Any.FromInt32Ary(new int[] { int.MaxValue, int.MaxValue - 1 });
            print(int32ary);
            Console.WriteLine("int64ary ------------------------------------------------");
            Any int64ary = Any.FromInt64Ary(new long[] { long.MaxValue, long.MaxValue - 1 });
            print(int64ary);
            Console.WriteLine("float32ary ------------------------------------------------");
            Any float32ary = Any.FromFloat32Ary(new float[] { float.MaxValue, float.MaxValue / 2 });
            print(float32ary);
            Console.WriteLine("float64ary ------------------------------------------------");
            Any float64ary = Any.FromFloat64Ary(new double[] { double.MaxValue, double.MaxValue / 2 });
            print(float64ary);
            Console.WriteLine("boolary ------------------------------------------------");
            Any boolary = Any.FromBoolAry(new bool[] { true, false });
            print(boolary);
            Console.WriteLine("strinary ------------------------------------------------");
            Any strary = Any.FromStringAry(new string[] { "hello", "world" });
            print(strary);
            Console.WriteLine("int32map ------------------------------------------------");
            Dictionary<string, int> int32map_value = new Dictionary<string, int>();
            int32map_value["a"] = int.MaxValue;
            int32map_value["b"] = -int.MaxValue / 2;
            Any int32map = Any.FromInt32Map(int32map_value);
            print(int32map);
            Console.WriteLine("int64map ------------------------------------------------");
            Dictionary<string, long> int64map_value = new Dictionary<string, long>();
            int64map_value["a"] = long.MaxValue;
            int64map_value["b"] = -long.MaxValue / 2;
            Any int64map = Any.FromInt64Map(int64map_value);
            print(int64map);
            Console.WriteLine("float32map ------------------------------------------------");
            Dictionary<string, float> float32map_value = new Dictionary<string, float>();
            float32map_value["a"] = float.MaxValue;
            float32map_value["b"] = -float.MaxValue / 2;
            Any float32map = Any.FromFloat32Map(float32map_value);
            print(float32map);
            Console.WriteLine("float64map ------------------------------------------------");
            Dictionary<string, double> float64map_value = new Dictionary<string, double>();
            float64map_value["a"] = double.MaxValue;
            float64map_value["b"] = -double.MaxValue / 2;
            Any float64map = Any.FromFloat64Map(float64map_value);
            print(float64map);
            Console.WriteLine("boolmap ------------------------------------------------");
            Dictionary<string, bool> boolmap_value = new Dictionary<string, bool>();
            boolmap_value["a"] = true;
            boolmap_value["b"] = false;
            Any boolmap = Any.FromBoolMap(boolmap_value);
            print(boolmap);
            Console.WriteLine("strmap ------------------------------------------------");
            Dictionary<string, string> strmap_value = new Dictionary<string, string>();
            strmap_value["a"] = "hello";
            strmap_value["b"] = "world";
            Any strmap = Any.FromStringMap(strmap_value);
            print(strmap);
        }

        static void testAny2()
        {
            Any any = Any.FromString("23232.2323");
            print(any);
            any = Any.FromString("343434343423232");
            print(any);
            any = Any.FromString("true");
            print(any);
            any = Any.FromString("[232323,23232323232323232, 232.32, 23232323232323232323.12, true]");
            print(any);
            any = Any.FromString("{\"int32\":1212,\"int64\":2323232323232323232, \"float32\":23.23, \"float64\":232323232323232232323.1, \"bool\":true, \"string\":\"hello\"}");
            print(any);
        }

        static void print(Any _any)
        {

            Console.WriteLine("As");
            Console.WriteLine(string.Format("int32: {0}", _any.AsInt32()));
            Console.WriteLine(string.Format("int64: {0}", _any.AsInt64()));
            Console.WriteLine(string.Format("float32: {0}", _any.AsFloat32()));
            Console.WriteLine(string.Format("float64: {0}", _any.AsFloat64()));
            Console.WriteLine(string.Format("bool: {0}", _any.AsBool()));
            Console.WriteLine(string.Format("string: {0}", _any.AsString()));
            Console.WriteLine(string.Format("bytes: {0} bytes", _any.AsBytes().Length));

            Console.WriteLine("As Ary");
            var ary1 = _any.AsInt32Ary();
            var ary2 = _any.AsInt64Ary();
            var ary3 = _any.AsFloat32Ary();
            var ary4 = _any.AsFloat64Ary();
            var ary5 = _any.AsBoolAry();
            var ary6 = _any.AsStringAry();

            Console.WriteLine("As Map");
            var map1 = _any.AsInt32Map();
            var map2 = _any.AsInt64Map();
            var map3 = _any.AsFloat32Map();
            var map4 = _any.AsFloat64Map();
            var map5 = _any.AsBoolMap();
            var map6 = _any.AsStringMap();

        }
    }
}
