<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetDynamicSetup" CodeFile="CostBudgetDynamicSetup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ԥ���������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><input type="button" runat="server" id="btnPBSChange" name="btnPBSChange" value="btnPBSChange"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">Ԥ���������</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="70" class="form-item">��Ŀ����&nbsp;&nbsp;<br>��ʾ��ʽ��</TD>
								<TD>
									<input type="radio" id="rdoOfflineType0" name="rdoOfflineType" runat="server" value="0">ʼ�ռ�ʱ
									<br>
									<input type="radio" id="rdoOfflineType1" name="rdoOfflineType" runat="server" value="1">�Ǽ�ʱ����Ч��Ϊ��<INPUT id="txtValidHours" type="text" class="input-nember" size="6" name="txtValidHours"
										runat="server">Сʱ
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server"
										onclick="document.all.divHintSave.style.display = '';" onserverclick="btnSave_ServerClick"> <input style="DISPLAY:none" id="btnDelete" name="btnDelete" type="button" class="submit"
										value="ɾ ��" runat="server" onclick="if (!confirm('ȷʵҪɾ����')) return false;">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 80px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
