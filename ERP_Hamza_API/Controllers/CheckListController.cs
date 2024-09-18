using ERP_Hamza_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_Hamza_API.Controllers
{
    public class CheckListController : ApiController
    {

        ERP_DBEntities db = new ERP_DBEntities();


        [HttpGet]
        public HttpResponseMessage GetCheckList1()
        {
            try
            {
                var existingData = db.CheckListNameLists.Take(15).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, existingData);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList2()
        {
            try
            {
                


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 16 && c.Id <= 47)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList3()
        {
            try
            {
                //var existingData = db.CheckListNameLists.Take(15).ToList();


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 48 && c.Id <= 56)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList4()
        {
            try
            {
                //var existingData = db.CheckListNameLists.Take(15).ToList();


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 57 && c.Id <= 76)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList5()
        {
            try
            {
                //var existingData = db.CheckListNameLists.Take(15).ToList();


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 77 && c.Id <= 102)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList6()
        {
            try
            {
                //var existingData = db.CheckListNameLists.Take(15).ToList();


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 103 && c.Id <= 121)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList7()
        {
            try
            {
                //var existingData = db.CheckListNameLists.Take(15).ToList();


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 122 && c.Id <= 157)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCheckList8()
        {
            try
            {
                //var existingData = db.CheckListNameLists.Take(15).ToList();


                var checklistItems = db.CheckListNameLists
                                         .Where(c => c.Id >= 158 && c.Id <= 174)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, checklistItems);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public class MyModelClass {

            public int FormId { get; set; }
            public int Id { get; set; } // row id
            public int SelectedValue { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage AddCheckLists([FromBody] List<MyModelClass> checklistItems)
        {
            try
            {



                var checkListDataEntities = new List<CheckListData>();

                foreach (var item in checklistItems)
                {
                    var existingRecord =  db.CheckListDatas
                        .FirstOrDefault(c => c.FormId == item.FormId && c.CheckListId == item.Id);

                    if (existingRecord != null)
                    {
                        // Update existing record
                        return Request.CreateResponse(HttpStatusCode.Conflict, "Record All Ready Exist");
                    }
                    else
                    {
                        // Add new record
                        checkListDataEntities.Add(new CheckListData
                        {
                            FormId = item.FormId,
                            CheckListId = item.Id,
                            CStatus = item.SelectedValue
                        });
                    }
                }

                if (checkListDataEntities.Any())
                {
                    db.CheckListDatas.AddRange(checkListDataEntities);
                    db.SaveChangesAsync();
                }

                 
                return Request.CreateResponse(HttpStatusCode.OK, "Check List Add Successfully.");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public IHttpActionResult GetCheckListDataByFormId([FromBody]CheckListData obj)
        {
            var data = (from cld in db.CheckListDatas
                        join cln in db.CheckListNameLists
                        on cld.CheckListId equals cln.Id
                        where cld.FormId == obj.FormId
                        select new
                        {
                            cld.Id,
                            cld.FormId,
                            cld.CStatus,
                            cld.CheckListId,
                            cln.CheckList
                        }).ToList();

            if (data.Count == 0)
            {
                return NotFound();
            }

            var data1to15 = data.Where(d => d.CheckListId >= 1 && d.CheckListId <= 15).ToList();
            var data16to47 = data.Where(d => d.CheckListId >= 16 && d.CheckListId <= 47).ToList();
            var data48to56 = data.Where(d => d.CheckListId >= 48 && d.CheckListId <= 56).ToList();
            var data57to76 = data.Where(d => d.CheckListId >= 57 && d.CheckListId <= 76).ToList();
            var data77to102 = data.Where(d => d.CheckListId >= 77 && d.CheckListId <= 102).ToList();
            var data103to121 = data.Where(d => d.CheckListId >= 103 && d.CheckListId <= 121).ToList();
            var data122to157 = data.Where(d => d.CheckListId >= 122 && d.CheckListId <= 157).ToList();
            var data158to174 = data.Where(d => d.CheckListId >= 158 && d.CheckListId <= 174).ToList();

            // Return grouped data
            var groupedData = new
            {
                Data1to15 = data1to15,
                Data16to47 = data16to47,
                Data48to56 = data48to56,
                Data57to76 = data57to76,
                Data77to102 = data77to102,
                Data103to121 = data103to121,
                Data122to157 = data122to157,
                Data158to174 = data158to174
            };

            return Ok(groupedData);
        }















        //last 
    }
}
