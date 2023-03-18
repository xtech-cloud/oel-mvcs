﻿/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.oelMVCS
{
    /// <summary>
    /// 数据单元
    /// </summary>
    public class Model
    {
        #region
        // 内部类，用于接口隔离,隐藏Model无需暴露给外部的公有方法
        internal class Inner
        {
            internal Inner(Model _unit, Board _board)
            {
                unit_ = _unit;
                unit_.board_ = _board;
            }

            public Model getUnit()
            {
                return unit_;
            }

            public void PreSetup()
            {
                unit_.preSetup();
            }

            public void Setup()
            {
                unit_.setup();
            }

            public void PostSetup()
            {
                unit_.postSetup();
            }

            public void PreDismantle()
            {
                unit_.preDismantle();
            }

            public void Dismantle()
            {
                unit_.dismantle();
            }

            public void PostDismantle()
            {
                unit_.postDismantle();
            }

            private Model unit_;
        }
        #endregion

        #region
        /// <summary> 状态 </summary>
        public class Status
        {
            // 内部工厂，用于赋值私有成员
            internal class Factory
            {
                public T New<T>(Board _board, string _uuid) where T : Status, new()
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

            public static T New<T>(int _code, string _message) where T : Status, new()
            {
                T status = new T();
                status.code_ = _code;
                status.message_ = _message;
                return status;
            }

            /// <summary> 访问数据中心中的已注册状态 </summary>
            /// <param name="_uuid"> 状态的唯一识别码 </param>
            public Status? Access(string _uuid)
            {
                if (string.IsNullOrEmpty(_uuid))
                    return null;
                return board_!.getModelCenter().FindStatus(_uuid);
            }

            protected int code_ = 0;
            protected string message_ = "";
            private string uuid_ = "";
            private Board? board_;
        }
        #endregion


        public Model(string _uid)
        {
            uid_ = _uid;
        }

        public string getUID()
        {
            return uid_;
        }

        /// <summary>
        /// 设置用户数据
        /// </summary>
        /// <param name="_key">键</param>
        /// <param name="_value">值，为空时删除对应的键</param>
        public void setUserData(string _key, UserData? _value)
        {
            if (null == _value)
            {
                if (userDataS_.ContainsKey(_key))
                    userDataS_.Remove(_key);
                return;
            }
            userDataS_[_key] = _value;
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="_key">键</param>
        /// <returns>不存在时返回空</returns>
        public UserData? getUserData(string _key)
        {
            if (!userDataS_.ContainsKey(_key))
                return null;
            return userDataS_[_key];
        }

        /// <summary>向视图层发布消息</summary>
        /// 适用于状态发生变化后，通知外部
        /// 在视图层使用addSubscriber和removeSubscriber
        /// <param name="_message">消息</param>
        /// <param name="_data">数据</param>
        public void Publish(string _message, object _data)
        {
            board_!.getModelCenter()!.Publish(_message, status_, _data);
        }

        /// <summary>
        /// 查找一个数据层
        /// </summary>
        /// <param name="_uuid"> 数据层唯一识别码</param>
        /// <returns>找到的数据层</returns>
        protected Model? findModel(string _uuid)
        {
            return board_!.getModelCenter().FindUnit(_uuid)?.getUnit();
        }

        /// <summary>
        /// 查找一个控制层
        /// </summary>
        /// <param name="_uuid"> 控制层唯一识别码</param>
        /// <returns>找到的控制层</returns>
        protected Controller? findController(string _uuid)
        {
            return board_!.getControllerCenter().FindUnit(_uuid)?.getUnit();

        }

        /// <summary>
        /// 从数据层中创建一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        /// <returns>状态<returns>
        protected Model.Status? spawnStatus<T>(string _uuid, out Error _err) where T : Model.Status, new()
        {
            Model.Status.Factory factory = new Model.Status.Factory();
            Status status = factory.New<T>(board_!, _uuid);
            _err = board_!.getModelCenter().PushStatus(_uuid, status);
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
            _err = board_!.getModelCenter().PopStatus(_uuid);
        }

        protected Status? status_;

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns>
        /// 日志实列
        /// </returns>
        protected Logger? getLogger()
        {
            return board_!.getLogger();
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>
        /// 配置实列
        /// </returns>
        protected Config? getConfig()
        {
            return board_!.getConfig();
        }

        /// <summary>
        /// 单元的安装前处理
        /// </summary>
        protected virtual void preSetup()
        {

        }

        /// <summary>
        /// 单元的安装
        /// </summary>
        protected virtual void setup()
        {

        }

        /// <summary>
        /// 单元的安装后处理
        /// </summary>
        protected virtual void postSetup()
        {

        }

        /// <summary>
        /// 单元的拆卸前处理
        /// </summary>
        protected virtual void preDismantle()
        {

        }

        /// <summary>
        /// 单元的拆卸
        /// </summary>
        protected virtual void dismantle()
        {

        }

        /// <summary>
        /// 单元的拆卸后处理
        /// </summary>
        protected virtual void postDismantle()
        {

        }

        internal void emit(System.Action<Status?, object> _slot, object _data)
        {
            _slot(status_, _data);
        }

        private Board? board_;
        private string uid_;
        private Dictionary<string, UserData> userDataS_ = new Dictionary<string, UserData>();
    }
}//namespace
