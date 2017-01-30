using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace addon.ClientHelper
{
    public class ClientConnect
    {
        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://localhost:5744/Api/AppLicenses";

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public ClientConnect()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<AppLicViewModel>> VerifyLicense()
        {
        
            
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/K01");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                List<AppLicViewModel> lic = JsonConvert.DeserializeObject<List<AppLicViewModel>>(responseData);
                //var lic = JsonConvert.DeserializeObject<AppLicViewModel>(responseData);

                return lic;


            }
            return null;
            
        }

    }
    public class AppLicViewModel
    {
        [JsonProperty("AppLicenseKey")]
        public string AppLicenseKey { get; set; }

        [JsonProperty("HardwareInfo")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public IEnumerable<String> HardwareInfo { get; set; }

    }



    class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<T> list = (List<T>)value;
            if (list.Count == 1)
            {
                value = list[0];
            }
            serializer.Serialize(writer, value);
        }
    }
}

