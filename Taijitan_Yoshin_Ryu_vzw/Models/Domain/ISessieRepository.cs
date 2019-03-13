﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan_Yoshin_Ryu_vzw.Models.Domain
{
    public interface ISessieRepository
    {
        IEnumerable<Sessie> GetAll();

        Sessie GetByDatumBeginUur(DateTime datumBeginUur);
        void Add(Sessie sessie);
        void SaveChanges();
    }
}
