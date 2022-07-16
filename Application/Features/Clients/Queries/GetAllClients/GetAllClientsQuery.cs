using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<PagedResponse<List<ClientDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDTO>>>
        {
            private readonly IRepositoryAsync<Client> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllClientsHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }
            public async Task<PagedResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var clients = await _repositoryAsync.ListAsync(new PagedClientsSpecification(request.PageSize, request.PageNumber, request.FirstName, request.LastName));
                var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);

                return new PagedResponse<List<ClientDTO>>(clientsDTO, request.PageNumber, request.PageSize);
            }
        }
    }
}
