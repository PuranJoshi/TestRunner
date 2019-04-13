using System;
using System.Collections.Generic;
using System.Text;

namespace Performance.TestRunner.Model
{
    public class Schedule
    {
        public readonly DateTime StartTime;

        public Schedule(DateTime startTime)
        {
            StartTime = startTime;
        }
    }
}
