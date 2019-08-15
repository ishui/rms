<%@ Reference Control="~/usercontrols/inputsubject.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.CBSBatchModify" CodeFile="CBSBatchModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>批量修改费用结构</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="if(event.keyCode==13) event.keyCode=9" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">批量修改费用结构</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div id="tbl-container">
						<table width="100%" class="tbl-list" border="0" cellpadding="0" cellspacing="0">
							<tr align="center" class="list-title">
								<th noWrap>费用项名称</th>
								<th noWrap>费用项编号</th>
								<th noWrap>科目编号</th>
								<th noWrap>费用分解说明</th>
								<th noWrap>备 注</th>
							</tr>
							<asp:repeater id="repeat1" runat="server">
								<ItemTemplate>
									<tr>
										<td nowrap class='<%# "list-" +  DataBinder.Eval(Container.DataItem, "Deep").ToString() %>'><%# DataBinder.Eval(Container.DataItem, "CostName") %>
											<input type=hidden runat=server id=txtCostCode value='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' NAME="txtCostCode">
											<input type=hidden runat=server id="txtCostName" value='<%# DataBinder.Eval(Container.DataItem, "CostName") %>' NAME="txtCostName">
										</td>
										<td nowrap align="right"><input name="txtSortID" type="text" id="txtSortID" size=8  class=input runat="server"  value='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'></td>
										<td nowrap ><uc1:InputSubject id="ucInputSubject" runat="server"
										ProjectCode='<%# DataBinder.Eval(Container, "DataItem.ProjectCode") %>'
										 value='<%# DataBinder.Eval(Container, "DataItem.SubjectCode") %>'
																></uc1:InputSubject></td>
										<td nowrap align="right"><input name="txtCostAllocationDescription" type="text" id="txtCostAllocationDescription"   class=input runat="server"  value='<%# DataBinder.Eval(Container, "DataItem.CostAllocationDescription") %>'></td>
										<td nowrap align="right"><input name="txtDescription" type="text" id="txtDescription"   class=input runat="server"  value='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></td>
									</tr>
								</ItemTemplate>
							</asp:repeater>
						</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<TR>
								<TD align="center"><asp:button id="btnSave" runat="server" CssClass="submit" Text="确 定" onclick="btnSave_Click"></asp:button>&nbsp;
									<INPUT class="submit" onclick="window.close();" type="button" value="取 消">
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
