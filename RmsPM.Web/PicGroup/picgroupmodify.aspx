<%@ Page language="c#" Inherits="RmsPM.Web.PicGroup.PicGroupModify" CodeFile="PicGroupModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>相册管理</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<Script language="javascript">
<!--

function checkSubmit(obj){
	if( ""==obj.TxtGroupName.value ){
		alert("请输入相册名称！");
		obj.TxtGroupName.focus();
		return false;
	}
	return true;
}

function doUploadPic(){
	var strURL = "./PicUpload.aspx?PBSPicCode=";
	
	strURL += "&MasterType=PicGroupLarge";
	strURL += "&MasterCode=" + Form1.HidePBSPicGroupCode.value;

	var NowTime = new Date();
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString() ;
	var theWin = OpenLargeWindow(strURL,"doUploadPic");
	theWin.focus();
}

function doModifyPic(strCode){
	var strURL = "./PicUpload.aspx?PBSPicCode=" + strCode;
	
	var NowTime = new Date();
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString() ;
	var theWin = OpenLargeWindow(strURL,"doUploadPic");
	theWin.focus();
}

//-->
		</Script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server" onsubmit="return checkSubmit(this);">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="3"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">相册管理</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">&nbsp;相册名称：<INPUT class="input" type="text" id="TxtGroupName" name="TxtGroupName" runat="server" size="40">&nbsp;&nbsp;<INPUT class="button" type="submit" value="确 定" id="btnSubmit" name="btnSubmit" runat="server" onserverclick="btnSubmit_ServerClick">&nbsp;&nbsp;
						<INPUT class="button" id="btnDel" type="button" value="删除相册" name="btnDel" runat="server" onclick="if (!confirm('确实要删除相册吗？')) return false;" onserverclick="btnDel_ServerClick">&nbsp;&nbsp;
						<INPUT class="button" id="btnUploadPic" type="button" value="添加图片" name="btnUploadPic"
							runat="server" onclick="doUploadPic();return false;">
					</td>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><FONT face="宋体">
								<asp:DataList id="dlPicList" runat="server" Width="100%" HorizontalAlign="Center" RepeatDirection="Horizontal"
									RepeatColumns="4" ShowHeader="False" ShowFooter="False" GridLines="Both" CellPadding="5" CssClass="list">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container, "DataItem.PicTitle") %>
										<br>
										<a href="#" onclick='doModifyPic("<%# DataBinder.Eval(Container, "DataItem.PBSPicCode") %>");return false;'>
											<img alt="点击图片进行修改" border="0" width="120" src='./PicShow.aspx?PicCode=<%# DataBinder.Eval(Container, "DataItem.PBSPicCode") %>'></a>
									</ItemTemplate>
								</asp:DataList></FONT></div>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<INPUT type="hidden" id="HideMasterType" name="HideMasterType" runat="server"><INPUT type="hidden" id="HideMasterCode" name="HideMasterCode" runat="server"><INPUT type="hidden" id="HidePBSPicGroupCode" name="HidePBSPicGroupCode" runat="server">
		</form>
	</body>
</HTML>
