using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTC.oelMVCS
{

    public class View
    {

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

        protected ViewCenter viewCenter_
        {
            get;
            set;
        }

        private ServiceCenter serviceCenter_
        {
            get;
            set;
        }

        private Dictionary<string, Action<Model.Status, object>> handlers = new Dictionary<string, Action<Model.Status, object>>();

        public void Setup(Board _board)
        {
            logger_ = _board.logger;
            config_ = _board.config;
            viewCenter_ = _board.viewCenter;
            modelCenter_ = _board.modelCenter;
            serviceCenter_ = _board.serviceCenter;
            logger_.Trace("setup view");
            setup();
            logger_.Trace("bind events");
            bindEvents();
        }

        public void Dismantle()
        {
            logger_.Trace("unbind events");
            unbindEvents();
            logger_.Trace("dismantle view");
            dismantle();
        }

        public void Handle(string _action, Model.Status _status, object _data)
        {
            if (!handlers.ContainsKey(_action))
                return;
            handlers[_action](_status, _data);
        }

        protected virtual void dismantle()
        {

        }

        protected virtual void setup()
        {

        }

        protected Model findModel(string _uuid)
        {
            return modelCenter_.FindModel(_uuid);
        }

        protected View findView(string _uuid)
        {
            return viewCenter_.FindView(_uuid);
        }

        protected Service findService(string _uuid)
        {
            return serviceCenter_.FindService(_uuid);
        }

        protected virtual void bindEvents()
        {

        }

        protected virtual void unbindEvents()
        {

        }

        protected void route(string _action, Action<Model.Status, object> _handler)
        {
            handlers[_action] = _handler;
        }
    }
}//namespace
