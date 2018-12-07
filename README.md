# FunctionAppCSVToJSON

Azure Function App [V2](https://docs.microsoft.com/en-us/azure/azure-functions/functions-versions) that converts CSV data to JSON

I Forked this from [aaronralls](https://github.com/aaronralls/FunctionAppCSVToJSON) originally and changed the convert function to leverage [csvhelper](https://joshclose.github.io/CsvHelper/). This Azure Function as part of a larger solution to process incomming SFTP files.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Sample Input

The JSON the function accepts consists of the following body fields.

fileName - This is the source file where the data came from. This is passed in for downstream processes that may need to know which file was processed.

csv - This is the raw data from the source file that you want to be converted to JSON. This content may contain \r\n or \n end of line markers. The function will detect them an process the csv data accordingly.

```
{
  "fileName": "MyTestCSVFile.csv",
  "csv":"ID,Name,Score
1,Aaron,99
2,Dave,55
3,Susy,77
"
}
```

### Sample Output

fileName - This is the source file where the data came from. This is passed in for downstream processes that may need to know which file was processed.

rows - list of the CSV data in JSON format, with the field name from the header row.

```
{
    "fileName":"MyTestCSVFile.csv",
    "rows":[
      {
        "ID":"1",
        "Name":"Aaron",
        "Score":"99"
       },
       {
        "ID":"2",
        "Name":"Dave",
        "Score":"55"
       },
       {
        "ID":"3",
        "Name":"Susy",
        "Score":"77"
        }]
}

```
### Prerequisites

What things you need to install the software and how to install them

```
Visual Studio Code
Postman v6.0.7
Azure Subscription
Azure funciton app service
```

### Installing

A step by step series of examples that tell you have to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Postman variables

These variables used in the Postman tests.

url - This is the URI of the Azure Function that you have published to or use for local testing. (ie: localhost:7071)
This variable is in each [test collection](https://github.com/houstonjb/FunctionAppCSVToJSON/Postman%20Tests). Be sure to update them both.

```
"variable": [
		{
			"id": "10226ddb-3af2-4d88-a4da-efff2ffed2b3",
			"key": "url",
			"value": "localhost:7071",
			"type": "text",
			"description": ""
		}
	]
```
Or
```
variable": [
		{
			"id": "34f41e2c-c10f-46e8-b08f-bb1d3be4b714",
			"key": "url",
			"value": "YOUR_FUNCTION_APP_NAME_GOES_HERE.azurewebsites.net",
			"type": "text",
			"description": ""
		}
```

functions-key - This is the Function Key that you can use to limit access to your Azure functions.
This variable is only in the [Azure test collection](https://github.com/houstonjb/FunctionAppCSVToJSON/Postman%20Tests/FunctionAppCSVToJSON%20Azure.postman_collection.json).

```
"variable": [
		....,
		{
			"id": "f7592072-64a6-4a15-b5d5-77c13b06487c",
			"key": "functions-key",
			"value": "YOUR_KEY_GOES_HERE",
			"type": "string",
			"description": ""
		}
	]
```

### Break down into end to end tests



#### Azure Testing Collection

FunctionAppCSVToJSON Azure

```
POST to CSVToJSON with Windows text file contents

Negative Test: POST to CSVToJSON with Windows text file contents, no filename

CSVToJSON swagger
```

## Deployment

deploy to Azure function app using your favorite deployment methods.

## Built With

.net core standard version 2.1 using Visual Studio Code and macOS. 

## Contributing

send it.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [releases on this repository](https://github.com/houstonjb/FunctionAppCSVToJSON/releases). 

## Authors

* **Aaron Ralls** - *Initial work* - [Aaron Ralls](https://github.com/AaronRalls)

* **houstonjb** - *Modified* - [houstonjb](https://github.com/houstonjb)

See also the list of [contributors](https://github.com/houstonjb/FunctionAppCSVToJSON/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details

## Acknowledgments

* This is based on the [CSVToJSON API](https://github.com/jeffhollan/CSVtoJSON) created by [Jeff Hollan](https://github.com/jeffhollan)

* etc

## Resources ##

- [Azure Functions Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)
