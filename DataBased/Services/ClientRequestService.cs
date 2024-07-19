using TranscriberApi.DataBased.Context;
using TranscriberApi.DataBased.Models;
using Dapper;

namespace TranscriberApi.DataBased.Services
{
    public class ClientRequestService
    {
        private readonly ClientsContext _context;

        public ClientRequestService(ClientsContext context)
        {
            _context = context;
        }

        public async Task<ClientRequestModel> GetClientByPhoneNumberAsync(string phoneNumber, DateTime start, DateTime end)
        {
            using var connection = _context.CreateConnection();
            var query = @"
                SELECT c.FullName AS ClientFullName, 
                       cl.FullName AS CallerFullName, 
                       LoanAmount
                FROM Clients c
                INNER JOIN Calls cl ON c.PhoneNumber = cl.PhoneNumber
                WHERE c.PhoneNumber = @PhoneNumber"; //add param start time

            
            var parameters = new { PhoneNumber = phoneNumber};
            var result = await connection.QuerySingleOrDefaultAsync<ClientRequestModel>(query, parameters);

            return result!;
        }
    }
}
