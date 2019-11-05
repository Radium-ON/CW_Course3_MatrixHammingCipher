﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammingCorrectorModule.ViewModels
{
    public class CorrectorViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public CorrectorViewModel()
        {
            Message = "View HammingCorrector from your Prism Module";
        }
    }
}