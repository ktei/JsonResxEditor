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
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : Window
    {
        public ProjectView()
        {
            InitializeComponent();
            this.Loaded += ProjectView_Loaded;
            this.Unloaded += ProjectView_Unloaded;
        }

        void ProjectView_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus();
            (this.DataContext as ProjectViewModel).RequestClose += ProjectView_RequestClose;
        }

        void ProjectView_Unloaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ProjectViewModel).RequestClose -= ProjectView_RequestClose;
        }

        void ProjectView_RequestClose(object sender, EventArgs e)
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
