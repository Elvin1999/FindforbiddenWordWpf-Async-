using FindForbiddenWordWpf.AdditionalClasses;
using FindForbiddenWordWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FindForbiddenWordWpf.Commands
{
    public class DoneCommand : ICommand
    {
        public DoneCommand(WordViewModel wordViewModel)
        {
            WordViewModel = wordViewModel;
        }

        public event EventHandler CanExecuteChanged;
        public WordViewModel WordViewModel { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var count = WordViewModel.AllWords.Count;
            if (count != 0)
            {
                Config config = new Config();
                config.ForbiddenWords = WordViewModel.AllWords;
                config.SeriailizeFilialsToJson();
            }
        }
    }
}
