using Stash.Project.Localization;
using Volo.Abp.Application.Services;

namespace Stash.Project;

/* Inherit your application services from this class.
 */
public abstract class ProjectAppService : ApplicationService
{
    protected ProjectAppService()
    {
        LocalizationResource = typeof(ProjectResource);
    }
}
