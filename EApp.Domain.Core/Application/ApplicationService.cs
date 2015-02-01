using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Domain.Core.Repositories;

namespace EApp.Domain.Core.Application
{
    public abstract class ApplicationService : IDisposable
    {
        private IRepositoryContext repositoryContext;

        public ApplicationService(IRepositoryContext repositoryContext)
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

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
