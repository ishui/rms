<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomModelModify" CodeFile="RoomModelModify.aspx.cs" AutoEventWireup="true" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>户型修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">户型修改</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="15%" class="form-item">户型</TD>
								<TD width="35%"><INPUT id="txtModelName" type="text" class="input" maxLength="50" size="20" name="txtModelName"
										runat="server"><font color="red">*</font></TD>
								<TD width="15%" class="form-item">房型</TD>
								<TD width="35%"><SELECT id="sltStructure" size="1" name="sltStructure" runat="server">
										<OPTION value="" selected>------请选择------</OPTION>
									</SELECT><font color="red">*</font></TD>
							</TR>
							<tr>
								<TD class="form-item">产品类型</TD>
								<TD colspan="3"><SELECT id="sltPBSTypeCode" class="select" name="sltPBSTypeCode" runat="server">
										<OPTION value="" selected>------请选择------</OPTION>
									</SELECT><font color="red">*</font></TD>
							</tr>
							<TR>
								<TD class="form-item">建筑面积</TD>
								<TD><igtxt:webnumericedit id="txtBuildArea" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
										MinDecimalPlaces="Two"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">套内面积</TD>
								<TD><igtxt:webnumericedit id="txtRoomArea" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
										MinDecimalPlaces="Two"></igtxt:webnumericedit>平米</TD>
							</TR>
							<TR>
								<TD class="form-item">图片路径</TD>
								<TD colspan="3"><INPUT id="FileUpload" class="input" style="WIDTH:100%" type="file" size="1" name="File1"
										runat="server"></TD>
							</TR>
							<TR>
								<TD class="form-item">备注</TD>
								<TD colspan="3"><TEXTAREA rows="4" class="textarea" style="WIDTH:100%" id="txtRemark" name="txtRemark" runat="server"></TEXTAREA>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick" />
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()"/>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtPicCode" type="hidden" name="txtPicCode" runat="server">
			<input id="txtModelCode" type="hidden" name="txtModelCode" runat="server"> <input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
