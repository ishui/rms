<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingModelList" CodeFile="UCBuildingModelList.ascx.cs" %>
<asp:datagrid id="dgList" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="0"
	CssClass="list" Width="100%">
	<FooterStyle CssClass="list-title"></FooterStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BuildingModelCode" ReadOnly="True" HeaderText="BuildingModelCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="户型名称" FooterText="合计">
			<ItemTemplate>
				<a href="#" onclick='doBModel_<%=ClientID%>("<%# DataBinder.Eval(Container, "DataItem.ProjectCode") %>","<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>","<%# DataBinder.Eval(Container, "DataItem.BuildingModelCode") %>","SingleView");return false;'>
					<%# DataBinder.Eval(Container, "DataItem.ModelName") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="单元数">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.BModelNum") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalBModelNum"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="面积(平米)">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.BModelArea") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalBModelArea"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="位置">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# RmsPM.BLL.ProductRule.GetBuildingStationName( DataBinder.Eval(Container, "DataItem.BuildingStationCode") ) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="功能">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# RmsPM.BLL.ProductRule.GetBuildingFunctionName( DataBinder.Eval(Container, "DataItem.BuildingFunctionCode") ) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<Script language="JavaScript" type="text/javascript">
<!--

function doBModel_<%=ClientID%>(projectCode,buildingCode,code,dotype){
	var NowTime = new Date();
	var strBuildingCode = buildingCode;
	var strProjectCode= projectCode;
	
	var strURL = './BuildingModelModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&ProjectCode=' + strProjectCode;
	
	strURL += '&CellCode=' + code;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	var theWin = OpenLargeWindow(strURL,"BuildingModelModify");
	theWin.focus();
}

//-->
</Script>