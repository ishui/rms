document.write("<div onmouseout=\"ContentMenuPanelOnMouseOut(this);\" id=\"ContentMenuDiv\" style=\"position:absolute;display:none; z-index:99;background-color:#333333\"><iframe id=\"ContentMenuIFrame\" frameborder=\"0\" src=\"about:blank\"></iframe></div>");
var _ContentMenuDiv=eval("ContentMenuDiv");
var _ContentMenuIFrames=document.all("ContentMenuIFrame");
var _ContentMenuIFrame=eval("ContentMenuIFrame");

function CreateContentMenu(items,classFile,x,y){
	_ContentMenuDiv.style.display="none";
	_ContentMenuIFrame.document.clear();

	var html="";
	html+="<html>";
	html+="<LINK href=\""+classFile+"\" type=\"text/css\" rel=\"stylesheet\">";
	html+="<body bottomMargin=\"0\" leftMargin=\"0\" topMargin=\"0\" rightMargin=\"0\" scroll=\"no\">";
	html+="<table class=\"ContentMenuPanel\" id=\"ContentMenuItemsTable\" cellSpacing=\"0\" cellPadding=\"3\">";

	for(var i=0;i<items.length;i++)
	{
		html+="<tr>";
		html+="<td nowrap align=\"center\" style=\"cursor:hand;\">";
		html+="<table cellSpacing=\"0\" width=\"100%\" cellPadding=\"2\" class=\"ContentMenuItem\" onmouseover=\"this.className='ContentMenuItemMouseOver';\" onmouseout=\"this.className='ContentMenuItem';\" onclick=\"window.parent.frames.HideContentMenu();window.parent.frames."+items[i][2]+"\"><tr>";
		html+="<td width=\"24\">"
		html+=(items[i][1]==""?"":"<img src=\""+items[i][1]+"\" border=\"0\">");
		html+="</td>"
		html+="<td>"
		html+=items[i][0];
		html+="&nbsp;&nbsp;</td>"
		html+="</tr></table>"
		html+="</td>";
		html+="</tr>";
	}

	html+="</table>";
	html+="</body></html>";

	_ContentMenuIFrame.document.open();
	_ContentMenuIFrame.document.write(html);
	_ContentMenuIFrame.document.close();

	_ContentMenuDiv.style.display="";
	_ContentMenuDiv.style.pixelWidth=_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetWidth;
	_ContentMenuDiv.style.pixelHeight=_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetHeight;
	_ContentMenuIFrames.style.pixelWidth=_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetWidth;
	_ContentMenuIFrames.style.pixelHeight=_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetHeight;

	_ContentMenuDiv.style.pixelLeft=x;
	_ContentMenuDiv.style.pixelTop=y;
}

function ContentMenuPanelOnMouseOut(obj){
	obj.style.display="none";
}

function HideContentMenu()
{
	var menu = document.all("ContentMenuDiv");
	if (menu)	menu.style.display="none";
}
