using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Commands.DeleteClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommand;
using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientsController : BaseApiController
    {
        //GET: api/<controller>
        [HttpGet()]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get([FromQuery] GetAllClientsParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientsQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                FirstName = filter.FirstName,
                LastName = filter.LastName
            }));
        }

        //GET: api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Put(int id, UpdateClientCommand command)
        {
            if(id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand { Id = id }));
        }
    }
}
