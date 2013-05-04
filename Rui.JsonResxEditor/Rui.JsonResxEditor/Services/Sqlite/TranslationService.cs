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
    [Export(typeof(ITranslationService))]
    public class TranslationService : ITranslationService
    {
        public void Save(Translation model)
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

        public IEnumerable<TranslationItem> FindAll(int sourceId, string locale)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<TranslationItem>(@"select i.Token, i.Id as ItemId, i.Text as ItemText, i.SourceId, t.Id, t.Text
                    from Item i left outer join Translation t on (i.Id = t.ItemId and t.Locale = ?)
                    where i.SourceId = ?", locale, sourceId);
            }
        }

        public void Delete(int translationId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Execute("delete from Translation where Id = ?", translationId);
            }
        }

        private void Insert(Translation model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Insert(model);
            }
        }

        private void Update(Translation model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Update(model);
            }
        }
    }
}
