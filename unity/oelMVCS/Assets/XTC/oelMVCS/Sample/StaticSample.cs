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

        framework.setLogger(logger);
        framework.setConfig(new Config());
        framework.Initialize();

        SampleModel model = new SampleModel();
        SampleView view = new SampleView();
        SampleController controller = new SampleController();
        SampleService service = new SampleService();
        service.mono = this;

        service.MockProcessor = service.mockProcessor;
        service.useMock = true;

        framework.getStaticPipe().RegisterModel(SampleModel.NAME, model);
        framework.getStaticPipe().RegisterView(SampleView.NAME, view);
        framework.getStaticPipe().RegisterController(SampleController.NAME, controller);
        framework.getStaticPipe().RegisterService(SampleService.NAME, service);
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

        framework.getStaticPipe().CancelModel(SampleModel.NAME);
        framework.getStaticPipe().CancelView(SampleView.NAME);
        framework.getStaticPipe().CancelController(SampleController.NAME);
        framework.getStaticPipe().CancelService(SampleService.NAME);

        foreach(UIFacade facade in uIFacades)
            facade.Cancel();
            
        framework.Release();
    }

}
