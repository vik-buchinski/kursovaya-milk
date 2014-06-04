using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Web.Security;
using Core.ViewModels.AdminPanel;
using DataAcess;
using DataAcess.Models;
using FrontEnd;
using WebMatrix.WebData;

namespace Core.Controllers
{
    [Authorize(Roles = Constants.ADMIN_ROLE)]
    public class AdminPanelController : Controller
    {
        private readonly Repositories _unitOfWork = new Repositories();

        [HttpGet]
        public PartialViewResult UserRoles()
        {
            IEnumerable<UserProfile> users =
                _unitOfWork.UserProfileRepository.Get(orderBy: o => o.OrderByDescending(f => f.LastName));
            return PartialView("_ManageUserRoles", users);
        }

        [HttpPost]
        public JsonResult RemoveUserFromRole(int user_id, string role_name)
        {
            UserProfile user = _unitOfWork.UserProfileRepository.GetById(user_id);

            if (role_name.Equals(Constants.ROLES_LIST.GetValue((int)Constants.Roles.ADMIN)))
            {
                var users = Roles.GetUsersInRole(role_name);
                if (users.Length == 1)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(Constants.DELETE_LAST_ADMIN_MESSAGE);
                }
            }

            Roles.RemoveUserFromRole(user.Email, role_name);

            return Json(GetRoleManagmentModel(user));
        }

        [HttpPost]
        public JsonResult AddUserToRole(int user_id, string role_name)
        {
            UserProfile user = _unitOfWork.UserProfileRepository.GetById(user_id);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(Constants.NO_USER_MESSAGE);
            }
            Roles.AddUserToRole(user.Email, role_name);

            return Json(GetRoleManagmentModel(user));
        }

        [HttpPost]
        public JsonResult GetRolesInfo(int user_id)
        {
            return Json(GetRoleManagmentModel(
                _unitOfWork.UserProfileRepository.GetById(user_id)));
        }

        [HttpGet]
        public PartialViewResult CreateCurator()
        {
            return PartialView("_CreateCurator");
        }

        [HttpPost]
        public PartialViewResult CreateCurator(CuratorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                                                     new
                                                     {
                                                         WorkPlace = model.WorkPlace,
                                                         Chair = model.Chair,
                                                         Post = model.Post,
                                                         AcademicTitle = model.AcademicTitle,
                                                         Degree = model.Degree,
                                                         CreatedAt = DateTime.Now,
                                                         UpdatedAt = DateTime.Now,
                                                         IsActive = true
                                                     },
                                                     false);
                    Roles.AddUserToRole(model.Email, Constants.ROLES_LIST[(int)Constants.Roles.CURATOR]);

                    ModelState.Clear();
                    ViewBag.IsCuratorCreated = true;
                    return PartialView("_CreateCurator");
                }
                catch (InvalidOperationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (ArgumentNullException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return PartialView("_CreateCurator", model);
        }


        private object GetRoleManagmentModel(UserProfile user)
        {
            var model = new RoleManagmentViewModel { current_roles = Roles.GetRolesForUser(user.Email) };
            model.available_roles =
                Constants.ROLES_LIST.Where(role => !model.current_roles.Contains(role)).ToList();
            return model;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
