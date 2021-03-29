class Framework:
    def __init__(self):
        self.__board: Board = Board()
        self.__staticPipe : StaticPipc = StaticPipe(self.__board)
        self.__dynamicPipe : DynamicPipc = DynamicPipe(self.__board)

    def getStaticPipe(self):
        return self.staticPipe_

    def getDynamicPipe(self):
        return self.dynamicPipe_

    def setConfig(self, _config : Config):
        self.__board.setConfig(_config);

    def setLogger(self, _logger: Logger):
        self.__board.setLogger(_logger);


    def void Initialize(self):
        '''
        框架初始化，完成各层中心的实例化
        '''
        board_.logger.Info("initialize framework");
        board_.viewCenter = new ViewCenter(board_);
        board_.modelCenter = new ModelCenter(board_);
        board_.controllerCenter = new ControllerCenter(board_);
        board_.serviceCenter = new ServiceCenter(board_);

    def Setup(self):
        '''
        框架安装，完成各层中心已注册组件的安装
        此过程将调用各派生组件的setup方法
        '''
        board_.logger.Info("setup framework");
        board_.viewCenter.Setup();
        board_.serviceCenter.Setup();
        board_.modelCenter.Setup();
        board_.controllerCenter.Setup();

    def Dismantle(self):
        '''
        框架拆卸，完成各层中心已注册组件的拆卸
        此过程将调用各派生组件的dismantle方法
        '''
        board_.logger.Info("dismantle framework");
        board_.viewCenter.Dismantle();
        board_.serviceCenter.Dismantle();
        board_.modelCenter.Dismantle();
        board_.controllerCenter.Dismantle();


    def Release(self):
        '''
        框架销毁，完成各层中心的释放
        '''
        board_.logger.Info("release framework");
        board_.viewCenter = null;
        board_.serviceCenter = null;
        board_.modelCenter = null;
        board_.controllerCenter = null;
