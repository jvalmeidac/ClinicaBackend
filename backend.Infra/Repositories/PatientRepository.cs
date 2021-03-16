using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories;
using backend.Infra.Repositories.Base;
using Dapper;
using System;
using System.Collections.Generic;

namespace backend.Infra.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DbSession _session;

        public PatientRepository(DbSession session)
        {
            _session = session;
        }

        public void Add(Patient entity)
        {
            string sql = "INSERT INTO projetotcc.patients VALUES('Id', 'FirstName', 'LastName', 'Email'," +
                " 'Password', 'Phone', 'BirthDate', 'CPF', 'RG', 'CreatedAt')";
            _session.Connection.Execute(sql, entity, _session.Transaction);
        }

        public Patient Edit(Patient entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            throw new NotImplementedException();
        }

        public Patient GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Func<Patient, bool> where)
        {
            throw new NotImplementedException();
        }
    }
}
