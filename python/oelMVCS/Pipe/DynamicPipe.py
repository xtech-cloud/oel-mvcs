# -*- coding: utf-8 -*-


class DynamicPipe:
    """ 动态管线 """

    def __init__(self, _board:Board):
        self.__board = _board

    def PushModel(self, _uuid :str, _model:Model) -> Error:
        """添加数据层
        Args:
            _uuid: 数据层唯一识别码
            _model: 数据层实列
        Returns:
            错误
        """
        inner = Model.Inner(_model);
        err = self.__board.getModelCenter().Register(_uuid, inner)
        if (!err.IsOK()):
            return err
        inner.Setup(self.__board)
        return Error.OK

    def PopModel(self, _uuid:str) -> Error:
        """删除数据层
        Args:
            _uuid: 数据层唯一识别码
        Returns:
            错误
        """
        inner = self.__board.getModelCenter().FindModel(_uuid)
        if (None == inner):
            return Error.NewAccessErr("model {} not found", _uuid)
        inner.Dismantle()
        return self.__board.getModelCenter().Cancel(_uuid)

    def PushView(self, _uuid:str, _view:View) -> Error:
        """ 添加视图层
        Args:
            _uuid: 视图层唯一识别码
            _view:  视图层实列
        Returns:
            错误
        """
        inner = View.Inner(_view);
        err = self.__board.getViewCenter().Register(_uuid, inner)
        if (!err.IsOK()):
            return err
        inner.Setup(self.__board)
        return Error.OK

    def PopView(self, _uuid:str) -> Error:
        """删除视图层
        Args:
            _uuid: 视图层唯一识别码
        Returns:
            错误
        """
        inner = self.__board.getViewCenter().FindView(_uuid)
        if (None == inner):
            return Error.NewAccessErr("view {} not found", _uuid)
        inner.Dismantle()
        return self.__board.getViewCenter().Cancel(_uuid)

    def PushController(self, _uuid:str, _controller:Controller) -> Error:
        """ 添加控制层
        Args:
            _uuid: 控制层唯一识别码
            _controller: 控制层实列
        Returns:
            错误
        """
        inner = Controller.Inner(_controller);
        err = self.__board.getControllerCenter().Register(_uuid, inner)
        if (!err.IsOK()):
            return err
        inner.Setup(self.__board)
        return Error.OK

    def PopController(self, _uuid:str) -> Error:
        """删除控制层
        Args:
            _uuid: 控制层唯一识别码
        Returns:
            错误
        """
        inner = self.__board.getControllerCenter().FindController(_uuid)
        if (None == inner):
            return Error.NewAccessErr("controller {} not found", _uuid)
        inner.Dismantle()
        return self.__board.getControllerCenter().Cancel(_uuid)

    def PushService(self, _uuid:str, _service:Service) -> Error:
        """ 添加服务层
        Args:
            _uuid: 服务层唯一识别码
            _service: 服务层实列
        Returns:
            错误
        """
        inner = Service.Inner(_service);
        err = self.__board.getServiceCenter().Register(_uuid, inner)
        if (!err.IsOK()):
            return err
        inner.Setup(self.__board)
        return Error.OK

    def PopService(self, _uuid:str) -> Error:
        """删除服务层
        Args:
            _uuid: 服务层唯一识别码
        Returns:
            错误
        """
        inner = self.__board.getServiceCenter().FindService(_uuid)
        if (None == inner):
            return Error.NewAccessErr("service {} not found", _uuid)
        inner.Dismantle()
        return self.__board.getServiceCenter().Cancel(_uuid)
