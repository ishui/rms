<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentTypeInfo" CodeFile="DocumentTypeInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ĵ�����</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">�ĵ����� 
									- �ĵ�����
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="table" vAlign="top">
						<TABLE id="TableToolbar" cellSpacing="0" cellPadding="5" border="0">
							<TR>
								<td width="16"><IMG src="../images/btn_li.gif"></td>
								<TD><input class="button" id="btnModify" onclick="Modify()" type="button" value="�� ��" name="btnModify"></TD>
								<TD><input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
										type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"></TD>
								<td><input class="button" id="btnAddChild" onclick="Insert();" type="button" value="��������" name="btnAddChild"
										runat="server">
								</td>
								<td>&nbsp;</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<td valign="top" class="table">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="120" class="form-item">�ĵ��������ƣ�</TD>
								<TD><asp:Label Runat="server" ID="lblTypeName"></asp:Label></TD>
							</TR>
							<tr>
								<TD class="form-item">�ϼ��ĵ����ͣ�</TD>
								<TD><asp:label id="lblParentName" runat="server"></asp:label></TD>
							</tr>
							<TR>
								<TD class="form-item">��ע��</TD>
								<TD><asp:Label Runat="server" ID="lblDescription"></asp:Label>
								</TD>
							</TR>
						</table>
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0" align="left">
							<tr>
								<td class="intopic">�ĵ���������</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<iframe id="frameTree" src='DocumentTypeTree.aspx?TreeType=Document&ParentCode=<%=Request["DocumentTypeCode"]%>' frameBorder="no"
								width="100%" scrolling="auto" height="100%"></iframe>
						</div>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<INPUT id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server"> <INPUT id="txtDocumentTypeCode" type="hidden" name="txtDocumentTypeCode" runat="server">
			<input type="hidden" id="txtParentCode" name="txtParentCode" runat="server">
		</form>
		<SCRIPT language="javascript">
	
	//��������
	function Insert(){
		var ParentCode = Form1.txtDocumentTypeCode.value;
		OpenCustomWindow("DocumentTypeModify.aspx?Action=AddChild&ParentCode="+ParentCode, "�ĵ������޸�", 400, 250);
	}

	//�޸�
	function Modify(){
		OpenCustomWindow("DocumentTypeModify.aspx?Action=Modify&DocumentTypeCode="+Form1.txtDocumentTypeCode.value, "�ĵ������޸�", 400, 250);
	}
	
	function GoBack(){
		window.location = Form1.txtFromUrl.value;
	}
	
		</SCRIPT>
	</body>
</HTML>
