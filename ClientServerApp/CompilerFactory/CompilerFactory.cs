using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Compiler.Factory
{
    public class CompilerFactory
    {
        public static bool Compile(string compilerToUse, string pathorfile, out string errorsOrMessages)
        {
            bool result = true;
            errorsOrMessages = "";
            switch (compilerToUse)
            {
                case "msbuild":                
                    {
                        MSBuild msbuild = new MSBuild();
                        var slnOrprojFile = Directory.GetFiles(pathorfile, "*.sln, *.proj").FirstOrDefault();
                        result = msbuild.Compile(slnOrprojFile, out errorsOrMessages);
                        break;
                    }
                case "compiler":
                default:
                    {
                        Compiler compiler = new Compiler();
                        result = compiler.Compile(pathorfile, out errorsOrMessages);
                        break;
                    }
            }
            return result;

        }

    }
}
