using FindForbiddenWordWpf.AdditionalClasses;
using FindForbiddenWordWpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindForbiddenWordWpf.ViewModels
{
    public class WordViewModel : BaseViewModel
    {
        public DoneCommand DoneCommand => new DoneCommand(this);
        public AddWordCommand AddWordCommand => new AddWordCommand(this);
        public StartCommand StartCommand => new StartCommand(this);
        public List<ForbiddenWord> AllWords { get; set; }
        private ForbiddenWord currentWord;
        public WordViewModel()
        {
            CurrentWord = new ForbiddenWord();
        }
        private string notification;
        public string Notification
        {
            get
            {
                return notification;
            }
            set
            {
                notification = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Notification)));
            }
        }
        public ForbiddenWord CurrentWord
        {
            get
            {
                return currentWord;
            }
            set
            {
                currentWord = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentWord)));
            }
        }
    }
}

