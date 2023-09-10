using Dapper;
using dotnet_demo.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace dotnet_demo.Repositories
{
    public class BaseRepository
    {
        private readonly ILogger<BaseRepository> _logger;
        private readonly AppSettings _appSettings;

        public BaseRepository(IOptions<AppSettings> appSettings, ILogger<BaseRepository> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_appSettings.connection);
            }
        }

        public bool Execute(string sQuery, object request = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection dbConnection = Connection)
            {
                bool result = false;
                try
                {
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, request, commandType: commandType);

                    result = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Execute");
                }
                return result;
            }
        }

        public List<T> GetList<T>(string sQuery, object request = null, CommandType commandType = CommandType.Text) where T : new()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(sQuery, request, commandType: commandType).ToList();
            }
        }

        public bool Insert(Object request, string sQuery)
        {
            using (IDbConnection dbConnection = Connection)
            {
                bool result = false;
                try
                {
                    dbConnection.Open();
                    if (dbConnection.Execute(sQuery, request) > 0) result = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Insert");
                }
                return result;
            }
        }

        public bool Update(Object request, string sQuery)
        {
            using (IDbConnection dbConnection = Connection)
            {
                bool result = false;
                try
                {
                    dbConnection.Open();
                    if (dbConnection.Execute(sQuery, request) > 0) result = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Update");
                }
                return result;
            }
        }

        public bool Delete(string id, string sQuery)
        {
            using (IDbConnection dbConnection = Connection)
            {
                bool result = false;
                try
                {
                    dbConnection.Open();
                    if (dbConnection.Execute(sQuery, new { id }) > 0) result = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Delete");
                }
                return result;
            }
        }

    }
}
