using backend.Domain.Interfaces.Repositories;
using backend.Domain.Pagination;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.ListAllPatients
{
    public class ListAllPatientsHandler : Notifiable, IRequestHandler<ListAllPatientsRequest, Response>
    {
        private readonly IPatientRepository _patientRepository;

        public ListAllPatientsHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response> Handle(ListAllPatientsRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é válida
            if (request == null)
            {
                AddNotification("Request", "A requisição é inválida!");
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Lista todos os pacientes
            var patients = _patientRepository.GetAll(request.pageParameters);
            var count = _patientRepository.GetPatientsCount();
            var paginationInfo = 
                new PagedList<Entities.Patient>(patients, count, 
                request.pageParameters.PageNumber, request.pageParameters.PageSize);

            //Cria o objeto da resposta
            var response = new Response(this, patients, paginationInfo);

            //Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
