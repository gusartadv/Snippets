using Newtonsoft.Json;
using Snippets.Entities;
using Snippets.Interfaces;
using System.Net;

namespace Snippets.Services
{
    public class CountryService : ICountryService
    {
        public async Task<List<Root>> GetCountry()
        {
            List<Root> responseObject = new List<Root>();
            string url = "https://restcountries.com/v3.1/name/colombia";

            var handler = new HttpClientHandler();

            //Esta linea es la que te acepta los certificados, esto no debe ir en produccion
            //handler.ServerCertificateCustomValidationCallback = delegate { return true; };

            using (var httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        responseObject = JsonConvert.DeserializeObject<List<Root>>(responseString);
                    }
                    else
                    {

                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        var exceptionType = ex.GetType().ToString();

                    }
                }

            }

            return responseObject;
        }
    }
}
