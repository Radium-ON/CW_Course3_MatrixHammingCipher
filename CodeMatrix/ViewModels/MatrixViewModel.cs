using System.Collections.ObjectModel;
using System.Windows;
using Prism.Mvvm;

namespace CodeMatrix.ViewModels
{
    public class MatrixViewModel : BindableBase
    {
        public MatrixViewModel()
        {
            HCodesCollection=new ObservableCollection<HCodeBlockViewModel>();
        }

        #region Backing Fields
        private ObservableCollection<HCodeBlockViewModel> _hcodesCollection;

        

        #endregion
        public ObservableCollection<HCodeBlockViewModel> HCodesCollection
        {
            get { return _hcodesCollection; }
            set { SetProperty(ref _hcodesCollection, value); }
        }

        private void MatrixRecieved(byte[,] matrix)
        {

        }
    }
}
