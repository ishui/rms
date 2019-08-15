<%@ Control Language="C#" AutoEventWireup="true" CodeFile="workflowmonitorselect.ascx.cs"
    Inherits="RmsPM.Web.WorkFlowControl.workflowselect" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<script language="javascript">
function ParentJs(CaseCode,ActCode,ApplicationPath,ApplicationCode,Status,Name)
{
    var flag = '<%=Request["Flag"]%>';
    <%=_ReturnFuncJs%>
}

</script>



<asp:DataGrid ID="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal"
    AutoGenerateColumns="False" PageSize="15" Width="100%" AllowPaging="True" runat="server"
    AllowSorting="True" OnSortCommand="dgList_SortCommand">
    <HeaderStyle CssClass="list-title"></HeaderStyle>
    <FooterStyle CssClass="list-title"></FooterStyle>
    <Columns>
        <asp:TemplateColumn HeaderText="��ˮ��">
            <ItemTemplate>
                <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowNumber(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="��Ŀ���">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseProjectName(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>'
                    ID="Label1">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="ProcedureName" HeaderText="��������">
            <HeaderStyle Wrap="False"></HeaderStyle>
        </asp:BoundColumn>
        <asp:TemplateColumn HeaderText="��ǰ����">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "ToTaskName") %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="����">
            <ItemTemplate>
                <a href="##" onclick="ParentJs('<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>','<%# DataBinder.Eval(Container.DataItem, "ActCode") %>','<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>','<%# DataBinder.Eval(Container.DataItem, "applicationCode") %>','<%# DataBinder.Eval(Container.DataItem, "Status") %>', '<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>'); return false;">
                    <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="������" SortExpression="FromUserCode">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "FromUserCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="��������" SortExpression="FromDate">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate((DateTime)DataBinder.Eval(Container.DataItem, "FromDate")) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="������">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# RmsPM.BLL.WorkFlowActUserSelect.GetWorkFlowActUser(DataBinder.Eval(Container.DataItem, "ActCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="ǩ������" SortExpression="SignDate">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "SignDate", "{0:yyyy-MM-dd HH:mm}")%>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False" HeaderText="�ƻ���������">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "PlanDate","{0:d}") %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="��������">
            <ItemTemplate>
                <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowHandmade(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="�汾">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetProcedureVersionByCode((string)DataBinder.Eval(Container, "DataItem.ProcedureCode")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="�汾˵��">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetProcedureVersionDescriptionByCode((string)DataBinder.Eval(Container, "DataItem.ProcedureCode")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle Visible="False" NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ"
        HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
</asp:DataGrid>
<cc1:GridPagination ID="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/"
    OnPageIndexChange="gpControl_PageIndexChange"></cc1:GridPagination>


