
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

        public Any(string _value)
        {
            AsString = _value;
        }

        public Any(float _value)
        {
            AsFloat = _value;
        }

        public Any(double _value)
        {
            AsDouble = _value;
        }

        public Any(bool _value)
        {
            AsBool = _value;
        }

        public Any(int _value)
        {
            AsInt = _value;
        }

        public Any(long _value)
        {
            AsLong = _value;
        }

        public bool IsNull
        {
            get
            {
                return tag_ == Tag.NULL;
            }
        }

        public bool IsString
        {
            get
            {
                return tag_ == Tag.StringValue;
            }
        }

        public bool IsInt
        {
            get
            {
                return tag_ == Tag.IntValue;
            }
        }

        public bool IsLong
        {
            get
            {
                return tag_ == Tag.LongValue;
            }
        }

        public bool IsFloat
        {
            get
            {
                return tag_ == Tag.FloatValue;
            }
        }

        public bool IsDouble
        {
            get
            {
                return tag_ == Tag.DoubleValue;
            }
        }

        public bool IsBool
        {
            get
            {
                return tag_ == Tag.BoolValue;
            }
        }

        public string AsString
        {
            get
            {
                return value_;
            }
            private set
            {
                value_ = value;
                tag_ = Tag.StringValue;
            }
        }


        public int AsInt
        {
            get
            {
                int v = 0;
                if (int.TryParse(value_, out v))
                    return v;
                return 0;
            }
            private set
            {
                value_ = value.ToString();
                tag_ = Tag.IntValue;
            }
        }

        public long AsLong
        {
            get
            {
                long v = 0;
                if (long.TryParse(value_, out v))
                    return v;
                return 0;
            }
            private set
            {
                value_ = value.ToString();
                tag_ = Tag.LongValue;
            }
        }

        public float AsFloat
        {
            get
            {
                float v = 0.0f;
                if (float.TryParse(value_, out v))
                    return v;
                return 0.0f;
            }
            private set
            {
                value_ = value.ToString();
                tag_ = Tag.FloatValue;
            }
        }

        public double AsDouble
        {
            get
            {
                double v = 0.0;
                if (double.TryParse(value_, out v))
                    return v;
                return 0.0;
            }
            private set
            {
                value_ = value.ToString();
                tag_ = Tag.DoubleValue;

            }
        }

        public bool AsBool
        {
            get
            {
                bool v = false;
                if (bool.TryParse(value_, out v))
                    return v;
                return !string.IsNullOrEmpty(value_);
            }
            private set
            {
                value_ = (value) ? "true" : "false";
                tag_ = Tag.BoolValue;
            }
        }
    }//class
}//namespace
