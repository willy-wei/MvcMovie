using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class MovieInfo
    {
        public string movie_name_ch { get; set; }
        public string movie_name_en { get; set; }
        public string release_time { get; set; }
        public string level { get; set; }
    }
}