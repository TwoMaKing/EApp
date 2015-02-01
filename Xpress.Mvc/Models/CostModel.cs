using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EApp.Domain.Core;

namespace Xpress.Mvc.Models
{
    public class CostModel
    {
        public IEnumerable<CostLine> Costs { get; set; }
    }

    public class CostLine 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int Popularity { get; set; }
    }

    public class Order 
    { 
    
    }

    public class OrderQueryRequest 
    {
        public int Id { get; set; }

        public string HostInfo { get; set; }
    }

    public enum productType
    {
        Annual,

        NonAnnual
    }

    public class Product : IEntity
    {
        public int Id
        {
            get;
            set;
        }

        public string Name { get; set; }

        public productType Type { get; set; }

        public Quotation Quotation { get; set; }

        public AnnualQuotation AnnualQuotation { get; set; }

        /// <summary>
        /// 受欢迎程度
        /// </summary>
        public int Popularity { get; set; }

        public int RecommandLevel { get; set; }

    }

    public class Quotation
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal StartingPrice { get; set; }

        public Information Info { get; set; }
    }

    public class AnnualQuotation : Quotation { }

    public class Information
    {
        public string Content { get; set; }

        public int Number { get; set; }
    }

    public class ProductRequest
    {
        public int OrderType { get; set; }

        public bool IsAnnualCover { get; set; }
    }

}
