<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectBuildingByPBSUnit" CodeFile="SelectBuildingByPBSUnit.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择楼栋</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
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
	
	function SingleSelect(code, name)
	{
		Form1.txtOutputCode.value = code;
		Form1.txtOutputName.value = name;
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
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="LoadSelect();" rightMargin="0"
		scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr height="100%">
					<td valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<TABLE id="Table3" class="tree" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
								border="0">
								<thead>
									<TR class="tree-title">
										<TD noWrap align="left"><span runat="server" id="spanColName">楼栋</span></TD>
										<TD noWrap align="left"><span runat="server" id="spanPBSUnitName">所属单位工程</span></TD>
										<TD noWrap align="center" width="40"></TD>
									</TR>
								</thead>
								<tbody id="Tree" class="tree">
								</tbody>
								<tfoot>
									<TR class="tree-title" style="DISPLAY:none">
										<TD noWrap align="center">&nbsp;</TD>
										<TD noWrap align="center">&nbsp;</TD>
									</TR>
								</tfoot>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center"><input id="btnOK" onclick="SaveSelect();" type="button" value="确定" class="submit">
									<input id="btnCancel" onclick="window.parent.close();" type="button" value="取消" class="submit">
									<input id="btnSave" style="DISPLAY: none" type="button" value="保存" runat="server" class="submit"
										NAME="btnSave" onserverclick="btnSave_ServerClick">
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<input id="txtInputCode" type="hidden" runat="server"><input id="txtOutputCode" type="hidden" runat="server">&nbsp;
			<INPUT id="txtOutputName" type="hidden" name="txtOutputName" runat="server"><INPUT id="txtInputName" type="hidden" name="txtInputName" runat="server">
			<input type="hidden" id="txtReturnFunc" name="txtReturnFunc" runat="server"> <input type="hidden" id="txtCanSelectPBSUnit" name="txtCanSelectPBSUnit" runat="server">
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtPBSTypeCode" name="txtPBSTypeCode" runat="server">
			<input type="hidden" id="txtAct" name="txtAct" runat="server"><input type="hidden" id="txtAlloType" name="txtAlloType" runat="server">
			<input type="hidden" id="txtType" name="txtType" runat="server">
		</form>
		<SCRIPT language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="SelectBuildingDataByPBSUnit.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&act=" + Form1.txtAct.value;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点
var TreeModels=new Array();
var ModelIndex = 0;
TreeModels[ModelIndex]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[ModelIndex]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[ModelIndex]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";

var type = Form1.txtType.value.toLowerCase();

if (type == "single")
{
	TreeModels[ModelIndex]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@BuildingCode\" onclick=\"javascript:SingleSelect('@BuildingCode', '@BuildingName');\">@BuildingName</a></td></tr></table>";
}
else
{
	TreeModels[ModelIndex]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@BuildingCode\" onclick=\"javascript:OpenBuildingInfo('@BuildingCode');\">@BuildingName</a></td></tr></table>";
}

//TreeModels[0]+="<td><a href=\"#\" id=\"@BuildingCode\" onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\">@BuildingName</a></td></tr></table>";

if (Form1.txtAct.value == "pbsunit")
{
	Form1.txtCanSelectPBSUnit.value == "1";
}
if (Form1.txtAct.value == "building")
{
	Form1.txtCanSelectPBSUnit.value == "0";
	
	ModelIndex++;
	TreeModels[ModelIndex]="@PBSUnitName";
}

if (type != "single")
{
	ModelIndex++;
	
	//是否可选择单位工程
	if (Form1.txtCanSelectPBSUnit.value == "0")
	{
		TreeModels[ModelIndex]="<input type=checkbox value=@BuildingCode title=@BuildingName id=chk name=chk onclick=\"ChangeCheck(this);  \" style=\"display:@NoSelectPBSUnit\">";
	}
	else
	{
		TreeModels[ModelIndex]="<input type=checkbox value=@BuildingCode title=@BuildingName id=chk name=chk onclick=\"ChangeCheck(this);  \">";
	}
}

//节点
function SpreadNodes(BuildingCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+BuildingCode,obj,TreeModels,"BuildingCode");
	ShowCheckBox();
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

if (Form1.txtAct.value == "pbsunit")
{
	Form1.txtAlloType.value = "U";
	document.all.spanColName.innerText = "单位工程";
	document.all.spanPBSUnitName.style.display = "none";
	GetChildNodes(DataSourceUrl+"&GetType=onlypbsunit",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}
if (Form1.txtAct.value == "building")
{
	Form1.txtAlloType.value = "B";
	document.all.spanColName.innerText = "楼栋";
	GetChildNodes(DataSourceUrl+"&GetType=building",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//ExpandAll(document.all.Tree);
//ShowAll();

function ExpandNode(node) {
	var id, layer;
	
	if (!node)
		return;

	if (node.NodeStatus == "Closed")
	{
		id = node.NodeId;
		layer = node.NodeLayer;
		
		SpreadNodes(id, layer, node);
	}
}

function ExpandAll(tree) {
	var node;
	var id;

	if (!tree)
		return;

	node = tree.childNodes[0];
	while (node)
	{
		ExpandNode(node);
		node = node.nextSibling;
	}
}
		</SCRIPT>
	</body>
</HTML>
