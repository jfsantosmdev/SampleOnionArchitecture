using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications
{
    public class PagedClientsSpecification : Specification<Client>
    {
        public PagedClientsSpecification(int pageSize, int pageNumber, string firstName, string lastName)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(firstName))
            {
                Query.Search(x => x.FirstName.ToLower(), "%" + firstName.ToLower() + "%");
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                Query.Search(x => x.LastName.ToLower(), "%" + lastName.ToLower() + "%");
            }
        }
    }
}
