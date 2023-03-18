﻿/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.oelMVCS
{
    internal class ServiceCenter
    {
        public ServiceCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Service.Inner _inner)
        {
            board_.getLogger()?.Info("register service: {0}", _uuid);
            if (unitS_.ContainsKey(_uuid))
                return Error.NewAccessErr("service {0} exists", _uuid);
            unitS_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger()?.Info("cancel service: {0}", _uuid);
            if (!unitS_.ContainsKey(_uuid))
                return Error.NewAccessErr("service {0} not found", _uuid);
            unitS_.Remove(_uuid);
            return Error.OK;
        }

        public Service.Inner? FindUnit(string _uuid)
        {
            Service.Inner? inner;
            if (!unitS_.TryGetValue(_uuid, out inner))
                return null;
            return inner;
        }

        public void PreSetup()
        {
            board_.getLogger()?.Info("perSetup services");
            foreach (Service.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("perSetup service: {0}", inner.getUnit().getUID());
                inner.PreSetup();
            }
        }

        public void Setup()
        {
            board_.getLogger()?.Info("setup services");
            foreach (Service.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("setup service: {0}", inner.getUnit().getUID());
                inner.Setup();
            }
        }

        public void PostSetup()
        {
            board_.getLogger()?.Info("postSetup services");
            foreach (Service.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("postSetup service: {0}", inner.getUnit().getUID());
                inner.PostSetup();
            }
        }

        public void PreDismantle()
        {
            board_.getLogger()?.Info("preDismantle services");
            foreach (Service.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("preDismantle service: {0}", inner.getUnit().getUID());
                inner.PreDismantle();
            }
        }

        public void Dismantle()
        {
            board_.getLogger()?.Info("dismantle services");
            foreach (Service.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("dismantle service: {0}", inner.getUnit().getUID());
                inner.Dismantle();
            }
        }

        public void PostDismantle()
        {
            board_.getLogger()?.Info("postDismantle services");
            foreach (Service.Inner inner in unitS_.Values)
            {
                board_.getLogger()?.Debug("postDismantle service: {0}", inner.getUnit().getUID());
                inner.PostDismantle();
            }
        }

        private Dictionary<string, Service.Inner> unitS_ = new Dictionary<string, Service.Inner>();
        private Board board_;
    }
}//namespace
