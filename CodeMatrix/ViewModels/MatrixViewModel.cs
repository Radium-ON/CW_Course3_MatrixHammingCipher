using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MatrixHammingCipher.Core;
using Prism.Events;
using Prism.Mvvm;

namespace CodeMatrix.ViewModels
{
    public class MatrixViewModel : BindableBase
    {
        public MatrixViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            HCodesCollection = new ObservableCollection<HCodeBlockViewModel>();
            _eventAggregator.GetEvent<EnterTextEncodedEvent>().Subscribe(EncodedCipherRecieved);
        }

        #region Backing Fields
        private ObservableCollection<HCodeBlockViewModel> _hcodesCollection;
        private IEventAggregator _eventAggregator;



        #endregion

        private MatrixCreator MatrixCreator { get; set; } = new MatrixCreator();

        public ObservableCollection<HCodeBlockViewModel> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
        }

        private void EncodedCipherRecieved(string encodedText)
        {
            var matrix = MatrixCreator.GetCodeConstructionsFromCipher(encodedText);
            var coll = new List<HCodeBlockViewModel>();
            foreach (var constr in matrix)
            {
                coll.Add(new HCodeBlockViewModel(constr));
            }

            HCodesCollection = new ObservableCollection<HCodeBlockViewModel>(coll);
        }
    }
}
