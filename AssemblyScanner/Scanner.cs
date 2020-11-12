using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace AssemblyScanner
{
    public class InfoCell:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string category;
        public string type { get; set; }
        public string name { get; set; }
        public List<InfoCell> InfoCells { get; set; }
            = new List<InfoCell>();
        public string Category
        {
            get { return category; }
            set
            {
                if (category == value) 
                    return; 
                category = value;
                OnPropertyChanged("category");
                
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Scanner
    {
        private Assembly assembly;

        public void AssemblyLoad(string assemblyFilePath)
        {
            assembly = Assembly.LoadFrom(assemblyFilePath);
        }

        public InfoCell AssemblyScan()
        {
            var infoAssembly = new InfoCell
            {
                Category = "Assembly",
                name = assembly.GetName().Name,
                type = assembly.GetType().ToString(),
            };

            var namespaces = new Dictionary<string, InfoCell>();

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                // get or create namespace info
                if (!namespaces.TryGetValue(type.Namespace, out var nsInfo)) {
                    nsInfo = namespaces[type.Namespace] = new InfoCell
                    {
                        name = type.Namespace,
                        Category = "Namespace",
                        type = "None",
                    };
                    infoAssembly.InfoCells.Add(nsInfo);
                }

                // get class info
                var classInfo = new InfoCell
                {
                    name = type.FullName,
                    Category = "Class",
                    type = "None"
                };
                nsInfo.InfoCells.Add(classInfo);

                var propsAndFields = type.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.Public
                );

                var methods = type.GetMethods(
                    BindingFlags.Instance |
                    BindingFlags.Public   |
                    BindingFlags.DeclaredOnly
                );
                foreach (var propOrField in propsAndFields)
                {
                    var propOrFieldInfo = new InfoCell
                    {
                        name = propOrField.Name,
                        type = propOrField.FieldType.ToString(),
                        Category = "propertyOrField"
                    };
                    classInfo.InfoCells.Add(propOrFieldInfo);
                }

                foreach (var method in methods)
                {
                    var param = method.GetParameters().Select(p => String.Format("{0} {1}", p.ParameterType.Name, p.Name));
                    string signature;
                    if (param.Count() == 0)
                    {
                        signature = method.Name + "()";
                    }else
                    {
                        signature = String.Format("{0} {1}({2})", method.ReturnType.Name, method.Name, String.Join(",", param));
                    };
                    var methodInfo = new InfoCell
                    {
                        name = method.Name,
                        type = signature,
                        Category = "method"
                    };
                    classInfo.InfoCells.Add(methodInfo);
                }


            }

            
            return infoAssembly;
        }

        public void AssemblyUnload()
        {
            assembly = null;
        }
    }
}
