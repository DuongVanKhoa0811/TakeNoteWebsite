using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TakeNoteWebsite.Models.DeepLearningModel.ImageClassification.ModelScorer;

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
            string assetsPath = Path.Combine(Environment.CurrentDirectory, "Models", "DeepLearningModel"
                , "ImageClassification");

            var tagsTsv = Path.Combine(assetsPath, "Input", "Images", "tags.tsv");
            var imagesFolder = Path.Combine(assetsPath, "Input", "Images");
            var inceptionPb = Path.Combine(assetsPath, "Input", "Inception", "tensorflow_inception_graph.pb");
            var labelsTxt = Path.Combine(assetsPath, "Input", "Inception", "imagenet_comp_graph_label_strings.txt");

            try
            {
                var modelScorer = new TFModelScorer(tagsTsv, imagesFolder, inceptionPb, labelsTxt);
                
                VariableDictionary result = new VariableDictionary();
                var score = modelScorer.Score((string)input["Image path 1"]
                    , (string)input["Image path 2"]);
                if (score == 0)
                    result["Similar between two images"] = float.MaxValue;
                else
                    result["Similar between two images"] = score;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return input;
        }
    }
}
