/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

namespace XTC.oelMVCS
{
    /// <summary>动态管线</summary>
    public class DynamicPipe
    {
        internal DynamicPipe(Board _board)
        {
            board_ = _board;
        }


        /// <summary>添加数据层</summary>
        /// <param name="_model">数据层实列</param>
        /// <returns>错误</returns>
        public Error PushModel(Model? _model)
        {
            if (null == _model)
                return Error.NewNullErr("args is null");
            Model.Inner inner = new Model.Inner(_model, board_);
            Error err = board_.getModelCenter().Register(_model.getUID(), inner);
            if (!Error.IsOK(err))
                return err;

            board_.getLogger()?.Debug("perSetup model: {0}", inner.getUnit().getUID());
            inner.PreSetup();
            board_.getLogger()?.Debug("setup model: {0}", inner.getUnit().getUID());
            inner.Setup();
            board_.getLogger()?.Debug("postetup model: {0}", inner.getUnit().getUID());
            inner.PostSetup();
            return Error.OK;
        }

        /// <summary>删除数据层</summary>
        /// <param name="_model">数据层实列</param>
        /// <returns>错误</returns>
        public Error PopModel(Model? _model)
        {
            if (null == _model)
                return Error.NewNullErr("args is null");
            Model.Inner? inner = board_.getModelCenter().FindUnit(_model.getUID());
            if (null == inner)
                return Error.NewAccessErr("model {0} not found", _model.getUID());
            board_.getLogger()?.Debug("preDismantle model: {0}", inner.getUnit().getUID());
            inner.PreDismantle();
            board_.getLogger()?.Debug("dismantle model: {0}", inner.getUnit().getUID());
            inner.Dismantle();
            board_.getLogger()?.Debug("postDismantle model: {0}", inner.getUnit().getUID());
            inner.PostDismantle();
            return board_.getModelCenter().Cancel(_model.getUID());
        }

        /// <summary>添加视图层</summary>
        /// <param name="_view">视图层实列</param>
        /// <returns>错误</returns>
        public Error PushView(View? _view)
        {
            if (null == _view)
                return Error.NewNullErr("args is null");
            View.Inner inner = new View.Inner(_view, board_);
            Error err = board_.getViewCenter().Register(_view.getUID(), inner);
            if (!Error.IsOK(err))
                return err;
            board_.getLogger()?.Debug("preSetup view: {0}", inner.getUnit().getUID());
            inner.PreSetup();
            board_.getLogger()?.Debug("setup view: {0}", inner.getUnit().getUID());
            inner.Setup();
            board_.getLogger()?.Debug("postSetup view: {0}", inner.getUnit().getUID());
            inner.PostSetup();
            return Error.OK;
        }

        /// <summary>删除视图层</summary>
        /// <param name="_view">视图层实列</param>
        /// <returns>错误</returns>
        public Error PopView(View? _view)
        {
            if (null == _view)
                return Error.NewNullErr("args is null");
            View.Inner? inner = board_.getViewCenter().FindUnit(_view.getUID());
            if (null == inner)
                return Error.NewAccessErr("view {0} not found", _view.getUID());
            board_.getLogger()?.Debug("preDismantle view: {0}", inner.getUnit().getUID());
            inner.PreDismantle();
            board_.getLogger()?.Debug("dismantle view: {0}", inner.getUnit().getUID());
            inner.Dismantle();
            board_.getLogger()?.Debug("postDismantle view: {0}", inner.getUnit().getUID());
            inner.PostDismantle();
            return board_.getViewCenter().Cancel(_view.getUID());
        }

        /// <summary>添加控制层</summary>
        /// <param name="_controller">控制层实列</param>
        /// <returns>错误</returns>
        public Error PushController(Controller? _controller)
        {
            if (null == _controller)
                return Error.NewNullErr("args is null");
            Controller.Inner inner = new Controller.Inner(_controller, board_);
            Error err = board_.getControllerCenter().Register(_controller.getUID(), inner);
            if (!Error.IsOK(err))
                return err;
            board_.getLogger()?.Debug("preSetup controller: {0}", inner.getUnit().getUID());
            inner.PreSetup();
            board_.getLogger()?.Debug("setup controller: {0}", inner.getUnit().getUID());
            inner.Setup();
            board_.getLogger()?.Debug("postSetup controller: {0}", inner.getUnit().getUID());
            inner.PostSetup();
            return Error.OK;
        }

        /// <summary>删除控制层</summary>
        /// <param name="_controller">控制层实列</param>
        /// <returns>错误</returns>
        public Error PopController(Controller? _controller)
        {
            if (null == _controller)
                return Error.NewNullErr("args is null");
            Controller.Inner? inner = board_.getControllerCenter().FindUnit(_controller.getUID());
            if (null == inner)
                return Error.NewAccessErr("controller {0} not found", _controller.getUID());
            board_.getLogger()?.Debug("preDismantle controller: {0}", inner.getUnit().getUID());
            inner.PreDismantle();
            board_.getLogger()?.Debug("dismantle controller: {0}", inner.getUnit().getUID());
            inner.Dismantle();
            board_.getLogger()?.Debug("postDismantle controller: {0}", inner.getUnit().getUID());
            inner.PostDismantle();
            return board_.getControllerCenter().Cancel(_controller.getUID());
        }

        /// <summary>添加服务层</summary>
        /// <param name="_service">服务层实列</param>
        /// <returns>错误</returns>
        public Error PushService(Service? _service)
        {
            if (null == _service)
                return Error.NewNullErr("args is null");
            Service.Inner inner = new Service.Inner(_service, board_);
            Error err = board_.getServiceCenter().Register(_service.getUID(), inner);
            if (!Error.IsOK(err))
                return err;
            board_.getLogger()?.Debug("preSetup service: {0}", inner.getUnit().getUID());
            inner.PreSetup();
            board_.getLogger()?.Debug("setup service: {0}", inner.getUnit().getUID());
            inner.Setup();
            board_.getLogger()?.Debug("postSetup service: {0}", inner.getUnit().getUID());
            inner.PostSetup();
            return Error.OK;
        }

        /// <summary>删除服务层</summary>
        /// <param name="_service">服务层实列</param>
        /// <returns>错误</returns>
        public Error PopService(Service? _service)
        {
            if (null == _service)
                return Error.NewNullErr("args is null");
            Service.Inner? inner = board_.getServiceCenter().FindUnit(_service.getUID());
            if (null == inner)
                return Error.NewAccessErr("service {0} not found", _service.getUID());
            board_.getLogger()?.Debug("preDismantle service: {0}", inner.getUnit().getUID());
            inner.PreDismantle();
            board_.getLogger()?.Debug("dismantle service: {0}", inner.getUnit().getUID());
            inner.Dismantle();
            board_.getLogger()?.Debug("postDismantle service: {0}", inner.getUnit().getUID());
            inner.PostDismantle();
            return board_.getServiceCenter().Cancel(_service.getUID());
        }

        /// <summary>添加UI装饰</summary>
        /// <param name="_facade">UI装饰实例</param>
        /// <returns>错误</returns>
        public Error PushFacade(View.Facade? _facade)
        {
            if (null == _facade)
                return Error.NewNullErr("args is null");
            return board_.getViewCenter().PushFacade(_facade);
        }

        /// <summary>删除UI装饰</summary>
        /// <param name="_facade">UI装饰实例</param>
        /// <returns>错误</returns>
        public Error PopFacade(View.Facade? _facade)
        {
            if (null == _facade)
                return Error.NewNullErr("args is null");
            return board_.getViewCenter().PopFacade(_facade);
        }

        private Board board_;

    }
}
