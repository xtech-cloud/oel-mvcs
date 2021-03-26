using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ServiceCenter
    {
        public Dictionary<string, string> storages
        {
            get;
            private set;
        }

        private Dictionary<string, Service.Inner> services  = new Dictionary<string, Service.Inner>();

        private Board board_
        {
            get;
            set;
        }


        public ServiceCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Service.Inner _inner)
        {
            board_.logger.Info("register service {0}", _uuid);

            if (services.ContainsKey(_uuid))
                return Error.NewAccessErr("services {0} exists", _uuid);
            services[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.logger.Info("cancel service {0}", _uuid);
            if (!services.ContainsKey(_uuid))
                return Error.NewAccessErr("service {0} not found", _uuid);
            services.Remove(_uuid);
            return Error.OK;
        }

        public Service.Inner FindService(string _uuid)
        {
            Service.Inner inner = null;
            services.TryGetValue(_uuid, out inner);
            return inner;
        }

        public void Setup()
        {
            storages = new Dictionary<string, string>();

            foreach (Service.Inner inner in services.Values)
            {
                inner.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (Service.Inner inner in services.Values)
            {
                inner.Dismantle();
            }
        }
    }
}//namespace
