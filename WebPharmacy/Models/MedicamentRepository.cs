using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPharmacy.Data;
using WebPharmacy.Models;

namespace WebPharmacy.Models
{
    public class MedicamentRepository:IMedicamentRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public MedicamentRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Medicament> Medicaments
        {
            get
            {
                return _appDbContext.Medicament.Include(c => c.Formulation);
            }
        }

    }
}
