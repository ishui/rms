
function CreateContentMenu(id,items,classFile,x,y){
	eval(id+'_ContentMenuDiv.style.display="none";');
	eval(id+'_ContentMenuIFrame.document.clear();');

	var html="";
	html+="<html>";
	html+="<LINK href=\""+classFile+"\" type=\"text/css\" rel=\"stylesheet\">";
	html+="<body bottomMargin=\"0\" leftMargin=\"0\" topMargin=\"0\" rightMargin=\"0\" scroll=\"no\">";
	html+="<table class=\"ContentMenuPanel\" id=\"ContentMenuItemsTable\" cellSpacing=\"0\" cellPadding=\"3\">";

	for(var i=0;i<items.length;i++)
	{
		html+="<tr>";
		html+="<td nowrap align=\"center\" style=\"cursor:hand;\">";
		html+="<table cellSpacing=\"0\" width=\"100%\" cellPadding=\"2\" class=\"ContentMenuItem\" onmouseover=\"this.className='ContentMenuItemMouseOver';\" onmouseout=\"this.className='ContentMenuItem';\" onclick=\"window.parent.frames."+items[i][2]+"\"><tr>";
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

	eval(id+'_ContentMenuIFrame.document.open();');
	eval(id+'_ContentMenuIFrame.document.write(html);');
	eval(id+'_ContentMenuIFrame.document.close();')

	eval(id+'_ContentMenuDiv.style.display="";');
	eval(id+'_ContentMenuDiv.style.pixelWidth='+id+'_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetWidth;');
	eval(id+'_ContentMenuDiv.style.pixelHeight='+id+'_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetHeight;');
	eval(id+'_ContentMenuIFrames.style.pixelWidth='+id+'_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetWidth;');
	eval(id+'_ContentMenuIFrames.style.pixelHeight='+id+'_ContentMenuIFrame.document.all("ContentMenuItemsTable").offsetHeight;');

	eval(id+'_ContentMenuDiv.style.pixelLeft=x;');
	eval(id+'_ContentMenuDiv.style.pixelTop=y;');
}

function ContentMenuPanelOnMouseOut(obj){
	obj.style.display="none";
}

function HideContentMenu()
{
	var menu = document.all("ContentMenuDiv");
	if (menu)	menu.style.display="none";
}
