using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class NumberService
    {
        private IConfiguration _configuration;
        public NumberService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Number> GetNumber()
        {
            try
            {
                string addressURL = _configuration.GetSection("addressURL").Value;
                HttpClient client = new HttpClient();
                HttpResponseMessage reponse = await client.GetAsync(addressURL);

                Number number;
                if (reponse.IsSuccessStatusCode)
                {
                    string responsenumber = await reponse.Content.ReadAsStringAsync();
                    number = JsonConvert.DeserializeObject<Number>(responsenumber);
                }
                else
                {
                    string errorMessage = "El servidor de números tuvo problemas";
                    throw new NumberServiceNotFoundException(errorMessage);
                }
                return number;
            }
            catch (Exception ex)
            {
                string errorMessage = "El servidor de números paso por problemas inesperados";
                throw new NumberServiceException(errorMessage);
            }
            finally
            {

            }
            
        }
    }
}