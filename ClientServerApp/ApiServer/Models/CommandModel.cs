using ApiServer.Controllers;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compiler.Factory;
using TestProvider.Factory;

namespace ApiServer.Models
{
    public class CommandModel
    {
        public List<Command> GetCommands()
        {
            List<Command> commands = new List<Command>();
            commands.Add(new Command()
            {
                Id = (int)CommandType.Connect,
                Cmd = Enum.GetName(typeof(CommandType), CommandType.Connect),
                Description = "Connects client with server for further operations"
            });
            commands.Add(new Command()
            {
                Id = (int)CommandType.Upload,
                Cmd = Enum.GetName(typeof(CommandType), CommandType.Upload),
                Description = "Upload a single Source File or Entire Project in a Zip File to server for compilation"
            });
            commands.Add(new Command()
            {
                Id = (int)CommandType.Compile,
                Cmd = Enum.GetName(typeof(CommandType), CommandType.Compile),
                Description = "Compiles the most recent uploaded File or Project and return results",
            });
            commands.Add(new Command()
            {
                Id = (int)CommandType.RunTests,
                Cmd = Enum.GetName(typeof(CommandType), CommandType.RunTests),
                Description = "Compiles the most recent uploaded File or Project and return results",
            });
            commands.Add(new Command()
            {
                Id = (int)CommandType.Disconnect,
                Cmd = Enum.GetName(typeof(CommandType), CommandType.Disconnect),
                Description = "Disconnect the client from the server",
            });
            return commands;
        }

        public bool Compile(string compilerToUse, string pathorFile, out string errorsOrMessages)
        {
            errorsOrMessages = string.Empty;
            CompilerFactory.Compile(compilerToUse, pathorFile, out errorsOrMessages);

            if (String.IsNullOrEmpty(errorsOrMessages))
                return true;
            else
                return false;
        }

        public bool RunTests(string providerToUse, string testLibraryPath, out string errorsOrMessages)
        {
            errorsOrMessages = string.Empty;
            TestProviderFactory.RunTests(providerToUse, testLibraryPath, out errorsOrMessages);

            if (String.IsNullOrEmpty(errorsOrMessages))
                return true;
            else
                return false;
        }
    }
}