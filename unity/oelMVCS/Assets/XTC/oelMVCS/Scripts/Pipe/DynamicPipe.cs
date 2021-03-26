using System.Collections.Generic;

namespace XTC.oelMVCS
{
    // 动态管线
    public class DynamicPipe
    {
        private Board board_
        {
            get;
            set;
        }

        public DynamicPipe(Board _board)
        {
            board_ = _board;
        }


        public Error PushModel(string _uuid, Model _model)
        {
            Model.Inner inner = new Model.Inner(_model);
            Error err = board_.modelCenter.Register(_uuid, inner);
            if (!err.IsOK)
                return err;
            inner.Setup(board_);
            return Error.OK;
        }

        public Error PopModel(string _uuid)
        {
            Model.Inner inner = board_.modelCenter.FindModel(_uuid);
            if (null == inner)
                return Error.NewAccessErr("model {0} not found", _uuid);
            inner.Dismantle();
            return board_.modelCenter.Cancel(_uuid);
        }

        public Error PushView(string _uuid, View _view)
        {
            View.Inner inner = new View.Inner(_view);
            Error err = board_.viewCenter.Register(_uuid, inner);
            if (!err.IsOK)
                return err;
            inner.Setup(board_);
            return Error.OK;
        }

        public Error PopView(string _uuid)
        {
            View.Inner inner = board_.viewCenter.FindView(_uuid);
            if (null == inner)
                return Error.NewAccessErr("view {0} not found", _uuid);
            inner.Dismantle();
            return board_.viewCenter.Cancel(_uuid);
        }

        public Error PushController(string _uuid, Controller _controller)
        {
            Controller.Inner inner = new Controller.Inner(_controller);
            Error err = board_.controllerCenter.Register(_uuid, inner);
            if (!err.IsOK)
                return err;
            inner.Setup(board_);
            return Error.OK;
        }

        public Error PopController(string _uuid)
        {
            Controller.Inner inner = board_.controllerCenter.FindController(_uuid);
            if (null == inner)
                return Error.NewAccessErr("controller {0} not found", _uuid);
            inner.Dismantle();
            return board_.controllerCenter.Cancel(_uuid);
        }

        public Error PushService(string _uuid, Service _service)
        {
            Service.Inner inner = new Service.Inner(_service);
            Error err = board_.serviceCenter.Register(_uuid, inner);
            if (!err.IsOK)
                return err;
            inner.Setup(board_);
            return Error.OK;
        }

        public Error PopService(string _uuid)
        {
            Service.Inner inner = board_.serviceCenter.FindService(_uuid);
            if (null == inner)
                return Error.NewAccessErr("controller {0} not found", _uuid);
            inner.Dismantle();
            return board_.serviceCenter.Cancel(_uuid);
        }
    }
}
