<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ProgressReportInfo" CodeFile="ProgressReportInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ȱ�����Ϣ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no"
		rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���ȱ���</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnModify" onclick="Modify();" type="button" value="�� ��" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
							type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnClose" onclick="javascript:window.close();" type="button"
							value="�� ��" name="btnClose" runat="server">
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="bottom" align="center" height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note">��λ���̣�<asp:label id="lblPBSUnitName" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="100">�������ڣ�</TD>
									<TD><asp:label id="lblReportDate" Runat="server"></asp:label></TD>
									<TD class="form-item" width="100">�����ˣ�</TD>
									<TD><asp:label id="lblReportPersonName" Runat="server"></asp:label></TD>
								</TR>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic">���ȱ���</td>
								</tr>
							</table>
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<TD class="form-item" width="100">��ǰ������ȣ�</TD>
									<TD><asp:label id="lblVisualProgress" Runat="server"></asp:label></TD>
									<td class="form-item">Ŀǰʩ��������</td>
									<td><asp:label id="lblCurrentLayer" Runat="server"></asp:label></td>
								</tr>
								<TR>
									<TD class="form-item" align="right">������</TD>
									<TD colSpan="3"><uc1:attachmentlist id="myAttachMentList" runat="server"></uc1:attachmentlist></TD>
								</TR>
								<TR>
									<TD class="form-item">���ݣ�</TD>
									<TD noWrap colSpan="3"><asp:label id="lblContent" Runat="server"></asp:label></TD>
								</TR>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic">���ձ���</td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgRisk" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="False" DataKeyField="ProgressRiskCode">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="��������">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RiskTypeName") %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����ָ��">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RiskIndexName") %>
														<asp:RadioButtonList Visible="False" Enabled=False Runat="server" ID="rdoRiskIndexCode" DataSource="<%# GetRiskIndexDataSource() %>" DataTextField="IndexName" DataValueField="IndexCode" RepeatColumns="20">
														</asp:RadioButtonList>
														<input type="hidden" name="txtRiskIndexCode" id="txtRiskIndexCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RiskIndexCode") %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="100">����������</TD>
									<TD noWrap><asp:label id="lblRiskRemark" Runat="server"></asp:label></TD>
								</TR>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
			<input id="txtProgressCode" type="hidden" name="txtProgressCode" runat="server"><input id="txtReportPersonCode" type="hidden" name="txtReportPersonCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//�޸Ľ��ȱ���
function Modify()
{
	OpenFullWindow("../Construct/ConstructProgressModify.aspx?ProgressCode=" + Form1.txtProgressCode.value + "&action=Modify", "���ȱ����޸�");
}

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}
//-->
		</SCRIPT>
	</body>
</HTML>
