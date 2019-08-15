<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectSystemGroup" CodeFile="SelectSystemGroup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function selectGroup(groupCode,groupName){
				var flag = '<%=Request["Flag"]%>';
				window.opener.<%=ViewState["ReturnFunc"]%>(groupCode,groupName,flag);
				window.close();
			}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ��
						<asp:Label id="lblClassName" runat="server" BackColor="Transparent"></asp:Label>����</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center" height="95%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
							width="98%">
							<thead>
								<TR class="tree" id="tree-title">
									<TD id="tdTypeName" noWrap align="left">��������</TD>
								</TR>
							</thead>
							<tbody id="Tree">
							</tbody>
							<tfoot>
								<TR class="tree-title">
									<TD></TD>
								</TR>
							</tfoot>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
		<SCRIPT language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

//var DataSourceUrl="../SelectBox/SystemGroupData.aspx?ClassCode=<%=Request["ClassCode"]%>"+"&ProjectCode=<%=ProjectCode%>" ;
var DataSourceUrl="SystemGroupData.aspx?ClassCode=<%=Request["ClassCode"]%>"+"&ProjectCode=<%=ProjectCode%>" ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��


var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td id='td@GroupCode'onclick=\"SpreadNodes('@GroupCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";

//�Ƿ������ѡ����
var SelectAllLeaf = '<%=Request["SelectAllLeaf"]%>';

if (SelectAllLeaf == "1")
	v0+="<td><a href=\"#\" onclick=\"selectGroup('@GroupCode','@GroupName')\">@SortID @GroupName</a></td></tr></table>";
else
                v0 += "<td><a style=\"display:@DisplayHref\" href=\"#\" onclick=\"selectGroup('@GroupCode','@GroupName')\">@SortID @GroupName</a><span style=\"display:@DisplaySpan\">@SortID @GroupName</span></td></tr></table>";
            
TreeModels.push(v0);
console.log("v0:" + v0)
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

//չ�������
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}
		</SCRIPT>
	</body>
</HTML>
