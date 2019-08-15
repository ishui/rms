<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_BiddingReturnSupplierList.ascx.cs" Inherits="BiddingManage_uc_BiddingReturnSupplierList" %>
<asp:datagrid id="dgList" DataKeyField="BiddingSupplierCode" AutoGenerateColumns="False" runat="server"
	Width="100%" PageSize="15" OnItemCommand="dgList_ItemCommand" >
	<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BiddingSupplierCode" ReadOnly="True" HeaderText="BiddingSupplierCode"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="SupplierCode" ReadOnly="True" HeaderText="SupplierCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="序号">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex+1%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		
		
		
		<asp:TemplateColumn HeaderText="公司名称">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<a href="#" onclick='doBiddingSupplierModify("<%# DataBinder.Eval(Container, "DataItem.BiddingSupplierCode") %>","SingleView","<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>");return false;'>
					<%# RmsPM.BLL.SupplierRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode"))%>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		
		<asp:BoundColumn HeaderStyle-CssClass="blackbordertd" ItemStyle-CssClass="blackbordertd" DataField="NominateUser" ReadOnly="True"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="第一次议价">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<INPUT class="infra-input-nember" style="border: 1px solid #B0C5E6;height:18px;font: 12px;font-family: 'Tahoma','宋体';" id="TxtReturn1" type="text" value='<%# DataBinder.Eval(Container, "DataItem.BiddingReturnMondey1") %>' name="TxtReturn1" runat="server">
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="第二次议价">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<INPUT class="infra-input-nember" style="border: 1px solid #B0C5E6;height:18px;font: 12px;font-family: 'Tahoma','宋体';" id="TxtReturn2" type="text" value='<%# DataBinder.Eval(Container, "DataItem.BiddingReturnMondey2") %>' name="TxtReturn2" runat="server">
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="第三次议价">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<INPUT class="infra-input-nember" style="border: 1px solid #B0C5E6;height:18px;font: 12px;font-family: 'Tahoma','宋体';" id="TxtReturn3" type="text" value='<%# DataBinder.Eval(Container, "DataItem.BiddingReturnMondey3") %>' name="TxtReturn3" runat="server">
			</ItemTemplate>
		</asp:TemplateColumn>
		
		<asp:ButtonColumn Text="删除" CommandName="Delete">
			<HeaderStyle Wrap="False" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle Wrap="False" CssClass="blackbordertd"></ItemStyle>
			<FooterStyle Wrap="False"></FooterStyle>
		</asp:ButtonColumn>
	</Columns>
</asp:datagrid>
<script>
	function IsWantToReturn()
	{
		//if()
		if(window.confirm("确定选好了招标单位吗?"))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){

	var strURL = '';
	
	strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
	var theWin = OpenFullWindow(strURL,'供应商信息');
	theWin.focus();

    }

 
</script>
