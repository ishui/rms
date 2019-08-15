<%@ Page language="c#" Inherits="RmsPM.Web.PicGroup.PicList" CodeFile="PicList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PicList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<Script language="javascript" src="../Rms.js"></Script>
		<SCRIPT language="VBScript">
Function CreateSafeArrayVB(p_iLength)
 Dim a()
 Redim a(p_iLength)
 CreateSafeArrayVB = a
End Function
Function SetSafeArrayElementVB(p_array, p_iIndex, p_value)
 p_array(p_iIndex) = p_value
 SetSafeArrayElementVB = p_array
End Function
		</SCRIPT>
		<SCRIPT language="JScript">
var iBodyLoadCount = 0;
function BodyLoaded()
{
 iBodyLoadCount++;
 if (iBodyLoadCount == 3)
 {
 Start();
 }
}
var blnIsUnloading = false;
function BodyUnloaded()
{
 blnIsUnloading = true;
 Finish();
}

var thePicGroupXMLData='PicXMLData.aspx?MasterCode=<%=Request.QueryString["PBSPicGroupCode"]+""%>';
		</SCRIPT>
		<LINK id="locCss" type="text/css" rel="stylesheet">
		<SCRIPT language="JScript" src="../Images/PicGroup/Startup.js"></SCRIPT>
	</HEAD>
	<BODY ondblclick="DebugClicked()" style="BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; OVERFLOW: hidden; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; BACKGROUND-COLOR: #ffffff"
		onload="BodyLoaded()" onunload="BodyUnloaded()">
		<xml id="xmlToLoad"></xml><IFRAME id="frameControl" src="about:blank" width="0" height="0"></IFRAME>
		<TABLE id="tblLoading" style="POSITION: absolute" height="100%" width="100%">
			<TBODY>
				<TR>
					<TD vAlign="middle" align="center" width="100%" height="100%">
						<SCRIPT language="JScript">
 document.write('<img src="../Images/PicGroup/splash.gif" width=67 height=58><br>');
						</SCRIPT>
						<SPAN id="cellLoading" style="PADDING-RIGHT: 30px; PADDING-LEFT: 30px; FONT-WEIGHT: normal; FONT-SIZE: 200%; ; FONT-SIZE: expression(document.body.clientWidth / 20); PADDING-BOTTOM: 0px; LINE-HEIGHT: normal; PADDING-TOP: 0px; FONT-STYLE: normal; FONT-VARIANT: normal">
							&nbsp;</SPAN></TD>
				</TR>
			</TBODY>
		</TABLE>
		<DIV id="divMain" style="HEIGHT: 1px"></DIV>
		<SCRIPT language="JScript">
BodyLoaded();
		</SCRIPT>
	</BODY>
</HTML>
