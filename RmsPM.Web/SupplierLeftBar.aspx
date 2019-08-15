<%@ Page language="c#" Inherits="RmsPM.Web.SupplierLeftBar" CodeFile="SupplierLeftBar.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InvestmentLeftBar</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Images/menu.css" rel="stylesheet" type="text/css">
		<SCRIPT language="JavaScript" src="Images/showmenu.js"></SCRIPT>
		<SCRIPT language="JavaScript">
<!--
function MM_reloadPage(init) {  //reloads the window if Nav4 resized
  if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
    document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
  else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
}
MM_reloadPage(true);
// -->

function MM_showHideLayers() { //v3.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v='hide')?'hidden':v; }
    obj.visibility=v; }
}
//-->
		</SCRIPT>
		<SCRIPT language="JScript">
function checkKey()
{
	var key = window.event.keyCode;
	if (window.event.shiftKey)
	{
		if (key == 65 || key == 97)
		{
			ShowAll();
		}
		else if (key == 67 || key == 99)
		{
			CloseAll();
		}
	}
}

function ShowAll()
{
	for(var i = 0; i < document.all.length; i++)
	{
	   if (document.all(i).className == "collapsed")
	   {
		document.all(i).className = "expanded" ;
	   }
	}
}

function CloseAll()
{
	for(var i = 0; i < document.all.length; i++)
	{
	   if (document.all(i).className == "expanded")
	   {
		document.all(i).className = "collapsed" ;
	   }
	}
}

function outliner()
{
    var child = document.all[event.srcElement.getAttribute("child",false)];
    if (null != child){
		if(child.className == "collapsed")
		{
            CloseAll();
			child.className = "expanded";
			return;
		}
		if(child.className == "expanded")
		{
			child.className = "collapsed";
			return;
		}
    }
}

function ShowFirst()
{
	var tdGroup = document.all("tdGroup0");
	
	if (tdGroup)
	{
		tdGroup.click();;
	}
}

function gotoUrl(url)
{
	window.open(url,'main');
}

function doIniBody()
{
	//window.open('desktop.aspx','main');
	ShowFirst();
}


//-->
		</SCRIPT>
		<SCRIPT language="JScript" event="onkeypress" for="DocBody">
<!--
checkKey();

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}
//-->
		</SCRIPT>
		<style> .collapsed { DISPLAY: none }
	.line { CURSOR: hand }
		</style>
	</HEAD>
	<body bgcolor="#e4eff6" leftmargin="5" topmargin="0" onLoad="doIniBody();">
		<table width="148" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
			<tr>
				<td width="167" height="17"><img src="Images/menu_top.jpg" width="148" height="17"></td>
			</tr>
			<tr>
				<td valign="top" class="bg">
					<table width="100%" border="0" cellpadding="0" cellspacing="0" class="table">
						<tr>
							<td>
								<table width="100%" height="37" border="0" cellpadding="0" cellspacing="0" class="topic"
									background="Images/menu_bg.gif">
									<tr>
										<td valign="middle" width="1"><%--<img src="Images/menu_topic.jpg" align="absMiddle">--%></td>
										<td><div id="divOAName" runat="server"></div>
										    数据资料
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td id="tdBar" runat="server">
							</td>
						</tr>
				  </table>
				</td>
			</tr>
			<tr>
				<td height="6" valign="top"></td>
			</tr>
		</table>
	</body>
</HTML>
