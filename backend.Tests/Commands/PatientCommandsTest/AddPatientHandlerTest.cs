using backend.Domain.Commands.Patient.AddPatient;
using backend.Domain.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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

        [TestInitialize]
        public void Init()
        {
            patientRepositoryMock = new Mock<IPatientRepository>();
            patientHandler = new AddPatientHandler(patientRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddPatientWithValidData()
        {
            AddPatientRequest patient = new()
            {
                FirstName = "Pedro",
                LastName = "Alves",
                Email = "palves@email.com",
                Password = "teste123",
                Phone = "99999999999",
                BirthDate = new DateTime(2000,04,19),
                CPF = "06017365320",
                RG = "0396069620105"
            };

            var result = await patientHandler.Handle(patient, CancellationToken.None);
            Assert.AreEqual(true, result.Success);
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
