using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ServiceCenter 
    {
        public ServiceCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Service.Inner _inner)
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

        public Service.Inner FindUnit(string _uuid)
        {
            Service.Inner inner = null;
            units_.TryGetValue(_uuid, out inner);
            return inner;
        }

        public void Setup()
        {
            foreach (Service.Inner inner in units_.Values)
            {
                inner.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (Service.Inner inner in units_.Values)
            {
                inner.Dismantle();
            }
        }

        private Dictionary<string, Service.Inner> units_ = new Dictionary<string, Service.Inner>();
        private Board board_ = null;
    }
}//namespace
