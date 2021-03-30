# -*- coding: utf-8 -*-

class Model:
    """数据层
    """

    class Inner:
        """内部类
        用于接口隔离,隐藏Controller无需暴露给外部的公有方法
        """

        def __init__(self, _model:Model):
            self.model = _model;

        def Setup(_board:Board):
            self.model.__board = _board;
            self.model.__board.getLogger().Trace("setup model")
            self.model.setup()

        def Dismantle():
            self.model.__board.getLogger().Trace("dismantle model")
            self.model.dismantle()

    class Status:
        """状态
        """

        class Factory:
            """内部工厂
            用于赋值私有成员
            """

            def New(_class, _board:Board, _uuid:str) -> Status:
                status = _class()
            status.__board_ = _board
            status.__uuid = _uuid
            return status

        def __init__(self):
            self.code :int = 0
            self.message :str = ""
            self.__board :Board = None
            self.__uuid = ""


        def getUuid() -> str:
            return self.__uuid

        def Access(string _uuid) -> Status:
            """访问数据中心中的已注册状态
            Args:
                _uuid: 状态的唯一识别码
            Returns:
                状态
            """
            if (None == self.__uuid):
                return None
            return self.__board.getModelCenter().FindStatus(_uuid)


    def __init__(self):
        self._logger :Logger = None
        self._config :Config = None
        self._status :Status = None
        self.__board :Board = None


    def Broadcast(self, _action:str, _data):
        """向视图层广播消息
        Args:
            _action: 行为
            _data: 数据
        """
        self.__board.getModelCenter().Broadcast(_action, status_, _data)

    def _findModel(_uuid:str) -> Model:
        """ 查找一个数据层
        Args:
            _uuid: 数据层唯一识别码
        Returns:
            找到的数据层
        """
        inner = self.__board.getModelCenter().FindModel(_uuid);
        if (None == inner):
            return None;
        return inner.model;

    def _findController(_uuid:str) -> View:
        """ 查找一个控制层
        Args:
            _uuid: 控制层唯一识别码
        Returns:
            找到的控制层
        """
        inner = self.__board.getControllerCenter().FindController(_uuid);
        if (None == inner):
            return None;
        return inner.controller;

    def _spawnStatus(_class， _uuid:str, _err:Error) -> Model.Status:
        """从数据层中创建一个已经注册的状态
        Args:
           _uuid: 状态唯一识别码
            _err: 错误
        Returns:
            状态
        """
        factory = Model.Status.Factory()
        status = factory.New(_class, board_, _uuid)
        _err = board_.getModelCenter().PushStatus(_uuid, status)
        if (Error.IsOK(_err)):
            return status
        return None

    def _killStatus(_uuid:str, _err:Error):
        """从数据中心中销毁一个已经注册的状态
        Args:
            _uuid: 状态唯一识别码
            _err: 错误
        """
        _err = board_.getModelCenter().PopStatus(_uuid)

    def setup():
        """
        控制层的安装
        """
        pass

    def dismantle():
        """
        控制层的拆卸
        """
        pass
