using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Korzh.EasyQuery;
using Korzh.EasyQuery.Db;
using Korzh.EasyQuery.Mvc;
using Korzh.EasyQuery.Services;
using Korzh.EasyQuery.Services.Db;
using Korzh.Utils.Db;
using O2CV1EntityDtos;
using O2V1BusinesLayer;
using O2V1DataAccess;
using O2V1DataAccess.Criteria;
using O2V1DataAccess.O2CV1Query;
using O2V1Web.Mappers;
using O2V1Web.Models.EFModels;
using O2V1Web.Models.ViewModels;

namespace O2V1Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _dbConnectionString;
        private readonly EqServiceProviderDb eqService;
        private readonly SchemaRepository schemaRepository;

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

            _dbConnectionString = ConfigurationManager.ConnectionStrings[@"O2DataMart"].ConnectionString;
            eqService.Connection = new SqlConnection(_dbConnectionString);
            eqService.Connection = new SqlConnection(_dbConnectionString);

            schemaRepository = new SchemaRepository(eqService.Connection.ConnectionString);
        }


        /// <summary>
        ///     Set error status, writes error message into response and return empty ActionResult object
        /// </summary>
        /// <param name="msg">A descriptive error message.</param>
        /// <returns></returns>
        protected ActionResult ErrorResult(string msg)
        {
            Response.StatusCode = 400;
            Response.Write(msg);
            return new EmptyResult();
        }

        private static List<DropDownItem> GetSelectListItems(IEnumerable<DropDownItem> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<DropDownItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
                selectList.Add(new DropDownItem
                {
                    DropDownDisplay = element.DropDownDisplay,
                    DropDownValue = element.DropDownValue
                });

            return selectList;
        }


        private string AddColumnsConditionsByCode()
        {
            var dataModel1 = eqService.GetModel("County");
            var query1 = eqService.GetQuery();
            query1.Clear();
            //create simple column
            var col = query1.CreateColumn("County.County1", "County1", SortDirection.Ascending);
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
            var builder = new EntitySqlQueryBuilder(query1);
            builder.BuildSQL();
            var sql = builder.Result.SQL;
            return sql;
        }

        #region public actions

        /// <summary>
        ///     Gets the model by its name
        /// </summary>
        /// <param name="modelName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetModel(string modelName)
        {
            var result = eqService.GetModel(modelName);
            return Json(result.SaveToDictionary());
        }

        /// <summary>
        ///     This action returns a custom list by different list request options (list name).
        /// </summary>
        /// <param name="options">List request options - an instance of <see cref="ListRequestOptions" /> type.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList(ListRequestOptions options)
        {
            return Json(eqService.GetList(options));
        }

        /// <summary>
        ///     Gets the query by its name
        /// </summary>
        /// <param name="queryName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuery(string queryId)
        {
            var query = eqService.GetQuery(queryId);
            return Json(query.SaveToDictionary());
        }

        public ActionResult Index()
        {
            //var tableNames = schemaRepository.GetSchemaTables();

            //var tableDropDownItem = new TableDropDownItem();


            //IEnumerable<TableDropDownItem> temptablelist = tableNames.Select(name => new TableDropDownItem
            //{
            //    TableNameDisplay = name,
            //    TableNameValue = name
            //}).OrderBy(x => x.TableNameDisplay).ToList();

            //var model = new CountsQueryModel {_tables = GetSelectListItems(temptablelist)};
            //ViewBag.temptablelist = temptablelist;

            //return View("Home", model);
            //return View("EasyQuery");
            var loginViewModel = new LoginViewModel();
            return View("Login", loginViewModel);
        }

        public ActionResult Counts()
        {
            O2CV1QueryDto queryDto;
            List<O2CV1QueryDto> listQuery = QueryRepository.GetQueries();
           
            return View(listQuery);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTableColumns(string tableName)
        {
            var columnsForTable = new List<DropDownItem>();
            if (!ModelState.IsValid) return Json(columnsForTable, JsonRequestBehavior.AllowGet);
            var tableSchemaModels = schemaRepository.GetSchemaTableColumns(tableName);
            IEnumerable<DropDownItem> tempColumnList = tableSchemaModels.MetaData.Select(col => new DropDownItem
            {
                DropDownDisplay = col.Name,
                DropDownValue = $"{tableName}.{col.Name}"
            }).OrderBy(x => x.DropDownDisplay).ToList();

            columnsForTable = GetSelectListItems(tempColumnList).ToList();
            return Json(columnsForTable, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQueryId(string queryName)
        {
            var criteriaRepository = new CriteriaRepository(_dbConnectionString);
            var idFound = criteriaRepository.GetIdOfQuery(queryName);

            return Content(idFound);
        }

        public ActionResult Criteria(string queryId)
        {
            var tableNames = schemaRepository.GetSchemaBackBoneRelatedTables();

            IEnumerable<DropDownItem> temptablelist = tableNames.Select(name => new DropDownItem
            {
                DropDownDisplay = name,
                DropDownValue = name
            }).OrderBy(x => x.DropDownDisplay).ToList();

            var model = new CountsQueryModel
            {
                _tables = GetSelectListItems(temptablelist),
                QueryId = queryId,
                QueryName = string.Empty,
                CriteriaModel = {_criteria = BuildModelCriteria()}
            };
            ViewBag.temptablelist = temptablelist;
            GetQueryCriteriaIntoModel(queryId, model);
            return View(model);
        }

        private List<CriteraDropDownItem> BuildModelCriteria()
        {
            var criteraDropDownItems = new List<CriteraDropDownItem>
            {
                new CriteraDropDownItem
                {
                    CriteraNameDisplay = "EqualTo",
                    CriteraNameValue = " = "
                },
                new CriteraDropDownItem
                {
                    CriteraNameDisplay = "LessThan",
                    CriteraNameValue = " < "
                },
                new CriteraDropDownItem
                {
                    CriteraNameDisplay = "GreaterThan",
                    CriteraNameValue = " > "
                },
                new CriteraDropDownItem
                {
                    CriteraNameDisplay = "NotEqualTo",
                    CriteraNameValue = " != "
                },
                new CriteraDropDownItem
                {
                    CriteraNameDisplay = "InList",
                    CriteraNameValue = " in "
                },
                 new CriteraDropDownItem
                {
                    CriteraNameDisplay = "NotLike",
                    CriteraNameValue = " not like "
                },
                new CriteraDropDownItem
                {
                    CriteraNameDisplay = "Like",
                    CriteraNameValue = " like "
                }

            };
            return criteraDropDownItems.OrderBy(x => x.CriteraNameDisplay).ToList();
        }

        /// <summary>
        ///     Saves the query.
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="queryName">Query name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveQuery(string queryJson, string queryName)
        {
            eqService.SaveQueryDict(queryJson.ToDictionary(), queryName);

            var dict = new Dictionary<string, object>();
            dict.Add("result", "OK");
            return Json(dict);
        }

        [HttpGet]
        public ActionResult GenerateSql(string queryId)
        {
            O2CV1QueryDto queryDto;
            CriteriaDto criteriaDto;
            var criteriaBusiness = new CriteriaBusiness(_dbConnectionString);
            var sqlFromQueryBuilder = criteriaBusiness.BuildSqlFromQuery(queryId);

            //string sqlReturned = queryBuilderWithJoin();

            //Dictionary<string, object> dict = new Dictionary<string, object>();
            //dict.Add("statement", sqlReturned);

            //var look = model.SelectedTable;

            //ConvertModelToJson modelConverter = new ConvertModelToJson();
            //string queryJson = modelConverter.ConvertSimpleTableQuery(model.SelectedTable.ToString());

            //string optionsJson = modelConverter.ConvertOptionsToJson();

            //var query = eqService.SyncQueryDict(queryJson.ToDictionary());


            //var statement = eqService.BuildQuery(query, optionsJson.ToDictionary());
            //Dictionary<string, object> dict = new Dictionary<string, object>();
            //dict.Add("statement", statement);
            //return Json(dict);

            return Content(sqlFromQueryBuilder);
        }

        /// <summary>
        ///     It's called when it's necessary to synchronize query on client side with its server-side copy.
        ///     Additionally this action can be used to return a generated SQL statement (or several statements) as JSON string
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncQuery(CountsQueryModel model)
        {
            if (ModelState.IsValid)
            {
                O2CV1QueryDto queryDto;
                CriteriaDto criteriaDto;
                BuildDtosForCriteriaAdd(model, out queryDto, out criteriaDto);
                var criteriaBusiness = new CriteriaBusiness(_dbConnectionString);
                var queryId = criteriaBusiness.CreateNextCriteriaForQuery(queryDto, criteriaDto);
                model.QueryId = queryId;
                queryDto.QueryId = queryId;

                //var parmsFromCountViewModel = new ParmsFromCountViewModel();
                //var queryBuilderParms = parmsFromCountViewModel.GetQueryParmFromCountView(model.SelectedTable);
                //var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
                //var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);

                ModelState.Clear();

                ResetCountsQueryModel(criteriaDto, queryDto, model);

                //string sqlReturned = queryBuilderWithJoin();

                //Dictionary<string, object> dict = new Dictionary<string, object>();
                //dict.Add("statement", sqlReturned);
                return View("Criteria", model);

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
            return Redirect("Index");
        }

        private void GetQueryCriteriaIntoModel(string queryId, CountsQueryModel model)
        {
            var tableNames = schemaRepository.GetSchemaTables();
            CriteriaRepository criteriaRepository = new CriteriaRepository(_dbConnectionString);
            var criteriaThisQuery = criteriaRepository.GetCriteriaForQuery(Convert.ToInt64(queryId));
            model.QueryCriteria = CriteriaMapper.MapCriteriaDtoToCriteriaGridViewModel(criteriaThisQuery);
        }

        private void ResetCountsQueryModel(CriteriaDto criteriaDto, O2CV1QueryDto queryDto, CountsQueryModel model)
        {
            var tableNames = schemaRepository.GetSchemaTables();

            IEnumerable<DropDownItem> temptablelist = tableNames.Select(name => new DropDownItem
            {
                DropDownDisplay = name,
                DropDownValue = name
            }).OrderBy(x => x.DropDownDisplay).ToList();

            model.SelectedTable = criteriaDto.TableName;
            model.QueryId = queryDto.QueryId;
            model.QueryName = queryDto.QueryName;
            model._tables = GetSelectListItems(temptablelist);
            model.QueryId = queryDto.QueryId;
            model.CriteriaModel = new CriteriaModel {_criteria = BuildModelCriteria()};
            ViewBag.temptablelist = temptablelist;

            var tableSchemaModels = schemaRepository.GetSchemaTableColumns(criteriaDto.TableName);
            IEnumerable<DropDownItem> tempColumnList = tableSchemaModels.MetaData.Select(col => new DropDownItem
            {
                DropDownDisplay = col.Name,
                DropDownValue = $"{criteriaDto.TableName}.{col.Name}"
            }).OrderBy(x => x.DropDownDisplay).ToList();

            model._columns = tempColumnList.ToList();
            CriteriaRepository criteriaRepository = new CriteriaRepository(_dbConnectionString);
            var criteriaThisQuery = criteriaRepository.GetCriteriaForQuery(Convert.ToInt64(queryDto.QueryId));
            model.QueryCriteria = CriteriaMapper.MapCriteriaDtoToCriteriaGridViewModel(criteriaThisQuery);
        }

        private void BuildDtosForCriteriaAdd(CountsQueryModel model, out O2CV1QueryDto queryDto, out CriteriaDto criteriaDto)
        {
            queryDto = new O2CV1QueryDto
            {
                CreatedBy = User.Identity.Name,
                Deleted = false,
                Description = model.QueryName,
                QueryName = model.QueryName,
                QueryId = model.QueryId
            };

            criteriaDto = new CriteriaDto
            {
                CompareOperator = model.CriteriaModel.SelectedCriteria,
                CompareValue = model.CriteriaModel.CriteriaCompareValue,
                Createdby = User.Identity.Name,
                TableName = model.SelectedTable,
                Description =
                    $"{model.SelectedTable} {model.SelectedColumn} {model.CriteriaModel.SelectedCriteria} {model.CriteriaModel.CriteriaCompareValue}",
                Disabled = false,
                Name =
                    $"{model.SelectedTable} {model.SelectedColumn} {model.CriteriaModel.SelectedCriteria} {model.CriteriaModel.CriteriaCompareValue}",
                TableColumn = model.SelectedColumn
            };
        }

        /// <summary>
        ///     Executes the query passed as JSON string and returns the result record set (again as JSON).
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query.</param>
        /// <param name="optionsJson">Different options in JSON format</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteQuery(string queryJson, string optionsJson)
        {
            var query = eqService.LoadQueryDict(queryJson.ToDictionary());

            var optionsDict = optionsJson.ToDictionary();

            eqService.LoadOptionsDict(optionsDict);

            var statement = eqService.BuildQuery(query, optionsDict);

            var resultSet = eqService.GetDataSetBySql(statement);

            var resultSetDict = eqService.DataSetToDictionary(resultSet, optionsDict);
            var dict = new Dictionary<string, object>();
            dict.Add("statement", statement);
            dict.Add("resultSet", resultSetDict);
            dict.Add("paging", eqService.Paging.SaveToDictionary());
            return Json(dict);
        }


        /// <summary>
        ///     Executes the SQL and returns the result set as JSON string
        /// </summary>
        /// <param name="sql">The SQL statement.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteSql(string sql, string optionsJson)
        {
            return Json(eqService.ExecuteSql(sql, optionsJson.ToDictionary()));
        }


        [HttpGet]
        public JsonResult GetQueryList(string modelName)
        {
            var queries = eqService.GetQueryList(modelName);

            return Json(queries, JsonRequestBehavior.AllowGet);
        }

        public void ErrorResponse(string msg)
        {
            Response.StatusCode = 400;
            Response.Write(msg);
            Response.Output.Flush();
        }

        [HttpGet]
        public void ExportToFileExcel()
        {
            Response.Clear();
            var query = eqService.GetQuery();

            if (!query.IsEmpty)
            {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                var dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null)
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.xls")));
                    DbExport.ExportToExcelHtml(dataset, Response.Output, ExcelFormats.Default);
                }
                else
                {
                    ErrorResponse("Empty dataset");
                }
            }
            else
            {
                ErrorResponse("Empty query");
            }
        }

        [HttpGet]
        public void ExportToFileCsv()
        {
            Response.Clear();
            var query = eqService.GetQuery();

            if (!query.IsEmpty)
            {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                var dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null)
                {
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.csv")));
                    DbExport.ExportToCsv(dataset, Response.Output, CsvFormats.Default);
                }
                else
                {
                    ErrorResponse("Empty dataset");
                }
            }
            else
            {
                ErrorResponse("Empty query");
            }
        }

        #endregion
    }
}