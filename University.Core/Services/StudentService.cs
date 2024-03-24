using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTO;
using University.Core.Interfaces;
using University.Core.IServices;
using University.Core.Models;

namespace University.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task CreateAsync(StudentDTO studentDTO)
        {
            Student student = StudentDTO.ToStudent(studentDTO);
            var dep = await _unitOfWork.Departments.Find(d => d.DepartmentName == studentDTO.DepartmentName);

            student.DepartmentId = dep.DepartmentId;
            try
            {
                await _unitOfWork.Students.Add(student);
                    _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                Student student = await _unitOfWork.Students.Find(s => s.StudentId == id);
               if (student != null)
                {
                     _unitOfWork.Students.Delete(student);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<IEnumerable<StudentDTO>> GetAll()
        {
            try
            {
                var studentsWithDepartments = await _unitOfWork.Students.GetAll(s => s.Department);
                var studentDtos = studentsWithDepartments.Select(s => Student.ToStudentDTO(s));
                return studentDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<StudentDTO>();
            }
        }

        public async Task<StudentDTO> GetStudentById(Guid id)
        {
            try
            {
                var student = await _unitOfWork.Students.Find(s=>s.StudentId==id,s=>s.Department);
                var studentDto = Student.ToStudentDTO(student);
                return studentDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StudentDTO();
            }
        }

        public async Task UpdateAsync(StudentDTO studentDto)
        {
            var student = StudentDTO.ToStudent(studentDto);
            try
            {
                _unitOfWork.Students.Update(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
