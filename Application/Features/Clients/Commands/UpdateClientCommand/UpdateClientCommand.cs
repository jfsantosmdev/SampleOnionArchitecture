using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;

        public UpdateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);

            if (client == null)
            {
                throw new KeyNotFoundException($"No client found with Id {request.Id}");
            }
            else
            {
                client.FirstName = request.FirstName;
                client.LastName = request.LastName;
                client.BirthDate = request.BirthDate;
                client.Phone = request.Phone;
                client.Email = request.Email;
                client.Address = request.Address;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<int>(client.Id);
            }
        }
    }
}
