using System.Collections.Generic;

namespace XTC.oelMVCS
{
    public class Config
    {
        private Dictionary<string, Any> fields = new Dictionary<string, Any>();

        public void Merge(string _content)
        {
            throw new System.NotImplementedException();
        }

        public bool Has(string _field)
        {
            return fields.ContainsKey(_field);
        }

        public Any this [string _aKey]
        {
            get
            {
                Any field;
                if (fields.TryGetValue(_aKey, out field))
                    return field;
                return new Any();
            }
        }
    }
}//namespace
