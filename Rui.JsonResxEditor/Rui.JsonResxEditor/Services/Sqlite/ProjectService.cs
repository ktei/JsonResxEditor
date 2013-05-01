using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
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
    }
}
