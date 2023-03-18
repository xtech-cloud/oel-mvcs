﻿/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.oelMVCS
{
    /// <summary>
    /// 服务层
    /// </summary>
    public class Service
    {
        #region
        // 内部类，用于接口隔离,隐藏Service无需暴露给外部的公有方法
        internal class Inner
        {
            public Inner(Service _unit, Board _board)
            {
                unit_ = _unit;
                unit_.board_ = _board;
            }

            public Service getUnit()
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

            private Service unit_;
        }
        #endregion

        /// <summary>
        /// 调用成功的委托回调
        /// </summary>
        /// <param name="_data">回复数据</param>
        public delegate void OnReplyDelegate(object _data);

        /// <summary>
        /// 调用错误的委托回调
        /// </summary>
        /// <param name="_error">错误</param>
        public delegate void OnErrorDelegate(Error _error);

        /// <summary> 选项 </summary>
        public class Options
        {
            // 是否使用mock
            public bool useMock = false;
        }

        public Service(string _uid)
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
        /// 查找一个服务层
        /// </summary>
        /// <param name="_uuid"> 服务层唯一识别码</param>
        /// <returns>找到的服务层</returns>
        protected Service? findService(string _uuid)
        {
            return board_!.getServiceCenter().FindUnit(_uuid)?.getUnit();
        }

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

        /// 别名列表
        private Board? board_;
        private string uid_;
        private Dictionary<string, UserData> userDataS_ = new Dictionary<string, UserData>();
    }
}//namespace
