﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taijitan_Yoshin_Ryu_vzw.Filters;
using Taijitan_Yoshin_Ryu_vzw.Models.Domain;
using Taijitan_Yoshin_Ryu_vzw.Models.LesmateriaalViewModels;

namespace Taijitan_Yoshin_Ryu_vzw.Controllers
{
    public class LesmateriaalController : Controller
    {
        private readonly IGraadRepository _graden;

        public LesmateriaalController(IGraadRepository graden)
        {
            _graden = graden;
        }

        public IActionResult ThemaView(int GraadId)
        {
            ViewBag.GeselecteerdeGraad = _graden.GetGraadWithId(GraadId);
            return PartialView("~/Views/Lesmateriaal/Thema.cshtml");
        }

        public IActionResult LesmateriaalView(string ThemaNaam, int GraadId)
        {
            ViewBag.ThemaNaam = ThemaNaam;
            
            return PartialView("~/Views/Lesmateriaal/LesmateriaalHead.cshtml");
        }

        public IActionResult LesmateriaalView(string ThemaNaam, int GraadId, int LesmateriaalId)
        {
            ViewBag.Lesmaterialen = _graden.GetGraadWithId(GraadId).GeefLesmateriaalMetThema(ThemaNaam).Where(l => l.Id == LesmateriaalId).First();
            return PartialView("~/Views/Lesmateriaal/Lesmateriaal.cshtml");
        }

        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Index(Gebruiker gebruiker)
        {
            return View(new LesmateriaalViewModel(gebruiker, _graden.GetAll().ToList()));
        }

    }
}