#ifndef oel_MVCS_STATIC_PIPE_H
#define oel_MVCS_STATIC_PIPE_H

#include <string>
#include "oelMVCS/Core/Error.h"

namespace XTC
{
namespace oelMVCS
{

//前置声明
class Board;
class Model;
class View;
class Controller;
class Service;

/// <summary>静态管线</summary>
class StaticPipe
{
public:
    StaticPipe(const Board* _board);

    /// <summary>注册数据层</summary>
    /// <param name="_uuid">数据层唯一识别码</param>
    /// <param name="_model">数据层实例</param>
    /// <returns>错误</returns>
    Error RegisterModel(std::string _uuid, const Model* _model);

    /// <summary>注销数据层</summary>
    /// <param name="_uuid">数据层唯一识别码</param>
    /// <returns>错误</returns>
    Error CancelModel(std::string _uuid);

    /// <summary>注册视图层</summary>
    /// <param name="_uuid">视图层唯一识别码</param>
    /// <param name="_model">视图层实例</param>
    /// <returns>错误</returns>
    Error RegisterView(std::string _uuid, const View* _view);

    /// <summary>注销视图层</summary>
    /// <param name="_uuid">视图层唯一识别码</param>
    /// <returns>错误</returns>
    Error CancelView(std::string _uuid);
    /// <summary>注册控制层</summary>
    /// <param name="_uuid">控制层唯一识别码</param>
    /// <param name="_model">控制层实例</param>
    /// <returns>错误</returns>
    Error RegisterController(std::string _uuid, const Controller* _controller);

    /// <summary>注销控制层</summary>
    /// <param name="_uuid">控制层唯一识别码</param>
    /// <returns>错误</returns>
    Error CancelController(std::string _uuid);

    /// <summary>注册服务层</summary>
    /// <param name="_uuid">服务层唯一识别码</param>
    /// <param name="_model">服务层实例</param>
    /// <returns>错误</returns>
    Error RegisterService(std::string _uuid, const Service* _service);

    /// <summary>注销服务层</summary>
    /// <param name="_uuid">服务层唯一识别码</param>
    /// <returns>错误</returns>
    Error CancelService(std::string _uuid);
private:
    Board* board_;
};//class

}//namespace
}//namespace
