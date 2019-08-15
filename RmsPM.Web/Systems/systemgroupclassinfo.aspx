<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SystemGroupClassInfo" CodeFile="SystemGroupClassInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
	//释放源
	function ClearSrc()
	{
		window.parent.document.all("txtIsCut").value = "";
		window.parent.document.all("txtSrcDesc").value = "";
		window.parent.document.all("txtSrcGroupCode").value = "";
		window.parent.document.all("txtSrcClassCode").value = "";
	}
	
	//刷新
	function Refresh(act)
	{
		if (act.toLowerCase() == "insert")
		{
			window.parent.frameMain.MyRefreshChild();
			window.location = window.location;
		}
		else
		{
			if (act.toLowerCase() == "move")
			{
				window.parent.frameMain.MyRefreshChild();
				window.parent.frameMain.MyDeleteSrc();

				ClearSrc();
				window.location = window.location;
			}
			else
			{
				window.parent.frameMain.MyRefreshNode();
				window.location = window.location;
			}
		}
	}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR id="trToolbar" runat="server">
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAddChild" onclick="Insert();" type="button" value="新增子项" name="btnAddChild"
							runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnPaste" onclick="if (!Paste()) return false;" type="button"
							value="粘 贴" name="btnPaste" runat="server" onserverclick="btnPaste_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="100">大类名称：</TD>
								<TD width="70%"><asp:label id="lblClassName" Runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr style="DISPLAY:none">
					<td class="table" vAlign="top" align="center">
						<table border="0" cellpadding="0" cellspacing="0" align="left">
							<tr>
								<td class="intopic">子项</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%" style="DISPLAY:none">
					<td class="table" vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						</div>
					</td>
				</tr>
				<tr id="tablePaste" style="DISPLAY:none">
					<td class="table" vAlign="bottom" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note">剪贴板：<span id="spanSrcDesc"></span></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtClassCode" type="hidden" name="txtClassCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input type="hidden" id="txtSrcGroupCode" name="txtSrcGroupCode" runat="server"><input type="hidden" id="txtSrcClassCode" name="txtSrcClassCode" runat="server">
			<input type="hidden" id="txtIsCut" name="txtIsCut" runat="server">
		</form>
		<SCRIPT language="javascript">
	
	//新增子项
	function Insert()
	{
		OpenCustomWindow("SystemGroupModify.aspx?Action=Insert&ParentCode=&ClassCode="+Form1.txtClassCode.value, "系统类别修改", 500, 300);
	}

	//刷新
	function Refresh(act)
	{
		if (act.toLowerCase() == "insert")
		{
			window.parent.frameMain.MyRefreshChild();
			window.location = window.location;
		}
		else
		{
			window.parent.frameMain.MyRefreshNode();
			window.location = window.location;
		}
	}

	//粘贴
	function Paste()
	{
		var srcGroupCode = window.parent.document.all("txtSrcGroupCode").value;
		var srcClassCode = window.parent.document.all("txtSrcClassCode").value;
		var srcDesc = window.parent.document.all("txtSrcDesc").value;
		var isCut = window.parent.document.all("txtIsCut").value;
		
		var dstGroupCode = "";
		var dstClassCode = Form1.txtClassCode.value;

		if (srcGroupCode == "") return false;

		if (isCut == "1")
		{
			if (dstGroupCode == srcGroupCode)
			{
				alert("无法移动 " + srcDesc + "：源结点和目标结点相同");
				return false;
			}
			
			if (!confirm("确实要移动 " + srcDesc + " 吗？")) return false;
		}
		else
		{
			if (!confirm("确实要粘贴吗？")) return false;
		}
		
		Form1.txtSrcGroupCode.value = srcGroupCode;
		Form1.txtSrcClassCode.value = srcClassCode;
		Form1.txtIsCut.value = isCut;
		
		return true;
	}
	
	function IniPaste()
	{
		var srcGroupCode = window.parent.document.all("txtSrcGroupCode").value;
		var srcDesc = window.parent.document.all("txtSrcDesc").value;
		document.all.spanSrcDesc.innerText = srcDesc;
		
		if (srcGroupCode == "")
		{
			Form1.btnPaste.style.display = "none";
			document.all.tablePaste.style.display = "none";
		}
		else
		{
			Form1.btnPaste.style.display = "";
			document.all.tablePaste.style.display = "";
		}
	}

	function winload()
	{
		IniPaste();
	}
		</SCRIPT>
	</body>
</HTML>
