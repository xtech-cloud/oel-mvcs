#ifndef oel_MVCS_ERROR_H
#define oel_MVCS_ERROR_H

#include <string>

namespace XTC
{
namespace oelMVCS
{

class Error
{

public:
    const int NULL = -1;
    const int PARAM = -2;
    const int ACCESS = -3;
    const int EXCEPTION = -99;

    static Error OK();
    Error(int _code, std::string _message);
    bool IsOK();
    std::string ToString();

    int getCode();
    std::string getMessage();

    static Error NewNullErr(std::string _message, params object[] _args);
    static Error NewParamErr(std::string _message, params object[] _args);
    static Error NewAccessErr(std::string _message, params object[] _args);
    static Error NewException(System.Exception _ex);

private:
    int code_;
    std::string message_;

};//class

}//namespace
}//namespace

#endif
