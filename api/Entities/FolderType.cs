using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public enum FolderType
    {
        Private = 1,
        Unlisted = 2,
        Listed = 3
    }
}
