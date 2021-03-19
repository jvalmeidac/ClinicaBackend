using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories;
using backend.Infra.Repositories.Base;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

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
            string sql = "UPDATE projetotcc.patients SET FirstName = @FirstName, LastName = @LastName, " +
                "Email = @Email, Password = @Password, " +
                "Phone = @Phone, BirthDate = @BirthDate, CPF = @CPF, RG = @RG WHERE Id = " + entity.Id;
            _session.Connection.Execute(sql, entity, _session.Transaction);

            return entity;
        }

        public IEnumerable<Patient> GetAll()
        {
            string sql = "SELECT * FROM projetotcc.patients";
            IEnumerable<Patient> patients = (IEnumerable<Patient>)_session.Connection.Query<IEnumerable<Patient>>(sql);

            return patients;
        }

        public Patient GetOne(Guid id)
        {
            string sql = "SELECT * FROM projetotcc.patients WHERE Id =" + id;
            Patient patient = _session.Connection.Query<Patient>(sql).FirstOrDefault();

            return patient;
        }

        public void Remove(Guid id)
        {
            string sql = "DELETE FROM projetotcc.patients WHERE Id =" + id;
            _session.Connection.Execute(sql);
        }

        public bool Exists(Guid id)
        {
            string sql = "SELECT COUNT(1) FROM projetotcc.patients WHERE Id=" + id;
            var exists = _session.Connection.ExecuteScalar<bool>(sql);

            return exists;
        }

        public bool Exists(string email, string cpf, string rg)
        {
            string sql = "SELECT COUNT(1) FROM projetotcc.patients WHERE Email = @email OR CPF = @cpf OR RG = @RG" + new { email, cpf, rg };
            var exists = _session.Connection.ExecuteScalar<bool>(sql);

            return exists;
        }
    }
}
