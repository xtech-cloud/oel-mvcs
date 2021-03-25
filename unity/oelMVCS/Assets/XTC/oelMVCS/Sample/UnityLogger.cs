public class UnityLogger : XTC.oelMVCS.Logger
{
    protected override void trace(string _categoray, string _message)
    {
        UnityEngine.Debug.LogFormat("{0} > {1}", _categoray, _message);
    }

    protected override void debug(string _categoray, string _message)
    {
        UnityEngine.Debug.LogFormat("{0} > {1}", _categoray, _message);
    }

    protected override void info(string _categoray, string _message)
    {
        UnityEngine.Debug.LogFormat("{0} > {1}", _categoray, _message);
    }

    protected override void warning(string _categoray, string _message)
    {
        UnityEngine.Debug.LogWarningFormat("{0} > {1}", _categoray, _message);
    }

    protected override void error(string _categoray, string _message)
    {
        UnityEngine.Debug.LogErrorFormat("{0} > {1}", _categoray, _message);
    }

    protected override void exception(System.Exception _exception)
    {
        UnityEngine.Debug.LogException(_exception);
    }
}
