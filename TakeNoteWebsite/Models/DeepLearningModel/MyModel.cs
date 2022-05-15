using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.DeepLearningModel
{
    public class MyModel
    {
        public MyModel()
        {
        }
        public virtual VariableDictionary Predict(VariableDictionary input)
        {
            return input;
        }
    }
}
