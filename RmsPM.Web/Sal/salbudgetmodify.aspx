<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalBudgetModify" CodeFile="SalBudgetModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>销售计划修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="SalBudget.js" charset="gb2312"></SCRIPT>
	</HEAD>
	<body scroll="no" onkeydown="if(event.keyCode==13) event.keyCode=9">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">销售计划</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note"><asp:label id="lblBudgetName" runat="server"></asp:label></td>
							</tr>
						</table>
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="100">计划周期：</TD>
								<TD><asp:label id="lblPeriodMonthDesc" runat="server"></asp:label></TD>
								<TD class="form-item" width="100">后续计划：</TD>
								<TD><asp:Label Runat="server" ID="lblAfterPeriodDesc"></asp:Label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table class="list" cellSpacing="0" cellPadding="0" id="tableList">
								<tr class="list-title" align="center">
									<td colSpan="2" rowSpan="2" width="80">&nbsp;</td>
									<td rowSpan="2" nowrap>产品类型</td>
									<td id="titleYear0" rowSpan="2" nowrap>期前累计</td>
									<td id="titleYear" colSpan="13"></td>
									<td id="titleYear1"></td>
									<td id="titleYear2"></td>
								</tr>
								<tr class="list-title" align="center">
									<td width="80">1</td>
									<td width="80">2</td>
									<td width="80">3</td>
									<td width="80">4</td>
									<td width="80">5</td>
									<td width="80">6</td>
									<td width="80">7</td>
									<td width="80">8</td>
									<td width="80">9</td>
									<td width="80">10</td>
									<td width="80">11</td>
									<td width="80">12</td>
									<td width="80" nowrap>年度计划</td>
									<td nowrap>年度计划</td>
									<td nowrap>年度计划</td>
								</tr>
								<asp:repeater id="dgList" Runat="server">
									<ItemTemplate>
										<tr id='mytr<%# DataBinder.Eval(Container.DataItem, "TrID")%>'
											PBSTypeCode='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'
											ParentCode='<%# DataBinder.Eval(Container.DataItem, "ParentCode")%>'
											Deep='<%# DataBinder.Eval(Container.DataItem, "Deep")%>'
											FieldName='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'
											IsAct='<%# DataBinder.Eval(Container.DataItem, "IsAct")%>'
										>
											<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
											<td>
												<input type="hidden" runat="server" name="txtFieldName" id="txtFieldName" value='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'>
												<input type="hidden" runat="server" name="txtPBSTypeCode" id="txtPBSTypeCode" value='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM1" id="txtM1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM2" id="txtM2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM3" id="txtM3"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m3"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM4" id="txtM4"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m4"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM5" id="txtM5"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m5"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM6" id="txtM6"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m6"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM7" id="txtM7"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m7"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM8" id="txtM8"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m8"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM9" id="txtM9"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m9"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM10" id="txtM10"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m10"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM11" id="txtM11"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m11"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM12" id="txtM12"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m12"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM0" id="txtM0"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m0"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY1" id="txtY1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY2" id="txtY2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
										</tr>
									</ItemTemplate>
								</asp:repeater>
								<tr>
								</tr>
								<asp:repeater id="dgListArea" Runat="server">
									<ItemTemplate>
										<tr id='mytr<%# DataBinder.Eval(Container.DataItem, "TrID")%>'
											PBSTypeCode='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'
											ParentCode='<%# DataBinder.Eval(Container.DataItem, "ParentCode")%>'
											Deep='<%# DataBinder.Eval(Container.DataItem, "Deep")%>'
											FieldName='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'
											IsAct='<%# DataBinder.Eval(Container.DataItem, "IsAct")%>'
										>
											<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
											<td>
												<input type="hidden" runat="server" name="txtFieldName" id="txtFieldName" value='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'>
												<input type="hidden" runat="server" name="txtPBSTypeCode" id="txtPBSTypeCode" value='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM1" id="txtM1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM2" id="txtM2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM3" id="txtM3"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m3"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM4" id="txtM4"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m4"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM5" id="txtM5"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m5"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM6" id="txtM6"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m6"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM7" id="txtM7"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m7"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM8" id="txtM8"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m8"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM9" id="txtM9"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m9"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM10" id="txtM10"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m10"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM11" id="txtM11"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m11"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM12" id="txtM12"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m12"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM0" id="txtM0"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m0"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY1" id="txtY1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY2" id="txtY2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
										</tr>
									</ItemTemplate>
								</asp:repeater>
								<tr>
								</tr>
								<asp:repeater id="dgListPrice" Runat="server">
									<ItemTemplate>
										<tr id='mytr<%# DataBinder.Eval(Container.DataItem, "TrID")%>'
											PBSTypeCode='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'
											ParentCode='<%# DataBinder.Eval(Container.DataItem, "ParentCode")%>'
											Deep='<%# DataBinder.Eval(Container.DataItem, "Deep")%>'
											FieldName='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'
											IsAct='<%# DataBinder.Eval(Container.DataItem, "IsAct")%>'
										>
											<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
											<td>
												<input type="hidden" runat="server" name="txtFieldName" id="txtFieldName" value='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'>
												<input type="hidden" runat="server" name="txtPBSTypeCode" id="txtPBSTypeCode" value='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM1" id="txtM1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM2" id="txtM2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM3" id="txtM3"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m3"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM4" id="txtM4"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m4"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM5" id="txtM5"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m5"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM6" id="txtM6"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m6"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM7" id="txtM7"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m7"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM8" id="txtM8"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m8"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM9" id="txtM9"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m9"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM10" id="txtM10"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m10"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM11" id="txtM11"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m11"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM12" id="txtM12"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m12"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM0" id="txtM0"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m0"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY1" id="txtY1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY2" id="txtY2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
										</tr>
									</ItemTemplate>
								</asp:repeater>
								<tr>
								</tr>
								<asp:repeater id="dgListMoney" Runat="server">
									<ItemTemplate>
										<tr id='mytr<%# DataBinder.Eval(Container.DataItem, "TrID")%>'
											PBSTypeCode='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'
											ParentCode='<%# DataBinder.Eval(Container.DataItem, "ParentCode")%>'
											Deep='<%# DataBinder.Eval(Container.DataItem, "Deep")%>'
											FieldName='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'
											IsAct='<%# DataBinder.Eval(Container.DataItem, "IsAct")%>'
										>
											<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
											<td>
												<input type="hidden" runat="server" name="txtFieldName" id="txtFieldName" value='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'>
												<input type="hidden" runat="server" name="txtPBSTypeCode" id="txtPBSTypeCode" value='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM1" id="txtM1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM2" id="txtM2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM3" id="txtM3"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m3"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM4" id="txtM4"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m4"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM5" id="txtM5"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m5"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM6" id="txtM6"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m6"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM7" id="txtM7"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m7"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM8" id="txtM8"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m8"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM9" id="txtM9"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m9"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM10" id="txtM10"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m10"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM11" id="txtM11"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m11"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM12" id="txtM12"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m12"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM0" id="txtM0"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m0"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY1" id="txtY1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY2" id="txtY2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
										</tr>
									</ItemTemplate>
								</asp:repeater>
								<tr>
								</tr>
								<asp:repeater id="dgListRcvMoney" Runat="server">
									<ItemTemplate>
										<tr id='mytr<%# DataBinder.Eval(Container.DataItem, "TrID")%>'
											PBSTypeCode='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'
											ParentCode='<%# DataBinder.Eval(Container.DataItem, "ParentCode")%>'
											Deep='<%# DataBinder.Eval(Container.DataItem, "Deep")%>'
											FieldName='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'
											IsAct='<%# DataBinder.Eval(Container.DataItem, "IsAct")%>'
										>
											<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
											<td>
												<input type="hidden" runat="server" name="txtFieldName" id="txtFieldName" value='<%# DataBinder.Eval(Container.DataItem, "FieldName")%>'>
												<input type="hidden" runat="server" name="txtPBSTypeCode" id="txtPBSTypeCode" value='<%# DataBinder.Eval(Container.DataItem, "PBSTypeCode")%>'>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM1" id="txtM1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM2" id="txtM2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM3" id="txtM3"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m3"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM4" id="txtM4"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m4"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM5" id="txtM5"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m5"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM6" id="txtM6"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m6"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM7" id="txtM7"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m7"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM8" id="txtM8"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m8"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM9" id="txtM9"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m9"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM10" id="txtM10"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m10"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM11" id="txtM11"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m11"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM12" id="txtM12"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m12"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtM0" id="txtM0"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "m0"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY1" id="txtY1"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y1"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
											<td>
												<input type="text" class="input-nember" size="8" runat="server" name="txtY2" id="txtY2"
													value='<%# RmsPM.BLL.SalBudgetRule.FormatSalBudgetFieldValue(DataBinder.Eval(Container.DataItem, "y2"), (string)DataBinder.Eval(Container.DataItem, "FieldName"))%>'>
											</td>
										</tr>
									</ItemTemplate>
								</asp:repeater>
								<tr>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" onclick="if (!Save()) return false;"
										runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtYear" type="hidden" name="txtYear" runat="server">
			<input id="txtProjectName" type="hidden" name="txtProjectName" runat="server"><input id="txtBudgetCode" type="hidden" name="txtBudgetCode" runat="server">
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server"><INPUT id="txtNodeExpandStatus" type="hidden" name="txtNodeExpandStatus" runat="server">
			<INPUT id="txtAllImageID" type="hidden" name="txtAllImageID" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//显示年份标题
function DisplayTitle()
{
	var year = parseInt(Form1.txtYear.value);
	
	document.all.titleYear.innerText = year;
	document.all.titleYear1.innerText = year + 1;
	document.all.titleYear2.innerText = year + 2;
}

function Save()
{
	GetNodeExpandStatus();
	
	document.all.divHintSave.style.display = '';
	return true;
}

function GetNodeExpandStatus()
{
	var arr = Form1.txtAllImageID.value.split(",");
	
	var status = "";
	for(var i=0;i<arr.length;i++)
	{
		var ele = document.all(arr[i]);
		
		if (ele)
		{ 
			if (status != "") status = status + ",";
			
			status = status + ele.id + "=" + ele.exp;
		}
	}
	
//	alert(status);
	Form1.txtNodeExpandStatus.value = status;
}

function SetNodeExpandStatus()
{
	var arr = Form1.txtNodeExpandStatus.value.split(",");
	
	for(var i=0;i<arr.length;i++)
	{
		if (arr[i] != "")
		{
			var arr2 = arr[i].split("=");

			var ele = document.all(arr2[0]);
			
			if (ele)
			{ 
				if (arr2[1] == "1")
				{
					NodeExpand(ele.PBSTypeCode, ele.Deep, ele.FieldName, ele.IsAct);
				}
				else
				{
					NodeCollapse(ele.PBSTypeCode, ele.Deep, ele.FieldName, ele.IsAct);
				}
			}
		}
	}
	
//	alert(status);
	Form1.txtNodeExpandStatus.value = status;
}

DisplayTitle();

var HasLoadGrid = '<%=ViewState["HasLoadGrid"]%>';
if (HasLoadGrid == "1")
{
	CollapseAllRoot();
}
else
{
	SetNodeExpandStatus();
}

//var eles = document.getElementsByTagName("img");
//alert(eles.length);
//-->
		</SCRIPT>
	</body>
</HTML>
