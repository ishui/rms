<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl"  %>
<%@ Page language="c#" Inherits="RmsPM.Web.Remind.Notify" CodeFile="Notify.aspx.cs" ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>公告</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();			
			}
			
			function ClientValidate(source, arguments)　　
			{ 
　　			arguments.IsValid=(arguments.Value.length!=0);　　　
　　		}

　　		
　　		function GetLastRight(s, val) 
　　		{
				var i = s.lastIndexOf(val);
				if (i > 0)
					return s.substring(i + 1);
				else
					return s;
			}
			function SelectPerson()
　　		{
　　			OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.txtUsers.value+"&StationCodes="+window.document.all.txtStations.value+"");
　　		}
　　		function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
			{
				window.document.all.txtUsers.value = userCodes;	
				window.document.all.txtStations.value = stationCodes;	
				window.document.all.SelectName.innerText = getString(userNames,stationNames);	
				window.document.all.hSelectName.innerText = getString(userNames,stationNames);	
			}
			function getString(str1,str2)
			{
				if(str1.length>0&&str2.length>0)
				{
					return str1+','+str2;
				}
				else
					return str1+str2;
			}
//			function doCheck()
//			{
//				if(window.document.all.txtTitle.value.length<1)
//				{
//					alert("请输入标题");
//					return false;
//				}
//				if(window.document.all.taContent.value.length<1)
//				{
//					alert("请输入内容");
//					return false;
//				}	

//				return true;
//			}
		</SCRIPT>

        <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
        <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body bottomMargin="0" bgColor="white" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" colSpan="2" height="25"><asp:label id="lblTitle" runat="server"></asp:label></td>
				</tr>
				<tr vAlign="top" width="100%">
					<td>
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR width="100%" runat="server" id="trNotice">
								<TD class="form-item" align="right" style="width: 194px; ">类型：</TD>
                                    <td>
                                        <SELECT id="DDLNoticeClass" size="1" name="sltNotice" runat="server">
										    <OPTION value="" selected>------请选择------</OPTION>
									        </SELECT><font color="red">*</font>
                                    </td>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" style="width: 194px; ">标题：</TD>
								<TD ><input class="input" id="txtTitle" style="WIDTH: 320px; HEIGHT: 18px" type="text" name="txtTitle"
										runat="server" size="44" visible="true">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Height="1px" Text="*" Width="1px"></asp:Label>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" style="width: 194px">附件：</TD>
								<TD colSpan="3">
									<uc1:AttachMentAdd id="myAttachMentAdd" runat="server"></uc1:AttachMentAdd></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" style="width: 194px">发布范围：</TD>
								<TD colSpan="3">
									<input id="txtUsers" type="hidden" name="txtUsers" runat="server"> <input id="txtStations" type="hidden" name="txtStations" runat="server">
									<input type="button" id="btSelectUser" value="选择分发范围" class="button-small" OnClick="SelectPerson();return false;"
										NAME="btSelectUser"><font color="#ff0033">(没有选择范围则向全体人员发布)</font>
									<div id="SelectName" runat="server"></div>
								</TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" style="width: 194px">内容：</TD>
								<TD colSpan="3">&nbsp;<textarea class="textarea" id="taContent" style="HEIGHT: 100px" name="taContent" cols="40"
										runat="server"></textarea>
										<input type="hidden" runat="server" ID="htType" value="" /> 
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr vAlign="top" width="100%">
					<td colSpan="2" height="100%">
						<table align="center">
							<tr align="center" width="100%">
								<td><input class="submit" id="SaveToolsButton" type="button" value="发 送" name="SaveToolsButton"
									 runat="server" onserverclick="SaveToolsButton_ServerClick"></td>
								<td>
									<input class="submit" id="btDelete" type="button" value="删除" name="btDelete" runat="server" onserverclick="btDelete_ServerClick" visible="false">
								</td>
								<td><input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="取 消" name="CancelToolsButton" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="hUserFlag" name="hUserFlag" runat="server" value="0"> <input type="hidden" id="hUserCode" name="hUserCode" runat="server">
			<input type="hidden" id="hSelectName" name="hSelectName">
		</form>
		<script language="javascript">
		var tmp = '<%=Request["hSelectName"]%>';
		if(tmp.length>0)
		{
			window.document.all.SelectName.innerText = '<%=Request["hSelectName"]%>';
			window.document.all.hSelectName.value = '<%=Request["hSelectName"]%>';
		}
		</script>
	</body>
</HTML>
