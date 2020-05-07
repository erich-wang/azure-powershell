using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using StaticAnalysis.DependencyAnalyzer;
using Tools.Common.Loggers;

namespace DependencyAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new DependencyAnalyzer();
            analyzer.Logger = new AnalysisLogger(Directory.GetCurrentDirectory());

            var installDir = @"C:\AME\work-azure-powershell\artifacts\Release";
            var directories = new List<string> { installDir }.Where((d) => Directory.Exists(d)).ToList<string>();
            analyzer.Analyze(directories, new List<string>());
        }
    }
}
