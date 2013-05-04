using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Models
{
    public class Locale
    {
        [Indexed]
        public int ProjectId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }
    }
}
