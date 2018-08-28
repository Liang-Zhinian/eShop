using Autofac;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        //public string QueriesConnectionString { get; }

        public ApplicationModule(/*string qconstr*/)
        {
            //QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            

        }
    }
}
