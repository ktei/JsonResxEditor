using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Infrasructure
{
    public interface ITranslationService
    {
        void Save(Translation model);
        IEnumerable<TranslationItem> FindAll(int sourceId, string locale);
        void Delete(int translationId);
    }
}
