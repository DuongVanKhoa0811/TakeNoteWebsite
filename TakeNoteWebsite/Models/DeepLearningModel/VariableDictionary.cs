using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.DeepLearningModel
{
    public class VariableDictionary
    {
        private Dictionary<string, object> dictionary;

        public VariableDictionary()
        {
            this.dictionary = new Dictionary<string, object>();
        }
        public object this[string i]
        {
            get
            {
                if (dictionary.ContainsKey(i))
                    return dictionary[i];
                return "";
            }
            set
            {
                if (dictionary.ContainsKey(i))
                    dictionary[i] = value;
                else
                    dictionary.Add(i, value);
            }
        }
    }
}
