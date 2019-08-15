<%@ Page language="c#" Inherits="RmsPM.Web.PBS.SelectBuildingWindow" CodeFile="SelectBuildingWindow.aspx.cs" %>
<%@ Register TagPrefix="uc2" TagName="SelectBuildingCtrl" Src="../PBS/SelectBuildingCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SelectBuildingWindow</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
function winload()
{
	if (Form1.txtBuildingCode.value == "")
	{
		Form1.txtBuildingCode.value = BuildingCtrlGetFirstRowCode();
	}
	
	BuildingCtrlSelectRowByCode(Form1.txtBuildingCode.value, true);
}
		</SCRIPT>
	</HEAD>
	<body onload="winload();" leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0"
		scroll="no" style="BORDER-RIGHT:0px">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolordark="#ffffff"
				bordercolor="#666666" height="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr style="PADDING-RIGHT:2px;PADDING-LEFT:5px;BACKGROUND-COLOR:#d4d0c8">
								<td style="COLOR:#000000">Â¥¶°ÁÐ±í</td>
								<td align="right"><img src="../images/btn_close_small.gif" onmousedown="this.src='../images/btn_close_small_click.gif'"
										onmouseup="this.src='../images/btn_close_small.gif'; parent.CloseBuildingWindow();" onmouseout="this.src='../images/btn_close_small.gif'"
										onmouseover="this.src='../images/btn_close_small_over.gif'"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%; BACKGROUND-COLOR: white">
							<uc2:SelectBuildingCtrl id="tbSelectBuildingCtrl" runat="server"></uc2:SelectBuildingCtrl>
						</div>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtBuildingCode" name="txtBuildingCode" runat="server">
			<input type="hidden" id="txtMulti" name="txtMulti" runat="server">
		</form>
	</body>
</HTML>
