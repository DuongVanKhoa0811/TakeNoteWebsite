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
        public static int SimilarFeature(string imagePath1, string imagePath2)
        {
            return 0;
        }
    }
}
