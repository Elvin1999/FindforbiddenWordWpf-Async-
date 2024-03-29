﻿using FindForbiddenWordWpf.AdditionalClasses;
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
        public WordViewModel wordViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            wordViewModel = new WordViewModel();
            Config = new Config();
            if (File.Exists("words.json"))
            {
                wordViewModel.AllWords = Config.DeserializeWordsFromJson();
            }
            else
            {
                wordViewModel.AllWords = new List<ForbiddenWord>();
            }
            wordViewModel.ProgressBarMaximum = Convert.ToInt32(filesProgressBar.Maximum);
            DataContext = wordViewModel;
        }
        private void WordListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }
        private void WriteNewData(string filename)
        {
            ReadTxt readTxt = new ReadTxt()
            {
                FileName = filename
            };
            var items = readTxt.GetWords();
            if (items != null)
            {
                Config config = new Config();
            
                if (File.Exists("words.json"))
                {
                    config.ForbiddenWords = config.DeserializeWordsFromJson();
                    config.ForbiddenWords.AddRange(items);
                    config.SeriailizeWordsToJson();
                }
                else
                {
                    config.ForbiddenWords = items;
                }
                config.SeriailizeWordsToJson();
                wordViewModel.AllWords.AddRange(items);
            }
        }
        private void WordListView_Drop(object sender, DragEventArgs e)
        {
            var mydata = e.Data.GetData(DataFormats.FileDrop) as string[];
            var Info = new FileInfo(mydata[0]);
            var filename = Info.FullName;
            WriteNewData(filename);
        }
    }
}









