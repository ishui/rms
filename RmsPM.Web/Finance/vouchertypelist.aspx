<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherTypeList" CodeFile="VoucherTypeList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>凭证类型</title>
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
									- 凭证类型</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add();" type="button" value="新增凭证类型" name="btnAdd"
							runat="server">
					</td>
				</TR>
				<!--
				<tr>
					<td class="table">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="note" height="32" valign="bottom">财务系统：<a href="#" onclick="FinanceInterfaceSetup();return false;"><span id="spanFinanceInterface" runat="server"></span></a></td>
							</tr>
						</table>
					</td>
				</tr>
				-->
				<TR height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15"
								CellPadding="0" CssClass="list" ShowFooter="False">
								<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="凭证类型编号">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.Code") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="凭证类型名称">
										<ItemTemplate>
											<a href="#" onclick="Modify(code); return false;" code='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
												<%# DataBinder.Eval(Container, "DataItem.Name") %>
											</a>
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

	function Add()
	{
		OpenSmallWindow('VoucherTypeModify.aspx','凭证类型修改'  );
	}
	
	function Modify(code)
	{
		OpenSmallWindow('VoucherTypeModify.aspx?Code='+code,'凭证类型修改'  );
	}
	
	function FinanceInterfaceSetup()
	{
		OpenCustomWindow("FinanceInterfaceSetup.aspx", "FinanceInterfaceSetup", 350, 200);
	}
	
//-->
			</script>
		</form>
	</body>
</HTML>
