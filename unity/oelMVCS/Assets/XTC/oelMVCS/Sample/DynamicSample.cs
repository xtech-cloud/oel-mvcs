using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XTC.oelMVCS;

public class DynamicSample: MonoBehaviour
{
    public Button btnLoad;
    public Button btnDestroy;

    private SampleModel model {get; set;}
    private SampleView view {get; set;}
    private SampleController controller  {get; set;}
    private SampleService service  {get; set;}

    private GameObject uiFacade;
    private Framework framework = new Framework();

    void Awake()
    {
        Debug.Log("---------------  Awake ------------------------");
        XTC.oelMVCS.Logger logger = new UnityLogger();
        UIFacade.logger = logger;
        framework.setLogger(logger);
        framework.setConfig(new Config());

        btnLoad.onClick.AddListener(() =>
        {
            btnLoad.gameObject.SetActive(false);
            btnDestroy.gameObject.SetActive(true);

            GameObject go = Resources.Load<GameObject>("SampleFacade");
            uiFacade = GameObject.Instantiate(go);
            uiFacade.transform.SetParent(btnLoad.transform.parent);
            uiFacade.transform.localScale = Vector3.one;

            RectTransform rt = uiFacade.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;

            uiFacade.GetComponent<UIFacade>().Register();

            model = new SampleModel();
            view = new SampleView();
            controller = new SampleController();
            service = new SampleService();

            service.MockProcessor = this.mockProcessor;
            service.useMock = true;

            framework.getDynamicPipe().PushModel(SampleModel.NAME, model);
            framework.getDynamicPipe().PushView(SampleView.NAME, view);
            framework.getDynamicPipe().PushController(SampleController.NAME, controller);
            framework.getDynamicPipe().PushService(SampleService.NAME, service);
        });

        btnDestroy.onClick.AddListener(() =>
        {
            btnLoad.gameObject.SetActive(true);
            btnDestroy.gameObject.SetActive(false);

            framework.getDynamicPipe().PopModel(SampleModel.NAME);
            framework.getDynamicPipe().PopView(SampleView.NAME);
            framework.getDynamicPipe().PopController(SampleController.NAME);
            framework.getDynamicPipe().PopService(SampleService.NAME);

            if(uiFacade != null)
            {
                uiFacade.GetComponent<UIFacade>().Cancel();
                GameObject.Destroy(uiFacade);
                uiFacade = null;
            }
        });

        framework.Initialize();
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
            if (_params["username"].AsString().Equals("admin") && _params["password"].AsString().Equals("admin"))
                _onReply("ok");
            else
                _onError(Error.NewAccessErr(""));
            yield break;
        }

        _onError(Error.NewAccessErr(""));
    }

}
