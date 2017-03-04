using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Korzh.EasyQuery.Mvc.Demo.EF.Controllers
{
    public class OldHomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //List<string> tableNames = schemaRepository.GetSchemaTables();

            //SelectTables selectTables = new SelectTables();

            //List<SelectTables> temptablelist = tableNames.Select(name => new SelectTables
            //{
            //    TableNameDisplay = name,
            //    TableNameValue = name
            //}).ToList();

            //ViewBag.temptablelist = temptablelist;


            //TablesDropDownModel model = new TablesDropDownModel();
            //model.DropDownListTableNames = new SelectList(temptablelist, "TableNameValue", "TableNameDisplay");

            return View("Proof");
            //return View("EasyQuery");
        }
	}
}