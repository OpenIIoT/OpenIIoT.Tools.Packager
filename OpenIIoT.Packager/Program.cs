using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    internal class Options
    {
    }

    internal class Program
    {
        [DisplayName("generate-manifest")]
        private static string GenerateManifest { get; set; }

        [DisplayName("five")]
        private static int Five { get; set; }

        private static void Main(string[] args)
        {
            ParseArgs(Environment.CommandLine);

            Console.WriteLine("GenerateManifest: " + GenerateManifest);
            Console.WriteLine("Five: " + Five);

            Console.ReadLine();
        }

        private static void ParseArgs(string args)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo property in typeof(Program).GetProperties(BindingFlags.NonPublic | BindingFlags.Static))
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

            foreach (Match match in Regex.Matches(String.Join(" ", args), regEx))
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