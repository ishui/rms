<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.BuildingTree" CodeFile="BuildingTree.ascx.cs" %>
<TABLE id="Table3" borderColor="#e7e7e7" class="tree" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
	border="0">
	<thead>
		<TR class="tree-title">
			<TD noWrap align="left">¥������</TD>
			<TD noWrap align="left">��Ʒ����</TD>
			<TD noWrap align="right">����</TD>
			<TD noWrap align="right">�ƻ��������(ƽ��)</TD>
			<TD noWrap align="right">ʵ�����(ƽ��)</TD>
			<!--TD noWrap align="left">������λ����</TD-->
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
<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
<input type="hidden" id="txtBuildingCode" name="txtBuildingCode" runat="server">
<script language="javascript">

//�鿴����¥��
function View(code){
	window.location.href = "PBSBuildInfo.aspx?BuildingCode="+code + "&FromUrl=" + escape(CurrUrl);
}

//�鿴��λ����
function GotoPBSUnitInfo(code)
{
	OpenCustomWindow("../PBS/PBSUnitInfo.aspx?action=view&FromUrl=" + escape(window.location) + "&PBSUnitCode=" + code + "&ProjectCode=" + Form1.txtProjectCode.value, "��λ����", 700, 500);
}

</script>
<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var ProjectCode = document.all(ClientID + "_txtProjectCode").value;
var BuildingCode = document.all(ClientID + "_txtBuildingCode").value;

var DataSourceUrl="PBSBuildingData.aspx?ProjectCode=" + ProjectCode + "&BuildingCode=" + BuildingCode;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[0]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";
TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a style=\"display=@ShowHref\" href=\"#\" id=\"@BuildingCode\" onclick=\"javascript:View('@BuildingCode');\">@BuildingName</a><span style=\"display=@ShowSpan\">@BuildingName</span></td></tr></table>";
//TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@BuildingCode\" onclick=\"TDClick(this, '@BuildingCode');return false;\">@BuildingName</a></td></tr></table>";
//TreeModels[1]="<a href=\"#\" id=\"@BuildingCode\" onclick=\"InsertChildPBSBuild('@BuildingCode','Building');return false;\">����¥��</a>";
TreeModels[1]="@PBSTypeName";
TreeModels[2]="<div align=right>@FloorCount</div>";
TreeModels[3]='<div align=right>@HouseArea</div>';
TreeModels[4]="<div align=right>@RoomArea</div>";
//TreeModels[5]="<a href=\"#\" onclick=\"javascript:GotoPBSUnitInfo('@PBSUnitCode');\">@PBSUnitName</a>";

//�ڵ�
function SpreadNodes(BuildingCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+BuildingCode,obj,TreeModels,"BuildingCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+BuildingCode+"&ShowSum=1",null,TreeModels,"BuildingCode",RowClassName,GridClassName);

//�ϼ���
if (document.all.Tree.childNodes.length > 0)
{
	var node = document.all.Tree.childNodes[document.all.Tree.childNodes.length - 1];
	for(var i=0;i<node.childNodes.length;i++)
	{
		node.childNodes[i].className = "tree-sum";
	}
}

</Script>
