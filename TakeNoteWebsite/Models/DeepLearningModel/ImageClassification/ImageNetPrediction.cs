using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeNoteWebsite.Models.DeepLearningModel.ImageClassification.ModelScorer;

namespace TakeNoteWebsite.Models.DeepLearningModel.ImageClassification
{
    public class ImageNetPrediction
    {
        [ColumnName(TFModelScorer.InceptionSettings.outputTensorName)]
        public float[] PredictedLabels;
    }
}
