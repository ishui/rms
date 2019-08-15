<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSubjectSet" Src="../UserControls/InputSubjectSet.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.UserModify" CodeFile="UserModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>�û�����</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�û���Ϣ</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE cellPadding="0" width="100%" class="form" cellSpacing="0">
							<TR>
								<TD class="form-item" width="20%">�� ¼ ����</TD>
								<TD width="30%">
									<asp:TextBox id="UserID" runat="server" CssClass="input"></asp:TextBox>
									<font color="red">*</font></TD>
								<TD class="form-item" width="20%">
									������</TD>
								<TD width="30%">
									<asp:TextBox id="UserName" runat="server" CssClass="input"></asp:TextBox>
									<font color="red">*</font></TD>
							</TR>
							<TR id="trPassword" runat="server">
								<TD class="form-item">
									���룺</TD>
								<TD>
									<input type="password" class="input" runat="server" id="PassWord" name="PassWord">
								</TD>
								<TD class="form-item">
									ȷ�����룺</TD>
								<TD>
									<input type="password" class="input" runat="server" id="ConfirmPassWord" name="ConfirmPassWord">
								</TD>
							</TR>
							<TR id="trOwnName" runat="server">
								<TD class="form-item">
									�������룺</TD>
								<TD>
									<input type="password" class="input" runat="server" id="OwnName" name="OwnName">
								</TD>
								<TD class="form-item">
									ȷ�ϸ������룺</TD>
								<TD>
									<input type="password" class="input" runat="server" id="ConfirmOwnName" name="ConfirmOwnName">
								</TD>
							</TR>
							<TR>
							    <TD class="form-item">�𡡡�����</TD>
								<TD>
									<asp:TextBox id="txtShortName" runat="server" CssClass="input"></asp:TextBox></TD>
								<TD class="form-item">���ţ�</TD>
								<TD>
									<asp:TextBox id="txtSortID" runat="server" CssClass="input"></asp:TextBox></TD>
								
							</TR>
							<TR>
							    <TD class="form-item">�Ա�</TD>
								<TD>
									<asp:RadioButton id="male" runat="server" GroupName="Sex"></asp:RadioButton>��&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:RadioButton id="female" runat="server" GroupName="Sex"></asp:RadioButton>Ů</TD>
								
								<TD class="form-item">
									���գ�</TD>
								<TD>
									<cc3:calendar id="Birthday" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True" Value=""></cc3:calendar></TD>
							</TR>
							<TR>
							    <TD class="form-item">�ƶ��绰��</TD>
								<TD>
									<asp:TextBox id="Mobile" runat="server" CssClass="input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Mobile"
                                        Display="Dynamic" ErrorMessage="������д�ƶ��绰��" Enabled=false ></asp:RequiredFieldValidator>
                                </TD>
								<TD class="form-item">
									�칫�绰��</TD>
								<TD>
									<asp:TextBox id="Phone" runat="server" CssClass="input"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="form-item">��ͥ�绰��</TD>
								<TD>
									<asp:TextBox id="txtPhoneHome" runat="server" CssClass="input"></asp:TextBox></TD>
								
								<TD class="form-item">
									���棺</TD>
								<TD>
									<asp:TextBox id="Fax" runat="server" CssClass="input"></asp:TextBox></TD>
							</TR>
							<TR>
							    <TD class="form-item">�����ʼ���</TD>
								<TD>
									<asp:TextBox id="MailBox" runat="server" Width="100%" CssClass="input"></asp:TextBox><br />
									<font color="red">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MailBox"
                                            Display="Dynamic" ErrorMessage="������д�����ʼ���" Enabled=false ></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="MailBox"
                                            Display="Dynamic" ErrorMessage="�����ʼ���ʽ����ȷ����some@website.com��" ValidationExpression="(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)+(;+(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*))*"></asp:RegularExpressionValidator>����ʼ��м�ʹ��[ ; ]�Ÿ���</font></TD>
								<TD class="form-item">
									��ַ��</TD>
								<TD class="tdBlankp">
									<asp:TextBox id="Address" runat="server" Width="424px" CssClass="input"></asp:TextBox></TD>
							</TR>
							<tr>
								<TD class="form-item" noWrap>״̬��</TD>
								<TD ><input type="radio" id="rdoStatus0" name="rdoStatus" runat="server" value="0">����&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="rdoStatus1" name="rdoStatus" runat="server" value="1">����</TD>
								<TD class="form-item" noWrap>ͬ��������Ϣ��</TD>
								<TD ><input type="radio" id="hrYes" name="hr" runat="server" value="0">ͬ��&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="hrNo" name="hr" runat="server" value="1" checked>��ͬ��</TD>
							</tr>
                            <tr>
                                <td class="form-item" noWrap>ǩ��ͼƬ��</td>
                                <td colspan="3">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <input id="btnDeleteSign" type="button" name="btnDeleteSign" class="button-small" value="ɾ��ǩ��ͼƬ" runat="server" onserverclick="btnDeleteSign_ServerClick" />
                                                <input id="btnUploadSign" type="button" name="btnUploadSign" class="button-small" onclick="<%=ClientID%>DoAdd();" value="�ϴ�ǩ��ͼƬ" />&nbsp;
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
								<td class="intopic" width="200">�������</td>
							</tr>
						</table>
						<uc1:InputSubjectSet id="ucInputSubjectSet" runat="server" TableName="SystemUserSubjectSet" KeyFieldName="SystemUserSubjectSetCode" CodeFieldName="UserCode"></uc1:InputSubjectSet>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="10" width="100%" border="0" height="100%">
							<tr align="center">
								<td><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
									<input class="submit" id="btnClose" onclick="window.close();" type="button" value="ȡ ��"
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
