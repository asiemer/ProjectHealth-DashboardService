using System.Collections.Generic;
using Dashboard.Models.RAGWidgetModels;
using Dashboard.ReadModel.Views;

namespace Dashboard.Extentions
{
    public static class ExtendsRAGWidgetInfo
    {
        public static RAGWidgetInfo ToRagWidgetInfo(this RAGWidgetView x)
        {
            return new RAGWidgetInfo
            {
                item = new List<TextValue>
                {
                    new TextValue {text = "Red", value = x.Red},
                    new TextValue {text = "Yellow", value = x.Yellow},
                    new TextValue {text = "Green", value = x.Green},
                }
            };
        }
    }
}