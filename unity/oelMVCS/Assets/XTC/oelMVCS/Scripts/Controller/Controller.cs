﻿namespace XTC.oelMVCS
{
    /// <summary>
    /// 控制单元
    /// </summary>
    public class Controller
    {
        #region
        // 内部类，用于接口隔离,隐藏Controller无需暴露给外部的公有方法
        public class Inner
        {
            public Inner(Controller _unit)
            {
                unit_ = _unit;
            }

            public Controller getUnit()
            {
                return unit_;
            }

            public void Setup(Board _board)
            {
                unit_.board_ = _board;
                unit_.setup();
            }

            public void Dismantle()
            {
                unit_.dismantle();
            }

            private Controller unit_ = null;
        }
        #endregion

        /// <summary>
        /// 查找一个数据层
        /// </summary>
        /// <param name="_uuid"> 数据层唯一识别码</param>
        /// <returns>找到的数据层</returns>
        protected Model findModel(string _uuid)
        {
            Model.Inner inner = board_.getModelCenter().FindUnit(_uuid);
            if (null == inner)
                return null;
            return inner.getUnit();
        }

        /// <summary>
        /// 查找一个视图层
        /// </summary>
        /// <param name="_uuid"> 视图层唯一识别码</param>
        /// <returns>找到的视图层</returns>
        protected View findView(string _uuid)
        {
            View.Inner inner = board_.getViewCenter().FindUnit(_uuid);
            if (null == inner)
                return null;
            return inner.getUnit();
        }

        /// <summary>
        /// 查找一个控制层
        /// </summary>
        /// <param name="_uuid"> 控制层唯一识别码</param>
        /// <returns>找到的控制层</returns>
        protected Controller findController(string _uuid)
        {
            Controller.Inner inner = board_.getControllerCenter().FindUnit(_uuid);
            if (null == inner)
                return null;
            return inner.getUnit();
        }

        /// <summary>
        /// 查找一个服务层
        /// </summary>
        /// <param name="_uuid"> 服务层唯一识别码</param>
        /// <returns>找到的服务层</returns>
        protected Service findService(string _uuid)
        {
            Service.Inner inner = board_.getServiceCenter().FindUnit(_uuid);
            if (null == inner)
                return null;
            return inner.getUnit();
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns>
        /// 日志实列
        /// </returns>
        protected Logger getLogger()
        {
            return board_.getLogger();
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>
        /// 配置实列
        /// </returns>
        protected Config getConfig()
        {
            return board_.getConfig();
        }

        /// <summary>
        /// 单元的安装
        /// </summary>
        protected virtual void setup()
        {

        }

        /// <summary>
        /// 单元的拆卸
        /// </summary>
        protected virtual void dismantle()
        {

        }

        private Board board_ = null;
    }
}//namespace MVCS
