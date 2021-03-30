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
        getLogger().Trace("setup SampleModel");
        Error err;
        status_ = this.spawnStatus<SampleStatus>("simple", out err);
        this.spawnStatus<SampleStatus>("test", out err);
    }

    protected override void dismantle()
    {
        //status_.Cancel();
        getLogger().Trace("dismantle SampleModel");
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
        getLogger().Trace("setup SampleView");
        uiLogin.root.SetActive(true);
        uiHome.root.SetActive(false);
        route("/simple/update", (_status, _data) =>
        {
            getLogger().Trace(_status.ToString());
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
        getLogger().Trace("dismantle SampleView");
    }

    private void onLoginClick()
    {
        getLogger().Trace("click login");
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
        getLogger().Trace("setup SampleController");
    }

    protected override void dismantle()
    {
        getLogger().Trace("dismantle SampleController");
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
        // 使用状态访问已注册的其他状态
        SampleModel.SampleStatus testStatus =  _status.Access("test") as SampleModel.SampleStatus;
        getLogger().Debug(testStatus.getUuid());

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

public class SampleService : UnityService
{
    public const string NAME = "SampleService";

    protected override void setup()
    {
        getLogger().Trace("setup SampleService");
    }

    protected override void dismantle()
    {
        getLogger().Trace("dismantle SampleService");
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
        param.Add("username", Any.FromString(_username));
        param.Add("password", Any.FromString(_password));
        post("/login", param, (_reply) =>
        {
            if (_reply.Equals("ok"))
                model.UpdateLoginResult(null);
            else
                model.UpdateLoginResult(_reply);
        }, (_error) =>
        {
            model.UpdateLoginResult("error");
        }, null);
    }

    public void mockProcessor(string _url, string _method, Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, Service.OnErrorCallback _onError, Service.Options _options)
    {
        Debug.Log("use mock processor");
        mono.StartCoroutine(asyncMockProcessor(_url, _method, _params, _onReply, _onError, _options));
    }

    IEnumerator asyncMockProcessor(string _url, string _method, Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, Service.OnErrorCallback _onError, Service.Options _options)
    {
        yield return new WaitForEndOfFrame();

        if (_url.EndsWith("/login"))
        {
            if (_params["username"].AsString().Equals("admin") && _params["password"].AsString().Equals("admin"))
                _onReply("ok");
            else
                _onError(Error.NewAccessErr(""));
            yield break;
        }

        _onError(Error.NewAccessErr("404 not found"));
    }
}
