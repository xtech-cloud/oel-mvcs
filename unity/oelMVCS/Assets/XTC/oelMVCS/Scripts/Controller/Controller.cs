namespace XTC.oelMVCS
{
    public class Controller
    {
        protected Logger logger_
        {
            get;
            set;
        }

        protected Config config_
        {
            get;
            set;
        }

        private ControllerCenter controllerCenter_
        {
            get;
            set;
        }

        private ViewCenter viewCenter_
        {
            get;
            set;
        }

        private ModelCenter modelCenter_
        {
            get;
            set;
        }

        private ServiceCenter serviceCenter_
        {
            get;
            set;
        }

        internal void Setup(Board _board)
        {
            logger_ = _board.logger;
            config_ = _board.config;
            controllerCenter_ = _board.controllerCenter;
            viewCenter_ = _board.viewCenter;
            serviceCenter_ = _board.serviceCenter;
            modelCenter_ = _board.modelCenter;

            logger_.Trace("setup controller");
            setup();
        }

        internal void Dismantle()
        {
            logger_.Trace("dismantle controller");
            dismantle();
        }

        protected Model findModel(string _uuid)
        {
            return modelCenter_.FindModel(_uuid);
        }

        protected View findView(string _uuid)
        {
            return viewCenter_.FindView(_uuid);
        }

        protected Controller findController(string _uuid)
        {
            return controllerCenter_.FindController(_uuid);
        }

        protected Service findService(string _uuid)
        {
            return serviceCenter_.FindService(_uuid);
        }


        // 派生类需要实现的方法
        protected virtual void setup()
        {

        }

        // 派生类需要实现的方法
        protected virtual void dismantle()
        {

        }
    }
}//namespace MVCS
