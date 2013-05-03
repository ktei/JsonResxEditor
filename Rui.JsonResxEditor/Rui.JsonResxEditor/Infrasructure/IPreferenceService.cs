using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Infrasructure
{
    public interface IPreferenceService
    {
        void Save(Preference model);
        IEnumerable<Preference> FindAll();
    }
}
