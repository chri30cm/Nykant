﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NykantMVC.Models.Facebook
{
    public class Summary
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}
