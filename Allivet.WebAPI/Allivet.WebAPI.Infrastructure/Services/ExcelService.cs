using Allivet.WebAPI.Infrastructure.Common.Interfaces;
using LargeXlsx;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Infrastructure.Services
{
   public class ExcelService : IExcelService
    {
        private readonly IConfiguration _configuration;
        public ExcelService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public byte[] GetDataExcelFileBytes<TEntity>(List<TEntity> data, List<string> columns, string fileName)
        {
            string fileServerPhysical = _configuration["FileServerPhysical"];
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            var cdnDirectory = new DirectoryInfo(
                currentDirectory.FullName +
                fileServerPhysical.Replace("{ProjectName}", currentDirectory.Name));

            var outputFilePath = this.OutputFile<TEntity>(data, currentDirectory, columns, fileName);
            byte[] fileBytes = File.ReadAllBytes(outputFilePath);
            if (File.Exists(outputFilePath)) // Delete Generated Test Files
            {
                File.Delete(outputFilePath);
            }
            return fileBytes;
        }

        private string OutputFile<TEntity>(List<TEntity> data,
            DirectoryInfo cdnDirectory
            , List<string> columns
            , string fileName)
        {

            var outputFilePath = $"{fileName}.xlsx";

            using var stream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            using var xlsxWriter = new XlsxWriter(stream);

            xlsxWriter.BeginWorksheet("Sheet 1").BeginRow();

            foreach (var column in columns) 
            {
                xlsxWriter.Write(column);
            }

            foreach (var x in data)
            {
                xlsxWriter.BeginRow();

                foreach (var column in columns)
                {
                    var text = x.GetType().GetProperty(column).GetValue(x, null);
                    if(text == null) xlsxWriter.Write("");
                    else xlsxWriter.Write(text.ToString());
                }
            }

            return outputFilePath;
        }
    }
}
