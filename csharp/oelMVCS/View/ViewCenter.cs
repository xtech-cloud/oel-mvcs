using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ViewCenter
    {
        public ViewCenter(Board _board)
        {
            board_ = _board;
        }

        public void HandleAction(string _action, Model.Status _status, object _obj)
        {
            foreach (View.Inner inner in units_.Values)
            {
                inner.Handle(_action, _status, _obj);
            }
        }

        public Error Register(string _uuid, View.Inner _inner)
        {
            board_.getLogger().Info("register {0}", _uuid);
            if (units_.ContainsKey(_uuid))
                return Error.NewAccessErr("{0} exists", _uuid);
            units_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger().Info("cancel {0}", _uuid);
            if (!units_.ContainsKey(_uuid))
                return Error.NewAccessErr("{0} not found", _uuid);
            units_.Remove(_uuid);
            return Error.OK;
        }

        public View.Inner FindUnit(string _uuid)
        {
            View.Inner inner = null;
            units_.TryGetValue(_uuid, out inner);
            return inner;
        }

        public void Setup()
        {
            foreach (View.Inner inner in units_.Values)
            {
                inner.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (View.Inner inner in units_.Values)
            {
                inner.Dismantle();
            }
        }

        protected Dictionary<string, View.Inner> units_ = new Dictionary<string, View.Inner>();

        private Board board_ = null;
    }
}//namespace
