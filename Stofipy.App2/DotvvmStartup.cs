using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Stofipy.App2
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);

            // https://www.dotvvm.com/docs/4.0/pages/concepts/configuration/explicit-assembly-loading
            config.ExperimentalFeatures.ExplicitAssemblyLoading.Enable();

            // Use this for command heavy applications
            // - DotVVM will store the viewmodels on the server, and client will only have to send back diffs
            // https://www.dotvvm.com/docs/4.0/pages/concepts/viewmodels/server-side-viewmodel-cache
            // config.ExperimentalFeatures.ServerSideViewModelCache.EnableForAllRoutes();

            // Use this if you are deploying to containers or slots
            //  - DotVVM will precompile all views before it appears as ready
            // https://www.dotvvm.com/docs/4.0/pages/concepts/configuration/view-compilation-modes
            // config.Markup.ViewCompilation.Mode = ViewCompilationMode.DuringApplicationStart;
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Home", "", "Views/Home.dothtml");
            config.RouteTable.Add("AuthorDetail", "author/{id:guid}", "Views/AuthorDetailView.dothtml");
            config.RouteTable.Add("PlaylistDetail", "playlist/{id:guid}", "Views/PlaylistDetailView.dothtml");
            config.RouteTable.Add("AlbumDetail", "album/{id:guid}", "Views/AlbumDetailView.dothtml");


            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config, viewsFolder: "Views"));   
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Left", "Sections/SectionLeft.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Right", "Sections/SectionRight.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Bottom", "Sections/SectionBottom.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Top", "Sections/SectionTop.dotcontrol");

        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("Styles", new StylesheetResource()
            {
                Location = new UrlResourceLocation("~/Resources/style.css")
            });
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
            options.AddHotReload();
        }
    }
}
