﻿using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class EducationController : BaseController
    {
        private TestDbContext context;
       
        public EducationController(TestDbContext context) 
        { 
                this.context = context;
                
        }

        [Authorize]
        public IActionResult CreateEducation()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEducation(Education evm)
        {

            if (ModelState.IsValid)
            {
                //Skapar ett ny instans av education och tilldelar attribut från vy-modellen till instansen
                var education = new Education();

                education.Name = evm.Name;
                education.Description = evm.Description;

                //Lägger till den nya utbildningen i databasen
                await context.AddAsync(education);
                await context.SaveChangesAsync();

                //Hämtar användarens nuvarande CV ID baserat på användar-ID
                string currentUserId = base.UserId;
                int currentCvId = context.Cvs.Where(c => c.User_ID == currentUserId).Single().Id;

                //Skapar en koppling mellan CV och den skapade utbildningen
                var cvEducation = new CvEducation();
                cvEducation.CvId = currentCvId;
                cvEducation.Education = education;

                //Lägger till kopplingen i databasen
                await context.AddAsync(cvEducation);
                await context.SaveChangesAsync();

                //Dirigerar användaren tillbaka till Cv-sidan.
                return RedirectToAction("ShowCv", "Cv");

            }
            return View(evm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //Hämtar education-objektet som matchar det angivna ID:t
            Models.Education education = context.Educations.Find(id);
            //Tar bort utbildningsobjektet från databasen och sparar ändringarna i databasen
            context.Educations.Remove(education);
            await context.SaveChangesAsync();

            return RedirectToAction("ShowCv", "Cv");

        }

    }
}
