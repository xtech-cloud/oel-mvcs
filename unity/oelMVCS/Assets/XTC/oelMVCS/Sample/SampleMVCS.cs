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
        Error err;
        status_ = this.spawnStatus<SampleStatus>("simple", (_board, _uuid) =>
        {
            return new SampleStatus(_board, _uuid);
        }, out err);
        Model.Status test = this.spawnStatus<SampleStatus>("test", (_board, _uuid) =>
        {
            return new SampleStatus(_board, _uuid);
        }, out err);
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
        route("/simple/update", (_status, _data) =>
        {
            Debug.Log(_status);
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

public class SampleService : UnityService
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
            model.UpdateLoginResult(_error.message);
        }, null);
    }
}
