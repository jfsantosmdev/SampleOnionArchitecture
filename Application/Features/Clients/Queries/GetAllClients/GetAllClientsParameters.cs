using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsParameters : RequestParameter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
