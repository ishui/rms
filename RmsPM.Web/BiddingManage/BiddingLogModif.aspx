<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingLogModif.aspx.cs" Inherits="BiddingManage_BiddingLogModif" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>招投标日志</title>
    <meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	<script language="javascript" src="../Rms.js"></script>
	
</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">招投标日志</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
				
						    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							    <TR>
								    <TD width="80" class="form-item">修改人：</TD>
								    <TD align=left runat=server id="TdUpdateMan">&nbsp;
								    </TD>
							    </TR>
							    <TR>
								    <TD width="80" class="form-item">修改日期：</TD>
								    <TD align=left runat="server" id="tdCreateDate">&nbsp;
								    </TD>
							    </TR>
							    <tr>
							        <TD width="80" class="form-item">原金额：</TD>
								    <TD align=left runat="server" id="tdFormerMoney">&nbsp;
								    </TD>
							    </tr>
							    <tr>
							        <TD width="80" class="form-item">修改后金额：</TD>
								    <TD align=left runat="server" id="tdTeamMoney">&nbsp;
								    </TD>
							    </tr>
						    </table>
					
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
										<input id="btnMore" name="btnMore" type="button" onclick="javascript:OpenLargeWindow('BiddingLogList.aspx?BiddingCode=<%= BiddingCode %>','招投标日志');" class="submit" value="更多...">
								        <input id="btnCancel" name="btnCancel" type="button" class="submit" value="关闭" onclick="javascript:self.close()">
								
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtSystemID" type="hidden" name="txtSystemID" runat="server">
		</form>
		<script language="javascript">
	        function BiddingLog()
            {

                OpenLargeWindow('BiddingLogList.aspx','招投标日志');
            }
        </script>
	</body>
</HTML>
