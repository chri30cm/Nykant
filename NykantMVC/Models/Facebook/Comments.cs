﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NykantMVC.Models.Facebook
{
    public class Comments
    {
        [JsonProperty("data")]
        public List<Comment> List { get; set; }
    }
}