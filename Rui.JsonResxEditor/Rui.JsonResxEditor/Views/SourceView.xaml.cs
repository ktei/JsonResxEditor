﻿using Rui.JsonResxEditor.ViewModels;
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
    /// Interaction logic for SourceView.xaml
    /// </summary>
    public partial class SourceView : Window
    {
        public SourceView()
        {
            InitializeComponent();
            this.Loaded += SourceView_Loaded;
            this.Unloaded += SourceView_Unloaded;
            this.KeyDown += SourceView_KeyDown;
        }

        void SourceView_KeyDown(object sender, KeyEventArgs e)
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

        void SourceView_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.FocusAndSelectAll();
            (this.DataContext as SourceViewModel).RequestClose += SourceView_RequestClose;
        }

        void SourceView_Unloaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as SourceViewModel).RequestClose -= SourceView_RequestClose;
        }


        void SourceView_RequestClose(object sender, EventArgs e)
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
