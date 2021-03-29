namespace XTC.oelMVCS
{
    /// <summary>
    /// 控制层
    /// </summary>
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

        /// <summary>
        /// 查找一个数据层
        /// </summary>
        /// <param name="_uuid"> 数据层唯一识别码</param>
        /// <returns>找到的数据层</returns>
        protected Model findModel(string _uuid)
        {
            Model.Inner inner = board_.modelCenter.FindModel(_uuid);
            if (null == inner)
                return null;
            return inner.model;
        }

        /// <summary>
        /// 查找一个视图层
        /// </summary>
        /// <param name="_uuid"> 视图层唯一识别码</param>
        /// <returns>找到的视图层</returns>
        protected View findView(string _uuid)
        {
            View.Inner inner = board_.viewCenter.FindView(_uuid);
            if (null == inner)
                return null;
            return inner.view;
        }

        /// <summary>
        /// 查找一个控制层
        /// </summary>
        /// <param name="_uuid"> 控制层唯一识别码</param>
        /// <returns>找到的控制层</returns>
        protected Controller findController(string _uuid)
        {
            Controller.Inner inner = board_.controllerCenter.FindController(_uuid);
            if (null == inner)
                return null;
            return inner.controller;
        }

        /// <summary>
        /// 查找一个服务层
        /// </summary>
        /// <param name="_uuid"> 服务层唯一识别码</param>
        /// <returns>找到的服务层</returns>
        protected Service findService(string _uuid)
        {
            Service.Inner inner = board_.serviceCenter.FindService(_uuid);
            if (null == inner)
                return null;
            return inner.service;
        }

        /// <summary>
        /// 控制层的安装
        /// </summary>
        protected virtual void setup()
        {

        }

        /// <summary>
        /// 控制层的拆卸
        /// </summary>
        protected virtual void dismantle()
        {

        }
    }
}//namespace MVCS
