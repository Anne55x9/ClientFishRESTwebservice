using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FishTrialBWebserviceASW.Model;
using Newtonsoft.Json;

namespace ClientFishRESTwebservice
{
    internal class RestClient
    {
        
        private const string uri = "http://restwebapplikation.azurewebsites.net/FishService.svc";


        public RestClient()
        {
        }


        public void Start()
        {
            var catcheslist = GetCatchesAsync().Result;
            Console.WriteLine("Alle fangster \n");

            foreach (Fangst c in catcheslist)
            {
                Console.WriteLine("Navn: " + c.Navn + " Art: " + "" + c.Art + "" + " Vægt: " + c.Veagt + " Sted: " + c.Sted + "" + " Uge: " + c.Uge);
            }

            //Console.WriteLine("Alle fangster\n" + String.Join("\n", catcheslist));


            var oneCatch = GetOneCatchAsync("1").Result;
            Console.WriteLine("En fangst nr" + 1 + "\n" + "Navn:" + oneCatch.Navn + "Art:" + oneCatch.Art + "Sted:" + oneCatch.Sted + "Vægt:" + oneCatch.Veagt + "Uge:" + oneCatch.Uge);


            //var deleteCatch = DeleteCatchAsync("4").Result;
            //Console.WriteLine("Fangst nr =" + 4 + " er slettet \n" + "Navn:" + deleteCatch.Navn + "Art:" + deleteCatch.Art + "Sted:" + deleteCatch.Sted + "Vægt:" + deleteCatch.Veagt + "Uge:" + deleteCatch.Uge);


            var catcheslistDelete = GetCatchesAsync().Result;
            Console.WriteLine("Alle fangster efter slet \n");

            foreach (Fangst c in catcheslistDelete)
            {
                Console.WriteLine("Navn: " + c.Navn + " Art: " + "" + c.Art + "" + " Vægt: " + c.Veagt + " Sted: " + c.Sted + "" + " Uge: " + c.Uge);
            }


            PostCatchAsync(new Fangst
            {
                Navn = "Lars",
                Art = "Torsk",
                Veagt = 66,
                Sted = "Halmi",
                Uge = 2
            });


            var catcheslistPost = GetCatchesAsync().Result;
            Console.WriteLine("Alle fangster efter post \n");

            foreach (Fangst c in catcheslistPost)
            {
                Console.WriteLine("Navn: " + c.Navn + " Art: " + "" + c.Art + "" + " Vægt: " + c.Veagt + " Sted: " + c.Sted + "" + " Uge: " + c.Uge);
            }

        }

       


            //Alle fangster
        private static async Task<IList<Fangst>> GetCatchesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/catches");
                IList<Fangst> cList = JsonConvert.DeserializeObject<IList<Fangst>>(content);
                return cList;
            }
        }

        //fangst vha id
        private static async Task<Fangst> GetOneCatchAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/catch/" + id);
                Fangst cList = JsonConvert.DeserializeObject<Fangst>(content);
                return cList;
            }
        }

        /// <summary>
        /// Slet fangst
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static async Task<Fangst> DeleteCatchAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = await client.DeleteAsync(uri + "/catches/" + id);
                Fangst c = JsonConvert.DeserializeObject<Fangst>(content.Content.ReadAsStringAsync().Result);
                return c;
            }
        }


        private static async Task<Fangst> PostCatchAsync(Fangst newFangst)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(newFangst));
                content.Headers.ContentType = new MediaTypeHeaderValue("Application/json");

                var result = await client.PostAsync(uri + "/catches", content);
                Fangst c = JsonConvert.DeserializeObject<Fangst>(result.Content.ReadAsStringAsync().Result);
                return c;
            }
        }

    }
}