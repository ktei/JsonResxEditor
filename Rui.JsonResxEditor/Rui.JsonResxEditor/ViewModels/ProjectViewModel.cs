using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Forms;

namespace Rui.JsonResxEditor.ViewModels
{
    public class ProjectViewModel : Caliburn.Micro.Screen
    {
        string _name;
        string _workspace;
        string _description;

        public ProjectViewModel()
        {
            DisplayName = "Create a project";
            this.SatisfyImports();
        }

        public ProjectViewModel(Project project)
            : this()
        {
            DisplayName = "Project properties";
            Name = project.Name;
            Description = project.Description;
            Workspace = project.Workspace;
            Id = project.Id;
        }

        [Import]
        public IProjectService ProjectService { get; set; }

        [Import]
        public ILocaleService LocaleService { get; set; }

        public event EventHandler RequestClose;

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

        public string Workspace
        {
            get { return _workspace; }
            set
            {
                if (_workspace != value)
                {
                    _workspace = value;
                    NotifyOfPropertyChange(() => Workspace);
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyOfPropertyChange(() => Description);
                }
            }
        }

        public void ChooseWorkspace()
        {
            var dlg = new FolderBrowserDialog();
            dlg.Description = "Select workspace";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Workspace = dlg.SelectedPath;
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
                var project = CreateProject();
                ProjectService.Save(project);
                Id = project.Id;
                var locale = CreateLocale(project.Id);
                LocaleService.Insert(locale);
                OnRequestClose();
            }
            catch (Exception ex)
            {
                MessageBoxSupport.ShowError(ex.Message);
            }
        }

        Project CreateProject()
        {
            return new Project()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Workspace = this.Workspace
            };
        }

        Locale CreateLocale(int projectId)
        {
            return new Locale()
            {
                Code = "en",
                DisplayName = "English",
                ProjectId = projectId
            };
        }

        bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBoxSupport.ShowError("Please provide a project name.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Workspace))
            {
                MessageBoxSupport.ShowError("Please choose a workspace.");
                return false;
            }
            if (!Directory.Exists(Workspace))
            {
                MessageBoxSupport.ShowError("The directory for workspace does not exist.");
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
