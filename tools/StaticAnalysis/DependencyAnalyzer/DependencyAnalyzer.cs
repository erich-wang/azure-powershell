﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
//using Microsoft.ApplicationInsights.Metrics.Extensibility;
using Newtonsoft.Json;
//using StaticAnalysis.Netcore.Properties;
//using Tools.Common.Helpers;
using Tools.Common.Issues;
using Tools.Common.Loaders;
using Tools.Common.Loggers;
using Tools.Common.Models;

namespace StaticAnalysis.DependencyAnalyzer
{
    /// <summary>
    /// The static analysis tool for ensuring no runtime conflicts in assembly dependencies
    /// </summary>
    public class DependencyAnalyzer : IStaticAnalyzer
    {
        private const int NoAssemblyVersionEvidence = 1000;
        private const int ReferenceDoesNotMatchAssemblyVersion = 1010;
        private const int ExtraAssemblyRecord = 2000;
        private const int MissingAssemblyRecord = 3000;
        private const int AssemblyVersionFileVersionMismatch = 7000;
        private const int CommonAuthenticationMismatch = 7010;

        private const string AzAccounts = "Az.Accounts";

        /*
        private static readonly List<string> FrameworkAssemblies = new List<string>
        {
            "Microsoft.CSharp",
            "Microsoft.Management.Infrastructure",
            "Microsoft.Build",
            "Microsoft.Build.Framework",
            "Microsoft.Win32.Primitives",
            "Microsoft.Win32.Registry",
            "mscorlib",
            "netstandard",
            "Microsoft.Win32.Primitives",
            "System.AppContext",
            "System.Collections",
            "System.Collections.Immutable",
            "System.Collections.Concurrent",
            "System.Collections.NonGeneric",
            "System.Collections.Specialized",
            "System.ComponentModel",
            "System.ComponentModel.EventBasedAsync",
            "System.ComponentModel.Primitives",
            "System.ComponentModel.TypeConverter",
            "System.Console",
            "System.Data.Common",
            "System.Diagnostics.Contracts",
            "System.Diagnostics.Debug",
            "System.Diagnostics.DiagnosticSource",
            "System.Diagnostics.FileVersionInfo",
            "System.Diagnostics.Process",
            "System.Diagnostics.StackTrace",
            "System.Diagnostics.TextWriterTraceListener",
            "System.Diagnostics.Tools",
            "System.Diagnostics.TraceSource",
            "System.Diagnostics.Tracing",
            "System.Drawing.Primitives",
            "System.Dynamic.Runtime",
            "System.Globalization",
            "System.Globalization.Calendars",
            "System.Globalization.Extensions",
            "System.IO",
            "System.IO.Compression",
            "System.IO.Compression.ZipFile",
            "System.IO.FileSystem",
            "System.IO.FileSystem.DriveInfo",
            "System.IO.FileSystem.Primitives",
            "System.IO.FileSystem.Watcher",
            "System.IO.IsolatedStorage",
            "System.IO.MemoryMappedFiles",
            "System.IO.Pipes",
            "System.IO.UnmanagedMemoryStream",
            "System.Linq",
            "System.Linq.Expressions",
            "System.Linq.Parallel",
            "System.Linq.Queryable",
            "System.Management.Automation",
            "System.Net.Http",
            "System.Net.NameResolution",
            "System.Net.NetworkInformation",
            "System.Net.Ping",
            "System.Net.Primitives",
            "System.Net.Requests",
            "System.Net.Security",
            "System.Net.Sockets",
            "System.Net.WebHeaderCollection",
            "System.Net.WebSockets",
            "System.Net.WebSockets.Client",
            "System.ObjectModel",
            "System.Private.DataContractSerialization",
            "System.Reflection",
            "System.Reflection.Emit",
            "System.Reflection.Emit.ILGeneration",
            "System.Reflection.Emit.Lightweight",
            "System.Reflection.Extensions",
            "System.Reflection.Metadata",
            "System.Reflection.Primitives",
            "System.Resources.Reader",
            "System.Resources.ResourceManager",
            "System.Resources.Writer",
            "System.Runtime",
            "System.Runtime.CompilerServices.VisualC",
            "System.Runtime.Extensions",
            "System.Runtime.Handles",
            "System.Runtime.InteropServices",
            "System.Runtime.InteropServices.RuntimeInformation",
            "System.Runtime.Numerics",
            "System.Runtime.Serialization.Formatters",
            "System.Runtime.Serialization.Json",
            "System.Runtime.Serialization.Primitives",
            "System.Runtime.Serialization.Xml",
            "System.Security.Claims",
            "System.Security.Cryptography.Algorithms",
            "System.Security.Cryptography.Csp",
            "System.Security.Cryptography.Encoding",
            "System.Security.Cryptography.Primitives",
            "System.Security.Cryptography.X509Certificates",
            "System.Security.Principal",
            "System.Security.SecureString",
            "System.Text.Encoding",
            "System.Text.Encoding.Extensions",
            "System.Text.RegularExpressions",
            "System.Threading",
            "System.Threading.Overlapped",
            "System.Threading.Tasks",
            "System.Threading.Tasks.Parallel",
            "System.Threading.Thread",
            "System.Threading.ThreadPool",
            "System.Threading.Timer",
            "System.ValueTuple",
            "System.Xml.ReaderWriter",
            "System.Xml.XDocument",
            "System.Xml.XmlDocument",
            "System.Xml.XmlSerializer",
            "System.Xml.XPath",
            "System.Xml.XPath.XDocument",
            "WindowsBase",
            "System.Security.Cryptography.Cng",
            "System.Security.Cryptography.Pkcs",
            "System.Private.CoreLib",
            "System.Private.ServiceModel",
            "System.Private.Xml.Linq",
            "System.Net.Http.WinHttpHandler",
            "System.Net.Mail",
            "System.Security.Permissions",
            "System.Runtime.Loader",
            "System.DirectoryServices",
            "System.Management",
            "System.Configuration",
            "System.Net.WebClient",
            "System.Memory",
            "System.Text.Encoding.CodePages",
            "System.Private.Xml",
            "System.Reflection.DispatchProxy",
            "System.ServiceModel",
            "System.ServiceModel.Syndication",
            "System.ServiceModel.Http",
            "System.ServiceModel.Duplex",
            "System.ServiceModel.NetTcp",
            "System.ServiceModel.Primitives",
            "System.ServiceModel.Security",
            "System.IO.FileSystem.AccessControl",
            "System.Security.Permissions",
            "System.Security.AccessControl",
            "System.Security.Principal.Windows",
            "System.Data.SqlClient",
            "System.Security.Cryptography.ProtectedData"
        };
        */
        private static Dictionary<string, string> FrameworkAssemblies;

        private readonly Dictionary<string, AssemblyRecord> _assemblies =
            new Dictionary<string, AssemblyRecord>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<AssemblyName, AssemblyRecord> _sharedAssemblyReferences =
            new Dictionary<AssemblyName, AssemblyRecord>(new AssemblyNameComparer());
        private readonly Dictionary<string, AssemblyRecord> _identicalSharedAssemblies =
            new Dictionary<string, AssemblyRecord>(StringComparer.OrdinalIgnoreCase);
        private ReportLogger<AssemblyVersionConflict> _versionConflictLogger;
        private ReportLogger<SharedAssemblyConflict> _sharedConflictLogger;
        private ReportLogger<MissingAssembly> _missingAssemblyLogger;
        private ReportLogger<ExtraAssembly> _extraAssemblyLogger;
        private ReportLogger<DependencyMap> _dependencyMapLogger;

        private bool _isNetcore;

        public DependencyAnalyzer()
        {
            Name = "Dependency Analyzer";
        }

        public AnalysisLogger Logger { get; set; }
        public string Name { get; private set; }
        public bool IsWindowsPwsh { get; set; }

        private static Dictionary<string, Version> Pwsh5Assemblies;
        private static Dictionary<string, Version> Pwsh6Assemblies;
        private static Dictionary<string, Version> Pwsh7Assemblies;

        static DependencyAnalyzer() {
            var pwsh6 = File.ReadAllText("./pwsh6.2.4.json");
            var pwsh7 = File.ReadAllText("./pwsh7.0.0.json");
            var pwsh5 = File.ReadAllText("./pwsh5.1.0.json");
            var pwsh5_1 = JsonConvert.DeserializeObject<Dictionary<string, Version>>(pwsh5);
            var pwsh6_2_4 = JsonConvert.DeserializeObject<Dictionary<string, Version>>(pwsh6);
            var pwsh7_0 = JsonConvert.DeserializeObject<Dictionary<string, Version>>(pwsh7);

            Pwsh5Assemblies = new Dictionary<string, Version>(pwsh5_1, StringComparer.InvariantCultureIgnoreCase);
            Pwsh6Assemblies = new Dictionary<string, Version>(pwsh6_2_4, StringComparer.InvariantCultureIgnoreCase);
            Pwsh7Assemblies = new Dictionary<string, Version>(pwsh7_0, StringComparer.InvariantCultureIgnoreCase);
        }

        public void Analyze(IEnumerable<string> scopes)
        {
            Analyze(scopes, null);
        }

        public void Analyze(IEnumerable<string> directories, IEnumerable<String> modulesToAnalyze)
        {
            if (directories == null)
            {
                throw new ArgumentNullException("directories");
            }

            var common = File.ReadAllText("./common.json");
            UpdateReferenceAssemblies(JsonConvert.DeserializeObject<Dictionary<string, Version>>(common));

            var pwshToFrameworkAssemblies = new Dictionary<string, Dictionary<string, string>>()
            {
            };

            Dictionary<string, AssemblyRecord> accountsAssemblies = LoadAzAccountsAssmeblies(directories);
            CheckAssemblies(accountsAssemblies);
            foreach(var item in accountsAssemblies)
            {
                UpdateReferenceAssemblies(item.Key, item.Value.Version);
            }

            foreach (var baseDirectory in directories)
            {
                foreach (var directoryPath in Directory.EnumerateDirectories(baseDirectory))
                {
                    if (directoryPath.EndsWith(AzAccounts))
                    {
                        continue;
                    }

                    if (modulesToAnalyze != null &&
                        modulesToAnalyze.Any() &&
                        !modulesToAnalyze.Any(m => directoryPath.EndsWith(m)))
                    {
                        continue;
                    }

                    if (!Directory.Exists(directoryPath))
                    {
                        throw new InvalidOperationException("Please pass a valid directory name as the first parameter");
                    }

                    Dictionary<string, AssemblyRecord> assemblies = LoadAssemblies(directoryPath);
                    CheckAssemblies(assemblies);
                }
            }


            foreach (var pwshItem in pwshToFrameworkAssemblies)
            {
                var pwshVersion = pwshItem.Key;
                FrameworkAssemblies = new Dictionary<string, string>(pwshItem.Value, StringComparer.InvariantCultureIgnoreCase);
                _versionConflictLogger = Logger.CreateLogger<AssemblyVersionConflict>($"AssemblyVersionConflict{pwshVersion}.csv");
                _sharedConflictLogger = Logger.CreateLogger<SharedAssemblyConflict>($"SharedAssemblyConflict{pwshVersion}.csv");
                _missingAssemblyLogger = Logger.CreateLogger<MissingAssembly>($"MissingAssemblies{pwshVersion}.csv");
                _extraAssemblyLogger = Logger.CreateLogger<ExtraAssembly>($"ExtraAssemblies{pwshVersion}.csv");
                _dependencyMapLogger = Logger.CreateLogger<DependencyMap>($"DependencyMap{pwshVersion}.csv");
                foreach (var baseDirectory in directories)
                {
                    foreach (var directoryPath in Directory.EnumerateDirectories(baseDirectory))
                    {
                        if (directoryPath.EndsWith(AzAccounts))
                        {
                            continue;
                        }

                        if (modulesToAnalyze != null &&
                            modulesToAnalyze.Any() &&
                            !modulesToAnalyze.Any(m => directoryPath.EndsWith(m)))
                        {
                            continue;
                        }

                        if (!Directory.Exists(directoryPath))
                        {
                            throw new InvalidOperationException("Please pass a valid directory name as the first parameter");
                        }

                        Logger.WriteMessage("Processing Directory {0}", directoryPath);
                        _assemblies.Clear();
                        _versionConflictLogger.Decorator.AddDecorator(r => { r.Directory = directoryPath; }, "Directory");
                        _missingAssemblyLogger.Decorator.AddDecorator(r => { r.Directory = directoryPath; }, "Directory");
                        _extraAssemblyLogger.Decorator.AddDecorator(r => { r.Directory = directoryPath; }, "Directory");
                        _dependencyMapLogger.Decorator.AddDecorator(r => { r.Directory = directoryPath; }, "Directory");
                        _isNetcore = directoryPath.Contains("Az.");
                        ProcessDirectory(directoryPath);
                        _versionConflictLogger.Decorator.Remove("Directory");
                        _missingAssemblyLogger.Decorator.Remove("Directory");
                        _extraAssemblyLogger.Decorator.Remove("Directory");
                        _dependencyMapLogger.Decorator.Remove("Directory");
                    }
                }
            }
        }

        private void UpdateReferenceAssemblies(Dictionary<string, Version> assemblies)
        {
            foreach (var item in assemblies)
            {
                UpdateReferenceAssemblies(item.Key, item.Value);
            }
        }

        private void UpdateReferenceAssemblies(string name, Version version)
        {
            if (!Pwsh5Assemblies.ContainsKey(name) || version < Pwsh5Assemblies[name])
            {
                Pwsh5Assemblies[name] = version;
            }
            if (!Pwsh6Assemblies.ContainsKey(name) || version < Pwsh6Assemblies[name])
            {
                Pwsh6Assemblies[name] = version;
            }
            if (!Pwsh7Assemblies.ContainsKey(name) || version < Pwsh7Assemblies[name])
            {
                Pwsh7Assemblies[name] = version;
            }
        }

        private void CheckAssemblies(Dictionary<string, AssemblyRecord> assemblies)
        {
            Dictionary<string, IList<string>> missedAssemblies = new Dictionary<string, IList<string>>();
            foreach (var item in assemblies)
            {
                var assemblyRecord = item.Value;
                if (Pwsh5Assemblies.ContainsKey(assemblyRecord.Name) && Pwsh5Assemblies[assemblyRecord.Name] < assemblyRecord.Version)
                {
                    Console.WriteLine($"{assemblyRecord.Name} is conflicts with Windows PowerShell.");
                }
                if (Pwsh6Assemblies.ContainsKey(assemblyRecord.Name) && Pwsh6Assemblies[assemblyRecord.Name] < assemblyRecord.Version)
                {
                    Console.WriteLine($"{assemblyRecord.Name} is conflicts with PowerShell Core.");
                }
                if (Pwsh7Assemblies.ContainsKey(assemblyRecord.Name) && Pwsh7Assemblies[assemblyRecord.Name] < assemblyRecord.Version)
                {
                    Console.WriteLine($"{assemblyRecord.Name} is conflicts with PowerShell 7.");
                }
                foreach (var child in assemblyRecord.Children)
                {
                    if(assemblies.ContainsKey(child.Name))
                    {
                        continue;
                    }
                    bool missed = false;
                    if (IsWindowsPwsh)
                    {
                        missed = missed || (!Pwsh5Assemblies.ContainsKey(child.Name));
                    }
                    else
                    {
                        missed = missed || (!Pwsh6Assemblies.ContainsKey(child.Name));
                        missed = missed || (!Pwsh7Assemblies.ContainsKey(child.Name));
                    }
                    if(missed)
                    {
                        if (!missedAssemblies.ContainsKey(child.Name))
                        {
                            missedAssemblies[child.Name] = new List<string>();
                        }
                        missedAssemblies[child.Name].Add(assemblyRecord.Name);
                    }
                }
            }
            foreach (var item in missedAssemblies)
            {
                Console.WriteLine($"{item.Key} is missing due to its parent(s) [{String.Join(", ", item.Value.ToArray())}].");
            }
        }

        private Dictionary<string, AssemblyRecord> LoadAzAccountsAssmeblies(IEnumerable<string> directories)
        {
            foreach (var baseDirectory in directories)
            {
                foreach (var directoryPath in Directory.EnumerateDirectories(baseDirectory))
                {
                    if(directoryPath.EndsWith(AzAccounts))
                    {
                        return LoadAssemblies(directoryPath);
                    }
                }

            }
            throw new InvalidOperationException("Cannot find the folder of Az.Accounts module.");
        }

        private AssemblyRecord CreateAssemblyRecord(AssemblyLoader loader, string path)
        {
            AssemblyRecord result = null;
            var fullPath = Path.GetFullPath(path);
            try
            {
                if (path.EndsWith("System.Runtime.CompilerServices.Unsafe.dll"))
                {
                }
                var assembly = LoadByReflectionFromFile(loader, fullPath);
                var versionInfo = FileVersionInfo.GetVersionInfo(fullPath);
                result = new AssemblyRecord
                {
                    AssemblyName = assembly.GetName(),
                    AssemblyFileMajorVersion = versionInfo.FileMajorPart,
                    AssemblyFileMinorVersion = versionInfo.FileMinorPart,
                    Location = fullPath
                };

                foreach (var child in assembly.GetReferencedAssemblies())
                {
                    result.Children.Add(child);
                }
            }
            catch(Exception e)
            {
                Logger.WriteError("Error loading assembly {0}", fullPath);
            }

            return result;
        }

        private bool AddSharedAssembly(AssemblyRecord assembly)
        {
            if (_sharedAssemblyReferences.ContainsKey(assembly.AssemblyName))
            {
                var stored = _sharedAssemblyReferences[assembly.AssemblyName];
                if (assembly.Equals(stored) || IsFrameworkAssembly(assembly.AssemblyName) && assembly.Version.Major <= 4) return true;

                _sharedConflictLogger.LogRecord(new SharedAssemblyConflict
                {
                    AssemblyName = assembly.Name,
                    AssemblyPathsAndFileVersions = new List<Tuple<string, Version>>
                    {
                        new Tuple<string, Version>(assembly.Location, new Version(assembly.AssemblyFileMajorVersion,
                            assembly.AssemblyFileMinorVersion)),
                        new Tuple<string, Version>(stored.Location, new Version(stored.AssemblyFileMajorVersion,
                            stored.AssemblyFileMinorVersion))

                    },
                    AssemblyVersion = assembly.Version,
                    Severity = 0,
                    ProblemId = AssemblyVersionFileVersionMismatch,
                    Description = "Shared assembly conflict, shared assemblies with the same assembly " +
                                  "version have differing file versions",
                    Remediation = string.Format("Update the assembly reference for {0} in one of the " +
                                                "referring assemblies", assembly.Name)
                });

                return false;
            }

            _sharedAssemblyReferences[assembly.AssemblyName] = assembly;

            return true;
        }

        private AssemblyMetadata LoadByReflectionFromFile(AssemblyLoader loader, string assemblyPath)
        {
            var info = loader.GetReflectedAssemblyFromFile(assemblyPath);
            if (info == null)
            {
                throw new InvalidOperationException();
            }

            return info;
        }

        private bool AddSharedAssemblyExactVersion(AssemblyRecord record)
        {
            if (_identicalSharedAssemblies.ContainsKey(record.Name))
            {
                var stored = _identicalSharedAssemblies[record.Name];
                if (record.Equals(stored) || IsFrameworkAssembly(record.AssemblyName)) return true;

                _sharedConflictLogger.LogRecord(new SharedAssemblyConflict
                {
                    AssemblyName = record.Name,
                    AssemblyVersion = record.Version,
                    Severity = 0,
                    ProblemId = CommonAuthenticationMismatch,
                    AssemblyPathsAndFileVersions = new List<Tuple<string, Version>>
                    {
                        new Tuple<string, Version>(record.Location, new Version(record.AssemblyFileMajorVersion,
                            record.AssemblyFileMinorVersion)),
                        new Tuple<string, Version>(stored.Location, new Version(stored.AssemblyFileMajorVersion,
                            stored.AssemblyFileMinorVersion)),
                    },
                    Description = string.Format("Assembly {0} has multiple versions as specified in 'Target'",
                        record.Name),
                    Remediation = string.Format("Ensure that all packages reference exactly the same package " +
                                                "version of {0}", record.Name)

                });

                return false;
            }

            _identicalSharedAssemblies[record.Name] = record;

            return true;
        }

        private static bool IsFrameworkAssembly(AssemblyName name)
        {
            return IsFrameworkAssembly(name.Name);
        }

        private static bool IsFrameworkAssembly(string name)
        {
            return FrameworkAssemblies.ContainsKey(name.ToLower());
        }

        private Dictionary<string, AssemblyRecord> LoadAssemblies(string directoryPath)
        {
            var savedDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(directoryPath);
            AssemblyLoader loader = new AssemblyLoader();
            Dictionary<string, AssemblyRecord> assemblies = new Dictionary<string, AssemblyRecord>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in Directory.GetFiles(directoryPath).Where(file => file.EndsWith(".dll")))
            {
                var platformFolder = IsWindowsPwsh ? "PreloadAssemblies" : "NetCoreAssemblies";
                if (file.Contains(platformFolder))
                    continue;

                var assembly = CreateAssemblyRecord(loader, file);
                //if (assembly?.Name != null && !IsFrameworkAssembly(assembly.Name))
                if (assembly?.Name != null)
                {
                    assemblies[assembly.Name] = assembly;
                    //AddSharedAssembly(assembly);
                }
                else
                {
                    Console.WriteLine($"Fail to load {Path.GetFileName(file)}");
                }

            }
            Directory.SetCurrentDirectory(savedDirectory);
            return assemblies;
        }

        private void ProcessDirectory(string directoryPath)
        {
            var savedDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(directoryPath);
            AssemblyLoader loader = new AssemblyLoader();
            foreach (var file in Directory.GetFiles(directoryPath).Where(file => file.EndsWith(".dll")))
            {
                var assembly = CreateAssemblyRecord(loader, file);
                if (assembly?.Name != null && !IsFrameworkAssembly(assembly.Name))
                {
                    _assemblies[assembly.Name] = assembly;
                    AddSharedAssembly(assembly);
                }

            }

            // Now check for assembly mismatches
            foreach (var assembly in _assemblies.Values)
            {
                foreach (var reference in assembly.Children)
                {
                    CheckAssemblyReference(reference, assembly);
                }
            }

            foreach (var assembly in _assemblies.Values)
            {
                if (!assembly.Name.Contains("Microsoft.IdentityModel") && !assembly.Name.Equals("Newtonsoft.Json") && !IsFrameworkAssembly(assembly.Name))
                {
                    foreach (var parent in assembly.ReferencingAssembly)
                    {
                        _dependencyMapLogger.LogRecord(
                            new DependencyMap
                            {
                                AssemblyName = assembly.Name,
                                AssemblyVersion = assembly.Version.ToString(),
                                ReferencingAssembly = parent.Name,
                                ReferencingAssemblyVersion = parent.Version.ToString(),
                                Severity = 3
                            });
                    }
                }

            }

            FindExtraAssemblies();
            Directory.SetCurrentDirectory(savedDirectory);
        }

        private static bool IsCommandAssembly(AssemblyRecord assembly)
        {
            return assembly.Name.Contains("Commands") || assembly.Name.Contains("Cmdlets");
        }

        private void FindExtraAssemblies()
        {
            if (!_assemblies.Values.Any(a =>
                !IsCommandAssembly(a)
                && (a.ReferencingAssembly == null
                || a.ReferencingAssembly.Count == 0
                || !a.GetAncestors().Any(IsCommandAssembly))))
            {
                return;
            }

            foreach (var assembly in _assemblies.Values.Where(a =>
                !IsCommandAssembly(a)
                && (a.ReferencingAssembly == null
                || a.ReferencingAssembly.Count == 0
                || !a.GetAncestors().Any(IsCommandAssembly))))
            {
                _extraAssemblyLogger.LogRecord(new ExtraAssembly
                {
                    AssemblyName = assembly.Name,
                    Severity = 2,
                    ProblemId = ExtraAssemblyRecord,
                    Description = string.Format("Assembly {0} is not referenced from any cmdlets assembly",
                        assembly.Name),
                    Remediation = string.Format("Remove assembly {0} from the project and regenerate the Wix " +
                                                "file", assembly.Name)
                });
            }
        }

        private void CheckAssemblyReference(AssemblyName reference, AssemblyRecord parent)
        {
            if (_assemblies.ContainsKey(reference.Name))
            {
                var stored = _assemblies[reference.Name];
                if (stored.Equals(reference))
                {
                    stored.ReferencingAssembly.Add(parent);
                }
                else if (reference.Version.Major == 0 && reference.Version.Minor == 0)
                {
                    Logger.WriteWarning("{0}.dll has reference to assembly {1} without any version specification.",
                        parent.Name, reference.Name);
                    _versionConflictLogger.LogRecord(new AssemblyVersionConflict
                    {
                        AssemblyName = reference.Name,
                        ActualVersion = stored.Version,
                        ExpectedVersion = reference.Version,
                        ParentAssembly = parent.Name,
                        ProblemId = NoAssemblyVersionEvidence,
                        Severity = 2,
                        Description = string.Format("Assembly {0} referenced from {1}.dll does not specify any " +
                                                   "assembly version evidence.  The assembly will use version " +
                                                   "{2} from disk.", reference.Name, parent.Name, stored.Version),
                        Remediation = string.Format("Update the reference to assembly {0} from {1} so that " +
                                                   "assembly version evidence is supplied", reference.Name,
                                                   parent.Name)
                    });
                }
                else if (_isNetcore && stored.Version < reference.Version)
                {
                    var minVersion = (stored.Version < reference.Version) ? stored.Version : reference.Version;
                    _versionConflictLogger.LogRecord(new AssemblyVersionConflict
                    {
                        AssemblyName = reference.Name,
                        ActualVersion = stored.Version,
                        ExpectedVersion = reference.Version,
                        ParentAssembly = parent.Name,
                        ProblemId = ReferenceDoesNotMatchAssemblyVersion,
                        Severity = 1,
                        Description = string.Format("Assembly {0} version {1} referenced from {2}.dll does " +
                                                    "not match assembly version on disk: {3}",
                                                    reference.Name, reference.Version, parent.Name, stored.Version),
                        Remediation = string.Format("Update any references to version {0} of assembly {1}",
                        minVersion, reference.Name)
                    });
                }
            }
            else if (!IsFrameworkAssembly(reference))
            {
                _missingAssemblyLogger.LogRecord(new MissingAssembly
                {
                    AssemblyName = reference.Name,
                    AssemblyVersion = reference.Version.ToString(),
                    ReferencingAssembly = parent.Name,
                    Severity = 0,
                    ProblemId = MissingAssemblyRecord,
                    Description = string.Format("Missing assembly {0} referenced from {1}", reference.Name,
                    parent.Name),
                    Remediation = "Ensure that the assembly is included in the Wix file or directory"
                });
            }
        }

        /// <summary>
        /// These methods will be added in a new work item that has enhancements for Static Analysis tool
        /// </summary>
        /// <param name="cmdletProbingDirs"></param>
        /// <param name="directoryFilter"></param>
        /// <param name="cmdletFilter"></param>
        void IStaticAnalyzer.Analyze(IEnumerable<string> cmdletProbingDirs, Func<IEnumerable<string>, IEnumerable<string>> directoryFilter, Func<string, bool> cmdletFilter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// These methods will be added in a new work item that has enhancements for Static Analysis tool
        /// </summary>
        /// <returns></returns>
        public AnalysisReport GetAnalysisReport()
        {
            throw new NotImplementedException();
        }

        public void Analyze(IEnumerable<string> cmdletProbingDirs, Func<IEnumerable<string>, IEnumerable<string>> directoryFilter, Func<string, bool> cmdletFilter, IEnumerable<string> modulesToAnalyze)
        {
            throw new NotImplementedException();
        }
    }
}
