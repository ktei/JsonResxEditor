﻿using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rui.JsonResxEditor.Shared;
using Rui.JsonResxEditor.Models;

namespace Rui.JsonResxEditor.ViewModels
{
    public class ProjectViewModel : Screen
    {
        string _name;
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
                Description = this.Description
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
