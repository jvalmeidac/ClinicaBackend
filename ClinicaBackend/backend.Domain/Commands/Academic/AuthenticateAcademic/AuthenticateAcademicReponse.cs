using System;

namespace backend.Domain.Commands.Academic.AuthenticateAcademic
{
    public class AuthenticateAcademicReponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public bool Authenticated { get; set; }

        public static explicit operator AuthenticateAcademicReponse(Entities.Academic academic)
        {
            return new AuthenticateAcademicReponse()
            {
                Id = Guid.Parse(academic.IdAcademic),
                Authenticated = true,
                FirstName = academic.FirstName
            };
        }
    }
}
