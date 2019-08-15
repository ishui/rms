<%@ Page language="c#" Inherits="RmsPM.Web.Systems.FunctionStructureTree" CodeFile="FunctionStructureTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ܽṹ��</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	

	function SelectFunctionStructure(FunctionStructureCode,FunctionStructureName){
		window.parent.frames.SelectFunctionStructure(FunctionStructureCode,FunctionStructureName);
	}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD noWrap align="left" id="tdFunctionStructureCode">���ܵ�</TD>
						<TD noWrap align="left" id="tdIsAvailable">�Ƿ���Ч</TD>
						<TD noWrap align="left" id="tdIsRightControlPoint">�Ƿ�Ȩ�޿��Ƶ�</TD>
						<TD noWrap align="left" id="tdIsRoleControlPoint">�Ƿ��ɫ���Ƶ�</TD>
						<TD noWrap align="left" id="tdIsSystemClassPoint">�Ƿ�ϵͳ������</TD>
						<TD noWrap align="left" id="tdDescription">˵��</TD>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="FunctionStructureData.aspx?a=1"  ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��



var TreeModels=new Array();

var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td  id='td@FunctionStructureCode' onclick=\"SpreadNodes('@FunctionStructureCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a href=\"#\" id=\"@FunctionStructureCode\" onclick=\"SelectFunctionStructure('@FunctionStructureCode','@FunctionStructureName');return false;\">@FunctionStructureCode  @FunctionStructureName</a></td></tr></table>";


TreeModels.push(v0);
TreeModels.push("@IsAvailable");
TreeModels.push("@IsRightControlPoint");
TreeModels.push("@IsRoleControlPoint");
TreeModels.push("@IsSystemClass");
TreeModels.push("@Description");

//�ڵ�
function SpreadNodes(FunctionStructureCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+FunctionStructureCode,obj,TreeModels,"FunctionStructureCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);

//չ�������
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
