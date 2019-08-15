<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicApplyInfo" CodeFile="DynamicApplyInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��̬����������Ϣ</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��̬����������Ϣ</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%">
							<tr>
								<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td><input class="button" id="btnModify" onclick="doDynamicModify(); return false;" type="button"
										value="�� ��" name="btnModify" runat="server"> <input class="button" id="btnCheck" onclick=" if (! confirm( 'ȷ�����ͨ�� ��' ) )  return false;"
										type="button" value="�� ��" name="btnCheck" runat="server" onserverclick="btnCheck_ServerClick"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
										type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp;<input class="button" id="btnClose" onclick="window.close();" type="button" value="�� ��"
										runat="server" name="btnClose">&nbsp;
								</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center" colSpan="6"><asp:label id="lblBudgetName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">����ԭ��</TD>
								<TD colSpan="5">
									<asp:Label id="lblReason" runat="server"></asp:Label></TD>
							</TR>
							<tr height="3">
								<td colSpan="12"></td>
							</tr>
							<TR>
								<TD class="form-item" width="13%">�� �� �ˣ�</TD>
								<TD width="20%"><asp:label id="lblApplyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">����ʱ�䣺</TD>
								<TD width="20%"><asp:label id="lblApplyDate" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%"></TD>
								<TD width="20%"></TD>
							</TR>
							<tr height="3">
								<td colSpan="12"></td>
							</tr>
							<TR>
								<TD class="form-item" width="13%">�� �� �ˣ�</TD>
								<TD width="20%"><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">���ʱ�䣺</TD>
								<TD width="20%"><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%"></TD>
								<TD width="20%"></TD>
							</TR>
						</table>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">���õ�����ϸ</td>
								<td></td>
							</tr>
						</table>
						<table class="list" id="tableMain" cellSpacing="0" cellPadding="0" width="98%" align="center"
							border="0">
							<TR>
								<td noWrap width="20%">��������</td>
								<td noWrap>������ϸ</td>
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
		window.navigate( '../Cost/DynamicApplyModify.aspx?Action=Modify&ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=<%=Request["BudgetCode"]%>','��̬����' );
	}
	
	function doViewDynamicCostInfo( costCode )
	{
		OpenFullWindow( '../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=' + costCode,'��̬������Ϣ'  );
	}
	
//-->
		</script>
	</body>
</HTML>
