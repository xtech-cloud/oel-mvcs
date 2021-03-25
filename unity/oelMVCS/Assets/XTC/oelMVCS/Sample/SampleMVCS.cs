using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XTC.oelMVCS;

public class SampleModel : Model
{
    public const string NAME = "SampleModel";

    public class SampleStatus : Model.Status
    {
        public string latestError = null;
        public string currentMessage = "";
        public List<string> messages = new List<string>();
    }

    protected override void setup()
    {
        Debug.Log("setup SampleModel");
        status_ = new SampleStatus();
        //status_.SetModelCenter(center_);
        //status_.Register(NAME);
    }

    protected override void dismantle()
    {
        //status_.Cancel();
        Debug.Log("dismantle SampleModel");
    }

    private SampleController controller
    {
        get
        {
            return findController(SampleController.NAME) as SampleController;
        }
    }

    private SampleStatus status
    {
        get
        {
            return status_ as SampleStatus;
        }
    }

    public void UpdateLoginResult(string _err)
    {
        status.latestError = _err;
        controller.UpdateLoginResult(status);
    }
}

public class SampleView : View
{
    public const string NAME = "SampleView";

    public LoginUI uiLogin
    {
        get
        {
            return (UIFacade.Find("SampleUI") as SampleUIFacade).uiLogin;
        }
    }

    public HomeUI uiHome
    {
        get
        {
            return (UIFacade.Find("SampleUI") as SampleUIFacade).uiHome;
        }
    }

    private SampleService service
    {
        get
        {
            return findService(SampleService.NAME) as SampleService;
        }
    }

    private SampleModel model
    {
        get
        {
            return findModel(SampleModel.NAME) as SampleModel;
        }
    }

    protected override void setup()
    {
        Debug.Log("setup SampleView");
        uiLogin.root.SetActive(true);
        uiHome.root.SetActive(false);
        route("/simple/update", (_status, _data)=>{
            Debug.Log(_status.uuid);
        });
    }

    protected override void bindEvents()
    {
        uiLogin.btnLogin.onClick.AddListener(this.onLoginClick);
    }

    protected override void unbindEvents()
    {
        uiLogin.btnLogin.onClick.RemoveListener(this.onLoginClick);
    }

    protected override void dismantle()
    {
        Debug.Log("dismantle SampleView");
    }

    private void onLoginClick()
    {
        uiLogin.txtTip.text = "";
        service.CallLogin(uiLogin.inputUsername.text, uiLogin.inputPassword.text);
        model.Broadcast("/simple/update", null);
    }

    public void RefreshError(string _error)
    {
        uiLogin.txtTip.text = _error;
    }

    public void SwitchHomePage()
    {
        uiLogin.root.SetActive(false);
        uiHome.root.SetActive(true);
    }
}

public class SampleController : Controller
{
    public const string NAME = "SampleController";

    protected override void setup()
    {
        Debug.Log("setup SampleController");
    }

    protected override void dismantle()
    {
        Debug.Log("dismantle SampleController");
    }

    private SampleView view
    {
        get
        {
            return findView(SampleView.NAME) as SampleView;
        }
    }

    public void UpdateLoginResult(SampleModel.SampleStatus _status)
    {
        if (null == _status.latestError)
        {
            view.SwitchHomePage();
        }
        else
        {
            view.RefreshError(_status.latestError);
        }
    }
}

public class SampleService : Service
{
    public const string NAME = "SampleService";

    protected override void setup()
    {
        Debug.Log("setup SampleService");
    }

    protected override void dismantle()
    {
        Debug.Log("dismantle SampleService");
    }

    private SampleModel model
    {
        get
        {
            return findModel(SampleModel.NAME) as SampleModel;
        }
    }

    public void CallLogin(string _username, string _password)
    {
        Dictionary<string, Any> param = new Dictionary<string, Any>();
        param.Add("username", new Any(_username));
        param.Add("password", new Any(_password));
        post("/login", param, (_reply) =>
        {
            if (_reply.Equals("ok"))
                model.UpdateLoginResult(null);
            else
                model.UpdateLoginResult(_reply);
        }, (_error) =>
        {
            model.UpdateLoginResult(_error);
        }, null);
    }
}

public class SampleMVCS : MonoBehaviour
{
    public UIFacade[] uIFacades;


    private Framework framework = new Framework();


    void Awake()
    {
        Debug.Log("---------------  Awake ------------------------");
        XTC.oelMVCS.Logger logger = new XTC.oelMVCS.Logger();
        UIFacade.logger = logger;
        
        foreach(UIFacade facade in uIFacades)
            facade.Register();

        framework.logger = logger;
        framework.config = new Config();
        framework.Initialize();

        SampleModel model = new SampleModel();
        SampleView view = new SampleView();
        SampleController controller = new SampleController();
        SampleService service = new SampleService();

        service.domain = "http://127.0.0.1";
        service.MockProcessor = this.mockProcessor;
        service.useMock = true;

        framework.staticPipe.RegisterModel(SampleModel.NAME, model);
        framework.staticPipe.RegisterView(SampleView.NAME, view);
        framework.staticPipe.RegisterController(SampleController.NAME, controller);
        framework.staticPipe.RegisterService(SampleService.NAME, service);
    }

    void OnEnable()
    {
        Debug.Log("---------------  OnEnable ------------------------");
        framework.Setup();
    }

    void OnDisable()
    {
        Debug.Log("---------------  OnDisable ------------------------");
        framework.Dismantle();
    }

    void OnDestroy()
    {
        Debug.Log("---------------  OnDestroy ------------------------");

        framework.staticPipe.CancelModel(SampleModel.NAME);
        framework.staticPipe.CancelView(SampleView.NAME);
        framework.staticPipe.CancelController(SampleController.NAME);
        framework.staticPipe.CancelService(SampleService.NAME);

        foreach(UIFacade facade in uIFacades)
            facade.Cancel();
            
        framework.Release();
    }

    void mockProcessor(string _url, string _method, Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, Service.OnErrorCallback _onError, Service.Options _options)
    {
        this.StartCoroutine(asyncMockProcessor(_url, _method, _params, _onReply, _onError, _options));
    }

    IEnumerator asyncMockProcessor(string _url, string _method, Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, Service.OnErrorCallback _onError, Service.Options _options)
    {
        yield return new WaitForEndOfFrame();

        if (_url.EndsWith("/login"))
        {
            if (_params["username"].AsString.Equals("admin") && _params["password"].AsString.Equals("admin"))
                _onReply("ok");
            else
                _onError("errcode:1");
            yield break;
        }

        _onError("");
    }
}
