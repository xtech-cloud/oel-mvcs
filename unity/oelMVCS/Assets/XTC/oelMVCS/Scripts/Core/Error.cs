/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

namespace XTC.oelMVCS
{
    public class Error
    {
        public const int NULL = -1;
        public const int PARAM = -2;
        public const int ACCESS = -3;
        public const int EXCEPTION = -99;

        public int code
        {
            get;
            private set;
        }

        public string message
        {
            get;
            private set;
        }

        private static Error ok_ = new Error(0, "");

        public Error(int _code, string _message)
        {
            code = _code;
            message = _message;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", code, message);
        }

        public static Error OK
        {
            get
            {
                return ok_;
            }
        }

        public bool IsOK
        {
            get
            {
                return code == 0;
            }
        }

        public static Error NewNullErr(string _message, params object[] _args)
        {
            string message = string.Format(_message, _args);
            return new Error(NULL, message);
        }

        public static Error NewParamErr(string _message, params object[] _args)
        {
            string message = string.Format(_message, _args);
            return new Error(PARAM, message);
        }

        public static Error NewAccessErr(string _message, params object[] _args)
        {
            string message = string.Format(_message, _args);
            return new Error(ACCESS, message);
        }

        public static Error NewException(System.Exception _ex)
        {
            return new Error(EXCEPTION, _ex.Message);
        }
    }//class
}//namespace
