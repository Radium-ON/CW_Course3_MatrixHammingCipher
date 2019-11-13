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
            _eventAggregator.GetEvent<EnterTextEncodedEvent>().Subscribe(EncodedCipherRecieved);
            MatrixCreator=new MatrixCreator();
            GeneratingCodesCollection = new ObservableCollection<byte[]>
                (ConvertTwoDimArrayToListArrays(MatrixCreator.GeneratingMatrix));
            CheckCodesCollection = new ObservableCollection<byte[]>
                (ConvertTwoDimArrayToListArrays(MatrixCreator.HammingCodesMatrix));
        }

        #region Backing Fields
        private ObservableCollection<HCodeBlockViewModel> _hcodesCollection;
        private IEventAggregator _eventAggregator;
        private ObservableCollection<byte[]> _generatingCodesCollection;
        private ObservableCollection<byte[]> _checkCodesCollection;



        #endregion

        private MatrixCreator MatrixCreator { get; set; }

        public ObservableCollection<HCodeBlockViewModel> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
        }

        public ObservableCollection<byte[]> GeneratingCodesCollection
        {
            get { return _generatingCodesCollection; }
            set { SetProperty(ref _generatingCodesCollection, value,OnGenMatrixChanged); }
        }

        private void OnGenMatrixChanged()
        {
            MatrixCreator = new MatrixCreator(ConvertListArraysToTwoDimArray(GeneratingCodesCollection.ToList()));
            CheckCodesCollection=new ObservableCollection<byte[]>
                (ConvertTwoDimArrayToListArrays(MatrixCreator.HammingCodesMatrix));
        }

        public ObservableCollection<byte[]> CheckCodesCollection
        {
            get { return _checkCodesCollection; }
            set { SetProperty(ref _checkCodesCollection, value); }
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

        private List<byte[]> ConvertTwoDimArrayToListArrays(byte[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            var list = new List<byte[]>();
            for (var row = 0; row < rows; row++)
            {
                var line = new byte[columns];
                for (var col = 0; col < columns; col++)
                {
                    line[col] = matrix[row, col];
                }
                list.Add(line);
            }

            return list;
        }
        private byte[,] ConvertListArraysToTwoDimArray(List<byte[]> list)
        {
            var rows = list.Count;
            var columns = list[0].Length;
            var matrix = new byte[rows,columns];
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    matrix[row, col]=list[row][col];
                }
            }

            return matrix;
        }
    }
}
