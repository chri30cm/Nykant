﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NykantMVC.Models.Facebook
{
    public class Comment
    {
        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("from")]
        public From From { get; set; }
        [JsonProperty("can_reply_privately")]
        public bool CanReplyPrivately { get; set; }
    }
}
