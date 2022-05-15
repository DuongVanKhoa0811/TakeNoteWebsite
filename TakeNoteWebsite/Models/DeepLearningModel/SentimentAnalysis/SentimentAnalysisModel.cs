using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.ML.DataOperationsCatalog;

namespace TakeNoteWebsite.Models.DeepLearningModel.SentimentAnalysis
{
    public class SentimentAnalysisModel : MyModel
    {
        MLContext mlContext;
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Models", "DeepLearningModel",
            "SentimentAnalysis", "yelp_labelled.txt");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Models", "DeepLearningModel",
            "SentimentAnalysis", "model.zip");
        public SentimentAnalysisModel(MLContext mlContext)
        {
            this.mlContext = mlContext;
        }

        public override VariableDictionary Predict(VariableDictionary input)
        {
            MLContext mlContext = new MLContext();

            TrainTestData splitDataView = LoadData(mlContext);

            ITransformer model = UseModelHadBeenTrained(mlContext);

            SentimentPrediction resultPrediction = UseModelWithSingleItem(mlContext, model, (string)input["Sentence"]);

            VariableDictionary result = new VariableDictionary();
            result["IsPositive"] = resultPrediction;

            return result;
        }

        public static TrainTestData LoadData(MLContext mlContext)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<SentimentData>(_dataPath, hasHeader: false);
            //IDataView dataView = mlContext.Data.LoadFromEnumerable(new List<SentimentData>());


            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            return splitDataView;
        }
        public static ITransformer UseModelHadBeenTrained(MLContext mlContext)
        {
            DataViewSchema modelSchema;

            ITransformer trainedModel = mlContext.Model.Load(_modelPath, out modelSchema);
            return trainedModel;
        }
        private static SentimentPrediction UseModelWithSingleItem(MLContext mlContext, ITransformer model, string title)
        {
            PredictionEngine<SentimentData, SentimentPrediction> predictionFunction = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            if(title == null || title == "")
            {
                var tmp = new SentimentPrediction();
                tmp.Probability = 0;
                return tmp;
            }

            SentimentData sampleStatement = new SentimentData
            {
                SentimentText = title
            };

            var resultPrediction = predictionFunction.Predict(sampleStatement);

            //Console.WriteLine($"Sentiment: {resultPrediction.SentimentText} | Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Positive" : "Negative")} | Probability: {resultPrediction.Probability} ");

            return resultPrediction;
        }

    }
}
