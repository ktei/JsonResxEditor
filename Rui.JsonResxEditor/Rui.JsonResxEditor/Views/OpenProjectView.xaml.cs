using Rui.JsonResxEditor.Models;
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
    /// Interaction logic for OpenProjectView.xaml
    /// </summary>
    public partial class OpenProjectView : Window
    {
        OpenProjectViewModel _model;

        public OpenProjectView()
        {
            InitializeComponent();
            this.Loaded += OpenProjectView_Loaded;
        }

        void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _model.SelectedProject = ProjectList.SelectedItem as Project;
            this.Close();
        }

        void OpenProjectView_Loaded(object sender, RoutedEventArgs e)
        {
            _model = this.DataContext as OpenProjectViewModel;
            _model.RequestClose += _model_RequestClose;
            _model.GetSelectedProject = () =>
            {
                return ProjectList.SelectedItem as Project;
            };
        }

        void _model_RequestClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
