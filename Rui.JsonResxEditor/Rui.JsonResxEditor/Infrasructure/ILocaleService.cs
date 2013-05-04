using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Infrasructure
{
    public interface ILocaleService
    {
        void Insert(Locale model);
        IEnumerable<Locale> FindAll(int projectId);
        bool Exists(string code, int projectId);
    }
}
