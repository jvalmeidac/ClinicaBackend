﻿using backend.Domain.Interfaces.Repositories;
using backend.Domain.Interfaces.Repositories.Base;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.AddPatient
{
    public class AddPatientHandler : Notifiable, IRequestHandler<AddPatientRequest, Response>
    {
        //Injeção de Dependência
        private readonly IPatientRepository _patientRepository;
        private readonly IUnityOfWork _unityOfWork;

        public AddPatientHandler(IPatientRepository patientRepository,
            IUnityOfWork unityOfWork)
        {
            _patientRepository = patientRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<Response> Handle(AddPatientRequest request, CancellationToken cancellationToken)
        {

            //Verifica se a requisição é válida
            if (request == null)
            {
                AddNotification("Request", "A requisição é inválida!");
                return new Response(this);
            }

            //Verifica se o paciente já está cadastrado
            if (_patientRepository.Exists(request.Email, request.CPF, request.RG))
            {
                AddNotification("Paciente", "Paciente já cadastrado");
                return new Response(this);
            }

            //Instancia o paciente e verifica se existe algum dado inválido
            Entities.Patient patient = new(request.FirstName, request.LastName,
                request.Email, request.Password, request.Phone, request.BirthDate, request.CPF, request.RG);
            AddNotifications(patient);

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Insere os dados no banco
            _unityOfWork.BeginTransaction();
            _patientRepository.Add(patient);
            _unityOfWork.Commit();

            //Cria o objeto da resposta
            var response = new Response(this, patient);

            //Retorna a resposta
            return await Task.FromResult(response);
        }
    }
}
