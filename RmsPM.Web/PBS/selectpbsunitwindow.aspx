<%@ Reference Control="~/pbs/selectpbsunitctrl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="SelectPBSUnitCtrl" Src="../PBS/SelectPBSUnitCtrl.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.SelectPBSUnitWindow" CodeFile="SelectPBSUnitWindow.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SelectPBSUnitWindow</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
function winload()
{
	if (Form1.txtPBSUnitCode.value == "")
	{
		Form1.txtPBSUnitCode.value = PBSUnitCtrlGetFirstRowCode();
	}
	
	PBSUnitCtrlSelectRowByCode(Form1.txtPBSUnitCode.value, true);
}
		</SCRIPT>
		
	</HEAD>
	<body onload="winload();" leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0" scroll="no" style="border-right:0">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolordark="#ffffff"
				bordercolor="#666666" height="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr style="PADDING-RIGHT:2px;PADDING-LEFT:5px;BACKGROUND-COLOR:#d4d0c8">
								<td style="COLOR:#000000">单位工程列表</td>
								<td align="right"><img src="../images/btn_close_small.gif" onmousedown="this.src='../images/btn_close_small_click.gif'"
										onmouseup="this.src='../images/btn_close_small.gif'; parent.ClosePBSUnitWindow();" onmouseout="this.src='../images/btn_close_small.gif'"
										onmouseover="this.src='../images/btn_close_small_over.gif'"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%; BACKGROUND-COLOR: white">
							<uc2:SelectPBSUnitCtrl id="tbSelectPBSUnitCtrl" runat="server"></uc2:SelectPBSUnitCtrl>
						</div>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtPBSUnitCode" name="txtPBSUnitCode" runat="server">
		</form>
	</body>
</HTML>
