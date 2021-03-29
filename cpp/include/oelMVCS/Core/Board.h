#ifndef oel_MVCS_BOARD_H
#define oel_MVCS_BOARD_H

namespace XTC
{
namespace oelMVCS
{

//前置声明
class Config;
class Logger;
class ModelCenter;
class ViewCenter;
class ControllerCenter;
class ServiceCenter;

// 内部通讯的主板
class Board
{
public:
    Config* getConfig();
    void setConfig(const Config* _config);
    Logger* getLogger();
    void setLogger(const Logger* _logger);

    ServiceCenter* getServiceCenter();
    ControllerCenter* getControllerCenter();
    ViewCenter* getViewCenter();
    ModelCenter* getModelCenter();

private:
    ModelCenter* modelCenter_;
    ViewCenter* viewCenter_;
    ControllerCenter* controllerCenter_;
    ServiceCenter* serviceCenter_;
    Config* config_;
    Logger* logger_;
};//class

}//namespace
}//namespace

#endif
