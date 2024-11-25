using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GPS.API.Data.Models
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}

//Multitenancy how it works.
//The class Tenant stores informations about all tenants, their Id and name. 
//All classes that we want to use multitenancy with have to implement the IMustHaveTenant interface.
//Implementing that class adds a TenantId field and also groups the classes for the override of SaveChanges,
//which when saving new data sets an entities tenantId to the currentTenantService id.
//The interface and class ICurrentTenantService and CurrentTenantService are added to the app as a service,
//and their purpose is to set the Id of the tenant currently using the application. 
//The way it sets that value is using the TenantResolver middleware
//When any request is made the middleware checks the authorization field for a JWT token which among other data also has a TenantId field.
//After getting the Id out of the header it then calls the CurrentTennatService to set the tenant.
//The way the JWT Token is generated is when a user logs in and registers.
//Those methods dont have a authorization header and instead set the tenant id themselves. 
//After generating a JWT token every other API request needs to include that token in its header as the Authorization field,
//requests that dont have that field will not be given an response. 
//After a valid JWT token is passed and the CurrentTenantSerivce sets the TenantId the data is filtered on the DB level using querry fileters,
//meaning only data whose TenantId matches the CurrentTenantService Id will be retrieved from the database
