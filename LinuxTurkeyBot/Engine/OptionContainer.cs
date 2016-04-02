using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinuxTurkeyBot.Engine
{
    public class OptionContainer
    {
        public static void Initialize()
        {
            Options = new HashSet<Type>();
            var type = typeof(ICommand);
            var optionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var optionType in optionTypes)
            {
                Options.Add(optionType);
            }
        }

        public static HashSet<Type> Options { get; set; }

        public static void Register(Type type)
        {
            Options.Add(type);
        }
    }
}