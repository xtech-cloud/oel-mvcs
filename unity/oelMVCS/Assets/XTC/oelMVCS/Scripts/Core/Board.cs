namespace XTC.oelMVCS
{
    // 内部通讯的主板
    // get和set方法为oel的多语言规范，不要直接使用C#的属性存取
    public class Board
    {
        public void setServiceCenter(ServiceCenter _value)
        {
            serviceCenter_ = _value;
        }

        public void setControllerCenter(ControllerCenter _value)
        {
            controllerCenter_ = _value;
        }

        public void setViewCenter(ViewCenter _value)
        {
            viewCenter_ = _value;
        }

        public void setModelCenter(ModelCenter _value)
        {
            modelCenter_ = _value;
        }

        public void setConfig(Config _value)
        {
            config_ = _value;
        }

        public void setLogger(Logger _logger)
        {
            logger_ = _logger;
        }

        public Config getConfig()
        {
            return config_;
        }

        public Logger getLogger()
        {
            return logger_;
        }

        public ServiceCenter getServiceCenter()
        {
            return serviceCenter_;
        }

        public ControllerCenter getControllerCenter()
        {
            return controllerCenter_;
        }

        public ViewCenter getViewCenter()
        {
            return viewCenter_;
        }

        public ModelCenter getModelCenter()
        {
            return modelCenter_;
        }

        private ServiceCenter serviceCenter_ = null;

        private ControllerCenter controllerCenter_ = null;

        private ViewCenter viewCenter_ = null;

        private ModelCenter modelCenter_ = null;

        private Config config_ = null;

        private Logger logger_ = null;
    }
}
