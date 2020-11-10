using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssemblyScanner;


namespace CCP_Lab_1_AssemblyBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Scanner scanner = new Scanner();
            scanner.AssemblyLoad("D:\\Bsuir\\SPP\\CCP_Lab_1_AssemblyBrowser\\UsersClasses.dll");
            var info = scanner.AssemblyScan();
            
        }
    }
}
