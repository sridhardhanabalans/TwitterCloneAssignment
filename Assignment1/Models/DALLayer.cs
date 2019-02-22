using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class DALLayer
    {

        public Profile LoadProfileHome(string id)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var person = twitterDBContext.People.Where(m => m.user_id == id).FirstOrDefault();
            Profile profileHome = new Profile();
            profileHome.user_id = person.user_id;
            var selfTweets = person.TWEETs;
            profileHome.selfTweets = new List<Tweet>();
            foreach (var item in selfTweets)
            {
                profileHome.selfTweets.Add(new Tweet
                {
                    created = item.created,
                    message = item.message,
                    user_id = item.user_id,
                    tweet_id = item.tweet_id
                });
            }

            var following = person.FOLLOWINGs1;
            profileHome.following = new List<Person>();
            foreach (var item in following)
            {
                profileHome.following.Add(new Person { user_id = item.following_id });
            }

            profileHome.allTweets = new List<Tweet>();
            profileHome.allTweets.AddRange(profileHome.selfTweets);
            foreach (var item in profileHome.following)
            {
                var followingTweet = twitterDBContext.TWEETs.Where(m => m.user_id == item.user_id);
                foreach (var value in followingTweet)
                {
                    profileHome.allTweets.Add(new Tweet { created = value.created, message = value.message, user_id = value.user_id, tweet_id = value.tweet_id });
                }
            }

            profileHome.allTweets = profileHome.allTweets.OrderByDescending(m => m.created).ToList();

            var followers = person.FOLLOWINGs;
            profileHome.followers = new List<Person>();
            foreach (var item in followers)
            {
                profileHome.followers.Add(new Person { user_id = item.user_id });
            }
            profileHome.tweetCount = person.TWEETs.Count;
            profileHome.followersCount = person.FOLLOWINGs.Count;
            profileHome.followingCount = person.FOLLOWINGs1.Count;
            profileHome.fullName = person.fullName;
            profileHome.email = person.email;
            profileHome.joined = person.joined;
            var twitterUsers = twitterDBContext.People;
            profileHome.twitterUsers = new List<Person>();
            foreach (var item in twitterUsers)
            {
                profileHome.twitterUsers.Add(new Person { user_id = item.user_id });
            }
            return profileHome;
        }

        public void DeactivateAccount(string id)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var result = twitterDBContext.People.Where(m => m.user_id == id).First();
            result.active = false;
            twitterDBContext.SaveChanges();
        }

        public Profile NewTweet(Tweet tweet, string user)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            Assignment1.TWEET tObj = new TWEET();
            tObj.user_id = user;
            tObj.message = tweet.message;
            tObj.created = DateTime.Now;
            twitterDBContext.TWEETs.Add(tObj);
            twitterDBContext.SaveChanges();

            var person = twitterDBContext.People.Where(m => m.user_id == user).FirstOrDefault();
            Profile pObj = new Profile();
            var selfTweets = person.TWEETs;
            pObj.selfTweets = new List<Tweet>();
            foreach (var item in selfTweets)
            {
                pObj.selfTweets.Add(new Tweet { created = item.created, message = item.message, user_id = item.user_id });
            }

            var following = person.FOLLOWINGs1;
            pObj.following = new List<Person>();
            foreach (var item in following)
            {
                pObj.following.Add(new Person { user_id = item.following_id });
            }

            pObj.allTweets = new List<Tweet>();
            pObj.allTweets.AddRange(pObj.selfTweets);
            foreach (var item in pObj.following)
            {
                var followingTweet = twitterDBContext.TWEETs.Where(m => m.user_id == item.user_id);
                foreach (var value in followingTweet)
                {
                    pObj.allTweets.Add(new Tweet { created = value.created, message = value.message, user_id = value.user_id });
                }
            }

            pObj.allTweets = pObj.allTweets.OrderByDescending(m => m.created).ToList();
            return pObj;
        }

        public Profile UpdateTweet(Tweet tweet, string user)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var tObj = twitterDBContext.TWEETs.Where(m => m.tweet_id == tweet.tweet_id).FirstOrDefault();
            tObj.message = tweet.message;
            tObj.created = DateTime.Now;
            twitterDBContext.SaveChanges();
            string id = user;
            var person = twitterDBContext.People.Where(m => m.user_id == id).FirstOrDefault();
            Profile pObj = new Profile();
            var selfTweets = person.TWEETs;
            pObj.selfTweets = new List<Tweet>();
            foreach (var item in selfTweets)
            {
                pObj.selfTweets.Add(new Tweet { created = item.created, message = item.message, user_id = item.user_id });
            }

            pObj.selfTweets = pObj.selfTweets.OrderByDescending(m => m.created).ToList();
            return pObj;
        }

        public Profile DeleteTweet(Tweet tweet, string user)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var tObj = twitterDBContext.TWEETs.Where(m => m.tweet_id == tweet.tweet_id).FirstOrDefault();
            twitterDBContext.TWEETs.Remove(tObj);
            twitterDBContext.SaveChanges();

            var person = twitterDBContext.People.Where(m => m.user_id == user).FirstOrDefault();
            Profile pObj = new Profile();
            var selfTweets = person.TWEETs;
            pObj.selfTweets = new List<Tweet>();
            foreach (var item in selfTweets)
            {
                pObj.selfTweets.Add(new Tweet { created = item.created, message = item.message, user_id = item.user_id });
            }

            pObj.selfTweets = pObj.selfTweets.OrderByDescending(m => m.created).ToList();
            return pObj;
        }

        public int Login(Person pObj)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var result = twitterDBContext.People.Where(m => m.user_id == pObj.user_id && m.password == pObj.password && m.active == true).Count();
            return result;
        }

        public void Signup(PERSON pObj)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            twitterDBContext.People.Add(pObj);
            twitterDBContext.SaveChanges();
        }

        public PERSON SearchUser(string id)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var result = twitterDBContext.People.Where(m => m.user_id == id && m.active == true).First();
            return result;
        }

        public void FollowUser(FOLLOWING following)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            twitterDBContext.FOLLOWINGs.Add(following);
            twitterDBContext.SaveChanges();
        }

        public void UnFollowUser(string id,string user)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var delObj = twitterDBContext.FOLLOWINGs.Where(m => m.user_id == user && m.following_id == id).First();
            twitterDBContext.FOLLOWINGs.Remove(delObj);
            twitterDBContext.SaveChanges();
        }

        public Profile LoadProfile(string id)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var person = twitterDBContext.People.Where(m => m.user_id == id).FirstOrDefault();
            Profile profilePage = new Profile();
            profilePage.user_id = person.user_id;
            var selfTweets = person.TWEETs;
            profilePage.selfTweets = new List<Tweet>();
            foreach (var item in selfTweets)
            {
                profilePage.selfTweets.Add(new Tweet { created = item.created, message = item.message, tweet_id = item.tweet_id });
            }
            profilePage.selfTweets = profilePage.selfTweets.OrderByDescending(m => m.created).ToList();
            var following = person.FOLLOWINGs1;
            profilePage.following = new List<Person>();
            foreach (var item in following)
            {
                profilePage.following.Add(new Person { user_id = item.following_id });
            }
            var followers = person.FOLLOWINGs;
            profilePage.followers = new List<Person>();
            foreach (var item in followers)
            {
                profilePage.followers.Add(new Person { user_id = item.user_id });
            }
            profilePage.fullName = person.fullName;
            profilePage.email = person.email;
            profilePage.joined = person.joined;
            return profilePage;
        }

        public void EditProfile(PERSON pObj)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var person = twitterDBContext.People.Where(m => m.user_id == pObj.user_id).FirstOrDefault();
            person.fullName = pObj.fullName;
            person.email = pObj.email;
            person.password = pObj.password;
            twitterDBContext.SaveChanges();
        }

        public Profile LoadProfilePage(string id)
        {
            TwitterDBContext twitterDBContext = new TwitterDBContext();
            var person = twitterDBContext.People.Where(m => m.user_id == id).FirstOrDefault();
            Profile profilePage = new Profile();
            profilePage.user_id = person.user_id;
            var selfTweets = person.TWEETs;
            profilePage.selfTweets = new List<Tweet>();
            foreach (var item in selfTweets)
            {
                profilePage.selfTweets.Add(new Tweet { created = item.created, message = item.message, tweet_id = item.tweet_id });
            }
            profilePage.selfTweets = profilePage.selfTweets.OrderByDescending(m => m.created).ToList();
            var following = person.FOLLOWINGs1;
            profilePage.following = new List<Person>();
            foreach (var item in following)
            {
                profilePage.following.Add(new Person { user_id = item.following_id });
            }
            var followers = person.FOLLOWINGs;
            profilePage.followers = new List<Person>();
            foreach (var item in followers)
            {
                profilePage.followers.Add(new Person { user_id = item.user_id });
            }
            profilePage.fullName = person.fullName;
            profilePage.email = person.email;
            profilePage.joined = person.joined;
            return profilePage;
        }

    }
}
