using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Stash.Project.Web;

[Dependency(ReplaceServices = true)]
public class ProjectBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Project";
}
