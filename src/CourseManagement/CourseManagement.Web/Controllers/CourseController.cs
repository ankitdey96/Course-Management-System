using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Exceptions;
using CourseManagement.Infrastructure.Membership;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CourseManagement.Web.Controllers
{
    [Authorize()]
    public class CourseController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ILogger<CourseController> _logger;
        public UserManager<ApplicationUser> _userManager;
        public CourseController(IServiceScopeFactory serviceScopeFactory,ILogger<CourseController>logger,UserManager<ApplicationUser>userManager ) 
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
            _userManager = userManager;
        }   

        public IActionResult ViewCourses()
        {
            return View();
        }

        public IActionResult ViewAssignedCourses()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var oCourseVM = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CourseVM>();
            oCourseVM.Resolve(_scopeFactory);
            var oAccountVM = Activator.CreateInstance<AccountVM>();
            oAccountVM.Resolve(_scopeFactory);
            oCourseVM.TeacherList = await oAccountVM.GetTeacherList();
            return View(oCourseVM);
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var oCourseVM = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CourseVM>();
            var model = await oCourseVM.GetCoure(Id);
            var oAccountVM = Activator.CreateInstance<AccountVM>();
            oAccountVM.Resolve(_scopeFactory);
            model.TeacherList = await oAccountVM.GetTeacherList();

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
        public async Task<IList<Course>> GetCourses([FromBody]CourseVM oCourseVM)
        {
            try
            {
                oCourseVM.Resolve(_scopeFactory);
                IList<Course> data = await oCourseVM.GetCourseWithPaginationAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["error"] = ex.Message;
                return [];
            }

        }

        [HttpPost]
        public async Task<IList<Course>> GetAssignedCourses()
        {
            try
            {
                CourseVM oCourseVM = Activator.CreateInstance<CourseVM>();
                oCourseVM.Resolve(_scopeFactory);

                string LogInUserID = _userManager.GetUserId(User);
                if(LogInUserID is not null)
                {
                    IList<Course> data = await oCourseVM.GetAssignedCourseOfTeacher(LogInUserID);
                    return data;
                }
                else
                {
                    throw new NotFoundException("User");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["error"] = ex.Message;
                return [];
            }

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

        public async Task<IActionResult> AddCourseOutline(Guid Id)
        {
            var oCourseVM = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CourseVM>();
            var model = await oCourseVM.GetCoure(Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseOutLine([FromBody]List<CourseTopicVM>oCourseTopicVMs)
        {
                try
                {
                    var oCourseTopicVM = Activator.CreateInstance<CourseTopicVM>();
                    oCourseTopicVM.Resolve(_scopeFactory);
                    await oCourseTopicVM.CreateoutLine(oCourseTopicVMs);
                    TempData["success"] = "Data Saved Successfully";

                    return RedirectToAction("ViewCourses");
                }
                catch (Exception ex)
                {
                    TempData["error"] = "There was a Problem in Creating Course Outline.";
                    _logger.LogError(ex.Message);
                }

            return View(oCourseTopicVMs);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EnrollCourse(Guid CourseID)
        {
            try
            {
                var oCourseEnrollMentVM = Activator.CreateInstance<CourseEnrollMentVM>();
                Guid UserId = Guid.Parse(_userManager.GetUserId(User));
                oCourseEnrollMentVM.Resolve(_scopeFactory);
                await oCourseEnrollMentVM.EnrollCourse(CourseID, UserId);
                TempData["success"] = "Enrolled Successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                _logger.LogError(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
