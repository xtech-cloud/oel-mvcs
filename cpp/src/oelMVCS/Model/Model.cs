using System.Collections.Generic;

namespace XTC.oelMVCS
{

    /// <summary>
    /// 数据层
    /// </summary>
    public class Model
    {
        #region
        // 内部类，用于接口隔离,隐藏Model无需暴露给外部的公有方法
        public class Inner
        {
            public Model model
            {
                get;
                private set;
            }

            public Inner(Model _model)
            {
                model = _model;
            }

            internal void Setup(Board _board)
            {
                model.board_ = _board;
                model.board_.logger.Trace("setup model");
                model.setup();
            }

            internal void Dismantle()
            {
                model.board_.logger.Trace("dismantle model");
                model.dismantle();
            }
        }
        #endregion

        /// <summary> 状态 </summary>
        public class Status
        {
            // 内部工厂，用于赋值私有成员
            public class Factory
            {
                public T New<T>(Board _board, string _uuid) where T:Status, new()
                {
                    T status = new T();
                    status.board_ = _board;
                    status.uuid = _uuid;
                    return status;
                }
            }

            /// 错误码
            public int code;
            /// 错误信息
            public string message;

            private Board board_
            {
                get;
                set;
            }

            /// 数据层中心中的唯一识别码
            public string uuid
            {
                get;
                private set;
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
                return board_.modelCenter.FindStatus(_uuid);
            }
        }



        /*
        public Dictionary<string, object> property
        {
            get;
            private set;
        }
        */

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


        protected Status status_
        {
            get;
            set;
        }

        private Board board_
        {
            get;
            set;
        }

        /// <summary>向视图层广播消息</summary>
        /// <param name="_action">行为</param>
        /// <param name="_data">数据</param>
        public void Broadcast(string _action, object _data)
        {
            board_.modelCenter.Broadcast(_action, status_, _data);
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
        /// 从数据层中创建一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        /// <returns>状态<returns>
        protected Model.Status spawnStatus<T>(string _uuid, out Error _err) where T: Model.Status, new ()
        {
            Model.Status.Factory factory = new Model.Status.Factory();
            Status status = factory.New<T>(board_, _uuid);
            _err = board_.modelCenter.PushStatus(_uuid, status);
            if (_err.IsOK)
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
            _err = board_.modelCenter.PopStatus(_uuid);
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
}//namespace
