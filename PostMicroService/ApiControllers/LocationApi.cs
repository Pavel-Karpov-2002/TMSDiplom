using PostMicroService.DbStuff.Models;

namespace PostMicroService.ApiControllers
{
    public class LocationApi 
    {
        private HttpClient _httpClient;

        public LocationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<LocationPost?> GetLocationByIp(string ip)
        {
            return _httpClient.GetFromJsonAsync<LocationPost>(
                $"/json/{ip}");
        }
    }
}
