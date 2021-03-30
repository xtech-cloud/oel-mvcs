using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class Config
    {
        protected Dictionary<string, Any> fields_ = new Dictionary<string, Any>();

        public virtual void Merge(string _content)
        {
            throw new System.NotImplementedException();
        }

        public bool Has(string _field)
        {
            return fields_.ContainsKey(_field);
        }

        public Any this[string _aKey]
        {
            get
            {
                Any field;
                if (fields_.TryGetValue(_aKey, out field))
                    return field;
                return new Any();
            }
        }
    }
}//namespace
