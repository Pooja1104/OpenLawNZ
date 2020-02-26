using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using api.Entities;
using api.Models;
using System;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/Folders/{folderId}")]
    // [Authorize]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly BackendContext _context;
        private readonly ILogger<CasesController> _logger;

        public CasesController(BackendContext context, ILogger<CasesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Cases
        [HttpGet("~/api/Cases")]
        public async Task<ActionResult<IEnumerable<LegalCaseDTO>>> GetLegalCases([FromQuery]string user)
        {
            var cases = await _context.Cases.ToListAsync();
            var casesDTOs = cases.Select(c => c.MapToDTO(Url)).ToList();

            return casesDTOs;
        }

        // PUT: api/Folders/3/Cases/5
        /// <summary>
        /// Add a case to a folder
        /// </summary>
        [HttpPut("Cases/{caseId}")]
        public async Task<IActionResult> PutLegalCase(long folderId, string caseId)
        {
            if (!LegalCase.IsValidCaseId(caseId) || !FolderExists(folderId))
            {
                return NotFound();
            }

            var legalCase = new LegalCase { FolderId = folderId, CaseId = Int64.Parse(caseId) };

            _context.Cases.Add(legalCase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Folders/3/Cases/5
        /// <summary>
        /// Remove a case from a folder
        /// </summary>
        [HttpDelete("Cases/{caseId}")]
        public async Task<IActionResult> DeleteLegalCase(long folderId, string caseId)
        {
            if (!LegalCase.IsValidCaseId(caseId)) return NotFound();

            var legalCase = await _context.Cases.FirstOrDefaultAsync(c => c.FolderId == folderId && c.CaseId == Int64.Parse(caseId));
            if (legalCase == null)
            {
                return NotFound();
            }

            _context.Cases.Remove(legalCase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LegalCaseExists(long id)
        {
            return _context.Cases.Any(e => e.Id == id);
        }

        private bool FolderExists(long id)
        {
            return _context.Folders.Any(e => e.Id == id);
        }
    }
}
