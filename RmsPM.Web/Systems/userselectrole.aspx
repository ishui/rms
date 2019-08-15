<%@ Page language="c#" Inherits="RmsPM.Web.Systems.UserSelectRole" CodeFile="UserSelectRole.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择角色</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XMLTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<style>.LeftMenuItem { FONT-SIZE: 12px; MARGIN: 1px; COLOR: #00309c }
	.LeftMenuItemOnMouseOver { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #fffbff }
	.LeftMenuItemOnActivty { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #ffe794 }
	A { COLOR: #000000; TEXT-DECORATION: none }
	A:hover { TEXT-DECORATION: none }
		</style>
		<SCRIPT language="javascript">

	var clicktd;
	var clicknode;
	
	function TDClick(obj) {
		if (clicktd != undefined) {
			clicktd.className='';
		}
		obj.className='LeftMenuItemOnMouseOver';
		clicktd = obj;
	}
	
	function ShowRoleList(UnitCode){
		document.all.frameRoleList.src = "SelectRoleList.aspx?UnitCode=" + UnitCode + "&UserCode=" + Form1.txtUserCode.value + "&SelectRoleCode=" + Form1.txtSelectRoleCode.value;
	}
	
/*	function SaveSelect() {
		Form1.txtSelectRoleCode.value;
		window.close();
	}
*/
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" colSpan="2" height="25"><asp:label id="lbTitle" runat="server" CssClass="TitleText" BackColor="Transparent">选择角色</asp:label>——用户：<asp:label id="lblUserName" runat="server" BackColor="Transparent"></asp:label></td>
				</tr>
				<TR>
					<TD vAlign="top" align="center" width="150" background="../Images/LeftBg.gif" bgColor="#e7efff">
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr vAlign="top">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<TABLE id="Table3" borderColor="#e7e7e7" cellSpacing="0" cellPadding="3" width="100%" border="0">
											<TBODY id="Tree" align="top">
											</TBODY>
										</TABLE>
									</div>
								</td>
							</tr>
						</TABLE>
					</TD>
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><iframe id="frameRoleList" src="SelectRoleList.aspx" frameBorder="0" width="100%" height="100%">
							</iframe>
						</div>
						<INPUT id="btnUnitClick" type="button" value="Button" name="btnUnitClick" runat="server">
					</td>
				</TR>
				<tr height="50">
					<td colSpan="2">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center" width="50%"><input class="submit" id="btnOK" type="button" value="确 定" runat="server" onserverclick="btnOK_ServerClick"></TD>
								<TD align="center" width="50%"><input class="submit" id="btnCancel" onclick="window.close();" type="button" value="取 消">
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<asp:label id="lblMessage" ForeColor="red" Runat="server"></asp:label></TD></TR></TBODY></TABLE><input id="txtFrom" type="hidden" name="txtFrom" runat="server"><input id="txtUserCode" type="hidden" name="txtUserCode" runat="server"><INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server"><INPUT id="txtSelectUnitCode" type="hidden" name="txtSelectUnitCode" runat="server">
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtRootUnitCode" type="hidden" name="txtRootUnitCode" runat="server"><input id="txtSelectRoleCode" type="hidden" value="first" name="txtSelectRoleCode" runat="server">
		</form>
		<SCRIPT language="javascript">

//Tree
var TreeObj=document.all("Tree");
var RowClassName="ListBodyTr1";
var GridClassName="TreeViewItemTd";

var DataSourceUrl="DepartmentData.aspx?TreeType=";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点

//部门
var TreeModels=new Array();
var v0 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadUnitNodes('@Id','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td style=\"CURSOR: hand\" id=\"@Id\" onclick=\"TDClick(this);ShowRoleList('@Id');return false;\"><img src=\"../Images/dept.gif\">@UnitName</td></tr></table>";
//v0+="<td><img src=\"../Images/@ImageName\"><a href=\"#\" id=\"@Id\" onclick=\"ShowEditMenu(this, '@Layer', '@ParentCode', '@NodeType');return false;\">@UnitName</a></td></tr></table>";
TreeModels.push(v0);

//展开部门
function SpreadUnitNodes(UnitCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&UnitCode="+UnitCode + "&ParentLayer=" + LayerNumber,obj,TreeModels,"Id");
}

var rootCode = Form1.txtRootUnitCode.value;

	if (rootCode != "")
	{
		GetChildNodes(DataSourceUrl+"&GetType=SingleNode&NodeId=" + rootCode,null,TreeModels,"Id",RowClassName,GridClassName);
		SpreadUnitNodes(rootCode, 2, document.all.Tree.childNodes[0]);
	}
	else
	{
		GetChildNodes(DataSourceUrl+"&GetType=all&ParentLayer=0",null,TreeModels,"Id",RowClassName,GridClassName);
		SpreadUnitNodes("", 1, document.all.Tree.childNodes[0]);
//		GetChildNodes(DataSourceUrl+"&UnitCode=" + rootCode,null,TreeModels,"Id",RowClassName,GridClassName);
	}

	ShowRoleList("");
		</SCRIPT>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>
