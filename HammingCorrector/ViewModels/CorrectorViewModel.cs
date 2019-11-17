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

        private ObservableCollection<ObservableCollection<byte>> _hcodesCollection;



        #endregion



        public CorrectorViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            _eventAggregator.GetEvent<HammingCodeSentEvent>().Subscribe(HammingCodesRecieved);
        }

        public ObservableCollection<ObservableCollection<byte>> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
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
    }
}
