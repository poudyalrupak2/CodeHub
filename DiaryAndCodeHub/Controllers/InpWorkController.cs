using DataAccess.Implementation;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DiaryAndCodeHub.Controllers
{
    [Authorize(Roles = "User")]

    public class InpWorkController : Controller
    {
      
            private readonly UnitOfWork unitOfWork = new UnitOfWork();

            // GET: Code
            public ActionResult Index()
            {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            IEnumerable<Claim> claims =   identity.Claims;


            var currentUsername = claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            var a = unitOfWork.InpWorksRepository.GetAll(filter:m=>m.UserCreated==currentUsername);
                return View(a);
            }
            public ActionResult Create()
            {
            InpWorks inp = new InpWorks();
                return View(inp);

            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            [ValidateInput(false)]
            public ActionResult Create(InpWorks code)
            {
                if (ModelState.IsValid)
                {
                    code.Id = Guid.NewGuid();

                    unitOfWork.InpWorksRepository.Insert(code);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                return View();


            }
            public ActionResult Edit(Guid id)
            {
                var a = unitOfWork.InpWorksRepository.GetById(id);
                if (a == null)
                {
                    return HttpNotFound();
                }
                return View(a);

            }
            public ActionResult Details(Guid id)
            {
                var a = unitOfWork.InpWorksRepository.GetById(id);
                if (a == null)
                {
                    return HttpNotFound();
                }
               

                return View(a);

            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [ValidateInput(false)]
            public ActionResult Edit(InpWorks code)
            {
                if (ModelState.IsValid)
                {



                unitOfWork.InpWorksRepository.Update(code);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }


                return View(code);


            }

        }

    }
