using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace XTC.oelMVCS
{

    public class Service
    {
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
            get;
            set;
        }
        protected Config config_
        {
            get;
            set;
        }

        private ServiceCenter serviceCenter_
        {
            get;
            set;
        }

        private ModelCenter modelCenter_
        {
            get;
            set;
        }


        public void Setup(Board _board)
        {
            logger_ = _board.logger;
            config_ = _board.config;
            serviceCenter_ = _board.serviceCenter;
            modelCenter_ = _board.modelCenter;
            logger_.Trace("setup service");
            setup();
        }

        public void Dismantle()
        {
            logger_.Trace("dismantle service");
            dismantle();
        }

        protected virtual void dismantle()
        {

        }

        protected virtual void setup()
        {

        }

        protected Model findModel(string _uuid)
        {
            return modelCenter_.FindModel(_uuid);
        }

        protected Service findService(string _uuid)
        {
            return serviceCenter_.FindService(_uuid);
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
