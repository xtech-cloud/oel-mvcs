using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class ViewCenter
    {
        private Board board_
        {
            get;
            set;
        }

        private Dictionary<string, View> views = new Dictionary<string, View>();

        public ViewCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, View _view)
        {
            board_.logger.Info("register view {0}", _uuid);
            if (views.ContainsKey(_uuid))
                return Error.NewAccessErr("view {0} exists", _uuid);
            views[_uuid] = _view;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.logger.Info("cancel view {0}", _uuid);
            if (!views.ContainsKey(_uuid))
                return Error.NewAccessErr("view {0} not found", _uuid);
            views.Remove(_uuid);
            return Error.OK;
        }


        public View FindView(string _uuid)
        {
            if (views.ContainsKey(_uuid))
                return views[_uuid];
            return null;
        }


        public void Setup()
        {
            foreach (View view in views.Values)
            {
                view.Setup(board_);
            }
        }

        public void Dismantle()
        {
            foreach (View view in views.Values)
            {
                view.Dismantle();
            }
        }

        public void HandleAction(string _action, Model.Status _status, object _obj)
        {
            foreach (View view in views.Values)
            {
                view.Handle(_action, _status, _obj);
            }
        }
    }
}//namespace
