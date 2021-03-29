#ifndef oel_MVCS_CONTROLLER_H
#define oel_MVCS_CONTROLLER_H

#include <string>

namespace XTC
{
namespace oelMVCS
{

// 前置声明
class Board;
class Logger;
class Config;
class Model;
class View;
class Controller;
class Service;

/// <summary>
/// 控制层
/// </summary>
class Controller
{
public:
    // 内部类，用于接口隔离,隐藏Controller无需暴露给外部的公有方法
    class Inner
    {
    public:
        Inner(Controller* _controller);

        Controller* setController();

        void Setup(const Board* _board);

        void Dismantle();
    private:
        Controller* controller_;
    };//class

protected:

    Logger* getLogger();

    Config* getConfig();

    /// <summary>
    /// 查找一个数据层
    /// </summary>
    /// <param name="_uuid"> 数据层唯一识别码</param>
    /// <returns>找到的数据层</returns>
    Model* findModel(std::string _uuid);

    /// <summary>
    /// 查找一个视图层
    /// </summary>
    /// <param name="_uuid"> 视图层唯一识别码</param>
    /// <returns>找到的视图层</returns>
    View* findView(std::string _uuid);

    /// <summary>
    /// 查找一个控制层
    /// </summary>
    /// <param name="_uuid"> 控制层唯一识别码</param>
    /// <returns>找到的控制层</returns>
    Controller* findController(std::string _uuid);

    /// <summary>
    /// 查找一个服务层
    /// </summary>
    /// <param name="_uuid"> 服务层唯一识别码</param>
    /// <returns>找到的服务层</returns>
    Service* findService(std::string _uuid);

    /// <summary>
    /// 控制层的安装
    /// </summary>
    virtual void setup();

    /// <summary>
    /// 控制层的拆卸
    /// </summary>
    virtual void dismantle();
private:
    Board* board_;
};//class

}//namespace MVCS
}//namespace MVCS

#endif
