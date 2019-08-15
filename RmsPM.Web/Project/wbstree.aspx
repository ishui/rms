<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSTree" CodeFile="WBSTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBSTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language=javascript src="../Images/LockTable.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript">
	function ShowWBS(WBSCode){
		window.parent.frames.ShowWBS(WBSCode);		
	}
	
	function AddNewTask(WBSCode)
	{
		OpenMiddleWindow( 'WBSModify.aspx?Action=Insert&ProjectCode=<%=Request["ProjectCode"]%>&ParentCode=' + WBSCode,"" );
	}
	
	function Select(Flag1,Code)
	{
		window.parent.TreeSplitTop.updateChildNodes(Code);
	}
	
//�����˵�ѡ�񼶱�
function ShowMenuLevel()
{
	var cssFile="../Images/ContentMenu.css";

	var Items = new Array();

	Items[0] = new Array(3);
	Items[0][0] = "��������";
	Items[0][1] = "";
	Items[0][2] = "LevelChange(-1);";

	for(var i=1;i<=9;i++)
	{
		Items[i] = new Array(3);
		Items[i][0] = i + "��";
		Items[i][1] = "";
		Items[i][2] = "LevelChange(" + i + ");";
	}
	
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

//���뿪ʼ
function HintLoadStart()
{
	document.all.divMain.style.display = "none";
	document.all.divHintLoad.style.display = "";
}

//�������
function HintLoadEnd()
{
	document.all.divHintLoad.style.display = "none";
	document.all.divMain.style.display = "";
}

//�Զ�չ����ָ������
function LevelChange(level)
{
	HintLoadStart();
	
	window.setTimeout("AutoLevelChange('" + level + "');", 1);
}

	// <img src="../images/icon_showimportant.gif">
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onload="LockTable(Table3,0,1,0);" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
			<tr><td>
			<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" class="tree" border="0">
				<thead>
					<TR class="tree-title" width="100%">
						
						<TD noWrap id="tdTaskName" style="DISPLAY: block;COLOR: #000000;BORDER-BOTTOM: #515e69 1px solid;HEIGHT: 23px"
							bgcolor="#d7e9f8" valign="middle">&nbsp;&nbsp;<img style="cursor:hand" title="չ����ָ������" src="../images/icon_showstate.gif" onclick="ShowMenuLevel();return false;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����������</TD>
						<TD noWrap id="tdMaster" style="DISPLAY: block" class="list-a">������</TD>
						<TD noWrap id="tdCompletePercent" style="DISPLAY: block" class="list-a">��ɽ���</TD>
						<TD noWrap id="tdPlannedStartDate" style="DISPLAY: block" class="list-a">�ƻ���ʼʱ��</TD>
						<TD noWrap id="tdPlannedFinishDate" style="DISPLAY: block" class="list-a">�ƻ�����ʱ��</TD>
						<TD noWrap id="tdAcutalStartDate" style="DISPLAY: block" class="list-a">ʵ�ʿ�ʼʱ��</TD>
						<TD noWrap id="tdActualFinishDate" style="DISPLAY: block" class="list-a">ʵ�ʽ���ʱ��</TD>
						<TD noWrap id="tdLastModifyDate" style="DISPLAY: block" class="list-a">����޸�ʱ��</TD>
					</TR>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
			</div>
			</td>
			</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input type="hidden" id="txtShowItems" runat="server" name="txtShowItems"> <input type="hidden" id="txtTreeType" runat="server" name="txtTreeType">
		</form>
		<Script language="javascript">


//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";


var DataSourceUrl="WBSData.aspx?" + "<%=Request.QueryString%>";
//alert(DataSourceUrl);
// @IndentStart			��������ѭ����ʼ��<font color=white>
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" width=100%><tr @ColorSymbolStart|bgcolor=\"#FFcc33\" @ColorSymbolEnd >"; //@ColorSymbolStartbgcolor=\"\"|@ColorSymbolEnd
TreeModels[0]+="<td width=\"30\" align=\"right\" >&nbsp;@ImageSymbolStart<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\">|<img src=\"../Images/icon_going.gif\" title=\"�����й���\">|<img src=\"../Images/icon_pause.gif\" title=\"����ͣ����\">|<img src=\"../Images/icon_cancel.gif\" title=\"��ȡ������\">|<img src=\"../Images/icon_over.gif\" title=\"����ɹ���\">@ImageSymbolEnd&nbsp;&nbsp;</td>";
TreeModels[0]+="@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd<td id=\"WBSCode\" onclick=\"SpreadNodes('@WBSCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[0]+="<td width=\"15\" align=\"center\" >@ImportantSymbolStart|<img src=\"../Images/icon_important.gif\">@ImportantSymbolEnd</td>";
TreeModels[0]+="<td classname=\"@Flag\">@myRightSymbolStart|<a href=\"#\" id=\"@WBSCode\"  onclick=\"ShowWBS(@WBSCode);return false;\">@myRightSymbolEnd@TaskName</a>@ImportantSymbolStart|<img src=\"../Images/icon_important.gif\">@ImportantSymbolEnd";
//TreeModels[0]+="<td classname=\"@Flag\">@IsRightSymbolStart@TaskName|<a href=\"#\" id=\"@WBSCode\"  onclick=\"ShowWBS(@WBSCode);return false;\">@TaskName</a>@IsRightSymbolEnd@ImportantSymbolStart|<img src=\"../Images/icon_important.gif\">@ImportantSymbolEnd";
//TreeModels[0]+="&nbsp;&nbsp;<a href=\"#\" onclick=\"window.parent.InsertWBS(@WBSCode);return false;\"><Font color=red>��</font></a></td>";@ColorSymbolStart<font color=\"red\">|<font color=\"\">@ColorSymbolEnd</font>

TreeModels[1]="@Master"
TreeModels[1]+="&nbsp;</td>"

TreeModels[2]="@CompletePercent"
TreeModels[2]+="%&nbsp;</td>"

TreeModels[3]="@WorkStartDate"
TreeModels[3]+="&nbsp;</td>"

TreeModels[4]="@WorkEndDate"
TreeModels[4]+="&nbsp;</td>"

TreeModels[5]="@WorkActualStartDate"
TreeModels[5]+="&nbsp;</td>"

TreeModels[6]="@WorkActualFinishDate" 
TreeModels[6]+="&nbsp;</td><tr><table>"

TreeModels[7]="@LastModifyDate" 
TreeModels[7]+="&nbsp;</td><tr><table>"

//�ڵ�
function SpreadNodes(WBSCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+WBSCode,obj,TreeModels,"WBSCode");
	LockTable(Table3,0,1,0);
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"WBSCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"WBSCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"WBSCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"WBSCode",RowClassName,GridClassName);
}

var RootCode = '<%=Request["ParentCode"]%>';

if (RootCode != "") //ȡָ�����
{
	GetChildNodes(DataSourceUrl+"&GetType=SingleNode&NodeId="+RootCode,null,TreeModels,"WBSCode",RowClassName,GridClassName);
}
else //ȡ��1�����
{
	GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+RootCode,null,TreeModels,"WBSCode",RowClassName,GridClassName);
}

	if (document.all.Tree.childNodes.length == 1)
	{
		SpreadNodes(document.all.Tree.childNodes[0].NodeId, document.all.Tree.childNodes[0].NodeLayer, document.all.Tree.childNodes[0]);
	}

//չ����ָ������
function AutoLevelChange(level)
{
	if (level < 0)
	{
		//չ��ȫ��
		ExpandToLayer(TreeObj, 999, DataSourceUrl, TreeModels, "WBSCode");
	}
	else
	{
		//�۵���ָ������
		CollapseToLayer(TreeObj, level);

		//չ����ָ������
		ExpandToLayer(TreeObj, level, DataSourceUrl, TreeModels, "WBSCode");
	}
	
	HintLoadEnd();
}


		</Script>
		</TABLE></ENTITY>
	</body>
</HTML>
