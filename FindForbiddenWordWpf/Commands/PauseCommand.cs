﻿using FindForbiddenWordWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FindForbiddenWordWpf.Commands
{
    public class PauseCommand : ICommand
    {
        public PauseCommand(WordViewModel wordViewModel)
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
            if (WordViewModel.Thread.ThreadState != System.Threading.ThreadState.Suspended)
            {

                WordViewModel.Thread.Suspend();
            }
            else
            {
                WordViewModel.Thread.Resume();
            }
        }
    }
}
