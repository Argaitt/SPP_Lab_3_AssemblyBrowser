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
using Microsoft.Win32;
using System.IO;

namespace CCP_Lab_1_AssemblyBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Scanner scanner = new Scanner();
        InfoCell info = new InfoCell();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                scanner.AssemblyLoad(File.ReadAllText(openFileDialog.FileName));
            info = scanner.AssemblyScan();
            var nodes = new List<InfoCell>();
            nodes.Add(info);
            assembly_info.ItemsSource = nodes;
        }
    }
}
