using System;
using CommandLine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public interface ICommand
    {
        Message Respond(Message message, Parser parser, Type[] commands);
    }
}