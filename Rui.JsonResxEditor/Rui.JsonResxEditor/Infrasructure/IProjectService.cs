﻿using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Infrasructure
{
    public interface IProjectService
    {
        void Save(Project model);
        IEnumerable<Project> FindAll();
    }
}