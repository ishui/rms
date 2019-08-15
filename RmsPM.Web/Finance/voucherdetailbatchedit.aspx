<%@ Register TagPrefix="cc1" Namespace="ZL.WebControls.DateTimeBox" Assembly="ZL.WebControls.DateTimeBox" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherDetailBatchEdit" CodeFile="VoucherDetailBatchEdit.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>批量修改凭证分录</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onkeydown="if(event.keyCode==13) event.keyCode=9" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">批量修改凭证分录</td>
				</tr>
				<tr height="80%">
					<td class="topic" vAlign="top" align="center">
						<table cellpadding="0" cellspacing="0" border="0" width="100%" class="form">
							<TR>
								<TD class="form-item" noWrap width="30%"><INPUT id="chkSubjectCodeJ" onclick="chkClick(this);" type="checkbox" name="chkSubjectCodeJ"
										runat="server">借方科目：</TD>
								<TD width="70%">
									<div id="divSubjectCodeJ">
										<uc1:InputSubject id="ucInputSubjectJ" runat="server"></uc1:InputSubject>
									</div>
								</TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap><INPUT id="chkSubjectCodeD" onclick="chkClick(this);" type="checkbox" name="chkSubjectCodeD"
										runat="server">贷方科目：</TD>
								<TD>
									<div id="divSubjectCodeD">
										<uc1:InputSubject id="ucInputSubjectD" runat="server"></uc1:InputSubject>
									</div>
								</TD>
							</TR>
							<TR>
								<TD class="form-item"><INPUT id="chkSummary" onclick="chkClick(this);" type="checkbox" name="chkSummary" runat="server">摘 
									要：</TD>
								<TD><input class="input" id="txtSummary" type="text" size="30" name="txtSummary" runat="server"></TD>
							</TR>
							<TR>
								<TD class="form-item"><INPUT id="chkSupl" onclick="chkClick(this);" type="checkbox" name="chkSupl" runat="server">供 
									应 商：</TD>
								<TD width="70%">
									<div id="divSupl"><span id="divSuplName"></span> <input class="input" id="txtSuplName" type="hidden" size="30" name="txtSuplName" runat="server">
										<A href="javascript:SelectSupl();"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><input class="textbox" id="txtSuplCode" type="hidden" size="30" name="txtSuplCode" runat="server"></div>
								</TD>
							</TR>
							<TR>
								<TD class="form-item"><INPUT id="chkCust" onclick="chkClick(this);" type="checkbox" name="chkCust" runat="server">客 
									户：</TD>
								<TD width="70%">
									<div id="divCust"><span id="divCustName"></span> <input class="input" id="txtCustName" type="hidden" size="30" name="txtCustName" runat="server">
										<A href="javascript:SelectSupplier();"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><input class="textbox" id="txtCustCode" type="hidden" size="30" name="txtCustCode" runat="server"></div>
								</TD>
							</TR>
							<TR>
								<TD class="form-item"><INPUT id="chkUFUnit" onclick="chkClick(this);" type="checkbox" name="chkCust" runat="server">单 
									位：</TD>
								<TD width="70%">
									<div id="divUFUnit"><span id="divUFUnitName"></span> <input class="input" id="txtUFUnitName" type="hidden" size="30" name="txtUFUnitName" runat="server">
										<A href="javascript:SelectUFUnit();"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><input class="textbox" id="txtUFUnitCode" type="hidden" size="30" name="txtUFUnitCode"
											runat="server"></div>
								</TD>
							</TR>
							<TR>
								<TD class="form-item"><INPUT id="chkUFProject" onclick="chkClick(this);" type="checkbox" name="chkCust" runat="server">项 
									目：</TD>
								<TD width="70%">
									<div id="divUFProject"><span id="divUFProjectName"></span> <input class="input" id="txtUFProjectName" type="hidden" size="30" name="txtUFProjectName"
											runat="server"> <A href="javascript:SelectUFProject();"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><input class="textbox" id="txtUFProjectCode" type="hidden" size="30" name="txtUFProjectCode"
											runat="server"></div>
								</TD>
							</TR>
							<TR>
								<TD class="form-item"><INPUT id="chkBillNo" onclick="chkClick(this);" type="checkbox" name="chkBillNo" runat="server">票 
									号：</TD>
								<TD><input class="input" id="txtBillNo" type="text" size="30" name="txtBillNo" runat="server"></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD class="form-item"><INPUT id="chkPaymentType" onclick="chkClick(this);" type="checkbox" name="chkPaymentType"
										runat="server">付款类型：</TD>
								<TD><SELECT id="sltPaymentType" class="select" style="WIDTH: 200px" name="sltPaymentType" runat="server">
										<OPTION selected></OPTION>
									</SELECT></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD class="form-item"><INPUT id="chkRemark" class="select" onclick="chkClick(this);" type="checkbox" name="chkRemark"
										runat="server">备 注：</TD>
								<TD><input class="input" id="txtRemark" type="text" size="30" name="txtRemark" runat="server"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<TR>
								<TD align="center"><asp:button id="btnSave" runat="server" Text="确 定" CssClass="submit" onclick="btnSave_Click"></asp:button>&nbsp;
									<INPUT class="submit" onclick="window.close();" type="button" value="取 消"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtDetailVoucherCode" type="hidden" name="txtDetailVoucherCode" runat="server">
			<input id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<input id="txtSubjectNameJ" type="hidden" name="txtSubjectNameJ" runat="server"><input id="txtSubjectNameD" type="hidden" name="txtSubjectNameD" runat="server">
			<input id="txtHintJ" type="hidden" name="txtHintJ" runat="server"><input id="txtHintD" type="hidden" name="txtHintD" runat="server">
			<input type="hidden" name="txtIsD">
			<SCRIPT language="javascript">
<!--
	function chkClick(obj)
	{
		var display;
		
		if (obj.checked)
			display = "block";
		else
			display = "none";
			
		if (obj == Form1.chkSubjectCodeJ)
		{
			document.all.divSubjectCodeJ.style.display = display;
		}
		if (obj == Form1.chkSubjectCodeD)
		{
			document.all.divSubjectCodeD.style.display = display;
		}
		if (obj == Form1.chkPaymentType)
		{
			Form1.sltPaymentType.style.display = display;
		}
		if (obj == Form1.chkSummary)
		{
			Form1.txtSummary.style.display = display;
		}
		if (obj == Form1.chkRemark)
		{
			Form1.txtRemark.style.display = display;
		}

		if (obj == Form1.chkSupl)
		{
			document.all.divSupl.style.display = display;
		}

		if (obj == Form1.chkCust)
		{
			document.all.divCust.style.display = display;
		}

		if (obj == Form1.chkUFUnit)
		{
			document.all.divUFUnit.style.display = display;
		}

		if (obj == Form1.chkUFProject)
		{
			document.all.divUFProject.style.display = display;
		}

		if (obj == Form1.chkBillNo)
		{
			Form1.txtBillNo.style.display = display;
		}

	}
	
	chkClick(Form1.chkSubjectCodeJ);
	chkClick(Form1.chkSubjectCodeD);
	chkClick(Form1.chkPaymentType);
	chkClick(Form1.chkSummary);
	chkClick(Form1.chkRemark);
	chkClick(Form1.chkSupl);
	chkClick(Form1.chkCust);
	chkClick(Form1.chkUFUnit);
	chkClick(Form1.chkUFProject);
	chkClick(Form1.chkBillNo);
			
//选择供应商
function SelectSupl(i)
{
	OpenCustomWindow("SelectSuplList.aspx", "选择供应商", 500, 580);
}

//选择供应商返回
function SelectSuplReturn(code, name)
{
	Form1.txtSuplCode.value = code;
	Form1.txtSuplName.value = name;
	document.all.divSuplName.innerText = name;
}

//选择客户
function SelectSupplier(i)
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "选择供应商");
}

//选择客户返回
function DoSelectSupplierReturn(code, name)
{
	Form1.txtCustCode.value = code;
	Form1.txtCustName.value = name;
	document.all.divCustName.innerText = name;
}

//选择核算单位
function SelectUFUnit(i)
{
	OpenCustomWindow("SelectUFUnitList.aspx", "选择核算单位", 500, 580);
}

//选择核算单位返回
function SelectUFUnitReturn(code, name)
{
	Form1.txtUFUnitCode.value = code;
	Form1.txtUFUnitName.value = name;
	document.all.divUFUnitName.innerText = name;
}

//选择核算项目
function SelectUFProject(i)
{
	OpenCustomWindow("SelectUFProjectList.aspx", "选择核算单位", 500, 580);
}

//选择核算项目返回
function SelectUFProjectReturn(code, name)
{
	Form1.txtUFProjectCode.value = code;
	Form1.txtUFProjectName.value = name;
	document.all.divUFProjectName.innerText = name;
}

	Form1.txtDetailVoucherCode.value = window.opener.document.all.txtSelect.value;
//-->
			</SCRIPT>
		</form>
	</body>
</HTML>
