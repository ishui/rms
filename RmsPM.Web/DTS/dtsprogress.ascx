<%@ Control Language="c#" Inherits="RmsPM.Web.DTS.DtsProgress" CodeFile="DtsProgress.ascx.cs" %>
<table height="50" width="200" bgcolor="#e1e5f4" border="1" cellpadding="0" cellspacing="0"
	align="center" bordercolorlight="#b5bce8" bordercolordark="#666666" style="BORDER-COLLAPSE: collapse">
	<tr>
		<td nowrap align="center" id="tdHint" runat="server">正在处理，请稍候。。。</td>
	</tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
	<tr align="center">
		<td>
			<asp:Label id="lbIndex" runat="server"></asp:Label>
			of
			<asp:Label id="lbCount" runat="server"></asp:Label>
		</td>
	</tr>
	<!--
				<tr>
					<td><input type=button value="停止" name="btnStop" id="btnStop" runat="server" onclick="Stop();"></td>
				</tr>-->
</table>
<script language="javascript">

function Stop()
{
	if (window.confirm("确实要停止吗？"))
	{
		Form1.txtIsContinue.value = "0";
	}
}

</script>
