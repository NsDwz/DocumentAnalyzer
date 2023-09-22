using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBasedServises
{
    public class OcrProcessed
    {
        [JsonProperty]
        private string text;
        
        [JsonProperty]
        private double angle;
        
        [JsonProperty]
        private double fontSize;

        [JsonProperty]
        private bool hasHandWrittings;

        [JsonProperty] 
        private string handWrittings;

        [JsonProperty] 
        private string fontType;

        public OcrProcessed(
            string text, 
            double angle, 
            double fontSize, 
            bool hasHandWrittings, 
            string handWrittings,
            string fontType
            )
        {
            this.text = text;
            this.angle = angle;
            this.fontSize = fontSize;
            this.hasHandWrittings = hasHandWrittings;
            this.handWrittings = handWrittings;
            this.fontType = fontType;
        }

    }
}
