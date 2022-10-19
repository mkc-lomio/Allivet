using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Application.VeterinaryLocationManagement.Queries
{
   public class VeterinaryLocationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime DateModified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
