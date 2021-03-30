using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ModelCenter
    {
        public ModelCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Model.Inner _inner)
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

        public Model.Inner FindUnit(string _uuid)
        {
            Model.Inner inner = null;
            units_.TryGetValue(_uuid, out inner);
            return inner;
        }

        public void Setup()
        {
            foreach (Model.Inner inner in units_.Values)
            {
                inner.PreSetup();
            }
            foreach (Model.Inner inner in units_.Values)
            {
                inner.Setup();
            }
            foreach (Model.Inner inner in units_.Values)
            {
                inner.PostSetup();
            }
        }

        public void Dismantle()
        {
            foreach (Model.Inner inner in units_.Values)
            {
                inner.PreDismantle();
            }
            foreach (Model.Inner inner in units_.Values)
            {
                inner.Dismantle();
            }
            foreach (Model.Inner inner in units_.Values)
            {
                inner.PostDismantle();
            }
        }

        public Error PushStatus(string _uuid, Model.Status _status)
        {
            board_.getLogger().Info("push status {0}", _uuid);

            if (status_.ContainsKey(_uuid))
                return Error.NewAccessErr("status {0} exists", _uuid);
            status_[_uuid] = _status;
            return Error.OK;
        }

        public Error PopStatus(string _uuid)
        {
            board_.getLogger().Info("pop status {0}", _uuid);

            if (!status_.ContainsKey(_uuid))
                return Error.NewAccessErr("status {0} not found", _uuid);
            status_.Remove(_uuid);
            return Error.OK;
        }

        public Model.Status FindStatus(string _uuid)
        {
            Model.Status status = null;
            status_.TryGetValue(_uuid, out status);
            return status;
        }

        public void Broadcast(string _action, Model.Status _status, object _data)
        {
            board_.getViewCenter().HandleAction(_action, _status, _data);
        }

        private Dictionary<string, Model.Inner> units_ = new Dictionary<string, Model.Inner>();

        // 状态列表
        private Dictionary<string, Model.Status> status_ = new Dictionary<string, Model.Status>();

        private Board board_ = null;
    }
}//namespace
