using System.Collections.Generic;

namespace XTC.oelMVCS
{
    // 静态管线
    public class StaticPipe
    {
        private Board board_
        {
            get;
            set;
        }

        public StaticPipe(Board _board)
        {
            board_ = _board;
        }


        public Error RegisterModel(string _uuid, Model _model)
        {
            Model.Inner inner = new Model.Inner(_model);
            return board_.modelCenter.Register(_uuid, inner);
        }

        public Error CancelModel(string _uuid)
        {
            return board_.modelCenter.Cancel(_uuid);
        }

        public Error RegisterView(string _uuid, View _view)
        {
            View.Inner inner = new View.Inner(_view);
            return board_.viewCenter.Register(_uuid, inner);
        }

        public Error CancelView(string _uuid)
        {
            return board_.viewCenter.Cancel(_uuid);
        }

        public Error RegisterController(string _uuid, Controller _controller)
        {
            Controller.Inner inner = new Controller.Inner(_controller);
            return board_.controllerCenter.Register(_uuid, inner);
        }

        public Error CancelController(string _uuid)
        {
            return board_.controllerCenter.Cancel(_uuid);
        }

        public Error RegisterService(string _uuid, Service _service)
        {
            Service.Inner inner = new Service.Inner(_service);
            return board_.serviceCenter.Register(_uuid, inner);
        }

        public Error CancelService(string _uuid)
        {
            return board_.serviceCenter.Cancel(_uuid);
        }
    }
}
