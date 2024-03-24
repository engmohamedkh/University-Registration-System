using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTO;

namespace University.Core.IServices
{
     public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentDTO>> GetAll();
        public Task<IEnumerable<DepartmentDTO>> GetAllWithStudent();
        public Task<DepartmentDTO> GetDepartmentById(Guid id);
        public Task CreateAsync(DepartmentDTO departDTO);
        public Task UpdateAsync(DepartmentDTO departDTO);
        public Task DeleteAsync(Guid id);

    }
}
