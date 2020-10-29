using Movers.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ViewModel.Utility;

namespace Movers.Services
{
    public class DatabaseService
    {
        private readonly HttpServices client;
        //public const string BaseUrl = "http://192.168.1.102/MoversApi/";
        //public const string BaseUrl = "http://172.16.2.109/MoversApi/";
        //public const string BaseUrl = "http://185.15.244.235/HWRateApi/";
        public const string BaseUrl = "http://3.21.206.147/";

        public DatabaseService()
        {
            client = new HttpServices();
        }

        public async Task<Response> SendEmail(RevisionFormVM revisionFormVM)
        {
            Response response = new Response();

            try
            {
                response = JsonConvert.DeserializeObject<Response>(await client.PostAsync($"{BaseUrl}Email/SendEmail", revisionFormVM));
            }
            catch (Exception ex)
            {

            }

            return response;
        }

    }
}
