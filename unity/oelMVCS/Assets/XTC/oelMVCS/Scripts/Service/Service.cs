using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace XTC.oelMVCS
{

    public class Service
    {
        #region
        // 内部类，用于接口隔离,隐藏Service无需暴露给外部的公有方法
        public class Inner
        {
            public Service service
            {
                get;
                private set;
            }

            public Inner(Service _service)
            {
                service = _service;
            }

            internal void Setup(Board _board)
            {
                service.board_ = _board;
                service.board_.logger.Trace("setup service");
                service.setup();
            }

            internal void Dismantle()
            {
                service.board_.logger.Trace("dismantle service");
                service.dismantle();
            }
        }
        #endregion

        public class Options
        {
            public Dictionary<string, string> header = new Dictionary<string, string>();
        }

        public delegate void MockProcessorDelegate(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options);

        public delegate void OnReplyCallback(string _reply);
        public delegate void OnErrorCallback(string _error);
        public delegate string JsonPackDelegate(Dictionary<string, Any> _params);

        public static JsonPackDelegate jsonPack;


        public string domain = "";

        public MockProcessorDelegate MockProcessor;

        public bool useMock = false;

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

        protected Error post(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            try
            {
                if (useMock && null != MockProcessor)
                    MockProcessor(_url, "POST", _params, _onReply, _onError, _options);
                else
                    asyncRequest(_url, "POST", _params, _onReply, _onError, _options);
            }
            catch (System.Exception ex)
            {
                logger_.Exception(ex);
                return Error.NewException(ex);
            }
            return Error.OK;
        }

        protected Error get(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            try
            {
                if (useMock && null != MockProcessor)
                    MockProcessor(_url, "GET", _params, _onReply, _onError, _options);
                else
                    asyncRequest(_url, "GET", _params, _onReply, _onError, _options);
            }
            catch (System.Exception ex)
            {
                logger_.Exception(ex);
                return Error.NewException(ex);
            }
            return Error.OK;
        }

        protected Error delete (string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            try
            {
                if (useMock && null != MockProcessor)
                    MockProcessor(_url, "DELETE", _params, _onReply, _onError, _options);
                else
                    asyncRequest(_url, "DELETE", _params, _onReply, _onError, _options);
            }
            catch (System.Exception ex)
            {
                logger_.Exception(ex);
                return Error.NewException(ex);
            }
            return Error.OK;
        }

        protected Error put(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            try
            {
                if (useMock && null != MockProcessor)
                    MockProcessor(_url, "PUT", _params, _onReply, _onError, _options);
                else
                    asyncRequest(_url, "PUT", _params, _onReply, _onError, _options);
            }
            catch (System.Exception ex)
            {
                logger_.Exception(ex);
                return Error.NewException(ex);
            }
            return Error.OK;
        }

        protected virtual void asyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
        }
    }
}//namespace
