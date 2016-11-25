using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ADL.Models
{
    public class EFSchoolRepository : ISchoolRepository
    {
        private ApplicationDbContext context;

        public EFSchoolRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<School> Schools { get; }
        
        public void AddSchoolsToDb()
        {
            
        }
    }
}
