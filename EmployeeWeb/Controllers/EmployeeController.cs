using EmployeeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<EmployeeViewModel> _employeeDetails = new List<EmployeeViewModel>();
        private readonly HttpClient _httpClient;

        public EmployeeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult EmployeeList()
        {
            var model = new EmployeeViewModel
            {
                SubmittedDetails = _employeeDetails
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmployment(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }

            if (ModelState.IsValid)
            {
                var request = new EmployeeViewModel
                {
                    employeeId = employeeViewModel.employeeId,
                    companyName = employeeViewModel.companyName,
                    verficationCode = employeeViewModel.verficationCode,
                    SubmittedDetails = employeeViewModel.SubmittedDetails
                };
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7160/api/EmploymentVerification/verify-employment", request, options);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    using (var document = JsonDocument.Parse(jsonResponse))
                    {
                        var isVerified = document.RootElement.GetProperty("isVerified").GetBoolean();
                        _employeeDetails.Add(request);
                        return Json(new { isVerified });
                    }
                }
                else
                {
                    return Json(new { isVerified = false, error = "Error verifying employment" });
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeGridDetail()
        {
            var response = await _httpClient.GetAsync("https://localhost:7160/api/EmploymentVerification/getAllEmployees");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var employees = JsonSerializer.Deserialize<List<EmployeeViewModel>>(jsonResponse);
                if (employees != null && employees.Count > 0)
                {
                    return Json(new { isSuccess = true, employees });
                }
                else
                {
                    return Json(new { isSuccess = false, message = "No employees found." });
                }
            }
            else
            {
                return Json(new { isSuccess = false, error = "Error fetching employees from the API." });
            }
        }
    }
}