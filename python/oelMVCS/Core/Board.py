# -*- coding: utf-8 -*-

class Board:
    """内部通许主板"""
    def __init__(self):
        self.__config: Config = None
        self.__logger: Logger = None
        self.__modelCenter: ModelCenter = None
        self.__viewCenter: ViewCenter = None
        self.__controllerCenter: ControllerCenter = None
        self.__serviceCenter: ServiceCenter = None

    def getConfig() -> Config:
        return self.__config

    def getLogger() -> Logger:
        return self.__logger

    def getModelCenter() -> ModelCenter:
        return self.__modelCenter

    def getViewCenter() -> ViewCenter:
        return self.__viewCenter

    def getControllerCenter() -> ControllerCenter:
        return self.__controllerCenter

    def getServiceCenter() -> ServiceCenter:
        return self.__serviceCenter

    def setConfig(_value:Config):
        self.__config = _value

    def setLogger(_value:Logger):
        self.__logger = _value

    def setModelCenter(_value:ModelCenter):
        self.__modelCenter = _value

    def setViewCenter(_value:ViewCenter):
        self.__viewCenter = _value

    def setControllerCenter(_value:ControllerCenter):
        self.__controllerCenter = _value

    def setServiceCenter(_value:ServiceCenter):
        self.__serviceCenter = _value

