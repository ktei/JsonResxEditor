using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int SourceId { get; set; }

        [Indexed]
        public int ProjectId { get; set; }

        [Unique]
        public string Token { get; set; }

        public string Text { get; set; }
    }
}
