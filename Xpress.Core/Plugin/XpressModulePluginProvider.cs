using System.Collections.Generic;
using System.Linq;
using EApp.Core.Plugin;
using Xpress.Core.Common;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;

namespace Xpress.Core.Plugin
{
    public class XpressModulePluginProvider : RibbonModulePluginProvider
    {
        static XpressModulePluginProvider()
        {
            NavigationNodeList.Add(new NavigationNodeItem("Cost", "Cost Inputs"));

            NavigationNodeList.Add(new NavigationNodeItem("Price", "Aadvanced Pricing"));
            
            InitializeCostModulePluginItems();

            InitializePriceModulePluginItems();
        }

        public XpressModulePluginProvider() { }

        private static void InitializeCostModulePluginItems() 
        {
            NavigationNodeItem costNavigation = NavigationNodeList[0];

            PluginItemCollection<RibbonModulePluginItem> costPluginModuleCommandItems = new PluginItemCollection<RibbonModulePluginItem>();

            var purchaseModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Purchase", "Purchase", "server_add_32x32", string.Empty, costNavigation);
            var LeaseModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Lease", "Lease", "contract_32x32", string.Empty, costNavigation);
            var maintenanceModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Maintenance", "Maintenance", "toolbox_32x32", string.Empty, costNavigation);
            var hrModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("HR", "HR", "user_monitor_32x32", string.Empty, costNavigation);
            var laborModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Labor", "Labor", "users4_32x32", string.Empty, costNavigation);
            var standardServiceModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Standard Service", "Standard Service", "box_software_32x32", string.Empty, costNavigation);

            var costAddNewModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Add New Cost", "New Line", "table_row_add_32x32", string.Empty, costNavigation);
            
            costAddNewModuleCommandItem.Alignment = RibbonButtonAlignment.Right;

            costPluginModuleCommandItems.AddRange(new RibbonModulePluginItem[] 
            { purchaseModuleCommandItem, LeaseModuleCommandItem, maintenanceModuleCommandItem, 
              hrModuleCommandItem, laborModuleCommandItem, standardServiceModuleCommandItem, costAddNewModuleCommandItem });

            PluginsByNavigation.Add(costNavigation, costPluginModuleCommandItems);
        }

        private static void InitializePriceModulePluginItems()
        {
            NavigationNodeItem priceNavigation = NavigationNodeList[1];

            PluginItemCollection<RibbonModulePluginItem> pricePluginModuleCommandItems = new PluginItemCollection<RibbonModulePluginItem>();

            var addDirectPricingModuleCommandItem =
                CreateXpressModulePluginItem<XpressModulePluginItem>("Add Direct Pricing", "Add Pricing", "add_16x16", 
                string.Empty, RibbonButtonAlignment.Right, priceNavigation);

            var deleteDirectPricingModuleCommandItem =
                CreateXpressModulePluginItem<XpressModulePluginItem>("Delete Direct Pricing", "Delete Pricing", "CANCEL_RED_16x16", 
                string.Empty, RibbonButtonAlignment.Right, priceNavigation);

            var addNonStandardPaymentModuleCommandItem =
                CreateXpressModulePluginItem<XpressModulePluginItem>("Add Non-Standard Payment", "Add Payment", "add_16x16", 
                string.Empty, RibbonButtonAlignment.Right, priceNavigation);

            var deleteNonStandardPaymentModuleCommandItem =
                CreateXpressModulePluginItem<XpressModulePluginItem>("Delete Non-Standard Payment", "Delete Payment", "CANCEL_RED_16x16", 
                string.Empty, RibbonButtonAlignment.Right, priceNavigation);


            var directPricingModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Direct Pricing", "Direct Pricing", "invoice_dollar_32x32", 
                string.Empty, priceNavigation);
            
            directPricingModuleCommandItem.SubItems.AddRange(new XpressModulePluginItem[] 
            { addDirectPricingModuleCommandItem, deleteDirectPricingModuleCommandItem });


            var nonStandardPaymentModuleCommandItem = CreateXpressModulePluginItem<XpressModulePluginItem>("Non-Standard Payment", "Non-Standard Payment", 
                "money_envelope_32x32", string.Empty, priceNavigation);

            nonStandardPaymentModuleCommandItem.SubItems.AddRange(new XpressModulePluginItem[] 
            { addNonStandardPaymentModuleCommandItem, deleteNonStandardPaymentModuleCommandItem });

            pricePluginModuleCommandItems.AddRange(new XpressModulePluginItem[] { directPricingModuleCommandItem, 
                nonStandardPaymentModuleCommandItem });

            PluginsByNavigation.Add(priceNavigation, pricePluginModuleCommandItems);            
        }

    }
}
