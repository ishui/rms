<%@ Page language="c#" Inherits="RmsPM.Web.Project.PBS" CodeFile="PBS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<Script language="javascript" src="../Images/XmlTree.js"></Script>
	<script language="javascript" src="../Images/ContentMenu.js"></script>
	<script language="javascript" src="../Rms.js"></script>
	<Script language="javascript">
	function InsertPBS(PBSCode){
		OpenMiddleWindow("PBSModify.aspx?Action=Insert&PBSCode="+PBSCode,null);
	}
	function ModifyPBS(PBSCode){
		OpenMiddleWindow("PBSModify.aspx?Action=Modify&PBSCode="+PBSCode,null);
	}
	function RemovePBS(PBSCode){
		OpenMiddleWindow("PBSModify.aspx?Action=Remove&PBSCode="+PBSCode,null);
	}
	function DetailPBS(PBSCode){
	}
	</Script>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD colSpan="2" height="40"></TD>
				</TR>
				<TR>
					<TD bgColor="#ffd373" colSpan="2" height="1"><IMG src="../Images/spacer.gif"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="150" background="../Images/LeftBg.gif" bgColor="#e7efff"></TD>
					<TD vAlign="top" align="left">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE id="Table2" cellSpacing="10" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<TABLE id="Table5" cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR>
												<TD>
													<asp:label id="LabelTitle" runat="server" CssClass="TitleText"></asp:label></TD>
												<TD align="center"><FONT face="宋体"><a href="#" onclick="InsertPBS();return false;">新增</a></FONT></TD>
											</TR>
										</TABLE>
										<TABLE id="Table4" height="1" cellSpacing="0" cellPadding="0" width="100%" bgColor="#00309c"
											border="0">
											<TR>
												<TD><IMG src="../Images/spacer.gif"></TD>
											</TR>
										</TABLE>
										<TABLE id="Table3" borderColor="#e7e7e7" cellSpacing="0" cellPadding="3" width="100%" border="1">
											<thead>
												<TR>
													<TD noWrap align="center"><FONT face="宋体"></FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">占地面积</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">容积率</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">建筑面积</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">可售率</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">可售面积</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">产品比例</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">平均每户面积</FONT></TD>
													<TD noWrap align="center"><FONT face="宋体">总户数</FONT></TD>
												</TR>
											</thead>
											<tbody id="Tree">
											</tbody>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</div>
					</TD>
				</TR>
			</TABLE>
		</form>
		<Script language="javascript">
//ContentMenu
function ShowEditMenu(obj){
	var cssFile="../Images/ContentMenu.css";
	var Items=new Array();
	Items[0]=new Array(2);
	Items[0][0]="修改分解项";
	Items[0][1]="";
	Items[0][2]="ModifyPBS('"+obj.id+"');";
	Items[1]=new Array(2);
	Items[1][0]="删除分解项";
	Items[1][1]="";
	Items[1][2]="RemovePBS('"+obj.id+"');";
	Items[2]=new Array(2);
	Items[2][0]="拆分成本";
	Items[2][1]="";
	Items[2][2]="InsertPBS('"+obj.id+"');";
	Items[3]=new Array(2);
	Items[3][0]="分解项属性";
	Items[3][1]="";
	Items[3][2]="DetailPBS('"+obj.id+"');";
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

//Tree
var TreeObj=document.all("Tree");
var RowClassName="TreeViewItemTr";
var GridClassName="TreeViewItemTd";

var DataSourceUrl="PBSData.aspx";
// #Id					节点的唯一编号
// #LayerNumber			层次关键字
var DataSourceUrlParams=new Array();
DataSourceUrlParams[0]="PBSCode";
DataSourceUrlParams[1]="#Id";
DataSourceUrlParams[2]="Layer";
DataSourceUrlParams[3]="#LayerNumber";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@Id','@Deep',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
TreeModels[0]+="<td><a href=\"#\" id=\"@Id\" onclick=\"ShowEditMenu(this);return false;\">@Name</a></td></tr></table>";
TreeModels[1]="<div align=\"center\">@Col1</div>";
TreeModels[2]="<div align=\"center\">@Col2</div>";
TreeModels[3]="<div align=\"center\">@Col3</div>";
TreeModels[4]="<div align=\"center\">@Col4</div>";
TreeModels[5]="<div align=\"center\">@Col5</div>";
TreeModels[6]="<div align=\"center\">@Col6</div>";
TreeModels[7]="<div align=\"center\">@Col7</div>";
TreeModels[8]="<div align=\"center\">@Col8</div>";

function SpreadNodes(PBSCode,LayerNumber,obj){
	//alert(DataSourceUrl+"?Layer="+(parseInt(LayerNumber)+1)+"&GetType=ChildNodes&SubjectCode="+SubjectCode);
	ShowChildNode(DataSourceUrl+"?Layer="+(parseInt(LayerNumber)+1)+"&PBSCode="+PBSCode,obj,TreeModels,"PBSCode");
}

function ShowLayer(layer){
	ShowChildNodeByLayer(TreeObj,layer,true,TreeModels,DataSourceUrl,DataSourceUrlParams,"PBSCode");
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"PBSCode",RowClassName,GridClassName);

		</Script>
	</body>
</HTML>
