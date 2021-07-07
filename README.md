
---
[TOC]
---



# 简介

oelMVCS是一个轻量级的MVCS框架。

## 框架组成

oelMVCS由数据层（model）、视图层（view）、控制层（controller）、服务层（service）四层组成。
每一层均有一个管理中心(center)负责实例的注册。层之间可以通过各自的管理中心获取其它层的实例。层之间的访问权限如下所示：

```
          ______________________________
         |        |                    |
        \|/      \|/                   |
    Service -> Model -> Controller -> View
        /|\      /|\         | 
         |________|__________|
```

## 数据流

oelMVCS使用单向数据流，数据流使用状态(status)进行数据的传递。状态使用归并方式，总是将新的数据和现有状态合并为新的状态。以保证数据的有效更改均发生在数据层（model）。

### 状态

状态（status）被定义为变化时会引起视图层（view）变化的那一部分数据。

### 属性

属性（property）被定义为即使发生变化，也不引起视图层（view）变化的那一部分数据。

## 管线
为了便于进行组件的自动化操作和手动化操作，提供了管线的概念
- 静态管线（StaticPipe）
 操作在主循环外部自动进行安装和拆卸的组件
- 动态管线（DynamicPipe）
 操作在主循环内部手动安装和拆卸的组件


## 生命期

```

框架初始化（Initialize）        ：创建组件中心
 |
\|/
静态管线注册组件（Register）      ：在组件中心添加组件
 |
\|/
框架安装已注册组件（Setup）        ： 调用所有组件的setup方法
 |
\|/
主循环                          ：主循环内使用动态管线手动安装和拆卸组件
 | 
\|/
框架拆卸已注册组件（Dismantle）    ：调用所有组件的Dismantle方法
 |
\|/
静态管线注销组件（Cancel）         ：在组件中心移除组件
 |
\|/
框架销毁（Release）               ：销毁组件中心
```



# 开始使用


## 构建框架

```csharp
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace test
{
    class SimpleModel : Model
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleModel");
        }
    }
    class SimpleView : View
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleView");
        }
    }

    class SimpleController : Controller
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleController");
        }
    }

    class SimpleService: Service
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleService");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Framework framework = new Framework();
			framework.setLogger(new Logger());
            framework.Initialize();

            framework.getStaticPipe().RegisterModel("SimpleModel", new SimpleModel());
            framework.getStaticPipe().RegisterView("SimpleView", new SimpleView());
            framework.getStaticPipe().RegisterController("SimpleController", new SimpleController());
            framework.getStaticPipe().RegisterService("SimpleService", new SimpleService());

            framework.Setup();

            // 主循环

            framework.Dismantle();
            framework.getStaticPipe().CancelService("SimpleService");
            framework.getStaticPipe().CancelController("SimpleController");
            framework.getStaticPipe().CancelView("SimpleView");
            framework.getStaticPipe().CancelModel("SimpleModel");

            framework.Release();
        }
    }//class
    
}//namespace
```



## 注入日志

```csharp
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
        protected override void setup()
        {
            getLogger().Info("setup SimpleModel");
        }
    }
    class SimpleView : View
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleView");
        }
    }

    class SimpleController : Controller
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleController");
        }
    }

    class SimpleService: Service
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleService");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Framework framework = new Framework();
			framework.setLogger(new ConsoleLogger());
            framework.Initialize();

            framework.getStaticPipe().RegisterModel("SimpleModel", new SimpleModel());
            framework.getStaticPipe().RegisterView("SimpleView", new SimpleView());
            framework.getStaticPipe().RegisterController("SimpleController", new SimpleController());
            framework.getStaticPipe().RegisterService("SimpleService", new SimpleService());

            framework.Setup();

            // 主循环

            framework.Dismantle();
            framework.getStaticPipe().CancelService("SimpleService");
            framework.getStaticPipe().CancelController("SimpleController");
            framework.getStaticPipe().CancelView("SimpleView");
            framework.getStaticPipe().CancelModel("SimpleModel");

            framework.Release();
        }
    }//class
    
}//namespace
```

控制台打印日志如下：

```
INFO [oelMVCS::Initialize] - initialize framework
INFO [oelMVCS::Register] - register SimpleModel
INFO [oelMVCS::Register] - register SimpleView
INFO [oelMVCS::Register] - register SimpleController
INFO [oelMVCS::Register] - register SimpleService
INFO [oelMVCS::Setup] - setup framework
INFO [oelMVCS::setup] - setup SimpleModel
INFO [oelMVCS::setup] - setup SimpleView
INFO [oelMVCS::setup] - setup SimpleController
INFO [oelMVCS::setup] - setup SimpleService
INFO [oelMVCS::Dismantle] - dismantle framework
INFO [oelMVCS::Cancel] - cancel SimpleService
INFO [oelMVCS::Cancel] - cancel SimpleController
INFO [oelMVCS::Cancel] - cancel SimpleView
INFO [oelMVCS::Cancel] - cancel SimpleModel
INFO [oelMVCS::Release] - release framework
```



## 简易请求

改写View

```csharp
 class SimpleView : View
    {
        private SimpleService service { get; set; }
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
    }
```

改写Service

```csharp
 class SimpleService : Service
    {
        protected override void setup()
        {
            getLogger().Info("setup SimpleService");
        }

        public void PostSignin(string _username, string _password)
        {
            Dictionary<string, Any> parameter = new Dictionary<string, Any>();
            parameter["username"] = Any.FromString(_username);
            parameter["password"] = Any.FromString(_password);
            post("http://localhost/signin", parameter, (_reply) =>
            {
                getLogger().Info(_reply);
            }, (_err) =>
            {
                getLogger().Error(_err.ToString());
            }, null);
        }
    }
```

改写主函数

```csharp
 static void Main(string[] args)
        {
            //testAny1();
            Framework framework = new Framework();
            framework.setLogger(new ConsoleLogger());

            framework.Initialize();

            framework.getStaticPipe().RegisterModel("SimpleModel", new SimpleModel());
            SimpleView simpleView = new SimpleView();
            framework.getStaticPipe().RegisterView("SimpleView", simpleView);
            framework.getStaticPipe().RegisterController("SimpleController", new SimpleController());
            framework.getStaticPipe().RegisterService("SimpleService", new SimpleService());

            framework.Setup();

            // 主循环
            // 模拟登录按钮被点击时
            simpleView.OnSigninClicked();

            framework.Dismantle();
            framework.getStaticPipe().CancelService("SimpleService");
            framework.getStaticPipe().CancelController("SimpleController");
            framework.getStaticPipe().CancelView("SimpleView");
            framework.getStaticPipe().CancelModel("SimpleModel");

            framework.Release();
        }
```



运行后控制台输出如下

```
INFO [oelMVCS::Initialize] - initialize framework
INFO [oelMVCS::Register] - register SimpleModel
INFO [oelMVCS::Register] - register SimpleView
INFO [oelMVCS::Register] - register SimpleController
INFO [oelMVCS::Register] - register SimpleService
INFO [oelMVCS::Setup] - setup framework
INFO [oelMVCS::setup] - setup SimpleModel
INFO [oelMVCS::setup] - setup SimpleView
INFO [oelMVCS::setup] - setup SimpleController
INFO [oelMVCS::setup] - setup SimpleService
ERROR [oelMVCS::<PostSignin>b__1_1] - -99:The method or operation is not implemented.
INFO [oelMVCS::Dismantle] - dismantle framework
INFO [oelMVCS::Cancel] - cancel SimpleService
INFO [oelMVCS::Cancel] - cancel SimpleController
INFO [oelMVCS::Cancel] - cancel SimpleView
INFO [oelMVCS::Cancel] - cancel SimpleModel
INFO [oelMVCS::Release] - release framework
```

其中有一条Error，这是因为并没有http://localhost/signin这个服务。先使用模拟的方式。改写主函数如下：



```csharp
 class Program
    {
        class SimpleMock
        {
            public static void MockProcessor(string _url, string _method, 
            	Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, 
                Service.OnErrorCallback _onError, Service.Options _options)
            {
                if (!_url.Equals("http://localhost/signin"))
                {
                    _onError(Error.NewAccessErr("404 not found"));
                    return;
                }
                _onReply("OK");
            }
        }

        static void Main(string[] args)
        {
            //testAny1();
            Framework framework = new Framework();
            framework.setLogger(new ConsoleLogger());

            framework.Initialize();

            framework.getStaticPipe().RegisterModel("SimpleModel", new SimpleModel());
            SimpleView simpleView = new SimpleView();
            framework.getStaticPipe().RegisterView("SimpleView", simpleView);
            framework.getStaticPipe().RegisterController("SimpleController", new SimpleController());
            SimpleService service = new SimpleService();
            service.useMock = true;
            service.MockProcessor = SimpleMock.MockProcessor;
            framework.getStaticPipe().RegisterService("SimpleService", service);

            framework.Setup();

            // 主循环
            // 模拟登录按钮被点击时
            {
                simpleView.OnSigninClicked();
            }

            framework.Dismantle();
            framework.getStaticPipe().CancelService("SimpleService");
            framework.getStaticPipe().CancelController("SimpleController");
            framework.getStaticPipe().CancelView("SimpleView");
            framework.getStaticPipe().CancelModel("SimpleModel");

            framework.Release();
        }
 }
```

运行后控制台输出如下：
```
INFO [oelMVCS::Initialize] - initialize framework
INFO [oelMVCS::Register] - register SimpleModel
INFO [oelMVCS::Register] - register SimpleView
INFO [oelMVCS::Register] - register SimpleController
INFO [oelMVCS::Register] - register SimpleService
INFO [oelMVCS::Setup] - setup framework
INFO [oelMVCS::setup] - setup SimpleModel
INFO [oelMVCS::setup] - setup SimpleView
INFO [oelMVCS::setup] - setup SimpleController
INFO [oelMVCS::setup] - setup SimpleService
INFO [oelMVCS::<PostSignin>b__1_0] - OK
INFO [oelMVCS::Dismantle] - dismantle framework
INFO [oelMVCS::Cancel] - cancel SimpleService
INFO [oelMVCS::Cancel] - cancel SimpleController
INFO [oelMVCS::Cancel] - cancel SimpleView
INFO [oelMVCS::Cancel] - cancel SimpleModel
INFO [oelMVCS::Release] - release framework
```

INFO [oelMVCS::<PostSignin>b__1_0] - OK 请求成功。

useMock用于设置服务是否以模拟的方式请求，如果设置为true，需要设置服务的MockProcessor委托。useMock可以方便的在本地模拟和真实远端之间切换。



## 简单范例

```csharp
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
            if(_reply.getCode() != 0)
            {
                view.PrintError(_reply.getMessage());
                return;
            }
            _status.uuid = _uuid;
            view.PrintUUID(_status);
        }
    }

    class SimpleService : Service
    {
        private SimpleModel model { get; set; }
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
            post("http://localhost/signin", parameter, (_reply) =>
            {
                Model.Status reply = Model.Status.New<Model.Status>(0, "");
                string uuid = _reply;
                model.UpdateSignin(reply, uuid);
            }, (_err) =>
            {
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateSignin(reply, "");
            }, null);
        }
    }

   


    class Program
    {
        class SimpleMock
        {
            public static void MockProcessor(string _url, string _method, Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, Service.OnErrorCallback _onError, Service.Options _options)
            {
                if (!_url.Equals("http://localhost/signin"))
                {
                    _onError(Error.NewAccessErr("404 not found"));
                    return;
                }
                if(_params["username"].AsString().Equals("admin") && _params["password"].AsString().Equals("11223344"))
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

            framework.getStaticPipe().RegisterModel("SimpleModel", new SimpleModel());
            SimpleView simpleView = new SimpleView();
            framework.getStaticPipe().RegisterView("SimpleView", simpleView);
            framework.getStaticPipe().RegisterController("SimpleController", new SimpleController());
            SimpleService service = new SimpleService();
            service.useMock = true;
            service.MockProcessor = SimpleMock.MockProcessor;
            framework.getStaticPipe().RegisterService("SimpleService", service);

            framework.Setup();

            // 主循环
            // 模拟登录按钮被点击时
            {
                simpleView.OnSigninClicked();
            }

            framework.Dismantle();
            framework.getStaticPipe().CancelService("SimpleService");
            framework.getStaticPipe().CancelController("SimpleController");
            framework.getStaticPipe().CancelView("SimpleView");
            framework.getStaticPipe().CancelModel("SimpleModel");

            framework.Release();
        }
    }
}

```

 登录成功时的打印如下

```
INFO [oelMVCS::Initialize] - initialize framework
INFO [oelMVCS::Register] - register SimpleModel
INFO [oelMVCS::Register] - register SimpleView
INFO [oelMVCS::Register] - register SimpleController
INFO [oelMVCS::Register] - register SimpleService
INFO [oelMVCS::Setup] - setup framework
INFO [oelMVCS::PushStatus] - push status SimpleStatus
INFO [oelMVCS::setup] - setup SimpleModel
INFO [oelMVCS::setup] - setup SimpleView
INFO [oelMVCS::setup] - setup SimpleController
INFO [oelMVCS::setup] - setup SimpleService
ERROR [oelMVCS::PrintError] - password is not matched         //<-------
INFO [oelMVCS::Dismantle] - dismantle framework
INFO [oelMVCS::PopStatus] - pop status SimpleStatus
INFO [oelMVCS::Cancel] - cancel SimpleService
INFO [oelMVCS::Cancel] - cancel SimpleController
INFO [oelMVCS::Cancel] - cancel SimpleView
INFO [oelMVCS::Cancel] - cancel SimpleModel
INFO [oelMVCS::Release] - release framework
```

将service.PostSignin("admin", "11223344");改为service.PostSignin("admin", "11");，登录失败时的打印如下

```
INFO [oelMVCS::Initialize] - initialize framework
INFO [oelMVCS::Register] - register SimpleModel
INFO [oelMVCS::Register] - register SimpleView
INFO [oelMVCS::Register] - register SimpleController
INFO [oelMVCS::Register] - register SimpleService
INFO [oelMVCS::Setup] - setup framework
INFO [oelMVCS::PushStatus] - push status SimpleStatus
INFO [oelMVCS::setup] - setup SimpleModel
INFO [oelMVCS::setup] - setup SimpleView
INFO [oelMVCS::setup] - setup SimpleController
INFO [oelMVCS::setup] - setup SimpleService
ERROR [oelMVCS::PrintError] - password is not matched             //<------
INFO [oelMVCS::Dismantle] - dismantle framework
INFO [oelMVCS::PopStatus] - pop status SimpleStatus
INFO [oelMVCS::Cancel] - cancel SimpleService
INFO [oelMVCS::Cancel] - cancel SimpleController
INFO [oelMVCS::Cancel] - cancel SimpleView
INFO [oelMVCS::Cancel] - cancel SimpleModel
INFO [oelMVCS::Release] - release framework
```



