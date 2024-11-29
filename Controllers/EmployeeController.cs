using CRUDWithADONet.DAL;
using CRUDWithADONet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithADONet.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {

                TempData["errorMeassage"] = ex.Message;
            }
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMeassage"] = "Model data InValid";
                }
                bool result = _dal.Insert(model);
                if (!result)
                {
                    TempData["errorMeassage"] = "Unable to save data";
                    return View();
                }
                TempData["succesMessage"] = " Emaployee details";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMeassage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                EmployeeModel employeeModel = _dal.GetById(id);
                if (employeeModel.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details are not found: {id}";
                    return RedirectToAction("Index");
                }
                return View(employeeModel);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
                
            }

        }
        [HttpPost]
        public IActionResult Edit(EmployeeModel employeeModel)
        {
            try
            {
              if(!ModelState.IsValid)
                {
                    TempData["errorMeassage"] = "Model data InValid";
                    return View();
                }
              bool result= _dal.Update(employeeModel);
                
                    if (!result)
                    {
                        TempData["errorMeassage"] = "Unable to update data";
                        return View();
                    }
                TempData["succesMessage"] = " Emaployee details updated";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
                
            }

        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeModel employeeModel = _dal.GetById(id);
                if (employeeModel.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details are not found: {id}";
                    return RedirectToAction("Index");
                }
                return View(employeeModel);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(EmployeeModel employeeModel)
        {
            try
            {
                bool result = _dal.Delete(employeeModel.Id);

                if (!result)
                {
                    TempData["errorMeassage"] = "Unable to delete data";
                    return View();
                }
                TempData["succesMessage"] = " Emaployee details deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
    }
}
