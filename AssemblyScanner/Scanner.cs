using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyScanner
{
    public class InfoCell:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string category;
        private string type;
        private string name;
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
                OnPropertyChanged();
                
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged();

            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                if (type == value)
                    return;
                type = value;
                OnPropertyChanged();

            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Scanner
    {
        private Assembly assembly;

        public void AssemblyLoad(string assemblyFilePath)
        {
            try
            {
                assembly = Assembly.LoadFrom(assemblyFilePath);
            }
            catch (Exception)
            {
                Console.WriteLine("something wrong");
            }
            
        }

        public InfoCell AssemblyScan()
        {
            try
            {
                var infoAssembly = new InfoCell
                {
                    Category = "Assembly",
                    Name = assembly.GetName().Name,
                    Type = assembly.GetType().ToString(),
                };

                var namespaces = new Dictionary<string, InfoCell>();

                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    // get or create namespace info
                    if (!namespaces.TryGetValue(type.Namespace, out var nsInfo))
                    {
                        nsInfo = namespaces[type.Namespace] = new InfoCell
                        {
                            Name = type.Namespace,
                            Category = "Namespace",
                            Type = "None",
                        };
                        infoAssembly.InfoCells.Add(nsInfo);
                    }

                    // get class info
                    var classInfo = new InfoCell
                    {
                        Name = type.FullName,
                        Category = "Class",
                        Type = "None"
                    };
                    nsInfo.InfoCells.Add(classInfo);

                    var propsAndFields = type.GetFields(
                        BindingFlags.Instance |
                        BindingFlags.Public
                    );

                    var methods = type.GetMethods(
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.DeclaredOnly
                    );
                    foreach (var propOrField in propsAndFields)
                    {
                        var propOrFieldInfo = new InfoCell
                        {
                            Name = propOrField.Name,
                            Type = propOrField.FieldType.ToString(),
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
                        }
                        else
                        {
                            signature = String.Format("{0} {1}({2})", method.ReturnType.Name, method.Name, String.Join(",", param));
                        };
                        var methodInfo = new InfoCell
                        {
                            Name = method.Name,
                            Type = signature,
                            Category = "method"
                        };
                        classInfo.InfoCells.Add(methodInfo);
                    }


                }


                return infoAssembly;
            }
            catch (Exception)
            {
                var infoAssembly = new InfoCell
                {
                    Category = "err",
                    Name = "err",
                    Type = "err",
                };
                Console.WriteLine("smt wrong");
                return infoAssembly;
            }
            
        }

        public void AssemblyUnload()
        {
            assembly = null;
        }
    }
}
