<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetInfoTree" CodeFile="CostBudgetInfoTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostBudgetInfoTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="CostBudgetInfo.js" charset="gb2312"></SCRIPT>
		<script language="javascript" src="../Images/locked-column.js"></script>
		<SCRIPT language="javascript">
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<div id="tbl-container" onkeydown="return ListKeyDown(this);">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tbl-tree" border="0"
				width="100%">
				<thead>
					<tr class="tree-title">
						<th noWrap align="center" rowSpan="2">
							������</th>
						<th noWrap align="center" rowSpan="2">
							��ͬ���</th>
						<th noWrap align="center" rowSpan="2">
							����</th>
						<th noWrap align="center" rowSpan="2">
							�а���</th>
						<th noWrap align="center" rowSpan="2">
							˵��</th>
						<th id="tdTargetHead" noWrap align="center" rowSpan="2" onmouseup1="ShowTargetHeadMenu(this);">
							����Ԥ��<br>
							<asp:label id="lblTargetCheckDate" Runat="server"></asp:label>&nbsp;<A id="hrefTargetVerID" onclick="ShowTargetHeadMenu(this);return false;" href="#" runat="server"></A><span id="spanTargetVerID" runat="server"></span><span id="spanListTitleTargetMoney" style="DISPLAY: none" runat="server"><br>
								(<span id="spanListTitleTargetMoneyDesc" runat="server"></span>��)</span><br>
							(A)</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							�Ѷ���ͬ<br>
							(B)</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							�Ѷ����<br>
							(C)</th>
						<th noWrap align="center" rowSpan="2">
							����<br>
							��ͬ/���<br>
							(D)</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							�������ռ�<br>
							(E)=B+C+D</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							���<br>
							(F)=E-A</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							�������<br>
							(G)=E/GFA</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							��Ԫ���<br>
							E/��Ԫ��</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							�ۼ�����<br>
							(H)</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							����%<br>
							(I)=H/E</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							δ����<br>
							(J)=E-H</th>
						<th noWrap align="center" rowSpan="2">
							<br>
							�ۼ��Ѹ�<br>
							(K)</th>
					</tr>
					<tr class="tree-title">
					</tr>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtExpandNode" type="hidden" name="txtExpandNode" runat="server"><INPUT id="txtCostBudgetCode" type="hidden" name="txtCostBudgetCode" runat="server">
			<INPUT id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<INPUT id="txtPBSType" type="hidden" name="txtPBSType" runat="server"><INPUT id="txtPBSCode" type="hidden" name="txtPBSCode" runat="server">
			<INPUT id="txtFirstCostBudgetCode" type="hidden" name="txtFirstCostBudgetCode" runat="server">
			<INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server"> <INPUT id="txtListScrollTop" type="hidden" name="txtListScrollTop" runat="server"><INPUT id="txtListScrollLeft" type="hidden" name="txtListScrollLeft" runat="server">
			<input id="txtSessionEntityID" type="hidden" name="txtSessionEntityID" runat="server">
			<INPUT id="txtLastSelectedRowID" type="hidden" name="txtLastSelectedRowID" runat="server">
			<INPUT id="txtLastSelectedRowClass" type="hidden" name="txtLastSelectedRowClass" runat="server"><INPUT id="Hidden3" type="hidden" name="txtListScrollLeft" runat="server">
			<INPUT id="txtShowTargetMoneyHisVerID" type="hidden" name="txtShowTargetMoneyHisVerID"
				runat="server"> <INPUT id="txtHasTargetHis" type="hidden" name="txtHasTargetHis" runat="server"><INPUT id="txtShowTargetHis" type="hidden" name="txtShowTargetHis" runat="server">
			<input id="txtCostBudgetBackupCode" type="hidden" name="txtCostBudgetBackupCode" runat="server"><input id="txtCostBudgetBackupSetCode" type="hidden" name="txtCostBudgetBackupSetCode"
				runat="server">
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="";
var GridClassName="tree-tr";

var DataSourceUrl="CostBudgetInfoTreeData.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostBudgetSetCode=<%=Request["CostBudgetSetCode"]%>&CostBudgetBackupSetCode=<%=Request["CostBudgetBackupSetCode"]%>&SessionEntityID=<%=SessionEntityID%>";

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��

var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td><span>&nbsp;&nbsp;&nbsp;</span></td>@IndentEnd";
v0+="<td nowrap onclick=\"SpreadNodes('@CostBudgetDtlCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td nowrap align=\"left\" title=\"@SortID\">@CostName</td></tr></table>";

TreeModels.push(v0);

TreeModels.push("<div class=\"@ClassTd\">@ContractIDHtml</div>");
TreeModels.push("<div class=\"@ClassTd\">@ContractNameHtml</div>");
TreeModels.push("<div class=\"@ClassTd\">@SupplierNameHtml</div>");
TreeModels.push("<div class=\"@ClassTd\">@DescriptionHtml</div>");

TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleBudgetMoney\">@BudgetMoney</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractMoney\">@ContractMoney</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractChangeMoney\">@ContractChangeMoney</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractApplyMoney\">@ContractApplyMoney</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractTotalMoney\">@ContractTotalMoney</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractBudgetBalance\">@ContractBudgetBalance</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleBuildingPrice\">@BuildingPrice</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleHousePrice\">@HousePrice</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractPay1\">@HtmlContractPay1</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right>@ContractPayPercent</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractPayBalance\">@ContractPayBalance</div>");
TreeModels.push("<div class=\"@ClassTd\" align=right title=\"@TitleContractPayReal\">@HtmlContractPayReal<div>");

//�ڵ�
function SpreadNodes(Code,LayerNumber,obj){
    GridClassName = "tree-tr-" + (parseInt(LayerNumber)+1);
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+Code,obj,TreeModels,"CostBudgetDtlCode");

    //ÿ���ӽڵ���¼�
    var node = GetTreeNode(obj);

    var parent = node;
    while (node.nextSibling)
    {
        node = node.nextSibling;
        if (IsChildNode(node, parent))
		{
    		node.onclick = XmlTreeRowClick;
    	}
    	else
    	{
    	    break;
    	}
	}
}

/*
//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostBudgetDtlCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostBudgetDtlCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostBudgetDtlCode",RowClassName,CostBudgetSetCode);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostBudgetDtlCode",RowClassName,GridClassName);
}
*/

//var d1 = new Date();

GridClassName="tree-tr-1";
GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostBudgetDtlCode",RowClassName,GridClassName);

//var d2 = new Date();
//alert(d1.getMinutes() + ":" + d1.getSeconds() + "." + d1.getMilliseconds() + " - " + d2.getMinutes() + ":" + d2.getSeconds() + "." + d2.getMilliseconds());	

//ÿ���ӽڵ���¼�
for(var i=0;i<TreeObj.childNodes.length;i++)
{
    var node = GetTreeNode(TreeObj.childNodes[i]);
	node.onclick = XmlTreeRowClick;
}

//չ�������
if (TreeObj.childNodes.length == 1)
{
	SpreadNodes(TreeObj.childNodes[0].NodeId, parseInt(TreeObj.childNodes[0].NodeLayer) - 1, TreeObj.childNodes[0]);
}

		</Script>
	</body>
</HTML>
