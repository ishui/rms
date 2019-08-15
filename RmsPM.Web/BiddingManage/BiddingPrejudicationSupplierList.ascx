<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingPrejudicationSupplierList.ascx.cs" Inherits="BiddingManage_BiddingPrejudicationSupplierList" %>
<asp:datagrid id="dgList" DataKeyField="BiddingSupplierCode" AutoGenerateColumns="False" runat="server"
	Width="100%" PageSize="15" >
	<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BiddingSupplierCode" ReadOnly="True" HeaderText="BiddingSupplierCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="序号">
			<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title" HorizontalAlign="left"></HeaderStyle>
			<ItemTemplate>
				<%# Container.ItemIndex + 1%>
			</ItemTemplate>
		</asp:TemplateColumn>
		
		<asp:TemplateColumn HeaderText="投标单位名称">
			<FooterStyle CssClass="list-title"></FooterStyle>
		    <HeaderStyle CssClass="list-title" HorizontalAlign="left"></HeaderStyle>
			<ItemTemplate>
				<a href="#" onclick="doBiddingSupplierModify('<%#DataBinder.Eval(Container, "DataItem.SupplierCode") %>','SingleView','<%#DataBinder.Eval(Container, "DataItem.SupplierCode") %>');return false;"><%#  RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode"))%></a>
				
			</ItemTemplate>
		</asp:TemplateColumn>
		
		
		
		<asp:TemplateColumn  HeaderText="预审">
			<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title" HorizontalAlign="left"></HeaderStyle>
			<ItemTemplate>
				<%# (string)DataBinder.Eval(Container, "DataItem.Flag")=="1"?"<font color=\"green\">通过</fong>":""%>
					
			</ItemTemplate>
		</asp:TemplateColumn>
		
		<asp:TemplateColumn   HeaderText="提名人">
			<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title" HorizontalAlign="left"></HeaderStyle>
			<ItemTemplate>
			<%# DataBinder.Eval(Container, "DataItem.NominateUser")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn  HeaderText="提交日期">
		<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title" HorizontalAlign="left"></HeaderStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.NominateDate")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		
		<asp:TemplateColumn  HeaderText="备注">
			<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title" HorizontalAlign="left"></HeaderStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.Remark")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		
	</Columns>
	
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
			CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<script language="javascript">
	function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){

	var strURL = '';
	
	strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
	var theWin = OpenFullWindow(strURL,'供应商信息');
	theWin.focus();
	}
</script>

