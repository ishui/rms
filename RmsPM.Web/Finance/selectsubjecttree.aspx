<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SelectSubjectTree" CodeFile="SelectSubjectTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择科目</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Images/XmlTree.js"></script>
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Rms.js"></script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="0" border="0" width="100%" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td>科目编号：</td>
											<td><input type="text" name="txtSearchSubjectCode" class="input" id="txtSearchSubjectCode"
													runat="server"></td>
											<td><input type="button" class="submit" value="搜 索" name="btnSearch" id="btnSearch" onclick="btnSearchClick();"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table border="0" cellspacing="0" cellpadding="0" width="100%">
							<TR>
								<TD class="note"><asp:label id="LabelSubjectSetName" runat="server" CssClass="TitleText"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table cellSpacing="0" rules="rows" cellPadding="0" border="0" width="100%" class="tree">
								<thead>
									<tr class="tree-title">
										<td width="160">编号</td>
										<td>名称</td>
									</tr>
								</thead>
								<tbody id="Tree">
								</tbody>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table height="100%" cellSpacing="0" cellPadding="10" width="100%" border="0">
							<tr align="center">
								<td><input type="button" name="btnClose" class="submit" value="取 消" onclick="window.close();"></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<input type="hidden" id="txtSubjectCode" name="txtSubjectCode" runat="server"> <input type="hidden" id="txtAct" name="txtAct" runat="server">
			<input type="hidden" name="txtSubjectSetCode" id="txtSubjectSetCode" runat="server"><input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server">
			<input type="hidden" name="txtReturnFunc" id="txtReturnFunc" runat="server"> <input type="hidden" name="txtDefine1" id="txtDefine1" runat="server">
		</form>
		<Script language="javascript">
		
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="SubjectData.aspx?SubjectSetCode=" + Form1.txtSubjectSetCode.value ;

//是否可任意选择
var SelectAllLeaf = '<%=Request["SelectAllLeaf"]%>';

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

//TreeModels[1]="<a href=\"#\" id=\"@SubjectCode\" onclick=\"Select(this);return false;\" FullName=\"@SubjectFullName\">@SubjectName</a>";

if (SelectAllLeaf == "1")
    TreeModels[1]="<a href=\"#\" id=\"@SubjectCode\" onclick=\"Select(this);return false;\" FullName=\"@SubjectFullName\">@SubjectName</a>";
else
    TreeModels[1]="<a href=\"#\" style=\"display:@DisplayHref\" id=\"@SubjectCode\" onclick=\"Select(this);return false;\" FullName=\"@SubjectFullName\">@SubjectName</a><span style=\"display:@DisplaySpan\">@SubjectName</span>";


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

var SubjectCode = Form1.txtSearchSubjectCode.value;
GetChildNodes(DataSourceUrl+"&RootId=" + SubjectCode,null,TreeModels,"SubjectCode",RowClassName,GridClassName);

//选择科目
	function Select(obj)
	{
		var name = obj.FullName;
//		var name = obj.innerText;
		
//		alert(name);
//		name = name.substr(obj.NodeLayer + 1, name.length - obj.NodeLayer);
//		alert(name);

		window.opener.<%=ReturnFunc%>(obj.id, name, Form1.txtDefine1.value);
		window.close();
	}
	
	function btnSearchClick1()
	{
		var val = Form1.txtSearchSubjectCode.value;

		var tree = document.all.Tree;
		var s;
		var id;
		var obj;
		var NodeLayer;
		var NodeType;
		var parentNode;
		var parentCode;
		
		if (val == "")
			return;
		
		node = tree.childNodes[0];

		//从下一节点开始查找
		while ((node != undefined) && (node != null))
		{
			NodeLayer = node.NodeLayer;
			
			s = node.innerText;
			s = s.substr(node.NodeLayer + 1, s.length - node.NodeLayer);

			if (s == val)
//            if (s.indexOf(val) >= 0)
			{
				parentCode = GetParentNodeId(node);
				
				id = node.NodeId;
				var obj = document.all(id);
				NodeType = GetNodeType(id);
				
				//展开节点
				SpreadNodes(node.NodeId, node.NodeLayer, node);
			
//				TDClick(obj);

//				alert("找到了");
				return;
			}
			
			node = node.nextSibling;
		}
		
		alert("未找到");
	}
		
	function btnSearchClick()
	{
		if (document.all.Tree.childNodes.length > 0)
		{
			RemoveAllChildNode(document.all.Tree);
		}
		
		var SubjectCode = Form1.txtSearchSubjectCode.value;
		GetChildNodes(DataSourceUrl+"&RootId=" + SubjectCode,null,TreeModels,"SubjectCode",RowClassName,GridClassName);
	}

		</Script>
	</body>
</HTML>
