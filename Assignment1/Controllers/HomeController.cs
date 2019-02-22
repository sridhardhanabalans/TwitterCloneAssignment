using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class HomeController : Controller
    {
        DALLayer dalLayer = new DALLayer();
        public ActionResult ProfileHome(string id)
        {
            Profile profileHome = new Profile();
            profileHome = dalLayer.LoadProfileHome(id);
            return View(profileHome);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
        public ActionResult Delete(string id)
        {
            dalLayer.DeactivateAccount(id);
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
        public ActionResult newTweet(Tweet tweet)
        {
            try
            {
                Profile pObj = new Profile();
                string id = Session["user"].ToString();
                pObj = dalLayer.NewTweet(tweet, id);
                return PartialView("GetTweets", pObj.allTweets);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult updateTweet(Tweet tweet)
        {
            try
            {
                Profile pObj = new Profile();
                string id = Session["user"].ToString();
                pObj = dalLayer.UpdateTweet(tweet, id);
                return PartialView("SelfTweets", pObj.selfTweets);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult deleteTweet(Tweet tweet)
        {
            try
            {
                Profile pObj = new Profile();
                string id = Session["user"].ToString();
                pObj = dalLayer.DeleteTweet(tweet, id);
                return PartialView("SelfTweets", pObj.selfTweets);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Person pObj = new Person();
            pObj.user_id = collection["user_id"].ToString();
            pObj.password = collection["password"].ToString();

            var result = dalLayer.Login(pObj);
            if (result == 1)
            {
                Session["user"] = pObj.user_id;
                return RedirectToAction("ProfileHome", new { id = pObj.user_id });
            }
            else
            {
                ViewBag.Message = "*Account doesn't exists! Click Sign Up for creating new Account!";
                return View();
            }
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Assignment1.PERSON pObj = new Assignment1.PERSON();
            pObj.user_id = collection["user_id"].ToString();
            pObj.password = collection["password"].ToString();
            pObj.fullName = collection["fullname"].ToString();
            pObj.email = collection["email"].ToString();
            pObj.joined = DateTime.Now;
            pObj.active = true;

            dalLayer.Signup(pObj);
            Session["user"] = pObj.user_id;

            return RedirectToAction("ProfileHome", new { id = pObj.user_id });
        }

        public ActionResult Search(string id)
        {
            try
            {
                var result = dalLayer.SearchUser(id);
                return Json(result.user_id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult FollowUser(string id)
        {
            if (id == Session["user"].ToString())
            {
                return Json("Follow option is for other user's profile!", JsonRequestBehavior.AllowGet);
            }
            try
            {
                FOLLOWING following = new FOLLOWING();
                following.user_id = Session["user"].ToString();
                following.following_id = id;
                dalLayer.FollowUser(following);
                return Json("You are now following - " + following.following_id + "!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UnFollowUser(string id)
        {
            if (id == Session["user"].ToString())
            {
                return Json("UnFollow option is for other user's profile!", JsonRequestBehavior.AllowGet);
            }
            try
            {
                string user = Session["user"].ToString();
                TwitterDBContext twitterDBContext = new TwitterDBContext();
                dalLayer.UnFollowUser(id, user);
                return Json(id + " - unfollowed!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditProfile(string id)
        {
            if (id != Session["user"].ToString())
            {
                return Json("You can't edit other's profile!", JsonRequestBehavior.AllowGet);
            }

            Profile profilePage = new Profile();
            profilePage = dalLayer.LoadProfile(id);
            return View(profilePage);
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            string user = Session["user"].ToString();
            PERSON person = new PERSON();
            person.user_id = user;
            person.fullName = collection["fullName"].ToString();
            person.email = collection["email"].ToString();
            person.password = collection["password"].ToString();
            dalLayer.EditProfile(person);
            return RedirectToAction("ProfileHome", new { id = user });
        }

        public ActionResult ProfilePage(string id)
        {
            Profile profilePage = new Profile();
            profilePage = dalLayer.LoadProfilePage(id);
            return View(profilePage);
        }

    }
}