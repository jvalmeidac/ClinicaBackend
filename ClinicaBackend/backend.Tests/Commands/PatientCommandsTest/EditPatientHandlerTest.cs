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
                Id= Guid.Parse("97627c60-95f8-4701-ac59-9eb00c71e3c9"),
                FirstName = "Pedro",
                LastName = "Alves",
                Email = "palves@email.com",
                Password = "teste123",
                Phone = "99999999999",
                BirthDate = new DateTime(2000, 04, 19),
                CPF = "06017365320",
                RG = "0396069620105",
                CEP = "65980000",
                Address = "Rua do Petroleo",
                District = "Centro",
                Complement = null,
                City = "Carolina",
                State = "MA"
            };


            var result = await editPatientHandler.Handle(patient, CancellationToken.None);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task EditPatientWithInvalidData()
        {
            EditPatientRequest patient = new()
            {
                Id = Guid.Empty,
                FirstName = "Pedro",
                LastName = "Alves",
                Email = "palves@email.com",
                Password = "teste123",
                Phone = "99999999999",
                BirthDate = new DateTime(2000, 04, 19),
                CPF = "06017365320",
                RG = "0396069620105",
                CEP = "65980000",
                Address = "Rua do Petroleo",
                District = "Centro",
                Complement = null,
                City = "Carolina",
                State = "MA"
            };

            var result = await editPatientHandler.Handle(patient, CancellationToken.None);
            Assert.IsFalse(result.Success);
        }
    }
}
