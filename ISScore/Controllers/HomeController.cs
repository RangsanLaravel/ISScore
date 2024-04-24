using ISScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;

namespace ISScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string _ConnectionString;
        private readonly SqlConnection _sqlConnection;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _ConnectionString = configuration.GetConnectionString("ConnectionSQLServer");
            this._sqlConnection = new SqlConnection(_ConnectionString);
            this._configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Search search)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(search);
                }
                //RenderDetail detail = new RenderDetail
                //{
                //    Name = "Mr.Sivawut Tatan",
                //    Employee_id = search.EMPLOYEE_ID,
                //    Desk_Check1 = "0",
                //    Desk_Check2 = "0",
                //    HQ_Mock_test = "1"
                //};
                var dt = Query(search);
                ViewBag.ColumnHeader = GetColumnHeaders(dt);
                ViewBag.DataSearch = dt;
                return View(search);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            
        }
        public IActionResult Privacy()
        {
            return View();
        }

        private DataTable Query(Search search)
        {
            DataTable dataTable = new DataTable();
            try
            {
                _sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    Connection = this._sqlConnection,
                    CommandText = this._configuration["Query"]
                };
                sqlCommand.Parameters.AddWithValue("@E_CO", search.EMPLOYEE_ID);

                // Execute the command asynchronously
                using (SqlDataReader sqlDataReader =  sqlCommand.ExecuteReader())
                {
                    // Load data into DataTable
                    dataTable.Load(sqlDataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return dataTable;
        }

        private List<string> GetColumnHeaders(DataTable dataTable)
        {
            List<string> columnHeaders = new List<string>();

            foreach (DataColumn column in dataTable.Columns)
            {
                columnHeaders.Add(column.ColumnName);
            }

            return columnHeaders;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
