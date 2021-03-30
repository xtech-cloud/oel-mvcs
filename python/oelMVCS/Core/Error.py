# -*- coding: utf-8 -*-

class Error:
    NULL:int = -1
    PARAM:int = -2
    ACCESS:int = -3
    EXCEPTION:int = -99
    OK = Error(0, "")

    def __init__(self):
        self.__code:int = 0
        self.__message:str = ""

    def __init__(self, _code:int, _message:str):
        self.__code:int = _code
        self.__message:str = _message

    def IsOK(_err) -> bool:
        return _err.__code == 0

    def getCode(self) -> int:
        return self.__code

    def getMessage(self) -> str:
        return self.__message

    def ToString(self):
        return "{}.{}".format(self.__code, self.__message)

    def NewNullErr(_message, _args) -> Error:
        message = str.Format(_message, _args)
        return Error(Error.NULL, message)

    def NewParamErr(_message, _args) -> Error:
        message = str.Format(_message, _args)
        return Error(Error.PARAM, message)

    def NewAccessErr(_message, _args) -> Error:
        message = str.Format(_message, _args)
        return Error(Error.ACCESS, message)

    def NewExceptionErr(_message, _args) -> Error:
        message = str.Format(_message, _args)
        return Error(Error.EXCEPTION, message)
