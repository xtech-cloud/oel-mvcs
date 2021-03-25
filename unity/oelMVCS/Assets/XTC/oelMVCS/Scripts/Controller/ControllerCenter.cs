using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ControllerCenter
    {
        private Dictionary<string, Controller> controllers  = new Dictionary<string, Controller>();

        private Board board_
        {
            get;
            set;
        }

        public ControllerCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Controller _controller)
        {
            board_.logger.Info("register controller {0}", _uuid);
            if (controllers.ContainsKey(_uuid))
                return Error.NewAccessErr("controller {0} exists", _uuid);
            controllers[_uuid] = _controller;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.logger.Info("cancel controller {0}", _uuid);
            if (!controllers.ContainsKey(_uuid))
                return Error.NewAccessErr("controller {0} not found", _uuid);
            controllers.Remove(_uuid);
            return Error.OK;
        }

        public Controller FindController(string _uuid)
        {
            if (controllers.ContainsKey(_uuid))
                return controllers[_uuid];
            return null;
        }

        public void Setup()
        {
            foreach (Controller controller in controllers.Values)
            {
                controller.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (Controller controller in controllers.Values)
            {
                controller.Dismantle();
            }
        }
    }
}//namespace
