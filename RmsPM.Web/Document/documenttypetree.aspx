<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentTypeTree" CodeFile="DocumentTypeTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DocumentTypeTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	function ShowDocumentType(DocumentTypeCode){
		window.parent.location = "DocumentTypeInfo.aspx?DocumentTypeCode="+DocumentTypeCode + "&FromUrl=" + escape(window.parent.location);
	}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" borderColor="#e7e7e7" class="tree" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
				border="0">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD noWrap align="left" id="tdTypeName">�ĵ�����</TD>
					</TR>
				</thead>
				<tbody id="Tree" class="tree">
				</tbody>
				<tfoot style="display:none">
					<TR class="tree-title">
						<TD noWrap align="center" colspan="20">&nbsp;</TD>
					</TR>
				</tfoot>
			</TABLE>
			<input type="hidden" id="txtShowItems" runat="server" NAME="txtShowItems"><INPUT id="txtTreeType" type="hidden" name="txtTreeType" runat="server">
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var TreeType = Form1.txtTreeType.value; 
var ParentCode = '<%=Request["ParentCode"]%>';
var DataSourceUrl="DocumentTypeData.aspx?Fixed=0";

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
	if ( obj != null )
	{
		if ( HasString (showItemString,SItems[i]  ) )
			obj.style.display = "";
		else
			obj.style.display = "none";
	}
}

var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@DocumentTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"ShowDocumentType('@DocumentTypeCode');return false;\">@TypeName</a></td></tr></table>";


TreeModels.push(v0);
//����
//TreeModels.push("<a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"AddDocumentTypeChild('@DocumentTypeCode');return false;\">����ӽڵ�</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"DeleteDocumentType('@DocumentTypeCode');return false;\">ɾ��</a>");


//�ڵ�
function SpreadNodes(DocumentTypeCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+DocumentTypeCode,obj,TreeModels,"DocumentTypeCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId=" + ParentCode,null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);

		</Script>
	</body>
</HTML>