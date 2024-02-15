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
        public DepartmentServiceTest(IDepartmentService departmentService)
        {
            _departmentService = departmentService;

        }
        #region AddDepartment
        //When the Department request is null -> Throw ArgumentNullException
        [Fact]
        public async Task AddDepartment_NullDepartment()
        {
            //Arrang
            DepartmentAddRequest? request = null;
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _departmentService.AddDepartment(request);
            });


        }
        //When the Department name is null -> Throw ArgumentException
        [Fact]
        public async Task AddDepartment_DeparmentNameNull()
        {
            //Arrang
            DepartmentAddRequest? request = new DepartmentAddRequest() { DepartmentName = null };
            //Assert
            await Assert.ThrowsAsync<ArgumentException>( async () =>
            {
                //Act
                await _departmentService.AddDepartment(request);
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
        public async Task AddDepartment_ProperDepartment()
        {
            //Arrang
            DepartmentAddRequest? request = new DepartmentAddRequest() { DepartmentName = "Sales" };
            //Act
            DepartmentResponse response = await _departmentService.AddDepartment(request);
            //Assert
            Assert.True(response.DeparmentId != Guid.Empty);
        }
        #endregion

        #region GetAllDepartment
        //

        [Fact]
        public async Task GetAllDepartment_Test()
        {
            //Arrange
            List<DepartmentAddRequest> departmentRequestList = new List<DepartmentAddRequest> { 
                new DepartmentAddRequest() {DepartmentName = "IT" },
                new DepartmentAddRequest() {DepartmentName = "Sales"} 
            };
            List<DepartmentResponse> departmentResponseList = new List<DepartmentResponse>();

            //Act
            foreach (DepartmentAddRequest department in departmentRequestList)
            {
                DepartmentResponse response = await _departmentService.AddDepartment(department);
                departmentResponseList.Add(response);

            }
            List<DepartmentResponse> actualResponseList = await _departmentService.GetAllDepartment();

            foreach (DepartmentResponse expected in departmentResponseList)
            {
                //Assert.Contains(expected, actualResponseList);
                Assert.Contains(actualResponseList, item => item.DeparmentId == expected.DeparmentId);
            }
        }
        #endregion
        #region GetCountryByCountryID

        [Fact]
        //If we supply null as CountryID, it should return null as CountryResponse
        public async Task GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? departmentID = null;

            //Act
            DepartmentResponse? department_response_from_get_method = await _departmentService.GetDepartmentByID(departmentID);


            //Assert
            Assert.Null(department_response_from_get_method);
        }


        [Fact]
        //If we supply a valid country id, it should return the matching country details as CountryResponse object
        public async Task GetDepartmentByID_ValidDepartmenID()
        {
            //Arrange
            DepartmentAddRequest? department_add_request = new DepartmentAddRequest() { DepartmentName = "Sales" };
            DepartmentResponse department_response_from_add =await _departmentService.AddDepartment(department_add_request);

            //Act
            DepartmentResponse? department_response_from_get = 
                await _departmentService.GetDepartmentByID(department_response_from_add.DeparmentId);

            //Assert
            Assert.Equal(department_response_from_add.DeparmentId, department_response_from_get.DeparmentId);
        }
        #endregion
    }
}
