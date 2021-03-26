import time

class Logger:

    __logger = None 

    def __init__(self):
        self.__logger = None

    def Inject(self, _logger):
        self.__logger = _logger

    def Trace(self, _params):
        if self.__logger == None:
            dt = time.strftime('%H:%M:%S', time.localtime())
            print('{} TRACE - {}'.format(dt, _params))
            return
        self.__logger.Trace(_params)

    def Debug(self, _params):
        if self.__logger == None:
            dt = time.strftime('%H:%M:%S', time.localtime())
            print('{} DEBUG - {}'.format(dt, _params))
            return
        self.__logger.Debug(_params)

    def Info(self, _params):
        if self.__logger == None:
            dt = time.strftime('%H:%M:%S', time.localtime())
            print('{} INFO  - {}'.format(dt, _params))
            return
        self.__logger.Trace(_params)

    def Warn(self, _params):
        if self.__logger == None:
            dt = time.strftime('%H:%M:%S', time.localtime())
            print('{} WARN  - {}'.format(dt, _params))
            return
        self.__logger.Warn(_params)

    def Error(self, _params):
        if self.__logger == None:
            dt = time.strftime('%H:%M:%S', time.localtime())
            print('{} ERROR - {}'.format(dt, _params))
            return
        self.__logger.Error(_params)
