using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.DeepLearningModel.ImageClassification
{
    public class ImageClassifyModel : MyModel
    {
        MLContext mlContext;
        public ImageClassifyModel(MLContext mlContext)
        {
            this.mlContext = mlContext;
        }
        public override VariableDictionary Predict(VariableDictionary input)
        {
            return input;
        }
    }
}
