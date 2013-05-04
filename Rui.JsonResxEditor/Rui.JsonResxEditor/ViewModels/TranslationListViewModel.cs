using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace Rui.JsonResxEditor.ViewModels
{
    [Export]
    public class TranslationListViewModel : Screen, IHandle<OpenProjectMessage>
    {
        private ObservableCollection<SourceViewModel> _sourceList;
        private ObservableCollection<TranslationViewModel> _translationList;
        private ObservableCollection<LocaleViewModel> _localeList;
        private SourceViewModel _activeSource;
        private TranslationViewModel _selectedTranslation;
        private string _selectedLocale;
        static readonly LocaleViewModel AddLocaleOption = new LocaleViewModel() { Code = "_add", Name = "Add locale..." };

        public TranslationListViewModel()
        {
            DisplayName = "Translations";
            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        [Import]
        public ITranslationService TranslationService { get; set; }

        [Import]
        public ISourceService SourceService { get; set; }

        [Import]
        public ILocaleService LocaleService { get; set; }

        [Import]
        public IItemService ItemService { get; set; }

        public TranslationViewModel SelectedTranslation
        {
            get { return _selectedTranslation; }
            set
            {
                if (_selectedTranslation != value)
                {
                    _selectedTranslation = value;
                    NotifyOfPropertyChange(() => SelectedTranslation);
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

        public IEnumerable<LocaleViewModel> LocaleList
        {
            get
            {
                if (_localeList == null)
                {
                    LoadLocaleList();
                }
                return _localeList;
            }
        }

        public IEnumerable<TranslationViewModel> TranslationList
        {
            get
            {
                if (_translationList == null)
                {
                    LoadTranslationList();
                }
                return _translationList;
            }
        }

        public string SelectedLocale
        {
            get { return _selectedLocale; }
            set
            {
                var previousLocale = _selectedLocale;
                if (_selectedLocale != value)
                {
                    if (SelectedTranslation != null)
                    {
                        SelectedTranslation.Save();
                    }
                    _selectedLocale = value;
                    NotifyOfPropertyChange(() => SelectedLocale);
                    if (_selectedLocale == "_add")
                    {
                        AddLocale();
                        SelectedLocale = previousLocale;
                    }
                    else
                    {
                        LoadTranslationList();
                        NotifyOfPropertyChange(() => TranslationList);
                    }
                }
            }
        }

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
                    _translationList.Insert(0, new TranslationViewModel(model));
                }
            }
        }

        public void DeleteItems(IList selectedItems)
        {
            if (selectedItems != null && selectedItems.Count > 0 && MessageBoxSupport.Confirm("Are you sure you want to delete selected item?"))
            {
                List<TranslationViewModel> translations = new List<TranslationViewModel>();
                foreach (TranslationViewModel t in selectedItems)
                {
                    translations.Add(t);
                }
                ItemService.Delete(translations.Select(x => x.ItemId));
                foreach (var t in translations)
                {
                    _translationList.Remove(t);
                }
            }
        }

        public void Handle(OpenProjectMessage message)
        {
            LoadLocaleList();
            LoadSourceList();
            LoadTranslationList();
        }

        public void SaveTranslation()
        {
            if (SelectedTranslation != null)
            {
                SelectedTranslation.Save();
            }
        }

        void LoadTranslationList()
        {
            if (ActiveSource != null)
            {
                _translationList = new ObservableCollection<TranslationViewModel>(
                    TranslationService.FindAll(ActiveSource.Id, SelectedLocale).Select(x => new TranslationViewModel(x)));
            }
            else
            {
                _translationList = null;
            }
        }

        void LoadLocaleList()
        {
            var activeProjectId = IoC.Get<IShell>().ActiveProjectId;
            if (activeProjectId != null)
            {
                _localeList = new ObservableCollection<LocaleViewModel>(
                    LocaleService.FindAll(activeProjectId.Value).Select(x => new LocaleViewModel(x)));
                _localeList.Add(AddLocaleOption);
                NotifyOfPropertyChange(() => LocaleList);
                if (_localeList.Count > 0)
                {
                    SelectedLocale = _localeList[0].Code;
                }
            }
        }

        void ActivateSource(SourceViewModel source)
        {
            _activeSource = source;
            LoadTranslationList();
            NotifyOfPropertyChange(() => TranslationList);
        }

        void LoadSourceList()
        {
            var activeProjectId = IoC.Get<IShell>().ActiveProjectId;
            if (activeProjectId != null)
            {
                _sourceList = new ObservableCollection<SourceViewModel>(
                    SourceService.FindAll(activeProjectId.Value).Select(x => new SourceViewModel(x)));
                NotifyOfPropertyChange(() => SourceList);
            }
        }

        void AddLocale()
        {
            var model = new AddLocaleViewModel();
            IoC.Get<IWindowManager>().ShowDialog(model);
            if (model.IsSaved)
            {
                _localeList.Insert(_localeList.Count - 1,
                    new LocaleViewModel() { Code = model.Code, Name = model.Name });
            }
        }
    }
}
