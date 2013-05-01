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
    public class OpenProjectViewModel : Screen
    {
        private List<Project> _projectList;

        public OpenProjectViewModel()
        {
            DisplayName = "Choose a project to open";
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

        void LoadProjectList()
        {
            _projectList = ProjectService.FindAll().ToList();
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
