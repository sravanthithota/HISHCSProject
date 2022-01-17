using ExcelDataReader;
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

namespace HIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment;
        private ResponseViewModel res = new ResponseViewModel();

        [Route("ReadFile")]
        [HttpPost]
        public IActionResult ReadFile(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                //Create a Folder.
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                //Read the connection string for the Excel file.
                string excelstr = Configuration.GetConnectionString("ExcelConString");
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
                /* conString = this.Configuration.GetConnectionString("constr");
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
                 }*/
                res.msg = dt.ToString();
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
            // List<> studentList = new List<>();
            return BadRequest();
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
    }
}

  

