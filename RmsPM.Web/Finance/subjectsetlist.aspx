<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectSetList" CodeFile="SubjectSetList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���׹���</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">ϵͳ���� 
									- ���׹���</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="addNewSubjectSet();" type="button" value="��������"
							name="btnAdd" runat="server">
					</td>
				</TR>
				<TR height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15"
								CellPadding="0" CssClass="list" ShowFooter="False">
								<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="��������">
										<ItemTemplate>
											<a href="#" onclick="modifyNewSubjectSet(code); return false;" code='<%# DataBinder.Eval(Container, "DataItem.SubjectSetCode") %>'>
												<%# DataBinder.Eval(Container, "DataItem.SubjectSetName") %>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="�� ˾">
										<ItemTemplate>
											<%# RmsPM.BLL.SubjectRule.GetSelfAccountUnitName ( DataBinder.Eval(Container, "DataItem.SubjectSetCode").ToString() ) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="��Ŀ�������">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.SubjectRule") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="����ϵͳ�ӿ�">
										<ItemTemplate>
											<%# RmsPM.BLL.SubjectRule.GetFinanceInterfaceName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.FinanceInterface"))) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="�� ע">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.Remark") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="��ƿ�Ŀά��">
										<ItemTemplate>
											<a href="#" SubjectSetCode='<%# DataBinder.Eval(Container, "DataItem.SubjectSetCode") %>' onclick='ViewSubject(SubjectSetCode);'>
												��ƿ�Ŀά��</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="�������ά��">
										<ItemTemplate>
											<a href="#" SubjectSetCode='<%# DataBinder.Eval(Container, "DataItem.SubjectSetCode") %>' onclick='ViewFinanceInterface(SubjectSetCode);'>
												�������ά��</a>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</TR>
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
			</TABLE>
			<script language="javascript">
<!--

	function addNewSubjectSet()
	{
		OpenCustomWindow('SubjectSetModify.aspx','��������', 500, 360);
	}
	
	function modifyNewSubjectSet(subjectSetCode)
	{
		OpenCustomWindow('SubjectSetModify.aspx?SubjectSetCode='+subjectSetCode,'�޸�����', 500, 360);
	}
	
	function ViewSubject( subjectSetCode )
	{
		window.navigate('../Finance/SubjectTree.aspx?SubjectSetCode=' + subjectSetCode);
	}

	function ViewFinanceInterface( subjectSetCode )
	{
		window.navigate('../Finance/FinanceInterfaceFrame.aspx?SubjectSetCode=' + subjectSetCode);
	}

//-->
			</script>
		</form>
	</body>
</HTML>
