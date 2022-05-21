using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeNoteWebsite.Models.DeepLearningModel.SentimentAnalysis;

namespace TakeNoteWebsite.Models.DeepLearningModel
{
    public class DeepLearningModel
    {
        ModelStorage modelStorage = new ModelStorage();
        public static bool PositiveNegative(string title)
        {
            if (title == null || title == "")
                return true;
            MyModel myModel = ModelStorage.GetModel("Sentiment analysis model");
            if (myModel == null)
                return false;
            VariableDictionary input = new VariableDictionary();
            input["Sentence"] = title;
            SentimentPrediction result = (SentimentPrediction)myModel.Predict(input)["IsPositive"];
            if (result == null || result.Probability == 0)
                return false;
            return Convert.ToBoolean(result.Prediction);
        }
        public static float SimilarFeature(string imagePath1, string imagePath2)
        {
            MyModel myModel = ModelStorage.GetModel("Image classifcation model");
            if (myModel == null)
                return 0;
            VariableDictionary input = new VariableDictionary();
            input["Image path 1"] = imagePath1;
            input["Image path 2"] = imagePath2;
            var tmp = myModel.Predict(input)["Similar between two images"];
            float result = float.MaxValue;
            if (tmp.ToString() != "")
                result = (float)tmp;
            return result;
        }
    }
}
