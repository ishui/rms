<%@ Control Language="C#" CodeFile="BiddingDtlModify.ascx.cs" Inherits="RmsPM.Web.BiddingManage.BiddingManage_BiddingDtlModify" %>
<%@ Register Src="../UserControls/inputcostbudgetdtl.ascx" TagName="inputcostbudgetdtl"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<div id="OperableDiv" runat="server">
<asp:datagrid id="dgListEdit" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" runat="server" AutoGenerateColumns="False" OnDeleteCommand="dgListEdit_DeleteCommand">
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="BiddingDtlCode" HeaderText="BiddingDtlCode" Visible="False"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="标段">
            <ItemTemplate>
                <asp:TextBox ID="txtTitle" CssClass="input" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>'></asp:TextBox> <font color="red">*</font>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="费用项">
            <ItemTemplate>
                &nbsp;<uc1:inputcostbudgetdtl ID="Inputcostbudgetdtl1" runat="server" ProjectCode='<%# ProjectCode%>' CostCode='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' CostBudgetSetCode='<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetCode") %>' PBSType='<%# DataBinder.Eval(Container, "DataItem.PBSType") %>' PBSCode='<%# DataBinder.Eval(Container, "DataItem.PBSCode") %>'/> <font color="red">*</font>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn  HeaderText="预算费用(RMB)" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
            <igtxt:webnumericedit id="TxtTemMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
				ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
				JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" ValueText='<%# DataBinder.Eval(Container, "DataItem.Money") %>'></igtxt:webnumericedit> <font color="red">*</font>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="金额"  Visible="false"  ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="lbTemMoney" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.Money")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn  HeaderText="清单费用(RMB)" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
            <igtxt:webnumericedit id="TxtOtherMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
				ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
				JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" ValueText='<%# DataBinder.Eval(Container, "DataItem.OtherMoney") %>'></igtxt:webnumericedit>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="备注">
            <ItemTemplate>
            <asp:TextBox ID="txtRemark" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.remark") %>' TextMode="MultiLine" Width="329px"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:ButtonColumn CommandName="Delete" Text="删除"></asp:ButtonColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
</div>
<div id="EyeableDiv" runat="server">
<asp:datagrid id="dgListView" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" runat="server" AutoGenerateColumns="False">
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="BiddingDtlCode" HeaderText="BiddingDtlCode" Visible="False"></asp:BoundColumn>
		<asp:BoundColumn DataField="Title" HeaderText="标段"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="费用项">
            <ItemTemplate>
                <%# RmsPM.BLL.CostBudgetRule.GetCostBudgetSetName((string)DataBinder.Eval(Container, "DataItem.CostBudgetSetCode")) %> &nbsp; 
                <%# RmsPM.BLL.CBSRule.GetCostFullName((string)DataBinder.Eval(Container, "DataItem.CostCode"))%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="预算费用(RMB)"  ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="lbTemMoney" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.Money")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        
          <asp:TemplateColumn  HeaderText="清单费用(RMB)" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
            <asp:Label ID="lblOtherMoney" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.OtherMoney")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
       
		<asp:BoundColumn DataField="remark" HeaderText="备注"></asp:BoundColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
</div>
