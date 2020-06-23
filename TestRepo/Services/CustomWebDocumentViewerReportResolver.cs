using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using System;

namespace TestRepo
{
    public class CustomWebDocumentViewerReportResolver : IWebDocumentViewerReportResolver
    {

        public ReportStorageWebExtension ReportStorage { get; private set; }

        public CustomWebDocumentViewerReportResolver()
        {
            ReportStorage = new CustomReportStorageWebExtension();
        }

        public XtraReport Resolve(string reportName)
        {
            switch (reportName)
            {
                case "Zaposleni 1":
                    return new ZaposleniSaoOsig1();
                case "Zaposleni 2":
                    return new ZaposleniSaoOsig2();
                case "Zaposleni 3":
                    return new ZaposleniSaoOsig3();
                default:
                    // Try to create a report using the fully qualified type name.
                    Type t = Type.GetType(reportName);
                    return typeof(XtraReport).IsAssignableFrom(t) ?
                        (XtraReport)Activator.CreateInstance(t) :
                        null;
            }
        }
    }
}