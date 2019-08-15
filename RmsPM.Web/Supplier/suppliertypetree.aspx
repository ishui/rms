<%@ Page language="c#" Inherits="RmsPM.Web.Supplier.SupplierTypeTree" CodeFile="SupplierTypeTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>SupplierTypeTree</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function SelectNode(GroupCode){
				window.parent.SelectNode(GroupCode);
			}

		</SCRIPT>
</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			
			
			<TABLE id="Table3" borderColor="#e7e7e7" cellSpacing="0" cellPadding="0" rules="rows" width="100%"
				border="0" class="tree">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD id="tdGroupName" noWrap><a href=## onclick='SelectNode("");'>��������</a></TD>
						<TD id="tdDescription" noWrap>˵ ��</TD>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
				<tfoot>
					<TR class="tree-title">
						<TD id="tdFootGroupName">&nbsp;</TD>
						<TD id="tdFootDescription">&nbsp;</TD>
					</TR>
				</tfoot>
			</TABLE>
		</form>
		
		<SCRIPT language="javascript">
		
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var treeType = '<%=Request["TreeType"]%>';
var DataSourceUrl='SupplierData.aspx?ProjectCode=<%=Request["ProjectCode"]%>' ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��


var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@GroupCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a href=\"#\" id=\"@GroupCode\" onclick=\"SelectNode('@GroupCode');return false;\">@SortID @GroupName</a></td></tr></table>";

v1="@Description";

TreeModels.push(v0);
if ( treeType != 'Single' )
	TreeModels.push(v1);
else
	{
		document.all("tdDescription").style.display = "none";
		document.all("tdFootDescription").style.display = "none";
	}


//�ڵ�
function SpreadNodes(GroupCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+GroupCode,obj,TreeModels,"GroupCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"GroupCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"GroupCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"GroupCode",RowClassName,GridClassName);

		</SCRIPT>
	</body>
</HTML>
