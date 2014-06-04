using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.ViewModels.Conference;
using DataAcess;
using DataAcess.Models.Conference;
using DataAcess.Models.ConferenceMember;
using FrontEnd;
using WebMatrix.WebData;

namespace Core.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly Repositories _unitOfWork = new Repositories();

        [HttpGet]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public PartialViewResult AddConferenceCategory()
        {
            var model = new ConferenceCategoryViewModel();
            var conference = _unitOfWork.ConferenceRepository.FirstOrDefault(o => o.IsActive);
            if (conference == null)
            {
                ModelState.AddModelError("", "No started conference");
                ViewBag.IsCallValidationSummary = true;
                return PartialView("_AddConferenceCategory", model);
            }
            model.Categories = GetConferenceCategories(conference.Id);
            return PartialView("_AddConferenceCategory", model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public PartialViewResult AddConferenceCategory(ConferenceCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var conference = _unitOfWork.ConferenceRepository.FirstOrDefault(o => o.IsActive);

                _unitOfWork.ConferenceCategoryRepository.Insert(new ConferenceCategoryModel
                    {
                        ConferenceId = conference.Id,
                        Name = model.CategoryName,
                        CreatedAt = DateTime.Now.ToString(),
                        UpdatedAt = DateTime.Now.ToString(),
                        IsActive = true
                    });
                _unitOfWork.Save();
                ModelState.Clear();
                model.Categories = GetConferenceCategories(conference.Id);
                model.CategoryName = null;
                ViewBag.IsCategoryCreated = true;
                return PartialView("_AddConferenceCategory", model);
            }
            return PartialView("_AddConferenceCategory", model);
        }

        private List<string> GetConferenceCategories(int conferenceId)
        {
            var categories = _unitOfWork.ConferenceCategoryRepository.Get(o => o.ConferenceId == conferenceId);
            return categories.Select(conferenceCategoryModel => conferenceCategoryModel.Name).ToList();
        }

        [HttpGet]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public PartialViewResult ConferenceManagment()
        {
            return PartialView("_ConferenceManagment");
        }

        [HttpPost]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public JsonResult StartNewConference()
        {
            var conferences = _unitOfWork.ConferenceRepository.Get(orderBy: o => o.OrderByDescending(f => f.Id), filter: o => o.IsActive);
            foreach (var conferenceModel in conferences)
            {
                conferenceModel.IsActive = false;
                conferenceModel.IsAvailableForRegistration = false;
                _unitOfWork.ConferenceRepository.Update(conferenceModel);
            }

            var conference = new ConferenceModel
                {
                    CreatedAt = DateTime.Now.ToString(),
                    UpdatedAt = DateTime.Now.ToString(),
                    IsActive = true
                };
            _unitOfWork.ConferenceRepository.Insert(conference);
            _unitOfWork.Save();

            return Json("");
        }

        [HttpPost]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public JsonResult GetCurrentConferenceStatus()
        {
            var conference = _unitOfWork.ConferenceRepository.FirstOrDefault(o => o.IsActive);
            if (conference == null)
            {
                return Json(null);
            }
            return
                Json(new ConferenceStatusViewModel
                    {
                        is_conference_available_for_registration = conference.IsAvailableForRegistration
                    });
        }

        [HttpPost]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public JsonResult OpenRegistration()
        {
            var conference = _unitOfWork.ConferenceRepository.FirstOrDefault(o => o.IsActive);
            if (conference == null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(Constants.NO_ACTIVE_CONFERENCE_MESSAGE);
            }
            conference.IsAvailableForRegistration = true;
            _unitOfWork.ConferenceRepository.Update(conference);
            _unitOfWork.Save();
            return Json("");
        }

        [HttpGet]
        [Authorize]
        public ActionResult TakePart()
        {
            var conference = _unitOfWork.ConferenceRepository.FirstOrDefault(o => o.IsActive);
            if (conference == null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(Constants.NO_ACTIVE_CONFERENCE_MESSAGE, JsonRequestBehavior.AllowGet);
            }
            if (!conference.IsAvailableForRegistration)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(Constants.REGISTRATION_IS_NOT_AVAILABLE_MESSAGE, JsonRequestBehavior.AllowGet);
            }
            var categories =
                _unitOfWork.ConferenceCategoryRepository.Get(category => category.ConferenceId == conference.Id);
            var viewCategories = categories.Select(conferenceCategoryModel => new TakePartCategotyViewModel
                {
                    Category = conferenceCategoryModel,
                    RegisteredCount = _unitOfWork.MembersRepository.Get(member => member.IsActive && member.CategoryId == conferenceCategoryModel.Id).Count()
                }).ToList();

            return PartialView("_TakePart", viewCategories);
        }

        [HttpGet]
        [Authorize]
        public JsonResult RegistrateMember(int conference_category)
        {
            if (conference_category < 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(Constants.WRONG_ID_MESSAGE, JsonRequestBehavior.AllowGet);
            }

            var member = new MemberViewModel { CategoryId = conference_category };

            return Json(Common.RenderViewToString(this, "_RegistrateMember", member), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult RegistrateMember(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                string currentDate = DateTime.Now.ToString();
                var member = new MemberModel
                    {
                        CategoryId = model.CategoryId,
                        UserId = WebSecurity.CurrentUserId,
                        ClassOrGroup = model.ClassOrGroup,
                        EducationalInstitution = model.EducationalInstitution,
                        NameOrNumber = model.NameOrNumber,
                        State = model.State,
                        Town = model.Town,
                        IsActive = true,
                        CreatedAt = currentDate,
                        UpdatedAt = currentDate
                    };
                _unitOfWork.MembersRepository.Insert(member);
                _unitOfWork.Save();
                ModelState.Clear();
                model.MemberId = _unitOfWork.MembersRepository.FirstOrDefault(
                    registredMember => registredMember.CategoryId == member.CategoryId
                              && registredMember.UserId == member.UserId
                              && registredMember.CreatedAt == currentDate
                              && registredMember.UpdatedAt == currentDate).Id;
                ViewBag.IsRegistrationComplete = true;
                return PartialView("_RegistrateMember", model);
            }
            return PartialView("_RegistrateMember", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RegistrateWork(int member_id)
        {
            if (_unitOfWork.MembersRepository.FirstOrDefault(member => member.Id == member_id) == null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(Constants.WRONG_ID_MESSAGE, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        [Authorize]
        public JsonResult UploadWork(int member_id, int category_id)
        {
            var conferencion = _unitOfWork.ConferenceRepository.FirstOrDefault(conf => conf.IsActive);
            if (conferencion == null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(Constants.NO_ACTIVE_CONFERENCE_MESSAGE);
            }
            var category =
                _unitOfWork.ConferenceCategoryRepository.FirstOrDefault(categ => categ.Id == category_id);

            if (category == null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json("No category with id " + category_id);
            }
            if (!category.IsActive)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json("Category is no active");
            }
            if (category.ConferenceId != conferencion.Id)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json("Selected category is not active");
            }
            if (!conferencion.IsAvailableForRegistration)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json("Registration on conferention is closed");
            }
            if (!conferencion.IsActive)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json("Conferention is closed");
            }

            var model = new UploadWorkViewModel
                {
                    CategoryId = category_id,
                    MemberId = member_id,
                    UploadedWorks = _unitOfWork.MembersWorksRepository.Get(
                        work => work.CategoryId == category_id && work.MemberId == member_id)
                };

            return Json(Common.RenderViewToString(this, "_UploadWork", model), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteWork(string file_name, int member_id, int category_id)
        {
            var fullPath = GetPathToWork(member_id, category_id, file_name);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                _unitOfWork.MembersWorksRepository.Delete(
                    _unitOfWork.MembersWorksRepository.FirstOrDefault(
                    work => work.FileName == file_name
                        && work.MemberId == member_id
                        && work.CategoryId == category_id).Id);
                _unitOfWork.Save();
            }
            return null;
        }

        [HttpGet]
        [Authorize]
        public FileResult DownloadWork(string file_name, int member_id, int category_id)
        {
            return File(GetPathToWork(member_id, category_id, file_name), System.Net.Mime.MediaTypeNames.Application.Octet, file_name);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult WorkUploading(IEnumerable<HttpPostedFileBase> files, int? member_id, int? category_id)
        {
            if (files != null)
            {
                if (member_id == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json("member_id can't be null");
                }
                if (category_id == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json("category_id can't be null");
                }
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        if (file.ContentLength > Constants.UPLOAD_FILE_LIMIT_IN_BYTES)
                        {
                            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            return Json(file.FileName + Constants.FILE_TOO_BIG_MESSAGE + Constants.UPLOAD_FILE_LIMIT_IN_BYTES / 1024 / 1024);
                        }
                        if (!Directory.Exists(Server.MapPath(Constants.WORKS_PATH + "/category_" + category_id + "/member_" + member_id + "/")))
                        {
                            Directory.CreateDirectory(Server.MapPath(Constants.WORKS_PATH + "/category_" + category_id + "/member_" + member_id + "/"));
                        }
                        string fileNameWithExt = Path.GetFileName(file.FileName);
                        if (fileNameWithExt != null)
                        {

                            var path = GetPathToWork(member_id, category_id, fileNameWithExt);

                            string fileName = Path.GetFileNameWithoutExtension(path);
                            string fileExt = Path.GetExtension(path);
                            if (System.IO.File.Exists(path))
                            {
                                string uniquePath = "";
                                var idx = "";
                                for (int i = 0; i < int.MaxValue; i++)
                                {
                                    idx = (i == 0) ? "" : (" (" + i.ToString() + ")");
                                    uniquePath = GetPathToWork(member_id, category_id, fileName + idx + fileExt);
                                    if (!System.IO.File.Exists(uniquePath)) break;
                                }
                                file.SaveAs(uniquePath);
                                _unitOfWork.MembersWorksRepository.Insert(new MemberWorkModel
                                {
                                    CategoryId = category_id.Value,
                                    FileName = fileName + idx + fileExt,
                                    MemberId = member_id.Value
                                });
                                _unitOfWork.Save();
                            }
                            else
                            {
                                file.SaveAs(path);
                                _unitOfWork.MembersWorksRepository.Insert(new MemberWorkModel
                                {
                                    CategoryId = category_id.Value,
                                    FileName = fileNameWithExt,
                                    MemberId = member_id.Value
                                });
                                _unitOfWork.Save();
                            }
                        }
                    }
                }
            }
            return null;
        }

        private string GetPathToWork(int? memberId, int? categoryId, string filename)
        {
            return Path.Combine(Server.MapPath(Constants.WORKS_PATH + "/category_" + categoryId + "/member_" + memberId + "/"), filename);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
