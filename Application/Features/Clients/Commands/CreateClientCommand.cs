using Application.Wrappers;
using MediatR;
using System;

namespace Application.Features.Clients.Commands
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
