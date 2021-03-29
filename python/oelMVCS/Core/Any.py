# -*- coding: utf-8 -*-

from enum import Enum

class Any:
    """
    基础值的任意类型
    """

    class Tag(Enum):
        NULL = 0
        StringValue = 1
        IntValue = 2
        LongValue = 3
        FloatValue = 4
        DoubleValue = 5
        BoolValue = 6

    def __init__(self):
        self.__value = None
        self.__tag:Tag = Tag.Null

    def FromString(_value:str)
        newAny = Any()
        newAny.__value = _value
        newAny.__Tag = Tag.StringValue
        return newAny

    def FromFloat(_value:float)
        newAny = Any()
        newAny.__value = _value
        newAny.__Tag = Tag.FloatValue
        return newAny

    def FromDouble(_value:float)
        newAny = Any()
        newAny.__value = _value
        newAny.__Tag = Tag.DoubleValue
        return newAny

    def FromBool(_value:bool)
        newAny = Any()
        newAny.__value = _value
        newAny.__Tag = Tag.BoolValue
        return newAny

    def FromInt(_value:int)
        newAny = Any()
        newAny.__value = _value
        newAny.__Tag = Tag.IntValue
        return newAny

    def FromLong(_value:long)
        newAny = Any()
        newAny.__value = _value
        newAny.__Tag = Tag.LongValue
        return newAny

    def IsNull(self) -> bool:
        return self.__tag == Tag.NULL

    def IsString(self) -> bool:
        return self.__tag == Tag.StringValue

    def IsInt(self) -> bool:
        return self.__tag == Tag.IntValue

    def IsLong(self) -> bool:
        return self.__tag == Tag.LongValue

    def IsFloat(self) -> bool:
        return self.__tag == Tag.FloatValue

    def IsDouble(self) -> bool:
        return self.__tag == Tag.DoubleValue

    def IsBool(self) -> bool:
        return self.__tag == Tag.BoolValue

    def AsString(self) -> str:
        return self.__value

    def AsInt(self) -> int:
        return self.__value

    def AsLong(self) -> long:
        return self.__value

    def AsFloat(self) -> float:
        return self.__value

    def AsDouble(self) -> double:
        return self.__value

    def AsBool(self) -> bool:
        return self.__value
