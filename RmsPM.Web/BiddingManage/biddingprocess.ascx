<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingProcess" CodeFile="BiddingProcess.ascx.cs" %>
<FONT face="����">
	<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
		Width="100%" runat="server">
		<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="���" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# Container.ItemIndex + 1%>
				</ItemTemplate>
			</asp:TemplateColumn>						
			<asp:TemplateColumn HeaderText="|" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.|") %>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="���" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.���")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="�б�" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.�б�")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="Ͷ�굥λ����" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.Ͷ�굥λ����")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="Ԥ��" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.Ԥ��")%>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="��󱨼�" HeaderStyle-HorizontalAlign="Center">
			    <ItemStyle HorizontalAlign="right" />
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.��󱨼�")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="�������" HeaderStyle-HorizontalAlign="Center">
			    <ItemStyle HorizontalAlign="right" />
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.�������")%>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="��ע" HeaderStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.��ע")%>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
			CssClass="ListHeadTr"></PagerStyle>
	</asp:datagrid></FONT>
	<script language="javascript">
	function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){

	var strURL = '';
	
	strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
	var theWin = OpenFullWindow(strURL,'��Ӧ����Ϣ');
	theWin.focus();
	}
	</script>

