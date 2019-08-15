<%@ Register TagPrefix="uc1" TagName="Control_CreatStyle" Src="Control_CreatStyle.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.EditControl.test" CodeFile="test.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>流程导入导出系统</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<table height="100%" width="100%">
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" background="../images/topic_bg.gif"
							border="0">
							<tr>
								<td class="topic" width="100%" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									系统管理 - 流程导入导出
								</td>
								<td><IMG height="25" src="../images/topic_corr.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD class="tools-area" vAlign="top">
						<P><IMG src="../images/btn_li.gif" align="absMiddle"><INPUT class="button" id="BT_OutAll" onclick="javascript:if(!window.confirm('将覆盖原有的备份,确实要导出数据吗?')) return false;"
								type="button" value="导出数据" runat="server" onserverclick="BT_OutAll_ServerClick"> <input class="button" id="BT_OutWorkFlow" onclick="javascript:if(!window.confirm('将清空原有流程表,确实要导入数据吗?')) return false;"
								type="button" value="导入数据" runat="server" onserverclick="BT_OutWorkFlow_ServerClick"></P>
					</TD>
				</tr>
				<tr>
					<TD class="tools-area" vAlign="top">
						<P><IMG src="../images/btn_li.gif" align="absMiddle"><INPUT id="UpFile" style="WIDTH: 232px; HEIGHT: 22px" type="file" size="19" name="UpFile"><INPUT class="button" id="BT_Up" onclick="javascript:if(!window.confirm('将覆盖原有的备份,确实要上传吗?')) return false;"
								type="button" value="上传文件" name="Button1" runat="server" onserverclick="BT_Up_ServerClick"></P>
					</TD>
				</tr>
				<tr>
					<td><FONT face="宋体"></FONT></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
