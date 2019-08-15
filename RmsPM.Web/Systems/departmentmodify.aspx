<%@ Page language="c#" Inherits="RmsPM.Web.Systems.DepartmentModify" CodeFile="DepartmentModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputSubjectSet" Src="../UserControls/InputSubjectSet.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>部门信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
	<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script>
		function SelectUnit()
		{
			OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=100009");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.parentUnitName.value = name;
			window.document.all.parentUnit.value = code;
		}	
		</script>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<TBODY>
					<tr>
						<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="LabelTitle" runat="server" BorderColor="Transparent" BackColor="Transparent"
								CssClass="TitleText"></asp:label>&nbsp;-
							<asp:label id="lblParentUnitName" runat="server" CssClass="TitleText"></asp:label></td>
					</tr>
					<tr height="100%">
						<td vAlign="top" align="left">
							<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="20%">部门名称：</TD>
									<TD width="30%"><asp:textbox id="TextBoxName" runat="server" CssClass="input" Width="150px"></asp:textbox><font color="red">*</font></TD>
									<TD class="form-item" width="20%">部门编号：</TD>
									<TD width="30%"><asp:textbox id="txtSortID" runat="server" CssClass="input" Width="150px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="form-item" style="height: 33px">部门负责人：</TD>
									<TD style="height: 33px"><SELECT id="sltPrincipal" style="WIDTH: 152px" name="sltPrincipal" runat="server">
											<OPTION value="" selected>------请选择------</OPTION>
										</SELECT></TD>
									<TD class="form-item" style="height: 33px">部门类型：</TD>
									<TD style="height: 33px"><select id="sltUnitType" runat="server">
											<option value="部门" selected>部门</option>
											<option value="公司">公司</option>
										</select></TD>
								</TR>
								<TR>
									<TD class="form-item">
                                        上级部门 ：</TD>
                                    <td colspan="5">
                                        &nbsp;<input id="parentUnit" runat="server" class="input" name="parentUnit"
                                            style="width: 33px; height: 18px" type="hidden" /><input id="parentUnitName" runat="server"
                                                class="input" name="parentUnitName" style="width: 133px; height: 18px" type="text" readonly="readOnly" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif" style="cursor: hand" /></td>
                                   <%-- <td class="form-item">
                                        帐套：</td>
									<TD colspan="3">&nbsp;独立核算 <INPUT id="chkSelfAccount" onclick="showSubjectSet();" type="checkbox" runat="server">&nbsp;
										<SELECT id="sltSubjectSet" style="DISPLAY: none" name="Select1" runat="server">
											<OPTION value="" selected>------请选择------</OPTION>
										</SELECT></TD>--%>
								</TR>
								<TR>
									<TD class="form-item">备注：</TD>
									<TD colSpan="3"><asp:textbox id="TextBoxRemark" runat="server" Width="80%" Height="60px" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
							</TABLE>
							<br>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="intopic" width="200">财务编码</td>
								</tr>
							</table>
							<uc1:InputSubjectSet id="ucInputSubjectSet" runat="server" TableName="UnitSubjectSet" KeyFieldName="UnitSubjectSetCode"
								CodeFieldName="UnitCode"></uc1:InputSubjectSet>
						</td>
					</tr>
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="10" width="100%" border="0">
								<tr align="center">
									<td><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
										<input class="submit" id="btnClose" onclick="window.close();" type="button" value="取 消"
											name="btnClose" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
			<INPUT id="txtInputUnitCode" type="hidden" name="txtInputUnitCode" runat="server"><INPUT id="txtAction" type="hidden" name="txtAction" runat="server">
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
		</form>
<%--		<script language="javascript">
<!--

	showSubjectSet();

	function showSubjectSet()
	{
		var chk = Form1.chkSelfAccount.checked;
		if ( chk )
			Form1.sltSubjectSet.style.display = "";
		else
			Form1.sltSubjectSet.style.display = "none";
	}


//-->
		</script>--%>
		
	</body>
</HTML>
