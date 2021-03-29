#ifndef oel_MVCS_DYNAMIC_PIPE_H
#define oel_MVCS_DYNAMIC_PIPE_H

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

/// <summary>动态管线</summary>
class DynamicPipe
{
public:
    DynamicPipe(const Board* _board);

    /// <summary>添加数据层</summary>
    /// <param name="_uuid">数据层唯一识别码</param>
    /// <param name="_model">数据层实列</param>
    /// <returns>错误</returns>
    Error PushModel(std::string _uuid, const Model* _model);

    /// <summary>删除数据层</summary>
    /// <param name="_uuid">数据层唯一识别码</param>
    /// <returns>错误</returns>
    Error PopModel(std::string _uuid);

    /// <summary>添加视图层</summary>
    /// <param name="_uuid">视图层唯一识别码</param>
    /// <param name="_view">视图层实列</param>
    /// <returns>错误</returns>
    Error PushView(std::string _uuid, const View* _view);

    /// <summary>删除视图层</summary>
    /// <param name="_uuid">视图层唯一识别码</param>
    /// <returns>错误</returns>
    Error PopView(std::string _uuid);

    /// <summary>添加控制层</summary>
    /// <param name="_uuid">控制层唯一识别码</param>
    /// <param name="_view">控制层实列</param>
    /// <returns>错误</returns>
    Error PushController(std::string _uuid, const Controller* _controller);

    /// <summary>删除控制层</summary>
    /// <param name="_uuid">控制层唯一识别码</param>
    /// <returns>错误</returns>
    Error PopController(std::string _uuid);

    /// <summary>添加服务层</summary>
    /// <param name="_uuid">服务层唯一识别码</param>
    /// <param name="_view">服务层实列</param>
    /// <returns>错误</returns>
    Error PushService(std::string _uuid, const Service* _service);

    /// <summary>删除服务层</summary>
    /// <param name="_uuid">服务层唯一识别码</param>
    /// <returns>错误</returns>
    Error PopService(std::string _uuid);
private:
    Board* board_;

};//class

}//namespace
}//namespace
