using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticAnalysis.DependencyAnalyzer;
using Tools.Common.Loggers;

namespace DependencyAnalysis.Fx
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new DependencyAnalyzer();
            analyzer.Logger = new AnalysisLogger(Directory.GetCurrentDirectory());
            analyzer.IsWindowsPwsh = true;

            var installDir = @"C:\AME\work-azure-powershell\artifacts\Release";
            var directories = new List<string> { installDir }.Where((d) => Directory.Exists(d)).ToList<string>();
            analyzer.Analyze(directories, new List<string>());
        }
    }
}
