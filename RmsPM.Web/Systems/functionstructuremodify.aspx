<%@ Reference Control="~/usercontrols/inputsubject.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.FunctionStructureModify" CodeFile="FunctionStructureModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ܵ���Ϣ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���ܵ���Ϣ</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnSave" type="button" value="�� ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('��ɾ���ù��ܵ�����������еĹ��ܵ㣬�Ƿ�ɾ�� ��')) return false;"
							type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp; <input class="button" id="btnClose" onclick="window.close();" type="button" value="�� ��"
							runat="server">
					</td>
				</TR>
				<tr>
					<td vAlign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="15%">�ϼ����ܵ㣺</TD>
								<TD colSpan="3"><asp:label id="labelParent" runat="server" CssClass="Lable"></asp:label><FONT face="����">&nbsp; 
										�ӹ��ܵ���Ϊ�����ܵ����ټ�����λ������ϸ��д������У��</FONT></TD>
							</TR>
							<TR>
								<TD class="form-item" width="15%">���ܵ���룺</TD>
								<TD width="35%"><input class="input" id="txtFunctionStructureCode" type="text" runat="server"><FONT color="red">*</FONT></TD>
								<TD class="form-item" width="15%">���ܵ����ƣ�</TD>
								<TD width="35%"><input class="input" id="txtFunctionStructureName" type="text" name="Text1" runat="server"><font color="red">*</font></TD>
							</TR>
							<tr>
								<td class="form-item"></td>
								<TD colSpan="3"><input id="chkIsAvailable" type="checkbox" CHECKED value="chkIsAvailable" name="chkIsAvailable"
										runat="server"> ��Ч&nbsp; �����һ���ڵ���Ч����ô���������нڵ㶼��Ч�� ��֮�����һ���ڵ���Ч�����ĸ��ڵ��Ȼ��Ч��
								</TD>
							</tr>
							<tr>
								<td class="form-item"></td>
								<TD colSpan="3"><input id="chkRight" type="checkbox" CHECKED value="chkRight" name="RadioGroup" runat="server">
									��Ȩ�޿���
								</TD>
							</tr>
							<tr>
								<td class="form-item"></td>
								<TD><input id="chkRole" type="checkbox" CHECKED value="chkRole" name="RadioGroup" runat="server">
									�ɽ�ɫ������п���</TD>
								<TD colSpan="2"><input id="chkSystemClass" type="checkbox" value="chkSystemClass" name="RadioGroup"
										runat="server"> ��ϵͳ���Ĵ��� (&nbsp;���ɽ�ɫ������п��ƣ���ϵͳ�������ƵĴ��� )</TD>
							</tr>
							<tr>
								<TD class="form-item">˵ ����</TD>
								<TD colSpan="3"><textarea id="txtDescription" style="WIDTH: 80%" rows="2" runat="server"></textarea></TD>
							</tr>
							<tr>
								<TD class="form-item">��Ŀ�ر�˵����</TD>
								<TD colSpan="3"><textarea id="txtProjectSpecialDescription" style="WIDTH: 80%" rows="2" runat="server"></textarea></TD>
							</tr>
							<tr>
								<TD class="form-item">�����ر�˵����</TD>
								<TD colSpan="3"><textarea id="txtOtherSpecialDescription" style="WIDTH: 80%" rows="2" runat="server"></textarea></TD>
							</tr>
						</table>
						<table id="tableList" cellSpacing="10" width="100%" runat="server">
							<tr>
								<td vAlign="top">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="intopic" width="200">�ӹ��ܵ�</td>
											<td><input class="button-small" id="btnAddChild" onclick="InsertFunctionStructure(); return false;"
													type="button" value="�������ܵ�" name="btnAddChild" runat="server"></td>
										</tr>
									</table>
									<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
										<tr class="list-title" align="center">
											<td noWrap width="30%">���ܵ�</td>
											<td noWrap>˵��</td>
										</tr>
										<asp:repeater id="repeatList" runat="server">
											<ItemTemplate>
												<tr class="list-i">
													<td nowrap width="50%">
														<a href="#"  onclick='ModifyFunctionStructure(this)' code='<%# DataBinder.Eval(Container.DataItem, "FunctionStructureCode") %>' >
															<%# DataBinder.Eval(Container.DataItem, "FunctionStructureCode") %>
															<%# DataBinder.Eval(Container.DataItem, "FunctionStructureName") %>
														</a>
													</td>
													<td nowrap><%# DataBinder.Eval(Container.DataItem, "Description") %></td>
												</tr>
											</ItemTemplate>
										</asp:repeater></table>
								</td>
							</tr>
						</table>
						<table id="TableSQLScript" height="60%" cellSpacing="10" width="100%" runat="server">
							<tr>
								<td vAlign="top">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="intopic" width="200">SQL Script</td>
										</tr>
									</table>
									<table class="list" cellSpacing="5" cellPadding="0" width="100%" border="0">
										<tr>
											<td>
												<textarea id="txtSQLScript" style="WIDTH: 95%" name="txtSQLScript" rows="3" readOnly runat="server"></textarea>
												<script language="JavaScript">
												WriteTextAreaControl('txtSQLScript',true);
												</script>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="txtInputCode" type="hidden" name="txtInputCode" runat="server">
		</form>
		<script language="javascript">
<!--
	function InsertFunctionStructure(){
		window.open('FunctionStructureModify.aspx?Action=AddChild&FunctionStructureCode=<%=Request["FunctionStructureCode"]%>');
	}
	
	
	function ModifyFunctionStructure ( obj )
	{
		window.open("FunctionStructureModify.aspx?Action=Modify&FunctionStructureCode=" +obj.code);
	}


//-->
		</script>
	</body>
</HTML>
