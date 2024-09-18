using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace ERP_Hamza_API.Controllers
{
    public class FaceController : ApiController
	{
		
		private static readonly HttpClient client = new HttpClient();
		public FaceController()
		{
			client.BaseAddress = new System.Uri("https://face-recognition26.p.rapidapi.com/");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "e9f18afe33mshae73bb79f1bc471p10c9dcjsne691100a8902");
			client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "face-recognition26.p.rapidapi.com");
		}

		[System.Web.Http.HttpPost]
		public async Task<IHttpActionResult> CompareFaces(FaceCompareRequest request)
		{
			var json = JsonConvert.SerializeObject(new
			{
				image1 = request.Image1,
				image2 = request.Image2
			});

			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("api/face_compare_base64", content);

			if (response.IsSuccessStatusCode)
			{
				var responseData = await response.Content.ReadAsStringAsync();
				return Ok(responseData);
			}
			else
			{
				return Content(response.StatusCode, response.ReasonPhrase);
			}
		}
	}





	public class FaceCompareRequest
	{
        public string Image1 { get;  set; }
        public string Image2 { get;  set; }
    }
}