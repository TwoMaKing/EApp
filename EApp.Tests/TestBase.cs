using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EApp.Common;
using EApp.Common.Serialization;
using EApp.Core;
using EApp.Core.Application;
using EApp.Data;
using EApp.Data.Mapping;
using EApp.Data.Queries;
using NUnit.Framework;
using Xpress.Chat.Domain.Models;

namespace EApp.Tests
{
    public class TestBase
    {
        public TestBase() 
        {
            EAppRuntime.Instance.Create();
        }
    }
}
