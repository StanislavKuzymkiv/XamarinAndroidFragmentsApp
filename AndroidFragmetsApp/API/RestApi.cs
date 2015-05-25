using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics;
using Org.Json;
using Android.Util;


namespace AndroidFragmetsApp
{
    public class RestApi
    {
        public RestApi()
        {
        }
        public async Task<String> SaveImageData(Photo photo)
        {

            try
            {              
                string Url = "http://10.0.3.2:7793/api/values/";
                var jsonString = SerializeToJson(photo);
                return null;
                HttpClientHandler objch = new HttpClientHandler(){ 
                    UseDefaultCredentials=true};
                using (var client = new HttpClient(objch))
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("API key", "Key");
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    using (                     
                        var message =
                        await client.PostAsync(Url, content))
                    {
                        var input = await message.Content.ReadAsStringAsync();                           
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        private string SerializeToJson(Photo photo){
            var fileName =App._file.Name;
            byte[] byteArray = File.ReadAllBytes(App._file.Path);
            var stringBytes = Encoding.UTF8.GetString(byteArray);
            var jsonObj = new JSONObject();

            jsonObj.Put("photoName",fileName);
            jsonObj.Put("photoData", stringBytes);
            jsonObj.Put("photoComment",photo.Comment);
            return jsonObj.ToString();
        }
       
    }
}

