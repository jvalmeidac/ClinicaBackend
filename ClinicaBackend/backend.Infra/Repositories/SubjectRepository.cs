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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DbSession _session;

        public SubjectRepository(DbSession session)
        {
            _session = session;
        }

        public void Add(Subject entity)
        {
            string sql = "INSERT INTO subjects VALUES(@SubjectId, @Name, @Description," +
                " @Code, @WeekDay, @DayPeriod)";
            _session.Connection.Execute(sql, entity, _session.Transaction);
        }

        public Subject Edit(Subject entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Subject> GetAll(PageParameters pageParameters)
        {
            string sql = $"SELECT * FROM subjects ORDER BY Name LIMIT {pageParameters.PageNumber}, 10";
            List<Subject> subjects = _session.Connection.Query<Subject>(sql).ToList();

            return subjects;
        }

        public Subject GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetSubjectsCount()
        {
            string sql = "SELECT COUNT(1) FROM subjects";
            int count = _session.Connection.ExecuteScalar<int>(sql);

            return count;
        }
    }
}
