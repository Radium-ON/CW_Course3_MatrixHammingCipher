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

namespace GilbertMourEncoding.ViewModels
{
    public class GilbertViewModel : BindableBase
    {
        #region AutoProperties
        public TableRecord Entropy { get; set; }



        #endregion

        public GilbertViewModel()
        {
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
        }



        private void OnTextEnterChanged()
        {
            Entropy = new TableRecord(EnterText);
            CharStatsCollection = new ObservableCollection<TableRecord.CharStats>(Entropy.ListStatsOneChar);
        }

        #region Backing Fields
        private string _enterText;
        private ObservableCollection<TableRecord.CharStats> _charStatsCollection;
        private bool _isTextEntered;
        private ObservableCollection<GilbertMourCodeAlgorithm.CodeEntry> _mourCollection;

        #endregion

        #region Properties
        public string EnterText
        {
            get => _enterText;
            set => SetProperty(ref _enterText, value, OnTextEnterChanged);
        }

        public ObservableCollection<TableRecord.CharStats> CharStatsCollection
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
