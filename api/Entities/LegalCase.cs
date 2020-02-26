using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class LegalCase
    {
        public long Id { get; set; }
        public long CaseId { get; set; }
        public long FolderId { get; set; }
        public Folder Folder { get; set; }

        
        /// <remarks>Insulates clients from changes to the CaseId format</remarks>
        internal static bool IsValidCaseId(string caseId){
            long n;
            return Int64.TryParse(caseId, out n);
        }
    }
}
