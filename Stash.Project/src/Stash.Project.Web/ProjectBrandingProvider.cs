using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Stash.Project.Web;

[Dependency(ReplaceServices = true)]
public class ProjectBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Project";
}
