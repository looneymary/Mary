using System.Windows;

namespace WorkerViewer
{
    public enum ViewForm { Create = 0, Update = 1, View = 2 }
        
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            InitializeComponent();
        }
    }
}
