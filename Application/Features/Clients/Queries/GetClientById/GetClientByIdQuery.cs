using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
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

namespace Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<Response<ClientDTO>>
    {
        public int Id { get; set; }

        public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Response<ClientDTO>>
        {
            private readonly IRepositoryAsync<Client> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetClientByIdQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"No client found with Id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<ClientDTO>(client);
                    return new Response<ClientDTO>(dto);
                }
            }
        }
    }
}
