<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptSalList" CodeFile="RptSalList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptSalList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<style>
.list-t1 { FONT-WEIGHT: bold; BACKGROUND-COLOR: #f0fff0; TEXT-ALIGN: left }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr style="">
					<td>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="100%" height="25" valign="bottom" class="note">Ӫ��ͳ�Ʒ�������<span runat="server" id="lblYm"></span></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
								<tr>
									<td nowrap class="form-item">��ĿԤ�ƿ��۽������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">סլԤ�ƿ������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��ҵԤ�ƿ������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">����Ԥ�ƿ�����</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��Ŀ��������</td>
									<td nowrap width="80"></td>
								</tr>
								<tr>
									<td nowrap class="form-item">��ĿԤ�����������ܶ�</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">סլԤ�������ܶ�</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��ҵԤ�������ܶ�</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">����Ԥ�����۶�</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��Ŀ��������</td>
									<td nowrap width="80"></td>
								</tr>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0" width="100%">
								<tr>
									<td>
										<table class="list" cellSpacing="0" cellPadding="0" border="0" width="100%">
											<tr class="list-title" align="center">
												<td noWrap rowSpan="2" colspan="2"></td>
												<td noWrap colSpan="3">����</td>
												<td noWrap colSpan="2">����Ԥ��</td>
												<td noWrap colSpan="3">�����ۼ�</td>
												<td noWrap colSpan="3">��Ŀ�ۼ�</td>
											</tr>
											<tr class="list-title" align="center">
												<td nowrap>ʵ�ʷ���</td>
												<td nowrap>Ԥ��/����</td>
												<td nowrap>�Ա�%</td>
												<td nowrap>1������</td>
												<td nowrap>3������</td>
												<td nowrap>ʵ�ʷ���</td>
												<td nowrap>Ԥ��/����</td>
												<td nowrap>�Ա�%</td>
												<td nowrap>ʵ�ʷ���</td>
												<td nowrap>Ԥ��/����</td>
												<td nowrap>�Ա�%</td>
											</tr>
											<asp:repeater id="dgListMoney" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListHouseArea" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListPrice" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 50px">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtYear" type="hidden" name="txtYear" runat="server"><input id="txtMonth" type="hidden" name="txtMonth" runat="server">
		</form>
	</body>
</HTML>
