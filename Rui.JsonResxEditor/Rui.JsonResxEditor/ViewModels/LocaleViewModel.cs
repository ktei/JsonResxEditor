using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.ViewModels
{
    public class LocaleViewModel
    {
        public LocaleViewModel()
        {
        }

        public LocaleViewModel(Locale locale)
        {
            Name = locale.DisplayName;
            Code = locale.Code;
        }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}
