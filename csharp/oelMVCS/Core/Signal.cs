/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class Signal
    {
        private Model model_;
        private List<System.Action<Model.Status?, object>> slotS_;

        public Signal(Model _model)
        {
            model_ = _model;
            slotS_ = new List<System.Action<Model.Status?, object>>();
        }

        public void Connect(System.Action<Model.Status?, object> _slot)
        {
            if (slotS_.Contains(_slot))
                return;
            slotS_.Add(_slot);
        }

        public void Disconnect(System.Action<Model.Status?, object> _slot)
        {
            if (!slotS_.Contains(_slot))
                return;
            slotS_.Remove(_slot);
        }

        public void Emit(object _data)
        {
            foreach(var slot in slotS_)
            {
                model_.emit(slot, _data);
            }
        }
    }
}