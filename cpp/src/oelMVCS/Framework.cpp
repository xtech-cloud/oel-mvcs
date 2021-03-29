#include "oelMVCS/Core/Logger.h"
#include "oelMVCS/Core/Config.h"
#include "oelMVCS/Framework.h"

namespace XTC
{
namespace oelMVCS
{

StaticPipe* Framework::getStaticPipe()
{
    return staticPipe_;
}

DynamicPipe* Framework::getDynamicPipe()
{
    return dynamicPipe_;
}

void Framework::setConfig(const Config* _config)
{
    board_.config = _config;
}

void Framework::setLogger(const Logger* _logger)
{
    return logger_;
}

Framework::Framework()
{
    board_ = new Board();
    staticPipe_ = new StaticPipe(board_);
    dynamicPipe_ = new DynamicPipe(board_);
}

~Framework::Framework()
{
    if (0 != staticPipe_)
    {
        delete staticPipe_;
        staticPipe_ = 0;
    }
    if (0 != dynamicPipe_)
    {
        delete dynamicPipe_;
        dynamicPipe_ = 0;
    }
    if (0 != board_)
    {
        delete board_;
        board_ = 0;
    }
}

void Framework::Initialize()
{
    board_->getLogger()->Info("initialize framework");
}

void Framework::Setup()
{
    board_->getLogger()->Info("setup framework");
    board_->getViewCenter()->Setup();
    board_->getServiceCenter()->Setup();
    board_->getModelCenter()->Setup();
    board_->getControllerCenter()->Setup();
}

void Framework::Dismantle()
{
    board_->getLogger()->Info("dismantle framework");
    board_->getViewCenter()->Dismantle();
    board_->getServiceCenter()->Dismantle();
    board_->getModelCenter()->Dismantle();
    board_->getControllerCenter()->Dismantle();
}


void Framework::Release()
{
    board_->getLogger()->Info("release framework");
}

}//namespace
}//namespace
