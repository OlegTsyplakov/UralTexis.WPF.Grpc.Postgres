using System.Windows;
using UralTexis.WPF.ViewModels;

namespace UralTexis.WPF.Grpc.Postgres
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
  
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
