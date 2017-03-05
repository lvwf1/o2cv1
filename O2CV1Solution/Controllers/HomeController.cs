using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeEngine.Framework.QueryBuilder;
using CodeEngine.Framework.QueryBuilder.Enums;
using Korzh.EasyQuery;
using Korzh.EasyQuery.Db;
using Korzh.EasyQuery.Mvc;
using Korzh.EasyQuery.Services;
using Korzh.EasyQuery.Services.Db;
using Korzh.Utils.Db;
using O2V1BusinesLayer;
using O2V1DataAccess;
using O2V1Web.Models.EFModels;
using O2V1Web.Models.ViewModels;

namespace O2V1Web.Controllers {
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

            TableDropDownItem tableDropDownItem = new TableDropDownItem();

 
            IEnumerable<TableDropDownItem> temptablelist = tableNames.Select(name => new TableDropDownItem
            {
                TableNameDisplay = name,
                TableNameValue = name
            }).OrderBy(x => x.TableNameDisplay).ToList();

            var model = new TablesDropDownViewModel { _tables = GetSelectListItems(temptablelist)};
            ViewBag.temptablelist = temptablelist;

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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncQuery(TablesDropDownViewModel model)
        {
            if (ModelState.IsValid)
            {

                string sqlReturned = queryBuilderWithJoin();

                //Dictionary<string, object> dict = new Dictionary<string, object>();
                //dict.Add("statement", sqlReturned);
                return Content(sqlReturned);

                //var look = model.SelectedTable;

                //ConvertModelToJson modelConverter = new ConvertModelToJson();
                //string queryJson = modelConverter.ConvertSimpleTableQuery(model.SelectedTable.ToString());

                //string optionsJson = modelConverter.ConvertOptionsToJson();

                //var query = eqService.SyncQueryDict(queryJson.ToDictionary());


                //var statement = eqService.BuildQuery(query, optionsJson.ToDictionary());
                //Dictionary<string, object> dict = new Dictionary<string, object>();
                //dict.Add("statement", statement);
                //return Json(dict);
            }
            else
            {
               return Redirect("Index");
            }
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

        private static List<TableDropDownItem> GetSelectListItems(IEnumerable<TableDropDownItem> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<TableDropDownItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new TableDropDownItem()
                {
                    TableNameValue = element.TableNameValue,
                    TableNameDisplay = element.TableNameDisplay
                });
            }

            return selectList;
        }
        private string queryBuilderWithJoin()
        {
            SelectQueryBuilder query = new SelectQueryBuilder();
            query.SelectFromTable("Property");

            query.AddJoin(JoinType.InnerJoin,
                          "Persons", "State",
                          Comparison.Equals,
                          "Property", "State");

            query.AddWhere("Property.OwnerOccupant", Comparison.Equals, 1);
            query.SelectAllColumns();
            return query.BuildQuery();

            //// or, have a DbCommand object built for even more safety against SQL Injection attacks:
            //query.SetDbProviderFactory(DbProviderFactories.GetFactory("System.Data.SqlClient"));
            //DbCommand command = query.BuildCommand();

            //statement = string.Format(
            //    "Query built by BuildCommand (not showing parameter values):\n\n{0}",
            //    command.CommandText);

        }
        private string queryBuilderSingleTableAllColumnsWhere()
        {
            int maxRecords = 10;
            SelectQueryBuilder query = new SelectQueryBuilder();
            query.SelectFromTable("Counties");
            query.SelectAllColumns();
           // query.TopRecords = maxRecords;

         
                query.AddWhere("State", Comparison.Like, "w%");

        
                query.AddWhere("County", Comparison.Like, "Ad%");
 
            string statement = string.Format(
                "Query built by BuildQuery:\n\n{0}",
                query.BuildQuery());

            return statement;

            //// or, have a DbCommand object built for even more safety against SQL Injection attacks:
            //query.SetDbProviderFactory(DbProviderFactories.GetFactory("System.Data.SqlClient"));
            //DbCommand command = query.BuildCommand();

            //statement = string.Format(
            //    "Query built by BuildCommand (not showing parameter values):\n\n{0}",
            //    command.CommandText);

        }
        private string AddColumnsConditionsByCode()
        {
     
            DbModel dataModel1 = eqService.GetModel("County");
            DbQuery query1 = eqService.GetQuery();
            query1.Clear();
            //create simple column
            Column col = query1.CreateColumn("County.County1", "County1", SortDirection.Ascending);
            query1.Columns.Add(col);
           

            //create aggregate column
            //EntityAttr attr = dataModel1.EntityRoot.FindAttribute(EntityAttrProp.Expression, "Orders.Freight");
            //col = new DbColumn("Total sum", SortDirection.None);
            //col.Expr = new DbAggrFuncExpr(dataModel1, "SUM", new DbEntityAttrExpr(dataModel1, attr));
            //query1.Columns.Add(col);

            ////create conditions
            ////here we create condition object
            //SimpleCondition cond = new DbSimpleCondition(dataModel1);

            ////then we search for an entity attribute which will be used in the left side of condition
            //attr = dataModel1.EntityRoot.FindAttribute(EntityAttrProp.Expression, "Orders.OrderDate");

            ////after that we add found entity attribute as first (left side) expression of our condition
            //cond.BaseExpr = new DbEntityAttrExpr(dataModel1, attr);

            ////here we set an operator used in condition. In our case it will be  "is less than" (< symbol in SQL syntax)
            //cond.Operator = dataModel1.Operators.FindByID("LessThan");

            ////finally we set the rigth side expression which is some constant value in our case.
            //cond.SetValueExpr(1, new ConstExpr(DataType.Date, "2005-01-01"));
            //cond.ReadOnly = true;

            ////when all parts of our condition are ready - we add it to query
            //query1.Root.Conditions.Add(cond);

            ////here is more simple and quicker way to add a condition (same attribute, operator and value)
            //query1.Root.AddSimpleCondition("Orders.OrderDate", "DateBeforePrecise", "2005-01-01");

            ////here is one more example: how to add a group of conditions
            //var predicate = query.AddPredicate(query1.Root, query1.Root.Conditions.Count);
            //predicate.AddSimpleCondition("Customers.City", "Equal", "London");
            //predicate.AddSimpleCondition("Customer.City", "Equal", "NewYork");


            //generate SQL statement
            EntitySqlQueryBuilder builder = new EntitySqlQueryBuilder(query1);
            builder.BuildSQL();
            string sql = builder.Result.SQL;
            return sql;
            
        }


    }

}
