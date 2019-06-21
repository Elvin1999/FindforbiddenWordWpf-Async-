using FindForbiddenWordWpf.AdditionalClasses;
using FindForbiddenWordWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FindForbiddenWordWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Config Config { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WordViewModel wordViewModel = new WordViewModel();
            Config = new Config();
            if (File.Exists("words.json"))
            {
                wordViewModel.AllWords = Config.DeserializeFilialsFromJson();
            }
            else
            {
                wordViewModel.AllWords = new List<ForbiddenWord>();
            }      
            DataContext = wordViewModel;
        }

   
        private void WordListView_DragEnter(object sender, DragEventArgs e)
        {
            //MessageBox.Show("I entered");
        }

        private void WordListView_Drop(object sender, DragEventArgs e)
        {
            //MessageBox.Show("I dropped");
        }
    }
}
