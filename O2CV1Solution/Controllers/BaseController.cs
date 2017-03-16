using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace O2V1Web.Controllers
{
    public class BaseController : Controller
    {
       // public ISessionWrapper SessionWrapper { get; set; }

        public BaseController()
        {
            //SessionWrapper = new HttpContextSessionWrapper();
        }

       
    }

    //public interface ISessionWrapper
    //{
    //    T GetFromSession<T>(string key);
    //    void SetInSession(string key, object value);
    //}

    //public class HttpContextSessionWrapper : ISessionWrapper
    //{
    //    public T GetFromSession<T>(string key)
    //    {
    //        return (T)HttpContext.Current.Session[key];
    //    }

    //    public void SetInSession(string key, object value)
    //    {
    //        HttpContext.Current.Session[key] = value;
    //    }


    //}
    public static class SessionExtensions
    {
        public static T GetDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
            return (T)session[key];
        }

        public static void SetDataInSession<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }

        
    }

}