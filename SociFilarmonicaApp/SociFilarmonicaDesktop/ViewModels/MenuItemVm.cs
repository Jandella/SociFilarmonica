using System;
using System.Collections.Generic;
using System.Text;

namespace SociFilarmonicaDesktop.ViewModels
{
    public class MenuItemVm : ObservableVm
    {
        private string _itemName;
        public string ItemName
        {
            get => _itemName;
            set => Set(() => ItemName, ref _itemName, value);
        }

        private object _content;
        public object Content
        {
            get => _content;
            set => Set(() => Content, ref _content, value);
        }
    }
}
