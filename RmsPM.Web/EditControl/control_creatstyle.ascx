<%@ Control Language="c#" Inherits="RmsPM.Web.Control_CreatStyle" CodeFile="Control_CreatStyle.ascx.cs" %>
<LINK href="../images/index.css" type="text/css" rel="stylesheet">
<div align="center">
	<table border="0" width="500" cellspacing="0" cellpadding="0" id="table1">
		<tr>
			<td width="1009" colspan="4" align="center">
				<p>
				</p>
				<p>&nbsp;</p>
				<P><FONT size="5">������ʾ����</FONT></P>
				<p>
				</p>
			</td>
		</tr>
		<tr>
			<td width="290" style="HEIGHT: 20px" align="left">ѡ���λ:&nbsp;
			</td>
			<td width="259" style="HEIGHT: 20px" align="right">
				<asp:DropDownList runat="server" Height="19px" Width="160px" AutoPostBack="True" ID="DDL_StationList" onselectedindexchanged="DDL_StationList_SelectedIndexChanged"></asp:DropDownList>
			</td>
			<td width="290" style="HEIGHT: 20px" align="left">
				<P>&nbsp; ѡ����ʽ:
				</P>
			</td>
			<td width="245" style="HEIGHT: 20px" align="right">
				<asp:DropDownList runat="server" Height="19px" Width="160px" AutoPostBack="True" ID="DDL_StyleList" onselectedindexchanged="DDL_StyleList_SelectedIndexChanged"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td width="519" height="30" colspan="2">
				�����ʾ:</td>
			<td width="490" height="30" colspan="2">
				&nbsp;
				�ұ���ʾ:</td>
		</tr>
		<tr>
			<td width="519" align="right" colspan="2">
				<FONT face="����">
					<asp:ListBox runat="server" Height="88px" Width="160px" ID="LB_Left"></asp:ListBox>
				</FONT>
			</td>
			<td width="490" align="right" colspan="2">
				<FONT face="����">
					<asp:ListBox runat="server" Height="88px" Width="160px" ID="LB_Right"></asp:ListBox>
				</FONT>
			</td>
		</tr>
		<tr>
			<td width="519" colspan="2">
			</td>
			<td width="490" align="right" height="30" valign="bottom" colspan="2">
				<FONT face="����">
					<asp:Button id="Bt_Sumit" runat="server" Enabled="False" Text="�������" onclick="Bt_Sumit_Click"></asp:Button></FONT>
			</td>
		</tr>
	</table>
</div>
