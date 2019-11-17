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
        private IEventAggregator _eventAggregator;

        private string _message;
        private ObservableCollection<byte[]> _hcodesCollection;

        

        #endregion

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public CorrectorViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            _eventAggregator.GetEvent<HammingCodeSentEvent>().Subscribe(HammingCodesRecieved);
            Message = "View HammingCorrector from your Prism Module";
        }

        public ObservableCollection<byte[]> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
        }

        private void HammingCodesRecieved(byte[][] matrix)
        {
            var coll = new List<byte[]>();
            foreach (var constr in matrix)
            {
                coll.Add(constr);
            }

            HCodesCollection = new ObservableCollection<byte[]>(coll);
        }
    }
}
