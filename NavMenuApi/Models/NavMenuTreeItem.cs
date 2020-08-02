using System;
using System.Collections.Generic;

namespace NavMenuApi
{
    public class NavMenuTreeItem
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public Guid? Parent { get; set; }
        public List<NavMenuTreeItem>? Children { get; set; }
        public string Image { get; set; }
        public string Href { get; set; }
    }
}