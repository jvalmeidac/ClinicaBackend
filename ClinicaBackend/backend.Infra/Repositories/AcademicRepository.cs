using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories;
using backend.Domain.Pagination;
using backend.Infra.Repositories.Base;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Infra.Repositories
{
    public class AcademicRepository : IAcademicRepository
    {
        private readonly DbSession _session;

        public AcademicRepository(DbSession session)
        {
            _session = session;
        }

        public void Add(Academic entity)
        {
            string sql = "INSERT INTO academics " +
                "VALUES(@IdAcademic, @FirstName, @LastName, @Email, @Password, @Registration, @CreatedAt)";
            _session.Connection.Execute(sql, entity, _session.Transaction);
        }

        public Academic Edit(Academic entity)
        {
            string sql = "UPDATE academics SET FirstName = @FirstName, LastName = @LastName, " +
                $"Email = @Email, Password = @Password, Registration = @Registration WHERE IdAcademic = '{entity.IdAcademic}'";
            _session.Connection.Execute(sql, entity, _session.Transaction);

            return entity;
        }

        public bool Exists(Guid id)
        {
            string sql = $"SELECT COUNT(1) FROM academics WHERE IdAcademic = '{id}'";
            bool exists = _session.Connection.ExecuteScalar<bool>(sql);

            return exists;
        }

        public List<Academic> GetAll(PageParameters pageParameters)
        {
            string sql = "SELECT * FROM academics";
            List<Academic> academics = (List<Academic>)_session.Connection.Query<Academic>(sql);

            return academics;
        }

        public Academic GetOne(Guid id)
        {
            string sql = $"SELECT * FROM academics WHERE IdAcademic = '{id}'";
            Academic academic = _session.Connection.QueryFirstOrDefault<Academic>(sql);

            return academic;
        }

        public void Remove(Guid id)
        {
            string sql = $"DELETE FROM academics WHERE IdAcademic = '{id}'";
            _session.Connection.Execute(sql, _session.Transaction);
        }

        public int GetAcademicsCount()
        {
            string sql = "SELECT COUNT(*) FROM academics";
            int count = _session.Connection.ExecuteScalar<int>(sql);

            return count;
        }

        public Academic Login(string email, string password)
        {
            string sql = $"SELECT * FROM academics WHERE Email='{email}' AND Password='{password}'";
            Academic academic = _session.Connection.QueryFirstOrDefault<Academic>(sql);

            return academic;
        }

        public List<Appointment> GetAppointmentsOpened()
        {
            string sql = $"SELECT * FROM appointments WHERE AcademicId IS NULL;";
            List<Appointment> appointments = _session.Connection.Query<Appointment>(sql).ToList();

            return appointments;
        }

        public List<Appointment> GetAppointmentsClosed(Guid academicId)
        {
            string sql = $"SELECT * FROM appointments WHERE AcademicId = '{academicId}'";
            List<Appointment> appointments = _session.Connection.Query<Appointment>(sql).ToList();

            return appointments;
        }
    }
}
