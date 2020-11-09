using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace AssemblyScanner
{ 
    class InfoCell
    {
        internal string category;
        internal string type;
        internal string name;
        internal List<InfoCell> InfoCells;
        public  InfoCell()
        {
            InfoCells = new List<InfoCell>();
        }
    }
    public class Scanner
    {
        private Assembly assembly;
        public void AssemblyLoad(string assemblyFilePath)
        {
            assembly = Assembly.LoadFrom(assemblyFilePath);
            
        }
        public object AssemblyScan()
        {
            InfoCell infoAssembly = new InfoCell();
            infoAssembly.category = "Assembly";
            infoAssembly.name = assembly.GetName().Name;
            infoAssembly.type = assembly.GetType().ToString();

            
            Type[] types = assembly.GetTypes();
            foreach (var item in types)
            {
               
                if (infoAssembly.InfoCells.Find(namespaceItem => namespaceItem.name == item.Namespace) == null)
                {
                    InfoCell infoNamespace = new InfoCell();
                    infoNamespace.name = item.Namespace;
                    infoNamespace.category = "Namespace";
                    infoNamespace.type = "None";
                    infoAssembly.InfoCells.Add(infoNamespace);
                }      
            }

            foreach (var item in types)
            {
                var element = infoAssembly.InfoCells.Find(namespaceItem => namespaceItem.name == item.Namespace);
                InfoCell infoClass = new InfoCell();
                infoClass.name = item.Name;
                infoClass.category = "Class";
                infoClass.type = "None";
                element.InfoCells.Add(infoClass);
                FieldInfo[] PropertysInfo = item.GetType().GetFields();
                MethodInfo[] MethodsInfo = item.GetType().GetMethods(BindingFlags.DeclaredOnly);
            }

            object assemblyData = new object();
            return assemblyData;
        }
        public void AssemblyUnload()
        {
            assembly = null;
        }
    }
}
