using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.Infrasructure
{
    public interface IItemService
    {
        void Save(Item model);
        IEnumerable<Item> FindAll(int sourceId);
        void Delete(int itemId);
    }
}
