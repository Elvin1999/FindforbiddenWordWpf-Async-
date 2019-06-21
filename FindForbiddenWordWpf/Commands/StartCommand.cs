using FindForbiddenWordWpf.AdditionalClasses;
using FindForbiddenWordWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FindForbiddenWordWpf.Commands
{
    public class StartCommand : ICommand
    {
        public StartCommand(WordViewModel wordViewModel)
        {
            WordViewModel = wordViewModel;
        }

        public event EventHandler CanExecuteChanged;
        public WordViewModel WordViewModel { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public bool HasForbiddenWord(string word)
        {

            var hasWord = Data.Contains(word);
            if (hasWord)
            {
                return true;
            }
            return false;
        }
        public string Data { get; set; }
        public void DirSearch(string sDir)
        {
            Data = String.Empty;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {                        
                        foreach (var item in WordViewModel.AllWords)
                        {
                                  
                            if (f.Contains(".txt"))
                            {
                                using (StreamReader sr = new StreamReader(f))
                                {
                                    Data = sr.ReadToEnd();
                                }
                                if (HasForbiddenWord(item.Word))
                                {
                                    ++item.Count;

                                }
                            }
                        }

                    }
                    DirSearch(d);
                }
               // WordViewModel.Notification = "Process finished .";
               timer.Stop();
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            WordViewModel.All_Files_Count += 1;
        }
        public void Execute(object parameter)
        {
            DirSearch(@"C:\Users\Jama_yw17");
            Config config = new Config()
            {
                FileName = "words.json"
            };
            config.ForbiddenWords = WordViewModel.AllWords;
            config.SeriailizeFilialsToJson();
        }
    }
}
