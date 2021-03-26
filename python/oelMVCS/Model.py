class Status:
    def __init__(self):
        self.uuid:str = ''
        self.code = 0
        self.message = ''
        self.center = None

    def Register(self):
        if '' == self.uuid:
            raise Exception('uuid is empty')
        if None == self.center:
            raise Exception("center is none")
        self.center.Register(self.uuid, self)

    def Cancel(self):
        if '' == self.uuid:
            raise Exception('uuid is empty')
        if None == self.center:
            raise Exception("center is none")
        self.center.Cancel(self.uuid)

    def Access(self, _name):
        if None == self.center:
            raise Exception("center is none")
        return self.center.Find(_name)

class Model:
    def __init__(self):
        self.status = None

    def Setup(self, _framework):
        self.logger = _framework.logger
        self.config = _framework.config
        self.statusCenter = _framework.statusCenter
        self.modelCenter = _framework.modelCenter
        self.controllerCenter = _framework.controllerCenter
        self._setup()

    def Dismantle(self):
        self._dismantle()

    def Broadcast(self, _action, _data):
        self.modelCenter.broadcast(_action, self.status, _data)

    def _setup(self):
        pass

    def _dismantle(self):
        pass
