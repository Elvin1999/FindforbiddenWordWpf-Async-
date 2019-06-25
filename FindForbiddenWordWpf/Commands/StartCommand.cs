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
        public int FileCount { get; set; } = 0;
        public void SetFileCount(string sDir)
        {

            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        FileCount++;
                    }
                    SetFileCount(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

        }
        public string MyDirectory { get; set; }
        public void DirSearch(string sDir)
        {
            Data = String.Empty;

            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {

                        counter++;
                        WordViewModel.All_Files_Count = counter * 100 / FileCount;
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
                                    string currentPath = Directory.GetCurrentDirectory();
                                    MyDirectory = currentPath + @"\MyDir";
                                    Directory.CreateDirectory(MyDirectory);
                                    ++item.Count;
                                }
                            }
                        }
                        File.Copy(f, MyDirectory +"\\"+ Path.GetFileName(f));
                    }
                    
                    DirSearch(d);
                }
                //WordViewModel.Notification = "Process finished .";
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

        }
        int counter = 0;
        int divCount = 0;
        public void Execute(object parameter)
        {
            string file_name = @"C:\Users\Jama_yw17\source\repos\FindforbiddenWordWpf-Async-\FindForbiddenWordWpf\bin\Debug";
            SetFileCount(file_name);
            DirSearch(file_name);       
            Config config = new Config()
            {
                FileName = "words.json"
            };
            config.ForbiddenWords = WordViewModel.AllWords;
            config.SeriailizeFilialsToJson();
        }
    }
}
