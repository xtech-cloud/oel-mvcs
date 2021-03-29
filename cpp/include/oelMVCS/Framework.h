#ifndef oel_MVCS_VIEW_H
#define oel_MVCS_VIEW_H

namespace XTC
{
namespace oelMVCS
{

//前置声明
class Logger;
class Config;
class StaticPipe;
class DynamicPipe;
class Board;

class Framework
{
public:
    Framework();

    ~Framework();

    void setConfig(const Config* _config);

    void setLogger(const Logger* _logger);

    StaticPipe* getStaticPipe();

    DynamicPipe* getDynamicPipe();


    /// <summary>
    /// 框架初始化，完成各层中心的实例化
    /// </summary>
    void Initialize();

    /// <summary>
    /// 框架安装，完成各层中心已注册组件的安装
    /// 此过程将调用各派生组件的setup方法
    /// </summary>
    void Setup();

    /// <summary>
    /// 框架拆卸，完成各层中心已注册组件的拆卸
    /// 此过程将调用各派生组件的dismantle方法
    /// </summary>
    void Dismantle();

    /// <summary>
    /// 框架销毁，完成各层中心的释放
    /// </summary>
    void Release();

private:
    StaticPipe* staticPipe_;
    DynamicPipe* dynamicPipe_;
    Board* board_;
};//class

}//namespace
}//namespace

#endif
