using ERP_Hamza_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ERP_Hamza_API.Controllers
{
    public class FunderQController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();

        [HttpPost]
        public HttpResponseMessage AddFQ(FunderQuery data)
        {
            try
            {


                db.FunderQueries.Add(data);
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

        [HttpPost]
        public HttpResponseMessage GetFQList(FunderQuery FQ) 
        {
            try
            {
                var data = db.FunderQueries.Where(m => m.FormNo == FQ.FormNo).ToList();

                var cli = data.Select(m => new
                {
                    m.Id,
                    m.FunderText,
                    m.MDate,
                    m.MTime,
                    m.MemberType,
                    m.Member,
                    m.Status
                    
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cli);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }



        [HttpPost]
        public async Task<HttpResponseMessage> UpdateStatus(List<FunderQuery> updatedQueries)
        {
            try
            {
                foreach (var updatedQuery in updatedQueries)
                {
                    var query = await db.FunderQueries.FindAsync(updatedQuery.Id);
                    if (query != null)
                    {
                        query.Status = "Booked";
                        query.MDate = updatedQuery.MDate;
                        query.MTime = updatedQuery.MTime;
                        query.MemberType = updatedQuery.MemberType;
                        query.Member = updatedQuery.Member;
                        
                    }
                }

                await db.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateStatusToDone(List<FunderQuery> updatedQueries)
        {
            try
            {
                foreach (var updatedQuery in updatedQueries)
                {
                    var query = await db.FunderQueries.FindAsync(updatedQuery.Id);
                    if (query != null)
                    {
                        query.Status = "Done";
                        
                        
                        
                        

                    }
                }

                await db.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpPost]
        public IHttpActionResult SaveFQItemsList(List<ItemDto> items)
        {
            try
            {
                
                foreach (var item in items)
                {
                    //complete the code
                var data = new FunderQuery();
                    data.FunderText = item.Text;
                    data.FunderName = item.Dropdown;
                    data.FormNo = item.FormNo;
                    data.Status = "Awaiting";
                    db.FunderQueries.Add(data);
                    db.SaveChanges();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public class ItemDto
        {
            public string Dropdown { get; set; }
            public string Text { get; set; }
            public int FormNo { get; set; }
        }








    }
}
