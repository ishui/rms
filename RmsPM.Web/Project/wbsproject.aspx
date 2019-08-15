<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSProject" CodeFile="WBSProject.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();
			}
			
			function InputWBS()
			{
				window.opener.InputWBS( 'WBSTempletIn.aspx&ProjectCode=<%=Request["ProjectCode"]%>',"模板导入" );
			}
			
			function OutputWBS ()
			{
				window.opener.OutputWBS( 'WBSTempletOut.aspx&ProjectCode=<%=Request["ProjectCode"]%>',"模板导出" );
			}
			
			function AddNewTask()
			{
				window.opener.AddNewTask( '<%=Request.QueryString["WBSCode"]%>' );
			}
			
			function Select(Flag1,Flag2)
			{
				window.opener.location.reload();
			}
			
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr valign="middle">
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="lblTitle" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="topic" valign="top" align="left">
						<table>
							<tr>
								<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td>
									<input type="button" class="button" id="AddNewNodeButton" onclick="AddNewTask();return false;"
										value="新增工作项">
								</td>
								<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td><input class="button" id="SaveToolsButton" type="button" value="模板导入" onclick="InputWBS();return false;">
									<input class="button" id="CancelToolsButton" onclick="OutputWBS();return false;" type="button"
										value="模板导出">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<td class="form-item">项目名称：</td>
								<TD colspan="5">
									<asp:label id="lblProjectName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<td class="form-item">项目简称：</td>
								<TD width="20%">
									<asp:label id="lblProjectShortName" runat="server"></asp:label></TD>
								<TD class="form-item">帐套：</TD>
								<TD width="20%"><asp:label id="LabelSubjectSet" runat="server"></asp:label></TD>
								<TD class="form-item">项目状态：</TD>
								<TD width="20%"><asp:label id="lblStatus" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">城市：</TD>
								<TD><asp:label id="LabelCity" runat="server"></asp:label></TD>
								<TD class="form-item">区域：</TD>
								<TD><asp:label id="LabelArea" runat="server"></asp:label></TD>
								<TD class="form-item">项目地址：</TD>
								<TD><asp:label id="lblProjectAddress" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">地块：</TD>
								<TD><asp:label id="LabelBlockName" runat="server"></asp:label></TD>
								<TD class="form-item">地块编号：</TD>
								<TD><asp:label id="LabelBlockID" runat="server"></asp:label></TD>
								<TD class="form-item">开发单位：</TD>
								<TD><asp:label id="lblDevelopUnit" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">基地：</TD>
								<TD><asp:label id="lblJD" runat="server"></asp:label></TD>
								<TD class="form-item">基地编码：</TD>
								<TD><asp:label id="lblJDBM" runat="server"></asp:label></TD>
								<TD class="form-item">基地性质：</TD>
								<TD><asp:label id="lblJDXZ" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">建筑用地：</TD>
								<TD><asp:label id="LabelBuildSpace" runat="server"></asp:label></TD>
								<TD class="form-item">总建筑用地：</TD>
								<TD><asp:label id="LabelTotalBuildingSpace" runat="server"></asp:label></TD>
								<TD class="form-item">总占地面积：</TD>
								<TD><asp:label id="LabelTotalFloorSpace" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">绿 化 率：</TD>
								<TD><asp:label id="LabelAfforestingRate" runat="server"></asp:label>%</TD>
								<TD class="form-item">计划容积率：</TD>
								<TD><asp:label id="LabelPlannedVolumeRate" runat="server"></asp:label>%</TD>
								<TD class="form-item">建筑密度：</TD>
								<TD><asp:label id="LabelBuildingDensity" runat="server"></asp:label>%</TD>
							</TR>
							<TR>
								<TD class="form-item">计容积率<br>
									的建筑面积：</TD>
								<TD><asp:label id="LabelBuildingSpaceForVolumeRate" runat="server"></asp:label></TD>
								<TD class="form-item">不计容积率<br>
									的建筑面积：</TD>
								<TD><asp:label id="LabelBuildingSpaceNotVolumeRate" runat="server"></asp:label></TD>
								<td class="form-item"></td>
								<td></td>
							</TR>
							<TR>
								<TD class="form-item">开工时间：</TD>
								<TD><asp:Label Runat="server" ID="lblkgDate"></asp:Label></TD>
								<TD class="form-item">竣工时间：</TD>
								<TD><asp:Label Runat="server" ID="lbljgDate"></asp:Label></TD>
								<TD class="form-item">销售系统项目：</TD>
								<TD><asp:Label Runat="server" ID="lblSalProjectName"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">备注：</TD>
								<TD colSpan="5"><asp:label id="LabelRemark" runat="server"></asp:label></TD>
							</TR>
						</table>
						<br>
					</td>
				</tr>
				<tr width=100% align=center valign=top height=100%><td valign=top height=100%><input type=button class=submit value="关闭窗口" onclick="doCancel();return false;"></td></tr>
			</table>
			<input type="hidden" id="txtProjectWBSCode" runat="server">
		</form>
	</body>
</HTML>
