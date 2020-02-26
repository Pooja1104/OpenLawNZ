using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using api.Entities;

namespace api.Models
{
    public class FolderForCreationDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string FolderName { get; set; }
        public FolderType FolderStatus { get; set; }
        
        [CaseId]
        public List<string> Cases { get; set; } = new List<string>();
    }
}