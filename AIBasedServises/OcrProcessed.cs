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

        public OcrProcessed(string text, double angle, double fontSize)
        {
            this.text = text;
            this.angle = angle;
            this.fontSize = fontSize;
        }

    }
}
