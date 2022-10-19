using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Infrastructure.Common.Interfaces
{
    public interface IExcelService
    {
        public byte[] GetDataExcelFileBytes<TEntity>(List<TEntity> data, List<string> columns, string fileName);
    }
}
