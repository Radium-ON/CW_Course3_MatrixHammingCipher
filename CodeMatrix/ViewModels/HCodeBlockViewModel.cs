using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMatrix.ViewModels
{
    public class HCodeBlockViewModel : BindableBase
    {
        public HCodeBlockViewModel()
        {

        }

        public HCodeBlockViewModel(byte[] codeblock)
        {
            if (codeblock.Length == 8)
            {
                _parityBit = codeblock[0].ToString();
                _infoBits = codeblock.Skip(1).Take(4).ToString();
                _checkBits = codeblock.Skip(5).ToString();
            }
        }

        #region Backing Fields

        private string _parityBit;
        private string _infoBits;
        private string _checkBits;

        #endregion

        public string InfoBits
        {
            get => _infoBits;
            set => SetProperty(ref _infoBits, value);
        }


        public string CheckBits
        {
            get => _checkBits;
            set => SetProperty(ref _checkBits, value);
        }



        public string ParityBit
        {
            get => _parityBit;
            set => SetProperty(ref _parityBit, value);
        }
    }
}