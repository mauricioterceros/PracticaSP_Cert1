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
                number = new Number();
            }
            return number;
        }
    }
}