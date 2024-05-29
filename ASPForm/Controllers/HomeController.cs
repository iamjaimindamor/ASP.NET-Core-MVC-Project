using ASPForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASPForm.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly AbhaDBContext abha;

        public HomeController(AbhaDBContext abha)
        {
            this.abha = abha;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ABHAModel a)
        {
            if (ModelState.IsValid)
            {

                await abha.abhaTab.AddAsync(a);
                await abha.SaveChangesAsync();
                TempData["inserted"] = "Inserted";
                return RedirectToAction("Registered", "Home");
            }
            return View(a);
        }

        [HttpPost("FindUser")]
        public async Task<IActionResult> Details(int id, ABHAModel a)
        {
            var userdata = abha.abhaTab.FirstOrDefault(x => x.id == id);

            if (userdata == null) { return NotFound(); }
            else
            {
                var details = await abha.abhaTab.FindAsync(id);
                return Ok(details);
            }


        }

        public async Task<IActionResult> Edits(int id)
        {
            var userdata = abha.abhaTab.FirstOrDefault(x => x.id == id);

            if (userdata == null) { return NotFound(); }
            else
            {
                var details = await abha.abhaTab.FindAsync(id);
                return View(details);
            }

        }

        [HttpPost("UpdateDetails")]
        [ValidateAntiForgeryToken]

        public IActionResult Edits(ABHAModel a)
        {
            if (ModelState.IsValid)
            {
                abha.abhaTab.Update(a);
                abha.SaveChanges();
                TempData["edit"] = "User Updated";
                return RedirectToAction("Registered", "Home");
            }
            return View(a);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var review_deleteData = await abha.abhaTab.FirstOrDefaultAsync(x => x.id == id);
                return View(review_deleteData);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ABHAModel a)
        {
            if (a != null)
            {
                abha.abhaTab.Remove(a);
                abha.SaveChanges();
                TempData["deleted"] = "User Deleted";
                return RedirectToAction("Registered");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("FetchDetails")]
        public IActionResult Registered()
        {
            var data = abha.abhaTab.ToList();

            return Ok(data);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        private FetchModel GetDetails()
        {
            FetchModel fetching = new FetchModel();
            fetching.abha_wale = new List<SelectListItem>();

            //get the details from the abha data table
            var details = abha.abhaTab.ToList();

            //default value
            fetching.abha_wale.Add(
                  new SelectListItem
                  {
                      Text = "Select Beneficiary",
                      Value = ""
                  });

            //loop through the details and store it in the list 
            foreach (var item in details)
            {
                fetching.abha_wale.Add(
                    new SelectListItem
                    {
                        Value = item.id.ToString(),
                        Text = item.Full_Name
                    });

            }
            return fetching;
        }

        public IActionResult fetch()

        {
            FetchModel f = GetDetails();

            return View(f);
        }

        [HttpPost("List")]
        public async Task<IActionResult> fetch(FetchModel m)
        {
            FetchModel New = GetDetails();
            TempData.Keep();
            var data = await abha.abhaTab.Where(x => x.id == m.Id).FirstOrDefaultAsync();

            if (data != null)
            {
                TempData["abhahai"] = true;
                TempData["abhaID"] = data.ABHA_Number;
                TempData["abhaName"] = data.Full_Name;
                ViewBag.abhaDOB = data.DateOfBirth;

            }
            else
            {
                ViewBag.Message = "Data Not Found";
            }
            return View(New);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
