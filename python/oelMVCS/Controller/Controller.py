# -*- coding: utf-8 -*-


class Controller:
    """控制层"""

    class Inner:
        """内部类
        用于接口隔离,隐藏Controller无需暴露给外部的公有方法
        """

        def __init__(self, _controller: Controller):
            self.controller: Controller = _controller

        def Setup(self, _board: Board):
            self.controller.__board_ = _board
            self.controller.__board_.logger.Trace("setup controller")
            self.controller.setup()

        def Dismantle(self):
            self.controller.__board_.logger.Trace("dismantle controller")
            self.controller.dismantle()

    def __init__(self):
        self._logger: Logger = None
        self._config: Config = None
        self.__board: Board = None

    def _findModel(_uuid: str) -> Model:
        """查找一个数据层
        Args:
            _uuid: 数据层唯一识别码
        Returns:
            找到的数据层
        """
        inner = self.__board.getModelCenter().FindModel(_uuid)
        if None == inner:
            return None
        return inner.model

    def _findView(_uuid: str) -> View:
        """查找一个视图层
        Args:
            _uuid: 视图层唯一识别码
        Returns:
            找到的视图层
        """
        inner = self.__board.getViewCenter().FindView(_uuid)
        if None == inner:
            return None
        return inner.view

    def _findController(_uuid: str) -> View:
        """查找一个控制层
        Args:
            _uuid: 控制层唯一识别码
        Returns:
            找到的控制层
        """
        inner = self.__board.getControllerCenter().FindController(_uuid)
        if None == inner:
            return None
        return inner.controller

    def _findService(_uuid: str) -> View:
        """查找一个服务层
        Args:
            _uuid: 服务层唯一识别码
        Returns:
            找到的服务层
        """
        inner = self.__board.getServiceCenter().FindService(_uuid)
        if None == inner:
            return None
        return inner.service

    def setup():
        """控制层的安装"""
        pass

    def dismantle():
        """控制层的安装"""
        pass
