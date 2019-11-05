using System.Windows.Controls;
using CW_Course3_MatrixHammingCipher.Pages.Settings;

namespace CW_Course3_MatrixHammingCipher.Views.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new AppearanceViewModel();
        }
    }
}
