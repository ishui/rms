<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.UserInfo" CodeFile="UserInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>用户信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
	function doModifyUser()
	{
		window.location = "UserModify.aspx?UserCode=<%=Request["UserCode"]%>";
//		OpenMiddleWindow('UserModify.aspx?UserCode=<%=Request["UserCode"]%>','编辑用户');
	}
	
		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">用户信息</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnModify" onclick="doModifyUser();return false;" type="button"
							value="修 改" name="btnModify" runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnClose" onclick="window.close();" type="button" value="关 闭"
							name="btnClose" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="20%">登 录 名：</TD>
								<TD width="20%"><asp:label id="lblUserID" runat="server"></asp:label></TD>
								<TD class="form-item" width="20%">姓名：</TD>
								<TD width="20%"><asp:label id="lblUserName" runat="server"></asp:label></TD>
							</TR>
							<TR>
							    <TD class="form-item">别　　名：</TD>
								<TD>
									<asp:label id="lblShortName" runat="server"></asp:label></TD>
								<TD class="form-item">工号：</TD>
								<TD>
									<asp:label id="lblSortID" runat="server"></asp:label></TD>
								
							</TR>
							<TR>
							    <TD class="form-item">性别：</TD>
								<TD>
									<asp:label id="lblSex" runat="server"></asp:label></TD>
								
								<TD class="form-item">生日：</TD>
								<TD>
									<asp:label id="lblBirthDay" runat="server"></asp:label></TD>
							</TR>
							<TR>
							    <TD class="form-item">移动电话：</TD>
								<TD>
									<asp:label id="lblMobile" runat="server"></asp:label></TD>
								<TD class="form-item">办公电话：</TD>
								<TD>
									<asp:label id="lblPhone" runat="server"></asp:label></TD>
								
							</TR>
							<TR>
							    <TD class="form-item">家庭电话：</TD>
								<TD>
									<asp:label id="lblPhoneHome" runat="server" DESIGNTIMEDRAGDROP="426"></asp:label></TD>
								<TD class="form-item">传真：</TD>
								<TD><asp:label id="lblFax" runat="server"></asp:label></TD>
							</TR>
							<TR>
							    <TD class="form-item">电子邮件：</TD>
								<TD><asp:label id="lblEMail" runat="server"></asp:label></TD>
								<TD class="form-item">地址：</TD>
								<TD class="tdBlankp" ><asp:label id="lblAddress" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap>岗位：</TD>
								<TD noWrap colSpan="3" id="tdStationName" runat="server"></TD>
							</TR>
							<tr>
								<TD class="form-item" noWrap>状态：</TD>
								<TD colspan="3"><asp:label id="lblU" runat="server"></asp:label></TD>
							</tr>
							<tr>
								<TD class="form-item">财务编码：</TD>
								<TD noWrap colspan="3"><asp:Label Runat="server" ID="lblSubjectSetDesc"></asp:Label></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
		</form>
	</body>
</HTML>
