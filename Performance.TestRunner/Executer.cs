using Performance.TestRunner.Contract;
using Performance.TestRunner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Performance.TestRunner
{
    public class Executer
    {
        private ITestScript _testScript;
        private readonly int _testStartDelayInSecs = 10;
        private readonly TestRunnerSettings _testRunnerSettings;
        
        public Executer(Dictionary<string,string> keyValuePairs)
        {
            _testRunnerSettings = new TestRunnerSettings(keyValuePairs);
        }

        public Executer(TestRunnerSettings testRunnerSettings)
        {
            _testRunnerSettings = testRunnerSettings;
        }

        public async Task ExecuteAsync(Dictionary<string,string> testScriptArgs)
        {
            var assemblyName = testScriptArgs["assembly_name"];

            var testName = testScriptArgs["test_name"];

            _testScript = TestFactory.Instance(assemblyName).Create(testName);

            foreach (var schedule in GetRunningSchedule())
            {
                try
                {
                    var delayTask = Task.Delay(schedule.StartTime - DateTime.UtcNow);

                    _testScript.Intialize(testScriptArgs);

                    await delayTask;

                    new Timer(x => 
                    {
                        _testScript.StartAsync();

                    }, null, 0,0);
                }
                catch { }
            }
        }

        private List<Schedule> GetRunningSchedule()
        {
            var testStartTime = DateTime.UtcNow.AddSeconds(_testStartDelayInSecs);

            var testRunDuration = DateTime.UtcNow.AddSeconds(_testRunnerSettings.TimerInSecs.TestDuration + _testStartDelayInSecs);

            var triggerIntervalInMs
                = (double)(1000 / _testRunnerSettings.LoadSetting.Start);

            var maxStepSize = _testRunnerSettings.LoadSetting.End;

            var nextStep 
                = _testRunnerSettings.LoadSetting.Start + _testRunnerSettings.LoadSetting.StepSize;

            var triggerChangeIntervalInSecs 
                = _testRunnerSettings.TimerInSecs.LoadChangeInterval + _testStartDelayInSecs;

            var schedules = new List<Schedule>();

            DateTime nextTriggerIntervalChangeTime = DateTime.UtcNow.AddSeconds(triggerChangeIntervalInSecs);

            for (var timer = testStartTime; timer <= testRunDuration; )
            {
                if (nextTriggerIntervalChangeTime <= timer)
                {
                    var newTriggerIntervalInMs = (double)(1000 / nextStep);

                    triggerIntervalInMs = newTriggerIntervalInMs;

                    nextTriggerIntervalChangeTime = timer.AddSeconds(triggerChangeIntervalInSecs);

                    nextStep += _testRunnerSettings.LoadSetting.StepSize;

                    nextStep = nextStep >= maxStepSize ? maxStepSize : nextStep;
                }

                timer = timer.AddMilliseconds(triggerIntervalInMs);

                schedules.Add(new Schedule(timer));
            }

            return schedules;
        }
    }
}
