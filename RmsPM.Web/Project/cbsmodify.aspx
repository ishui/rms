<%@ Page language="c#" Inherits="RmsPM.Web.Project.CBSModify" CodeFile="CBSModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用项信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/XmlCom.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Finance/Subject.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">费用项信息</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnSave" type="button" value="保 存" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnBatchModify" onclick="BatchModify(); return false;" type="button"
										value="批量修改" name="btnCancel" runat="server"> <input class="button" id="btnClose" onclick="window.close();" type="button" value="关 闭"
										runat="server">
					</td>
				</tr>
				<tr>
					<td>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="15%" nowrap>费用名称：</TD>
								<TD><asp:textbox id="txtCostName" runat="server" CssClass="input"></asp:textbox><font color="red">*</font></TD>
								<TD class="form-item" width="15%" nowrap>上级费用项：</TD>
								<TD><uc1:inputcost id="ucParent" runat="server" SelectAllLeaf="true"></uc1:inputcost>
								    <select runat="server" id="sltParent" name="sltParent" class="select" visible="false">
								                    <option value="">----请选择----</option>
								                </select>
								    <asp:label Visible="false" id="labelParentCostName" runat="server" CssClass="Lable"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item" nowrap>费用项编码：</TD>
								<TD><asp:textbox id="txtSortID" runat="server" CssClass="input"></asp:textbox><FONT color="red">*</FONT></TD>
								<TD class="form-item" nowrap>费用分解说明：</TD>
								<TD><asp:textbox id="txtCostAllocationDescription" runat="server" CssClass="input"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="form-item" nowrap>会计科目：</TD>
								<TD colspan="3"><uc1:InputSubject id="ucInputSubject" runat="server"></uc1:InputSubject></TD>
							</TR>
							<tr>
								<TD class="form-item" nowrap>预算类别：</TD>
								<TD colspan="3"><uc1:inputgroup id="ucBudgetType" runat="server" ClassCode="0411"></uc1:inputgroup></TD>
							</tr>
							<TR>
								<TD class="form-item" nowrap>备注：</TD>
								<TD colSpan="3"><asp:textbox id="txtDescription" runat="server" CssClass="input" Width="504px"></asp:textbox></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<table width="100%" id="tableList" runat="server" height="100%">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0" width="100%">
										<tr>
											<td class="intopic" width="200">子费用项</td>
											<td><input class="button-small" id="btnAddChild" onclick="InsertCBS(); return false;" type="button"
													value="新增费用项" name="btnAddChild" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div id="tbl-container">
									<table width="100%" align="center" class="tbl-list" border="0" cellpadding="0" cellspacing="0">
										<tr align="center" class="list-title">
											<th noWrap>费用项</th>
											<th noWrap>科目</th>
										</tr>
										<asp:repeater id="repeatList" runat="server">
											<ItemTemplate>
												<tr class="list-i">
													<td nowrap width="50%">
														<a href="#"  onclick='ModifyCBS(this)' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' >
															<%# DataBinder.Eval(Container.DataItem, "SortID") %>
															<%# DataBinder.Eval(Container.DataItem, "CostName") %>
														</a>
													</td>
													<td nowrap><%# DataBinder.Eval(Container, "DataItem.SubjectCode") %>
														<%# RmsPM.BLL.SubjectRule.GetSubjectName( DataBinder.Eval(Container, "DataItem.SubjectCode").ToString(), DataBinder.Eval(Container, "DataItem.SubjectSetCode").ToString() )%>
													</td>
												</tr>
											</ItemTemplate>
										</asp:repeater>
									</table>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<INPUT id="txtTempCostCode" type="hidden" name="txtTempCostCode" runat="server">
			<input id="txtRelationSno" type="hidden" name="txtRelationSno" runat="server"><input id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server">
		</form>
		<script language="javascript">
<!--
	function InsertCBS(){
			OpenLargeWindow('CBSModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Action=AddChild&CostCode=<%=Request["CostCode"]%>',"添加费用项");
	}
	
	// 从当前的费用项打开,批量修改明细信息
	function BatchModify()
	{
		OpenFullWindow( 'CBSBatchModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=<%=Request["CostCode"]%>','批量修改费用结构');
	}
	
	function ModifyCBS ( obj )
	{
		OpenLargeWindow("CBSModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Action=Modify&CostCode=" +obj.code,'费用项信息');
	}


//-->
		</script>
	</body>
</HTML>
