using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var demoServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
            var logServerUrl = ConfigurationManager.AppSettings["LogServerUrl"];

            LogServerHelper logger = new LogServerHelper(logServerUrl);
            ApiServerHelper server = new ApiServerHelper(demoServerUrl);

            try
            {
                int input = 0;
                input = Connect(server);

                do
                {
                    if (input != 0)
                    {
                        ExecutionResult output = null;
                        Console.WriteLine();
                        Console.WriteLine("Please select from the following actions");
                        Console.WriteLine();

                        switch (input)
                        {
                            case (int)CommandType.Upload:
                                Console.WriteLine("Enter the filepath(for Code) or zipfilePath(for Projects) to upload for compilation/to run test package");
                                output = server.Upload(Console.ReadLine());
                                break;
                            case (int)CommandType.Compile:
                                output = server.Compile();
                                break;
                            case (int)CommandType.RunTests:
                                output = server.RunTests();
                                break;
                            case (int)CommandType.Disconnect:
                                output = server.Disconnect();
                                break;
                        }

                        Console.WriteLine(output.Value ? "Success" : "Failure");
                        if (!string.IsNullOrEmpty(output.ErrorsOrMessages)) Console.WriteLine(output.ErrorsOrMessages);
                        Console.WriteLine();
                        Console.WriteLine("Enter the command option to execute or Press 0 to exit");
                        var id = Console.ReadLine();
                        input = Convert.ToInt32(id);
                    }
                }
                while (input != 0);
            }
            catch (Exception ex)
            {
                logger.Post(new LogItem() { Id = Guid.NewGuid(), LogType = "Error", Source = "ClientApp", LogText = ex.StackTrace });
            }
            Console.ReadLine();
        }

        private static int Connect(ApiServerHelper server)
        {
            int input;
            //get all Commands from Server
            List<Command> data = server.Connect().ToList();
            List<string> inputValues = new List<string>();

            foreach (Command c in data)
            {
                if (c.Id == (int)CommandType.Connect) continue;

                Console.WriteLine(string.Format("Option {0}", c.Id));
                Console.WriteLine(string.Format("Command: {0}", c.Cmd));
                Console.WriteLine(string.Format("Command Desc: {0}", c.Description));
                Console.WriteLine();
            }
            Console.WriteLine("Enter the Option Number to Execute or Press 0 to Exit");
            var id = Console.ReadLine();
            input = Convert.ToInt32(id);
            return input;
        }
    }
}
