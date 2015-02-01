using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EApp.Common.Util;
using EApp.Domain.Core;
using Xpress.Core.Entities;


namespace Xpress.Core.Common
{
    public sealed class DataGridViewCellHandler
    {
        private DataGridViewCellHandler() { }

        public static bool UpdateNewValueIntoCellDatail<TEntityItem>(object newValue, GridViewCellDetail<TEntityItem> cellDetail) 
            where TEntityItem : IEntity<int>
        {
            bool updateSuccess = true;

            if (cellDetail.ColumnType == GridViewColumnType.EditBox ||
                cellDetail.ColumnType == GridViewColumnType.ComboBox)
            {
                if (cellDetail.ValueType == GridViewCellValueType.String)
                {
                    if (newValue == null)
                    {
                        cellDetail.Value = string.Empty;
                    }
                    else
                    {
                        cellDetail.Value = newValue.ToString();
                    }
                }
                else if (cellDetail.ValueType == GridViewCellValueType.Decimal)
                {
                    string tempDecimalValue = null;

                    if (newValue != null)
                    {
                        tempDecimalValue = newValue.ToString();
                    }

                    try
                    {
                        decimal? newDecimalValue = LocalizationUtil.FormatStringTo2Decimal(tempDecimalValue);

                        cellDetail.Value = newDecimalValue;
                    }
                    catch
                    {
                        updateSuccess = false;
                    }

                }
                else if (cellDetail.ValueType == GridViewCellValueType.Integer)
                {
                    string tempIntValue = null;

                    if (newValue != null)
                    {
                        tempIntValue = newValue.ToString();
                    }

                    try
                    {
                        int? intValue = Convertor.ConvertToInteger(tempIntValue);

                        cellDetail.Value = intValue;
                    }
                    catch
                    {
                        updateSuccess = false;
                    }
                }
                else if (cellDetail.ValueType == GridViewCellValueType.Percentage)
                {
                    string tempPercentageValue = null;

                    if (newValue != null)
                    {
                        tempPercentageValue = Convert.ToString(newValue, 
                            CultureInfo.InvariantCulture).Replace("%", string.Empty);
                    }

                    try
                    {
                        decimal? percentValue = LocalizationUtil.FormatStringToRate(tempPercentageValue);

                        cellDetail.Value = percentValue;
                    }
                    catch
                    {
                        updateSuccess = false;
                    }
                }
            }
            else if (cellDetail.ColumnType == GridViewColumnType.DateChooser)
            {
                string tempDateTimeValue = null;

                if (newValue != null)
                {
                    tempDateTimeValue = newValue.ToString();
                }

                cellDetail.Value = DateTimeUtil.ToDateTime(tempDateTimeValue);
            }

            return updateSuccess;
        }

        public static bool UpdateNewValueIntoCellDatail(object newValue, GridViewCostCellDetail cellDetail) 
        {
            return UpdateNewValueIntoCellDatail<CostLineItemBase>(newValue, cellDetail);     
        }

    }
}
