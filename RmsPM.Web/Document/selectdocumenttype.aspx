<%@ Page language="c#" Inherits="RmsPM.Web.Document.SelectDocumentType" CodeFile="SelectDocumentType.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择文档类型</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<style> .tree-tr2 { border-bottom: 1px solid #D3DFE8; border-left: 1px solid #EAEFF4; padding-left: 5px; padding-right: 5px; height: 24px; background-color: #E5F0CE; }
		</style>
		<SCRIPT language="javascript">

	var chkArray = new Array();
	var chkArrayName = new Array();
	
	function ChangeCheck( chk )
	{
		var v = chk.value;
		var vname = chk.title;
		
		if ( chk.checked )
		{
			if ( ! IsContain(chkArray,v))
			{
				chkArray.push(v);
				chkArrayName.push(vname);
			}
		}
		else
		{
			if ( IsContain(chkArray,v))
			{
				chkArray = RemoveItem ( chkArray,v);
				chkArrayName = RemoveItem ( chkArrayName,vname);
			}
		}

		HightlightRow(chk);
	}

	function HightlightRow(chk)
	{
		//高亮选中该行		
		var node = FindNode(document.all.Tree, chk.value);
		if (chk.checked)
		{
			node.className = "tree-tr2";
			SetNodeGridClass(node, "tree-tr2");
		}
		else
		{
			node.className = "tree-tr";
			SetNodeGridClass(node, "tree-tr");
		}
	}
	
	function IsContain( ar , code )
	{
		var iCount = ar.length;
		for ( var i=0; i<iCount; i++)
		{
			if ( ar[i] == code )
				return true;
		}
		return false;
	}
	
	function RemoveItem(ar,code)
	{
		var tempArray = new Array();
		var iCount = ar.length;
		for ( var i=0; i<iCount; i++)
		{
			if ( ar[i] != code )
				tempArray.push(ar[i]);
		}
		return tempArray;
	}
	
	function GetSelect(chk)
	{
		var v = chk.join(',');
		return v;
	}
	
	function SaveSelect()
	{
		Form1.txtOutputCode.value = GetSelect(chkArray);
		Form1.txtOutputName.value = GetSelect(chkArrayName);
		Form1.btnSave.click();
	}
	
	function ShowCheckBox()
	{
		var chks = document.all("chk");
		
		if (!chks) return;
		
		//数组
		if ( chks[0])
		{
			var iCount = chks.length;
			for ( var i=0; i<iCount; i++)
			{
				if ( IsContain(chkArray,chks[i].value))
				{
					chks[i].checked=true;
					chkArrayName.push(chks[i].title);
					HightlightRow(chks[i]);
				}
			}
		}
		//只有一个
		else
		{
			if ( IsContain(chkArray,chks.value))
			{
				chks.checked = true;
				chkArrayName.push(chks.title);
				HightlightRow(chks);
			}
		}
	}
	
	function LoadSelect()
	{
		var codes = Form1.txtInputCode.value;
		
		if (codes != "")
		{
			var code = codes.split(',');
			var iCount = code.length;
			
//			var names = Form1.txtInputName.value;
//			var name = names.split(',');

			for ( var i=0;i<iCount; i++)
			{
				chkArray.push(code[i]);
				
//				if (name.length > i)
//					chkArrayName.push(name[i]);
			}
		}

		ShowCheckBox();
	}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="LoadSelect();" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr vAlign="top" height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<TABLE id="Table3" class="tree" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
								border="0">
								<thead>
									<TR class="tree-title">
										<TD noWrap align="left">文档类型</TD>
										<TD noWrap align="center" width="40"></TD>
									</TR>
								</thead>
								<tbody id="Tree" class="tree">
								</tbody>
								<tfoot>
									<TR class="tree-title" style="DISPLAY:none">
										<TD noWrap align="center">&nbsp;</TD>
										<TD noWrap align="center"></TD>
									</TR>
								</tfoot>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="10" width="100%" border="0">
							<TR>
								<TD align="center"><input id="btnOK" onclick="SaveSelect();" type="button" value="确定" class="submit">
								<input id="btnCancel" onclick="window.close();" type="button" value="取消" class="submit">
									<input id="btnSave" style="DISPLAY: none" type="button" value="保存" runat="server" class="submit" onserverclick="btnSave_ServerClick">
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<input id="txtInputCode" type="hidden" runat="server"><input id="txtOutputCode" type="hidden" runat="server">&nbsp;
			<INPUT id="txtOutputName" type="hidden" name="txtOutputName" runat="server"><INPUT id="txtInputName" type="hidden" name="txtInputName" runat="server"></form>
		<SCRIPT language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="DocumentTypeData.aspx?Fixed=0";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@DocumentTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";
TreeModels[0]+="<td onclick=\"SpreadNodes('@DocumentTypeCode','@Layer',this);\" style=\"cursor:hand\">@TypeName</td></tr></table>";
//TreeModels[0]+="<td><a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"SpreadNodes('@DocumentTypeCode','@Layer',this);\">@TypeName</a></td></tr></table>";
TreeModels[1]="<input type=checkbox value=@DocumentTypeCode title=@TypeName id=chk name=chk onclick=\"ChangeCheck(this);  \" >";

//节点
function SpreadNodes(DocumentTypeCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+DocumentTypeCode,obj,TreeModels,"DocumentTypeCode");
	ShowCheckBox();
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);

		</SCRIPT>
	</body>
</HTML>
