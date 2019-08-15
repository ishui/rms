<%@ Control Language="c#" Inherits="RmsPM.Web.EditControl.Control_UserDestop" CodeFile="Control_UserDestop.ascx.cs" %>
<LINK href="../images/index.css" type="text/css" rel="stylesheet">
<style>
.toptxt { COLOR: #ff0000 }
</style>
<asp:DataGrid id="DataGrid1" DataKeyField="ControlID" runat="server" AutoGenerateColumns="False"
	Width="136px" GridLines="None">
	<Columns>
		<asp:TemplateColumn>
			<HeaderTemplate>
				<FONT face="ËÎÌå"></FONT>
			</HeaderTemplate>
			<ItemTemplate>
				<asp:CheckBox id=CB_Control runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ControlTitle")%>' Checked='<%# DataBinder.Eval(Container, "DataItem.IsShow")=="true"%>'>
				</asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
<table border="0" width="92" cellspacing="0" cellpadding="0" height="24" id="table1">
	<tr>
		<td align="center" background="../Images/bg_bn.gif" height="24">
			<asp:Button id="Bt_Update" Width="84px" runat="server" Text="±£ ´æ" BackColor="Transparent" BorderColor="Transparent"
				BorderStyle="None" Height="24px" onclick="Bt_Update_Click"></asp:Button></td>
	</tr>
</table>