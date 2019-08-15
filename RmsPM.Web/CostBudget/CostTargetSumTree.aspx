<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetSumTree" CodeFile="CostTargetSumTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostTargetSumTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	

	function SelectCBS(CostCode,CostName){
		window.parent.frames.SelectCBS(CostCode,CostName);
	}
	
	function ShowSumMoney( EstimateCost)
	{
		document.all("tdFootTotalMoney").innerText = EstimateCost;

	}

	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr height="100%">
					<td>
						<div style="overflow: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
								width="100%">
								<thead>
									<TR class="tree-title" id="trTitle">
										<TD noWrap align="left">������Ŀ</TD>
										<TD noWrap align="left">��Ŀ</TD>
										<TD noWrap align="right" id="tdHeadTargetMoney" style="display:none">Ŀ�����(��Ԫ)</TD>
										<TD noWrap align="right" id="tdHeadBudgetMoney" style="display:none">Ԥ���ܶ�(��Ԫ)</TD>
									</TR>
								</thead>
								<tbody id="Tree">
								</tbody>
								<tfoot style="display:none">
									<TR class="sum">
										<TD noWrap id="tdFootCostName" class="sum-item"></TD>
										<TD noWrap id="tdFootSubjectName">&nbsp;</TD>
										<TD noWrap align="left" id="tdFootCostAllocationDescription">&nbsp;</TD>
									</TR>
								</tfoot>
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

var DataSourceUrl="CostTargetData.aspx?ProjectCode=<%=Request["ProjectCode"]%>"  ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��



var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td  id='td@CostCode' onclick=\"SpreadNodes('@CostCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a  style=\"display=none\" href=\"#\" id=\"@CostCode\" onclick=\"SelectCBS('@CostCode','@CostName');return false;\">@SortID  @CostName</a><span style=\"display=\">@SortID  @CostName</span></td></tr></table>";

TreeModels.push(v0);

//��Ŀ
TreeModels.push("@SubjectCode");

//�Ƿ�Ŀ�����
var TargetFlag = '<%=Request["TargetFlag"]%>';
if (TargetFlag == "1")  //Ŀ�����
{
	TreeModels.push("<div align=right>@TotalTargetMoney</div>");
	document.all("tdHeadTargetMoney").style.display = "";
}
else  //��̬Ԥ��
{
	TreeModels.push("<div align=right>@TotalBudgetMoney</div>");
	document.all("tdHeadBudgetMoney").style.display = "";
}


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

//չ�������
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
