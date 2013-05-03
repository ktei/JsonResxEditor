using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rui.JsonResxEditor.Shared;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Infrasructure;
using System.ComponentModel.Composition;

namespace Rui.JsonResxEditor.ViewModels
{
    public class ItemViewModel : Screen
    {
        string _token;
        string _text;

        public ItemViewModel(int sourceId)
        {
            DisplayName = "Item properties";
            this.SatisfyImports();
            SourceId = sourceId;
        }

        public ItemViewModel(Item item)
            : this(item.SourceId)
        {
            Id = item.Id;
            Token = item.Token;
            Text = item.Text;
        }

        [Import]
        public IItemService ItemService { get; set; }

        public event EventHandler RequestClose;

        public int Id { get; set; }

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

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    NotifyOfPropertyChange(() => Text);
                }
            }
        }

        public void Save()
        {
            if (!Validate())
            {
                return;
            }
            try
            {
                var model = CreateModel();
                ItemService.Save(model);
                Id = model.Id;
                OnRequestClose();
            }
            catch (Exception ex)
            {
                MessageBoxSupport.ShowError(ex.Message);
            }
        }

        Item CreateModel()
        {
            return new Item()
            {
                Id = this.Id,
                Token = this.Token,
                Text = this.Text,
                SourceId = this.SourceId,
                ProjectId = IoC.Get<IShell>().ActiveProjectId.Value
            };
        }

        bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Token))
            {
                MessageBoxSupport.ShowError("Please provide a token.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Text))
            {
                MessageBoxSupport.ShowError("Text cannot be empty.");
                return false;
            }
            return true;
        }

        void OnRequestClose()
        {
            if (RequestClose != null)
            {
                RequestClose(this, EventArgs.Empty);
            }
        }
    }
}
