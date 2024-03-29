﻿using System.Collections.Generic;

namespace XTC.oelMVCS
{

    /// <summary>
    /// 数据单元
    /// </summary>
    public class Model 
    {
        #region
        // 内部类，用于接口隔离,隐藏Model无需暴露给外部的公有方法
        public class Inner
        {
            public Inner(Model _unit)
            {
                unit_ = _unit;
            }

            public Model getUnit()
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

            private Model unit_ = null;
        }
        #endregion

        #region
        /// <summary> 状态 </summary>
        public class Status
        {
            // 内部工厂，用于赋值私有成员
            public class Factory
            {
                public T New<T>(Board _board, string _uuid) where T: Status, new ()
                {
                    T status = new T();
                    status.board_ = _board;
                    status.uuid_ = _uuid;
                    return status;
                }
            }

            /// <summary>获取状态码</summary>
            public int getCode()
            {
                return code_;
            }

            /// <summary>获取状态信息</summary>
            public string getMessage()
            {
                return message_;
            }

            /// <summary>获取唯一识别码</summary>
            public string getUuid()
            {
                return uuid_;
            }

            public Status()
            {
            }

            /// <summary> 访问数据中心中的已注册状态 </summary>
            /// <param name="_uuid"> 状态的唯一识别码 </param>
            public Status Access(string _uuid)
            {
                if (string.IsNullOrEmpty(_uuid))
                    return null;
                return board_.getModelCenter().FindStatus(_uuid);
            }

            protected int code_ = 0;
            protected string message_ = "";
            private string uuid_ = "";
            private Board board_ = null;
        }
        #endregion


        /*
        public Dictionary<string, object> property
        {
            get;
            private set;
        }
        */


        /// <summary>向视图层广播消息</summary>
        /// <param name="_action">行为</param>
        /// <param name="_data">数据</param>
        public void Broadcast(string _action, object _data)
        {
            board_.getModelCenter().Broadcast(_action, status_, _data);
        }


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
        /// 从数据层中创建一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        /// <returns>状态<returns>
        protected Model.Status spawnStatus<T>(string _uuid, out Error _err) where T: Model.Status, new ()
        {
            Model.Status.Factory factory = new Model.Status.Factory();
            Status status = factory.New<T>(board_, _uuid);
            _err = board_.getModelCenter().PushStatus(_uuid, status);
            if (Error.IsOK(_err))
                return status;
            return null;
        }

        /// <summary>
        /// 从数据中心中销毁一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        protected void killStatus(string _uuid, out Error _err)
        {
            _err = board_.getModelCenter().PopStatus(_uuid);
        }

        protected Status status_ = null;

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
