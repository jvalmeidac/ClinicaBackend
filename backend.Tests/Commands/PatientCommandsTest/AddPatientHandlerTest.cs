using backend.Domain.Commands.Patient.AddPatient;
using backend.Domain.Interfaces.Repositories;
using backend.Domain.Interfaces.Repositories.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Tests.Commands.PatientCommandsTest
{
    [TestCategory("Commands")]
    [TestClass]
    public class AddPatientHandlerTest
    {
        AddPatientHandler patientHandler;
        Mock<IPatientRepository> patientRepositoryMock;
        Mock<IUnityOfWork> unitOfWork;

        [TestInitialize]
        public void Init()
        {
            patientRepositoryMock = new Mock<IPatientRepository>();
            unitOfWork = new Mock<IUnityOfWork>();
            patientHandler = new AddPatientHandler(patientRepositoryMock.Object, unitOfWork.Object);
        }

        [TestMethod]
        public async Task AddPatientWithValidData()
        {
            try
            {
                AddPatientRequest patient = new()
                {
                    FirstName = "Pedro",
                    LastName = "Alves",
                    Email = "palves@email.com",
                    Password = "teste123",
                    Phone = "99999999999",
                    BirthDate = new DateTime(2000, 04, 19),
                    CPF = "06017365320",
                    RG = "0396069620105"
                };

                var result = await patientHandler.Handle(patient, CancellationToken.None);
                Assert.AreEqual(true, result.Success);
            }
            catch (Exception ex)
            {
                Debug.Write($"{ex.Message}");
            }
        }

        [TestMethod]
        public async Task AddPatientWithInvalidData()
        {
            AddPatientRequest patient = new()
            {
                FirstName = "Pedro",
                LastName = "Alves",
                Email = "palvestest",
                Password = "teste123",
                Phone = "99999999999",
                BirthDate = DateTime.Today,
                CPF = "teste",
                RG = "123458"
            };

            var result = await patientHandler.Handle(patient, CancellationToken.None);
            Assert.AreEqual(false, result.Success);
        }
    }
}
