using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ControllerCenter
    {
        private Dictionary<string, Controller.Inner> controllers  = new Dictionary<string, Controller.Inner>();

        private Board board_
        {
            get;
            set;
        }

        public ControllerCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Controller.Inner _inner)
        {
            board_.logger.Info("register controller {0}", _uuid);
            if (controllers.ContainsKey(_uuid))
                return Error.NewAccessErr("controller {0} exists", _uuid);
            controllers[_uuid] = _inner;
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

        public Controller.Inner FindController(string _uuid)
        {
            Controller.Inner inner = null;
            controllers.TryGetValue(_uuid, out inner);
            return inner;
        }

        public void Setup()
        {
            foreach (Controller.Inner inner in controllers.Values)
            {
                inner.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (Controller.Inner inner in controllers.Values)
            {
                inner.Dismantle();
            }
        }
    }
}//namespace
