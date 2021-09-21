using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace super_duper_octo_palm_tree.app.services
{
    public class CurrencyService : BackgroundService
    {
        private readonly SharedCurrencyService sharedCurrencyService;
        HttpClient _httpClient;

        public CurrencyService(SharedCurrencyService sharedCurrencyService)
        {
            _httpClient = new HttpClient();
            this.sharedCurrencyService = sharedCurrencyService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int failedCount = 0;
            while (true)
            {
                bool failed = false;
                try
                {
                    // poll xml
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
                    request.Headers.Add(HttpRequestHeader.Accept.ToString(), "*/*");
                    var response = await _httpClient.SendAsync(request);
                    //parse xml
                    if (response.IsSuccessStatusCode)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(await response.Content.ReadAsStringAsync());
                        var t = double.Parse(doc.GetElementsByTagName("Cube").Cast<XmlNode>()
                            .Where(x => x.Attributes.Count >= 2 && x.Attributes.GetNamedItem("currency").Value == "USD")
                            .Select(x => x.Attributes.GetNamedItem("rate").Value)
                            .First(), CultureInfo.InvariantCulture);

                        if(t == sharedCurrencyService._currency)
                        {
                            failedCount++;
                            failed = true;
                        }
                        else
                        {
                            sharedCurrencyService._currency = t;
                            failedCount = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (!failed || (failed && failedCount >= 4))
                {
                    failedCount = 0;
                    // Wait for tomorrow
                    var delay = DateTime.Now.ToUniversalTime().AddDays(1).Date.AddHours(14) - DateTime.Now;
                    await Task.Delay(delay);
                }
                else
                {
                    await Task.Delay(TimeSpan.FromMinutes(15));
                }
            }
        }
    }
}
