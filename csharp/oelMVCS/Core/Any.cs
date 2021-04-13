
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
            BoolValue = 6,
            StringAryValue = 11,
            IntAryValue = 12,
            LongAryValue = 13,
            FloatAryValue = 14,
            DoubleAryValue = 15,
            BoolAryValue = 16,
            ObjectValue = 99
        }

        private object value_ = null;
        private Tag tag_ = Tag.NULL;

        public Any()
        {
        }

        public static Any FromObject(object _value)
        {
            Any any = new Any();
            any.tag_ = Tag.ObjectValue;
            any.value_ = _value;
            return any;
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

        public static Any FromStringAry(string[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.StringAryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromFloatAry(float[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.FloatAryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromDoubleAry(double[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.DoubleAryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromBoolAry(bool[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolAryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromIntAry(int[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.IntAryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromLongAry(long[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.LongAryValue;
            any.value_ = _value;
            return any;
        }

        public bool IsNull()
        {
            return tag_ == Tag.NULL;
        }

        public bool IsObject()
        {
            return tag_ == Tag.ObjectValue;
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
        public bool IsStringAry()
        {
            return tag_ == Tag.StringAryValue;
        }

        public bool IsIntAry()
        {
            return tag_ == Tag.IntAryValue;
        }

        public bool IsLongAry()
        {
            return tag_ == Tag.LongAryValue;
        }

        public bool IsFloatAry()
        {
            return tag_ == Tag.FloatAryValue;
        }

        public bool IsDoubleAry()
        {
            return tag_ == Tag.DoubleAryValue;
        }

        public bool IsBoolAry()
        {
            return tag_ == Tag.BoolAryValue;
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

        public string[] AsStringAry()
        {
            if (IsStringAry())
                return (string[])value_;
            return new string[0];
        }


        public int[] AsIntAry()
        {
            if (IsIntAry())
                return (int[])value_;
            return new int[0];
        }

        public long[] AsLongAry()
        {
            if (IsLongAry())
                return (long[])value_;
            return new long[0];
        }

        public float[] AsFloatAry()
        {
            if (IsFloatAry())
                return (float[])value_;
            return new float[0];
        }

        public double[] AsDoubleAry()
        {
            if (IsDoubleAry())
                return (double[])value_;
            return new double[0];
        }

        public bool[] AsBoolAry()
        {
            if (IsBoolAry())
                return (bool[])value_;
            return new bool[0];
        }

        public object AsObject()
        {
            return value_;
        }
    }//class
}//namespace
