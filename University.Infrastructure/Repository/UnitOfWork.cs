
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Dbcontext;

namespace Booking.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UniversityDbContext _context;

        public IBaseRepository<Student> Students { get; private set; }
        public IBaseRepository<Department> Departments { get; private set; }
        

        public UnitOfWork(UniversityDbContext context)
        {
            _context = context;
            Students = new BaseRepository<Student>(_context);
            Departments = new BaseRepository<Department>(_context);
            
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
