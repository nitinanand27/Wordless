using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Wordless.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1774392159446483",
                client_secret = "8f9a13caa407845dc68c0b1bf585ece6",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FbLogin()
        {
            return View();
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "1774392159446483",
                client_secret = "8f9a13caa407845dc68c0b1bf585ece6",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // TODO: Authenticate User
            // Store the access token in the session
            Session["AccessToken"] = accessToken;
            Session["loginStatus"] = true;

            // update the facebook client with the access token so
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
            string email = me.email;

            string currentUsername = me.first_name;
            Session["currentUsername"] = currentUsername;
            string currentUserLastName = me.last_name;
            Session["currentUserLastName"] = currentUserLastName;

            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(email, false);

            return RedirectToAction("Index", "Home");
        }
    }
}