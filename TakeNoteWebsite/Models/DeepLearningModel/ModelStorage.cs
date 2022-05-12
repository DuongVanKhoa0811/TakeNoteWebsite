using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.DeepLearningModel
{
    public class ModelStorage
    {
        static Dictionary<string, Model> modelDict = new Dictionary<string, Model>();
        static ModelStorage()
        {
            /*Add all models to dictionary*/
        }
        static Model GetModel(string name)
        {
            return modelDict.ContainsKey(name) ? modelDict[name] : null;
        }
    }
}
