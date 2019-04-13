using System;
using System.Collections.Generic;
using System.Text;

namespace Performance.TestRunner.Model
{
    public class TestRunnerSettings
    {
        public TimerInSecs TimerInSecs { get; }
        public LoadSetting LoadSetting { get; }

        public TestRunnerSettings(Dictionary<string,string> keyValuePairs)
        {
            TimerInSecs = new TimerInSecs(keyValuePairs);
            LoadSetting = new LoadSetting(keyValuePairs);
        }

        public TestRunnerSettings(TimerInSecs timerInSecs, LoadSetting loadSetting)
        {
            TimerInSecs = timerInSecs;
            LoadSetting = loadSetting;
        }
    }
}
