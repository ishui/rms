<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.GroundWorkInfo" CodeFile="GroundWorkInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��������ͼ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="tools-area" vAlign="top">
						���̣�<select class="select" id="sltGroundWork" onchange="GroundWorkChange();" name="sltGroundWork"
							runat="server">
						</select>
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add();" type="button" value="�� ��" name="btnAdd"
							runat="server"> <input class="button" id="btnModify" onclick="Modify();" type="button" value="�� ��" name="btnModify"
							runat="server"> <input class="button" id="btnModifyLocation" onclick="ModifyLocation();" type="button"
							value="�޸�λ��" name="btnModifyLocation" runat="server"> <input class="button" id="btnUpload" onclick="Upload();" type="button" value="�ϴ�ƽ��ͼ" name="btnUpload"
							runat="server" style="display:none"> <input class="button" id="btnDelete" onclick="if (!Delete()) return false;" type="button"
							value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" id="tableMain">
							<tr>
								<td><iframe name="frameChart" marginWidth="0" marginHeight="0" src="" frameBorder="no" width="100%"
										scrolling="no" height="100%"></iframe>
								</td>
							</tr>
						</table>
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
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"><input id="txtDefaultGroundWorkCode" type="hidden" name="txtDefaultGroundWorkCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function winload()
{
	GroundWorkChange();
}

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

//�ϴ�ƽ��ͼ
function Upload()
{
	var GroundWorkCode = Form1.sltGroundWork.value;
	
	if (GroundWorkCode == "") return;
	
	var i = Form1.sltGroundWork.selectedIndex;
	var WBSCode = Form1.sltGroundWork.item(i).WBSCode;
	
	OpenCustomWindow("../UserControls/SaveAttach.aspx?strMasterCode=" + WBSCode + "&strAttachMentType=GroundWork", null,400,300);
}

function Refresh()
{
}

//����
function Add()
{
	OpenCustomWindow("../ConstructProg/GroundWorkModify.aspx?ProjectCode=" + Form1.txtProjectCode.value, "��������ͼ�޸�", 760, 540);
}

//�޸�
function Modify()
{
	var code = Form1.sltGroundWork.value;
	OpenCustomWindow("../ConstructProg/GroundWorkModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&GroundWorkCode=" + code, "��������ͼ�޸�", 760, 540);
}

//ת������
function GroundWorkChange()
{
	var code = Form1.sltGroundWork.value;
	
	if (code == "")
	{
		document.all.tableMain.style.display = "none";
		if (Form1.btnModifyLocation)	Form1.btnModifyLocation.style.display = "none";
//		if (Form1.btnUpload)	Form1.btnUpload.style.display = "none";
		if (Form1.btnModify)	Form1.btnModify.style.display = "none";
		if (Form1.btnDelete)	Form1.btnDelete.style.display = "none";
	}
	else
	{
		document.all.tableMain.style.display = "";

		if (Form1.btnModifyLocation)	Form1.btnModifyLocation.style.display = "";
//		if (Form1.btnUpload)	Form1.btnUpload.style.display = "";
		if (Form1.btnModify)	Form1.btnModify.style.display = "";
		if (Form1.btnDelete)	Form1.btnDelete.style.display = "";

		document.all("frameChart").src = "GroundWorkChart.aspx?GroundWorkCode=" + code;
	}
}

//ɾ��
function Delete()
{
	if (!confirm("ȷʵҪɾ����")) return false;
	
	document.all.divHintLoad.style.display='';
	return true;
}

//�޸�λ��
function ModifyLocation()
{
	var GroundWorkCode = Form1.sltGroundWork.value;
	window.location = "GroundWorkLocation.aspx?GroundWorkCode=" + GroundWorkCode + "&FromUrl=" + escape(window.location);
}

//-->
		</SCRIPT>
	</body>
</HTML>
