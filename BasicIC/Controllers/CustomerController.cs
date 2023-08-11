using BasicIC.Models.Main.M03;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BasicIC.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44332/api/customer/get-all", new PagingParam());

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ResponseService<ListResult<CustomerModel>>>();
                    if (apiResponse.status)
                    {
                        customers = apiResponse.data.items; // Assuming Items is the property containing the list of customers
                    }
                    else
                    {
                        // Handle error response from the API
                    }
                }
                else
                {
                    // Handle error response from the API
                }
            }
            ViewData["customers"] = customers;
            return View();
        }

    }
}
