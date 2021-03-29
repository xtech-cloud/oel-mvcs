# -*- coding: utf-8 -*-

from enum import Enum

class LogLevel(Enum):
    NONE = 0
    EXCEPTION = 1
    ERROR = 2
    WARNING = 3
    INFO = 4
    DEBUG = 5
    TRACE = 6
    ALL = 7

class Logger:
    def __init__(self):
        self.__level = LogLevel.ALL

    def Trace(self, _message:str, _args):
        self.__log(LogLevel.TRACE, self._trace, _message, _args)

    def Debug(self, _message:str, _args):
        self.__log(LogLevel.DEBUG, self._debug, _message, _args)

    def Info(self, _message:str, _args):
        self.__log(LogLevel.INFO, self._info, _message, _args)

    def Warning(self, _message:str, _args):
        self.__log(LogLevel.WARNING, self._warning, _message, _args)

    def Error(self, _message:str, _args):
        self.__log(LogLevel.ERROR, self._error, _message, _args)

    def Exception(self, _message:str, _args):
        if (self.__level < LogLevel.EXCEPTION):
            return
        self._exception(_ex);

    def _trace(self, _categoray:str, _message:str):
        pass

    def _debug(self, _categoray:str, _message:str):
        pass

    def _info(self, _categoray:str, _message:str):
        pass

    def _warning(self, _categoray:str, _message:str):
        pass

    def _error(self, _categoray:str, _message:str):
        pass

    def _exception(self, _categoray:str, _message:str):
        pass

    def __log(self, _level:LogLevel, _log, _message:str, _args):
        if (self.__level < _level):
            return

        message = str.format(_message, _args)
        categoray = str.format("oelMVCS::{}", getMethod())
        self._log(categoray, message)

    def getMethod(self) -> str:
        return ""
