using System;
using System.Collections.Generic;

namespace XTC.oelMVCS
{
    /// <summary>
    /// 视图层
    /// </summary>
    public class View
    {
        #region
        // 内部类，用于接口隔离,隐藏View无需暴露给外部的公有方法
        public class Inner
        {
            public Inner(View _unit)
            {
                unit_ = _unit;
            }

            public View getUnit()
            {
                return unit_;
            }

            public void Setup(Board _board)
            {
                unit_.board_ = _board;
                unit_.setup();
                unit_.bindEvents();
            }

            public void Dismantle()
            {
                unit_.unbindEvents();
                unit_.dismantle();
            }

            public Error Handle(string _action, Model.Status _status, object _data)
            {
                if (!unit_.handlers_.ContainsKey(_action))
                    return Error.NewParamErr("handler {0} exists", _action);
                unit_.handlers_[_action](_status, _data);
                return Error.OK;
            }

            private View unit_ = null;
        }
        #endregion

        private Dictionary<string, Action<Model.Status, object>> handlers_ = new Dictionary<string, Action<Model.Status, object>>();


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

        // 派生类需要实现的方法
        protected virtual void bindEvents()
        {

        }

        // 派生类需要实现的方法
        protected virtual void unbindEvents()
        {

        }

        /// <summary>
        /// 行为路由，使用指定函数处理指定行为
        /// 设置了路由的行为，在数据层进行广播时，会自动调用相应的处理函数
        /// </summary>
        /// <param name="_action">需要处理的行为</param>
        /// <param name="_action">行为对应的处理函数</param>
        protected void route(string _action, Action<Model.Status, object> _handler)
        {
            handlers_[_action] = _handler;
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
}//namespace
