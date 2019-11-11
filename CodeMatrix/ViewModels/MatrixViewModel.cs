using Prism.Mvvm;

namespace CodeMatrix.ViewModels
{
    public class MatrixViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MatrixViewModel()
        {
            Message = "View CodeMatrix from your Prism Module";
        }
    }
}
