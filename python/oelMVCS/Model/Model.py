# -*- coding: utf-8 -*-

    /// <summary>
    /// 数据层
    /// </summary>
class Model:
    """
    数据层
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
                self.model.__board.getLogger().Trace("dismantle model";
                self.model.dismantle()

        class Status:
            """
            状态
            """
            class Factory:
                """
                内部工厂
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
                """
                访问数据中心中的已注册状态
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


        /// <summary>向视图层广播消息</summary>
        /// <param name="_action">行为</param>
        /// <param name="_data">数据</param>
    def Broadcast(self, _action:str, _data):
            self.__board.getModelCenter().Broadcast(_action, status_, _data);

        /// <summary>
        /// 查找一个数据层
        /// </summary>
        /// <param name="_uuid"> 数据层唯一识别码</param>
        /// <returns>找到的数据层</returns>
        protected Model findModel(string _uuid)
        {
            Model.Inner inner = board_.modelCenter.FindModel(_uuid);
            if (null == inner)
                return null;
            return inner.model;
        }

        /// <summary>
        /// 查找一个控制层
        /// </summary>
        /// <param name="_uuid"> 控制层唯一识别码</param>
        /// <returns>找到的控制层</returns>
        protected Controller findController(string _uuid)
        {
            Controller.Inner inner = board_.controllerCenter.FindController(_uuid);
            if (null == inner)
                return null;
            return inner.controller;
        }

        /// <summary>
        /// 从数据层中创建一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        /// <returns>状态<returns>
        protected Model.Status spawnStatus<T>(string _uuid, out Error _err) where T: Model.Status, new ()
        {
            Model.Status.Factory factory = new Model.Status.Factory();
            Status status = factory.New<T>(board_, _uuid);
            _err = board_.modelCenter.PushStatus(_uuid, status);
            if (_err.IsOK)
                return status;
            return null;
        }

        /// <summary>
        /// 从数据中心中销毁一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        protected void killStatus(string _uuid, out Error _err)
        {
            _err = board_.modelCenter.PopStatus(_uuid);
        }

        /// <summary>
        /// 控制层的安装
        /// </summary>
        protected virtual void setup()
        {

        }

        /// <summary>
        /// 控制层的拆卸
        /// </summary>
        protected virtual void dismantle()
        {

        }
    }
}//namespace
