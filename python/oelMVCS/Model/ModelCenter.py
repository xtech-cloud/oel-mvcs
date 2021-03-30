# -*- coding: utf-8 -*-

class ModelCenter:
    """数据层中心
    """

    def __init__(self, _board:Board):
        self.__board:Board = _board
        self.__models:dict<str, Model.Inner> = {}
        self.__status:dict<str, Model.Inner> = {}

    def Register(_uuid:str, _inner:Model.Inner) -> Error:
        self.__board.getLogger().Info("register model {}", _uuid)
        if (_uuid in self.__models.keys()):
            return Error.NewAccessErr("model {} exists", _uuid)
        self.__models[_uuid] = _inner
        return Error.OK

    def Cancel(_uuid:str)->Error:
        self.__board_.getLogger().Info("cancel model {}", _uuid)

        if (not _uuid in self.__models.keys()):
            return Error.NewAccessErr("model {} not found", _uuid)
        self.__models.Remove(_uuid)
        return Error.OK;

    def FindModel(_uuid:str)->Model.Inner:
        return self.__models[_uuid]

    def Setup(self):
        for inner in self.__models.values():
            inner.Setup(self.__board)

    def Dismantle(self):
        for inner in self.__models.values():
            inner.Dismantle();

    def PushStatus(_uuid:str, _status:Model.Status) -> Error:
        self.__board.getLogger().Info("push status {}", _uuid)

        if (_uuid in self.__status.keys()):
            return Error.NewAccessErr("status {} exists", _uuid)
        self.__status[_uuid] = _status
        return Error.OK

    def PopStatus(_uuid:str) -> Error:
        self.__board.getLogger().Info("pop status {}", _uuid);

        if (not _uuid in status.keys()):
            return Error.NewAccessErr("status {} not found", _uuid)
        self.__status.Remove(_uuid)
        return Error.OK

    def FindStatus(_uuid:str) -> Error:
        return self.__status[_uuid]

    def Broadcast(_action:str, _status:Model.Status, _data):
        self.__board.getViewCenter().HandleAction(_action, _status, _data)
