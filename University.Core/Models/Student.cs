using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTO;

namespace University.Core.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        // Foreign Key
        public Guid DepartmentId { get; set; }

        // Navigation Property
        public virtual Department? Department { get; set; }
        public static StudentDTO ToStudentDTO(Student student)
        {
            return new StudentDTO
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Email = student.Email,
                DepartmentName = student.Department?.DepartmentName
            };
        }
    }
   
}
