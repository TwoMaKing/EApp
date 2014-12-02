using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Core.Common
{
    public enum ChangeStatus 
    { 
        /// <summary>
        /// No Changes 
        /// </summary>
        Current = 0,

        /// <summary>
        /// Newly added 
        /// </summary>
        New = 1,

        /// <summary>
        /// Edited
        /// </summary>
        Modification = 2,

        /// <summary>
        /// deleted
        /// </summary>
        Deletion = 3
    }

    /// <summary>
    /// Cost Type
    /// </summary>
    public enum CostLineType 
    { 
        Purchase,

        Lease,

        Maintenance,

        HR,

        Labor,

        StandardService,

        NA
    }


    public enum ProductionSource
    {
        HP,

        ThirdParty
    }

    public enum HRSettlementMode
    { 
        RetentionBonus,
        
        Severance,
        
        Relocation,

        Miscellaneous
    }

    public enum InvoicingTreatment 
    {
        Upfront,
        
        Spread,

        Milestone
    }


    public enum CompliantLeaseOption 
    {
        Compliant,

        PartialAssetRecovery,

        FullAssetRecovery
    }

    /// <summary>
    /// The privilege of the column.
    /// </summary>
    public enum GridViewColumnAccess
    {
        Edit = 10,

        View = 20,

        Hide = 30,
    }

    /// <summary>
    /// The control type of the column.
    /// </summary>
    public enum GridViewColumnType
    {
        EditBox = 10,

        ComboBox = 20,

        CheckBox = 30,
        
        RadioButton = 60,
        
        IconBox = 40,

        TextImageBox = 50,

        DateChooser = 60,

        MonthChooser = 70,

        MultipleLineEditBox = 80,

        NA = 250
    }

    /// <summary>
    /// The value type of the cell
    /// </summary>
    public enum GridViewCellValueType
    {
        Integer = 10,

        String = 20,

        DateTime = 30,

        Decimal = 40,

        Percentage = 50,

        Boolean = 60,

        Icon = 70,

        TextImage = 80,

        NA = 250
    }



}
