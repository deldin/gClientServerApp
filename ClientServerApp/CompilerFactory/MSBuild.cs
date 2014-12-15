using Microsoft.Build.BuildEngine;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.IO;

namespace Compiler.Factory
{
public class MSBuild
{
    BasicFileLogger b;
    
    [STAThread]
    internal bool Compile(string projectFileName, out string errorsOrMessages)
    {
        string logfile = Path.GetTempFileName();
        b = new BasicFileLogger();
        b.Parameters = logfile;
        b.register();
        
        Engine.GlobalEngine.BuildEnabled = true;
        Project p = new Project(Engine.GlobalEngine);
        p.BuildEnabled = true;
        p.Load(projectFileName);
        bool result = p.Build();

        string output = b.getLogoutput();
        output += "nt" + b.Warningcount + " Warnings. ";
        output += "nt" + b.Errorcount + " Errors. ";
        b.Shutdown();
        File.Delete(logfile);

        errorsOrMessages = output;

        return result;
    }
}
}