#ifndef oel_MVCS_CONFIG_H
#define oel_MVCS_CONFIG_H

#include <string>
#include <map>
#include "oelMVCS/Core/Any.hpp"

namespace XTC
{
namespace oelMVCS
{

class Config
{
public:
    void Merge(std::string _content);

    bool Has(std::string _field);

    Any this [std::string _aKey];
private:
    std::map<std::string, Any>* fields;
};//class

}//namespace
}//namespace

#endif
