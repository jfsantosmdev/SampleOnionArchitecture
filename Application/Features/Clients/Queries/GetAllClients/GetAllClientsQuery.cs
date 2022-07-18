using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
            private readonly IDistributedCache _distributedCache;
            private readonly IMapper _mapper;

            public GetAllClientsHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }
            public async Task<PagedResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var cacheKey = $"clientsList_{request.PageSize}_{request.PageNumber}_{request.FirstName}_{request.LastName}";
                string serializedClientsList;
                var clientsList = new List<Client>();

                var redisClientsList = await _distributedCache.GetAsync(cacheKey);
                if (redisClientsList != null)
                {
                    serializedClientsList = Encoding.UTF8.GetString(redisClientsList);
                    clientsList = JsonConvert.DeserializeObject<List<Client>>(serializedClientsList);
                }
                else
                {
                    clientsList = await _repositoryAsync.ListAsync(new PagedClientsSpecification(request.PageSize, request.PageNumber, request.FirstName, request.LastName));
                    serializedClientsList = JsonConvert.SerializeObject(clientsList);
                    redisClientsList = Encoding.UTF8.GetBytes(serializedClientsList);

                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey, redisClientsList, options);
                }

                var clientsDTO = _mapper.Map<List<ClientDTO>>(clientsList);
                return new PagedResponse<List<ClientDTO>>(clientsDTO, request.PageNumber, request.PageSize);
            }
        }
    }
}
