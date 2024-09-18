using ERP_Hamza_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;



namespace ERP_Hamza_API.Controllers
{
    public class AuthController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();

        [HttpPost]
        public HttpResponseMessage login(Login obj)
        {
            try
            {
                var user = db.Logins.Where(m => m.Email == obj.Email && m.Password == obj.Password).FirstOrDefault();

                //var user = db.Logins.SingleOrDefault(x => x.Email == Email && x.Password == Password);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invaild Email or Password");
                }
                var responseUser = new
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role,
                   


                };
                return Request.CreateResponse(HttpStatusCode.OK, responseUser);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage Register(Login reg)
        {
            try
            {

                var existingUser = db.Logins.FirstOrDefault(u => u.Email == reg.Email);

                if (existingUser != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Email already exists");
                }
                else
                {
                    db.Logins.Add(reg);
                    db.SaveChanges();
                   // return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Success", UserId = reg.Id });
                    return Request.CreateResponse(HttpStatusCode.OK, reg.Id);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}