using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Services.Sqlite
{
    [Export(typeof(ISourceService))]
    public class SourceService : ISourceService
    {
        public void Save(Source model)
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

        private void Insert(Source model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Insert(model);
            }
        }

        private void Update(Source model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Update(model);
            }
        }

        public IEnumerable<Source> FindAll(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Source>("select * from Source where ProjectId = ?", projectId);
            }
        }

        public void Delete(int sourceId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Execute("delete from Translation where SourceId = ?", sourceId);
                db.Execute("delete from Source where Id = ?", sourceId);
            }
        }
    }
}
