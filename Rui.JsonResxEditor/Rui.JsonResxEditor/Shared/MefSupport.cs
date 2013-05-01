using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Rui.JsonResxEditor.Shared
{
    public static class MefSupport
    {
        public static Action<object> OnSatisfyImports { get; set; }

        public static void SatisfyImports(this object target)
        {
            if (OnSatisfyImports != null)
            {
                OnSatisfyImports(target);
            }
            else
            {
                throw new InvalidOperationException("OnSatisfyImports is null!");
            }
        }
    }
}
