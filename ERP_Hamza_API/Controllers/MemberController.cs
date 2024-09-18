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
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using Microsoft.SqlServer.Server;

namespace ERP_Hamza_API.Controllers
{
    public class MemberController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();

        [HttpPost]
        public HttpResponseMessage AddMember(Member data)
        {
            try
            {
               

                db.Members.Add(data);
                db.SaveChanges();
                //var RecordId = data.Id;
                //Audit(data.Id, data.CreateBy, DateTime.Now, "Data matched");
                //AddRecordInFormJob(data.Id, 1);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetLDGen() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Lead Generator").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetMeasure() 
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Measure").ToList();
               
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetSurveyorName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Surveyor").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetPASSInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "PAS Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetLOFTInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "LOFT Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetRIRInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "RIR Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetEWIInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "EWI Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetIWIInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "IWI Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetBoilerInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Boiler Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetHCInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "HC Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetSolarPvInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Solar Pv Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetASHPInstallerName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "ASHP Installer").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetRetrifitAssessorName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Retrifit Assessor").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetRetrofitCoordinator() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Retrofit Coordinator").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetFunderName() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == "Funder Name").ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

		[HttpGet]
		public HttpResponseMessage GetCompanyName() // get all client for list  Lead Generator
		{
			try
			{
				var data = db.Members.Where(m => m.Member1 == "Company").ToList();
				var cli = data.Select(m => m.Name).ToList();
				return Request.CreateResponse(HttpStatusCode.OK, cli);

			}
			catch (Exception ex)
			{

				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

		}


		[HttpGet]
        public HttpResponseMessage GetMemberList() // get all client for list  Lead Generator
        {
            try
            {
                var data = db.Members.ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
                var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage GetMemberListByType(Member mbr) 
        {
            try
            {
                var data = db.Members.Where(m=>m.Member1==mbr.Member1).ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
               // var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost]
        public HttpResponseMessage GetMemberNamesByType(Member mbr)
        {
            try
            {
                var data = db.Members.Where(m => m.Member1 == mbr.Member1).ToList();
                // var clientlist = db.Members.OrderBy(f => f.Id).ToList();
               var cli = data.Select(m => m.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }



    }
}