using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeNoteWebsite.Models.DeepLearningModel.ImageClassification;
using TakeNoteWebsite.Models.DeepLearningModel.SentimentAnalysis;

namespace TakeNoteWebsite.Models.DeepLearningModel
{
    public class ModelStorage
    {
        static MLContext mlContext = new MLContext();
        static Dictionary<string, MyModel> modelDict = new Dictionary<string, MyModel>();
        static ModelStorage()
        {
            modelDict.Add("Image classifcation model", new ImageClassifyModel(mlContext));
            modelDict.Add("Sentiment analysis model", new SentimentAnalysisModel(mlContext));
        }
        public static MyModel GetModel(string name)
        {
            return modelDict.ContainsKey(name) ? modelDict[name] : null;
        }
    }
}
