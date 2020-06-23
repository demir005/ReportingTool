using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;

namespace TestRepo.ReportsDesign
{
    public class ReportFactory
    {
        static ReportFactory()
        {
            Report.Add("Zaposleni 1", () => new ZaposleniSaoOsig1());
            Report.Add("Zaposleni 2", () => new ZaposleniSaoOsig2());
            Report.Add("Zaposleni 3", () => new ZaposleniSaoOsig3());
        }

        public static Dictionary<string, Func<XtraReport>> Report = new Dictionary<string, Func<XtraReport>>();
    }
}