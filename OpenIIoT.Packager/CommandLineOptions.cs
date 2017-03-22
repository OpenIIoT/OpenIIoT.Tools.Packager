using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    public class CommandLineArguments
    {
        public string Arguments { get; private set; }

        public CommandLineArguments(string arguments)
        {
            Arguments = arguments;
        }

        public void ParseInto(Type type)
        {
            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Static))
            {
                CustomAttributeData attribute = property.CustomAttributes.Where(a => a.AttributeType.Name == "DisplayNameAttribute").FirstOrDefault();

                if (attribute != default(CustomAttributeData))
                {
                    string displayName = (string)attribute.ConstructorArguments[0].Value;
                    if (!properties.ContainsKey(displayName))
                    {
                        properties.Add(displayName, property);
                    }
                }
            }

            string regEx = "(?:[-]{1,2}|\\/)([\\w-]+)[=|:]?(\\w\\S*|\\\".*\\\"|\\\'.*\\\')?";

            foreach (Match match in Regex.Matches(Arguments, regEx))
            {
                // ensure the match contains three tuples
                if (match.Groups.Count == 3)
                {
                    if (properties.ContainsKey(match.Groups[1].Value))
                    {
                        PropertyInfo property = properties[match.Groups[1].Value];
                        object converted = Convert.ChangeType(match.Groups[2].Value, property.PropertyType);

                        property.SetValue(null, converted);
                    }
                }
            }
        }
    }
}