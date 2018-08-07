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


namespace AnketHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }

        private void textBlock_Drop(object sender, DragEventArgs e)
        {
            string[] droppedFilePath = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePath[0]!="")
            {
                textBlock.Text = System.IO.Path.GetFullPath(droppedFilePath[0]);
                //System.Diagnostics.Process.Start(droppedFilePath[0]);
               
            }
        }

        private void label4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dr =fbd.ShowDialog();
            if (dr== System.Windows.Forms.DialogResult.OK)
            {
                label4.Content = fbd.SelectedPath+@"\";
            }
        }
    }
}
