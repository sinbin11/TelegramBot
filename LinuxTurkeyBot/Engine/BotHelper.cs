using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using CommandLine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public static class BotHelper
    {
        public static Message Run(this Message message)
        {
            if(OptionContainer.Options.Count <= 0)
                return null;

            var parser = new Parser(settings =>
            {
                settings.CaseSensitive = false;
                settings.EnableDashDash = false;
                settings.IgnoreUnknownArguments = true;
                settings.ParsingCulture = CultureInfo.InvariantCulture;
            });

            if (message.Text.Trim().ToLower().Equals("help"))
            {
                var helpWriter = new StringWriter();

                foreach (var option in OptionContainer.Options)
                {
                    var verb = option.GetCustomAttribute<VerbAttribute>();
                    if(verb == null)
                        continue;

                    helpWriter.WriteLine($"`{verb.Name}` : {verb.HelpText}");
                    helpWriter.WriteLine();
                    var properties = option.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (var property in properties)
                    {
                        var opt = property.GetCustomAttribute<OptionAttribute>();
                        if(opt == null)
                            continue;

                        helpWriter.WriteLine($"`-{opt.ShortName}` : {opt.HelpText}");
                        helpWriter.WriteLine();
                    }
                }

                return message.CreateReplyMessage(helpWriter.ToString());
            }

            var parse = parser.ParseArguments(CommandLineToArgs(message.Text), OptionContainer.Options.ToArray());

            if (parse.Tag == ParserResultType.NotParsed)
            {
                return Config.Config.Current.Responses.Where(s => s.Key.Split(' ').All(m => message.Text.Split(' ').Any(k => k.Contains(m))))
                    .Select(s => message.CreateReplyMessage(s.Value)).FirstOrDefault();
            }

            return ((parse as Parsed<object>)?.Value as ICommand)?.Respond(message, parser, OptionContainer.Options.ToArray());

        }

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] CommandLineToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }
    }
}