<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CBSTempletList" CodeFile="CBSTempletList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用分解模板</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">系统管理 
										- 费用分解模板</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnNewCBSModule" onclick="addNewCBSModule('');return false;"
							type="button" value="新增模板" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td valign="top" class="table">
						<asp:datagrid id="dgList" runat="server" CssClass="list" AutoGenerateColumns="False" CellPadding="2"
							Width="100%" ShowFooter="True">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="模板名称" ItemStyle-Width="30%">
									<ItemTemplate>
										<a href="##" onclick="doModifyCBSModule(code)" code='<%# DataBinder.Eval(Container.DataItem, "CBSModuleCode") %>' >
											<%# DataBinder.Eval(Container.DataItem, "CBSModuleName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="模板说明">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Remark") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
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
			</TABLE>
		</form>
		<script language="javascript">
<!--
		function doModifyCBSModule(code)
		{
			window.navigate( '../Project/CBS.aspx?Type=SystemModule&ProjectCode='+code,"费用模板" );
		}
			
		function addNewCBSModule()
		{
			OpenSmallWindow( '../Cost/CBSModuleModify.aspx'  ,"新建费用模板" );
		}
		
//-->
		</script>
	</body>
</HTML>
