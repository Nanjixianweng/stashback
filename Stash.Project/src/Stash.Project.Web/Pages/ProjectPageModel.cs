using Stash.Project.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Stash.Project.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ProjectPageModel : AbpPageModel
{
    protected ProjectPageModel()
    {
        LocalizationResourceType = typeof(ProjectResource);
    }
}
