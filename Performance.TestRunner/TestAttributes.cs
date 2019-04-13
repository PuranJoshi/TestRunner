using System;

namespace Performance.TestRunner
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute : Attribute
    {
        public TestAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public String Name { get; private set; }

        public string Description { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TestFieldAttribute : Attribute
    {
        public TestFieldAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.IsMandatory = true;
            this.DefaultValue = null;

        }

        public TestFieldAttribute(string name, string description, string defaultValue)
        {
            this.Name = name;
            this.Description = description;
            this.IsMandatory = false;
            this.DefaultValue = defaultValue;
        }


        public String Name { get; private set; }

        public string Description { get; private set; }

        public bool IsMandatory { get; private set; }

        public string DefaultValue { get; private set; }
    }
}
