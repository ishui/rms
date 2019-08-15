<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.BuildingFloorProgressModify" CodeFile="BuildingFloorProgressModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋形象进度</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">楼栋形象进度</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" noWrap width="120">楼栋名称：</TD>
								<TD><asp:label id="lblBuildingName" Runat="server"></asp:label></TD>
								<TD class="form-item" width="120">形象进度：</TD>
								<TD><asp:label id="lblVisualProgressName" Runat="server"></asp:label></TD>
							</TR>
							<tr>
								<TD class="form-item" noWrap>楼 层：</TD>
								<TD><asp:label id="lblFloorName" Runat="server"></asp:label></TD>
								<TD class="form-item" noWrap>工 作：</TD>
								<TD><asp:label id="lblTaskName" Runat="server"></asp:label></TD>
							</tr>
							<TR>
								<TD class="form-item">状态：</TD>
								<td><select runat="server" id="sltStatus" name="sltStatus" class="select" onchange="sltStatusChange();">
										<option value="0">未开始</option>
										<option value="1">进行中</option>
										<option value="2">已完成</option>
									</select>
								</td>
								<TD class="form-item" id="tdCompletePercentCaption">完成百分比：</TD>
								<td id="tdCompletePercent"><input type="text" class="input-nember" size="3" id="txtCompletePercent" name="txtCompletePercent"
										runat="server">%</td>
							</TR>
							<TR>
								<TD class="form-item">计划开始日期：</TD>
								<TD noWrap><cc3:calendar id="txtPStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
								<TD class="form-item">计划结束日期：</TD>
								<TD noWrap><cc3:calendar id="txtPEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True" Value=""></cc3:calendar></TD>
							</TR>
							<TR>
								<TD class="form-item" id="tdStartDateCaption">实际开始日期：</TD>
								<TD noWrap id="tdStartDate"><cc3:calendar id="txtStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
								<TD class="form-item" id="tdEndDateCaption">实际结束日期：</TD>
								<TD noWrap id="tdEndDate"><cc3:calendar id="txtEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True" Value=""></cc3:calendar></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" onclick="document.all.divHintSave.style.display='';"
										name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server"><input id="txtBuildingFloorCode" name="txtBuildingFloorCode" runat="server">
			<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"><input id="txtVisualProgressCode" type="hidden" name="txtVisualProgressCode" runat="server">
			<input id="txtProgressCode" type="hidden" name="txtProgressCode" runat="server">
		</form>
<script language="javascript">

function winload()
{
	sltStatusChange();
}

function sltStatusChange()
{
	var status = Form1.sltStatus.value;
	
	if (status == "0")
	{
		//未开始
		document.all.tdStartDateCaption.style.display = "none";
		document.all.tdStartDate.style.display = "none";
		document.all.tdEndDateCaption.style.display = "none";
		document.all.tdEndDate.style.display = "none";
		
		document.all.tdCompletePercentCaption.style.display = "none";
		document.all.tdCompletePercent.style.display = "none";
	}
	if (status == "1")
	{
		//进行中
		document.all.tdStartDateCaption.style.display = "";
		document.all.tdStartDate.style.display = "";
		document.all.tdEndDateCaption.style.display = "none";
		document.all.tdEndDate.style.display = "none";
		
		document.all.tdCompletePercentCaption.style.display = "";
		document.all.tdCompletePercent.style.display = "";
	}
	if (status == "2")
	{
		//已完成
		document.all.tdStartDateCaption.style.display = "";
		document.all.tdStartDate.style.display = "";
		document.all.tdEndDateCaption.style.display = "";
		document.all.tdEndDate.style.display = "";
		
		document.all.tdCompletePercentCaption.style.display = "none";
		document.all.tdCompletePercent.style.display = "none";
	}
}

</script>
	</body>
</HTML>
