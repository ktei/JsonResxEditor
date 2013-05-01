using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        public ShellViewModel()
        {
            DisplayName = "Json Resource Editor";
        }

        [Import]
        public TranslationsViewModel Translations { get; set; }

        [Import]
        public SourcesViewModel Sources { get; set; }

        protected override void OnViewLoaded(object view)
        {
            Items.Add(Sources);
            Items.Add(Translations);
            ActivateItem(Sources);
        }

        public void NewProject()
        {
            IoC.Get<IWindowManager>().ShowDialog(new ProjectViewModel());
        }

        public void OpenProject()
        {
            var model = new OpenProjectViewModel();
            IoC.Get<IWindowManager>().ShowDialog(model);
            var selectedProject = model.SelectedProject;
            if (selectedProject != null)
            {
                IoC.Get<IEventAggregator>().Publish(new OpenProjectMessage(selectedProject.Id));
            }
        }
    }
}
