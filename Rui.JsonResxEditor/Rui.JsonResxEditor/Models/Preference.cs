using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Models
{
    public class Preference
    {
        public const string DEFAULT_PROJECT_ID = "DefaultProjectId";

        [Indexed]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
