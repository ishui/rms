<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectCost" CodeFile="SelectCost.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ�������</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--
	function SelCode()
	{
		var codeObject= document.all("CostCode");
		var codes="";
		var names="";
		
		if ( codeObject[0])
		{
			for(var i=0;i<codeObject.length;i++)
			{
				if ((codeObject[i].checked))
				{
					codes+=((codes=="")?(codeObject[i].value):(","+codeObject[i].value));
					names+=((names=="")?(codeObject[i].text):(","+codeObject[i].text));
				}
			}
		}
		else
		{
			if ( codeObject.checked )
			{
				codes = codeObject.value;
				names = codeObject.text;
			}
		}
		
		var flag = '<%=Request["Flag"]%>';
		window.opener.<%=ViewState["ReturnFunc"]%>(codes, names, flag);
		window.close();
	}
	
	function SelectSingleCost( costCode,costName)
	{
		var flag = '<%=Request["Flag"]%>';
		window.opener.<%=ViewState["ReturnFunc"]%>(costCode,costName, flag);
		window.close();
		
	}
	
	
//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ�������</td>
				</tr>
				<tr height="100%">
					<td align="left" vAlign="top" align="center" height="95%">
						<div style="position:absolute;overflow:auto;width:100%;height:100%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
							width="98%">
							<thead>
								<TR class="tree-title" id="trTitle">
									<TD noWrap align="left" id="tdCostName" style="DISPLAY: none">�ɱ���Ŀ</TD>
									<TD noWrap align="left" id="tdCheckBox" style="DISPLAY: none">ѡ��</TD>
								</TR>
							</thead>
							<tbody id="Tree">
							</tbody>
							<tfoot style="display:none">
								<TR class="tree-title">
									<TD>&nbsp;</TD>
									<TD>&nbsp;</TD>
								</TR>
							</tfoot>
						</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE id="tableButton" cellSpacing="9" cellPadding="0" align="center" border="0" runat="server">
							<tr>
								<td align="center"><INPUT id="btnOK" name="btnOK" onclick="javascript:SelCode();" type="button" value="ȷ ��" class="submit">
									<INPUT onclick="javascript:window.close();" type="button" value="ȡ ��" class="submit">
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtShowItems" runat="server" NAME="txtShowItems"> <INPUT id="txtTreeType" type="hidden" name="txtTreeType" runat="server">
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl='../SelectBox/SelectCostData.aspx?ProjectCode=<%=Request["ProjectCode"]%>&AccountPoint=<%=Request["AccountPoint"]%>' ;

// @IndentStart			��������ѭ����ʼ��
// @IndentEnd			��������ѭ��������
// @NodeSymbolStart		�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @NodeSymbolEnd		�ڵ��־��ʼ��
// @JsCodeStart			�ڵ��־��ʼ�� ��ʼ�ͽ���������д�� �պϱ�ʾ|������ʾ|�޽ڵ��ʾ
// @JsCodeEnd			�ڵ��־��ʼ��

var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td id='td@CostCode'  onclick=\"SpreadNodes('@CostCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";

//��ѡ���ѡ
var MultiSelect='<%=Request["MultiSelect"]%>';

//�Ƿ������ѡ�������
var SelectAllLeaf = '<%=Request["SelectAllLeaf"]%>';

if ( MultiSelect == "1" )
{
	//��ѡʱ����ʾCHECKBOX
	v0+="<td>";
	v0+="<input type='checkbox' value=@CostCode style='display:@Display' text=@CostName name='CostCode'>";
	v0+="@CostName</td></tr></table>";
}
else
{
	//��ѡʱ����ʾ����
	Form1.btnOK.style.display = "none";

	if (SelectAllLeaf == "1")
		v0+="<td><a href=\"#\" onclick=\"SelectSingleCost('@CostCode','@CostName')\">@CostName</a></td></tr></table>";
	else
		v0+="<td><a style=\"display:@DisplayHref\" href=\"#\" onclick=\"SelectSingleCost('@CostCode','@CostName')\">@CostName</a><span style=\"display:@DisplaySpan\">@CostName</span></td></tr></table>";
}

//��һ�У��ɱ���
TreeModels.push(v0);

//�ڵ�
function SpreadNodes(CostCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+CostCode,obj,TreeModels,"CostCode");
}

//����
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostCode",RowClassName,GridClassName);
}

//ָ����
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostCode",RowClassName,GridClassName);
}

//���½ڵ�
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostCode",RowClassName,GridClassName);
}

//�����ӽڵ�
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostCode",RowClassName,GridClassName);
}



GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostCode",RowClassName,GridClassName);

//չ�������
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
