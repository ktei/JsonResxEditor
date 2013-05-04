using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Models
{
    public class TranslationItem
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int ItemId { get; set; }
        public string Token { get; set; }
        public string ItemText { get; set; }
        public string Text { get; set; }
    }
}
