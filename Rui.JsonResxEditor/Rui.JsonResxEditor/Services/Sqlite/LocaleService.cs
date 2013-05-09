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
    [Export(typeof(ILocaleService))]
    public class LocaleService : ILocaleService
    {
        public void Insert(Locale model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Insert(model);
            }
        }

        public IEnumerable<Locale> FindAll(int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Locale>("select * from Locale where ProjectId = ? order by DisplayName", projectId);
            }
        }


        public bool Exists(string code, int projectId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.ExecuteScalar<int>("select count(Code) from Locale where ProjectId = ? and Code = ?", projectId, code) > 0;
            }
        }
    }
}
