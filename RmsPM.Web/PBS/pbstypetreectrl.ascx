<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.PBSTypeTreeCtrl" CodeFile="PBSTypeTreeCtrl.ascx.cs" %>
<TABLE id="Table3" borderColor="#e7e7e7" class="tree" cellSpacing="0" cellPadding="3" rules="rows"
	width="100%" border="0">
	<thead>
		<TR class="tree-title">
			<TD noWrap align="left">��Ʒ�������</TD>
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
<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtPBSTypeCode" name="txtPBSTypeCode" runat="server">
<input type="hidden" id="txtShowType" name="txtShowType" runat="server">
<script language="javascript">
	//�鿴
	function View(code){
		window.location.href = "PBSTypeInfo.aspx?PBSTypeCode="+code + "&FromUrl=" + escape(window.location.href);
	}

</script>
<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var ProjectCode = document.all(ClientID + "_txtProjectCode").value;
var PBSTypeCode = document.all(ClientID + "_txtPBSTypeCode").value;
var ShowType = document.all(ClientID + "_txtShowType").value;

var DataSourceUrl="PBSTypeData.aspx?ProjectCode=" + ProjectCode + "&PBSTypeCode=" + PBSTypeCode;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@PBSTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[0]+="<td onclick=\"SpreadNodes('@PBSTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";
TreeModels[0]+="<td><a href=\"#\" id=\"@PBSTypeCode\" onclick=\"javascript:View('@PBSTypeCode');\">@PBSTypeName</a></td></tr></table>";
//TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@PBSTypeCode\" onclick=\"TDClick(this, '@PBSTypeCode');return false;\">@BuildingName</a></td></tr></table>";
//TreeModels[1]="<a href=\"#\" id=\"@PBSTypeCode\" onclick=\"InsertChildPBSBuild('@PBSTypeCode','Building');return false;\">����¥��</a>";

//�ڵ�
function SpreadNodes(PBSTypeCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+PBSTypeCode,obj,TreeModels,"PBSTypeCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
}

if (ShowType == "all")
{
	GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+PBSTypeCode,null,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
	var l = TreeObj.childNodes.length;
	for(var i=l-1;i>=0;i--)
	{
		SpreadNodes(TreeObj.childNodes[i].NodeId, TreeObj.childNodes[i].NodeLayer, TreeObj.childNodes[i]);
	}
//	ShowAll();
}
else
{
	GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+PBSTypeCode,null,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
}
</Script>
