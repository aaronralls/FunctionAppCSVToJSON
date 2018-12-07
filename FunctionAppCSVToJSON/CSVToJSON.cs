

using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System;
using CsvHelper;
using System.Text;
using Microsoft.Extensions.Logging;

namespace FunctionAppCSVToJSON
{
public static class CSVToJSON
{
    [FunctionName("CSVToJSON")]
    public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function CSVToJSON processed a request.");

        string fileName = req.Query["fileName"];
        string errorMessage = "";

        string requestBody = new StreamReader(req.Body).ReadToEnd();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        fileName = fileName ?? data?.fileName;          

        if (fileName == null)
        {
            errorMessage = "Please pass a fileName on the query string or in the request body";
            log.LogInformation("BadRequest: " + errorMessage);
            return new BadRequestObjectResult(errorMessage);
        }            

        string csvData = data?.csv;

        if (csvData == null)
        {
            errorMessage = "Please pass the csv data in using the csv attribute in the request body";
            log.LogInformation("BadRequest: " + errorMessage);
            return new BadRequestObjectResult(errorMessage);
        }

        log.LogInformation("csv data is present.");

        

        

        JsonResult resultSet = new JsonResult(fileName);

        byte[] byteArray = Encoding.UTF8.GetBytes(csvData);
        MemoryStream csvStream = new MemoryStream(byteArray);

        var records = Convert(csvStream);

        JArray jsonarray = JArray.FromObject(records);

        foreach(var row in jsonarray.Children()){
            resultSet.Rows.Add(row);
        }

        

        log.LogInformation(string.Format("There are {0} lines in the csv records content {1}.", records.Count(), fileName));

        
        

        return (ActionResult)new OkObjectResult(resultSet);
    }

        public static List<object> Convert(Stream blob)
        {
            var sReader = new StreamReader(blob);
            var csv = new CsvReader(sReader);

            csv.Read();
            csv.ReadHeader();

            var csvRecords = csv.GetRecords<object>().ToList();

            return (csvRecords);
        }
        

    public class JsonResult
    {
        public JsonResult(string fileName)
        {
            Rows = new JArray();
            FileName = fileName;
        }

        public string FileName { get; set; }
        public JArray Rows { get; set; }
    }
}
}
