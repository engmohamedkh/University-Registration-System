using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTO;

namespace University.Core.IServices
{
    public interface IStudentService
    {
        public Task<IEnumerable<StudentDTO>> GetAll();
        public Task<StudentDTO> GetStudentById(Guid id);
        public Task CreateAsync(StudentDTO customerDTO);
        public Task UpdateAsync(StudentDTO customerDTO);
        public Task DeleteAsync(Guid id);
    }
}
