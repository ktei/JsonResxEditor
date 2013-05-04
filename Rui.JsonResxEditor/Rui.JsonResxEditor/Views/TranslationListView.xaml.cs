using Rui.JsonResxEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rui.JsonResxEditor.Views
{
    /// <summary>
    /// Interaction logic for TranslationsView.xaml
    /// </summary>
    public partial class TranslationListView : UserControl
    {
        public TranslationListView()
        {
            InitializeComponent();
        }

        private void TranslationTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
             
        }

        private void SourceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
