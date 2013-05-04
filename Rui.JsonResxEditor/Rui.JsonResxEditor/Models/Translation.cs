using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Models
{
    public class Translation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int ItemId { get; set; }

        [Indexed]
        public int SourceId { get; set; }

        [Indexed]
        public int ProjectId { get; set; }

        [Indexed]
        public string Locale { get; set; }

        public string Text { get; set; }
    }
}
