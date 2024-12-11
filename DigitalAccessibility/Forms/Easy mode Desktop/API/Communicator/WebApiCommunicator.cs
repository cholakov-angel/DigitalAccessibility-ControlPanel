using Easy_mode_Desktop.API.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Easy_mode_Desktop
{
    public static class WebApiCommunicator
    {
        static HttpClient client = new HttpClient();
        private static HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7247/") };

        public async static Task<BlindUserIdDTO> Activate(string license, string masterKey)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            httpClient = new HttpClient(handler);
            StringContent jsonContent = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    masterKey = masterKey,
                    licenseKey = license,
                    macAddress = GetMACAddress(),
                    dateTime = DateTime.Now
                }),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(
                "https://localhost:7247/Licence/ActivateLicense",
                jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Error occurred!");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BlindUserIdDTO>(await response.Content.ReadAsStringAsync());
            return model;
        } // Activate

        // Взимане на MAC адреса на мрежовата карта
        private static string GetMACAddress()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
        }  // GetMACAddress

        public async static Task<EmailDTO> GetEmail(string blindUserId, string administratorId)
        {
            client.BaseAddress = new Uri("https://localhost:7247/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("https://localhost:7247/BlindUser/GetEmail",
                new
                {
                    id = blindUserId,
                    administratorId = administratorId
                });

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Not found!");
            }
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Error occurred!");
            }

            string result = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<EmailDTO>(result);

            return model;
        } // GetEmail
    } // WebApiCommunicator
}
