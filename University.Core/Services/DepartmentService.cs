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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IUnitOfWork unitOfWork, ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task CreateAsync(DepartmentDTO departmentDTO)
        {
            Department department = DepartmentDTO.ToDepartment(departmentDTO);
            try
            {
                await _unitOfWork.Departments.Add(department);
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
                Department department = await _unitOfWork.Departments.Find(d => d.DepartmentId == id);
                if (department != null)
                {
                    _unitOfWork.Departments.Delete(department);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAll()
        {
            try
            {
                var departments = await _unitOfWork.Departments.GetAll();
                var departmentDtos = departments.Select(d => Department.ToDepartmentDTO(d));
                return departmentDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<DepartmentDTO>();
            }
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllWithStudent()
        {
            try
            {
                var departments = await _unitOfWork.Departments.GetAll(d=>d.Students);
                var departmentDtos = departments.Select(d => Department.ToDepartmentDTO(d));
                return departmentDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<DepartmentDTO>();
            }
        }

        public async Task<DepartmentDTO> GetDepartmentById(Guid id)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetById(id);
                var departmentDto = Department.ToDepartmentDTO(department);
                return departmentDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DepartmentDTO();
            }
        }

        public async Task UpdateAsync(DepartmentDTO departmentDto)
        {
            var department = DepartmentDTO.ToDepartment(departmentDto);
            try
            {
                _unitOfWork.Departments.Update(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
