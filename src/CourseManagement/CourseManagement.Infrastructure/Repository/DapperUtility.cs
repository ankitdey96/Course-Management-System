using CourseManagement.Domain.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CourseManagement.Infrastructure.Repository
{
    public class DapperUtility:IDapperUtility
    {
        private readonly DbConnection _connection;

        public DapperUtility(DbConnection dbConnection) 
        {
            _connection = dbConnection;
        }

        public async Task<(IEnumerable<TReturn> result, IDictionary<string, object> outValues)> ExecuteStoredProcedure<TReturn>(string storedProcedureName, IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null) where TReturn : class, new()

        {
            var DbCommand = CreateCommand(storedProcedureName, parameters, outParameters);
            bool connectionOpened = false;
            if(_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
                connectionOpened = true;
            }
            IEnumerable<TReturn> result = null;
            try
            {
                result = await _connection.QueryAsync<TReturn>(DbCommand);
            }
            catch(Exception ex)
            {
                _connection.Close();
            }
            finally
            {
                if (connectionOpened)
                    await _connection.CloseAsync();
            }

            var dynamicParameters = (DynamicParameters)DbCommand.Parameters;
            IDictionary<string,object> outValues = new Dictionary<string, object>();
            if (outParameters is not null)
            {
                foreach (var outParam in outParameters)
                {
                    outValues[outParam.Key] = dynamicParameters.Get<object>(outParam.Key);
                }
            }
            return (result,outValues);
        }

        private CommandDefinition CreateCommand(string storedProcedureName,
         IDictionary<string, object> parameters = null,
         IDictionary<string, Type> outParameters = null)
        {
            var dynamicParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    dynamicParameters.Add(item.Key,GetPerameterValue(item.Value));
                }
            }

            if (outParameters != null)
            {
                foreach (var item in outParameters)
                {
                    dynamicParameters.Add(item.Key,null ,dbType: GetDbTypeFromType(item.Value), direction:ParameterDirection.Output);
                }
            }
            return new CommandDefinition(storedProcedureName,dynamicParameters,null,null,CommandType.StoredProcedure);
        }

        private object GetPerameterValue(object parameterValue)
        {
            if (parameterValue != null && parameterValue.GetType().Name == "DateTime")
            {
                if (Convert.ToDateTime(parameterValue) < SqlDateTime.MinValue.Value)
                    return SqlDateTime.MinValue.Value;
                else
                    return parameterValue;
            }
            else
                return parameterValue;
        }
        private DbType GetDbTypeFromType(Type type)
        {
            if (type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(short) ||
                type == typeof(ushort))
                return DbType.Int32;
            else if (type == typeof(long) || type == typeof(ulong))
                return DbType.Int64;
            else if (type == typeof(double) || type == typeof(decimal))
                return DbType.Decimal;
            else if (type == typeof(string))
                return DbType.String;
            else if (type == typeof(DateTime))
                return DbType.DateTime;
            else if (type == typeof(bool))
                return DbType.Boolean;
            else if (type == typeof(Guid))
                return DbType.Guid;
            else if (type == typeof(char))
                return DbType.String;
            else
                return DbType.String;
        }

    }
}
