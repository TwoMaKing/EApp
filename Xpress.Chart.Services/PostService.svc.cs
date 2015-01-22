using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Commands;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.LightBus;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chart.DataObjects;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Commands;
using Xpress.Chart.Domain.Events;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using Xpress.Chart.Domain.Services;
using Xpress.Chart.ServiceContracts;


namespace Xpress.Chart.Services
{

    public class PostService : IPostCommandService
    {
        public void PublishPost(PostDataObject post)
        {
            throw new NotImplementedException();
        }
    }
}
