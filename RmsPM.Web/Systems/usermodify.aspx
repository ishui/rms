<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSubjectSet" Src="../UserControls/InputSubjectSet.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.UserModify" CodeFile="UserModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>用户管理</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<Script language="javascript">

		</SCRIPT>
</HEAD>
	<body scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">用户信息</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE cellPadding="0" width="100%" class="form" cellSpacing="0">
							<TR>
								<TD class="form-item" width="20%">登 录 名：</TD>
								<TD width="30%">
									<asp:TextBox id="UserID" runat="server" CssClass="input"></asp:TextBox>
									<font color="red">*</font></TD>
								<TD class="form-item" width="20%">
									姓名：</TD>
								<TD width="30%">
									<asp:TextBox id="UserName" runat="server" CssClass="input"></asp:TextBox>
									<font color="red">*</font></TD>
							</TR>
							<TR id="trPassword" runat="server">
								<TD class="form-item">
									密码：</TD>
								<TD>
									<input type="password" class="input" runat="server" id="PassWord" name="PassWord">
								</TD>
								<TD class="form-item">
									确认密码：</TD>
								<TD>
									<input type="password" class="input" runat="server" id="ConfirmPassWord" name="ConfirmPassWord">
								</TD>
							</TR>
							<TR id="trOwnName" runat="server">
								<TD class="form-item">
									辅助密码：</TD>
								<TD>
									<input type="password" class="input" runat="server" id="OwnName" name="OwnName">
								</TD>
								<TD class="form-item">
									确认辅助密码：</TD>
								<TD>
									<input type="password" class="input" runat="server" id="ConfirmOwnName" name="ConfirmOwnName">
								</TD>
							</TR>
							<TR>
							    <TD class="form-item">别　　名：</TD>
								<TD>
									<asp:TextBox id="txtShortName" runat="server" CssClass="input"></asp:TextBox></TD>
								<TD class="form-item">工号：</TD>
								<TD>
									<asp:TextBox id="txtSortID" runat="server" CssClass="input"></asp:TextBox></TD>
								
							</TR>
							<TR>
							    <TD class="form-item">性别：</TD>
								<TD>
									<asp:RadioButton id="male" runat="server" GroupName="Sex"></asp:RadioButton>男&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:RadioButton id="female" runat="server" GroupName="Sex"></asp:RadioButton>女</TD>
								
								<TD class="form-item">
									生日：</TD>
								<TD>
									<cc3:calendar id="Birthday" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True" Value=""></cc3:calendar></TD>
							</TR>
							<TR>
							    <TD class="form-item">移动电话：</TD>
								<TD>
									<asp:TextBox id="Mobile" runat="server" CssClass="input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Mobile"
                                        Display="Dynamic" ErrorMessage="必须填写移动电话！" Enabled=false ></asp:RequiredFieldValidator>
                                </TD>
								<TD class="form-item">
									办公电话：</TD>
								<TD>
									<asp:TextBox id="Phone" runat="server" CssClass="input"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="form-item">家庭电话：</TD>
								<TD>
									<asp:TextBox id="txtPhoneHome" runat="server" CssClass="input"></asp:TextBox></TD>
								
								<TD class="form-item">
									传真：</TD>
								<TD>
									<asp:TextBox id="Fax" runat="server" CssClass="input"></asp:TextBox></TD>
							</TR>
							<TR>
							    <TD class="form-item">电子邮件：</TD>
								<TD>
									<asp:TextBox id="MailBox" runat="server" Width="100%" CssClass="input"></asp:TextBox><br />
									<font color="red">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MailBox"
                                            Display="Dynamic" ErrorMessage="必须填写电子邮件！" Enabled=false ></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="MailBox"
                                            Display="Dynamic" ErrorMessage="电子邮件格式不正确！（some@website.com）" ValidationExpression="(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)+(;+(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*))*"></asp:RegularExpressionValidator>多个邮件中间使用[ ; ]号隔开</font></TD>
								<TD class="form-item">
									地址：</TD>
								<TD class="tdBlankp">
									<asp:TextBox id="Address" runat="server" Width="424px" CssClass="input"></asp:TextBox></TD>
							</TR>
							<tr>
								<TD class="form-item" noWrap>状态：</TD>
								<TD ><input type="radio" id="rdoStatus0" name="rdoStatus" runat="server" value="0">启用&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="rdoStatus1" name="rdoStatus" runat="server" value="1">禁用</TD>
								<TD class="form-item" noWrap>同步人事信息：</TD>
								<TD ><input type="radio" id="hrYes" name="hr" runat="server" value="0">同步&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="hrNo" name="hr" runat="server" value="1" checked>不同步</TD>
							</tr>
                            <tr>
                                <td class="form-item" noWrap>签名图片：</td>
                                <td colspan="3">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <input id="btnDeleteSign" type="button" name="btnDeleteSign" class="button-small" value="删除签名图片" runat="server" onserverclick="btnDeleteSign_ServerClick" />
                                                <input id="btnUploadSign" type="button" name="btnUploadSign" class="button-small" onclick="<%=ClientID%>DoAdd();" value="上传签名图片" />&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><asp:Image ID="imgSign" runat="server" Width="120"/></td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>							
						</TABLE>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">财务编码</td>
							</tr>
						</table>
						<uc1:InputSubjectSet id="ucInputSubjectSet" runat="server" TableName="SystemUserSubjectSet" KeyFieldName="SystemUserSubjectSetCode" CodeFieldName="UserCode"></uc1:InputSubjectSet>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="10" width="100%" border="0" height="100%">
							<tr align="center">
								<td><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
									<input class="submit" id="btnClose" onclick="window.close();" type="button" value="取 消"
										runat="server" NAME="btnClose">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtOldUserID" type="hidden" name="txtOldUserID" runat="server" />
			<input id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server" />
			<input id="txtRoleCode" type="hidden" name="txtRoleCode" runat="server" /> 
			<input id="txtAct" type="hidden" name="txtAct" runat="server" />
			<input id="txtUserCode" type="hidden" name="txtUserCode" runat="server" />
		</form>
<script language="javascript">

function winload()
{
	Form1.PassWord.value = "1*#06#";
	Form1.ConfirmPassWord.value = Form1.PassWord.value;
}

	function <%=ClientID%>DoAdd()
	{
		OpenCustomWindow("../UserControls/SaveAttach.aspx?strMasterCode=<%=strMasterCode%>&strAttachMentType=<%=strAttachMentType%>&SingleFile=1",null,400,300);
	}
    function Refresh()
	{
		window.document.forms[0].submit();
	}

</script>
	</body>
</HTML>
