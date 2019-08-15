<%@ Reference Control="~/pbs/pbstypetreectrl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PBSTypeTreeCtrl" Src="PBSTypeTreeCtrl.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSTypeInfo" CodeFile="PBSTypeInfo.aspx.cs" %>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 产品组合信息</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="Modify()" type="button" value="修 改" name="btnModify">
						<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
						<input class="button" id="btnAddChild" onclick="Insert();" type="button" value="新增子项" name="btnAddChild"
							runat="server">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="30%">名称：</TD>
								<TD width="70%"><asp:label id="lblPBSTypeName" Runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td class="form-item">所属产品组合：</td>
								<td><asp:label id="lblParentName" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<TD class="form-item" width="20%">描述：</TD>
								<TD><asp:label id="lblDescription" Runat="server"></asp:label></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table border="0" cellpadding="0" cellspacing="0" align="left">
							<tr> 
								<td class="intopic">产品组合子项</td>
							</tr>
						</table>
					</td>
				</tr>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtPBSTypeCode" type="hidden" name="txtPBSTypeCode" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
		</form>
		<SCRIPT language="javascript">
	
	//新增子项
	function Insert(){
		var ParentCode = Form1.txtPBSTypeCode.value;
		var w = 400;
		var h = 300;
		window.open("PBSTypeModify.aspx?Action=Insert&ParentCode="+ParentCode+"&ProjectCode="+Form1.txtProjectCode.value, "产品组合修改" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
	}

	//修改
	function Modify(){
		var code = Form1.txtPBSTypeCode.value;
		var w = 400;
		var h = 300;
		window.open("PBSTypeModify.aspx?Action=Modify&PBSTypeCode="+code, "产品组合修改" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
	}
	
		</SCRIPT>
	</body>
</HTML>
