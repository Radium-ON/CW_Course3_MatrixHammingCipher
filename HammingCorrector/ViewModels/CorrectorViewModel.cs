using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixHammingCipher.Core;
using Prism.Events;

namespace HammingCorrector.ViewModels
{
    public class CorrectorViewModel : BindableBase
    {
        #region Backing Fields
        private readonly IEventAggregator _eventAggregator;

        private byte[,] _checkCodesMatrix;

        private ObservableCollection<ObservableCollection<byte>> _hcodesCollection;
        private ObservableCollection<SyndromeViewModel> _syndromeCollection;
        private ObservableCollection<CorrectionViewModel> _corrections;

        #endregion



        public CorrectorViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            _eventAggregator.GetEvent<HammingCodeSentEvent>().Subscribe(HammingCodesRecieved);
            _eventAggregator.GetEvent<CheckCodesMatrixSentEvent>().Subscribe(CheckMatrixRecieved);
            CorrectCodeCommand = new DelegateCommand(RepairMessageBlocks, CanRepair).ObservesProperty(() => HCodesCollection);
        }

        private void CheckMatrixRecieved(byte[,] matrix)
        {
            _checkCodesMatrix = matrix;
        }

        private bool CanRepair()
        {
            return !(HCodesCollection == null || HCodesCollection.Count == 0);
        }

        private void RepairMessageBlocks()
        {

        }

        public ObservableCollection<ObservableCollection<byte>> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
        }

        public ObservableCollection<SyndromeViewModel> SyndromeCollection
        {
            get { return _syndromeCollection; }
            set { SetProperty(ref _syndromeCollection, value); }
        }

        public ObservableCollection<CorrectionViewModel> Corrections
        {
            get { return _corrections; }
            set { SetProperty(ref _corrections, value); }
        }

        private void HammingCodesRecieved(byte[][] matrix)
        {
            var collection = new ObservableCollection<ObservableCollection<byte>>();
            foreach (var constr in matrix)
            {
                var line = new ObservableCollection<byte>();
                foreach (var b in constr)
                {
                    line.Add(b);
                }
                collection.Add(line);
            }

            HCodesCollection = collection;
        }

        #region DelegateCommands

        public DelegateCommand CorrectCodeCommand { get; private set; }

        #endregion
    }
}
