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
            Error err = board_.modelCenter.Register(_uuid, _model);
            if (!err.IsOK)
                return err;
            _model.Setup(board_);
            return Error.OK;
        }

        public Error PopModel(string _uuid)
        {
            Model model = board_.modelCenter.FindModel(_uuid);
            if (null == model)
                return Error.NewAccessErr("model {0} not found", _uuid);
            model.Dismantle();
            return board_.modelCenter.Cancel(_uuid);
        }

        public Error PushView(string _uuid, View _view)
        {
            Error err = board_.viewCenter.Register(_uuid, _view);
            if (!err.IsOK)
                return err;
            _view.Setup(board_);
            return Error.OK;
        }

        public Error PopView(string _uuid)
        {
            View view = board_.viewCenter.FindView(_uuid);
            if (null == view)
                return Error.NewAccessErr("view {0} not found", _uuid);
            view.Dismantle();
            return board_.viewCenter.Cancel(_uuid);
        }

        public Error PushController(string _uuid, Controller _controller)
        {
            Error err = board_.controllerCenter.Register(_uuid, _controller);
            if (!err.IsOK)
                return err;
            _controller.Setup(board_);
            return Error.OK;
        }

        public Error PopController(string _uuid)
        {
            Controller controller = board_.controllerCenter.FindController(_uuid);
            if (null == controller)
                return Error.NewAccessErr("controller {0} not found", _uuid);
            controller.Dismantle();
            return board_.controllerCenter.Cancel(_uuid);
        }

        public Error PushService(string _uuid, Service _service)
        {
            Error err = board_.serviceCenter.Register(_uuid, _service);
            if (!err.IsOK)
                return err;
            _service.Setup(board_);
            return Error.OK;
        }

        public Error PopService(string _uuid)
        {
            Service service = board_.serviceCenter.FindService(_uuid);
            if (null == service)
                return Error.NewAccessErr("controller {0} not found", _uuid);
            service.Dismantle();
            return board_.serviceCenter.Cancel(_uuid);
        }
    }
}
