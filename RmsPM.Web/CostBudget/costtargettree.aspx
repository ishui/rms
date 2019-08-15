<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetTree" CodeFile="CostTargetTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostTargetTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
//�鿴Ŀ�����
function View(CostBudgetCode, CostBudgetSetCode)
{
    if (CostBudgetCode == "") //�½�Ŀ�����
		OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + CostBudgetSetCode , "Ԥ���Ŀ������޸�");
	else if (CostBudgetCode.substr(0, 5) == "NULL_")
		OpenFullWindow("CostTargetInfo.aspx?CostBudgetSetCode=" + CostBudgetSetCode , "");
	else
		OpenFullWindow("CostTargetInfo.aspx?CostBudgetCode=" + CostBudgetCode + "&CostBudgetSetCode=" + CostBudgetSetCode , "");
}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr height="100%">
					<td>
			<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%" id="tbl-container">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tbl-tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TH noWrap align="center">Ԥ���</TH>
						<TH noWrap align="center">״̬</TH>
						<TH noWrap align="center">Ԥ�����(Ԫ)</TH>
						<TH noWrap align="center">������</TH>
						<TH noWrap align="center">��������</TH>
						<TH noWrap align="center">����޸���</TH>
						<TH noWrap align="center">����޸�����</TH>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
			</div>
					</td>
				</tr>
			</table>
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="CostTargetTreeData.aspx?ProjectCode=<%=Request["ProjectCode"]%>"  ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��



var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td><span>&nbsp;&nbsp;&nbsp;</span></td>@IndentEnd";
v0+="<td nowrap id='td@CostBudgetCode' onclick=\"SpreadNodes('@CostBudgetCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td nowrap><img src=\"../Images/@IconName\" style=\"display:@IconDisplay\"><a style=\"display=@ShowHref\" href=\"#\" id=\"@CostBudgetCode\" onclick=\"View('@CostBudgetCode', '@CostBudgetSetCode');return false;\">@CostBudgetSetName @VerName</a><span style=\"display=@ShowSpan\">@CostBudgetSetName @VerName</span></td></tr></table>";
TreeModels.push(v0);

//TreeModels.push("@VerName");
TreeModels.push("@StatusName");
TreeModels.push("<div align=right>@TotalBudgetMoney</div>");
TreeModels.push("@CreatePersonName");
TreeModels.push("@CreateDate");
TreeModels.push("@ModifyPersonName");
TreeModels.push("@ModifyDate");

//�ڵ�
function SpreadNodes(Code,LayerNumber,obj){
	if (Code.substr(0, 2) == "G_")  //Ԥ�������չ��
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfGroup&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetCode");
	}
	else if (Code.substr(0, 2) == "D_")  //����չ��
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfDistrict&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetCode");
	}
	else
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfTarget&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetCode");
	}
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostBudgetCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostBudgetCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostBudgetCode",RowClassName,CostBudgetCode);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostBudgetCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostBudgetCode",RowClassName,GridClassName);

/*
//�ϼ���
if (document.all.Tree.childNodes.length > 0)
{
	var node = document.all.Tree.childNodes[document.all.Tree.childNodes.length - 1];
	for(var i=0;i<node.childNodes.length;i++)
	{
		node.childNodes[i].className = "tree-sum";
	}
}
*/

//չ�������
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
