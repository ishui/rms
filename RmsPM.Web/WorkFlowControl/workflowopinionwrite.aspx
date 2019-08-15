<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowOpinionWrite" CodeFile="WorkFlowOpinionWrite.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">填写流程意见</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="1" rowSpan="1">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table id="OpinionTitle" cellSpacing="0" cellPadding="0" border="0" runat="server">
										<tr>
											<td class="intopic">处理意见</td>
											<td><input id="ChkShow" type="checkbox" name="ChkShow" runat="server" style="DISPLAY: none"
													checked></td>
													<td>
													<select id="sltTemplateOpinion" runat="server" visible="false">
                    </select>
													</td>
										</tr>
									</table>
									<table class="input" id="OpinionTable" width="100%" runat="server" name="OpinionTable">
										<tr>
											<td align="left"><TEXTAREA rows="20" runat="server" id="FlowOpinion" style="WIDTH: 100%; HEIGHT: 324px" cols="118"
													NAME="FlowOpinion"></TEXTAREA>
												<uc1:attachmentadd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:attachmentadd></td>
										</tr>
									</table>
									<br>
									<table width="100%">
										<tr>
											<td align="center"><input class="submit" id="btnSend" onclick="returnSaveOpinion(); " type="button" value="确 定">
												<input class="submit" id="btnCancel" onclick="window.close();" type="button" value="取 消"
													name="btnCancel" runat="server">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script>
		function returnSaveOpinion()
		{
			if ( Form1.FlowOpinion.value == "" )
			{
				alert ('请填写意见内容 ！');
				return false;
			}
			if(document.Form1.ChkShow)
				window.opener.returnSaveOpinion(Form1.FlowOpinion.value,document.Form1.ChkShow.checked);
			else
				window.opener.returnSaveOpinion(Form1.FlowOpinion.value,"false");
		
			window.close();
		}
		</script>
	</body>
</HTML>
