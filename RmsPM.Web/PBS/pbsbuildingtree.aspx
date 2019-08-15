<%@ Reference Control="~/pbs/buildingtree.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BuildingTree" Src="BuildingTree.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSBuildingTree" CodeFile="PBSBuildingTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBSTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 项目导图
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" type="button" value="新增区域" name="btnAddArea" id="btnAddArea" runat="server"
							onclick="doCreateArea(Form1.txtProjectCode.value, '');"> <input class="button" type="button" value="新增楼栋" name="btnAddBuilding" id="btnAddBuilding"
							runat="server" onclick="doCreateBuilding(Form1.txtProjectCode.value, '');"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" type="button" value="分布图" name="btnGotoBuildingGraph" onclick="GotoBuildingGraph(Form1.txtProjectCode.value, '');">
					</td>
				</tr>
				<tr height="100%">
					<td valign="top" class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<uc1:BuildingTree id="ucBuildingTree" runat="server"></uc1:BuildingTree>
						</div>
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
			</TABLE>
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
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
		<SCRIPT language="javascript">
	var clicktd;
	var clickid;
	
	var CurrUrl = "PBSBuildingTree.aspx?ProjectCode=" + Form1.txtProjectCode.value;
/*	
	function TDClick(obj1, code) {
		if (clicktd != undefined) {
			clicktd.className='list-i';
		}

		var node = FindNode(document.all("Tree"), code);
		clickid = code;
		
		node.className = "list-2";

		clicktd = node;
		
		document.all.tdAddChild.style.display = "block";
		document.all.tdModify.style.display = "block";
	}

	//编辑区域、楼栋
	function Modify(code){
		var w = 760;
		var h = 540;
		window.open("PBSBuildModify.aspx?Action=Modify&BuildingCode="+code, "楼栋修改" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
	}
*/

	//查看区域、楼栋
	function View(code)
	{
		document.all.divHintLoad.style.display = '';
		window.location.href = "PBSBuildInfo.aspx?BuildingCode="+code + "&FromUrl=" + escape(CurrUrl);
	}
	
		</SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
	</body>
</HTML>
