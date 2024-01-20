using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using StaffHub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.Tests
{
    public class DepartmentServiceTest
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentServiceTest()
        {
            _departmentService = new DepartmentService();

        }

        //When the Department request is null -> Throw ArgumentNullException
        [Fact]
        public void AddDepartment_NullDepartment()
        {
            //Arrang
            DepartmentAddRequest? request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _departmentService.AddDepartment(request);
            });


        }
        //When the Department name is null -> Throw ArgumentException
        [Fact]
        public void AddDepartment_DeparmentNameNull()
        {
            //Arrang
            DepartmentAddRequest? request = new DepartmentAddRequest() { DepartmentName = null };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _departmentService.AddDepartment(request);
            });

        }


        //When the Department name is duplicate -> Throw ArgumentException
        [Fact]
        public void AddDepartment_DuplicateDeparmentName()
        {
            //Arrang
            DepartmentAddRequest? request1 = new DepartmentAddRequest() { DepartmentName = "IT" };
            DepartmentAddRequest? request2 = new DepartmentAddRequest() { DepartmentName = "IT" };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _departmentService.AddDepartment(request1);
                _departmentService.AddDepartment(request2);
            });

        }

        //When supplied proper Department name insert it in to the Deparment list
        [Fact]
        public void AddDepartment_ProperDepartment()
        {
            //Arrang
            DepartmentAddRequest? request = new DepartmentAddRequest() { DepartmentName = "Sales" };
            //Act
            DepartmentResponse response = _departmentService.AddDepartment(request);
            //Assert
            Assert.True(response.DeparmentId != Guid.Empty);
        }
    }
}
