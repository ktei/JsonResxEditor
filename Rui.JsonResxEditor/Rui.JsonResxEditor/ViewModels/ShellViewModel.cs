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
using System.Windows.Input;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        private Project _activeProject;

        public ShellViewModel()
        {
            DisplayName = "Json Resource Editor";
        }

        [Import]
        public TranslationListViewModel Translations { get; set; }

        [Import]
        public SourceListViewModel Sources { get; set; }

        [Import]
        public IProjectService ProjectService { get; set; }

        [Import]
        public IPreferenceService PreferenceService { get; set; }

        public ICommand OpenProjectCommand
        {
            get
            {
                return new RelayCommand(x => OpenProject());
            }
        }

        public ICommand CreateProjectCommand
        {
            get
            {
                return new RelayCommand(x => CreateProject());
            }
        }

        public Project ActiveProject
        {
            get { return _activeProject; }
            private set
            {
                if (_activeProject != value)
                {
                    _activeProject = value;
                    NotifyOfPropertyChange(() => ActiveProject);
                }
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            LoadDefaultProject();
        }

        protected override void OnViewLoaded(object view)
        {
            Items.Add(Sources);
            Items.Add(Translations);
            ActivateItem(Sources);
        }

        public void CreateProject()
        {
            var model = new ProjectViewModel();
            IoC.Get<IWindowManager>().ShowDialog(model);
            if (model.Id > 0 && ActiveProject == null)
            {
                ActivateProject(ProjectService.Get(model.Id));
            }
        }

        public void OpenProject()
        {
            var model = new OpenProjectViewModel();
            IoC.Get<IWindowManager>().ShowDialog(model);
            var selectedProject = model.SelectedProject;
            if (selectedProject != null)
            {
                ActivateProject(selectedProject);
            }
        }

        public void PersistPreference()
        {
            var projectPref = new Preference()
            {
                Name = Preference.DEFAULT_PROJECT_ID,
                Value = ActiveProject == null ? string.Empty : ActiveProject.Id.ToString(),
                WindowsUser = Environment.UserName
            };
            PreferenceService.Save(projectPref);
        }

        void LoadDefaultProject()
        {
            var prefs = PreferenceService.FindAll(Environment.UserName);
            var p = prefs.FirstOrDefault(x => x.Name == Preference.DEFAULT_PROJECT_ID);
            if (p == null)
            {
                return;
            }
            int defaultProjectId = 0;
            if (int.TryParse(p.Value, out defaultProjectId))
            {
                var project = ProjectService.Get(defaultProjectId);
                if (project != null)
                {
                    ActivateProject(project);
                }
            }
        }

        void ActivateProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }
            ActiveProject = project;
            IoC.Get<IEventAggregator>().Publish(new OpenProjectMessage(project.Id));
            DisplayName = string.Format("{0} - Json Resource Editor", project.Name);
        }



    }
}
