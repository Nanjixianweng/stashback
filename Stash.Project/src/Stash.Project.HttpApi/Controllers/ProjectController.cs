using Stash.Project.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Stash.Project.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProjectController : AbpControllerBase
{
    protected ProjectController()
    {
        LocalizationResource = typeof(ProjectResource);
    }
}
