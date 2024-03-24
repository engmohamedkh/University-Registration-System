using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTO;

namespace University.Core.Models
{
    
    public class Department
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Location { get; set; }

        // Navigation Property
        public List<Student>? Students { get; set; }
  
        public static DepartmentDTO ToDepartmentDTO(Department department)
        {
            var departmentDTO = new DepartmentDTO
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Location = department.Location
            };

            departmentDTO.studentnames = department.Students?.Select(s => $"{s.FirstName} {s.LastName}").ToList() ?? new List<string>();

            return departmentDTO;
        }
    }
}
