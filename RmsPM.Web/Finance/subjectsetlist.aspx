<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectSetList" CodeFile="SubjectSetList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>帐套管理</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">系统管理 
									- 帐套管理</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="addNewSubjectSet();" type="button" value="新增帐套"
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
									<asp:TemplateColumn HeaderText="帐套名称">
										<ItemTemplate>
											<a href="#" onclick="modifyNewSubjectSet(code); return false;" code='<%# DataBinder.Eval(Container, "DataItem.SubjectSetCode") %>'>
												<%# DataBinder.Eval(Container, "DataItem.SubjectSetName") %>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="公 司">
										<ItemTemplate>
											<%# RmsPM.BLL.SubjectRule.GetSelfAccountUnitName ( DataBinder.Eval(Container, "DataItem.SubjectSetCode").ToString() ) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="科目编码规则">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.SubjectRule") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="财务系统接口">
										<ItemTemplate>
											<%# RmsPM.BLL.SubjectRule.GetFinanceInterfaceName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.FinanceInterface"))) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="备 注">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.Remark") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="会计科目维护">
										<ItemTemplate>
											<a href="#" SubjectSetCode='<%# DataBinder.Eval(Container, "DataItem.SubjectSetCode") %>' onclick='ViewSubject(SubjectSetCode);'>
												会计科目维护</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="财务编码维护">
										<ItemTemplate>
											<a href="#" SubjectSetCode='<%# DataBinder.Eval(Container, "DataItem.SubjectSetCode") %>' onclick='ViewFinanceInterface(SubjectSetCode);'>
												财务编码维护</a>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
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
		OpenCustomWindow('SubjectSetModify.aspx','新增帐套', 500, 360);
	}
	
	function modifyNewSubjectSet(subjectSetCode)
	{
		OpenCustomWindow('SubjectSetModify.aspx?SubjectSetCode='+subjectSetCode,'修改帐套', 500, 360);
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
