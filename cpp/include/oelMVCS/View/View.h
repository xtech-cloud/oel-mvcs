#ifndef oel_MVCS_VIEW_H
#define oel_MVCS_VIEW_H

#include <string>
#include <map>
#include "oelMVCS/Core/Error.h"
#include "oelMVCS/Model/Model.h"

namespace XTC
{
namespace oelMVCS
{

/// <summary>
/// 视图层
/// </summary>
class View
{
public:
    // 内部类，用于接口隔离,隐藏View无需暴露给外部的公有方法
    class Inner
    {
    public:
        Inner(const View* _view);

        void Setup(const Board* _board);

        void Dismantle();

        Error Handle(std::string _action, Model.Status* _status, object _data);
    private:
        View* view_;
    }


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
    /// 查找一个服务层
    /// </summary>
    /// <param name="_uuid"> 服务层唯一识别码</param>
    /// <returns>找到的服务层</returns>
    Service* findService(std::string _uuid);

    /// <summary>
    /// 控制层的安装
    /// </summary>
    virtual void setup();

    // 派生类需要实现的方法
    virtual void bindEvents();

    // 派生类需要实现的方法
    virtual void unbindEvents();

    /// <summary>
    /// 控制层的拆卸
    /// </summary>
    virtual void dismantle();

    /// <summary>
    /// 行为路由，使用指定函数处理指定行为
    /// 设置了路由的行为，在数据层进行广播时，会自动调用相应的处理函数
    /// </summary>
    /// <param name="_action">需要处理的行为</param>
    /// <param name="_action">行为对应的处理函数</param>
    void route(std::string _action, Action<Model.Status, object> _handler);

private:
    std::map<std::string, Action<Model.Status, object>> handlers_;
};//class

}//namespace
}//namespace
