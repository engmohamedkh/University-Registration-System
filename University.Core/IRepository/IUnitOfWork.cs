using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Models;

namespace University.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Department> Departments { get; }

        public int Complete();
    }
}
