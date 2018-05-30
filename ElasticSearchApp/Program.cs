using Elasticsearch.Net;
using ElasticSearchApp.Accounts;
using ElasticSearchApp.Common;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Linq.Expressions;



//{"mappings" : { "employees" : { "properties" : 

//{ "FirstName" : { "type" : "text" }, 
//"LastName" : { "type" : "text" }, 
//"Designation" : { "type" : "text" }, 
//"Salary" : { "type" : "integer" }, 
//"DateOfJoining" : 
//{ "type" : "date", "format": "yyyy-MM-dd" }, 
//"Address" : { "type" : "text" }, 
//"Gender" : { "type" : "text" }, 
//"Age" : { "type" : "integer" }, 
//"MaritalStatus" : { "type" : "text" }, 
//"Interests" : { "type" : "text" }}}}}'

public enum Gender
{
    Male,
    Female
}
public enum MaritalStatus
{
    Married,
    Unmarried,
    Divorced
}

public class Personal
{
    public string Name { get; set; }

    public Gender Gender { get; set; }

    public uint Age { get; set; }

    public string City { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string State { get; set; }

    public string Street { get; set; }
}

public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Designation { get; set; }
    public decimal Salary { get; set; }

    [Date(Format = "yyyy-MM-dd")]
    public DateTime DateOfJoining { get; set; }

    public string Address { get; set; }

    [Text]
    [JsonConverter(typeof(StringEnumConverter))]
    public Gender Gender { get; set; }

    public int Age { get; set; }

    [Text]
    [JsonConverter(typeof(StringEnumConverter))]
    public MaritalStatus MaritalStatus { get; set; }

    public string Interests { get; set; }
}

namespace ElasticSearchApp
{

 
    class Program
    {

        public static TypeMappingDescriptor<Personal> MapPersonal(TypeMappingDescriptor<Personal> personalDescriptor)
        {
            return personalDescriptor.Properties(persProps => persProps
                                .CreateTextFieldWithKeyword(pers => pers.Name)
                                .CreateTextFieldWithKeyword(pers => pers.Gender)
                                .CreateNumber(pers => pers.Age)
                                .CreateTextFieldWithKeyword(pers => pers.Email)
                                .CreateTextFieldWithKeyword(pers => pers.Phone)
                                .CreateTextFieldWithKeyword(pers => pers.Street)
                                .CreateTextFieldWithKeyword(pers => pers.City)
                                .CreateTextFieldWithKeyword(pers => pers.State));
        }

        static void Main(string[] args)
        {
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

            var settings = new ConnectionSettings(connectionPool)
                .DisableDirectStreaming();

            var client = new ElasticClient(settings);

            const string custoemrs = "customers";


			//if (client.IndexExists("companyDatabase").Exists)
			//    client.DeleteIndex("companyDatabase");

			//var rsp = client.CreateIndex("companyDatabase", i => i
			//                  .Mappings(mp => mp
			//                    .Map<Employee>(emp => emp
			//                        .AutoMap())));

			IndexBuilder<Account> indexConfig = new AccountMapping();


			var rsp = client.CreateIndex(indexConfig.IndexName, i => i
                              .Settings(indexConfig.ConfigureIndexSettings)
                              .Mappings(mp => mp
                                .Map<Account>(indexConfig.MapAccount)));




			var rspa = rsp.IsValid;

        }
    }
}
