using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Models;

namespace University.Core.DTO
{
    public class DepartmentDTO
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Location { get; set; }
        public List<string>? studentnames { get; set; }= new List<string>();
        public static Department ToDepartment(DepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
                throw new ArgumentNullException(nameof(departmentDTO));

            var department = new Department
            {
                DepartmentId = departmentDTO.DepartmentId,
                DepartmentName = departmentDTO.DepartmentName,
                Location = departmentDTO.Location

            };

            //// You might need to adjust this part based on your actual data model
            //department.Students = departmentDTO.StudentNames?.Select(studentName => new Student
            //{
            //    // Assuming Student has FirstName and LastName properties
            //    FirstName = ParseFirstName(studentName),
            //    LastName = ParseLastName(studentName)
            //}).ToList();

            return department;
        }
    }
}
