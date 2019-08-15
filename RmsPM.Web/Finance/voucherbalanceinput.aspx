<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherBalanceInput" CodeFile="VoucherBalanceInput.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>总帐导入</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
						<iframe src="../Cost/SavingWating.htm" style="DISPLAY:none" id="divHintSave" frameBorder="no"
							width="100%" scrolling="auto" height="100%"></iframe>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white"
				id="tableFull">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导入总帐凭证</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="tableMain">
							<tr>
								<td class="form-item">文件：</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"><font color="red">*</font></td>
							</tr>
							<tr>
								<td class="form-item">请款单类型：</td>
								<TD>
									<uc1:InputSystemGroup id="inputSystemGroupPayment" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
								</TD>
							</tr>
							<tr>
								<td class="form-item">付款单类型：</td>
								<TD>
									<uc1:InputSystemGroup id="inputSystemGroupPayout" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr align="center">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">注意：使用从用友财务软件导出的总帐凭证文件：<br>
									1.文件类型必须是csv（逗号分隔）<br>
									2.文件的第1行为标题行，以后为数据行。<br>
									3.数据行依次为：月,日,凭证号数,科目编码,科目名称,摘要,借方,贷方,方向8,余额
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10" id="tableButton">
							<tr>
								<td align="center">
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="清 空" runat="server"
										onclick="if (!Delete()) return false;" onserverclick="btnDelete_ServerClick"> <input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="doSave();" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--

	//清空
	function Delete()
	{
		if (!confirm("确实要清空所有导入的总帐吗？")) return false;
		
		document.all.divHintSave.style.display = "";
		return true;
	}

	function doSave()
	{
		document.all.divHintSave.style.display = "";
	}


//-->
		</script>
	</body>
</HTML>
