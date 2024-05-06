using ImageDataExtractor.ViewModels;
using System.Windows;

namespace ImageDataExtractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.MainWidnowViewModel viewModel;
        public MainWindow()
        {
            viewModel = new MainWidnowViewModel();
            this.DataContext = viewModel;

            InitializeComponent();
        }


    }
}
