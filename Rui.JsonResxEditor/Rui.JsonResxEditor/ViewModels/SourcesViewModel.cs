using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export]
    public class SourcesViewModel : Screen, IHandle<OpenProjectMessage>
    {
        public SourcesViewModel()
        {
            DisplayName = "Sources";
            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        public void Handle(OpenProjectMessage message)
        {
            
        }
    }
}
