using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TestRepo
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string path = @"\Reports\";
                CustomReportStorageWebExtension reportsStorage = new CustomReportStorageWebExtension(path);
                ddlReportName.DataSource = reportsStorage.GetUrls();
                ddlReportName.DataBind();

                //Call function for populate cb
                FillStatus();
                FillOrgUnit();
            }

            else
            {
                XtraReport reportToOpen = null;
                switch (ddlReportName.SelectedValue)
                {
                    case "Zaposleni 1":
                        reportToOpen = new ZaposleniSaoOsig1();
                        break;
                    case "Zaposleni 2":
                        reportToOpen = new ZaposleniSaoOsig2();
                        break;
                    case "Zaposleni 3":
                        reportToOpen = new ZaposleniSaoOsig3();
                        break;
                }
                GetReports(reportToOpen);
                ASPxWebDocumentViewer1.OpenReport(reportToOpen);
            }
        }


        private void GetReports(XtraReport report)
        {
            try
            {
                string connString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=DesignSaoOsig1;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connString);
                string strproc = "TestReport";
                using (SqlDataAdapter sda = new SqlDataAdapter(strproc, connString))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand();
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = ddlStatus.SelectedValue == "1" ? true : false;
                    sda.SelectCommand.Parameters.Add("@OrgJed", SqlDbType.Int).Value = ddlOrgUnit.SelectedValue;
                    sda.Fill(ds);



                    string[] arrvalues = new string[ds.Tables[0].Rows.Count];

                    for (int loopcounter = 0; loopcounter < ds.Tables[0].Rows.Count; loopcounter++)
                    {
                        //assign dataset values to array
                        arrvalues[loopcounter] = ds.Tables[0].Rows[loopcounter]["PrezimeIme"].ToString();
                        arrvalues[loopcounter] = ds.Tables[0].Rows[loopcounter]["NetworkLogin"].ToString();
                        arrvalues[loopcounter] = ds.Tables[0].Rows[loopcounter]["Status"].ToString();
                        arrvalues[loopcounter] = ds.Tables[0].Rows[loopcounter]["OrgUnitID"].ToString();
                    }

                    report.DataSource = ds;
                    report.DataMember = ds.Tables[0].TableName.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selecting1(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        public void FillOrgUnit()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=DesignSaoOsig1;Integrated Security=True"))
            {
                string com = "SELECT DISTINCT OrgUnitID FROM tblZaposleni_AD ORDER BY OrgUnitID ASC";
                SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlOrgUnit.DataSource = dt;
                ddlOrgUnit.DataTextField = "OrgUnitID";
                ddlOrgUnit.DataValueField = "OrgUnitID";
                ddlOrgUnit.DataBind();
                ddlOrgUnit.Items.Insert(0, new ListItem("-- Izaberi Org Jedinicu --", "NULL"));
            }
        }

        public void FillStatus()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=DesignSaoOsig1;Integrated Security=True"))
            {
                string com = "SELECT DISTINCT Status FROM tblZaposleni_AD";
                SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlStatus.DataSource = dt;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "Status";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("-- Izaberi Status --", "NULL"));
            }
        }


        protected void DdlOrgUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlOrgUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


}