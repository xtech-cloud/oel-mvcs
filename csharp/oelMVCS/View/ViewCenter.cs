﻿/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.oelMVCS
{
    internal class ViewCenter
    {
        public ViewCenter(Board _board)
        {
            board_ = _board;
        }

        public void RouteMessage(string _message, Model.Status? _status, object _obj)
        {
            foreach (View.Inner inner in unitS_.Values)
            {
                inner.RouteMessage(_message, _status, _obj);
            }
        }

        public Error Register(string _uuid, View.Inner _inner)
        {
            board_.getLogger()?.Info("register view: {0}", _uuid);
            if (unitS_.ContainsKey(_uuid))
                return Error.NewAccessErr("view {0} exists", _uuid);
            unitS_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger()?.Info("cancel view: {0}", _uuid);
            if (!unitS_.ContainsKey(_uuid))
                return Error.NewAccessErr("view {0} not found", _uuid);
            unitS_.Remove(_uuid);
            return Error.OK;
        }

        public View.Inner? FindUnit(string _uuid)
        {
            View.Inner? inner;
            if (!unitS_.TryGetValue(_uuid, out inner))
                return null;
            return inner;
        }

        public Error PushFacade(View.Facade _facade)
        {
            board_.getLogger()?.Info("push facade: {0}", _facade.getUID());

            if (facadeS_.ContainsKey(_facade.getUID()))
                return Error.NewAccessErr("facade {0} exists", _facade.getUID());
            facadeS_[_facade.getUID()] = _facade;
            return Error.OK;
        }

        public Error PopFacade(View.Facade _facade)
        {
            board_.getLogger()?.Info("pop facade: {0}", _facade.getUID());

            if (!facadeS_.ContainsKey(_facade.getUID()))
                return Error.NewAccessErr("facade {0} not found", _facade.getUID());
            facadeS_.Remove(_facade.getUID());
            return Error.OK;
        }

        public View.Facade? FindFacade(string _uuid)
        {
            View.Facade? facade;
            if (!facadeS_.TryGetValue(_uuid, out facade))
                return null;
            return facade;
        }

        public void PreSetup()
        {
            board_.getLogger()?.Info("preSetup views");
            foreach (View.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("preSetup view: {0}", inner.getUnit().getUID());
                inner.PreSetup();
            }
        }

        public void Setup()
        {
            board_.getLogger()?.Info("setup views");
            foreach (View.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("setup view: {0}", inner.getUnit().getUID());
                inner.Setup();
            }
        }

        public void PostSetup()
        {
            board_.getLogger()?.Info("postSetup views");
            foreach (View.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("postSetup view: {0}", inner.getUnit().getUID());
                inner.PostSetup();
            }
        }

        public void PreDismantle()
        {
            board_.getLogger()?.Info("preDismantle views");
            foreach (View.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("preDismantle view: {0}", inner.getUnit().getUID());
                inner.PreDismantle();
            }
        }

        public void Dismantle()
        {
            board_.getLogger()?.Info("dismantle views");
            foreach (View.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("dismantle view: {0}", inner.getUnit().getUID());
                inner.Dismantle();
            }
        }

        public void PostDismantle()
        {
            board_.getLogger()?.Info("postDismantle views");
            foreach (View.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("postDismantle view: {0}", inner.getUnit().getUID());
                inner.PostDismantle();
            }
        }

        private Dictionary<string, View.Inner> unitS_ = new Dictionary<string, View.Inner>();
        private Dictionary<string, View.Facade> facadeS_ = new Dictionary<string, View.Facade>();

        private Board board_;
    }
}//namespace
