#ifndef oel_MVCS_LOGGER_H
#define oel_MVCS_LOGGER_H

#include <string>

namespace XTC
{
namespace oelMVCS
{

enum LogLevel
{
    ALL,
    TRACE,
    DEBUG,
    INFO,
    WARNING,
    ERROR,
    EXCEPTION,
    NONE
};

class Logger
{
public:
    void setLevel(LogLevel _level);
    LogLevel getLevel();

    void Trace(std::string _message, params object[] _args);

    void Debug(std::string _message, params object[] _args);

    void Info(std::string _message, params object[] _args);

    void Warning(std::string _message, params object[] _args);

    void Error(std::string _message, params object[] _args);

    void Exception(System.Exception _ex);

protected:
    virtual void trace (std::string _categoray, string _message);

    virtual void debug (std::string _category, string _message);

    virtual void info (std::string _category, string _message);

    virtual void warning (std::string _category, string _message);

    virtual void error (std::string _category, string _message);

    virtual void exception (System.Exception _exception);

    void log(LogLevel _level,  System.Action<string, string> _log, string _message, params object[] _args);
private:
    std::string getMethod();
    LogLevel level_;
};//class

}//namespace
}//namespace

#endif
