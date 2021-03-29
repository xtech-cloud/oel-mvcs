# -*- coding: utf-8 -*-

class StaticPipe:
    """静态管线
    """

    def __init__(self, _board:Board):
        self.__board = _board

        def RegisterModel(_uuid:str, _model:Model) -> Error:
            """注册数据层
            Args:
                _uuid: 数据层唯一识别码
                _model: 数据层实例
            Returns:
                错误
            """
            inner = Model.Inner(_model)
            return self.__board.getModelCenter().Register(_uuid, inner)

        def CancelModel(_uuid:str) -> Error:
            """注销数据层
            Args:
                _uuid: 数据层唯一识别码
            Returns:
                错误
            """
            return self.__board.getModelCenter().Cancel(_uuid)

        def RegisterView(_uuid:str, _view:View) -> Error:
            """注册视图层
            Args:
                _uuid: 视图层唯一识别码
                _model:  视图层实例
            Returns:
                错误
            """
            inner = View.Inner(_view)
            return self.__board.getViewCenter().Register(_uuid, inner)

        def CancelView(_uuid:str) -> Error:
            """注销视图层
            Args:
                _uuid: 视图层唯一识别码
            Returns:
                错误
            """
            return self.__board.getViewCenter().Cancel(_uuid)

        def RegisterController(_uuid:str, _controller:Controller) -> Error:
            """注册控制层 
            Args:
                _uuid: 控制层唯一识别码
                _model: 控制层实例
            Returns:
                错误
            """
            inner = Controller.Inner(_controller)
            return self.__board.getControllerCenter().Register(_uuid, inner)

        def CancelController(_uuid:str) -> Error:
            """注销控制层
            Args:
                _uuid: 控制层唯一识别码
            Returns:
                错误
            """
            return self.__board.getControllerCenter().Cancel(_uuid)

        def RegisterService(_uuid:str, _service:Service) -> Error:
            """注册服务层
            Args:
                _uuid: 服务层唯一识别码
                _model:  服务层实例
            Returns:
                错误
            """
            inner = Service.Inner(_service)
            return self.__board.getServiceCenter().Register(_uuid, inner)

        def CancelService(_uuid:str) -> Error:
            """注销服务层
            Args:
                _uuid: 服务层唯一识别码
            Returns:
                错误
            """
            return self.__board.getServiceCenter().Cancel(_uuid)

