<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptConstructProgList" CodeFile="RptConstructProgList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptConstructProgList</title>
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
								<td width="100%" height="25" valign="bottom" class="note">���̽��ȷ�������<span runat="server" id="lblYm"></span></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
								<tr>
									<td nowrap class="form-item">��Ŀ�滮�������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">סլ�滮�������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��ҵ�滮�������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">�����潨���</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��Ŀ��������</td>
									<td nowrap width="80"></td>
								</tr>
								<tr>
									<td nowrap class="form-item">��Ŀ�����������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">סլ�����������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">��ҵ�����������</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">������������</td>
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
											<tr>
												<td nowrap rowspan="12" class="list-c">��<br>��<br>��</td>
												<td nowrap class="sum-item">���̿�����(ƽ��)</td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;סլ</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;��ҵ</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;����</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="sum-item">�����ڽ���(ƽ��)</td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;סլ</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;��ҵ</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;����</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="sum-item">���̿�����(ƽ��)</td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;סլ</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;��ҵ</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;����</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<table class="list" cellSpacing="0" cellPadding="0" border="0" width="100%">
											<tr class="list-title" align="center">
												<td nowrap rowspan="8" class="list-c">��<br>��</td>
												<td noWrap>��Ŀ������Ҫ������</td>
												<td nowrap>�ƻ���ʼʱ��</td>
												<td nowrap>�ƻ���ʱ</td>
												<td nowrap>ʵ�ʿ�ʼʱ��</td>
												<td nowrap>��ֹ���º�ʱ</td>
												<td nowrap>��ֹ���������</td>
												<td nowrap>����ܿ�������</td>
												<td nowrap>��ע˵��</td>
											</tr>
											<tr>
												<td nowrap class="list-c">ǰ����֤ȡ��</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">��Ŀ����Ǩ������ƽ��</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">׮��������ʩ��</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">��Ŀ��Ԥ�����֤��ȡ��</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">��Ŀ����</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">��Ŀ����ʩ�����ﵽԤ��������</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">��Ŀ��סլ����ʹ�����֤��ȡ��</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
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
