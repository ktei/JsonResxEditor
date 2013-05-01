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
    public class TranslationsViewModel : Screen, IHandle<OpenProjectMessage>
    {
        public TranslationsViewModel()
        {
            DisplayName = "Translations";
            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        public void Handle(OpenProjectMessage message)
        {
            
        }
    }
}
