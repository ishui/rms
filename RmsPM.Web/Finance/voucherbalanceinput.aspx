<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherBalanceInput" CodeFile="VoucherBalanceInput.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ʵ���</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��������ƾ֤</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="tableMain">
							<tr>
								<td class="form-item">�ļ���</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"><font color="red">*</font></td>
							</tr>
							<tr>
								<td class="form-item">�����ͣ�</td>
								<TD>
									<uc1:InputSystemGroup id="inputSystemGroupPayment" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
								</TD>
							</tr>
							<tr>
								<td class="form-item">������ͣ�</td>
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
								<td style="COLOR: blue">ע�⣺ʹ�ô����Ѳ����������������ƾ֤�ļ���<br>
									1.�ļ����ͱ�����csv�����ŷָ���<br>
									2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br>
									3.����������Ϊ����,��,ƾ֤����,��Ŀ����,��Ŀ����,ժҪ,�跽,����,����8,���
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
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="�� ��" runat="server"
										onclick="if (!Delete()) return false;" onserverclick="btnDelete_ServerClick"> <input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server"
										onclick="doSave();" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--

	//���
	function Delete()
	{
		if (!confirm("ȷʵҪ������е����������")) return false;
		
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
