<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkFlowList.ascx.cs"
    Inherits="WorkFlowControl_WorkFlowList" %>
<%@ Register TagPrefix="cc2" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<asp:DataGrid ID="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
    AutoGenerateColumns="False" PageSize="18" Width="100%" AllowPaging="True">
    <HeaderStyle CssClass="list-title"></HeaderStyle>
    <FooterStyle CssClass="list-title"></FooterStyle>
    <Columns>
        <asp:TemplateColumn HeaderText="流水号">
            <ItemTemplate>
                <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowNumber(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="项目简称">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseProjectName(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>'
                    ID="Label1">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="流程名称">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Description") %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="主题">
            <ItemTemplate>
                <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode") %>"); return false;'>
                    <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="发件人">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "SourceUserCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="发件日期">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd HH:mm}")%>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="结束日期">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "FinishDate", "{0:yyyy-MM-dd HH:mm}")%>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="手送资料">
            <ItemTemplate>
                <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowHandmade(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页"
        HorizontalAlign="Right" CssClass="ListHeadTr" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
<cc2:GridPagination ID="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/"
    OnPageIndexChange="GridPagination1_PageIndexChange"></cc2:GridPagination>

<script>
	function gotoDirect ( path , CaseCode,ApplicationCode)
	{
		OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+"CaseCode=" + CaseCode +"&ApplicationCode="+ApplicationCode,'流程处理');
	}
</script>

