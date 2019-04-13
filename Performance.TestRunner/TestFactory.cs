using Performance.TestRunner.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Performance.TestRunner
{
    public class TestFactory
    {

        private static TestFactory _instance = null;
        
        public static TestFactory Instance(string AssemblyName)
        { 
            if (_instance == null)
                _instance = BuildInstance(AssemblyName);
            return _instance;
        }

        private static TestFactory BuildInstance(string _assemblyName)
        {
            var factory = new TestFactory();

            var assmebly = Assembly.Load(new AssemblyName(_assemblyName));
                    assmebly.DefinedTypes
                    .Where(t => t.IsPublic == true && t.IsAbstract == false)
                    .Where(t => typeof(ITestScript)
                    .IsAssignableFrom(t.BaseType))
                    .Select(t => new { Type = t, Attribute = t.GetCustomAttribute<TestAttribute>(true) })
                    .Where(x => x.Attribute != null)
                    .ToList()
                    .ForEach(x =>
                    {
                        factory.Register(x.Attribute.Name, x.Type.AsType());
                    });
            return factory;
        }

        private readonly Dictionary<string, Type> _mapping = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public TestFactory Register<TImpl>(string name)
            where TImpl : ITestScript
        {
            return Register(name, typeof(TImpl));
        }

        public TestFactory Register(string name, Type type)
        {
            _mapping[name] = type;
            return this;
        }

        public ITestScript Create(string name)
        {
            if (_mapping.TryGetValue(name, out var type) == false)
                throw new Exception(string.Format("No test found with the name {0}", name));
            return Activator.CreateInstance(type) as ITestScript;
        }

        public Type GetTypeMapping(string name)
        {
            if (_mapping.TryGetValue(name, out var type))
                return type;
            return null;
        }

        public IEnumerable<Type> GetAllTestTypes()
        {
            return _mapping.Values.ToArray();
        }
    }
}
