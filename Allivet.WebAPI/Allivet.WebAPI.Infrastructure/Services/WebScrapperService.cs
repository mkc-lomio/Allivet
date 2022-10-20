using Allivet.WebAPI.Domain.Entities;
using Allivet.WebAPI.Infrastructure.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace Allivet.WebAPI.Infrastructure.Services
{
    public class WebScrapperService : IWebScrapperService
    {
        public async Task<List<VeterinaryLocation>> GetDataInPetMedWebApp()
        {
            var url = "https://www.1800petmeds.com/vetdirectory?horizontalView=true";
            List<VeterinaryLocation> veterinaryLocations = new List<VeterinaryLocation>();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            var divs = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'store-details')]");
            foreach (HtmlNode div in divs)
            {
                var veterinaryLocation = new VeterinaryLocation();
                var textArray = div.InnerText.Split("\n");
                for (int i = 0; i <= textArray.Length - 1; i++)
                {
                    var text = textArray[i];
                    if (!string.IsNullOrEmpty(text))
                    {
                        if (i == 1)
                            veterinaryLocation.Name = text;
                        else if (Double.TryParse(text, out _) && text.Length == 10)
                            veterinaryLocation.ContactNumber = text;
                        else
                            veterinaryLocation.Address += string.Format(@"{0} ", text);
                    }
                }
                veterinaryLocations.Add(veterinaryLocation);
            }
            return veterinaryLocations;
        }
    }
}
