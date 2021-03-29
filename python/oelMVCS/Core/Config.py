# -*- coding: utf-8 -*-

class Config:

    def __init__(self):
        self.__fields = {}

    def Merge(self, _content:str):
        pass

    def Has(_field:str) -> bool:
        return _field in self.__fields.keys()
