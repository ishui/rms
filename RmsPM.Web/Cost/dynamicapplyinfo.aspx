<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicApplyInfo" CodeFile="DynamicApplyInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态调整申请信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onkeydown="if(event.keyCode==13) event.keyCode=9">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">动态调整申请信息</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%">
							<tr>
								<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td><input class="button" id="btnModify" onclick="doDynamicModify(); return false;" type="button"
										value="修 改" name="btnModify" runat="server"> <input class="button" id="btnCheck" onclick=" if (! confirm( '确定审核通过 ？' ) )  return false;"
										type="button" value="审 核" name="btnCheck" runat="server" onserverclick="btnCheck_ServerClick"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp;<input class="button" id="btnClose" onclick="window.close();" type="button" value="关 闭"
										runat="server" name="btnClose">&nbsp;
								</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center" colSpan="6"><asp:label id="lblBudgetName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">调整原因：</TD>
								<TD colSpan="5">
									<asp:Label id="lblReason" runat="server"></asp:Label></TD>
							</TR>
							<tr height="3">
								<td colSpan="12"></td>
							</tr>
							<TR>
								<TD class="form-item" width="13%">申 请 人：</TD>
								<TD width="20%"><asp:label id="lblApplyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">申请时间：</TD>
								<TD width="20%"><asp:label id="lblApplyDate" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%"></TD>
								<TD width="20%"></TD>
							</TR>
							<tr height="3">
								<td colSpan="12"></td>
							</tr>
							<TR>
								<TD class="form-item" width="13%">审 核 人：</TD>
								<TD width="20%"><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">审核时间：</TD>
								<TD width="20%"><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%"></TD>
								<TD width="20%"></TD>
							</TR>
						</table>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">费用调整明细</td>
								<td></td>
							</tr>
						</table>
						<table class="list" id="tableMain" cellSpacing="0" cellPadding="0" width="98%" align="center"
							border="0">
							<TR>
								<td noWrap width="20%">费用名称</td>
								<td noWrap>调整明细</td>
							</TR>
							<asp:repeater id="repeatList" runat="server">
								<ItemTemplate>
									<tr>
										<td><a href="##" onclick="doViewDynamicCostInfo(code); return false;" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'><%# RmsPM.BLL.CBSRule.GetCostName( DataBinder.Eval(Container.DataItem, "CostCode").ToString()) %></a></td>
										<td><%# DataBinder.Eval(Container.DataItem, "AdjustDetail") %></td>
									</tr>
								</ItemTemplate>
							</asp:repeater>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
	function doDynamicModify()
	{
		window.navigate( '../Cost/DynamicApplyModify.aspx?Action=Modify&ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=<%=Request["BudgetCode"]%>','动态调整' );
	}
	
	function doViewDynamicCostInfo( costCode )
	{
		OpenFullWindow( '../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=' + costCode,'动态费用信息'  );
	}
	
//-->
		</script>
	</body>
</HTML>
