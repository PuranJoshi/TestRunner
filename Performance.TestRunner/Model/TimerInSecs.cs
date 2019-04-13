using System;
using System.Collections.Generic;
using System.Text;

namespace Performance.TestRunner.Model
{
    public class TimerInSecs
    {
        public readonly long TestDuration;
        public readonly long LoadChangeInterval;

        public TimerInSecs(Dictionary<string,string> keyValuePairs)
        {
            long.TryParse(keyValuePairs["test_duration"], out TestDuration);
            long.TryParse(keyValuePairs["load_change_interval"], out LoadChangeInterval);
        }

        public TimerInSecs(long testDuration, long loadChangeInterval)
        {
            TestDuration = testDuration;
            LoadChangeInterval = loadChangeInterval;
        }
    }
}
