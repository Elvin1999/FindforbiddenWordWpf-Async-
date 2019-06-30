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
        public Config config { get; set; }
        public StartCommand(WordViewModel wordViewModel)
        {
            config = new Config();
            WordViewModel = wordViewModel;
            if (File.Exists("reports.json"))
            {
                Reports = config.DeserializeReportsFromJson();
            }
            else
            {
                Reports = new Reports();
                Reports.AllReports = new List<Report>();
            }
            Reports.Top10FamousWord = new List<ForbiddenWord>();
        }
        public Reports Reports { get; set; }
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
        public void ChangeForbiddenWords()
        {


            foreach (string f in Directory.GetFiles(MyDirectory))
            {

                foreach (var item in WordViewModel.AllWords)
                {
                    String strFile = File.ReadAllText(f);
                    strFile = strFile.Replace(item.Word, "*******");
                    File.WriteAllText(f, strFile);
                }
            }

        }
        public string MyDirectory { get; set; }
        public Report Report { get; set; }
        public void DirSearch(string sDir)
        {

            Data = String.Empty;
            string currentPath = Directory.GetCurrentDirectory();
            MyDirectory = currentPath + @"\MyDir";
            //MessageBox.Show(FileCount.ToString());
            string copyfile = String.Empty;
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        Report = new Report();
                        Report.ForbiddenWords = new List<string>();
                        counter++;
                        WordViewModel.All_Files_Count = counter * 100 / FileCount + 1;
                        // MessageBox.Show(counter.ToString());
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
                                    Directory.CreateDirectory(MyDirectory);
                                    ++item.Count;
                                    Report.ForbiddenWords.Add(item.Word);
                                    copyfile = f;
                                }
                            }
                        }
                        try
                        {
                            File.Copy(copyfile, MyDirectory + "\\" + Path.GetFileName(copyfile));
                            FileInfo fileInfo = new FileInfo(f);
                            Report.FileSize = fileInfo.Length;
                            Report.FilePath = fileInfo.FullName;
                            Reports.AllReports.Add(Report);
                        }
                        catch (Exception)
                        {
                        }

                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
        int counter = 0;
        public bool IsEntered { get; set; } = false;
        public void Execute(object parameter)
        {
            string file_name = @"C:\Users\Documents\source\repos\FindforbiddenWordWpf-Async-2";
            SetFileCount(file_name);
            DirSearch(file_name);
            config.ForbiddenWords = WordViewModel.AllWords;
            config.SeriailizeWordsToJson();
            var items = WordViewModel.AllWords.OrderByDescending(x => x.Count).ToList();
            if (items.Count >= 10)
            {
                var top10Items = items.Take(10).ToList();

                Reports.Top10FamousWord = top10Items;
            }
            else
            {
                Reports.Top10FamousWord = items;

            }
            config.Reports = Reports;
            config.SeriailizeReportsToJson();
            ChangeForbiddenWords();
            
            config.SeriailizeWordsToJson();

        }
    }
}
