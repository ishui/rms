<%@ Page Language="c#" Inherits="RmsPM.Web.Desktop" CodeFile="Desktop.aspx.cs" %>

<%@ Register Src="DeskTopControls/control_rpnotice.ascx" TagName="control_rpnotice"
    TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Desktop</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="full" name="WebPartPageExpansion">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Images/index.css" type="text/css" rel="stylesheet">
    <style type="text/css">TABLE.txt TD { COLOR: #aaaaaa; PADDING-TOP: 5px; BORDER-BOTTOM: #e7e7e7 1px dotted }
	.toptxt { COLOR: #ff0000 }
	</style>

    <script language="javascript" src="Rms.js"></script>

    <script language="javascript">
			
			//打开工作报告属性项
			function OpenExecute(WBSCode,ExecuteCode,ProjectCode)
			{
				OpenMiddleWindow('Project/WBSExecuteInfo.aspx?TaskExecuteCode=' + ExecuteCode + '&ActionState=Modify&WBSCode='+WBSCode+"&ProjectCode="+ProjectCode,'');
			}
			function ModifyNotice(Code)
			{
				OpenFullWindow('Remind/Notify.aspx?&Code=' + Code+'&Action=Modify','');
			}
			function OpenTask(Code,ProjectCode)
			{
				OpenFullWindow('Project/WBSInfo.aspx?WBSCode=' + Code+"&ProjectCode="+ProjectCode,"");
			}
			function OpenAttention(Url)// url已经保存到数据库中，
			{
				OpenFullWindow(Url,"");
			}
			function GoMore(Url)
			{
				window.location.href=Url;
			}
			function OpenAudit(Url)
			{
				window.location.href = Url;
			}
			function OpenRemind(Url)
			{
				window.location.href = Url;
			}
			function DoNewNotice(Url)
			{
				OpenMiddleWindow(Url,"");
			}
			function refresh()
			{
				window.document.Form1.submit();
			}			
			function OpenMoreUnderWayTask(ProjectCode) //此时无projectcode
			{
				window.location.href = "Project/WBSStatusUnderWay.aspx?TaskStatus=1&ProjectCode="+ProjectCode;
			}
			function OpenMoreOverTimeTask(ProjectCode)
			{
				window.location.href = "Project/WBSStatusOverTime.aspx?Exceed=1&ProjectCode="+ProjectCode;
			}
			function OpenMoreTaskExecute(ProjectCode)
			{
				window.location.href = "Project/WBSExecuteList.aspx?ProjectCode="+ProjectCode;
			}
			function OpenMoreWBSAttention(ProjectCode)
			{
				window.location.href = "Project/WBSAttention.aspx?ProjectCode="+ProjectCode;
			}
			function OpenDesktopManage()
			{
			    var deskType = '<%=Request["DesktopType"] %>';
				window.open("EditControl/EditDestopNum.aspx?DesktopType="+deskType+"","桌面编辑","Width=190,Height=290");
			}
			function OpenDesktopSet()
			{
				OpenMiddleWindow("EditControl/DestopManage.aspx");			
			}
			function doChangePWD()
	        {
		        OpenCustomWindow('Systems/ChgPwd.aspx?ModifyPwd=pwd','修改密码', 280, 150);
	        }
	        
	        function OpenGoLink(url)
	        {
	            window.open(url,"系统链接");
	        }		
    </script>

    <link href="../images/index.css" rel="stylesheet" type="text/css" />
    <link href="../images/index.css" rel="stylesheet" type="text/css" />
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <table style="border-right: #e4eff6 5px solid" height="100%" cellspacing="0" cellpadding="0"
            width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="images/topic_bg.gif">
                                <img height="25" src="images/topic_li.jpg" align="absMiddle">
                                <asp:Label Font-Size="9pt" ForeColor="white" runat="server" ID="deskLabel"></asp:Label>
                                </td>
                            <td class="topic" background="images/topic_bg.gif">
                            </td>
                            <td class="topic" align="right" background="images/topic_bg.gif">
                            </td>
                            <td class="topic" align="right" background="images/topic_bg.gif">
                                <font face="宋体">
                                    <asp:Label ID="IsAllow" runat="server" Visible="False" BorderStyle="None" Font-Underline="True"
                                        ForeColor="White" Width="60px">主页管理</asp:Label>
                                    <asp:Label ID="Lb_DesktopM" runat="server" BorderStyle="None" Font-Underline="True"
                                        ForeColor="White" Width="60px">桌面设置</asp:Label>
                                    <asp:Label ID="lblChangePWD" runat="server" BorderStyle="None" Font-Underline="True"
                                        ForeColor="White" Width="60px">更改密码</asp:Label>
                                </font>
                            </td>
                            <td width="9">
                                <img height="25" src="images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <div>
                    </div>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" style="padding-right: 0px; padding-left: 10px; padding-bottom: 0px;
                    padding-top: 10px" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td valign="top" width="63%" height="100%">
                                    <table width="98%" align="left">
                                        <tr>
                                            <td id="LeftPane" style="border-top-width: 1px; border-right: 1px solid; border-left-width: 1px;
                                                border-bottom-width: 1px" runat="server">
                                                <font face="宋体">
                                                    <uc1:control_rpnotice ID="Control_rpnotice1" runat="server" IsOther="true" Visible="false" />
                                                </font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="33%" runat="server">
                                    <table width="98%" align="left">
                                        <tr>
                                            <td id="RightPane" runat="server">
                                                <font face="宋体"></font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="bottom" height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td valign="bottom" background="images/corl_bg.gif">
                                <img height="12" src="images/corl.gif" width="12"></td>
                            <td valign="bottom" width="12">
                                <img height="12" src="images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="mytime" bgcolor="#e4eff6" height="6" runat="server">
                    <p>
                    </p>
                    <p>
                        <font face="宋体"></font>
                    </p>
                </td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
