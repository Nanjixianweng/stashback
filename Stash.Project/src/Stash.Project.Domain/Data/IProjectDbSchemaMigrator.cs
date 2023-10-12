using System.Threading.Tasks;

namespace Stash.Project.Data;

public interface IProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
