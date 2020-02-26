using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using api.Entities;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly BackendContext _context;
        private readonly ILogger<FoldersController> _logger;

        public FoldersController(BackendContext context, ILogger<FoldersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Folders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolderDTO>>> GetFolders([FromQuery]string user)
        {
            var folders = await _context.Folders.Include(l => l.Cases).ToListAsync();

            var folderDTOs = folders.Select(folder => folder.MapToDTO(Url)).ToList();

            return folderDTOs;
        }

        // GET: api/Folders/5
        [HttpGet("{folderId}")]
        public async Task<ActionResult<FolderDTO>> GetFolder(long folderId)
        {
            var folder = await _context.Folders.Include(f => f.Cases).FirstOrDefaultAsync(f => f.Id == folderId);

            if (folder == null)
            {
                return NotFound();
            }

            return folder.MapToDTO(Url);
        }

        // PUT: api/Folders/5
        /// <summary>
        /// Update a folder. Leaves contained cases unchanged.
        /// </summary>
        [HttpPut("{folderId}")]
        public async Task<IActionResult> PutFolder(long folderId, FolderForUpdateDTO folderDTO)
        {
            var folder = await _context.Folders.FindAsync(folderId);

            if (folder == null)
            {
                return NotFound();
            }

            folderDTO.ApplyToEntity(folder);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Folders
        [HttpPost]
        public async Task<ActionResult<FolderDTO>> PostFolder(FolderForCreationDTO folderDTO)
        {
            var folder = folderDTO.MapToEntity("userId");

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolder", new { folderId = folder.Id }, folder.MapToDTO(Url));
        }

        // DELETE: api/Folders/5
        [HttpDelete("{folderId}")]
        public async Task<IActionResult> DeleteFolder(long folderId)
        {
            var folder = await _context.Folders.FindAsync(folderId);
            if (folder == null)
            {
                return NotFound();
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FolderExists(long id)
        {
            return _context.Folders.Any(e => e.Id == id);
        }
    }
}
