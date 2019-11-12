using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GilbertMourEncoding.Models;
using MatrixHammingCipher.Core;
using Prism.Events;

namespace GilbertMourEncoding.ViewModels
{
    public class GilbertViewModel : BindableBase
    {
        #region AutoProperties
        public CodingStepsTable Entropy { get; set; }



        #endregion

        public GilbertViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            MourCodingCommand = new DelegateCommand(Coding, CanCoding).ObservesProperty(() => EnterText);
            MourCollection = new ObservableCollection<GilbertMourCodeAlgorithm.CodeEntry>();
        }

        private bool CanCoding()
        {
            return !string.IsNullOrEmpty(EnterText);
        }

        private void Coding()
        {
            var mour = new GilbertMourCodeAlgorithm(CharStatsCollection);
            MourCollection = new ObservableCollection<GilbertMourCodeAlgorithm.CodeEntry>(mour.CodeEntries);
            EncodedText = GetCipherFromEnterText(mour.CodeEntries, EnterText);
            //опубликована посылка со строкой шифротекста
            _eventAggregator.GetEvent<EnterTextEncodedEvent>().Publish(EncodedText);
        }

        private string GetCipherFromEnterText(List<GilbertMourCodeAlgorithm.CodeEntry> mourCodeEntries, string enterText)
        {
            var sb = new StringBuilder();
            foreach (var sign in enterText)
            {
                var code = mourCodeEntries.Where(s => s.Xi == sign).Select(c => c.SigmaLimit).Single();
                sb.Append(code);
            }
            return sb.ToString();
        }


        private void OnTextEnterChanged()
        {
            Entropy = new CodingStepsTable(EnterText);
            CharStatsCollection = new ObservableCollection<CodingStepsTable.TableRecord>(Entropy.ListStatsOneChar);
        }

        #region Backing Fields
        IEventAggregator _eventAggregator;
        private string _enterText;
        private ObservableCollection<CodingStepsTable.TableRecord> _charStatsCollection;
        private bool _isTextEntered;
        private ObservableCollection<GilbertMourCodeAlgorithm.CodeEntry> _mourCollection;
        private string _encodedText;

        #endregion

        #region Properties
        public string EnterText
        {
            get => _enterText;
            set => SetProperty(ref _enterText, value, OnTextEnterChanged);
        }
        public string EncodedText
        {
            get => _encodedText;
            set => SetProperty(ref _encodedText, value);
        }

        public ObservableCollection<CodingStepsTable.TableRecord> CharStatsCollection
        {
            get => _charStatsCollection;
            set => SetProperty(ref _charStatsCollection, value);
        }

        public ObservableCollection<GilbertMourCodeAlgorithm.CodeEntry> MourCollection
        {
            get => _mourCollection;
            set => SetProperty(ref _mourCollection, value);
        }

        public bool IsTextEntered
        {
            get => _isTextEntered;
            set => SetProperty(ref _isTextEntered, value);
        }
        #endregion

        #region DelegateCommands
        public ICommand MourCodingCommand { get; private set; }


        #endregion
    }
}
