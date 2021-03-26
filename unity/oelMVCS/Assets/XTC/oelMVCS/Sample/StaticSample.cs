using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XTC.oelMVCS;

public class StaticSample: MonoBehaviour
{
    public UIFacade[] uIFacades;


    private Framework framework = new Framework();


    void Awake()
    {
        Debug.Log("---------------  Awake ------------------------");
        XTC.oelMVCS.Logger logger = new UnityLogger();
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
                _onError(Error.NewAccessErr(""));
            yield break;
        }

        _onError(Error.NewAccessErr(""));
    }
}
