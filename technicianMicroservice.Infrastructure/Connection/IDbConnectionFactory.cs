using System.Data.Common;

namespace technicianMicroservice.Infrastructure.Connection;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection();

    string GetProviderName();
}
