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
        public static Parser Parser { get; set; } = new Parser(settings =>
        {
            settings.CaseSensitive = false;
            settings.EnableDashDash = false;
            settings.IgnoreUnknownArguments = false;
            settings.ParsingCulture = CultureInfo.InvariantCulture;
        });

        public static Message Run(this Message message)
        {
            if (!message.From.IsAdmin() && Config.Config.Current.IgnoreList.Any(m => m.Equals(message.From.Id))) return null;

            if (message.Text.TrimmedLower().Equals("help") && message.From.IsAdmin())
                return message.CreateReplyMessage(HelpGenerator().ToString());

            var parse = Parser.ParseArguments(CommandLineToArgs(message.Text), OptionContainer.Options.ToArray());
            var parseObject = (parse as Parsed<object>)?.Value as ICommand;

            var response = parseObject?.Respond(message, Parser, OptionContainer.Options.ToArray());
            if (response != null)
                return response;

            var messageResponse = Config.Config.Current.Responses.FirstOrDefault(s => s.Key.Match(message.Text.TrimmedLower())).Value;

            return messageResponse != null ? message.CreateReplyMessage(messageResponse) : null;
        }

        private static StringWriter HelpGenerator()
        {
            var helpWriter = new StringWriter();

            foreach (var option in OptionContainer.Options)
            {
                var verb = option.GetCustomAttribute<VerbAttribute>();
                if (verb == null)
                    continue;

                helpWriter.WriteLine($"`{verb.Name}` : {verb.HelpText}");
                helpWriter.WriteLine();
                var properties = option.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var property in properties)
                {
                    var opt = property.GetCustomAttribute<OptionAttribute>();
                    if (opt == null)
                        continue;

                    helpWriter.WriteLine($"`-{opt.ShortName}` : {opt.HelpText}");
                    helpWriter.WriteLine();
                }
            }
            return helpWriter;
        }

        public static bool Match(this string value, string to)
        {
            return value.Split(' ').All(m => to.Split(' ').Any(s => s.Contains(m)));
        }

        public static bool IsAdmin(this ChannelAccount account)
        {
            return Config.Config.Current.AdminList.Any(s => s.Equals(account.Id));
        }

        public static string TrimmedLower(this string str)
        {
            return str.Trim().ToLower();
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