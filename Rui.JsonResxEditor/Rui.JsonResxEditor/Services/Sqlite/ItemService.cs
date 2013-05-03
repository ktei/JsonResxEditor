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
    [Export(typeof(IItemService))]
    public class ItemService : IItemService
    {
        public void Save(Item model)
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

        public IEnumerable<Item> FindAll(int sourceId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Item>("select * from Item where SourceId = ?", sourceId);
            }
        }

        private void Insert(Item model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Insert(model);
            }
        }

        private void Update(Item model)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Update(model);
            }
        }


        public void Delete(int itemId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                db.Execute("delete from Item where Id = ?", itemId);
            }
        }
    }
}
