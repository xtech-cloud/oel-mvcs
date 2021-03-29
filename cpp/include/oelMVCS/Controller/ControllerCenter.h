#ifndef oel_MVCS_CONTROLLER_CENTER_H
#define oel_MVCS_CONTROLLER_CENTER_H

#include <map>
#include "oelMVCS/Core/Error.h"
#include "oelMVCS/Controller/Controller.h"

namespace XTC
{
namespace oelMVCS
{

//前置声明
class Board;

class ControllerCenter
{
public:

    ControllerCenter(const Board* _board);

    Error Register(std::string _uuid, const Controller.Inner* _inner);

    Error Cancel(std::string _uuid);

    Controller.Inner* FindController(std::string _uuid);

    void Setup();

    void Dismantle();
private:
    std::map<std::string, Controller.Inner> controllers_;
    Board* board_;
}; //class

}//namespace
}//namespace

#endif
