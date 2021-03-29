using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            public View view
            {
                get;
                private set;
            }


            public Inner(View _view)
            {
                view = _view;
            }

            public void Setup(Board _board)
            {
                view.board_ = _board;
                view.board_.logger.Trace("setup view");
                view.setup();
                view.bindEvents();
            }

            public void Dismantle()
            {
                view.board_.logger.Trace("dismantle view");
                view.unbindEvents();
                view.dismantle();
            }

            public Error Handle(string _action, Model.Status _status, object _data)
            {
                if (!view.handlers.ContainsKey(_action))
                    return Error.NewParamErr("handler {0} exists", _action);
                view.handlers[_action](_status, _data);
                return Error.OK;
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

        private Dictionary<string, Action<Model.Status, object>> handlers = new Dictionary<string, Action<Model.Status, object>>();


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

        // 派生类需要实现的方法
        protected virtual void bindEvents()
        {

        }

        // 派生类需要实现的方法
        protected virtual void unbindEvents()
        {

        }

        /// <summary>
        /// 控制层的拆卸
        /// </summary>
        protected virtual void dismantle()
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
            handlers[_action] = _handler;
        }
    }
}//namespace
