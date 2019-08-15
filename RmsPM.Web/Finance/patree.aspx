<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PATree" CodeFile="PATree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PATree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
		
	function selectBuildingNode(alloType,buildingCode,buildingName){
		if ( alloType == "P" )
		{}
		else if (alloType=="U")
		{
			buildingCode = buildingCode.replace( "PBSUnit","" );
		}
		else if ( alloType == "B" )
		{
			buildingCode = buildingCode.replace( "Building","" );
		}
		//alert(buildingCode);
		window.parent.frames.selectBuildingNode(alloType,buildingCode,buildingName);
	}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr height="100%">
					<td vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
								width="100%">
								<thead>
									<TR class="tree-title" id="trTitle">
										<TD noWrap align="left" id="tdBuildingName">����</TD>
									</TR>
								</thead>
								<tbody id="Tree">
								</tbody>
								<tfoot style="display:none">
									<TR class="sum">
										<TD noWrap id="tdFootBuildingName" class="sum-item"></TD>
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

var DataSourceUrl="PATreeData.aspx?ProjectCode=<%=Request["ProjectCode"]%>"  ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��



var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a href=\"#\" id=\"@BuildingCode\" onclick=\"selectBuildingNode('@alloType','@BuildingCode','@BuildingName');return false;\"> @BuildingName </a></td></tr></table>";

TreeModels.push(v0);
//��Ŀ��ź�����
//TreeModels.push("@BuidingName");

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
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"BuildingCode",RowClassName,GridClassName);

		</Script>
	</body>
</HTML>
