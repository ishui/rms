<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesignDocumentList.ascx.cs" Inherits="RmsPM.Web.DesignDocumentManage.DesignDocumentManage_DesignDocumentList" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
   <asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="DesignDocumentCode" HeaderText="DesignDocumentCode" Visible="False"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="名称">
            <ItemTemplate>
                <a href="#" onclick='javascript:OpenModify("<%# DataBinder.Eval(Container, "DataItem.DesignDocumentCode") %>");'><%# DataBinder.Eval(Container, "DataItem.Title") %></a>
            </ItemTemplate>
        </asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="项目">
            <ItemTemplate>
                <%# RmsPM.BLL.ProjectRule.GetProjectName((string)DataBinder.Eval(Container, "DataItem.ProjectCode")) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="部门">
            <ItemTemplate>
                <%# RmsPM.BLL.SystemRule.GetUnitName((string)DataBinder.Eval(Container, "DataItem.UnitCode")) %>
            </ItemTemplate>
        </asp:TemplateColumn>
		<asp:BoundColumn DataField="Context" HeaderText="摘要"></asp:BoundColumn>
		<asp:BoundColumn DataField="CreateDate" HeaderText="创建日期"></asp:BoundColumn>
		<asp:BoundColumn DataField="CreateUser" HeaderText="CreateUser" Visible="False"></asp:BoundColumn>
		<asp:BoundColumn DataField="State" HeaderText="State" Visible="False"></asp:BoundColumn>
		<asp:BoundColumn DataField="Flag" HeaderText="Flag" Visible="False"></asp:BoundColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" PageIndexChange="gpControl_PageIndexChange"></cc1:GridPagination>
<script>
function OpenModify(code)
{
	OpenFullWindow('DesignDocumentModify.aspx?State=view&ProjectCode=<%= Request["ProjectCode"]+"" %>&Type=<%=this.Type %>&ApplicationCode='+code+'&Title=<%= this.PageTitle %>>','设计新增');
}
</script>