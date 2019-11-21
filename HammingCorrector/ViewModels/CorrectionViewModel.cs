using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HammingCorrector.ViewModels
{
    public class CorrectionViewModel : BindableBase
    {
        public CorrectionViewModel(byte[] errorType, byte[] correction)
        {
            ErrorType = errorType;
            CorrectConstruction = correction;
        }

        private byte[] _correctConstruction;
        public byte[] CorrectConstruction
        {
            get { return _correctConstruction; }
            set { SetProperty(ref _correctConstruction, value); }
        }

        private byte[] _errorType;
        public byte[] ErrorType
        {
            get { return _errorType; }
            set { SetProperty(ref _errorType, value); }
        }

    }
}
