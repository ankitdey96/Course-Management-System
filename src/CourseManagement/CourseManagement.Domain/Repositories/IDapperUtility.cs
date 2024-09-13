using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Repositories
{
    public interface IDapperUtility
    {
        Task<(IEnumerable<TReturn> result, IDictionary<string, object> outValues)> ExecuteStoredProcedure<TReturn>(string storedProcedureName, IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null) where TReturn : class, new();

    }
}
