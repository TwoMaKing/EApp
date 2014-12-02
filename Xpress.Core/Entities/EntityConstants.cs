using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public sealed class EntityConstants
    {
        private EntityConstants() { }

        static EntityConstants() 
        {
            InitializeEnumValueTextPair();
        }

        public const string Status_Current = "STATUS_CURRENT";
        public const string Status_New = "STATUS_NEW";
        public const string Status_Edit = "STATUS_EDITED";
        public const string Status_Deletion = "STATUS_DELETED";

        public static string[][] ProductionSources { get; private set; }

        public static string[][] HRSettlementModes { get; private set; }

        public static string[][] InvoicingTreatments { get; private set; }

        public static string[][] CompliantLeaseOptions { get; private set; }

        public static string GetTextByEnumValue(string[][] dictionary, Enum value)
        {
            if (dictionary == null ||
                dictionary.Length == 0 ||
                value == null)
            {
                return null;
            }

            for (int i = 0; i < dictionary.Length; i++)
            {
                if (dictionary[i][0].Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return dictionary[i][1];
                }
            }

            return null;
        }

        private static void InitializeEnumValueTextPair()
        {
            ProductionSources = new string[2][];
            ProductionSources[0] = new string[] { ProductionSource.HP.ToString(), "HP Production" };
            ProductionSources[1] = new string[] { ProductionSource.ThirdParty.ToString(), "Third Party Production" };

            HRSettlementModes = new string[4][];
            HRSettlementModes[0] = new string[] { HRSettlementMode.RetentionBonus.ToString(), "Retention Bonus" };
            HRSettlementModes[1] = new string[] { HRSettlementMode.Severance.ToString(), "Severance" };
            HRSettlementModes[2] = new string[] { HRSettlementMode.Relocation.ToString(), "Relocation" };
            HRSettlementModes[3] = new string[] { HRSettlementMode.Miscellaneous.ToString(), "Miscellaneous" };

            InvoicingTreatments = new string[3][];
            InvoicingTreatments[0] = new string[] { InvoicingTreatment.Upfront.ToString(), "Upfront" };
            InvoicingTreatments[1] = new string[] { InvoicingTreatment.Spread.ToString(), "Spread" };
            InvoicingTreatments[2] = new string[] { InvoicingTreatment.Milestone.ToString(), "Milestone" };

            CompliantLeaseOptions = new string[3][];
            CompliantLeaseOptions[0] = new string[] { CompliantLeaseOption.FullAssetRecovery.ToString(), "Non-Compliant ICOEM - Full Asset Recovery" };
            CompliantLeaseOptions[1] = new string[] { CompliantLeaseOption.PartialAssetRecovery.ToString(), "Non-Compliant ICOEM - Partial Asset Recovery" };
            CompliantLeaseOptions[2] = new string[] { CompliantLeaseOption.Compliant.ToString(), "Compliant/Non-Compliant - Revenue Share" };
        }
    }
}
