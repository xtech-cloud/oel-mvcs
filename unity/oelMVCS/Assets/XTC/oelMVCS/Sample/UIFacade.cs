using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTC.oelMVCS
{
    public class UIFacade : MonoBehaviour
    {
        public static Dictionary<string, UIFacade> uiFacades = new Dictionary<string, UIFacade>();
        public static Logger logger
        {
            get;
            set;
        }

        public string UUID = "";

        public static UIFacade Find(string _uuid)
        {
            if (uiFacades.ContainsKey(_uuid))
                return uiFacades[_uuid];
            return null;
        }

        public void Register()
        {
            logger.Info("register facade {0}", UUID);
            if (uiFacades.ContainsKey(UUID))
                throw new System.ArgumentException("facade {0} exists", UUID);
            else
                uiFacades[UUID] = this;
        }

        public void Cancel()
        {
            logger.Info("cancel facade {0}", UUID);
            if (uiFacades.ContainsKey(UUID))
                uiFacades.Remove(UUID);
        }
    }
}//namespace
