<%@ Control Language="c#" Inherits="RmsPM.Web.SendMsg.SendMsgInfo" CodeFile="SendMsgInfo.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" border="0" runat="server" id="Table1">
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td class="intopic">��������</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<table width="100%" runat="server" id="Table2" name="CopyTable" class="input">
	<tr>
		<td width="100%">
			<TEXTAREA class="input" name="txtContent" id="txtContent" style="WIDTH: 100%; HEIGHT: 56px"
				size="15"></TEXTAREA>
		</td>
	</tr>
</table>
<table cellSpacing="0" cellPadding="0" border="0" runat="server" id="CopyTitle">
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td class="intopic">������</td>
				</tr>
			</table>
		</td>
		<td><input type="button" class="button-small" id="btnCopy" onclick="addNewTaskActor();" value=" ѡ�� "
				runat="server" NAME="btnCopy"></td>
	</tr>
</table>

<table width="100%" runat="server" id="CopyTable" name="CopyTable" class="input">
	<tr>
		<td width="100%">
			<input type="text" class="input" name="txtUserName" id="txtUserName" style="WIDTH: 100%; HEIGHT: 56px"
				size="15" disabled>
		</td>
	</tr>
</table>
<table width="100%">
	<tr>
		<td align="center">
			<input class="submit" id="btnSend" type="button" value="ȷ ��" onclick="getSelectTaskActor(); " runat="server">
			<input class="submit" id="btnCancel" onclick="window.close();" type="button" value="�� ��" name="btnCancel" runat="server">
		</td>
	</tr>
</table>
<input type="hidden" class="input" name="txtUserCode" id="txtUserCode">
<script>

	function addNewTaskActor()
	{
		OpenLargeWindow('../SelectBox/SelectSUMain.aspx?ReturnFunc=SelectReturn','ѡ������');
	}
	function SelectReturn(userCodes,userNames,stationCodes,stationNames)
    {
    	
    	document.all("txtUserName").value = userNames;
		document.all("txtUserCode").value = userCodes;
	}

</script>
