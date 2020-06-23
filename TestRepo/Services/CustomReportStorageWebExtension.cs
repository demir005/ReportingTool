using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using TestRepo.ReportsDesign;

namespace TestRepo
{
    public class CustomReportStorageWebExtension : ReportStorageWebExtension
    {
        readonly string reportDirectory;
        const string FileExtension = ".repx";

        public CustomReportStorageWebExtension(string reportDirectory)
        {
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            this.reportDirectory = reportDirectory;
        }

        

        private bool IsWithinReportsFolder(string url, string folder)
        {
            var rootDirectory = new DirectoryInfo(folder);
            var fileInfo = new FileInfo(Path.Combine(folder, url));
            return fileInfo.Directory.FullName.ToLower().StartsWith(rootDirectory.FullName.ToLower());
        }

        public override bool CanSetData(string url)
        {
            return true;
        }
        public override bool IsValidUrl(string url)
        {
            return Path.GetFileName(url) == url;
        }

        public override byte[] GetData(string url)
        {
            try
            {
                if (Directory.EnumerateFiles(reportDirectory).Select(Path.GetFileNameWithoutExtension).Contains(url))
                {
                    return File.ReadAllBytes(Path.Combine(reportDirectory, url + FileExtension));
                }
                if (ReportFactory.Report.ContainsKey(url))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ReportFactory.Report[url]().SaveLayoutToXml(ms);
                        return ms.ToArray();
                    }
                }
                throw new FaultException(new FaultReason(string.Format("Could not find report '{0}'.", url)), new FaultCode("Server"), "GetData");
            }
            catch (Exception)
            {
                throw new FaultException(new FaultReason(string.Format("Could not find report '{0}'.", url)), new FaultCode("Server"), "GetData");
            }
        }


        public override Dictionary<string, string> GetUrls()
        {

            return Directory.GetFiles(reportDirectory, "*" + FileExtension)
                                     .Select(Path.GetFileNameWithoutExtension)
                                     .Concat(ReportFactory.Report.Select(x => x.Key))
                                     .ToDictionary(x => x);
        }

        public override void SetData(XtraReport report, string url)
        {
            if (!IsWithinReportsFolder(url, reportDirectory))
                throw new FaultException(new FaultReason("Invalid report name."), new FaultCode("Server"), "GetData");
            report.SaveLayoutToXml(Path.Combine(reportDirectory, url + FileExtension));
        }

        public override string SetNewData(XtraReport report, string defaultUrl)
        {
            SetData(report, defaultUrl);
            return defaultUrl;
        }
    }
}
