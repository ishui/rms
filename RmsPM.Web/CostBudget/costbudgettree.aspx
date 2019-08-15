<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetTree" CodeFile="CostBudgetTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostBudgetTree</title>
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
	
//�鿴��̬����
function View(CostBudgetSetCode)
{
	var CostBudgetBackupCode = '<%=Request["CostBudgetBackupCode"]%>';
	
	if (CostBudgetBackupCode != "") //����
	{
		if (CostBudgetSetCode.substr(0, 2) == "G_")  //Ԥ������
		{
			var GroupCode = CostBudgetSetCode.replace("G_", "");
			OpenFullWindow("CostBudgetGroupInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&GroupCode=" + GroupCode + "&CostBudgetBackupCode=" + CostBudgetBackupCode + "&FullCost=<%=Request["FullCost"]%>" + "&HideBudget=<%=Request["HideBudget"]%>", "CostBudgetGroupBackupInfo_" + GroupCode);
		}
		else if (CostBudgetSetCode.substr(0, 2) == "D_")  //����
		{
		}
		else
		{
			OpenFullWindow("CostBudgetInfo.aspx?CostBudgetSetCode=" + CostBudgetSetCode + "&CostBudgetBackupCode=" + CostBudgetBackupCode + "&FullCost=<%=Request["FullCost"]%>" + "&HideBudget=<%=Request["HideBudget"]%>", "CostBudgetBackupInfo_" + CostBudgetSetCode);
		}
	}
	else
	{
		if (CostBudgetSetCode.substr(0, 2) == "G_")  //Ԥ������
		{
			var GroupCode = CostBudgetSetCode.replace("G_", "");
			OpenFullWindow("CostBudgetGroupInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&GroupCode=" + GroupCode + "&FullCost=<%=Request["FullCost"]%>" + "&HideBudget=<%=Request["HideBudget"]%>", "CostBudgetGroupInfo_" + GroupCode);
		}
		else if (CostBudgetSetCode.substr(0, 2) == "D_")  //����
		{
		}
		else
		{
			OpenFullWindow("CostBudgetInfo.aspx?CostBudgetSetCode=" + CostBudgetSetCode + "&FullCost=<%=Request["FullCost"]%>" + "&HideBudget=<%=Request["HideBudget"]%>", "CostBudgetInfo_" + CostBudgetSetCode);
		}
	}
}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%" id="tbl-container">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tbl-tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TH noWrap align="center">Ԥ���</TH>
						<TH noWrap align="center" style='display:<%=ShowColBudget?"":"none"%>'><br>����Ԥ��<br>(A)</TH>
						<TH noWrap align="center"><br>�Ѷ���ͬ<br>(B)</TH>
						<TH noWrap align="center"><br>�Ѷ����<br>(C)</TH>
						<TH noWrap align="center">����<br>��ͬ/���<br>(D)</TH>
						<TH noWrap align="center"><br>�������ռ�<br>(E)=B+C+D</TH>
						<TH noWrap align="center" style='display:<%=ShowContractAccountMoney?"":"none"%>'><br>�ѽ���<br>(E2)</TH>
						<TH noWrap align="center" style='display:<%=ShowColBudget?"":"none"%>'><br>���<br>(F)=E-A</TH>
						<TH noWrap align="center" style='display:<%=ShowColBeforeChange?"":"none"%>'>Ԥ��<br>�������<br>(G2)=A/GFA</TH>
						<TH noWrap align="center" style='display:<%=ShowColBeforeChange?"":"none"%>'>���ǰ<br>�������<br>(G3)=B/GFA</TH>
						<TH noWrap align="center"><br>�������<br>(G1)=E/GFA</TH>
						<TH noWrap align="center"><br>�ۼ�����<br>(H)</TH>
						<TH noWrap align="center"><br>����%<br>(I)=H/E</TH>
						<TH noWrap align="center"><br>δ����<br>(J)=E-H</TH>
						<TH noWrap align="center"><br>�ۼ��Ѹ�<br>(K)</TH>
						<TH noWrap align="center" style="display:none">����޸���</TH>
						<TH noWrap align="center" style="display:none">����޸�����</TH>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
			</div>
		</form>
		<Script language="javascript">
		
var ShowColBeforeChange = '<%=ShowColBeforeChange%>';
var ShowContractAccountMoney = '<%=ShowContractAccountMoney%>';
var ShowColBudget = '<%=ShowColBudget%>';
		
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="CostBudgetTreeData.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostBudgetBackupCode=<%=Request["CostBudgetBackupCode"]%>"  ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��

var TreeModels=new Array();

//old
/*
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td  id='td@CostBudgetSetCode' onclick=\"SpreadNodes('@CostBudgetSetCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td nowrap><a style=\"display=@ShowHref\" href=\"#\" id=\"@CostBudgetSetCode\" onclick=\"View('@CostBudgetSetCode');return false;\">@CostBudgetSetName</a><span style=\"display=@ShowSpan\">@CostBudgetSetName</span></td></tr></table>";
*/

//new
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td><span>&nbsp;&nbsp;&nbsp;</span></td>@IndentEnd";
v0+="<td nowrap id='td@CostBudgetSetCode' onclick=\"SpreadNodes('@CostBudgetSetCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td nowrap><img src=\"../Images/@IconName\" style=\"display:@IconDisplay\"><a style=\"display=@ShowHref\" href=\"#\" id=\"@CostBudgetSetCode\" onclick=\"View('@CostBudgetSetCode');return false;\">@CostBudgetSetName</a><span style=\"display=@ShowSpan\">@CostBudgetSetName</span></td></tr></table>";

TreeModels.push(v0);

//Ԥ��
if (ShowColBudget == "True")
{
    TreeModels.push("<div align=right>@BudgetMoney</div>");
}

TreeModels.push("<div align=right>@ContractMoney</div>");
TreeModels.push("<div align=right>@ContractChangeMoney</div>");
TreeModels.push("<div align=right>@ContractApplyMoney</div>");
TreeModels.push("<div align=right>@ContractTotalMoney</div>");

//�ѽ���
if (ShowContractAccountMoney == "True")
{
    TreeModels.push("<div align=right>@ContractAccountMoney</div>");
}

//Ԥ��
if (ShowColBudget == "True")
{
    //TreeModels.push("<div align=right>@ContractBudgetBalance</div>");
    TreeModels.push("<div align=right @AttributesContractBudgetBalance>@ContractBudgetBalance</div>");
}

//���ǰ�ɱ��Ա�
if (ShowColBeforeChange == "True")
{
    TreeModels.push("<div align=right>@BudgetPrice</div>");
    TreeModels.push("<div align=right>@ContractOriginalPrice</div>");
}

TreeModels.push("<div align=right>@BuildingPrice</div>");
TreeModels.push("<div align=right>@ContractPay1</div>");
TreeModels.push("<div align=right>@ContractPayPercent</div>");
TreeModels.push("<div align=right>@ContractPayBalance</div>");
TreeModels.push("<div align=right>@ContractPayReal<div>");

//TreeModels.push("@ModifyPersonName");
//TreeModels.push("@ModifyDate");

//�ڵ�
function SpreadNodes(Code,LayerNumber,obj){
	if (Code.substr(0, 2) == "G_")  //Ԥ�������չ��
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfGroup&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetSetCode");
	}
	else if (Code.substr(0, 2) == "D_")  //����չ��
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfDistrict&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetSetCode");
	}
	else
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+Code,obj,TreeModels,"CostBudgetSetCode");
	}
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostBudgetSetCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostBudgetSetCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostBudgetSetCode",RowClassName,CostBudgetSetCode);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostBudgetSetCode",RowClassName,GridClassName);
}

//var d1 = new Date();

GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostBudgetSetCode",RowClassName,GridClassName);

//var d2 = new Date();
//alert(d1.getMinutes() + ":" + d1.getSeconds() + "." + d1.getMilliseconds() + " - " + d2.getMinutes() + ":" + d2.getSeconds() + "." + d2.getMilliseconds());	

//�ϼ���
if (document.all.Tree.childNodes.length > 0)
{
	var node = document.all.Tree.childNodes[document.all.Tree.childNodes.length - 1];
	for(var i=0;i<node.childNodes.length;i++)
	{
		node.childNodes[i].className = "tree-sum";
	}
}

//չ�������
if (document.all.Tree.childNodes.length == 2)  //�кϼ��У�������2
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

if (window.parent.document.all.divHintLoad)
    window.parent.document.all.divHintLoad.style.display = 'none';

		</Script>
	</body>
</HTML>
