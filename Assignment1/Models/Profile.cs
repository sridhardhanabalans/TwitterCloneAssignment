using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class Profile : Person
    {
        public List<Tweet> allTweets { get; set; }
        public List<Tweet> selfTweets { get; set; }
        public int tweetCount { get; set; }
        [Display(Name = "Total Followers")]
        public int followersCount { get; set; }
        [Display(Name = "Total Following")]
        public int followingCount { get; set; }
        public Tweet newTweet { get; set; }
        [Display(Name = "Followers")]
        public List<Person> followers { get; set; }
        [Display(Name = "Following")]
        public List<Person> following { get; set; }
        public List<Person> twitterUsers { get; set; }
    }
}