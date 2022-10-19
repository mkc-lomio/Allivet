using Allivet.WebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Domain.Entities
{
   public class VeterinaryLocation : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }
}
