using System.Collections.Generic;

namespace XTC.oelMVCS
{

    public class Model
    {
        #region
        // 内部类，用于接口隔离,隐藏Model无需暴露给外部的公有方法
        public class Inner
        {
            public Model model
            {
                get;
                private set;
            }

            public Inner(Model _model)
            {
                model = _model;
            }

            internal void Setup(Board _board)
            {
                model.board_ = _board;
                model.board_.logger.Trace("setup model");
                model.setup();
            }

            internal void Dismantle()
            {
                model.board_.logger.Trace("dismantle model");
                model.dismantle();
            }
        }
        #endregion

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
            get
            {
                return board_.logger;
            }
        }

        protected Config config_
        {
            get
            {
                return board_.config;
            }
        }


        protected Status status_
        {
            get;
            set;
        }

        private Board board_
        {
            get;
            set;
        }

        public void Broadcast(string _action, object _data)
        {
            board_.modelCenter.Broadcast(_action, status_, _data);
        }

        protected Model findModel(string _uuid)
        {
            Model.Inner inner = board_.modelCenter.FindModel(_uuid);
            if (null == inner)
                return null;
            return inner.model;
        }

        protected Controller findController(string _uuid)
        {
            Controller.Inner inner = board_.controllerCenter.FindController(_uuid);
            if (null == inner)
                return null;
            return inner.controller;
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
