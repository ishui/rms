<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectTaskRelaType" CodeFile="SelectTaskRelaType.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>选择工作项类型</title>
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
		<style> .tree-tr2 { PADDING-RIGHT: 5px; PADDING-LEFT: 5px; BORDER-LEFT: #eaeff4 1px solid; BORDER-BOTTOM: #d3dfe8 1px solid; HEIGHT: 24px; BACKGROUND-COLOR: #e5f0ce } </style>
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
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="winload();" rightMargin="0"
		scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择工作项类型</td>
				</tr>
				<tr>
					<td>
						<table class="form" cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td class="form-item">类型：</td>
								<td><asp:RadioButtonList Runat="server" ID="rdoType" RepeatColumns="3" onclick="TypeClick();">
										<asp:ListItem Selected="True" Value=" ">一般工作项</asp:ListItem>
										<asp:ListItem Value="U">单位工程</asp:ListItem>
										<asp:ListItem Value="B">楼栋</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<table height="100%" width="100%">
							<tr>
								<td>
									<iframe name="frameMain" src="" frameBorder="no" width="100%" scrolling="no" height="100%"
										marginwidth="0" marginheight="0"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trBtn">
					<td>
						<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center"><input id="btnOK" onclick="SaveSelect();" type="button" value="确定" class="submit">
									<input id="btnCancel" onclick="window.close();" type="button" value="取消" class="submit">
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
			<input type="hidden" id="txtReturnFunc" name="txtReturnFunc" runat="server"> <input type="hidden" id="txtCanSelectArea" name="txtCanSelectArea" runat="server">
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtPBSTypeCode" name="txtPBSTypeCode" runat="server">
			<input type="hidden" id="txtRelaType" name="txtRelaType" runat="server"><INPUT id="txtOutputFullName" type="hidden" name="txtOutputFullName" runat="server">
		</form>
		<SCRIPT language="javascript">
function TypeClick()
{
	if (Form1.rdoType[0].checked)
	{
		document.all.trBtn.style.display = "";
		document.all.frameMain.src = "";
	}

	if (Form1.rdoType[1].checked)
	{
		document.all.trBtn.style.display = "none";
		document.all.frameMain.src = "../SelectBox/SelectBuildingByPBSUnit.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&act=pbsunit&SelectCode=" + Form1.txtInputCode.value + "&type=Single";
	}

	if (Form1.rdoType[2].checked)
	{
		document.all.trBtn.style.display = "none";
		document.all.frameMain.src = "../SelectBox/SelectBuildingByPBSUnit.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&act=building&SelectCode=" + Form1.txtInputCode.value + "&type=Single";
	}
}

function winload()
{
	TypeClick();
}

function SelectReturn(code, name)
{
	Form1.txtOutputCode.value = code;
	Form1.txtOutputName.value = name;
	Form1.btnSave.click();
}

		</SCRIPT>
	</body>
</HTML>
