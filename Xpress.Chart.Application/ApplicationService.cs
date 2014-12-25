using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chart.DataObjects;
using Xpress.Chart.Domain;
using Xpress.Chart.Repositories;
using Xpress.Chart.ServiceContracts;

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
        
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
