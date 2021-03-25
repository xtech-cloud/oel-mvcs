# -*- coding: utf-8 -*-

class StatusCenter:
    def __init__(self, _framework):
        self.__framework = _framework
        self.status = {}

    def Register(self, _uuid, _status):
        self.__framework.logger.Info('register status %s' % (_uuid))
        if _uuid in self.status:
            raise KeyError('status is exists')
        self.status[_uuid] = _status

    def Cancel(self, _uuid):
        self.__framework.logger.Info('cancel status %s' % (_uuid))
        if not _uuid in self.status:
            raise KeyError('status not found')
        del self.status[_uuid]

    def Find(self, _uuid):
        return self.status[_uuid]

class ModelCenter:

    def __init__(self, _framework):
        self.__framework = _framework
        self.models = {}

    def Register(self, _uuid, _model):
        self.__framework.logger.Info('register model %s' % (_uuid))
        if _uuid in self.models:
            raise KeyError('model is exists')
        self.models[_uuid] = _model

    def Cancel(self, _uuid):
        self.__framework.logger.Info('cancel model %s' % (_uuid))
        if not _uuid in self.models:
            raise KeyError('model not found')
        del self.models[_uuid]

    def Find(self, _uuid):
        return self.models[_uuid]

    def Setup(self):
        for v in self.models.values():
            v.Setup(self.__framework)

    def Dismantle(self):
        for v in self.models.values():
            v.Dismantle()

    def broadcast(self, _action, _status, _data):
        self.__framework.viewCenter.handleAction(_action, _status, _data)
