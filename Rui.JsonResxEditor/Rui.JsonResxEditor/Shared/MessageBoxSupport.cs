﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rui.JsonResxEditor.Shared
{
    public static class MessageBoxSupport
    {
        public static void ShowError(string error)
        {
            MessageBox.Show(error, "Error - JsonResxEditor", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
