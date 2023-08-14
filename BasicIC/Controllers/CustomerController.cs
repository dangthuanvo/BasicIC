using BasicIC.Models.Main.M03;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BasicIC.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<ActionResult> Index(string searchString)
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;
                if (searchString == null || searchString == "")
                {
                    response = await client.PostAsJsonAsync("https://localhost:44332/api/customer/get-all", new PagingParam());
                }
                else
                {
                    PagingParam pagingParam = new PagingParam();
                    SearchParam searchField = new SearchParam();
                    searchField.name_field = nameof(CustomerModel.customer_name);
                    searchField.value_search = searchString;
                    pagingParam.field_get_list.Add(searchField);
                    response = await client.PostAsJsonAsync("https://localhost:44332/api/customer/get-all", pagingParam);
                }
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ResponseService<ListResult<CustomerModel>>>();
                    if (apiResponse.status)
                    {
                        customers = apiResponse.data.items;
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            ViewData["customers"] = customers;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SearchResults", customers);
            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(FormCollection form)
        {
            CustomerModel newCustomer = new CustomerModel
            {
                customer_name = form["name"],
                gender = int.Parse(form["gender"]),
                phone_number = form["phone"],
                account_name = form["account"],
                password = form["password"],
                email = form["email"]
            };
            //newCustomer.AddInfo();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44332/api/customer/create", newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ResponseService<CustomerModel>>();
                    if (apiResponse.status)
                    {
                        // Customer creation successful, you can redirect to the index page
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle error response from the API
                        ViewBag.ErrorMessage = apiResponse.message;
                        return null; // Create an "Error.cshtml" view
                    }
                }
                else
                {
                    // Handle error response from the API
                    ViewBag.ErrorMessage = "An error occurred while communicating with the server.";
                    //string responseContent = await response.Content.ReadAsStringAsync();
                    //dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    //string innerExceptionMessage = jsonResponse.inner_exception;

                    return null; // Create an "Error.cshtml" view
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(FormCollection form)
        {
            CustomerModel newCustomer = new CustomerModel
            {
                id = new Guid(form["id"]),
                customer_name = form["name"],
                gender = int.Parse(form["gender"]),
                phone_number = form["phone"],
                email = form["email"],
                account_name = form["account"],
                password = form["password"]
            };
            //newCustomer.AddInfo();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44332/api/customer/update", newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ResponseService<CustomerModel>>();
                    if (apiResponse.status)
                    {
                        // Customer creation successful, you can redirect to the index page
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle error response from the API
                        ViewBag.ErrorMessage = apiResponse.message;
                        return null; // Create an "Error.cshtml" view
                    }
                }
                else
                {
                    // Handle error response from the API
                    ViewBag.ErrorMessage = "An error occurred while communicating with the server.";
                    //string responseContent = await response.Content.ReadAsStringAsync();
                    //dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    //string innerExceptionMessage = jsonResponse.inner_exception;

                    return null; // Create an "Error.cshtml" view
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(FormCollection form)
        {
            CustomerModel newCustomer = new CustomerModel
            {
                id = new Guid(form["id"]),
            };
            //newCustomer.AddInfo();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44332/api/customer/delete", newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ResponseService<bool>>();
                    if (apiResponse.status)
                    {
                        // Customer creation successful, you can redirect to the index page
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle error response from the API
                        ViewBag.ErrorMessage = apiResponse.message;
                        return null; // Create an "Error.cshtml" view
                    }
                }
                else
                {
                    // Handle error response from the API
                    ViewBag.ErrorMessage = "An error occurred while communicating with the server.";
                    //string responseContent = await response.Content.ReadAsStringAsync();
                    //dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    //string innerExceptionMessage = jsonResponse.inner_exception;

                    return null; // Create an "Error.cshtml" view
                }
            }
        }


    }
}
