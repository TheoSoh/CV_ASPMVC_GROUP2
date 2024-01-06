﻿using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CompetenceController : BaseController
    {
        private TestDbContext context;

        public CompetenceController(TestDbContext context) 
        {
            this.context = context;
            
        }
        

        public IActionResult Index()
        {
            var items = context.Competences.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult CreateCompetence()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetence(Competence cvm)
        {


            if (ModelState.IsValid)
            {
                //Skapar ett ny instans av competence och tilldelar attribut från vy-modellen till instansen
                var competence = new Competence();

                competence.Name = cvm.Name;
                competence.Description = cvm.Description;

                //Lägger till den nya kompetensen i databasen
                await context.AddAsync(competence);
                await context.SaveChangesAsync();

                //Hämtar användarens nuvarande ID
                string currentUserId = base.UserId;

                //Hämtar användarens nuvarande CV ID baserat på användar-ID
                int currentCvId = context.Cvs.Where(c => c.User_ID == currentUserId).Single().Id;

                //Skapar en koppling mellan CV och den skapade kompetensen
                var cvCompetence = new CvCompetence();
                cvCompetence.Competence = competence;
                cvCompetence.CvId = currentCvId;

                //Lägger till kopplingen i databasen
                await context.AddAsync(cvCompetence);
                await context.SaveChangesAsync();

                //Omdirigerar användaren till samma vy för att skapa ny kompetens
                return RedirectToAction("CreateCompetence", "Competence");

            }
            //Returnerar vy-modellen om validering misslyckades
            return View(cvm);

        }

    }
}
