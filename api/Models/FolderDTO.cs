using System;
using System.Collections.Generic;
using api.Entities;

namespace api.Models
{
    public class FolderDTO
    {
        public long FolderId { get; set; }
        public string FolderUrl { get; set; }
        public string FolderName { get; set; }
        public FolderType FolderStatus { get; set; }
        public string OwnerId { get; set; }
        public List<LegalCaseDTO> Cases { get; set; } = new List<LegalCaseDTO>();
    }
}