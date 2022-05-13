using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
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
            get => this.dictionary[i];
            set => this.dictionary[i] = value;
        }
    }
}
