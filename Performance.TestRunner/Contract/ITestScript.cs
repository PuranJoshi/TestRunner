using System;
using System.Collections.Generic;
using System.Text;

namespace Performance.TestRunner.Contract
{
    public interface ITestScript
    {
        void Intialize(Dictionary<string, string> preRequisites);
        Dictionary<string, string> StartAsync();
    }
}
