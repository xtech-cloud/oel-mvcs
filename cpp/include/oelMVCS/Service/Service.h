#ifndef oel_MVCS_SERVICE_H
#define oel_MVCS_SERVICE_H

#include <string>
#include <map>
#include "oelMVCS/Core/Error.h"

namespace XTC
{
namespace oelMVCS
{

//前置声明
class Board;

/// <summary>
/// 服务层
/// </summary>
class Service
{
public:
    // 内部类，用于接口隔离,隐藏Service无需暴露给外部的公有方法
    class Inner
    {
    public:
        Inner(const Service* _service);

        void Setup(const Board* _board);

        void Dismantle();
    private:
        Service* service_;
    };//class

    /// <summary> 选项 </summary>
    class Options
    {
    public:
        std::map<std::string, std::string>& getHeader();
    private:
        /// 头
        std::map<std::string, std::string> header_;
    };//class

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

    /// <summary>json序列化委托定义</summary>
    /// <param name="_params">参数字典</param>
    public delegate string JsonPackDelegate(Dictionary<string, Any> _params);

    /// json序列化委托
    public static JsonPackDelegate jsonPack;

    /// Mock处理委托
    public MockProcessorDelegate MockProcessor;

    /// 是否使用mock
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
    /// 服务层的安装
    /// </summary>
    protected virtual void setup()
    {

    }

    /// <summary>
    /// 服务层的拆卸
    /// </summary>
    protected virtual void dismantle()
    {

    }

    /// <summary>
    /// RESTful的POST形式调用
    /// </summary>
    /// <param name="_url">访问地址</param>
    /// <param name="_params">参数字典</param>
    /// <param name="_onReply">回复的回调方法</param>
    /// <param name="_onError">错误的回调方法</param>
    /// <param name="_options">选项字典</param>
    /// <returns>错误</returns>
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

    /// <summary>
    /// RESTful的GET形式调用
    /// </summary>
    /// <param name="_url">访问地址</param>
    /// <param name="_params">参数字典</param>
    /// <param name="_onReply">回复的回调方法</param>
    /// <param name="_onError">错误的回调方法</param>
    /// <param name="_options">选项字典</param>
    /// <returns>错误</returns>
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

    /// <summary>
    /// RESTful的DELETE形式调用
    /// </summary>
    /// <param name="_url">访问地址</param>
    /// <param name="_params">参数字典</param>
    /// <param name="_onReply">回复的回调方法</param>
    /// <param name="_onError">错误的回调方法</param>
    /// <param name="_options">选项字典</param>
    /// <returns>错误</returns>
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

    /// <summary>
    /// RESTful的PUT形式调用
    /// </summary>
    /// <param name="_url">访问地址</param>
    /// <param name="_params">参数字典</param>
    /// <param name="_onReply">回复的回调方法</param>
    /// <param name="_onError">错误的回调方法</param>
    /// <param name="_options">选项字典</param>
    /// <returns>错误</returns>
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
};//class

}//namespace
}//namespace
