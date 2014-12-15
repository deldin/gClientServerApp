using System;
using System.IO;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace Compiler.Factory
{
    internal class Compiler
    {
        public bool Compile(string sourceFilePath, out string errorsOrMessages)
        {
            FileInfo sourceFile = new FileInfo(sourceFilePath);
            StringBuilder sb = new StringBuilder();
            CodeDomProvider provider = null;
            bool compileOk = false;

            provider = GetProvider(sourceFile);

            if (provider != null)
            {

                // Format the executable file name. 
                // Build the output assembly path using the current directory 
                // and <source>_cs.exe or <source>_vb.exe.

                String exeName = String.Format(@"{0}\{1}.exe",
                    System.Environment.CurrentDirectory,
                    sourceFile.Name.Replace(".", "_"));

                CompilerParameters cp = new CompilerParameters();

                // Generate an executable instead of  
                // a class library.
                cp.GenerateExecutable = false;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = exeName;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = true;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                    sourceFilePath);


                if (cr.Errors.Count > 0)
                {
                    foreach (CompilerError ce in cr.Errors)
                    {
                        sb.AppendLine(ce.ToString());
                    }
                }
                else
                {
                    // Display a successful compilation message.
                    Console.WriteLine("Source {0} built into {1} successfully.",
                        sourceFile, cr.PathToAssembly);
                }

                // Return the results of the compilation. 
                if (cr.Errors.Count > 0)
                {
                    compileOk = false;
                }
                else
                {
                    compileOk = true;
                }
            }

            errorsOrMessages = sb.ToString();
            return compileOk;
        }

        private static CodeDomProvider GetProvider(FileInfo sourceFile)
        {
            CodeDomProvider provider = null;
            // Select the code provider based on the input file extension. 
            if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS")
            {
                provider = CodeDomProvider.CreateProvider("CSharp");
            }
            else if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".VB")
            {
                provider = CodeDomProvider.CreateProvider("VisualBasic");
            }
            else
            {
                throw new Exception("Source file must have a .cs or .vb extension");
            }
            return provider;
        }
    }
}