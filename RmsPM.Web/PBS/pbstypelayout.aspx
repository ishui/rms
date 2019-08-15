<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSTypeLayout" CodeFile="PBSTypeLayout.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>规划要点</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 规划要点</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absmiddle"> <input class="button" id="btnModify" onclick="Modify()" type="button" value="修改规划要点" name="btnModify"
							runat="server"> <span style="display:none"><input class="button" id="btnAddPBSType" onclick="AddPBSType();" type="button" value="新增产品组合"
								name="btnAddPBSType" runat="server"> <input class="button" id="btnPBSTypeImport" onclick="if (!PBSTypeImport()) return false;"
								type="button" value="产品组合从模板导入" name="btnPBSTypeImport" runat="server" onserverclick="btnPBSTypeImport_ServerClick"> <input class="button" id="btnPBSTypeExport" onclick="if (!PBSTypeExport()) return false;"
								type="button" value="产品组合导出成模板" name="btnPBSTypeExport" runat="server" onserverclick="btnPBSTypeExport_ServerClick"> <input class="button" id="btnPBSTypeImportAllProject" onclick="if (!PBSTypeImport()) return false;"
								type="button" value="产品组合导入所有" name="btnPBSTypeImportAllProject" runat="server" onserverclick="btnPBSTypeImportAllProject_ServerClick">
						</span>
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="200" height="25" valign="bottom" class="note">项目信息</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="form-item" width="15%">总占地面积：</td>
								<td class="tdBlank" width="15%"><asp:label id="lblTotalFloorSpace" Runat="server"></asp:label></td>
								<td class="form-item" width="15%">建筑占地面积：</td>
								<td class="tdBlank" width="15%"><asp:label id="lblBuildSpace" Runat="server"></asp:label></td>
								<td class="form-item" width="15%">绿化率：</td>
								<td class="tdBlank" width="15%"><asp:label id="lblAfforestingRate" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="form-item">总建筑面积：</td>
								<td class="tdBlank"><asp:label id="lblTotalBuildingSpace" Runat="server"></asp:label></td>
								<td class="form-item">容积率：</td>
								<td class="tdBlank"><asp:label id="lblPlannedVolumeRate" Runat="server"></asp:label></td>
								<td class="form-item">建筑密度：</td>
								<td class="tdBlank"><asp:label id="lblBuildingDensity" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="form-item">计容积率建筑面积：</td>
								<td class="tdBlank"><asp:label id="lblBuildingSpaceForVolumeRate" Runat="server"></asp:label></td>
								<td class="form-item">不计容积率建筑面积：</td>
								<td class="tdBlank"><asp:label id="lblBuildingSpaceNotVolumeRate" Runat="server"></asp:label></td>
								<td class="tdBlank"></td>
								<td class="tdBlank"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table">
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="200" height="25" valign="bottom" class="note">规划要点</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr vAlign="top" height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<table id="tbDtl" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server"
											class="list">
											<tr align="center" class="list-title">
												<td colSpan="2" width="16%">产品组合</td>
												<td width="10%" align="right">占地面积<br>
													(平米)</td>
												<td width="10%" align="right">容积率</td>
												<td width="10%" align="right">建筑面积<br>
													(平米)</td>
												<td width="10%" align="right">可售率</td>
												<td width="10%" align="right">可售面积<br>
													(平米)</td>
												<td width="10%" align="right">产品比例</td>
												<td width="10%" align="right">平均每户面积<br>
													(平米)</td>
												<td width="10%" align="right">总户数</td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
						</table>
					</td>
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
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--
var CurrUrl = window.location.href;

//新增产品组合
function AddPBSType(){
	var w = 400;
	var h = 300;
	window.open("PBSTypeModify.aspx?Action=Insert&ProjectCode=" + Form1.txtProjectCode.value, "产品组合修改" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}

//修改规划要点
function Modify()
{
	window.location.href = "PBSTypeLayoutModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value;
//	OpenCustomWindow("PBSTypeLayoutModify.aspx", "编辑规划要点", 500, 580);
}

//查看产品组合
function ShowPBSType(PBSTypeCode)
{
	window.location.href = "PBSTypeInfo.aspx?PBSTypeCode=" + PBSTypeCode + "&FromUrl=" + escape(CurrUrl);
}

//产品组合从模板导入
function PBSTypeImport()
{
	if (!confirm("将以模板覆盖当前产品组合，确实要导入吗？"))
		return false;
		
	return true;
}

//产品组合导出成模板
function PBSTypeExport()
{
	if (!confirm("将以当前产品组合覆盖模板，确实要导出吗？"))
		return false;
		
	return true;
}

//-->
		</SCRIPT>
	</body>
</HTML>
