using IPT102_PALINO_SYSTEM.Models;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Data.Entity.Validation;

namespace IPT102_PALINO_SYSTEM.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AccountController()
        {
            _dbContext = new AppDbContext();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Roles = new SelectList(_dbContext.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Login(User loginUser)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(loginUser.Password);
                var authenticatedUser = _dbContext.Users
                    .FirstOrDefault(u => u.Username == loginUser.Username && u.Password == hashedPassword);

                if (authenticatedUser != null)
                {
                    var userRole = authenticatedUser.RoleId;

                    switch (userRole)
                    {
                        case (int)UserRole.Admin:
                            return RedirectToAction("AdminDashboard", "Index");
                        case (int)UserRole.MedicalStaff:
                            return RedirectToAction("MedicalStaffDashboard", "Index");
                        case (int)UserRole.Patient:
                            return RedirectToAction("PatientDashboard", "Index");
                        default:
                            ModelState.AddModelError(string.Empty, "Invalid role");
                            break;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }

            ViewBag.Roles = new SelectList(_dbContext.Roles, "Id", "Name");
            return View(loginUser);
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Roles = new SelectList(_dbContext.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_dbContext.Users.Any(u => u.Username == newUser.Username))
                    {
                        ModelState.AddModelError("Username", "Username is already taken");
                        ViewBag.Roles = new SelectList(_dbContext.Roles, "Id", "Name");
                        return View(newUser);
                    }

                    if (_dbContext.Users.Any(u => u.Email == newUser.Email))
                    {
                        ModelState.AddModelError("Email", "Email is already registered");
                        ViewBag.Roles = new SelectList(_dbContext.Roles, "Id", "Name");
                        return View(newUser);
                    }

                    newUser.Password = HashPassword(newUser.Password);
                    newUser.RoleId = newUser.RoleId;

                    _dbContext.Users.Add(newUser);
                    _dbContext.SaveChanges();

                    TempData["RegistrationSuccess"] = "Registration successful. Please log in.";
                    return RedirectToAction("Login");
                }
                catch (DbEntityValidationException ex)
                {
                    LogValidationErrors(ex);
                    ModelState.AddModelError(string.Empty, "An error occurred during registration. Please try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during registration: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred during registration.");
                }
            }

            ViewBag.Roles = new SelectList(_dbContext.Roles, "Id", "Name");
            return View(newUser);
        }

        private void LogValidationErrors(DbEntityValidationException ex)
        {
            foreach (var eve in ex.EntityValidationErrors)
            {
                foreach (var ve in eve.ValidationErrors)
                {
                    Console.WriteLine($"Entity of type '{eve.Entry.Entity.GetType().Name}' in state '{eve.Entry.State}' has the following validation error: '{ve.ErrorMessage}'");
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
