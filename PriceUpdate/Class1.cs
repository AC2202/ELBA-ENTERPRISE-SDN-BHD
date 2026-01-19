using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCount;
using AutoCount.Scripting;
using AutoCount.PlugIn;

namespace PriceUpdate
{
    public class PlugInit : AutoCount.PlugIn.BasePlugIn
    {
        public PlugInit() : base(new Guid("{46771C32-841E-4901-9AB0-C668D1062BEC}"), "Selling Price in Stock Transfer", "2.2.0", "engkeat.cheow@softwaredepot.com.my")
        {
            SetMinimumAccountingVersionRequired("2.2.22.30");
            SetIsFreeLicense(false);
            SetDevExpressComponentVersionRequired("22.2.7");
            SetCopyright("Software Depot Sdn Bhd");
        }

        public override bool BeforeLoad(BeforeLoadArgs e)
        {
            AutoCount.Scripting.ScriptManager scriptManager = AutoCount.Scripting.ScriptManager.GetOrCreate(e.DBSetting);
            scriptManager.RegisterByType("XFER", typeof(StockTransferScript));
            return base.BeforeLoad(e);
        }

        public override void GetLicenseStatus(LicenseStatusArgs e)
        {
            e.LicenseStatus = LicenseStatus.Custom;
            e.CustomLicenseStatus = $"License is registered to {e.CompanyProfile.CompanyName}";
        }



    }
}
