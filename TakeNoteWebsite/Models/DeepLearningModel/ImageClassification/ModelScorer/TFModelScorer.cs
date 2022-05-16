using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.DeepLearningModel.ImageClassification.ModelScorer
{
    public class TFModelScorer
    {
        private readonly string dataLocation;
        private readonly string imagesFolder;
        private readonly string modelLocation;
        private readonly string labelsLocation;
        private readonly MLContext mlContext;
        private static string ImageReal = nameof(ImageReal);

        public TFModelScorer(string dataLocation, string imagesFolder, string modelLocation, string labelsLocation)
        {
            this.dataLocation = dataLocation;
            this.imagesFolder = imagesFolder;
            this.modelLocation = modelLocation;
            this.labelsLocation = labelsLocation;
            mlContext = new MLContext();
        }

        public struct ImageNetSettings
        {
            public const int imageHeight = 224;
            public const int imageWidth = 224;
            public const float mean = 117;
            public const bool channelsLast = true;
        }

        public struct InceptionSettings
        {
            public const string inputTensorName = "input";
            public const string outputTensorName = "softmax2";
        }

        public float Score(string imagePath1, string imagePath2)
        {
            var model = LoadModel(dataLocation, imagesFolder, modelLocation);
            //imagesFolder,
            var predictions = PredictDataUsingModel(dataLocation, imagePath1, imagePath2, labelsLocation, model).ToArray();
            var listPredictions = predictions.ToList();
            if (listPredictions.Count() != 2)
                return 0;
            else
                return DifferentBetweenTwoFloatArrayL1(listPredictions[0].PredictedArray, listPredictions[1].PredictedArray);
        }

        private PredictionEngine<ImageNetData, ImageNetPrediction> LoadModel(string dataLocation, string imagesFolder, string modelLocation)
        {
            Console.WriteLine("Read model");
            Console.WriteLine($"Model location: {modelLocation}");
            Console.WriteLine($"Images folder: {imagesFolder}");
            Console.WriteLine($"Training file: {dataLocation}");
            Console.WriteLine($"Default parameters: image size=({ImageNetSettings.imageWidth},{ImageNetSettings.imageHeight}), image mean: {ImageNetSettings.mean}");

            var data = mlContext.Data.LoadFromEnumerable(new List<ImageNetData>());
            //var data = mlContext.Data.LoadFromTextFile<ImageNetData>(dataLocation, hasHeader: true);

            var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: imagesFolder, inputColumnName: nameof(ImageNetData.ImagePath))
                            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: ImageNetSettings.imageWidth, imageHeight: ImageNetSettings.imageHeight, inputColumnName: "input"))
                            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: ImageNetSettings.channelsLast, offsetImage: ImageNetSettings.mean))
                            .Append(mlContext.Model.LoadTensorFlowModel(modelLocation).
                            ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2" },
                                                inputColumnNames: new[] { "input" }, addBatchDimensionInput: true));

            ITransformer model = pipeline.Fit(data);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<ImageNetData, ImageNetPrediction>(model);

            return predictionEngine;
        }

        protected IEnumerable<ImageNetDataProbability> PredictDataUsingModel(string testLocation,
                                                                  //string imagesFolder,
                                                                  string imagePath1,
                                                                  string imagePath2,
                                                                  string labelsLocation,
                                                                  PredictionEngine<ImageNetData, ImageNetPrediction> model)
        {
            Console.WriteLine("Classify images");
            Console.WriteLine($"Images folder: {imagesFolder}");
            Console.WriteLine($"Training file: {testLocation}");
            Console.WriteLine($"Labels file: {labelsLocation}");

            var labels = ModelHelpers.ReadLabels(labelsLocation);

            //var testData = ImageNetData.ReadFromCsv(testLocation, imagesFolder);

            IEnumerable<ImageNetData> tmp = Enumerable.Empty<ImageNetData>();
            tmp = tmp.Append(ImageNetData.ReadSingleImage(imagePath1));
            tmp = tmp.Append(ImageNetData.ReadSingleImage(imagePath2));


            var i = 0;
            var a = new float[1000];

            foreach (var sample in tmp)
            {
                var probs = model.Predict(sample).PredictedLabels;
                var imageData = new ImageNetDataProbability()
                {
                    ImagePath = sample.ImagePath,
                    Label = sample.Label
                };
                (imageData.PredictedLabel, imageData.Probability) = ModelHelpers.GetBestLabel(labels, probs);
                imageData.PredictedArray = probs;
                imageData.ConsoleWrite();
                if (i != 0)
                    Console.WriteLine(DifferentBetweenTwoFloatArrayL1(a, probs));
                a = probs;
                i += 1;
                yield return imageData;
            }
        }

        float DifferentBetweenTwoFloatArrayL1(float[] p, float[] q)
        {
            var lenP = p.Length;
            double result = 0;
            for (int i = 0; i < lenP; i++)
            {
                result += p[i]*Math.Log(q[i]);
            }
            return (float)(-result);
        }
    }
}
