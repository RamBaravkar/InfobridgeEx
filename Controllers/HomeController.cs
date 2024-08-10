using InfobridgeEx.Models;
using InfobridgeEx.StudentViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfobridgeEx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MyContext _Context;

        private readonly string _webRootPath;

        public HomeController(ILogger<HomeController> logger,MyContext myContext, IWebHostEnvironment env)
        {
            _logger = logger;
            _Context = myContext;
            _webRootPath = env.WebRootPath;
        }

        public IActionResult Index()
        {
            var rec = _Context.Students.ToList();
            return View(rec);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]

        public IActionResult Create(StudentVM model, IFormFile photo)
        {

            if (ModelState.IsValid)
            {
                string photoPath = null;
                if (photo != null && photo.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webRootPath, "images"); 
                    photoPath = Path.Combine(uploadsFolder, photo.FileName);

                    using (var fileStream = new FileStream(photoPath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
                Student student = new Student
                {
                    Name = model.Name,
                    DateofBirth = model.DateOfBirth,
                    Sex = model.Sex,
                    Phone = model.Phone,
                    Address = model.Address,
                    Photo = photoPath 
                };

                _Context.Students.Add(student);
                _Context.SaveChanges();

               
            }
            return RedirectToAction("Index");
        }
    

        [HttpGet]

        public IActionResult Edit(int id)
        {
            var student = _Context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentVM
            {
                Id = student.Id,
                Name = student.Name,
                DateOfBirth = student.DateofBirth,
                Sex = student.Sex,
                Phone = student.Phone,
                Address = student.Address,
                Photo = student.Photo
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentVM model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var student = _Context.Students.Find(model.Id);
                if (student == null)
                {
                    return NotFound();
                }

                // Update student properties
                student.Name = model.Name;
                student.DateofBirth = model.DateOfBirth;
                student.Sex = model.Sex;
                student.Phone = model.Phone;
                student.Address = model.Address;

                // Handle photo upload if a new photo is provided
                if (photo != null && photo.Length > 0)
                {
                    // Delete old photo if it exists
                    if (!string.IsNullOrEmpty(student.Photo))
                    {
                        var oldPhotoPath = Path.Combine(_webRootPath, "images", Path.GetFileName(student.Photo));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                    }

                   
                    string uploadsFolder = Path.Combine(_webRootPath, "images");
                    string newPhotoPath = Path.Combine(uploadsFolder, photo.FileName);

                    using (var fileStream = new FileStream(newPhotoPath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    student.Photo = newPhotoPath;
                }

                _Context.Students.Update(student);
                _Context.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            var rec = _Context.Students.Find(Id);
            _Context.Students.Remove(rec);
            _Context.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
