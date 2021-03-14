using backend.Domain.Entities.Base;

namespace backend.Domain.Entities
{
    public class Operator : EntityBase
    {
        public Operator(string firstName, string lastName, string email, string password) 
            : base(firstName, lastName, email, password)
        {}
    }

}
