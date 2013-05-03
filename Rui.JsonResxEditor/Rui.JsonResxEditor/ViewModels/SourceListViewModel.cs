using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export]
    public class SourceListViewModel : Screen, IHandle<OpenProjectMessage>
    {
        private ObservableCollection<SourceViewModel> _sourceList;
        private ObservableCollection<ItemViewModel> _itemList;
        private SourceViewModel _activeSource;

        public SourceListViewModel()
        {
            DisplayName = "Sources";
            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        [Import]
        public ISourceService SourceService { get; set; }

        [Import]
        public IItemService ItemService { get; set; }

        public SourceViewModel ActiveSource
        {
            get { return _activeSource; }
            set
            {
                if (_activeSource != value)
                {
                    ActivateSource(value);
                    NotifyOfPropertyChange(() => ActiveSource);
                }
            }
        }

        public IEnumerable<SourceViewModel> SourceList
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

        public IEnumerable<ItemViewModel> ItemList
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

        public ItemViewModel ActiveItem { get; set; }

        public void CreateSource()
        {
            var model = new SourceViewModel();
            IoC.Get<IWindowManager>().ShowDialog(model);
            if (model.Id > 0)
            {
                _sourceList.Add(model);
            }
        }

        public void EditSource()
        {
            if (ActiveSource != null)
            {
                IoC.Get<IWindowManager>().ShowDialog(ActiveSource);
            }
        }

        public void DeleteSource()
        {
            if (ActiveSource != null)
            {
                if (MessageBoxSupport.Confirm("Are you sure you want to delete selected source?"))
                {
                    SourceService.Delete(ActiveSource.Id);
                    _sourceList.Remove(ActiveSource);
                    ActivateSource(null);
                }
            }
        }

        public void CreateItem()
        {
            if (ActiveSource != null)
            {
                var model = new ItemViewModel(ActiveSource.Id);
                IoC.Get<IWindowManager>().ShowDialog(model);
                if (model.Id > 0)
                {
                    _itemList.Add(model);
                }
            }
        }

        public void EditItem()
        {
            if (ActiveItem != null)
            {
                IoC.Get<IWindowManager>().ShowDialog(ActiveItem);
            }
        }

        public void DeleteItem()
        {
            if (ActiveItem != null)
            {
                if (MessageBoxSupport.Confirm("Are you sure you want to delete selected item?"))
                {
                    ItemService.Delete(ActiveItem.Id);
                    _itemList.Remove(ActiveItem);
                }
            }
        }

        public void Handle(OpenProjectMessage message)
        {
            LoadSourceList();
            NotifyOfPropertyChange(() => SourceList);
        }

        void LoadSourceList()
        {
            var activeProjectId = IoC.Get<IShell>().ActiveProjectId;
            if (activeProjectId != null)
            {
                _sourceList = new ObservableCollection<SourceViewModel>(
                    SourceService.FindAll(activeProjectId.Value).Select(x => new SourceViewModel(x)));
            }
        }

        void ActivateSource(SourceViewModel source)
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
                _itemList = new ObservableCollection<ItemViewModel>(ItemService.FindAll(ActiveSource.Id).Select(x => new ItemViewModel(x)));
            }
        }
    }
}
