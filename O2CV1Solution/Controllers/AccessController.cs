using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using O2V1Web.Models.ViewModels;
using O2.Web.Context;

namespace O2V1Web.Controllers
{
    public class AccessController : BaseController
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginview)
        {
            if (ModelState.IsValid)
            {
                using (CountsContext Contsctx = new CountsContext())
                {
                    String userpass = Contsctx.Encrypt(loginview.Password);
                    var v = Contsctx.CountUsers.Where(a => a.Usermail.Equals(loginview.Username) && a.UserPassword.Equals(userpass)).FirstOrDefault();
                    if (v != null)
                    {
                        FormsAuthentication.SetAuthCookie(v.Usermail, false);
                        Session.SetDataInSession<string>("LogedUserID", v.CountUserId.ToString());
                        Session.SetDataInSession<string>("LogedUserFullname", v.Usermail.ToString());
                        //string value = Session.GetDataFromSession<string>("key1");
                        //Session.SetDataInSession("LogedUserID", v.CountUserId.ToString());
                        //System.Web.HttpContext.Current.Session["LogedUserID"] = v.CountUserId.ToString();
                        //System.Web.HttpContext.Current.Session["LogedUserFullname"] = v.Usermail.ToString();

                        return RedirectToAction("Mycount", "Counts");
                    }
                    else
                    {
                        ViewBag.LoginError = "Invalid Username or Password";
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginCos(LoginViewModel loginview)
        {
            if (ModelState.IsValid)
            {
                using (CountsContext Contsctx = new CountsContext())
                {
                    String userpass = Contsctx.Encrypt(loginview.Password);
                    var v = Contsctx.CountUsers.Where(a => a.Usermail.Equals(loginview.Username) && a.UserPassword.Equals(userpass)).FirstOrDefault();
                    if (v != null)
                    {
                        FormsAuthentication.SetAuthCookie(v.Usermail, false);
                        Session.SetDataInSession<string>("LogedUserID", v.CountUserId.ToString());
                        Session.SetDataInSession<string>("LogedUserFullname", v.Usermail.ToString());
                        //Session["CosDataSet"] = new DataSet();
                        return RedirectToAction("Counts", "Home");

                    }
                    else
                    {
                        ViewBag.LoginError = "Invalid Username or Password";
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            return RedirectToAction("Index", "Home");
        }
    }
}