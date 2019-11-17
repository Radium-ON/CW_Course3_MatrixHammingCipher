using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MatrixHammingCipher.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace CodeMatrix.ViewModels
{
    public class MatrixViewModel : BindableBase
    {
        public MatrixViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            _eventAggregator.GetEvent<EnterTextEncodedEvent>().Subscribe(EncodedCipherRecieved);

            GMatrixEditCommand = new DelegateCommand<DataGridCellEditEndingEventArgs>(OnCellEdited);
            SendMessageCommand=new DelegateCommand(SendMessage);

            MatrixCreator = new MatrixCreator();
            GeneratingCodesCollection = new ObservableCollection<ObservableCollection<byte>>
                (ConvertTwoDimArrayToListArrays(MatrixCreator.GeneratingMatrix));
            CheckCodesCollection = new ObservableCollection<ObservableCollection<byte>>
                (ConvertTwoDimArrayToListArrays(MatrixCreator.HammingCodesMatrix));
        }

        private void SendMessage()
        {
            _eventAggregator.GetEvent<HammingCodeSentEvent>().Publish(_matrix);
        }

        private bool CanCellEdit(DataGridCellEditEndingEventArgs args)
        {
            return args != null;
        }

        private void OnCellEdited(DataGridCellEditEndingEventArgs e)
        {
            OnGenMatrixChanged();
        }


        #region Backing Fields
        private ObservableCollection<HCodeBlockViewModel> _hcodesCollection;
        private IEventAggregator _eventAggregator;
        private byte[][] _matrix;
        private ObservableCollection<ObservableCollection<byte>> _generatingCodesCollection;
        private ObservableCollection<ObservableCollection<byte>> _checkCodesCollection;



        #endregion

        private MatrixCreator MatrixCreator { get; set; }

        public ObservableCollection<HCodeBlockViewModel> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
        }

        public ObservableCollection<ObservableCollection<byte>> GeneratingCodesCollection
        {
            get { return _generatingCodesCollection; }
            set { SetProperty(ref _generatingCodesCollection, value); }
        }

        private void OnGenMatrixChanged()
        {
            MatrixCreator = new MatrixCreator(ConvertListArraysToTwoDimArray(GeneratingCodesCollection));
            CheckCodesCollection = new ObservableCollection<ObservableCollection<byte>>
                (ConvertTwoDimArrayToListArrays(MatrixCreator.HammingCodesMatrix));
        }

        public ObservableCollection<ObservableCollection<byte>> CheckCodesCollection
        {
            get { return _checkCodesCollection; }
            set { SetProperty(ref _checkCodesCollection, value); }
        }

        private void EncodedCipherRecieved(string encodedText)
        {
            _matrix = MatrixCreator.GetCodeConstructionsFromCipher(encodedText);
            var coll = new List<HCodeBlockViewModel>();
            foreach (var constr in _matrix)
            {
                coll.Add(new HCodeBlockViewModel(constr));
            }

            HCodesCollection = new ObservableCollection<HCodeBlockViewModel>(coll);
        }

        private ObservableCollection<ObservableCollection<byte>> ConvertTwoDimArrayToListArrays(byte[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            var list = new ObservableCollection<ObservableCollection<byte>>();
            for (var row = 0; row < rows; row++)
            {
                var line = new ObservableCollection<byte>();
                for (var col = 0; col < columns; col++)
                {
                    line.Add(matrix[row, col]);
                }
                list.Add(line);
            }

            return list;
        }
        private byte[,] ConvertListArraysToTwoDimArray(ObservableCollection<ObservableCollection<byte>> list)
        {
            var rows = list.Count;
            var columns = list[0].Count;
            var matrix = new byte[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    matrix[row, col] = list[row][col];
                }
            }

            return matrix;
        }

        #region DelegateCommands
        public DelegateCommand<DataGridCellEditEndingEventArgs> GMatrixEditCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }
        #endregion
    }
}
