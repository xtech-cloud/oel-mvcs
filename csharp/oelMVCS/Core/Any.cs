
namespace XTC.oelMVCS
{
    public class Any
    {
        public enum Tag
        {
            NULL = 0,
            StringValue = 1,
            IntValue = 2,
            LongValue = 3,
            FloatValue = 4,
            DoubleValue = 5,
            BoolValue = 6
        }

        private object value_ = null;
        private Tag tag_ = Tag.NULL;

        public Any()
        {
        }

        public static Any FromString(string _value)
        {
            Any any = new Any();
            any.tag_ = Tag.StringValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromFloat(float _value)
        {
            Any any = new Any();
            any.tag_ = Tag.FloatValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromDouble(double _value)
        {
            Any any = new Any();
            any.tag_ = Tag.DoubleValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromBool(bool _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromInt(int _value)
        {
            Any any = new Any();
            any.tag_ = Tag.IntValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromLong(long _value)
        {
            Any any = new Any();
            any.tag_ = Tag.LongValue;
            any.value_ = _value;
            return any;
        }

        public bool IsNull()
        {
            return tag_ == Tag.NULL;
        }

        public bool IsString()
        {
            return tag_ == Tag.StringValue;
        }

        public bool IsInt()
        {
            return tag_ == Tag.IntValue;
        }

        public bool IsLong()
        {
            return tag_ == Tag.LongValue;
        }

        public bool IsFloat()
        {
            return tag_ == Tag.FloatValue;
        }

        public bool IsDouble()
        {
            return tag_ == Tag.DoubleValue;
        }

        public bool IsBool()
        {
            return tag_ == Tag.BoolValue;
        }

        public string AsString()
        {
            if (IsString())
                return (string)value_;
            return "";
        }


        public int AsInt()
        {
            if (IsInt())
                return (int)value_;
            return 0;
        }

        public long AsLong()
        {
            if (IsLong())
                return (long)value_;
            return 0;
        }

        public float AsFloat()
        {
            if (IsFloat())
                return (float)value_;
            return 0.0f;
        }

        public double AsDouble()
        {
            if (IsDouble())
                return (double)value_;
            return 0.0;
        }

        public bool AsBool()
        {
            if (IsBool())
                return (bool)value_;
            return false;
        }

        public object AsObject()
        {
            return value_;
        }
    }//class
}//namespace
