using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VintriCore.Models;
using System.Net;

namespace VintriCore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BeerRatingController : ControllerBase
    {
        readonly IHttpClientFactory _clientFactory;
        readonly IHostingEnvironment _env;
        readonly IFileSystem _fileSystem;

        public BeerRatingController(IHttpClientFactory clientFactory, IHostingEnvironment env, IFileSystem fileSystem)
        {
            _clientFactory = clientFactory;
            _env = env;
            _fileSystem = fileSystem;
        }



        // POST api/<controller>
        /// <summary>
        /// action is using filter to provide validation
        /// </summary>
        /// <param name="id">The beer Id in Punk to search for</param>
        /// <param name="rating">object representing user rating, email and comment about the beer</param>
        /// <returns>status message showing if the request was successful or failed</returns>
        /// 

        //mvccore 2.2 [ApiController automatically performs the validation check that the action filter did in .Net webapi]
        //[Filters.VintriFilter]
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsync([FromQuery]int id, [FromBody]BeerRatingViewModel rating)
        {
            string path = "https://api.punkapi.com/v2/beers/" + id;
            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync(path);
            //   Beerd BeerRes = null;
            if (response.IsSuccessStatusCode)
            {
                //read the result and convert it to a .net object
                var jsonResult = response.Content.ReadAsStringAsync();
                List<BeerRatingViewModel> json = JsonConvert.DeserializeObject<List<BeerRatingViewModel>>(jsonResult.Result);

                //make sure their is a result in our collection
                if (json.First() != null)
                {
                    var val = json.First();
                    //pass values frombody into values collected from punk api 
                    val.BeerId = id; val.Comments = rating.Comments; val.Rating = rating.Rating; val.Username = rating.Username;
                    try
                    {
                        List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();
                        //check and load any entrys in database.json
                        if (_fileSystem.File.Exists(_env.ContentRootPath + (@"/App_Data/Database.json")))
                        {
                            using (FileStream fs = (FileStream)_fileSystem.File.OpenRead(_env.ContentRootPath + (@"/App_Data/Database.json")))
                            {
                                punkBeers = await System.Text.Json.JsonSerializer.DeserializeAsync<List<BeerRatingViewModel>>(fs);
                            }
                        }
                        punkBeers.Add(val);
                        //write out the new database.json file
                        using (FileStream fs = (FileStream)_fileSystem.File.OpenWrite(_env.ContentRootPath + (@"/App_Data/Database.json")))
                        {
                            await System.Text.Json.JsonSerializer.SerializeAsync(fs, punkBeers);
                        }
                    }
                    catch (Exception)
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    }
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }



    [Route("api/[controller]")]
    [ApiController]
    public class BeerListController : ControllerBase
    {
        readonly IHttpClientFactory _clientFactory;
        readonly IHostingEnvironment _env;
        readonly IFileSystem _fileSystem;

        public BeerListController(IHttpClientFactory clientFactory, IHostingEnvironment env, IFileSystem fileSystem)
        {
            _clientFactory = clientFactory;
            _env = env;
            _fileSystem = fileSystem;
        }


        // GET api/<controller>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<IEnumerable<dynamic>> GetAsync(string id)
        {
            string path = "https://api.punkapi.com/v2/beers/?beer_name=" + id;
            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var Samochod = response.Content.ReadAsStringAsync();
                List<PunkBeerViewModel> json = JsonConvert.DeserializeObject<List<PunkBeerViewModel>>(Samochod.Result);

                //load the data from database.json
                List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();
                if (_fileSystem.File.Exists(_env.ContentRootPath + (@"\App_Data\Database.json")))
                {
                    using (FileStream fs = (FileStream)_fileSystem.File.OpenRead(_env.ContentRootPath + (@"/App_Data/Database.json")))
                    {
                        punkBeers = await System.Text.Json.JsonSerializer.DeserializeAsync<List<BeerRatingViewModel>>(fs);
                    }
                }
                //combine the 2 collections and return json response
                return (from c in json
                       join p in punkBeers on c.id equals p.BeerId into ps
                       select new { c.id, c.name, c.description, userRatings = ps.Select(ds => new { ds.Rating, ds.Username, ds.Comments })});
            }
            //call to service failed return no data
            return null;
        }






    }



}