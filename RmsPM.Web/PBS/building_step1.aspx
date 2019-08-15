<%@ Page language="c#" Inherits="RmsPM.Web.PBS.Building_Step1" CodeFile="Building_Step1.aspx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>User</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
	</HEAD>
	<BODY bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 房控图
								</td>
								<td style="CURSOR: hand" onclick="GoBack();" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" align="center">
						<table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
							<tr>
								<td height="25" valign="bottom" class="note" align="center">新建楼栋结构第一步</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" align="center" valign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="300" border="0">
							<tr>
								<td class="form-item">楼栋名称：</td>
								<td><asp:Label Runat="server" ID="lblBuildingName"></asp:Label></td>
							</tr>
							<tr>
								<td class="form-item">产品类型：</td>
								<td><select class="input" id="sltPBSTypeCode" name="sltPBSTypeCode" runat="server"></select></td>
							</tr>
							<tr>
								<td class="form-item">总层数：</td>
								<td><input class="input" id="txtFloorCount" style="TEXT-ALIGN: right" type="text" size="8"
										name="txtFloorCount" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr align="center">
					<td class="table" vAlign="top">
						<table>
							<tr>
								<td><input class="submit" id="btnSubmit" type="button" value="下一步" name="btnSubmit" runat="server" onclick="document.all.divHintLoad.style.display='';" onserverclick="btnSubmit_ServerClick">
									<input class="submit" id="btnSave" style="DISPLAY: none" type="button" value="保存" name="btnSave"
										runat="server" onclick="document.all.divHintSave.style.display='';"> <input class="submit" onclick="GotoBuildingPart(Form1.txtBuildingCode.value);" type="button"
										value="取 消">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">&nbsp;</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtParentCode" type="hidden" name="txtParentCode" runat="server">
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server"><input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtIsArea" type="hidden" name="txtIsArea" runat="server">
		</form>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		<!--
			function GoBack(){
				window.location.href = "BuildingPart.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&ProjectCode=" + Form1.txtProjectCode.value + "&action=prev";
			}
		//-->
		</script>
	</BODY>
</HTML>
