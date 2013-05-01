using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.ViewModels
{
    public class OpenProjectMessage
    {
        public OpenProjectMessage(int projectId)
        {
            ProjectId = projectId;
        }

        public int ProjectId { get; private set; }
    }
}
