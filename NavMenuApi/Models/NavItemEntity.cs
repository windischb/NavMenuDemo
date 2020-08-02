using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NavMenuApi
{
    public class NavItemEntity
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Image { get; set; }
        public string Href { get; set; }

        public Guid? Parent { get; set; }

        public ICollection<string> AllowedRoles { get; } = new Collection<string>();
    }
}