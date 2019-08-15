<%@ Reference Control="~/pbs/pbstypetreectrl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PBSTypeTreeCtrl" Src="PBSTypeTreeCtrl.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSTypeTmpl" CodeFile="PBSTypeTmpl.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">系统管理 
									- 产品组合</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAddPBSType" onclick="AddPBSType();" type="button" value="新增产品组合"
							name="btnAddPBSType" runat="server">
						<input style="display:none" class="button" id="btnUpdateProject" onclick="UpdateProject();" type="button" value="更新所有项目"
							name="btnUpdateProject" runat="server" onserverclick="btnUpdateProject_ServerClick">
					</TD>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<uc1:PBSTypeTreeCtrl id="ucPBSTypeTree" runat="server"></uc1:PBSTypeTreeCtrl>
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
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" value="0"> <input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
		</form>
		<SCRIPT language="javascript">
	
var CurrUrl = window.location.href;

//新增产品组合
function AddPBSType(){
	var w = 400;
	var h = 300;
	window.open("PBSTypeModify.aspx?Action=Insert&ProjectCode=" + Form1.txtProjectCode.value, "产品组合修改" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}
	
//查看产品组合
function ShowPBSType(PBSTypeCode)
{
	window.location.href = "PBSTypeInfo.aspx?PBSTypeCode=" + PBSTypeCode + "&FromUrl=" + escape(CurrUrl);
}

//产品组合模板更新到所有项目
function UpdateProject()
{
	if (!confirm("将以模板覆盖所有项目的产品组合，确实要更新吗？"))
		return false;
		
	return true;
}

		</SCRIPT>
	</body>
</HTML>
