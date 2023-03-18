﻿/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.oelMVCS
{
    internal class ControllerCenter
    {
        public ControllerCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Controller.Inner _inner)
        {
            board_.getLogger()?.Info("register controller: {0}", _uuid);
            if (unitS_.ContainsKey(_uuid))
                return Error.NewAccessErr("controller {0} exists", _uuid);
            unitS_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger()?.Info("cancel controller: {0}", _uuid);
            if (!unitS_.ContainsKey(_uuid))
                return Error.NewAccessErr("controller {0} not found", _uuid);
            unitS_.Remove(_uuid);
            return Error.OK;
        }

        public Controller.Inner? FindUnit(string _uuid)
        {
            Controller.Inner? inner;
            if (!unitS_.TryGetValue(_uuid, out inner))
                return null;
            return inner;
        }

        public void PreSetup()
        {
            board_.getLogger()?.Info("perSetup controllers");
            foreach (Controller.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("perSetup controller: {0}", inner.getUnit().getUID());
                inner.PreSetup();
            }
        }

        public void Setup()
        {
            board_.getLogger()?.Info("setup controllers");
            foreach (Controller.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("setup controller: {0}", inner.getUnit().getUID());
                inner.Setup();
            }
        }

        public void PostSetup()
        {
            board_.getLogger()?.Info("postSetup controllers");
            foreach (Controller.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("postSetup controller: {0}", inner.getUnit().getUID());
                inner.PostSetup();
            }
        }

        public void PreDismantle()
        {
            board_.getLogger()?.Info("perDismantle controllers");
            foreach (Controller.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("perDismantle controller: {0}", inner.getUnit().getUID());
                inner.PreDismantle();
            }
        }

        public void Dismantle()
        {
            board_.getLogger()?.Info("dismantle controllers");
            foreach (Controller.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("dismantle controller: {0}", inner.getUnit().getUID());
                inner.Dismantle();
            }
        }

        public void PostDismantle()
        {
            board_.getLogger()?.Info("postDismantle controllers");
            foreach (Controller.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("postDismantle controller: {0}", inner.getUnit().getUID());
                inner.PostDismantle();
            }
        }

        private Dictionary<string, Controller.Inner> unitS_ = new Dictionary<string, Controller.Inner>();

        private Board board_;
    }
}//namespace
