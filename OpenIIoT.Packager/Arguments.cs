using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLine
{
    public class ArgumentAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }

        public ArgumentAttribute(string name, string description, bool required = false)
        {
            Name = name;
            Description = description;
            Required = required;
        }
    }

    public class Arguments
    {
        private const string regEx = "(?:[-]{1,2}|\\/)([\\w-]+)[=|:| ]?(\\w\\S*|\\\".*\\\"|\\\'.*\\\')?";

        public static Dictionary<string, string> Parse(string arguments = "")
        {
            Dictionary<string, string> argumentDictionary = new Dictionary<string, string>();

            arguments = arguments.Equals(string.Empty) ? Environment.CommandLine : arguments;

            foreach (Match match in Regex.Matches(arguments, regEx))
            {
                if (match.Groups.Count == 3)
                {
                    if (!argumentDictionary.ContainsKey(match.Groups[1].Value))
                    {
                        argumentDictionary.Add(match.Groups[1].Value, match.Groups[2].Value);
                    }
                }
            }

            return argumentDictionary;
        }

        public static void Populate()
        {
            Type type = new StackFrame(1).GetMethod().DeclaringType;

            Dictionary<string, PropertyInfo> properties = GetArgumentProperties(type, typeof(ArgumentAttribute));
            Dictionary<string, string> argumentDictionary = Parse();

            foreach (string key in argumentDictionary.Keys)
            {
                if (properties.ContainsKey(key))
                {
                    PropertyInfo property = properties[key];

                    object convertedValue = Convert.ChangeType(argumentDictionary[key], property.PropertyType);

                    property.SetValue(null, convertedValue);
                }
            }
        }

        private static Dictionary<string, PropertyInfo> GetArgumentProperties(Type type, Type attributeType)
        {
            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Static))
            {
                CustomAttributeData attribute = property.CustomAttributes.Where(a => a.AttributeType.Name == attributeType.Name).FirstOrDefault();

                if (attribute != default(CustomAttributeData))
                {
                    string displayName = (string)attribute.ConstructorArguments[0].Value;
                    if (!properties.ContainsKey(displayName))
                    {
                        properties.Add(displayName, property);
                    }
                }
            }

            return properties;
        }
    }
}