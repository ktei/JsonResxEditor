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
using System.Collections.ObjectModel;

namespace Rui.JsonResxEditor.ViewModels
{
    public class OpenProjectViewModel : Screen
    {
        private ObservableCollection<Project> _projectList;

        public OpenProjectViewModel()
        {
            DisplayName = "Choose a project";
            this.SatisfyImports();
        }

        [Import]
        public IProjectService ProjectService { get; set; }

        public Project SelectedProject { get; set; }

        public Func<Project> GetSelectedProject { get; set; }

        public event EventHandler RequestClose;

        public IEnumerable<Project> ProjectList
        {
            get
            {
                if (_projectList == null)
                {
                    LoadProjectList();
                }
                return _projectList;
            }
        }

        public void Open()
        {
            if (GetSelectedProject != null)
            {
                SelectedProject = GetSelectedProject();
            }
            OnRequestClose();
        }

        public bool CanOpen
        {
            get { return ProjectList.Any(); }
        }

        public void Delete()
        {
            if (GetSelectedProject == null)
            {
                return;
            }
            var projectId = GetSelectedProject().Id;
            if (MessageBoxSupport.Confirm("Are you sure you want to delete selected project?"))
            {
                ProjectService.Delete(projectId);
                _projectList.Remove(_projectList.Single(x => x.Id == projectId));
                NotifyOfPropertyChange(() => CanOpen);
            }
        }

        void LoadProjectList()
        {
            if (IoC.Get<IShell>().ActiveProjectId != null)
            {
                _projectList = new ObservableCollection<Project>(ProjectService.FindAllExcept(IoC.Get<IShell>().ActiveProjectId.Value));
            }
            else
            {
                _projectList = new ObservableCollection<Project>(ProjectService.FindAll());
            }
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
