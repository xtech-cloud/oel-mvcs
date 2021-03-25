using System.Collections.Generic;

namespace XTC.oelMVCS
{

    public class Model
    {

        public class Status
        {
            public int code;
            public string message;

            public string uuid
            {
                get;
                private set;
            }

            // 访问指定状态
            public Status Access(string _uuid)
            {
                return null;
            }
        }


        public Dictionary<string, object> property
        {
            get;
            private set;
        }

        protected Logger logger_
        {
            get;
            set;
        }
        protected Config config_
        {
            get;
            set;
        }

        private ModelCenter modelCenter_
        {
            get;
            set;
        }

        private ControllerCenter controllerCenter_
        {
            get;
            set;
        }

        protected Status status_
        {
            get;
            set;
        }

        public void Setup(Board _board)
        {
            logger_ = _board.logger;
            config_ = _board.config;
            modelCenter_ = _board.modelCenter;
            controllerCenter_ = _board.controllerCenter;

            property = new Dictionary<string, object>();

            logger_.Trace("setup model");
            setup();
        }

        public void Dismantle()
        {
            logger_.Trace("dismantle model");
            dismantle();
        }

        public void Broadcast(string _action, object _data)
        {
            modelCenter_.Broadcast(_action, status_, _data);
        }

        protected Model findModel(string _uuid)
        {
            return modelCenter_.FindModel(_uuid);
        }

        protected Controller findController(string _uuid)
        {
            return controllerCenter_.FindController(_uuid);
        }


        // 派生类需要实现的方法
        protected virtual void setup()
        {

        }

        // 派生类需要实现的方法
        protected virtual void dismantle()
        {

        }
    }
}//namespace
