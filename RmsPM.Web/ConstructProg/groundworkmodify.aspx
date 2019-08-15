<%@ Register TagPrefix="uc1" TagName="InputTask" Src="../UserControls/InputTask.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.GroundWorkModify" CodeFile="GroundWorkModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工程剖面图</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><input type="button" runat="server" id="btnRefresh" name="btnRefresh" value="btnRefresh" onserverclick="btnRefresh_ServerClick"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">工程剖面图</td>
				</tr>
				<tr>
					<td valign="top">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" noWrap width="100">工程：</TD>
								<TD><uc1:InputTask id="ucTask" runat="server" OnChange="TaskChange();"></uc1:InputTask></TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap width="100">背景图：</TD>
								<td><a style="CURSOR:hand" onclick="ViewAttach(this.code);" id="hrefBg" runat="server"><asp:Label Runat="server" ID="lblBgImageName"></asp:Label></a>&nbsp;<input type="button" class="button-small" id="btnUpload" name="btnUpload" value="上 传" runat="server"
										onclick="UploadBg();"></td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<table cellSpacing="0" cellPadding="0" height="100%" width="100%">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="note" height="25">请注意：区域图片背景必须为蓝色(#0000ff)，前景透明</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">区域列表</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
										<table class="list" cellpadding="0" cellspacing="0" width="100%">
											<tr class="list-title">
												<td>区域名称</td>
												<td>图片</td>
											</tr>
											<asp:Repeater Runat="server" ID="dgList">
												<ItemTemplate>
													<tr>
														<td><%# DataBinder.Eval(Container, "DataItem.TaskName")%></td>
														<td>
															<a style="CURSOR:hand" onclick="ViewAttach(this.code);" id="A1" code='<%# DataBinder.Eval(Container, "DataItem.AttachMentCode")%>' runat="server">
																<%# DataBinder.Eval(Container, "DataItem.ImageName")%>
															</a><span style='display:<%# DataBinder.Eval(Container, "DataItem.Deep").ToString()=="1"?"":"none"%>'> &nbsp;<input type="button" class="button-small" id="Button1" name="btnUploadDtl" value="上 传" runat="server"
																onclick="UploadDtl(this.code, this.WBSCode);" code='<%# DataBinder.Eval(Container, "DataItem.AttachMentCode")%>' WBSCode='<%# DataBinder.Eval(Container, "DataItem.WBSCode")%>' runat="server">
																</span>
														</td>
													</tr>
												</ItemTemplate>
											</asp:Repeater>
										</table>
									</div>
								</td>
							</tr>
						</table>
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
			<input id="txtGroundWorkCode" type="hidden" name="txtGroundWorkCode" runat="server">
		</form>
		<script language="javascript">

function winload()
{
}

function ViewAttach(code)
{
	OpenMiddleWindow("../Project/WBSAttachMentView.aspx?Action=View&AttachMentCode=" + code, "");
}

//上传平面图
function UploadBg()
{
	var AttachMentCode = document.all.hrefBg.code;
	var WBSCode = document.all.ucTask_txtCode.value;
	OpenCustomWindow("../UserControls/SaveAttach.aspx?AttachMentCode=" + AttachMentCode + "&strMasterCode=" + WBSCode + "&strAttachMentType=GroundWork", null,400,300);
}

//上传图
function UploadDtl(AttachMentCode, WBSCode)
{
	OpenCustomWindow("../UserControls/SaveAttach.aspx?AttachMentCode=" + AttachMentCode + "&strMasterCode=" + WBSCode + "&strAttachMentType=GroundWork", null,400,300);
}

function Refresh()
{
	Form1.btnRefresh.click();
}

function TaskChange()
{
	Refresh();
}

		</script>
	</body>
</HTML>
