<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputSearch" CodeFile="InputSearch.ascx.cs" %>
<table border="0" width="100%">
	<tr>
		<td class="noborder">
			<input id="txt_N" runat="server" style="DISPLAY:none"> <INPUT id="InputSearch_C" style="DISPLAY:none;WIDTH:8px;HEIGHT:22px" size="1" name="Text1"
				runat="server"> <INPUT id="InputSearch_I" style="DISPLAY:none;WIDTH:8px;HEIGHT:22px" size="1" name="Text2"
				runat="server">
		</td>
		<td></td>
	</tr>
	<tr>
		<td colspan="2">
			<div style="Z-INDEX: 1; POSITION: absolute">
				<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" onselectedindexchanged="DataGrid1_SelectedIndexChanged">
					<Columns>
						<asp:TemplateColumn HeaderText="Ñ¡Ôñ">
							<ItemTemplate>
								<FONT face="ËÎÌå">
									<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox></FONT>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</div>
		</td>
	</tr>
</table>
