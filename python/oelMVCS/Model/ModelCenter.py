using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ModelCenter
    {
        // 数据列表
        private Dictionary<string, Model.Inner> models = new Dictionary<string, Model.Inner>();
        // 状态列表
        private Dictionary<string, Model.Status> status = new Dictionary<string, Model.Status>();

        private Board board_
        {
            get;
            set;
        }

        public ModelCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Model.Inner _inner)
        {
            board_.logger.Info("register model {0}", _uuid);

            if (models.ContainsKey(_uuid))
                return Error.NewAccessErr("model {0} exists", _uuid);
            models[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.logger.Info("cancel model {0}", _uuid);

            if (!models.ContainsKey(_uuid))
                return Error.NewAccessErr("model {0} not found", _uuid);
            models.Remove(_uuid);
            return Error.OK;
        }

        public Model.Inner FindModel(string _uuid)
        {
            Model.Inner inner = null;
            models.TryGetValue(_uuid, out inner);
            return inner;
        }

        public void Setup()
        {
            foreach (Model.Inner inner in models.Values)
            {
                inner.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (Model.Inner inner in models.Values)
            {
                inner.Dismantle();
            }
        }

        public Error PushStatus(string _uuid, Model.Status _status)
        {
            board_.logger.Info("push status {0}", _uuid);

            if (status.ContainsKey(_uuid))
                return Error.NewAccessErr("status {0} exists", _uuid);
            status[_uuid] = _status;
            return Error.OK;
        }

        public Error PopStatus(string _uuid)
        {
            board_.logger.Info("pop status {0}", _uuid);

            if (!status.ContainsKey(_uuid))
                return Error.NewAccessErr("status {0} not found", _uuid);
            status.Remove(_uuid);
            return Error.OK;
        }

        public Model.Status FindStatus(string _uuid)
        {
            Model.Status s = null;
            status.TryGetValue(_uuid, out s);
            return s;
        }

        public void Broadcast(string _action, Model.Status _status, object _data)
        {
            board_.viewCenter.HandleAction(_action, _status, _data);
        }
    }
}//namespace
