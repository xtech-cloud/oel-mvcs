
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

        private string value_ = "";
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
            any.value_ = _value.ToString();
            return any;
        }

        public static Any FromDouble(double _value)
        {
            Any any = new Any();
            any.tag_ = Tag.DoubleValue;
            any.value_ = _value.ToString();
            return any;
        }

        public static Any FromBool(bool _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolValue;
            any.value_ = _value.ToString();
            return any;
        }

        public static Any FromInt(int _value)
        {
            Any any = new Any();
            any.tag_ = Tag.IntValue;
            any.value_ = _value.ToString();
            return any;
        }

        public static Any FromLong(long _value)
        {
            Any any = new Any();
            any.tag_ = Tag.LongValue;
            any.value_ = _value.ToString();
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
            return value_;
        }


        public int AsInt()
        {
            int v = 0;
            if (int.TryParse(value_, out v))
                return v;
            return 0;
        }

        public long AsLong()
        {
            long v = 0;
            if (long.TryParse(value_, out v))
                return v;
            return 0;
        }

        public float AsFloat()
        {
            float v = 0.0f;
            if (float.TryParse(value_, out v))
                return v;
            return 0.0f;
        }

        public double AsDouble()
        {
            double v = 0.0;
            if (double.TryParse(value_, out v))
                return v;
            return 0.0;
        }

        public bool AsBool()
        {
            bool v = false;
            if (bool.TryParse(value_, out v))
                return v;
            return !string.IsNullOrEmpty(value_);
        }
    }//class
}//namespace
