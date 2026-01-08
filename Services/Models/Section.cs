using System;
using System.Collections.Generic;

namespace KnowledgeBase.Client.Services.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentSectionId { get; set; }
        public List<Section> ChildSections { get; set; } = new List<Section>();
    }

    public class CreateSectionDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentSectionId { get; set; }
    }

    public class UpdateSectionDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentSectionId { get; set; }
    }
}