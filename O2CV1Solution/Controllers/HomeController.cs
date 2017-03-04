using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

using Korzh.Utils.Db;
using Korzh.EasyQuery.Db;
using Korzh.EasyQuery.Services;
using Korzh.EasyQuery.Services.Db;
using Korzh.EasyQuery.EF;
using O2V1DataAccess;
using O2V1Web.Models;
using O2V1Web.Models.EFModels;
using O2V1Web.Models.ViewModels;

namespace Korzh.EasyQuery.Mvc.Demo.EF {
    public class HomeController : Controller {
        private EqServiceProviderDb eqService;
        private SchemaRepository schemaRepository;
        public HomeController()
        {


            eqService = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    model.LoadFromContext(typeof(O2DataMart));
                    //      model.LoadFromDBContext(O2DataMart.GetType(), DbContextOptions.IncludeComplexTypesInParentEntity);
                    model.SortEntities();
                }
            };

            var dbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[@"O2DataMart"].ConnectionString;
            eqService.Connection = new SqlConnection(dbConnectionString);
            eqService.Connection = new SqlConnection(dbConnectionString);

            schemaRepository = new SchemaRepository(eqService.Connection.ConnectionString);
        }


        #region public actions
        /// <summary>
        /// Gets the model by its name
        /// </summary>
        /// <param name="modelName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetModel(string modelName) {
            var result = eqService.GetModel(modelName);
            return Json(result.SaveToDictionary());
        }

        /// <summary>
        /// This action returns a custom list by different list request options (list name).
        /// </summary>
        /// <param name="options">List request options - an instance of <see cref="ListRequestOptions"/> type.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList(ListRequestOptions options) {
            return Json(eqService.GetList(options));
        }

        /// <summary>
        /// Gets the query by its name
        /// </summary>
        /// <param name="queryName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuery(string queryId) {
            var query = eqService.GetQuery(queryId);
            return Json(query.SaveToDictionary());
        }

        public ActionResult Index()
        {
            List<string> tableNames = schemaRepository.GetSchemaTables();

            SelectTables selectTables = new SelectTables();

            List<SelectTables> temptablelist = tableNames.Select(name => new SelectTables
            {
                TableNameDisplay = name,
                TableNameValue = name
            }).OrderBy(x => x.TableNameDisplay).ToList();

            ViewBag.temptablelist = temptablelist;


            var model = new TablesDropDownModel
            {
                DropDownListTableNames = new SelectList(temptablelist, "TableNameValue", "TableNameDisplay")
            };


            return View("Home",model);
            //return View("EasyQuery");
        }
        /// <summary>
        /// Saves the query.
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="queryName">Query name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveQuery(string queryJson, string queryName) {
            eqService.SaveQueryDict(queryJson.ToDictionary(), queryName);

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("result", "OK");
            return Json(dict);
        }

        /// <summary>
        /// It's called when it's necessary to synchronize query on client side with its server-side copy.
        /// Additionally this action can be used to return a generated SQL statement (or several statements) as JSON string
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="optionsJson">The additional parameters which can be passed to this method to adjust query statement generation.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncQuery(string queryJson, string optionsJson) {
            var query = eqService.SyncQueryDict(queryJson.ToDictionary());

           

            var statement = eqService.BuildQuery(query, optionsJson.ToDictionary());
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("statement", statement);
            return Json(dict);
        }

        /// <summary>
        /// Executes the query passed as JSON string and returns the result record set (again as JSON).
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query.</param>
        /// <param name="optionsJson">Different options in JSON format</param>
        /// <returns></returns>
        [HttpPost]
		public ActionResult ExecuteQuery(string queryJson, string optionsJson) {
			var query = eqService.LoadQueryDict(queryJson.ToDictionary());

			var optionsDict = optionsJson.ToDictionary();

			eqService.LoadOptionsDict(optionsDict);
			
			var statement = eqService.BuildQuery(query, optionsDict);

			var resultSet = eqService.GetDataSetBySql(statement);

			var resultSetDict = eqService.DataSetToDictionary(resultSet, optionsDict);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("statement", statement);
            dict.Add("resultSet", resultSetDict);
            dict.Add("paging", eqService.Paging.SaveToDictionary());
            return Json(dict);
        }


        /// <summary>
        /// Executes the SQL and returns the result set as JSON string
        /// </summary>
        /// <param name="sql">The SQL statement.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteSql(string sql, string optionsJson) {

            return Json(eqService.ExecuteSql(sql, optionsJson.ToDictionary()));
        }


        [HttpGet]
        public JsonResult GetQueryList(string modelName) {
            var queries = eqService.GetQueryList(modelName);

            return Json(queries, JsonRequestBehavior.AllowGet);
        }

        public void ErrorResponse(string msg) {
            Response.StatusCode = 400;
            Response.Write(msg);
            Response.Output.Flush();
        }

        [HttpGet]
        public void ExportToFileExcel() {
            Response.Clear();
            var query = eqService.GetQuery();

            if (!query.IsEmpty) {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                DataSet dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null) {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.xls")));
                    DbExport.ExportToExcelHtml(dataset, Response.Output, ExcelFormats.Default);
                }
                else
                    ErrorResponse("Empty dataset");
            }
            else
                ErrorResponse("Empty query");

        }

        [HttpGet]
        public void ExportToFileCsv() {
            Response.Clear();
            var query = eqService.GetQuery();

            if (!query.IsEmpty) {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                DataSet dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null) {
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.csv")));
                    DbExport.ExportToCsv(dataset, Response.Output, CsvFormats.Default);
                }
                else
                    ErrorResponse("Empty dataset");
            }
            else
                ErrorResponse("Empty query");
        }

        #endregion
       

        /// <summary>
        /// Set error status, writes error message into response and return empty ActionResult object 
        /// </summary>
        /// <param name="msg">A descriptive error message.</param>
        /// <returns></returns>
        protected ActionResult ErrorResult(string msg) {
            Response.StatusCode = 400;
            Response.Write(msg);
            return new EmptyResult();
        }

       
    }

}
