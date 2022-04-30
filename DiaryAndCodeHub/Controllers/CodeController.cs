using DataAccess.Implementation;
using DiaryAndCodeHub.Helper;
using Models;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DiaryAndCodeHub.Controllers
{
    [Authorize(Roles ="User")]
    public class CodeController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Code
        public ActionResult Index()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            IEnumerable<Claim> claims = identity.Claims;


            var currentUsername = claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            var a =unitOfWork.InpcodeRepository.GetAll(filter:m=>m.UserCreated==currentUsername);

            return View(a);
        }
        public ActionResult Create() {
            InpCodes codes = new InpCodes();
            return View(codes);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(InpCodes code,List<HttpPostedFileBase> Images) {
            if (ModelState.IsValid)
            {
                code.Id = Guid.NewGuid();
                List<InpCodesImages> im = new List<InpCodesImages>();
                if (Images.Count > 0)
                {
                    foreach (var images in Images)
                    {
                        if (images != null)
                        {
                            if (images.ContentLength > 0)
                            {

                                var fileName = Path.GetFileName(images.FileName);
                                var fileName1 = Path.GetFileNameWithoutExtension(images.FileName);

                                // extract only the fielname
                                var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.
                                if (Extension.ImageExt.Contains(ext.ToUpper()))
                                {


                                    string firstpath1 = "~/Files/";
                                    string secondpath = "~/Files/Code/";
                                    string ThirdPath = "~/Files/Code/Images";
                                    string ForthPath = "~/Files/Code/Images/" + code.Id + "/";
                                    string ForthPath1 = "/Files/Code/Images/" + code.Id + "/";

                                    bool exists1 = System.IO.Directory.Exists(Server.MapPath(firstpath1));
                                    bool exists2 = System.IO.Directory.Exists(Server.MapPath(secondpath));
                                    bool exists3 = System.IO.Directory.Exists(Server.MapPath(ThirdPath));
                                    bool exists4 = System.IO.Directory.Exists(Server.MapPath(ForthPath));

                                    if (!exists1)
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath1));

                                    }
                                    if (!exists2)
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath(secondpath));

                                    }

                                    if (!exists3)
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath(ThirdPath));

                                    }

                                    if (!exists4)
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath(ForthPath));

                                    }
                                    var path = Server.MapPath(ForthPath + fileName1 + ext);
                                    InpCodesImages img = new InpCodesImages();
                                    img.ImagePath = ForthPath1 + fileName1 + ext;
                                    img.Id = Guid.NewGuid();
                                    //img.Name = images.FileName;
                                    im.Add(img);
                                    images.SaveAs(path);
                                }
                            }
                        }
                    }
                }
                code.inpCodesImages = im;
                unitOfWork.InpcodeRepository.Insert(code);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();

        
        }
        public ActionResult Edit(Guid id)
        {
           
            var a = unitOfWork.InpcodeRepository.GetById(id);
            if(a==null)
            {
                return HttpNotFound();
            }
            return View(a);

        }
        public ActionResult Details(Guid id)
        {
            var a = unitOfWork.InpcodeRepository.GetById(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            var b= unitOfWork.InpImageRepository.GetAll(filter: m => m.InpCodesId == id);
            CodeHubVm v = new CodeHubVm();
            v.Inpcodes = a;
            v.Img = b;

            return View(v);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(InpCodes code,List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
             
                    List<InpCodesImages> im = new List<InpCodesImages>();
                    int i = 0;

                    if (Images.Count > 0)
                    {
                        foreach (var images in Images)
                        {
                            if (images != null)
                            {
                                if (images.ContentLength > 0)
                                {

                                    var fileName = Path.GetFileName(images.FileName);
                                    var fileName1 = Path.GetFileNameWithoutExtension(images.FileName);

                                    // extract only the fielname
                                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.
                                    if (Extension.ImageExt.Contains(ext.ToUpper()))
                                    {
                                        if (i ==0)
                                        {
                                            List<InpCodesImages> imgs = unitOfWork.InpImageRepository.GetAll(filter: m => m.InpCodesId == code.Id).ToList();
                                            DeleteFolder.ClearFolder(Server.MapPath("~/Files/Code/Images/" + code.Id + "/"));
                                            foreach(var item in imgs)
                                            {
                                                unitOfWork.InpImageRepository.Delete(item.Id);
                                            }
                                            
                                            
                                            
                                            i++;
                                        }
                                        string firstpath1 = "~/Files/";
                                        string secondpath = "~/Files/Code/";
                                        string ThirdPath = "~/Files/Code/Images";
                                        string ForthPath = "~/Files/Code/Images/" + code.Id + "/";
                                        string ForthPath1 = "/Files/Code/Images/" + code.Id + "/";

                                        bool exists1 = System.IO.Directory.Exists(Server.MapPath(firstpath1));
                                        bool exists2 = System.IO.Directory.Exists(Server.MapPath(secondpath));
                                        bool exists3 = System.IO.Directory.Exists(Server.MapPath(ThirdPath));
                                        bool exists4 = System.IO.Directory.Exists(Server.MapPath(ForthPath));

                                        if (!exists1)
                                        {
                                            System.IO.Directory.CreateDirectory(Server.MapPath(firstpath1));

                                        }
                                        if (!exists2)
                                        {
                                            System.IO.Directory.CreateDirectory(Server.MapPath(secondpath));

                                        }

                                        if (!exists3)
                                        {
                                            System.IO.Directory.CreateDirectory(Server.MapPath(ThirdPath));

                                        }

                                        if (!exists4)
                                        {
                                            System.IO.Directory.CreateDirectory(Server.MapPath(ForthPath));

                                        }
                                        var path = Server.MapPath(ForthPath + fileName1 + ext);
                                        InpCodesImages img = new InpCodesImages();
                                        img.Id = Guid.NewGuid();
                                        img.ImagePath = ForthPath1 + fileName1 + ext;
                                        //img.InpCodesId = code.Id;
                                        //img.Name = images.FileName;
                                        im.Add(img);
                                        images.SaveAs(path);
                                    }
                                }
                            }
                        }
                    }
                    if (im != null || im.Count()>0)
                    {
                        code.inpCodesImages = im;
                    }
                    else
                    {
                       code.inpCodesImages = unitOfWork.InpImageRepository.GetAll(filter: m => m.InpCodesId == code.Id).ToList();

                    }

                InpCodes cat = unitOfWork.InpcodeRepository.GetById(code.Id);

                cat.Description = code.Description;
                cat.inpCodesImages = code.inpCodesImages;
                cat.Topic = code.Topic;
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }

               
            return View(code);


        }

    }
}