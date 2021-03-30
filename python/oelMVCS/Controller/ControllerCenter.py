# -*- coding: utf-8 -*-

from typing import Dict, NewType

class ControllerCenter:
    """控制层中心
    """

    def __init__(self, _board:Board):
        self.__controllers : Dict[str,Controller.Inner] = {}
        self.__board :Board = _board

    def Register(self, _uuid:str, _inner:Controller.Inner) -> Error:
        self.__board.getLogger().Info("register controller {}", _uuid)
        if (_uuid in __controllers.keys()):
            return Error.NewAccessErr("controller {} exists", _uuid)
        self.__controllers[_uuid] = _inner
        return Error.OK

    def Cancel(self, string _uuid) -> Error:
        self.__board.getLogger().Info("cancel controller {}", _uuid)
        if (not _uuid in self.__controllers.keys()):
            return Error.NewAccessErr("controller {} not found", _uuid)
        self.__controllers.remove(_uuid)
        return Error.OK

    def FindController(self, _uuid:str) -> Controller.Inner:
        return self.__controllers[_uuid]

    def Setup(self):
        for inner in self.__controllers.values():
            inner.Setup(self.__board)

    def Dismantle(self):
        for inner in self.__controllers.values():
            inner.Dismantle();
