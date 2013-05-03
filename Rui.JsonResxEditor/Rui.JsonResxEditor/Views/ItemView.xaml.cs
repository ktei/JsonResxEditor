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
using System.Windows.Shapes;

namespace Rui.JsonResxEditor.Views
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : Window
    {
        public ItemView()
        {
            InitializeComponent();
            this.Loaded += ItemView_Loaded;
            this.Unloaded += ItemView_Unloaded;
        }

        void ItemView_Unloaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ItemViewModel).RequestClose -= ItemView_RequestClose;
        }

        void ItemView_Loaded(object sender, RoutedEventArgs e)
        {
            TokenTextBox.Focus();
            (this.DataContext as ItemViewModel).RequestClose += ItemView_RequestClose;
        }

        void ItemView_RequestClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.BindingGroup.CommitEdit();
        }
    }
}
