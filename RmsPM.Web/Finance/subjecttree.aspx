<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectTree" CodeFile="SubjectTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubjectTree</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Images/XmlTree.js"></script>
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/ContentMenu.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<Script language="javascript">
	function InsertSubject(parentSubjectCode){
		var subjectSetCode = Form1.txtSubjectSetCode.value;
		OpenSmallWindow('SubjectModify.aspx?Action=Insert&SubjectSetCode='+subjectSetCode+'&ParentSubjectCode='+parentSubjectCode);
	}
	function ModifySubject(subjectCode){
		var subjectSetCode = Form1.txtSubjectSetCode.value;
		OpenSmallWindow('SubjectModify.aspx?Action=Modify&SubjectSetCode='+subjectSetCode+'&SubjectCode='+subjectCode);
	}
	function RemoveSubject(subjectCode){
		var subjectSetCode = Form1.txtSubjectSetCode.value;
		if(window.confirm("您确定要删除这个科目吗？")){
			OpenSmallWindow('SubjectModify.aspx?Action=Remove&SubjectSetCode='+subjectSetCode+'&SubjectCode='+subjectCode);
		}
	}

	function Import()
	{
		var subjectSetCode = Form1.txtSubjectSetCode.value;
		OpenCustomWindow('ImportSubjectDlg.aspx?SubjectSetCode=' + subjectSetCode,"导入科目", 400, 300);
	}
	
	function Export()
	{
		var subjectSetCode = Form1.txtSubjectSetCode.value;
		OpenCustomWindow('ExportSubjectDlg.aspx?SubjectSetCode=' + subjectSetCode,"导出科目", 400, 300);
	
	}
		</Script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">财务管理 
										- 科目管理</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAdd" onclick="InsertSubject('');" type="button" value="新增科目"
							name="btnAdd" runat="server">
						<input class="button" id="btnImport" onclick="Import();" type="button" value="导入科目" name="btnImport"
							runat="server">
						<input class="button" id="btnExport" onclick="Export();" type="button" value="导出科目" name="btnExport"
							runat="server">
					</TD>
				</TR>
				<tr>
					<td vAlign="top" class="table">
						<table border="0" cellspacing="0" cellpadding="0" width="100%">
							<TR>
								<TD class="note"><asp:label id="LabelSubjectSetName" runat="server" CssClass="TitleText"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<TD vAlign="top" class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table cellSpacing="0" cellPadding="0" border="0" width="100%" class="tree">
								<thead>
									<tr class="tree-title">
										<td width="160">编号</td>
										<td>名称</td>
										<td align="center" width="80">是否借方科目</td>
										<td align="center" width="80">是否贷方科目</td>
									</tr>
								</thead>
								<tbody id="Tree">
								</tbody>
							</table>
						</div>
					</TD>
				</TR>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
			<input type="hidden" name="txtSubjectSetCode" id="txtSubjectSetCode" runat="server">
			<input type="hidden"  id="txtSelfAccount" runat="server">
		</form>
		<Script language="javascript">
/*
//ContentMenu
function ShowEditMenu(obj){
	var cssFile="../Images/ContentMenu.css";		
	var Items=new Array();
	Items[0]=new Array(2);
	Items[0][0]="修改科目";
	Items[0][1]="";
	Items[0][2]="ModifySubject('"+obj.id+"');";
	Items[1]=new Array(2);
	Items[1][0]="删除科目";
	Items[1][1]="";
	Items[1][2]="RemoveSubject('"+obj.id+"');";
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}
*/

//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl='SubjectData.aspx?SubjectSetCode=' + Form1.txtSubjectSetCode.value ;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点
var TreeModels=new Array();

TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@SubjectCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
TreeModels[0]+="<td>@SubjectCode</td></tr></table>";

//if ( Form1.txtSelfAccount.value == "1" )
	TreeModels[1]="<a href=\"#\" id=\"@SubjectCode\" onclick=\"ModifySubject(@SubjectCode);return false;\">@SubjectName</a>";
// else
//	TreeModels[1]="@SubjectName";
	
TreeModels[2]="<div align=\"center\">@JsCodeStart (parseInt(@IsDebit)==1)?\"是\":\"否\" @JsCodeEnd</div>";
TreeModels[3]="<div align=\"center\">@JsCodeStart (parseInt(@IsCrebit)==1)?\"是\":\"否\" @JsCodeEnd</div>";

//节点
function SpreadNodes(NodeId,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&Layer="+(parseInt(LayerNumber)+1)+"&GetType=ChildNodes&NodeId="+NodeId,obj,TreeModels,"SubjectCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"SubjectCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"SubjectCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"SubjectCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"SubjectCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl,null,TreeModels,"SubjectCode",RowClassName,GridClassName);
		</Script>
	</body>
</HTML>
