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

            //TableDropDownItem selectTables = new TableDropDownItem();

            //List<TableDropDownItem> temptablelist = tableNames.Select(name => new TableDropDownItem
            //{
            //    TableNameDisplay = name,
            //    TableNameValue = name
            //}).ToList();

            //ViewBag.temptablelist = temptablelist;


            //TablesModel model = new TablesModel();
            //model.DropDownListTableNames = new SelectList(temptablelist, "TableNameValue", "TableNameDisplay");

            return View("Proof");
            //return View("EasyQuery");
        }
	}
}