using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace VintriCore.Models
{
    /// <summary>
    /// path for where the Database.json file is located
    /// </summary>
    public class Db_Json_Path
    {
        /// <summary>
        /// the path collected from environment of the database.json fule
        /// </summary>
        public string database_json_Path => _env.ContentRootPath +(@"/App_Data/Database.json");

        readonly IHostingEnvironment _env;

        public Db_Json_Path(IHostingEnvironment env)
        {
            _env = env;            
        }


    }

    public class BeerRatingViewModel
    {
        /// <summary>
        /// id of beer from punk api used later to match comments to the beer
        /// </summary>
        public int BeerId { get; set; }

        /// <summary>
        /// username needs to be an email address
        /// </summary>
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Please provide a valid email address")]
        public string Username { get; set; }

        /// <summary>
        /// the rating of the beer value must be between 1 and 5
        /// </summary>
        [Range(1, 5, ErrorMessage = "Please provide a value between 1 and 5")]
        public int Rating { get; set; }

        /// <summary>
        /// comment left by users about the beer
        /// </summary>
        public string Comments { get; set; }


    }

    /// <summary>
    /// viewmodel representing the data to capture from punk api response
    /// </summary>
    public class PunkBeerViewModel
    {
        /// <summary>
        /// id returned from the punk deer json result
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// the name of the beer
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// a description about the beer
        /// </summary>
        public string description { get; set; }


    }



}
