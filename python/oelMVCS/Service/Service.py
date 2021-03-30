# -*- coding: utf-8 -*-

from typing import Callable, Dict, Any

JsonPackDelegate = Callable[[Dict[str,Any]], str]
"""
/// <summary>json序列化委托定义</summary>
/// <param name="_params">参数字典</param>
"""

OnReplyCallback = Callable[[str],None]
OnErrorCallback = Callable[[Error],None]
MockProcessorDelegate = Callable[[sre, str, Dict[str,Any], OnReplyCallback, OnErrorCallback, Options], None]

class Service:
    """服务层
    """

    class Inner:
        """内部类
        用于接口隔离,隐藏Service无需暴露给外部的公有方法
        """

        def __init__(self, _service:Service):
            self.service:Service = _service

        def Setup(self, _board:Board):
            self.service.__board = _board
            self.service.__board_.getLogger().Trace("setup service")
            service.setup();

        def Dismantle(self):
            self.service.__board_.getLogger().Trace("dismantle service")
            self.service.dismantle();

    class Options:
        """选项
        """
        def __init__(self):
            self.header:dict<str,str> = {}

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

        public delegate string JsonPackDelegate(Dictionary<string, Any> _params);


    def __init__(self):
        self.useMock :bool = false
        self.jsonPack :JsonPackDelegate = None
        self._logger :Logger = None
        self._config:Config = None
        self.__board:Board = None

    def _findModel(_uuid: str) -> Model:
        """查找一个数据层
        Args:
            _uuid: 数据层唯一识别码
        Returns:
            找到的数据层
        """
        inner = self.__board.getModelCenter().FindModel(_uuid)
        if None == inner:
            return None
        return inner.model


    def _findService(_uuid: str) -> View:
        """查找一个服务层
        Args:
            _uuid: 服务层唯一识别码
        Returns:
            找到的服务层
        """
        inner = self.__board.getServiceCenter().FindService(_uuid)
        if None == inner:
            return None
        return inner.service

    def setup():
        """控制层的安装"""
        pass

    def dismantle():
        """控制层的安装"""
        pass

    def _get(_url:str, _params:dict<str,Any>, _onReply, _onError, _options:Options) ->Error:
        """RESTful的GET形式调用
        Args:
            _url: 访问地址
            _params: 参数字典
            _onReply: 回复的回调方法
            _onError: 错误的回调方法
            _options: 选项字典
        Returns:
            错误
        """
        try:
            if (self.useMock and None != self.MockProcessor):
                self.MockProcessor(_url, "GET", _params, _onReply, _onError, _options)
            else:
                self._asyncRequest(_url, "GET", _params, _onReply, _onError, _options)
        except Exception as ex:
            self._logger.Exception(ex)
            return Error.NewException(ex)
        return Error.OK

    def _post(_url:str, _params:dict<str,Any>, _onReply, _onError, _options:Options) ->Error:
        """RESTful的POST形式调用
        Args:
            _url: 访问地址
            _params: 参数字典
            _onReply: 回复的回调方法
            _onError: 错误的回调方法
            _options: 选项字典
        Returns:
            错误
        """
        try:
            if (self.useMock and None != self.MockProcessor):
                self.MockProcessor(_url, "POST", _params, _onReply, _onError, _options)
            else:
                self._asyncRequest(_url, "POST", _params, _onReply, _onError, _options)
        except Exception as ex:
            self._logger.Exception(ex)
            return Error.NewException(ex)
        return Error.OK

    def _put(_url:str, _params:dict<str,Any>, _onReply, _onError, _options:Options) ->Error:
        """RESTful的PUT形式调用
        Args:
            _url: 访问地址
            _params: 参数字典
            _onReply: 回复的回调方法
            _onError: 错误的回调方法
            _options: 选项字典
        Returns:
            错误
        """
        try:
            if (self.useMock and None != self.MockProcessor):
                self.MockProcessor(_url, "PUT", _params, _onReply, _onError, _options)
            else:
                self._asyncRequest(_url, "PUT", _params, _onReply, _onError, _options)
        except Exception as ex:
            self._logger.Exception(ex)
            return Error.NewException(ex)
        return Error.OK

    def _delete(_url:str, _params:dict<str,Any>, _onReply, _onError, _options:Options) ->Error:
        """RESTful的DELETE形式调用
        Args:
            _url: 访问地址
            _params: 参数字典
            _onReply: 回复的回调方法
            _onError: 错误的回调方法
            _options: 选项字典
        Returns:
            错误
        """
        try:
            if (self.useMock and None != self.MockProcessor):
                self.MockProcessor(_url, "DELETE", _params, _onReply, _onError, _options)
            else:
                self._asyncRequest(_url, "DELETE", _params, _onReply, _onError, _options)
        except Exception as ex:
            self._logger.Exception(ex)
            return Error.NewException(ex)
        return Error.OK

    def _asyncRequest(_url:str, _method:str, _params:dict<str, Any>, _onReply, _onError, _options:Options) ->Error:
        """异步请求的虚函数
        Args:
            _url: 访问地址
            _params: 参数字典
            _onReply: 回复的回调方法
            _onError: 错误的回调方法
            _options: 选项字典
        Returns:
            错误
        """
        _onError(Error.NewException(new System.NotImplementedException()))
