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
    /// Interaction logic for AddLocaleView.xaml
    /// </summary>
    public partial class AddLocaleView : Window
    {
        public AddLocaleView()
        {
            InitializeComponent();
            this.Loaded += AddLocaleView_Loaded;
            this.Unloaded += AddLocaleView_Unloaded;
            this.KeyDown += AddLocaleView_KeyDown;
        }

        void AddLocaleView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Save.AutomationPeerInvoke();
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        void AddLocaleView_Unloaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AddLocaleViewModel).RequestClose -= AddLocaleView_RequestClose;
        }

        void AddLocaleView_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.FocusAndSelectAll();
            (this.DataContext as AddLocaleViewModel).RequestClose += AddLocaleView_RequestClose;
        }

        void AddLocaleView_RequestClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
