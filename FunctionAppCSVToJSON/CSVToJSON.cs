

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

namespace FunctionAppCSVToJSON
{
public static class CSVToJSON
{
    [FunctionName("CSVToJSON")]
    public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
    {
        log.Info("C# HTTP trigger function CSVToJSON processed a request.");

        char[] fieldSeperator = new char[] { ',' };

        string fileName = req.Query["fileName"];            
        string rowsToSkipStr = req.Query["rowsToSkip"];
        string errorMessage = "";

        string requestBody = new StreamReader(req.Body).ReadToEnd();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        int rowsToSkip = 0;
        long lineSkipCounter = 0;

        fileName = fileName ?? data?.fileName;          
        rowsToSkipStr = rowsToSkipStr ?? data?.rowsToSkip;

        if (rowsToSkipStr == null)
        {
            errorMessage = "Please pass a rowsToSkip on the query string or in the request body";
            log.Info("BadRequest: " + errorMessage);
            return new BadRequestObjectResult(errorMessage);
        }
        else
        {
            Int32.TryParse(rowsToSkipStr, out rowsToSkip);                
        }

        if (fileName == null)
        {
            errorMessage = "Please pass a fileName on the query string or in the request body";
            log.Info("BadRequest: " + errorMessage);
            return new BadRequestObjectResult(errorMessage);
        }            

        string csvData = data?.csv;

        if (csvData == null)
        {
            errorMessage = "Please pass the csv data in using the csv attribute in the request body";
            log.Info("BadRequest: " + errorMessage);
            return new BadRequestObjectResult(errorMessage);
        }

        log.Info("csv data is present.");

        string[] csvLines = ToLines(csvData);

        log.Info(string.Format("There are {0} lines in the csv content {1}.", csvLines.Count(), fileName));

        var headers = csvLines[0].Split(fieldSeperator).ToList<string>();

        JsonResult resultSet = new JsonResult(fileName);


        foreach (var line in csvLines.Skip(rowsToSkip))
        {
            //Check to see if a line is blank.
            //This can happen on the last row if improperly terminated.
            if (line != "" || line.Trim().Length > 0 )
            {
                var lineObject = new JObject();
                var fields = line.Split(fieldSeperator);

                for (int x = 0; x < headers.Count; x++)
                {
                    lineObject[headers[x]] = fields[x];
                }

                resultSet.Rows.Add(lineObject);
            }
            else
            {
                lineSkipCounter += 1;
            }
        }

        log.Info(string.Format("There were {0} lines skipped, not including the header row.", lineSkipCounter));

        return (ActionResult)new OkObjectResult(resultSet);
    }

    private static string[] ToLines(string dataIn)
        {            
            char[] EOLMarkerR = new char[] { '\r' };
            char[] EOLMarkerN = new char[] { '\n' };
            char[] EOLMarker = EOLMarkerR;

            //check to see if the file has both \n and \r for end of line markers.
            //common for files comming from Unix\Linux systems.
            if (dataIn.IndexOf('\n') > 0 && dataIn.IndexOf('\r') > 0)
            {
                //if we find both just remove one of them.
                dataIn = dataIn.Replace("\n", "");
            }
            //If the file only has \n then we will use that as the EOL marker to seperate the lines.
            else if(dataIn.IndexOf('\n') > 0)
            {
                EOLMarker = EOLMarkerN; 
            }

            //How do we know the dynamic data will have Split capability?
            return dataIn.Split(EOLMarker);
        }
    }

    public class JsonResult
    {
        public JsonResult(string fileName)
        {
            Rows = new List<object>();
            FileName = fileName;
        }

        public string FileName { get; set; }
        public List<object> Rows { get; set; }
    }
}
