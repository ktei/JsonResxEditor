using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.ViewModels
{
    public class SourceViewModel : Screen
    {
        string _name;

        public SourceViewModel()
        {
            DisplayName = "Source properties";
            this.SatisfyImports();
        }

        public SourceViewModel(Source source)
            : this()
        {
            Id = source.Id;
            Name = source.Name;
        }

        public event EventHandler RequestClose;

        [Import]
        public ISourceService SourceService { get; set; }

        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }

        public void Save()
        {
            if (!Validate())
            {
                return;
            }
            try
            {
                var model = CreateModel();
                SourceService.Save(model);
                Id = model.Id;
                OnRequestClose();
            }
            catch (Exception ex)
            {
                MessageBoxSupport.ShowError(ex.Message);
            }
        }

        Source CreateModel()
        {
            return new Source()
            {
                Id = this.Id,
                Name = this.Name,
                ProjectId = IoC.Get<IShell>().ActiveProjectId.Value
            };
        }

        bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBoxSupport.ShowError("Please provide a source name.");
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
