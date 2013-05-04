using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Automation.Provider;

namespace Rui.JsonResxEditor.Views
{
    public static class ControlExtensions
    {
        public static void AutomationPeerInvoke(this Button button)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProv = (IInvokeProvider)peer.GetPattern(PatternInterface.Invoke);
            invokeProv.Invoke();
        }

        public static void FocusAndSelectAll(this TextBox textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
        }
    }
}
