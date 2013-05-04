using Caliburn.Micro;
using Rui.JsonResxEditor.Infrasructure;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Shared;
using System.ComponentModel.Composition;

namespace Rui.JsonResxEditor.ViewModels
{
    public class TranslationViewModel : PropertyChangedBase
    {
        private string _token;
        private string _text;
        private string _itemText;
        private bool _isItemTextChanged;
        private bool _isTextChanged;

        public TranslationViewModel(ItemViewModel item)
        {
            ItemId = item.Id;
            Token = item.Token;
            ItemText = item.Text;
            SourceId = item.SourceId;
            this.SatisfyImports();
        }

        public TranslationViewModel(TranslationItem translationItem)
        {
            ItemId = translationItem.ItemId;
            Token = translationItem.Token;
            ItemText = translationItem.ItemText;
            Id = translationItem.Id;
            SourceId = translationItem.SourceId;
            Text = translationItem.Text;
            this.SatisfyImports();
        }

        [Import]
        public IItemService ItemService { get; set; }

        [Import]
        public ITranslationService TranslationService { get; set; }

        public int Id { get; set; }

        public int ItemId { get; set; }

        public int SourceId { get; set; }

        public string Token
        {
            get { return _token; }
            set
            {
                if (_token != value)
                {
                    _token = value;
                    NotifyOfPropertyChange(() => Token);
                }
            }
        }

        public string ItemText
        {
            get { return _itemText; }
            set
            {
                if (_itemText != value)
                {
                    _itemText = value;
                    _isItemTextChanged = true;
                    NotifyOfPropertyChange(() => ItemText);
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    _isTextChanged = true;
                    NotifyOfPropertyChange(() => Text);
                }
            }
        }

        public void Save()
        {
            if (_isTextChanged)
            {
                if (string.IsNullOrEmpty(Text))
                {
                    TranslationService.Delete(Id);
                }
                else
                {
                    TranslationService.Save(CreateTranslation());
                }
                _isTextChanged = false;
            }
            if (_isItemTextChanged)
            {
                ItemService.UpdateText(ItemText, ItemId);
                _isItemTextChanged = false;
            }
        }

        Translation CreateTranslation()
        {
            return new Translation()
            {
                Id = this.Id,
                ItemId = this.ItemId,
                Locale = IoC.Get<IShell>().ActiveLocale,
                ProjectId = IoC.Get<IShell>().ActiveProjectId.Value,
                SourceId = this.SourceId,
                Text = this.Text
            };
        }
    }
}
