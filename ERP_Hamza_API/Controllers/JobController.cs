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
using Newtonsoft.Json;

namespace ERP_Hamza_API.Controllers
{
    public class JobController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();

        [HttpPost]
        public HttpResponseMessage AddJob(JobForm1 data)
        {
            try
            {
                var existingRecord = db.JobForm1.FirstOrDefault(job => job.RefNo == data.RefNo);

                if (existingRecord != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "RefNo already exists");
                }

                db.JobForm1.Add(data);
                db.SaveChanges();
                var RecordId = data.Id;
                Audit(data.Id, data.CreateBy, DateTime.Now, "Data matched");
                AddRecordInFormJob(data.Id, 1);
                return Request.CreateResponse(HttpStatusCode.OK, data.Id);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJob(JobForm1 data)// updates status 1 to 2 
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }
                db.Entry(jobToUpdate).CurrentValues.SetValues(data);
                db.SaveChanges();

                Audit(jobToUpdate.Id, data.CreateBy, DateTime.Now, "Survey Booked");
                DeleteRecordInFormJob(jobToUpdate.Id, 1);
                AddRecordInFormJob(jobToUpdate.Id, 2);
                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }









        [HttpPost]
        public HttpResponseMessage UpdateJobBooked([FromBody] JobForm1 data) // change 2 to 3
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 3;
                db.SaveChanges();
                Audit(jobToUpdate.Id, data.CreateBy, DateTime.Now, "Survey Done");
                DeleteRecordInFormJob(jobToUpdate.Id, 2);
                AddRecordInFormJob(jobToUpdate.Id, 3);
                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

		[HttpPost]
		public HttpResponseMessage UpdateJobTo4([FromBody] JobForm1 data)
		{
			try
			{
				var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

				if (jobToUpdate == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
				}

		

				jobToUpdate.EprJobContrbText = data.EprJobContrbText;
				jobToUpdate.EprReportText = data.EprReportText;
				jobToUpdate.Status = 4;
				db.SaveChanges();

				return Request.CreateResponse(HttpStatusCode.OK, "Job updated successfully");
			}
			catch (Exception ex)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}


		[HttpPost]
        public HttpResponseMessage UpdateJobTo5([FromBody] JobForm1 data) // change 4  to 5
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }
                jobToUpdate.EprJobContrbText = data.EprJobContrbText;
                jobToUpdate.EprReportText = data.EprReportText;
                jobToUpdate.Status = 5;
                db.SaveChanges();
                // Audit(jobToUpdate.Id, data.CreateBy, DateTime.Now, "Job Contribution");
                // DeleteRecordInFormJob(jobToUpdate.Id, 4);
                // AddRecordInFormJob(jobToUpdate.Id, 5);
                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo6([FromBody] JobForm1 data) // change 5 to 6
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 6;
                db.SaveChanges();
                // Audit(jobToUpdate.Id, data.CreateBy, DateTime.Now, "Customer agreed");
                //  DeleteRecordInFormJob(jobToUpdate.Id, 5);
                //  AddRecordInFormJob(jobToUpdate.Id, 6);
                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage UpdateJobTo7([FromBody] JobForm1 data) // change 6 to 7
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 7;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage UpdateJobToPassInsta([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 28;
                jobToUpdate.PassInstaDate = data.PassInstaDate;
                jobToUpdate.PassInstaName = data.PassInstaName;
                jobToUpdate.PassInstaTime = data.PassInstaTime;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobToPassInstaTo28([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 8;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }







        [HttpPost]
        public HttpResponseMessage UpdateJobToLOFT([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 29;
                jobToUpdate.LoftDate = data.LoftDate;
                jobToUpdate.LoftName = data.LoftName;
                jobToUpdate.LoftTime = data.LoftTime;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateJobToLOFT29([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 29;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpPost]
        public HttpResponseMessage UpdateJobToRIR([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 30;
                jobToUpdate.RIRDate = data.RIRDate;
                jobToUpdate.RIRName = data.RIRName;
                jobToUpdate.RIRTime = data.RIRTime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateJobToRIR30([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 30;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





        [HttpPost]
        public HttpResponseMessage UpdateJobToEWI([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 27;
                jobToUpdate.EWIDate = data.EWIDate;
                jobToUpdate.EWIName = data.EWIName;
                jobToUpdate.EWITime = data.EWITime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //----------------------------------------- for EWI -------------------------------------------------
        [HttpPost]
        public HttpResponseMessage UpdateJobToEWITo27([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 27;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //----------------------------------------- for EWI -------------------------------------------------
        [HttpPost]
        public HttpResponseMessage UpdateJobToIWI([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 31;
                jobToUpdate.IWIName = data.IWIName;
                jobToUpdate.IWIDate = data.IWIDate;

                jobToUpdate.IWITime = data.IWITime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //----------------------------------------- for EWI -------------------------------------------------

        [HttpPost]
        public HttpResponseMessage UpdateJobToIWITo31([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 27;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        //----------------------------------------- for EWI -------------------------------------------------











        [HttpPost]
        public HttpResponseMessage UpdateJobToBoiler([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 32;
                jobToUpdate.BoilerDate = data.BoilerDate;
                jobToUpdate.BoilerName = data.BoilerName;
                jobToUpdate.BoilerTime = data.BoilerTime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateJobToBoilerTo32([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 32;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }






        [HttpPost]
        public HttpResponseMessage UpdateJobToHC([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 33;
                jobToUpdate.HCDate = data.HCDate;
                jobToUpdate.HCName = data.HCName;
                jobToUpdate.HCTime = data.HCTime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateJobToHC33([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 33;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }






        [HttpPost]
        public HttpResponseMessage UpdateJobToSolar([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 34;
                jobToUpdate.SolarName = data.SolarName;
                jobToUpdate.SolarDate = data.SolarDate;

                jobToUpdate.SolarTime = data.SolarTime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateJobToSolar34([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 34;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }






        [HttpPost]
        public HttpResponseMessage UpdateJobToAshp([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 35;
                jobToUpdate.AshipDate = data.AshipDate;
                jobToUpdate.AshipName = data.AshipName;
                jobToUpdate.AshipTime = data.AshipTime;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobToAshp35([FromBody] JobForm1 data) //installation done
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 35;


                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





        [HttpPost]
        public HttpResponseMessage UpdateJobTo8([FromBody] JobForm1 data) // change 7 to 8
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 8;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo9([FromBody] JobForm1 data) // change 8 to 9
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 9;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo10([FromBody] JobForm1 data) // change 5 to 7
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 10;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo17([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }
                jobToUpdate.IpadQAccessorNameRcInst = data.IpadQAccessorNameRcInst;
                jobToUpdate.Status = 17;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo18([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 18;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo19([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.RcInstructSideNote = data.RcInstructSideDate;
                jobToUpdate.RcInstructSideDate = data.RcInstructSideDate;
                jobToUpdate.Status = 19;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo20([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 20;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo21([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 21;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo22([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 22;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo23([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.FunderName = data.FunderName;
                jobToUpdate.FunderQText = data.FunderQText;
                jobToUpdate.Status = 23;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTo24([FromBody] JobForm1 data) // change 8 to 17
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 24;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //----------------------------------------- for ipad -------------------------------------------------
        [HttpPost]
        public HttpResponseMessage UpdateJobToIPAD25([FromBody] JobForm1 data) // for ipad
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }
                jobToUpdate.IpadQAccessorName = data.IpadQAccessorName;
                jobToUpdate.Status = 25;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateJobToIPAD26([FromBody] JobForm1 data) // for ipad - to 17 Rc Inst
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.Status = 26;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        //-----------------------------------------for ipad -------------------------------------------------

        [HttpGet]
        public HttpResponseMessage AllForm()
        {
            try
            {
                var pendingItems = db.JobForm1
             .ToList();


                if (pendingItems == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, pendingItems);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpGet]
        public HttpResponseMessage AllFormsCount() //get count of status 
        {
            try
            {
                using (var context = new ERP_DBEntities())
                {
                    var statusCounts = context.FormStatus
                .GroupBy(fs => fs.Status)
                .Select(g => new { StatusId = g.Key, Count = g.Count() })
                .ToDictionary(x => "status" + x.StatusId, x => x.Count);



                    return Request.CreateResponse(HttpStatusCode.OK, statusCounts);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
		[HttpPost]
		public HttpResponseMessage SingleFormsCount([FromBody]FormStatu obj) //get count of status 
		{
			try
			{
				using (var context = new ERP_DBEntities())
				{

					var statusCounts = context.FormStatus
                        .Where(fs => fs.FormId == obj.FormId)
				.GroupBy(fs => fs.Status)
				.Select(g => new { StatusId = g.Key, Count = g.Count() })
				.ToDictionary(x => "status" + x.StatusId, x => x.Count);



					return Request.CreateResponse(HttpStatusCode.OK, statusCounts);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}



		[HttpGet]
        public HttpResponseMessage Form1RefNo()
        {
            try
            {
                var refNos = db.JobForm1.Select(j => j.RefNo).ToList();


                if (refNos == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, refNos);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        public HttpResponseMessage FindByRefNo(JobForm1 obj) //list of RefNo
        {
            try
            {
                var form1 = db.JobForm1.Where(m => m.RefNo == obj.RefNo).FirstOrDefault();

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
        public HttpResponseMessage FindByRefNo2(JobForm1 obj) //list of RefNo
        {
            try
            {
                var jobForm1Records = db.JobForm1.Where(j => j.Id == obj.Id).ToList();
                var auditRecords = db.Audits.Where(a => a.FormOrId == obj.Id).ToList();
                var fromStatusRecords = db.FormStatus.Where(fs => fs.FormId == obj.Id).ToList();
                var result = new
                {
                    JobForm1Records = jobForm1Records,
                    AuditRecords = auditRecords,
                    FromStatusRecords = fromStatusRecords
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //------------------------------------------------------------------------------------------------

        [HttpPost]
        public HttpResponseMessage AddRecordInFormJob(int formid, int statusid)
        {
            try
            {
                //before saving check if the record already exists
                var existingRecord = db.FormStatus.FirstOrDefault(fs => fs.FormId == formid && fs.Status == statusid);
                if (existingRecord != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Already exists");
                }

                var Entry = new FormStatu
                {
                    FormId = formid,
                    Status = statusid

                };
                db.FormStatus.Add(Entry);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Entry saved successfully.");




            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public class FormData
        {
            public int FormNo { get; set; }
            public List<int> StatusIds { get; set; }
        }
        [HttpPost]
        public HttpResponseMessage AddMultipleRecordInFormJob([FromBody] FormData formData)
        {
            try
            {
                if (formData == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request data");
                }

                if (formData.StatusIds == null || formData.StatusIds.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "StatusIds list cannot be empty");
                }

                // Example logic to add records to the database
                using (var dbContext = new ERP_DBEntities())
                {
                    foreach (var statusId in formData.StatusIds)
                    {
                        dbContext.FormStatus.Add(new FormStatu
                        {
                            FormId = formData.FormNo,
                            Status = statusId
                        });
                    }

                    dbContext.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Move Successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteRecordInFormJob(int formid, int statusid)
        {
            try
            {
                var formStatus = db.FormStatus
                .FirstOrDefault(fs => fs.FormId == formid && fs.Status == statusid);

                if (formStatus == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }

                db.FormStatus.Remove(formStatus);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }





        //-----------------------------------------------------------------------------------------------------

        [HttpGet]
        public HttpResponseMessage Form1BySat1()//get form1 by status 1
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 1)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //[HttpGet]
        //public HttpResponseMessage Form1BySat1()//get form1 by status 2
        //{
        //	try
        //	{
        //		//var refNos2 = db.JobForm1.Where(m => m.Satetus == 2).ToList();

        //		var refNos = db.JobForm1
        //			.Where(m => m.Status == 1)
        //			.Select(m => new { m.Id, m.RefNo, m.SurveyName, m.AddressLine1 }) // Replace with your desired properties
        //			.ToList();


        //		if (refNos == null)
        //		{
        //			return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
        //		}
        //		return Request.CreateResponse(HttpStatusCode.OK, refNos);


        //	}
        //	catch (Exception ex)
        //	{

        //		return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //	}

        //}

        [HttpGet]
        public HttpResponseMessage Form1BySat2()//get form1 by status 2
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 2)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat3()//get form1 by status 3
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 3)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat4()//get form1 by status 4
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 4)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat5()//get form1 by status 5
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 5)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //[HttpGet]
        //public HttpResponseMessage Form1BySat6() // get form1 by status 6
        //{
        //    try
        //    {
        //        using (var context = new ERP_DBEntities())
        //        {
        //            var records = context.FormStatus
        //                .Where(fs => fs.Status == 6)
        //                .Join(
        //                    context.JobForm1,
        //                    fs => fs.FormId,
        //                    jf => jf.Id,
        //                    (fs, jf) => new
        //                    {
        //                        fs.Id,
        //                        fs.FormId,
        //                        fs.Status,
        //                        JobFormRecord = jf
        //                    })
        //                .Join(
        //                    context.TrackRCInstStates,
        //                    fs_jf => fs_jf.FormId,
        //                    trc => trc.FormNo,
        //                    (fs_jf, trc) => new
        //                    {
        //                        fs_jf.Id,
        //                        fs_jf.FormId,
        //                        fs_jf.Status,
        //                        fs_jf.JobFormRecord,
        //                        TrackRCInstStateId = trc.Id,
        //                        TrackRCInstStateName = trc.Name
        //                    })
        //                .GroupBy(r => new
        //                {
        //                    r.Id,
        //                    r.FormId,
        //                    r.Status,
        //                    r.JobFormRecord
        //                })
        //                .Select(g => new
        //                {
        //                    g.Key.Id,
        //                    g.Key.FormId,
        //                    g.Key.Status,
        //                    g.Key.JobFormRecord,
        //                    TrackRecords = g.Select(tr => new
        //                    {
        //                        tr.TrackRCInstStateId,
        //                        tr.TrackRCInstStateName
        //                    }).ToList()
        //                })
        //                .ToList();

        //            return Request.CreateResponse(HttpStatusCode.OK, records);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage Form1BySat6()//get form1 by status 5
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 6)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }



        [HttpGet]
        public HttpResponseMessage Form1BySat7()//get form1 by status 6
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 7)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat8()//get form1 by status 6
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 8)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat9()//get form1 by status 6
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 9)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat10()//get form1 by status 6
        {
            try
            {



                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 10)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat11()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 11)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat12()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 12)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat13()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 13)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat14()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 14)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat15()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 15)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat16()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 16)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat17()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 17)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat18()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 18)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat19()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 19)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat20()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 20)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat21()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 21)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat22()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 22)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat23()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 23)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat24()//get form1 by status 6
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 24)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat26()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 26)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat28()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 28)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat30()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 30)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat31()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 31)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpGet]
        public HttpResponseMessage Form1BySat32()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 32)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat34()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 34)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat36()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 36)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat38()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 38)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat40()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 40)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpGet]
        public HttpResponseMessage Form1BySat42()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 42)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat43()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 43)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat44()//get form1 by status 26
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 44)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Form1BySat45()//get form1 by status 45
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 45)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage Form1BySat46() // get form1 by status 46
        {
            try
            {
                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 46)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .Join(
                            context.AssignedJobDetails,
                            fs_jf => fs_jf.FormId,
                            atjd => atjd.FormNo,
                            (fs_jf, atjd) => new
                            {
                                fs_jf.Id,
                                fs_jf.FormId,
                                fs_jf.Status,
                                fs_jf.JobFormRecord,
                                AssignedToJobDetailRecord = atjd
                            })
                        .ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    
    [HttpGet]
        public HttpResponseMessage Form1BySat47()//get form1 by status 45
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 47)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage Form1BySat48()//get form1 by status 45
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 48)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage Form1BySat49()//get form1 by status 45
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 49)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage Form1BySat50()//get form1 by status 45
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 50)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage Form1BySat51()//get form1 by status 45
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == 51)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        public HttpResponseMessage Form1BySatId([FromBody] FormStatu obj)//get form1 by status id universal
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                        .Where(fs => fs.Status == obj.Status)
                        .Join(
                            context.JobForm1,
                            fs => fs.FormId,
                            jf => jf.Id,
                            (fs, jf) => new
                            {
                                fs.Id,
                                fs.FormId,
                                fs.Status,
                                JobFormRecord = jf
                            })
                        .ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }



            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }



        [HttpPost]
        public HttpResponseMessage Audit(int formid, string ActionBy, DateTime Adate, string Action)
        {
            try
            {

                var auditEntry = new Audit
                {
                    FormOrId = formid,
                    ActionBy = ActionBy,
                    ADate = Adate,
                    Action = Action
                };
                db.Audits.Add(auditEntry);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Audit entry saved successfully.");

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetUserList() // get all Member for list 
        {
            try
            {
                var makelist = db.Members.OrderBy(f => f.Id).ToList();
                var mkes = makelist.Select(m => m.Name).ToList();
                if (makelist == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, mkes);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }



        [HttpGet]
        public HttpResponseMessage GetJobAddress()
        {
            try
            {

                var jobToUpdate = db.JobForm1.Select(j => j.AddressLine1).ToList();

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, jobToUpdate);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetJobForRcInst()
        {
            try
            {
                using (var context = new ERP_DBEntities())
                {
                    var records = context.FormStatus
                .Where(fs => fs.Status == 17)
                .Join(
                    context.JobForm1,
                    fs => fs.FormId,
                    jf => jf.Id,
                    (fs, jf) => new
                    {
                        fs.Id,
                        fs.FormId,
                        fs.Status,
                        JobFormRecord = jf
                    })
                .GroupJoin(
                    context.TrackRCInstStates,
                    jf => jf.FormId,
                    tr => tr.FormNo,
                    (jf, trGroup) => new
                    {
                        jf.Id,
                        jf.FormId,
                        jf.Status,
                        jf.JobFormRecord,
                        TrackRCSRecords = trGroup.ToList()
                    })
                .ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        [HttpPost]
        public HttpResponseMessage PostTrackRCInstState([FromBody] TrackRCInstStateRequestDto requestDto)
        {

            try
            {
                for (int i = 0; i < requestDto.SelectedJobs.Count; i++)
                {
                    var entity = new TrackRCInstState
                    {
                        FormNo = requestDto.FormNo,
                        StateId = requestDto.SelectedJobs[i],
                        Name = requestDto.SelectedJobNames[i],
                        IsBooked = false // Set default value as false
                    };

                    db.TrackRCInstStates.Add(entity);
                }
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public class CHKLModel
        {
            public int Id { get; set; }
            public bool? ChkL1 { get; set; }
            public bool? ChkL2 { get; set; }
            public bool? ChkL3 { get; set; }
            public bool? ChkL4 { get; set; }
            public bool? ChkL5 { get; set; }
            public bool? ChkL6 { get; set; }
            public bool? ChkL7 { get; set; }
            public bool? ChkL8 { get; set; }
            public bool? IsGDGC { get; set; }
            public bool? isPostStateReq { get; set; }
            public bool? isLogemnt { get; set; }

        }

        [HttpPost]
        public HttpResponseMessage UpdateCHKL([FromBody] CHKLModel model)
        {

            try
            {
                var entity = db.JobForm1.Find(model.Id);
                if (entity == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
                }
                if (model.ChkL1.HasValue)
                {
                    entity.ChkL1 = model.ChkL1.Value;
                }

                if (model.ChkL2.HasValue)
                {
                    entity.ChkL2 = model.ChkL2.Value;
                }

                if (model.ChkL3.HasValue)
                {
                    entity.ChkL3 = model.ChkL3.Value;
                }

                if (model.ChkL4.HasValue)
                {
                    entity.ChkL4 = model.ChkL4.Value;
                }

                if (model.ChkL5.HasValue)
                {
                    entity.ChkL5 = model.ChkL5.Value;
                }

                if (model.ChkL6.HasValue)
                {
                    entity.ChkL6 = model.ChkL6.Value;
                }

                if (model.ChkL7.HasValue)
                {
                    entity.ChkL7 = model.ChkL7.Value;
                }

                if (model.ChkL8.HasValue)
                {
                    entity.ChkL8 = model.ChkL8.Value;
                }

                if (model.IsGDGC.HasValue)
                {
                    entity.IsGDGC = model.IsGDGC.Value;
                }
                if (model.isPostStateReq.HasValue)
                {
                    entity.isPostStateReq = model.isPostStateReq.Value;
                }

                if (model.isLogemnt.HasValue)
                {
                    entity.isLogemnt = model.isLogemnt.Value;
                }
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [HttpPost]
        public HttpResponseMessage UpdateBooked(int formid, int statusid, string name)
        {

            try
            {
                var jobToUpdate = db.TrackRCInstStates.FirstOrDefault(j => j.FormNo == formid && j.StateId == statusid && j.Name == name);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
                }

                jobToUpdate.IsBooked = true;
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IHttpActionResult AddMeasures(List<MeasureDto> measures)
        {
            try
            {
                foreach (var measure in measures)
                {
                    var newMeasure = new PostStateReqData 
                    {
                        Measure = measure.Item,
                        StartDate = measure.StartDate,
                        EndDate = measure.EndDate,
                        FormId = measure.FormId
                    };

                    db.PostStateReqDatas.Add(newMeasure);
                }

                db.SaveChanges();

                return Ok("Data added successfully");
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    

    public class MeasureDto
    {
        public string Item { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int FormId { get; set; }
    }
        [HttpPost]
        public IHttpActionResult SaveTMQItemsList(List<TMQuery> items)
        {
            try
            {

                foreach (var item in items)
                {

                    var data = new TMQuery();
                    data.TextA = item.TextA;
                    data.TextB = item.TextB;
                    data.FormNo = item.FormNo;
                    data.Status = item.Status;

                    db.TMQueries.Add(data);
                    db.SaveChanges();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public class IdModel
        {
            public int Id { get; set; }
        }
        [HttpPost]
        public IHttpActionResult DeleteTRCSRecords([FromBody] List<IdModel> ids)
        {
            try
            {
                var idsList = ids.Select(x => x.Id).ToList();
                var recordsToDelete = db.TrackRCInstStates.Where(record => idsList.Contains(record.Id)).ToList();

                if (!recordsToDelete.Any())
                {
                    return NotFound();
                }

                db.TrackRCInstStates.RemoveRange(recordsToDelete);
                db.SaveChanges();

                return Ok("Records deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpPost]
        public HttpResponseMessage AutoIdsDelete([FromBody] FormStatu obj)//get form1 by status id universal
        {
            try
            {

                using (var context = new ERP_DBEntities())
                {
                    // List of IDs to be deleted
                    var idsToDelete = new int?[] { 43, 27, 29, 31, 33, 35, 37, 41 };
                    var recordsToDelete = context.FormStatus
                                                 .Where(record => idsToDelete.Contains(record.Status) && record.FormId == obj.FormId)
                                                 .ToList();
                    context.FormStatus.RemoveRange(recordsToDelete);
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }




            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetRefAddress()
        {
            try
            {
                using (var context = new ERP_DBEntities())
                {
                //select refno and address from jobform1
                var records = context.JobForm1
                    .Select(jf => new
                    {   jf.Id,
                        jf.RefNo,
                        jf.AddressLine1
                    }).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, records);
                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobFunderName2([FromBody] JobForm1 data) // for ipad - to 17 Rc Inst
        {
            try
            {

                var jobToUpdate = db.JobForm1.FirstOrDefault(j => j.Id == data.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                }

                jobToUpdate.FunderName2 = data.FunderName2;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Job update successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


		[HttpPost]
		public HttpResponseMessage AssignToInspection(AssignedJobDetail data)
		{
			try
			{
				var existingRecord = db.AssignedJobDetails.FirstOrDefault(job => job.FormNo == data.FormNo);

				if (existingRecord != null)
				{
					return Request.CreateResponse(HttpStatusCode.Conflict, "Record already exists");
				}

				db.AssignedJobDetails.Add(data);
				db.SaveChanges();
				
				return Request.CreateResponse(HttpStatusCode.OK);

			}
			catch (Exception ex)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
        [HttpPost]
        public HttpResponseMessage UpdateAssignedJobStatus(AssignedJobDetail jd)
        {
            try
            {
                // Find the job by FormNo
                var jobToUpdate = db.AssignedJobDetails.FirstOrDefault(j => j.Id == jd.Id);

                if (jobToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found.");
                }

                // Check if the status is already the same
                if (jobToUpdate.Status == jd.Status)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Status already in " + jd.Status);
                }

                // Update the status and save changes
                jobToUpdate.Status = jd.Status;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Status updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }












    }
}