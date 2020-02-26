
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using api.Entities;

namespace api.Models
{
    /// <summary>
    /// Helper for mapping between entities and view models (DTOs)
    /// </summary>
    static class DTOMapper
    {
        internal static FolderDTO MapToDTO(this Folder folder, IUrlHelper helper){
            var dto = new FolderDTO {
                FolderId = folder.Id,
                FolderUrl = helper.ActionLink("GetFolder", "Folders", new { folderId = folder.Id }),
                FolderName = folder.FolderName,
                FolderStatus = folder.FolderStatus,
                OwnerId = folder.OwnerId,
                Cases = folder.Cases.Select(c=>c.MapToDTO(helper)).ToList()
            };

            return dto;
        }

        internal static Folder MapToEntity(this FolderForCreationDTO dto, string userId)
        {
            var folder = new Folder {
                FolderName = dto.FolderName,
                FolderStatus = dto.FolderStatus,
                OwnerId = userId
            };

            if(dto.Cases!=null) {
                dto.Cases.ForEach(caseId => folder.Cases.Add(new LegalCase { CaseId = Int64.Parse(caseId) }));
            }

            return folder;
        }

        public static void ApplyToEntity(this FolderForUpdateDTO dto, Folder folder)
        {
            folder.FolderName = dto.FolderName;
            folder.FolderStatus = dto.FolderStatus;
        }

        internal static LegalCaseDTO MapToDTO(this LegalCase legalCase, IUrlHelper helper){
            var dto = new LegalCaseDTO {
                CaseId = legalCase.CaseId.ToString(),
                CaseUrl = helper.ActionLink("DeleteLegalCase", "Cases", new { folderId = legalCase.FolderId, caseId= legalCase.CaseId })
            };

            return dto;
        }

        internal static LegalCase MapToEntity(this LegalCaseDTO dto)
        {
            return new LegalCase { CaseId = Int64.Parse(dto.CaseId) };
        }
    }
}