using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HammingCorrector.ViewModels
{
    public class SyndromeViewModel : BindableBase
    {
        public SyndromeViewModel(byte[] vector)
        {
            SOptional = vector.FirstOrDefault();
            Syndrome = string.Join(" ", vector.Skip(1));
        }

        private byte _soptional;
        public byte SOptional
        {
            get { return _soptional; }
            set { SetProperty(ref _soptional, value); }
        }

        private string _syndrome;
        public string Syndrome
        {
            get { return _syndrome; }
            set { SetProperty(ref _syndrome, value); }
        }
    }
}
