﻿using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Exceptions;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CourseManagement.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ILogger<CourseController> _logger;
        public CourseController(IServiceScopeFactory serviceScopeFactory,ILogger<CourseController>logger) 
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
        }   
        public IActionResult ViewCourses()
        {
            return View();
        }

        public IActionResult Create()
        {
            var oCourseVM = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CourseVM>();
            return View(oCourseVM);
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var oCourseVM = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CourseVM>();
            var model = await oCourseVM.GetCoure(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseVM oCourseVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    oCourseVM.Resolve(_scopeFactory);
                    await oCourseVM.UpdateAsync();
                    TempData["success"] = "Data Updated Successfully";

                    return RedirectToAction("ViewCourses");
                }
                catch(DuplicateTitleException de)
                {
                    TempData["error"] = de.Message;
                }
                catch(Exception ex)
                {
                    TempData["error"] = "There was a Problem in Updating Course";
                    _logger.LogError(ex.Message);
                }


            }

            return View(oCourseVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var oCourseVM = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CourseVM>();
            try
            {
                await oCourseVM.DeleteAsync(Id);
                TempData["success"] = "Data Deleted Successfully";
                return Json("Data Deleted Successfully");
            }
            catch(Exception ex)
            {
                TempData["error"] = "There was a Problem in Deleteing Course";
                _logger.LogError(ex.Message);
            }
            return Json("There was a Problem in Deleteing Course");

        }
        [HttpPost]
        public async Task<JsonResult> GetCourses([FromBody]CourseVM oCourseVM)
        {
            oCourseVM.Resolve(_scopeFactory);
            var data = await oCourseVM.GetCourseWithPaginationAsync();
            return Json(data, new JsonSerializerOptions { PropertyNamingPolicy=null});
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVM oCourseVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    oCourseVM.Resolve(_scopeFactory);
                    await oCourseVM.CreateAsync();
                    TempData["success"] = "Data Saved Successfully";

                    return RedirectToAction("ViewCourses");
                }
                catch(DuplicateTitleException ex)
                {
                    TempData["error"] = ex.Message;

                }
                catch (Exception ex)
                {
                    TempData["error"] = "There was a Problem in Creating Course";
                    _logger.LogError(ex.Message);
                }

            }
            return View(oCourseVM);
        }
    }
}
