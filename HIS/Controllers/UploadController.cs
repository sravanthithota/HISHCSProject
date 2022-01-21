/*using ExcelDataReader;
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data.OleDb;
using BAL.ViewModel;
using BAL.BL;
using System.Threading.Tasks;
using System.Collections;
/*using ClosedXML.Excel;
*/
using Microsoft.AspNetCore.StaticFiles;

namespace HIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnviroment;
        private SystemCodeBL bl = new SystemCodeBL();
        public UploadController(IWebHostEnvironment hostingEnviroment)
        {
            _hostingEnviroment = hostingEnviroment;
        }
        public IConfiguration Configuration { get; }
        private ResponseViewModel res = new ResponseViewModel();

       /* [Route("ReadFile")]
        [HttpPost]
        public IActionResult ReadFile()
        {
            try
            {
                var httpRequest = HttpContext.Request.Form;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = null;
                if (httpRequest.Files.Count > 0)
                {
                    IFormFile Inputfile = httpRequest.Files[0];
                    if (Inputfile != null)
                    {
                        if (string.IsNullOrWhiteSpace(_hostingEnviroment.WebRootPath))
                        {
                            _hostingEnviroment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        //Create a Folder.
                        string path = Path.Combine(_hostingEnviroment.WebRootPath, "Uploads");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //Save the uploaded Excel file.
                        string fileName = Path.GetFileName(Inputfile.FileName);
                        string filePath = Path.Combine(path, fileName);
                        using (FileStream stream1 = new FileStream(filePath, FileMode.Create))
                        {
                            Inputfile.CopyTo(stream1);
                        }

                        FileStream stream = new FileStream(filePath, FileMode.Create);
                        //Read the connection string for the Excel file.
                        string excelstr = Configuration.GetConnectionString("ExcelConString");
                        if (excelstr == null)
                        {
                            if (Inputfile != null)
                            {
                                if (Inputfile.FileName.EndsWith(".xls"))
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                else if (Inputfile.FileName.EndsWith(".xlsx"))
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                else
                                    res.msg = "The file format is not supported.";

                                dsexcelRecords = reader.AsDataSet();
                                reader.Close();

                                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                                {
                                    DataTable dtStudentRecords = dsexcelRecords.Tables[0];
                                    DataTable Students = new DataTable();
                                    for (int i = 0; i < dtStudentRecords.Rows.Count; i++)
                                    {
                                        SystemCodeViewModel objStudent = new SystemCodeViewModel();
                                        objStudent.ID = Convert.ToInt32(dtStudentRecords.Rows[i][0]);
                                        objStudent.CategoryCode = Convert.ToString(dtStudentRecords.Rows[i][0]);
                                        objStudent.Code = Convert.ToString(dtStudentRecords.Rows[i][1]);
                                        objStudent.Description = Convert.ToString(dtStudentRecords.Rows[i][2]);
                                        objStudent.ShortCode = Convert.ToString(dtStudentRecords.Rows[i][3]);
                                        objStudent.IsSystem = Convert.ToInt32(dtStudentRecords.Rows[i][4]);
                                        objStudent.ParentId = Convert.ToInt32(dtStudentRecords.Rows[i][4]);
                                        Students.Rows.Add(objStudent);
                                    }
                                    res.msg = Students.ToString();
                                    //  int output = Students.SaveChanges();
                                    *//*  if (i > 0)
                                          message = "The Excel file has been successfully uploaded.";
                                      else
                                         message = "Something Went Wrong!, The Excel file uploaded has fiald."; *//*
                                }
                                else
                                    res.msg = "Selected file is empty.";
                            }

                        }
                        DataTable dt = new DataTable();
                        excelstr = string.Format(excelstr, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(excelstr))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }

                        //Insert the Data read from the Excel file to Database Table.
                        *//* conString = this.Configuration.GetConnectionString("constr");
                         using (SqlConnection con = new SqlConnection(conString))
                         {
                             using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                             {
                                 //Set the database table name.
                                 sqlBulkCopy.DestinationTableName = "dbo.Customers";

                                 //[OPTIONAL]: Map the Excel columns with that of the database table.
                                 sqlBulkCopy.ColumnMappings.Add("Id", "CustomerId");
                                 sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                                 sqlBulkCopy.ColumnMappings.Add("Country", "Country");

                                 con.Open();
                                 sqlBulkCopy.WriteToServer(dt);
                                 con.Close();
                             }
                         }*//*
                        res.msg = dt.ToString();
                        res.status = HttpStatusCode.OK.ToString();
                        res.responseCode = "1";
                        return Ok(res);
                    }
                    else
                        res.msg = "Invalid File.";
                    res.status = HttpStatusCode.BadRequest.ToString();
                }
                else
                {
                    res.msg = HttpStatusCode.BadRequest.ToString();
                }
                // res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // List<> studentList = new List<>();

        }
        public static string convertDataTableToString(DataTable dataTable)
        {
            string data = string.Empty;
            int rowsCount = dataTable.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                DataRow row = dataTable.Rows[i];
                int columnsCount = dataTable.Columns.Count;
                for (int j = 0; j < columnsCount; j++)
                {
                    data += dataTable.Columns[j].ColumnName + "~" + row[j];
                    if (j == columnsCount - 1)
                    {
                        if (i != (rowsCount - 1))
                            data += "$";
                    }
                    else
                        data += "|";
                }
            }
            return data;
        }

        [Route("ReadEXcelFile")]
        [HttpPost]
        public IActionResult ReadExcelFile()
        {
            try
            {
                #region Variable Declaration
                string message = "";
                HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Request.Form;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                IFormFile Inputfile = null;
                Stream FileStream = null;
                // DataTable objStudent = new DataTable();
                #endregion

                #region Save Student Detail From Excel
                *//*  using (dbCodingvilaEntities objEntity = new dbCodingvilaEntities())
                  {*//*
                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];

                    FileStream = Inputfile.OpenReadStream();

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            message = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtStudentRecords = dsexcelRecords.Tables[0];
                            DataTable Students = new DataTable();
                            for (int i = 0; i < dtStudentRecords.Rows.Count; i++)
                            {
                                SystemCodeViewModel objStudent = new SystemCodeViewModel();
                                objStudent.ID = Convert.ToInt32(dtStudentRecords.Rows[i][0]);
                                objStudent.CategoryCode = Convert.ToString(dtStudentRecords.Rows[i][0]);
                                objStudent.Code = Convert.ToString(dtStudentRecords.Rows[i][1]);
                                objStudent.Description = Convert.ToString(dtStudentRecords.Rows[i][2]);
                                objStudent.ShortCode = Convert.ToString(dtStudentRecords.Rows[i][3]);
                                objStudent.IsSystem = Convert.ToInt32(dtStudentRecords.Rows[i][4]);
                                objStudent.ParentId = Convert.ToInt32(dtStudentRecords.Rows[i][4]);
                                Students.Rows.Add(objStudent);
                            }
                            res.msg = Students.ToString();
                            //  int output = Students.SaveChanges();
                            *//*  if (i > 0)
                                  message = "The Excel file has been successfully uploaded.";
                              else
                                 message = "Something Went Wrong!, The Excel file uploaded has fiald."; *//*
                        }
                        else
                            res.msg = "Selected file is empty.";
                    }
                    else
                        res.msg = "Invalid File.";
                }
                else
                    res.msg = HttpStatusCode.BadRequest.ToString();
                *//* }*//*
                //  res.msg = dt.ToString();
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
*/
        [Route("DownloadEXcelFile")]
        [HttpGet]
        public async Task<IActionResult> DownloadExcel([FromQuery] string Name)
        {
            List<SystemCodeViewModel> authors = new List<SystemCodeViewModel>();
            if (string.IsNullOrWhiteSpace(_hostingEnviroment.WebRootPath))
            {
                _hostingEnviroment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            //Create a Folder.
            string path = Path.Combine(_hostingEnviroment.WebRootPath, "files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = Path.Combine(path, Name);
            // var filePath = "C:/Users/sravanthi/Desktop/sampledatafoodsales.xlsx";

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), Name);
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }


}



