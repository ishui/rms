<%@ Page language="c#" Inherits="RmsPM.Web.Construct.VisualProgressModify" CodeFile="VisualProgressModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�������</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="80" class="form-item">������ȣ�</TD>
								<TD><input type="text" class="input" size="30" id="txtVisualProgress" name="txtVisualProgress"
										runat="server"><font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<td class="form-item">�������ͣ�</td>
								<td><select class="select" id="sltProgressType" name="sltProgressType" runat="server">
										<option selected value="">--��ѡ��--</option>
										<option value="-1">δ����</option>
										<option value="0">����</option>
										<option value="1">�ṹ</option>
										<option value="2">����</option>
									</select>
									<font color="red">*</font>
								</td>
							</tr>
							<tr>
								<td class="form-item">ʱ�����ͣ�</td>
								<td><input type="radio" id="rdoIsPoint0" name="rdoIsPoint" runat="server" value="0">ʱ���&nbsp;&nbsp;&nbsp;&nbsp;
									<input type="radio" id="rdoIsPoint1" name="rdoIsPoint" runat="server" value="1">ʱ���
								</td>
							</tr>
							<tr>
								<td class="form-item">Ͷ�ʱ�����</td>
								<td><input type="text" class="input-nember" size="6" id="txtInvestPercent" name="txtInvestPercent"
										runat="server">%</td>
							</tr>
							<tr>
								<td class="form-item">�� �� �ţ�</td>
								<td><input type="text" class="input-nember" size="2" id="txtSortID" name="txtSortID" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="ɾ ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
										runat="server" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtSystemID" type="hidden" name="txtSystemID" runat="server">
		</form>
	</body>
</HTML>
