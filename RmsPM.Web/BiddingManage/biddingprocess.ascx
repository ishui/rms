<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingProcess" CodeFile="BiddingProcess.ascx.cs" %>
<FONT face="宋体">
	<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
		Width="100%" runat="server">
		<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="序号" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# Container.ItemIndex + 1%>
				</ItemTemplate>
			</asp:TemplateColumn>						
			<asp:TemplateColumn HeaderText="|" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.|") %>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="标段" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.标段")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="中标" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.中标")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="投标单位名称" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.投标单位名称")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="预审" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.预审")%>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="最后报价" HeaderStyle-HorizontalAlign="Center">
			    <ItemStyle HorizontalAlign="right" />
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.最后报价")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="最后名次" HeaderStyle-HorizontalAlign="Center">
			    <ItemStyle HorizontalAlign="right" />
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.最后名次")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="备注" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.备注")%>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
			CssClass="ListHeadTr"></PagerStyle>
	</asp:datagrid></FONT>
	<script language="javascript">
	function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){

	var strURL = '';
	
	strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
	var theWin = OpenFullWindow(strURL,'供应商信息');
	theWin.focus();
	}
	</script>

