<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSTypeTree" CodeFile="PBSTypeTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBSTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">

	var clicktd;
	var clickid;
	
	function TDClick(obj1, code) {
		if (clicktd != undefined) {
			clicktd.className='list-i';
		}

		var node = FindNode(document.all("Tree"), code);
		clickid = code;
		
//		var node = document.all("table" + WBSCode);
//		node.style.backgroundColor="#ffffff";
		node.className = "list-2";

		clicktd = node;
		
		document.all.tdAddChild.style.display = "block";
		document.all.tdModify.style.display = "block";
	}

	function ShowPBSType(WBSCode){
//		alert(obj);
//		window.parent.frames.ShowPBSType(WBSCode);
	}

	//����
	function Insert(ParentCode){
		var w = 400;
		var h = 300;
		window.open("PBSTypeModify.aspx?Action=Insert&ParentCode="+ParentCode, "��Ʒ����޸�" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
	}

	//�޸�
	function Modify(code){
		var w = 400;
		var h = 300;
		window.open("PBSTypeModify.aspx?Action=Modify&PBSTypeCode="+code, "��Ʒ����޸�" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
	}
		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td width="16"><IMG src="../images/btn_li.gif"></td>
								<td><input class="button" type="button" value="�� ��" name="btnAdd" id="btnAdd" runat="server"
										onclick="Insert('');">
								</td>
								<td id="tdAddChild" style="DISPLAY:none"><input class="button" type="button" value="��������" name="btnAddChild" id="btnAddChild" runat="server"
										onclick="Insert(clickid);">
								</td>
								<td id="tdModify" style="DISPLAY:none"><input class="button" type="button" value="�� ��" name="btnModify" id="btnModify" runat="server"
										onclick="Modify(clickid);">
								</td>
								<td width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE class="list" id="Table3" cellSpacing="0" cellPadding="0" rules="rows" width="100%">
								<thead>
									<TR class="list-title">
										<TD noWrap align="left" width="100%">��Ʒ����</TD>
										<!--
									<TD noWrap align="right" width="20%">����</TD>
									<TD noWrap align="right" width="20%">���</TD>-->
									</TR>
								</thead>
								<tbody id="Tree">
								</tbody>
								<tfoot>
									<TR class="list-title" style="DISPLAY:none">
										<TD noWrap align="center" colspan="3">&nbsp;</TD>
									</TR>
								</tfoot>
							</TABLE>
						</div>
					</td>
				</tr>
			</TABLE>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtAct" name="txtAct" runat="server">
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="list-i";
var GridClassName="";//"TreeViewItemTd";

var DataSourceUrl="PBSTypeData.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&Act=building";

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" width=\"100%\" id=\"table@PBSTypeCode\">"
TreeModels[0]+="<tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@PBSTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";
TreeModels[0]+="<td><a href=\"#\" id=\"@PBSTypeCode\" onclick=\"TDClick(this, '@PBSTypeCode');ShowPBSType('@PBSTypeCode');return false;\">@PBSTypeName</a></td>";
TreeModels[0]+="</tr></table>";
//TreeModels[1]="<div align=\"right\">@HouseCount</div>";
//TreeModels[2]="<div align=\"right\">@HouseArea</div>";
//TreeModels[3]="<a href=\"#\" id=\"@PBSTypeCode\" onclick=\"InsertChildPBSType('@PBSTypeCode');return false;\">����</a>";

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

//ȫ��չ��
function expandAll() {
	var tree = document.all.Tree;
	var i;
	var node;
	var id;
	var layer;
	var type;

//alert(tree.childNodes[3].nextSibling);
//alert(tree.childNodes[1].NodeId);
//return;  

	node = tree.childNodes[0];
	while ((node != undefined) && (node != null))
	{
		expandNode(node);
		node = node.nextSibling;
	}
}

//չ��ĳ���ڵ�
function expandNode(node) {
	var id, layer, type;
	
	if ((node == undefined) || (node == null))
		return;

	if (node.NodeStatus == "Closed")
	{
		id = node.NodeId;
		layer = node.NodeLayer;
		
		SpreadNodes(id, layer, node);
	}
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"PBSTypeCode",RowClassName,GridClassName);
expandAll();

		</Script>
	</body>
</HTML>
