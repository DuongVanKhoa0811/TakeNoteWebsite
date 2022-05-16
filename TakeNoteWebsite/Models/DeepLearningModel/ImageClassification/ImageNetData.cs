using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.DeepLearningModel.ImageClassification
{
    public class ImageNetData
    {
        [LoadColumn(0)]
        public string ImagePath;

        [LoadColumn(1)]
        public string Label;

        public static IEnumerable<ImageNetData> ReadFromCsv(string file, string folder)
        {
            return File.ReadAllLines(file)
             .Select(x => x.Split('\t'))
             .Select(x => new ImageNetData { ImagePath = Path.Combine(folder, x[0]), Label = x[1] });
        }
        public static IEnumerable<ImageNetData> ReadFromFile(string imageFolder)
        {
            return Directory
                .GetFiles(imageFolder)
                .Where(filePath => Path.GetExtension(filePath) != ".tsv" & Path.GetExtension(filePath) != ".md")
                .Select(filePath => new ImageNetData { ImagePath = filePath, Label = Path.GetFileName(filePath) });
        }
        public static ImageNetData ReadSingleImage(string imagePath)
        {
            return new ImageNetData{ ImagePath = imagePath, Label = Path.GetFileName(imagePath)};
        }
    }

    public class ImageNetDataProbability : ImageNetData
    {
        public string PredictedLabel;
        public float Probability { get; set; }
        public float[] PredictedArray;
    }
}
