<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectPBS" CodeFile="SelectPBS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>选择单位工程</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript">

//单选
function DoSelect(PBSType, code, name)
{
	var flag = "<%=Request.QueryString["flag"]%>";
	
	if (PBSType == "P")
	{
		code = "";
		name = "项目";
	}
	
//	alert(PBSType + "\n" + code + "\n" + name);
	
	window.opener.SelectPBSReturn(PBSType, code, name, flag);
	window.close();
}
	
		</SCRIPT>
</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择单位工程</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<div style="overflow:auto;width:100%;height:100%">
						<TABLE id="Table3" borderColor="#e7e7e7" class="tree" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
							border="0">
							<thead style="display:none">
								<TR class="tree-title">
									<TD noWrap align="left">楼栋名称</TD>
								</TR>
							</thead>
							<tbody id="Tree">
							</tbody>
							<tfoot>
								<TR class="tree-title" style="DISPLAY:none">
									<TD noWrap align="center">&nbsp;</TD>
								</TR>
							</tfoot>
						</TABLE>
						</div>
					</td>
				</tr>
				<tr id="trBtn">
					<td>
						<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center"><input type="button" id="btnClear" class="submit" value="清 除" name="btnClear" onclick="DoSelect('', '', '');"
										runat="server">	<input id="btnCancel" onclick="window.close();" type="button" value="取消" class="submit">
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtPBSCode" name="txtPBSCode" runat="server">
			<input type="hidden" id="txtPBSType" name="txtPBSType" runat="server">
		</form>
		<SCRIPT language="javascript">

//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var ProjectCode = Form1.txtProjectCode.value;

var DataSourceUrl="../PBS/PBSBuildingData.aspx?ProjectCode=" + ProjectCode;

//项目节点
var TreeModelsProject=new Array();
TreeModelsProject[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModelsProject[0]+="<td onclick=\"SpreadProject('@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
TreeModelsProject[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@BuildingCode\" onclick=\"javascript:DoSelect('@PBSType', '@BuildingCode', '@BuildingName');\">项目 @BuildingName</a></td></tr></table>";

//楼栋节点
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@BuildingCode\" onclick=\"javascript:DoSelect('@PBSType', '@BuildingCode', '@BuildingName');\">@BuildingName</a></td></tr></table>";

//展开项目
function SpreadProject(LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfProject",obj,TreeModels,"BuildingCode");
}

//节点
function SpreadNodes(BuildingCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+BuildingCode,obj,TreeModels,"BuildingCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"&GetType=Project",null,TreeModelsProject,"BuildingCode",RowClassName,GridClassName);

//展开根结点
if (document.all.Tree.childNodes.length == 1)
{
	SpreadProject(parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</SCRIPT>
	</body>
</HTML>
