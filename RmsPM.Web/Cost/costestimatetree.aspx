<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostEstimateTree" CodeFile="CostEstimateTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostEstimateTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
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
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD noWrap align="left" id="tdCostName">������Ŀ</TD>
						<TD noWrap align="left" id="tdSubjectCode">��Ŀ���</TD>
						<TD noWrap align="left" id="tdEstimateMark">�������</TD>
						<TD noWrap align="right" id="tdUnitPrice">����(Ԫ)</TD>
						<TD noWrap align="left" id="tdMeasurementUnit">������λ</TD>
						<TD noWrap align="right" id="tdProjectQuantity">����</TD>
						<TD noWrap align="right" id="tdTotalMoney">�������</TD>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
				<tfoot>
					<TR class="sum">
						<TD noWrap id="tdFootCostName" >&nbsp;</TD>
						<TD noWrap id="tdFootSubjectCode">&nbsp;</TD>
						<TD noWrap id="tdFootEstimateMark">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootUnitPrice">&nbsp;</TD>
						<TD noWrap id="tdFootMeasurementUnit">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootProjectQuantity">&nbsp;</TD>
						<TD noWrap align="right" id="tdFootTotalMoney" runat="server" >&nbsp;</TD>
					</TR>
				</tfoot>
			</TABLE>
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="CostEstimateData.aspx?ProjectCode=<%=Request["ProjectCode"]%>"  ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��


var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td id='td@CostCode' onclick=\"SpreadNodes('@CostCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a  style=\"display=@ShowHref\" href=\"#\" id=\"@CostCode\" onclick=\"SelectCBS('@CostCode','@CostName');return false;\">@SortID  @CostName</a><span style=\"display=@ShowSpan\">@SortID  @CostName</span></td></tr></table>";


//��һ�У�������
TreeModels.push(v0);

//��Ŀ���
TreeModels.push("@SubjectCode");

//��������
TreeModels.push("@EstimateMark");

//����
TreeModels.push("<div align=right>@UnitPrice</div>");

//������λ
TreeModels.push("@MeasurementUnit");

//����
TreeModels.push("<div align=right>@ProjectQuantity</div>");

//�������
TreeModels.push("<div align=right>@TotalMoney</div>");


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
