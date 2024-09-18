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
using static ERP_Hamza_API.Controllers.CheckListController;

namespace ERP_Hamza_API.Controllers
{
    public class JobStagesController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();


        [HttpGet]
        public HttpResponseMessage GetCheckListStatus()
        {
            try
            {
                var query = from c in db.CheckListDatas
                            where c.CStatus == 3
                            join j in db.JobForm1 on c.FormId equals j.Id
                            join n in db.CheckListNameLists on c.CheckListId equals n.Id
                            select new
                            {
                                c.FormId,
                                j.AddressLine1,
                                j.RefNo,
                                c.CheckListId,
                                n.CheckList
                            };

                var result =  query.ToList();

                var groupedResult = result
                    .GroupBy(r => new { r.FormId, r.AddressLine1, r.RefNo })
                    .Select(g => new
                    {
                        g.Key.FormId,
                        g.Key.AddressLine1,
                        g.Key.RefNo,
                        CheckListRecord = g.Select(r => new
                        {
                            r.CheckListId,
                            r.CheckList
                        }).ToList()
                    })
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, groupedResult);
               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public class MyModelClass
        {
            public int CheckListId { get; set; }
            public int SelectedValue { get; set; }
            public int FormId { get; set; }
        }
        [HttpPost]
        public HttpResponseMessage UpdateCheckList([FromBody] List<MyModelClass> checklistItems)
        {
            try
            {
                foreach (var item in checklistItems)
                {
                    var checkListData = db.CheckListDatas.FirstOrDefault(c => c.FormId == item.FormId && c.CheckListId == item.CheckListId);
                    if (checkListData != null)
                    {
                        checkListData.CStatus = item.SelectedValue;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Check List Data not found.");
                    }
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Check List updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }






    }
}
