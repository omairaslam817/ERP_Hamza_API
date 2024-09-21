using ERP_Hamza_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_Hamza_API.Controllers
{
    public class SideBarController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();

        [HttpPost]
        public HttpResponseMessage FindByLeadGen(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.LeadGenerator == obj.LeadGenerator).ToList();

                if (form1 == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        public HttpResponseMessage FindByPassInstaName(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.PassInstaName !=null && m.PassInstaName == obj.PassInstaName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage FindByLoftName(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.LoftName != null && m.LoftName == obj.LoftName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage FindByRIRName(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.RIRName != null && m.RIRName == obj.RIRName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage FindByEWIName(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.EWIName != null && m.EWIName == obj.EWIName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage FindByIWIName(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.IWIName != null && m.IWIName == obj.IWIName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost]
        public HttpResponseMessage FindByBoilerName(JobForm1 obj)
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.BoilerName != null && m.BoilerName == obj.BoilerName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost]
        public HttpResponseMessage FindByHCName(JobForm1 obj)
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.HCName != null && m.HCName == obj.HCName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage FindBySolarName(JobForm1 obj)
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.SolarName != null && m.SolarName == obj.SolarName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage FindAshipName(JobForm1 obj)
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.AshipName != null && m.AshipName == obj.AshipName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, form1);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
		[HttpPost]
		public HttpResponseMessage GetJobData(JobForm1 obj)
		{
			try
			{
				var jobForm1Records = db.JobForm1.Where(j => j.Id == obj.Id).ToList();
				var auditRecords = db.Audits.Where(a => a.FormOrId == obj.Id).ToList();
				var fromStatusRecords = db.FormStatus.Where(fs => fs.FormId == obj.Id).ToList();
                var postStageDatesRecords = db.PostStateReqDatas.Where(fs=>fs.FormId==obj.Id).ToList();
                var funderQueryRecords = db.FunderQueries.Where(fs=>fs.FormNo==obj.Id).ToList();
                var tmQueryRecords = db.TMQueries.Where(fs=>fs.FormNo==obj.Id).ToList();
                var jobForm1Notes= db.JobForm1Notes.Where(fs=>fs.JobFormId==obj.Id).ToList();

				var result = new
				{
					JobForm1Records = jobForm1Records,
					AuditRecords = auditRecords,
					FromStatusRecords = fromStatusRecords,
                    PostStageDatesRecords= postStageDatesRecords,
					FunderQueryRecords= funderQueryRecords,
                    TMQueryRecords= tmQueryRecords,
                    JobForm1Notes = jobForm1Notes
                };

				return Request.CreateResponse(HttpStatusCode.OK, result);

			}
			catch (Exception ex)
			{

				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

		}


		[HttpPost]
		public HttpResponseMessage UpdateJobNote(JobForm1 obj)
		{
			try
			{
				var jobEntity = db.JobForm1.FirstOrDefault(j => j.Id == obj.Id);
				if (jobEntity != null)
				{
					jobEntity.JobNote = obj.JobNote; 
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.OK, "Jobnote updated successfully");
				}
				else
				{

					return Request.CreateResponse(HttpStatusCode.NotFound, "Job with ID 3 not found");
				}

			}
			catch (Exception ex)
			{

				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

		}

		[HttpPost]
		public HttpResponseMessage AddJobNote(JobForm1 jobForm1)
		{
			try
			{
				if (!string.IsNullOrEmpty(jobForm1.JobNote))
				{
					JobForm1Notes newJobNote = new JobForm1Notes
                    {
						Note = jobForm1.JobNote,
					    CreatedBy = jobForm1.CreateBy,
						CDate = DateTime.Now,
					    JobFormId = jobForm1.Id
					};

					db.JobForm1Notes.Add(newJobNote);

					db.SaveChanges();

					return Request.CreateResponse(HttpStatusCode.OK, "Job note added successfully");
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.NotFound, $"Job with ID not found");
				}
			}
			catch (Exception ex)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}














	}
}
