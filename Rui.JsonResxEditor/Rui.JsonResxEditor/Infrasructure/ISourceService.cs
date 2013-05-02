using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Infrasructure
{
    public interface ISourceService
    {
        void Save(Source model);
        IEnumerable<Source> FindAll(int projectId);
        void Delete(int sourceId);
    }
}
