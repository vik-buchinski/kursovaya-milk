using System;
using System.Web.Mvc;
using Core.ViewModels.Account;
using FrontEnd;
using WebMatrix.WebData;
using System.Web.Security;

namespace Core.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
        public PartialViewResult SignIn()
        {
            return PartialView("_SignIn");
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult SignIn(SignInViewModel model)
        {
            bool signInResult = false;
            try
            {
                signInResult = WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe);
                if (ModelState.IsValid && signInResult)
                {
                    ModelState.Clear();
                    ViewBag.IsCloseDialog = true;
                }
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            if (!signInResult)
            {
                ModelState.AddModelError("", "Wrong Email/password combination.");
            }
            return PartialView("_SignIn.cshtml", model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult SignUp()
        {
            return PartialView("_SignUp");
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                                                     new
                                                         {
                                                             FirstName = model.FirstName,
                                                             Patronymic = model.Patronymic,
                                                             LastName = model.LastName,
                                                             Email = model.Email,
                                                             HomePhone = model.HomePhone,
                                                             MobilePhone = model.MobilePhone,
                                                             CreatedAt = DateTime.Now,
                                                             UpdatedAt = DateTime.Now,
                                                             IsActive = true
                                                         },
                                                     false);
                    Roles.AddUserToRole(model.Email, Constants.ROLES_LIST[(int)Constants.Roles.USER]);
                    WebSecurity.Login(model.Email, model.Password);

                    ModelState.Clear();
                    ViewBag.IsRegistrationComplete = true;
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
            return PartialView("_SignUp", model);
        }

    }
}
