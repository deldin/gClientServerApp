using NUnit.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestProvider.Factory
{
    public class TestProviderFactory
    {

        public static bool RunTests(string providerToUse, string testLibraryPath, out string errorsOrMessages)
        {
            bool result = true;
            errorsOrMessages = "";

            switch (providerToUse)
            {
                case "NUnit":
                default:
                    {

                        CoreExtensions.Host.InitializeService();
                        TestPackage testPackage = new TestPackage(testLibraryPath);
                        testPackage.BasePath = Path.GetDirectoryName(testLibraryPath);
                        TestSuiteBuilder builder = new TestSuiteBuilder();
                        TestSuite suite = builder.Build(testPackage);
                        TestResult testresult = suite.Run(new NullListener(), TestFilter.Empty);
                        
                        result = testresult.IsSuccess;

                        errorsOrMessages += ("has results? " + testresult.HasResults);
                        errorsOrMessages += ("results count: " + testresult.Results.Count);
                        errorsOrMessages += ("success? " + testresult.IsSuccess);

                        //TestPackage testPackage = new TestPackage(testLibraryPath);
                        //RemoteTestRunner remoteTestRunner = new RemoteTestRunner();
                        //remoteTestRunner.Load(testPackage);
                        //TestResult testResult = remoteTestRunner.Run(new NullListener(), null, false, LoggingThreshold.All);
                        
                        break;
                    }
            }

            return result;
        }

    }
}
