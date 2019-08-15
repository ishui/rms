using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsDM.BFL;
using System.Data.SqlClient;
using RmsPM.Web;
using Telerik.WebControls;


public partial class WorkFlowContral_WorkFlowStartup : PageBase
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
            
            RadGrid RadGrid1 = new RadGrid();
            this.SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString .ToString();
            this.SqlDataSource2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString .ToString();
            
            //Add the RadGrid instance to the controls
            this.PlaceHolder1.Controls.Add(RadGrid1);
            if (!IsPostBack)
            {

                RadGrid1.Skin = "Default";

                RadGrid1.DataSourceID = "SqlDataSource1";
                //RadGrid1.DataSource = DataSourse().Tables[0];
                RadGrid1.MasterTableView.DataKeyNames = new string[] { "groupcode" };

                RadGrid1.Width = Unit.Percentage(100);
                RadGrid1.PageSize = 5;
                RadGrid1.AllowPaging = true;
                RadGrid1.AutoGenerateColumns = false;

                //Set options to enable Group-by 
                RadGrid1.GroupingEnabled = true;
                //RadGrid1.ShowGroupPanel = true;
               // RadGrid1.ClientSettings.AllowDragToGroup = false;
                RadGrid1.ClientSettings.AllowColumnsReorder = true;

                //Add Customers table
                RadGrid1.MasterTableView.PageSize = 15;

                GridBoundColumn boundColumn;
                //boundColumn = new GridBoundColumn();
                //RadGrid1.MasterTableView.Columns.Add(boundColumn);
                //boundColumn.DataField = "GroupCode";
                //boundColumn.HeaderText = "类别编号";

                boundColumn = new GridBoundColumn();
                RadGrid1.MasterTableView.Columns.Add(boundColumn);
                boundColumn.DataField = "GroupName";
                boundColumn.HeaderText = "类别名称";



                //Add Orders table
                GridTableView tableViewOrders = new GridTableView(RadGrid1);
                RadGrid1.MasterTableView.DetailTables.Add(tableViewOrders);

                tableViewOrders.DataSourceID = "SqlDataSource2";
                //tableViewOrders.DataSource = DataSourse().Tables[1];
                tableViewOrders.DataKeyNames = new string[] { "ProcedureCode" };

                GridRelationFields relationFields = new GridRelationFields();
                tableViewOrders.ParentTableRelation.Add(relationFields);

                relationFields.MasterKeyField = "groupcode";
                relationFields.DetailKeyField = "SysType";

                //Add a group-by expression as string into Customer's collection
                //RadGrid1.MasterTableView.GroupByExpressions.Add(new GridGroupByExpression("GroupName Group By GroupName"));

                //Add a group-by expression by defining fields into Orders' collection
                GridGroupByExpression expression = new GridGroupByExpression();

                GridGroupByField gridGroupByField = new GridGroupByField();

                //Add select fileds (before the "Group By" clause)
                //gridGroupByField = new GridGroupByField();
                //gridGroupByField.FieldName = "ProcedureCode";
                //gridGroupByField.FormatString = "ProcedureCode: {0}";
                //expression.SelectFields.Add(gridGroupByField);

                //gridGroupByField = new GridGroupByField();
                //gridGroupByField.FieldName = "ProcedureName";
                //gridGroupByField.FormatString = "Total shipping cost is <strong>{0}</strong>";
                ////gridGroupByField.Aggregate = GridAggregateFunction.Sum;
                //expression.SelectFields.Add(gridGroupByField);

                ////Add a filed for group-by (after the "Group By" clause) 
                //gridGroupByField = new GridGroupByField();
                //gridGroupByField.FieldName = "ProcedureCode";
                //expression.GroupByFields.Add(gridGroupByField);

                ////This expression as string would look like:
                //// "RequiredDate, Freight Group By RequiredDate"
                ////but the display format of filed values wolld be different
                //tableViewOrders.GroupByExpressions.Add(expression);

                boundColumn = new GridBoundColumn();
                tableViewOrders.Columns.Add(boundColumn);
                boundColumn.DataField = "ProcedureCode";
                boundColumn.HeaderText = "流程编号";

                boundColumn = new GridBoundColumn();

                GridHyperLinkColumn ghlk = new GridHyperLinkColumn();
                tableViewOrders.Columns.Add(ghlk);
               
                string[] urlField = new string[2];
                urlField[0] = "Applicationpath";
                urlField[1] = "ProcedureCode";
                ghlk.DataNavigateUrlFields = urlField;
                ghlk.HeaderText = "流程名称";
               // ghlk
                ghlk.DataNavigateUrlFormatString = "javascript:modifyProcedure('{0}','{1}','','');";
                ghlk.DataTextField = "ProcedureName";

            }
      
    }
}
