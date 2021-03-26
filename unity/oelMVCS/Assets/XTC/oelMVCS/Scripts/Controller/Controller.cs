namespace XTC.oelMVCS
{
    public class Controller
    {
        #region
        // 内部类，用于接口隔离,隐藏Controller无需暴露给外部的公有方法
        public class Inner
        {
            public Controller controller
            {
                get;
                private set;
            }

            public Inner(Controller _controller)
            {
                controller = _controller;
            }

            internal void Setup(Board _board)
            {
                controller.board_ = _board;
                controller.board_.logger.Trace("setup controller");
                controller.setup();
            }

            internal void Dismantle()
            {
                controller.board_.logger.Trace("dismantle controller");
                controller.dismantle();
            }
        }
        #endregion

        protected Logger logger_
        {
            get
            {
                return board_.logger;
            }
        }

        protected Config config_
        {
            get
            {
                return board_.config;
            }
        }

        private Board board_
        {
            get;
            set;
        }

        protected Model findModel(string _uuid)
        {
            Model.Inner inner = board_.modelCenter.FindModel(_uuid);
            if (null == inner)
                return null;
            return inner.model;
        }

        protected View findView(string _uuid)
        {
            View.Inner inner = board_.viewCenter.FindView(_uuid);
            if (null == inner)
                return null;
            return inner.view;
        }

        protected Controller findController(string _uuid)
        {
            Controller.Inner inner = board_.controllerCenter.FindController(_uuid);
            if (null == inner)
                return null;
            return inner.controller;
        }

        protected Service findService(string _uuid)
        {
            Service.Inner inner = board_.serviceCenter.FindService(_uuid);
            if (null == inner)
                return null;
            return inner.service;
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
