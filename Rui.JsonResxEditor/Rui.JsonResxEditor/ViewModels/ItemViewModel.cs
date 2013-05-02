using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rui.JsonResxEditor.Shared;
using Rui.JsonResxEditor.Models;
using Rui.JsonResxEditor.Infrasructure;

namespace Rui.JsonResxEditor.ViewModels
{
    public class ItemViewModel : Screen
    {
        string _token;
        string _text;

        public ItemViewModel()
        {
            DisplayName = "Item properties";
            this.SatisfyImports();
        }

        public event EventHandler RequestClose;

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

        Item CreateItem()
        {
            return new Item()
            {
                Token = this.Token,
                Text = this.Text,
                ProjectId = IoC.Get<IShell>().ActiveProject.Id
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
