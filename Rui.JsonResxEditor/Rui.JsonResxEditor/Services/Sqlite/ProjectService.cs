using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Rui.JsonResxEditor.Services.Sqlite
{
    [Export(typeof(IProjectService))]
    public class ProjectService : IProjectService
    {
        public void Save(Project model)
        {
            if (model.Id == 0)
            {
                Insert(model);
            }
            else
            {
                Update(model);
            }
        }


        public IEnumerable<Project> FindAll()
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                var enu = db.Table<Project>().GetEnumerator();
                while (enu.MoveNext())
                {
                    yield return enu.Current;
                }
            }
        }


        public IEnumerable<Project> FindAllExcept(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Project>("select * from Project where Id <> ?", projectId);
            }
        }


        public void Delete(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Execute("delete from Translation where ProjectId = ?", projectId);
                db.Execute("delete from Item where ProjectId = ?", projectId);
                db.Execute("delete from Source where ProjectId = ?", projectId);
                db.Execute("delete from Project where Id = ?", projectId);
            }
        }


        public Project Get(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Get<Project>(projectId);
            }
        }

        private void Insert(Project model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Insert(model);
            }
        }

        private void Update(Project model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Update(model);
            }
        }
    }
}
