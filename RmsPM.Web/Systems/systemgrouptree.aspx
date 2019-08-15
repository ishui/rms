<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SystemGroupTree" CodeFile="SystemGroupTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>

		<style>.LeftMenuItem { FONT-SIZE: 12px; MARGIN: 1px; COLOR: #00309c }
	.LeftMenuItemOnMouseOver { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #fffbff }
	.LeftMenuItemOnActivty { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #ffe794 }

		</style>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<TR style="DISPLAY:none">
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
					</TD>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE id="Table3" borderColor="#e7e7e7" class="" cellSpacing="0" cellPadding="3" rules="rows"
								width="100%" border="0">
								<thead>
									<TR class="tree-title" style="display:none">
										<TD noWrap align="left">�������</TD>
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
						</div>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" value="0">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
		</form>
		<SCRIPT language="javascript">
	var clicktd;
	var clickNodeID;
	var clickNode;
	var CurrCode;
	var CurrItemType;
	var srcNode;
	var srcNodeID = "";
	
	function TDClick(obj, NodeID) {
		clickNodeID = NodeID;
		
		if (clicktd != undefined) {
			clicktd.className='';
		}
		obj.className='LeftMenuItemOnMouseOver';
		clicktd = obj;
		
		clickNode = FindNode(document.all.Tree, clickNodeID);
	}
	
	function ShowInfo(code, ItemType, ClassCode)
	{
		CurrCode = code;
		CurrItemType = ItemType;
		
		var objFrame = window.parent.document.all("frameRight");
		
		if (ItemType == "C")
		{
			objFrame.src = "../Systems/SystemGroupClassInfo.aspx?ClassCode=" + code;
		}
		else if (ItemType == "P")
		{
			objFrame.src = "../Blank.htm";
		}
		else if (ItemType == "T")
		{
			objFrame.src = "../Systems/TaskAccessInfo.aspx?WBSCode=" + code;
		}
		else if (ItemType == "D")
		{
			objFrame.src =  "../Systems/SystemGroupInfo.aspx?GroupCode=" + code + "&ClassCode="+ClassCode;
		}
		else
		{
			objFrame.src = "../Systems/SystemGroupInfo.aspx?GroupCode=" + code + "&ClassCode="+ClassCode;
		}
	}	
	
	//ˢ���ӽ��
	function MyRefreshChild()
	{
		updateChildNodes(CurrCode, CurrItemType);
	}
	
	//ɾ��Դ���
	function MyDeleteSrc()
	{
//		var srcGroupCode = window.parent.document.all("txtSrcGroupCode").value;
//		var node = FindNode(document.all.Tree, srcGroupCode);
		
		if (srcNode)
		{
			RemoveNodeAndAllChild(srcNode);
		}
	}
	
	//ˢ�±����
	function MyRefreshNode()
	{
		updateNode(CurrCode, CurrItemType);

		var obj = document.all("td" + CurrCode);
		if (obj)
			obj.click();
	}

	//ɾ������ˢ�¸����
	function MyRefreshParent(ParentCode, ItemType)
	{
		updateChildNodes(ParentCode, ItemType);
		
		var obj = document.all("td" + ParentCode);
		if (obj)
			obj.click();
	}

	function SetNodeImage(NodeID, imgName)
	{
		if (NodeID == "") return;
		
		var img = document.all("img" + NodeID);
		if (img)
		{
			img.src = "../images/" + imgName;
		}
	}
	
	//���н��
	function CutNode()
	{
		srcNode = clickNode;
		
		SetNodeImage(srcNodeID, "Folder.gif");
		srcNodeID = clickNodeID;

		SetNodeImage(clickNodeID, "FolderCut.gif");
	}
	
	//���ƽ��
	function CopyNode()
	{
		srcNode = clickNode;
		
		SetNodeImage(srcNodeID, "Folder.gif");
		srcNodeID = clickNodeID;

		SetNodeImage(clickNodeID, "FolderCopy.gif");
	}
	
		</SCRIPT>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="TreeViewItemTd";
var GridClassName="TreeViewItemTd";

var FunctionStructureCode = '<%=Request["FunctionStructureCode"]%>';

var DataSourceUrl="SystemGroupData.aspx?FunctionStructureCode=" + FunctionStructureCode;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@GroupCode','@Layer','@ItemType','@ClassCode',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[0]+="<td onclick=\"SpreadNodes('@GroupCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";
TreeModels[0]+="<td style=\"CURSOR: hand\" id=\"td@GroupCode\" onclick=\"javascript:TDClick(this, '@GroupCode');ShowInfo('@GroupCode', '@ItemType', '@ClassCode');\"><img id=\"img@GroupCode\" src=\"../Images/@ImageFileName\"> @DisplayGroupName</td></tr></table>";
//TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@GroupCode\" onclick=\"TDClick(this, '@GroupCode');return false;\">@BuildingName</a></td></tr></table>";
//TreeModels[1]="<a href=\"#\" id=\"@GroupCode\" onclick=\"InsertChildPBSBuild('@GroupCode','Building');return false;\">����¥��</a>";

//�ڵ�
function SpreadNodes(code,layer,ItemType,ClassCode,obj){
	if (ItemType == "C")
	{
		//����
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfClass&NodeId=&ClassCode=" + ClassCode + "&NodeLayer=" + layer,obj,TreeModels,"GroupCode");
	}
	else if (ItemType == "P")
	{
		//��Ŀ
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId=&ProjectCode=" + code + "&ClassCode=" + ClassCode + "&NodeLayer=" + layer,obj,TreeModels,"GroupCode");
	}
	else
	{
		//���
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId=" + code + "&ClassCode=" + ClassCode + "&NodeLayer=" + layer,obj,TreeModels,"GroupCode");
	}
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
function updateNode(NodeId, ItemType){
	var node = FindNode(document.all.Tree, NodeId);
	var layer = node.NodeLayer;
	if(layer) layer = parseInt(layer)-1;

	if (ItemType == "C")
	{
		//����
		RefreshNode(DataSourceUrl+"&GetType=SingleNodeOfClass&NodeId="+NodeId + "&NodeLayer=" + layer,TreeObj,NodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
	else
	{
		//���
		RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId + "&NodeLayer=" + layer,TreeObj,NodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
}

//�����ӽڵ�
function updateChildNodes(parentNodeId, ItemType){
	var node = FindNode(document.all.Tree, parentNodeId);
	var layer = node.NodeLayer;
	if(layer) layer = parseInt(layer)-1;

	if (ItemType == "C")
	{
		//����
		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodesOfClass&NodeId=&ClassCode="+parentNodeId + "&NodeLayer=" + layer,TreeObj,parentNodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
	else
	{
		//���
		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId + "&NodeLayer=" + layer,TreeObj,parentNodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
}

GetChildNodes(DataSourceUrl+"&GetType=AllClass&NodeLayer=0",null,TreeModels,"GroupCode",RowClassName,GridClassName);
		</Script>
	</body>
</HTML>
