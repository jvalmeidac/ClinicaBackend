using backend.Domain.Commands.Patient.EditPatient;
using backend.Domain.Interfaces.Repositories;
using backend.Domain.Interfaces.Repositories.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Tests.Commands.PatientCommandsTest
{
    [TestClass]
    public class EditPatientHandlerTest
    {
        EditPatientHandler editPatientHandler;
        Mock<IPatientRepository> patientRepositoryMock;
        Mock<IUnityOfWork> unityOfWork;

        [TestInitialize]
        public void Init()
        {
            patientRepositoryMock = new Mock<IPatientRepository>();
            unityOfWork = new Mock<IUnityOfWork>();
            editPatientHandler = new EditPatientHandler(patientRepositoryMock.Object, unityOfWork.Object);
        }

        [TestMethod]
        public async Task EditPatientWithValidData()
        {
            EditPatientRequest patient = new()
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

            patient.Id = Guid.NewGuid();

            var result = await editPatientHandler.Handle(patient, CancellationToken.None);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task EditPatientWithInvalidData()
        {
            EditPatientRequest patient = new()
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

            patient.Id = Guid.Empty;

            var result = await editPatientHandler.Handle(patient, CancellationToken.None);
            Assert.IsFalse(result.Success);
        }
    }
}
