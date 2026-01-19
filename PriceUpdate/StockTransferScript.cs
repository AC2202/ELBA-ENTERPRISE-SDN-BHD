namespace PriceUpdate
{
    internal class StockTransferScript
    {
        public void OnMasterColumnChanged(AutoCount.Stock.StockTransfer.StockTransferMasterColumnChangedEventArgs e)
        {
            if (e.ChangedColumnName == "UDF_CUST")
            {
                for (int i = 0; i < e.MasterRecord.DetailCount; i++)
                {
                    string transferCust = e.MasterRecord.UDF["CUST"].ToString();

                    object debtorType = e.DBSetting.ExecuteScalar("SELECT DebtorType FROM Debtor Where AccNo=?",
                        transferCust);
                    string sDebtorType = debtorType.ToString();
                    string item = e.MasterRecord.GetDetailRecord(i).ItemCode.ToString();


                    if (sDebtorType == "CSGN-EM")
                    {
                        object price = e.DBSetting.ExecuteScalar("SELECT UDF_CSGNEM FROM Item Where ItemCode=?",
                            item);
                        e.MasterRecord.GetDetailRecord(i).UnitCost = AutoCount.Converter.ToDecimal(price);

                    }

                    else if (sDebtorType == "CSGN-PEN")
                    {
                        object price = e.DBSetting.ExecuteScalar("SELECT UDF_CSGNPEN FROM Item Where ItemCode=?",
                            item);
                        e.MasterRecord.GetDetailRecord(i).UnitCost = AutoCount.Converter.ToDecimal(price);
                    }

                    else if (sDebtorType == "CSGN-WE")
                    {
                        object price = e.DBSetting.ExecuteScalar("SELECT UDF_CSGNWE FROM Item Where ItemCode=?",
                            item);
                        e.MasterRecord.GetDetailRecord(i).UnitCost = AutoCount.Converter.ToDecimal(price);
                    }

                    else if (sDebtorType == "OURGT-FP")
                    {
                        object price = e.DBSetting.ExecuteScalar("SELECT UDF_OURGTFP FROM Item Where ItemCode=?",
                            item);
                        e.MasterRecord.GetDetailRecord(i).UnitCost = AutoCount.Converter.ToDecimal(price);
                    }

                    else if (sDebtorType == "OURGT-PEN")
                    {
                        object price = e.DBSetting.ExecuteScalar("SELECT UDF_OURGTPEN FROM Item Where ItemCode=?",
                            item);
                        e.MasterRecord.GetDetailRecord(i).UnitCost = AutoCount.Converter.ToDecimal(price);
                    }

                    else
                    {
                        return;
                    }
                }
            }
        }


        public void OnDetailColumnChanged(AutoCount.Stock.StockTransfer.StockTransferDetailColumnChangedEventArgs e)
        {

            if (e.ChangedColumnName == "ItemCode" || e.ChangedColumnName == "Qty")
            {
                if (e.MasterRecord.UDF["CUST"].ToString() == "")
                {
                    AutoCount.AppMessage.ShowMessage("Please select Customer at UDF Tab first");
                }                
                string transferCust = e.MasterRecord.UDF["CUST"].ToString();
                object debtorType = e.DBSetting.ExecuteScalar("SELECT DebtorType FROM Debtor Where AccNo=?",
                    transferCust);
                string sDebtorType = debtorType.ToString();
                string item = e.CurrentDetailRecord.ItemCode.ToString();

                if (sDebtorType == "CSGN-EM")
                {
                    //AutoCount.Application.Properties.DBSetting.ExecuteScalar("");
                    object price = e.DBSetting.ExecuteScalar("SELECT UDF_CSGNEM FROM Item Where ItemCode=?",
                        item);
                    e.CurrentDetailRecord.UnitCost = AutoCount.Converter.ToDecimal(price);
                }

                else if (sDebtorType == "CSGN-PEN")
                {
                    object price = e.DBSetting.ExecuteScalar("SELECT UDF_CSGNPEN FROM Item Where ItemCode=?",
                        item);
                    e.CurrentDetailRecord.UnitCost = AutoCount.Converter.ToDecimal(price);
                }

                else if (sDebtorType == "CSGN-WE")
                {
                    object price = e.DBSetting.ExecuteScalar("SELECT UDF_CSGNWE FROM Item Where ItemCode=?",
                        item);
                    e.CurrentDetailRecord.UnitCost = AutoCount.Converter.ToDecimal(price);
                }

                else if (sDebtorType == "OURGT-FP")
                {
                    object price = e.DBSetting.ExecuteScalar("SELECT UDF_OURGTFP FROM Item Where ItemCode=?",
                        item);
                    e.CurrentDetailRecord.UnitCost = AutoCount.Converter.ToDecimal(price);
                }

                else if (sDebtorType == "OURGT-PEN")
                {
                    object price = e.DBSetting.ExecuteScalar("SELECT UDF_OURGTPEN FROM Item Where ItemCode=?",
                        item);
                    e.CurrentDetailRecord.UnitCost = AutoCount.Converter.ToDecimal(price);
                }

                else
                {
                    return;
                }

            }

        }


        public void BeforeSave(AutoCount.Stock.StockTransfer.StockTransferBeforeSaveEventArgs e)
        {
            if (e.MasterRecord.UDF["CUST"] == null)
            {
                e.AbortSave();
            }

        }







    }
    }