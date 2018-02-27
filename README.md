# FunctionAppCSVToJSON

Azure Function App that converts CSV data to JSON

I created this Azure Function as part of a larger solution to process incomming SFTP files. One possible soltion is to convert the CSV data to JSON and then perform Azure [SQL Bulk OPENROWSETS Bulk Inserts](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2015/10/07/bulk-importing-json-files-into-sql-server/).

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Sample Input

The JSON the function accepts consists of the followning fields.

rowsToSkip - This indicates the number of rows to skip in the conversion process.

fileName - This is the source file where the data came from. This is passed in for down stream processes that may need to know which file was processed.

csv - This is the raw data from the source file that you want converted to JSON.
This content may contain \r\n or \n end of line markers. The function will detect them an process the csv data accordingly.

```
{
  "rowsToSkip": 1,
  "fileName": "MyTestCSVFile.csv",
  "csv":"ID,Name,Score
1,Aaron,99
2,Dave,55
3,Susy,77
"
}
```

### Sample Output

fileName - This is the source file where the data came from. This is passed in for down stream processes that may need to know which file was processed.

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
Visual Studio 15.5.7 
Postman v6.0.7
```
Download [Postman v6.0.7](https://www.getpostman.com/) 

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
url - This is the URI of the Azure Function that you have published to or use for local testing. (ie: localhost:7071)

functions-key - This is the Function Key that you can use to limit access to your Azure functions.

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With



## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/AaronRalls/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/AaronRalls/FunctionAppCSVToJSON/tags). 

## Authors

* **Aaron Ralls** - *Initial work* - [Aaron Ralls](https://github.com/AaronRalls)

See also the list of [contributors](https://github.com/AaronRalls/FunctionAppCSVToJSON/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* This is based on the [CSVToJSON API](https://github.com/jeffhollan/CSVtoJSON) created by [Jeff Hollan](https://github.com/jeffhollan)

* etc

