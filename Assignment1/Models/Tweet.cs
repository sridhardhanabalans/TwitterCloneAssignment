using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class Tweet
    {
        public int tweet_id { get; set; }
        public string user_id { get; set; }
        [Display(Name = "Tweet")]
        [StringLength(130)]
        public string message { get; set; }
        [Display(Name = "Tweeted On")]
        public DateTime created { get; set; }
    }
}