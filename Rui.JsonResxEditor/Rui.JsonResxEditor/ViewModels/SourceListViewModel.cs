using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export]
    public class SourceListViewModel : Screen, IHandle<OpenProjectMessage>
    {
        private ObservableCollection<Source> _sourceList;
        private ObservableCollection<Item> _itemList;
        private Source _activeSource;

        public SourceListViewModel()
        {
            DisplayName = "Sources";
            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        [Import]
        public ISourceService SourceService { get; set; }

        [Import]
        public IItemService ItemService { get; set; }

        public Source ActiveSource
        {
            get { return _activeSource; }
            set
            {
                if (_activeSource != value)
                {
                    ActivateSource(value);
                }
            }
        }

        public IEnumerable<Source> SourceList
        {
            get
            {
                if (_sourceList == null)
                {
                    LoadSourceList();
                }
                return _sourceList;
            }
        }

        public IEnumerable<Item> ItemList
        {
            get
            {
                if (_itemList == null)
                {
                    LoadItemList();
                }
                return _itemList;
            }
        }

        public void CreateSource()
        {
            var model = new SourceViewModel();
            IoC.Get<IWindowManager>().ShowDialog(model);
            if (model.Id > 0)
            {
                _sourceList.Add(new Source() { Id = model.Id, Name = model.Name, ProjectId = IoC.Get<IShell>().ActiveProject.Id });
            }
        }

        public void Handle(OpenProjectMessage message)
        {
            LoadSourceList();
            NotifyOfPropertyChange(() => SourceList);
        }

        void LoadSourceList()
        {
            _sourceList = new ObservableCollection<Source>(SourceService.FindAll(IoC.Get<IShell>().ActiveProject.Id));
        }

        void ActivateSource(Source source)
        {
            _activeSource = source;
            LoadItemList();
            NotifyOfPropertyChange(() => ItemList);
        }

        void LoadItemList()
        {
            if (ActiveSource == null)
            {
                _itemList = null;
            }
            else
            {
                _itemList = new ObservableCollection<Item>(ItemService.FindAll(ActiveSource.Id));
            }
        }
    }
}
