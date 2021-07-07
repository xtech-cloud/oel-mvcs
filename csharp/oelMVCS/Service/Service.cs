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
        public class Inner
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

            private Service unit_ = null;
        }
        #endregion

        /// <summary> 选项 </summary>
        public class Options
        {
            /// 头
            public Dictionary<string, string> header = new Dictionary<string, string>();
        }

        /// <summary>Mock处理委托定义</summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_methos">访问方法，POST|GET|DELETE|PUT</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        public delegate void MockProcessorDelegate(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options);

        /// <summary>收到回复的委托定义</summary>
        /// <param name="_reply">回复内容</param>
        public delegate void OnReplyCallback(string _reply);

        /// <summary>发生错误的委托定义</summary>
        /// <param name="_error">错误</param>
        public delegate void OnErrorCallback(Error _error);

        /// <summary>修改参数的委托定义</summary>
        /// <param name="_error">错误</param>
        public delegate void ParameterHandlerDelegate(Dictionary<string, Any> _params);

        /// Mock处理委托
        public MockProcessorDelegate MockProcessor;

        /// 是否使用mock
        public bool useMock = false;

        private class Alias
        {
            public string uri { get; set; }
            public string method { get; set; }
            public bool useMock{ get; set; }
            public Dictionary<string, Any> parameter { get; set; }
        }
        /// 别名列表
        private Dictionary<string, Alias> aliasMap_ = null;

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

        /// <summary>
        /// 使用别名调用服务
        /// </summary>
        /// <param name="_alias">别名</param>
        /// <param name="_parameterHandler">参数修改委托</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        protected void callAlias(string _alias, ParameterHandlerDelegate _parameterHandler, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            Alias alias;
            if (null == aliasMap_ || !aliasMap_.TryGetValue(_alias, out alias))
            {
                _onError(Error.NewAccessErr("alias not found"));
                return;
            }
            if(null != _parameterHandler)
            {
                _parameterHandler(alias.parameter);
            }
            send(alias.uri, alias.method, alias.useMock, alias.parameter, _onReply, _onError, _options);
        }

        /// <summary>
        /// 创建一个别名
        /// </summary>
        /// <param name="_alias">别名名称</param>
        /// <param name="_url">对应的地址</param>
        /// <param name="_method">对应的方法</param>
        /// <param name="_useMock">是否使用模拟方式</param>
        protected void createAlias(string _alias, string _url, string _method, bool _useMock, Dictionary<string, Any> _parameter)
        {
            if (null == aliasMap_)
                aliasMap_ = new Dictionary<string, Alias>();
            Alias alias = new Alias();
            alias.uri = _url;
            alias.method = _method;
            alias.useMock = _useMock;
            alias.parameter = _parameter;
            aliasMap_[_alias] = new Alias();
        }

        /// <summary>
        /// 对send的RESTful的POST封装
        /// </summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        protected void post(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            send(_url, "POST", useMock, _params, _onReply, _onError, _options);
        }

        /// <summary>
        /// 对send的RESTful的GET封装
        /// </summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        protected void get(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            send(_url, "GET", useMock, _params, _onReply, _onError, _options);
        }

        /// <summary>
        /// 对send的RESTful的DELETE封装
        /// </summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        protected void delete(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            send(_url, "DELETE", useMock, _params, _onReply, _onError, _options);
        }

        /// <summary>
        /// 对send的RESTful的PUT封装
        /// </summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        protected void put(string _url, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            send(_url, "PUT", useMock, _params, _onReply, _onError, _options);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        protected void send(string _url, string _method, bool _useMock, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            if (_useMock)
            {
                if (null == MockProcessor)
                {
                    _onError(Error.NewNullErr("MockProcessor is null"));
                    return;
                }

                MockProcessor(_url, _method, _params, _onReply, _onError, _options);
                return;
            }
            asyncRequest(_url, _method, _params, _onReply, _onError, _options);
        }



        /// <summary>
        /// 异步请求的虚函数
        /// </summary>
        /// <param name="_url">访问地址</param>
        /// <param name="_params">参数字典</param>
        /// <param name="_onReply">回复的回调方法</param>
        /// <param name="_onError">错误的回调方法</param>
        /// <param name="_options">选项字典</param>
        /// <returns>错误</returns>
        protected virtual void asyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            _onError(Error.NewException(new System.NotImplementedException()));
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

        private Board board_ = null;
    }
}//namespace
