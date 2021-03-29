#ifndef oel_MVCS_MODEL_CENTER_H
#define oel_MVCS_MODEL_CENTER_H

#include <string>
#include <map>

#include "oelMVCS/Core/Error.h"
#include "oelMVCS/Model/Model.h"

namespace XTC
{
namespace oelMVCS
{

class ModelCenter
{
    public:
    ModelCenter(const Board* _board);

    Error Register(std::string _uuid, const Model.Inner* _inner);

    Error Cancel(std::string _uuid)

    Model.Inner* FindModel(std::string _uuid);

    void Setup();

    void Dismantle();

    Error PushStatus(string _uuid, Model.Status _status);

    Error PopStatus(string _uuid);

    Model.Status FindStatus(string _uuid);

    void Broadcast(string _action, Model.Status _status, object _data);
private:
    // 数据列表
    std::map<std::string, Model.Inner> models_;
    // 状态列表
    std::map<std::string, Model.Status> status_;

    Board* board_;
};//class
}//namespace
}//namespace
