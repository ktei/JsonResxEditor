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
    [Export(typeof(IPreferenceService))]
    public class PreferenceService : IPreferenceService
    {
        public void Save(Preference model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                var existingCount = db.ExecuteScalar<int>("select count(Name) from Preference where Name = ? and WindowsUser = ?",
                   model.Name, model.WindowsUser);
                if (existingCount == 0)
                {
                    db.Insert(model);
                }
                else
                {
                    db.Execute("update Preference set Value = ? where Name = ? and WindowsUser = ?",
                        model.Value, model.Name, model.WindowsUser);
                }
            }
        }

        public IEnumerable<Preference> FindAll(string windowsUser)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Preference>("select * from Preference where WindowsUser = ?", windowsUser);
            }
        }
    }
}
