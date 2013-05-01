using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Rui.JsonResxEditor.Shared
{
    public static class DispatcherHelper
    {
        static Dispatcher _dispatcher;

        public static void Initialize(Dispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }
            _dispatcher = dispatcher;
        }

        public static void BeginInvoke(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (_dispatcher == null)
            {
                throw new InvalidOperationException("Dispatcher is not initalized yet!");
            }
            _dispatcher.BeginInvoke((Action)delegate { action(); });
        }
    }
}
