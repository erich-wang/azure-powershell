using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Profile.Utilities
{
    public static class CustomAssemblyResolver
    {
        public static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //args.Name
            //throw new NotImplementedException();
            return null;
        }
    }
}
