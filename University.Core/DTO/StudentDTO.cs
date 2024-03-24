using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Models;

namespace University.Core.DTO
{
    public class StudentDTO
    {
        public Guid StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string DepartmentName { get; set; }
     
        public static Student ToStudent(StudentDTO studentDTO)
        {
            return new Student
            {
                StudentId = studentDTO.StudentId,
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                BirthDate = studentDTO.BirthDate,
                Email = studentDTO.Email
            };
        }
    }
}
