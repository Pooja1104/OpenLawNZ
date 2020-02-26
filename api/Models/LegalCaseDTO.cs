using System;
using System.Collections.Generic;
using api.Entities;

namespace api.Models
{
    public class LegalCaseDTO
    {
        [CaseId]
        public string CaseId { get; set; }
        public string CaseUrl { get; set; }
    }
}