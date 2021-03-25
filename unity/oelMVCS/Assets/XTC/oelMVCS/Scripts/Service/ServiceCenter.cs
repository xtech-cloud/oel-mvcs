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

        private Dictionary<string, Service> services  = new Dictionary<string, Service>();

        private Board board_
        {
            get;
            set;
        }


        public ServiceCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Service _service)
        {
            board_.logger.Info("register service {0}", _uuid);

            if (services.ContainsKey(_uuid))
                return Error.NewAccessErr("services {0} exists", _uuid);
            services[_uuid] = _service;
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

        public Service FindService(string _uuid)
        {
            if (services.ContainsKey(_uuid))
                return services[_uuid];
            return null;
        }

        public void Setup()
        {
            storages = new Dictionary<string, string>();

            foreach (Service service in services.Values)
            {
                service.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (Service service in services.Values)
            {
                service.Dismantle();
            }
        }
    }
}//namespace
