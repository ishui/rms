<%@ Page language="c#" Inherits="RmsPM.Web.Cost.BudgetTree" CodeFile="BudgetTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����Ԥ��</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">

	var budgetCode = '<%=Request["BudgetCode"]%>';
/*	
	function ViewDynamicCostGraph(CostCode){
		OpenMiddleWindow("../Cost/CostContrast.aspx?CostCode=" + CostCode + "&BudgetCode=" + budgetCode ,"��̬����");
	}

*/	
	function doSelectBudgetNode(CostCode){
		window.parent.frames.doSelectBudgetNode(CostCode);
	}
	
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0" width=100%>
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD noWrap align="left" id="tdCostName" style="DISPLAY: none">������Ŀ</TD>
						<TD noWrap align="right" id="tdTotalMoney" style="DISPLAY: none">�������</TD>
						<TD noWrap align="right" id="tdBudgetCost" style="DISPLAY: none">Ԥ�����</TD>
						<TD noWrap align="right" id="tdBeforeHappenCost" style="DISPLAY: none">Ԥ��ǰ�ۼƷ�����</TD>
						<TD noWrap align="right" id="tdCurrentPlanCost" style="DISPLAY: none">����Ԥ��</TD>
						<TD noWrap align="right" id="tdAfterPlanCost" style="DISPLAY: none">������Ԥ��</TD>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
				<tfoot>
					<TR class="sum">
						<TD noWrap align="left" id="tdFootCostName" style="DISPLAY: none" class=sum-item>&nbsp;&nbsp;</TD>
						<TD noWrap align="right" id="tdFootTotalMoney" runat="server" style="DISPLAY: none">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootBudgetCost" runat="server" style="DISPLAY: none">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootBeforeHappenCost" runat="server" style="DISPLAY: none">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootCurrentPlanCost" runat="server" style="DISPLAY: none">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootAfterPlanCost" runat="server" style="DISPLAY: none">&nbsp;</TD>
					</TR>
				</tfoot>
			</TABLE>
			<input type="hidden" id="txtShowItems" runat="server" NAME="txtShowItems"><INPUT id="txtTreeType" type="hidden" name="txtTreeType" runat="server"><INPUT id="txtCheckBalance" type="hidden" name="txtCheckBalance" runat="server">
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var CheckBalance = Form1.txtCheckBalance.value;
var TreeType = Form1.txtTreeType.value; 
var DataSourceUrl="BudgetData.aspx?ProjectCode=<%=Request["ProjectCode"]%>&TreeType=" + TreeType + "&CheckBalance=" + CheckBalance + "&BudgetCode=" + budgetCode  ;


// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��


var showItemString = Form1.txtShowItems.value;
var SItems = showItemString.split(",")

var iCount = SItems.length;
for ( var i= 0; i<iCount ; i++)
{
	var obj = document.all("td" + SItems[i]  );
	var objFoot = document.all("tdFoot" + SItems[i]  );
	if ( obj != null && objFoot != null )
	{
		if ( HasString (showItemString,SItems[i]  ) )
		{
			obj.style.display = "";
			objFoot.style.display = "";
		}
		else
		{
			obj.style.display = "none";
			objFoot.style.display = "none";
		}
		
	}
	
}

var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@CostCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a  style=\"display=@ShowHref\" href=\"#\" id=\"@CostCode\" onclick=\"doSelectBudgetNode('@CostCode');return false;\">@SortID  @CostName</a><span style=\"display=@ShowSpan\">@SortID  @CostName</span></td></tr></table>";

/*
switch ( TreeType  )
{
	case "CBS":
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowCBS('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;
	case "CostEstimate" :
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowCostEstimate('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;
	case "CostEstimateCheck" :
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowCostEstimate('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;
	case "ReviseBudget":
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowReviseBudget('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;
	case "ReviseBudgetCheck":
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowReviseBudget('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;
		
	case "DynamicCost":
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowDynamicCost('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;

	case "PlanCost":
		v0+="<td><a href=\"#\" id=\"@CostCode\" onclick=\"ShowPlanCost('@CostCode');return false;\">@CostName</a></td></tr></table>";
		break;		
}
*/


//��һ�У�������
if ( HasString(showItemString,"CostName" ))
	TreeModels.push(v0);

//�������
if ( HasString(showItemString,"TotalMoney" ))
	TreeModels.push("<div align=right>@TotalMoney</div>");

//Ԥ�����
if ( HasString(showItemString,"BudgetCost" ))
	TreeModels.push("<div align=right>@BudgetCost</div>");

//��ǰ�ۼƷ�����
if ( HasString(showItemString,"BeforeHappenCost" ))
	TreeModels.push("<div align=right>@BeforeHappenCost</div>");

//����Ԥ��
if ( HasString(showItemString,"CurrentPlanCost" ))
	TreeModels.push("<div align=right>@CurrentPlanCost</div>");

//�����Ժ�ʣ��Ԥ��
if ( HasString(showItemString,"AfterPlanCost" ))
	TreeModels.push("<div align=right>@AfterPlanCost</div>");


// -----------------------------------
// ��ͨ�ã�����
// -----------------------------------

//�ڵ�
function SpreadNodes(CostCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+CostCode,obj,TreeModels,"CostCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostCode",RowClassName,GridClassName);

		</Script>
	</body>
</HTML>
