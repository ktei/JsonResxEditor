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
        public IEnumerable<Item> FindAll(int sourceId)
        {
            using (var db = new SQLite.SQLiteConnection(Settings.DatabasePath))
            {
                return db.Query<Item>("select * from Item where SourceId <> ?", sourceId);
            }
        }
    }
}
