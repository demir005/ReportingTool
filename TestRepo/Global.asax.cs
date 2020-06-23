using DevExpress.XtraReports.Web;
using System;

namespace TestRepo
{
    public class Global_asax : System.Web.HttpApplication {
        void Application_Start(object sender, EventArgs e) {


            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Required;
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            ASPxWebDocumentViewer.StaticInitialize();
           //DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new CustomReportStorageWebExtension());
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new CustomReportStorageWebExtension(Server.MapPath("/Reports")));
            ASPxReportDesigner.StaticInitialize();
            ASPxWebDocumentViewer.StaticInitialize();


            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;         
        }

        void Application_End(object sender, EventArgs e) {
            // Code that runs on application shutdown
        }
    
        void Application_Error(object sender, EventArgs e) {
            // Code that runs when an unhandled error occurs
        }
    
        void Session_Start(object sender, EventArgs e) {
            // Code that runs when a new session is started
           
        }
    
        void Session_End(object sender, EventArgs e) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}