using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIMuseos
{
    class Museo /*Contenido de la API https://museowebapp.azurewebsites.net/api/MuseosAPI */
    {
        public int Id { get; set; }
        public string Coordenadas { get; set; }
        public string Datacion { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
    }

    class Program
    {
        static async void leeMuseos()
        {
            using (var Clientes = new HttpClient())
            {
                List<Museo> ListaMuseos = new List<Museo>();
                string url = @"https://museowebapp.azurewebsites.net/api/MuseosAPI";
                var uri = new Uri(url);

                var response = await Clientes.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ListaMuseos = JsonConvert.DeserializeObject<List<Museo>>(content);

                    foreach (var mus in ListaMuseos)
                    {
                        Console.WriteLine(mus.Descripcion);
                    }
                }
            }
        }

        static async void leeMuseosVisitas()
        {
            using (var Clientes = new HttpClient())
            {
                string url = @"https://museowebapp.azurewebsites.net/api/MuseosAPI";
                var uri = new Uri(url);

                var response = await Clientes.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JArray Array = JArray.Parse(content);
                    var museosVisitados = from x in Array
                                  where (string)x["visita"] == "S"
                                  select x;

                    foreach(JToken mus in museosVisitados)
                    {
                        Console.WriteLine(mus.ToString());
                    }
                }

            }
        }

        static void Main (string[] args)
        {
            //leeMuseos();
            leeMuseosVisitas();
            Console.ReadLine();
        }
    }
}
