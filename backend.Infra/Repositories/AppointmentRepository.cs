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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DbSession _session;

        public AppointmentRepository(DbSession session)
        {
            _session = session;
        }

        public void Add(Appointment entity)
        {
            string sql = "INSERT INTO appointments VALUES(@AppointmentId, @Description, @Schedule, " +
                "@PatientId, @AppointmentType, @AppointmentStatus)";
            _session.Connection.Execute(sql, entity, _session.Transaction);
        }

        public List<Appointment> GetAppointmentsByPatientId(Guid id, PageParameters pageParameters)
        {
            string sql = $"SELECT * FROM appointments WHERE PatientId = '{id}' ORDER BY Schedule" +
                $" LIMIT {pageParameters.PageNumber}, {pageParameters.PageSize}";
            List<Appointment> appointments = _session.Connection.Query<Appointment>(sql).ToList();

            return appointments;
        }

        public Appointment Edit(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid id)
        {
            string sql = $"SELECT COUNT(1) FROM appointments WHERE AppointmentId = '{id}'";
            var exists = _session.Connection.ExecuteScalar<bool>(sql);

            return exists;
        }

        public List<Appointment> GetAll(PageParameters pageParameters)
        {
            throw new NotImplementedException();
        }

        public Appointment GetOne(Guid id)
        {
            string sql = $"SELECT * FROM appointments WHERE AppointmentId = '{id}'";
            var appointment = _session.Connection.Query<Appointment>(sql).FirstOrDefault();

            return appointment;
        }

        public void Remove(Guid id)
        {
            string sql = $"DELETE FROM appoitments WHERE AppointmentId = '{id}'";
            _session.Connection.Execute(sql, _session.Transaction);
        }

        public int GetAppointmentsCount(Guid id)
        {
            string sql = $"SELECT COUNT(*) FROM appointments WHERE PatientId = '{id}'";
            int count = _session.Connection.ExecuteScalar<int>(sql);

            return count;
        }
    }
}
