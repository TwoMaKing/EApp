using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Chart.Application
{
    public abstract class ApplicationService : IDisposable
    {
        private IRepositoryContext repositoryContext;

        protected ApplicationService(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        protected IRepositoryContext RepositoryContext 
        {
            get 
            {
                return this.repositoryContext;
            }
        }

        protected virtual void Dispose(bool disposing)
        { 
            if (disposing)
            {
                // to do...
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
