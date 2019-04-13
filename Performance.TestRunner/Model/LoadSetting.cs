using System;
using System.Collections.Generic;
using System.Text;

namespace Performance.TestRunner.Model
{
    public class LoadSetting
    {
        public readonly decimal Start;
        public readonly decimal End;
        public readonly decimal StepSize;

        public LoadSetting(Dictionary<string,string> keyValuePairs)
        {
            decimal.TryParse(keyValuePairs["min_load"], out Start);
            decimal.TryParse(keyValuePairs["max_load"], out End);
            decimal.TryParse(keyValuePairs["load_step"], out StepSize);
        }

        public LoadSetting(decimal start, decimal end, decimal stepSize)
        {
            Start = start;
            End = end;
            StepSize = Math.Abs(stepSize) * (start > end ? -1 : 1);
        }
    }
}
