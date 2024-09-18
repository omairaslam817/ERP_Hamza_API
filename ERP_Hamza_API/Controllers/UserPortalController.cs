using ERP_Hamza_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_Hamza_API.Controllers
{
    public class UserPortalController : ApiController
    {
        ERP_DBEntities db = new ERP_DBEntities();
        public class PageIdModel
        {
            public int PageId { get; set; }
        }

        public class PagesPermissionRequestModel
        {
            public int UserId { get; set; }
            public List<PageIdModel> PagesIDs { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage AddRights([FromBody] List<PagesPermissionRequestModel> models)
        {
            try
            {
                if (models == null || models.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Model is null or empty");
                }

                using (var context = new ERP_DBEntities())
                {
                    foreach (var model in models)
                    {
                        foreach (var pageIdModel in model.PagesIDs)
                        {
                            var permission = new PagesPermission
                            {
                                UserId = model.UserId,
                                PageId = pageIdModel.PageId
                            };
                            context.PagesPermissions.Add(permission);
                        }
                    }
                    context.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Add Successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }




}
