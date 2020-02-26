using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Folder
    {
        public long Id { get; set; }

        public string FolderName { get; set; }

        public FolderType FolderStatus { get; set; }

        public string OwnerId { get; set; }
        
        public ICollection<LegalCase> Cases { get; } = new List<LegalCase>();


    }
}
