using Caliburn.Micro;
using Newtonsoft.Json;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        private ProjectViewModel _activeProject;

        public ShellViewModel()
        {
            DisplayName = "Json Resource Editor";
        }

        [Import]
        public TranslationListViewModel Translations { get; set; }

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

        public string ActiveLocale
        {
            get
            {
                return Translations.SelectedLocale;
            }
        }

        public ProjectViewModel ActiveProject
        {
            get { return _activeProject; }
            private set
            {
                if (_activeProject != value)
                {
                    _activeProject = value;
                    _activeProject.PropertyChanged += (sender, e) =>
                        {
                            if (e.PropertyName == "Name")
                            {
                                DisplayName = string.Format("{0} - Json Resource Editor", _activeProject.Name);
                            }
                        };
                    NotifyOfPropertyChange(() => ActiveProject);
                }
            }
        }

        public int? ActiveProjectId
        {
            get
            {
                if (ActiveProject == null)
                {
                    return null;
                }
                return ActiveProject.Id;
            }
        }

        public void Generate()
        {
            try
            {
                ProjectService.WriteToFile(ActiveProject.Workspace, ActiveProject.Id);
                System.Diagnostics.Process.Start(ActiveProject.Workspace);
            }
            catch (Exception ex)
            {
                MessageBoxSupport.ShowError("Failed to generate localization files: " + ex.Message);
            }
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

        public void EditProject()
        {
            if (ActiveProject != null)
            {
                IoC.Get<IWindowManager>().ShowDialog(ActiveProject);
            }
        }

        public void PersistPreference()
        {
            var projectPref = new Preference()
            {
                Name = Preference.DEFAULT_PROJECT_ID,
                Value = ActiveProject == null ? string.Empty : ActiveProject.Id.ToString(),
            };
            PreferenceService.Save(projectPref);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            LoadDefaultProject();
        }

        protected override void OnViewLoaded(object view)
        {
            if (!ActiveProjectId.HasValue)
            {
                if (ProjectService.HasProject())
                {
                    OpenProject();
                }
                else
                {
                    CreateProject();
                }
            }
            ActivateItem(Translations);
        }

        void LoadDefaultProject()
        {
            var prefs = PreferenceService.FindAll();
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
            ActiveProject = new ProjectViewModel(project);
            IoC.Get<IEventAggregator>().Publish(new OpenProjectMessage(project.Id));
            DisplayName = string.Format("{0} - Json Resource Editor", project.Name);
        }



    }
}
