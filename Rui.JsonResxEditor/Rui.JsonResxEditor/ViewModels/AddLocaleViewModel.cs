using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rui.JsonResxEditor.Shared;
using Rui.JsonResxEditor.Infrasructure;
using System.ComponentModel.Composition;
using Rui.JsonResxEditor.Models;

namespace Rui.JsonResxEditor.ViewModels
{
    public class AddLocaleViewModel : Screen
    {
        public AddLocaleViewModel()
        {
            DisplayName = "Locale properties";
            this.SatisfyImports();
        }

        [Import]
        public ILocaleService LocaleService { get; set; }

        public event EventHandler RequestClose;

        public bool IsSaved { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public void Save()
        {
            if (!Validate())
            {
                return;
            }
            var model = new Locale() { DisplayName = Name, Code = Code, ProjectId = IoC.Get<IShell>().ActiveProjectId.Value };
            LocaleService.Insert(model);
            IsSaved = true;
            OnRequestClose();
        }

        bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBoxSupport.ShowError("Please provide a locale name.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Code))
            {
                MessageBoxSupport.ShowError("Please provide a locale code.");
                return false;
            }
            if (LocaleService.Exists(Code, IoC.Get<IShell>().ActiveProjectId.Value))
            {
                MessageBoxSupport.ShowError("Locale already exists: " + Code);
                return false;
            }

            return true;
        }

        void OnRequestClose()
        {
            if (RequestClose != null)
            {
                RequestClose(this, EventArgs.Empty);
            }
        }
    }
}
