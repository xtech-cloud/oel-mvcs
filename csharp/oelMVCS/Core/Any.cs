using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class Any
    {
        public enum Tag
        {
            NULL = 0,
            StringValue = 1,
            Int32Value = 2,
            Int64Value = 3,
            Float32Value = 4,
            Float64Value = 5,
            BoolValue = 6,
            StringAryValue = 11,
            Int32AryValue = 12,
            Int64AryValue = 13,
            Float32AryValue = 14,
            Float64AryValue = 15,
            BoolAryValue = 16,
            StringMapValue = 21,
            Int32MapValue = 22,
            Int64MapValue = 23,
            Float32MapValue = 24,
            Float64MapValue = 25,
            BoolMapValue = 26,
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

        public static Any FromFloat32(float _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float32Value;
            any.value_ = _value;
            return any;
        }

        public static Any FromFloat64(double _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float64Value;
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

        public static Any FromInt32(int _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int32Value;
            any.value_ = _value;
            return any;
        }

        public static Any FromInt64(long _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int64Value;
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

        public static Any FromFloat32Ary(float[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float32AryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromFloat64Ary(double[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float64AryValue;
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

        public static Any FromInt32Ary(int[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int32AryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromInt64Ary(long[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int64AryValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromStringMap(Dictionary<string, string> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.StringMapValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromFloat32Map(Dictionary<string, float> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float32MapValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromFloat64Map(Dictionary<string, double> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float64MapValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromBoolMap(Dictionary<string, bool> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolMapValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromInt32Map(Dictionary<string, int> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int32MapValue;
            any.value_ = _value;
            return any;
        }

        public static Any FromInt64Map(Dictionary<string, long> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int64MapValue;
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
            return tag_ == Tag.Int32Value;
        }

        public bool IsLong()
        {
            return tag_ == Tag.Int64Value;
        }

        public bool IsFloat()
        {
            return tag_ == Tag.Float32Value;
        }

        public bool IsDouble()
        {
            return tag_ == Tag.Float64Value;
        }

        public bool IsBool()
        {
            return tag_ == Tag.BoolValue;
        }
        public bool IsStringAry()
        {
            return tag_ == Tag.StringAryValue;
        }

        public bool IsInt32Ary()
        {
            return tag_ == Tag.Int32AryValue;
        }

        public bool IsInt64Ary()
        {
            return tag_ == Tag.Int64AryValue;
        }

        public bool IsFloat32Ary()
        {
            return tag_ == Tag.Float32AryValue;
        }

        public bool IsFloat64Ary()
        {
            return tag_ == Tag.Float64AryValue;
        }

        public bool IsBoolAry()
        {
            return tag_ == Tag.BoolAryValue;
        }

        public bool IsStringMap()
        {
            return tag_ == Tag.StringMapValue;
        }

        public bool IsInt32Map()
        {
            return tag_ == Tag.Int32MapValue;
        }

        public bool IsInt64Map()
        {
            return tag_ == Tag.Int64MapValue;
        }

        public bool IsFloat32Map()
        {
            return tag_ == Tag.Float32MapValue;
        }

        public bool IsFloat64Map()
        {
            return tag_ == Tag.Float64MapValue;
        }

        public bool IsBoolMap()
        {
            return tag_ == Tag.BoolMapValue;
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


        public int[] AsInt32Ary()
        {
            if (IsInt32Ary())
                return (int[])value_;
            return new int[0];
        }

        public long[] AsInt64Ary()
        {
            if (IsInt64Ary())
                return (long[])value_;
            return new long[0];
        }

        public float[] AsFloat32Ary()
        {
            if (IsFloat32Ary())
                return (float[])value_;
            return new float[0];
        }

        public double[] AsFloat64Ary()
        {
            if (IsFloat64Ary())
                return (double[])value_;
            return new double[0];
        }

        public bool[] AsBoolAry()
        {
            if (IsBoolAry())
                return (bool[])value_;
            return new bool[0];
        }

        public Dictionary<string, string> AsStringMap()
        {
            if (IsStringMap())
                return (Dictionary<string, string>)value_;
            return new Dictionary<string, string>();
        }


        public Dictionary<string, int> AsInt32Map()
        {
            if (IsInt32Ary())
                return (Dictionary<string, int>)value_;
            return new Dictionary<string, int>();
        }

        public Dictionary<string, long> AsInt64Map()
        {
            if (IsInt64Ary())
                return (Dictionary<string, long>)value_;
            return new Dictionary<string, long>();
        }

        public Dictionary<string, float> AsFloat32Map()
        {
            if (IsFloat32Map())
                return (Dictionary<string, float>)value_;
            return new Dictionary<string, float>();
        }

        public Dictionary<string, double> AsFloat64Map()
        {
            if (IsFloat64Map())
                return (Dictionary<string, double>)value_;
            return new Dictionary<string, double>();
        }

        public Dictionary<string, bool> AsBoolMap()
        {
            if (IsBoolMap())
                return (Dictionary<string, bool>)value_;
            return new Dictionary<string, bool>();
        }

        public object AsObject()
        {
            return value_;
        }
    }//class
}//namespace
