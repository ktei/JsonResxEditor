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
using Rui.JsonResxEditor.Shared;
using Rui.JsonResxEditor.ViewModels;
using Rui.JsonResxEditor.Models;

namespace Rui.JsonResxEditor.Views
{
    /// <summary>
    /// Interaction logic for SourcesView.xaml
    /// </summary>
    public partial class SourceListView : UserControl
    {
        public SourceListView()
        {
            InitializeComponent();
        }

        private void SourceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.DataContext as SourceListViewModel).ActiveSource = SourceList.SelectedItem as Source;
        }
    }
}
