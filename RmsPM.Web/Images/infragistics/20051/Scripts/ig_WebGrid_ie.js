/* 
Infragistics UltraWebGrid Script 
Version 5.1.20051.37
Copyright (c) 2001-2005 Infragistics, Inc. All Rights Reserved.
*/

function igtbl_button(gn,evnt)
{
	if(evnt.button==1)
		return 0;
	else if(evnt.button==4)
		return 1;
	else if(evnt.button==2)
		return 2;
	return -1;
}

function igtbl_srcElement(evnt)
{
	return evnt.srcElement;
}

function igtbl_changeStyle(gn,se,style)
{
	if(style==null)
	{
		se.runtimeStyle.cssText="";
		return;
	}
	var te=igtbl_getGridById(gn).Element;
	var rules=null;
	if(te.getAttribute(style))
		rules=te.getAttribute(style);
	else
	{
		rules=new Array();
		var styles=style.split(' ');
		for(var k=0;k<styles.length;k++)
			for(var i=0;i<document.styleSheets.length;i++)
			{
				var bFoundStyle = false;
				for(var j=0;j<document.styleSheets[i].rules.length;j++)
					if(document.styleSheets[i].rules[j].selectorText=="."+styles[k])
					{
						bFoundStyle=true;
						rules[rules.length]=document.styleSheets[i].rules[j];
						break;
					}
				for(var j=0;!bFoundStyle && j<document.styleSheets[i].imports.length;j++)
					for(var m=0;!bFoundStyle && m<document.styleSheets[i].imports[j].rules.length;m++)
					if(document.styleSheets[i].imports[j].rules[m].selectorText=="."+styles[k])
					{
						bFoundStyle=true;
						rules[rules.length]=document.styleSheets[i].imports[j].rules[m];
						break;
					}
			}
		if(rules.length==0)
		{
			delete rules;
			return;
		}
		te.setAttribute(style,rules);
	}
	se.runtimeStyle.cssText="";
	for(var i=0;i<rules.length;i++)
		se.runtimeStyle.cssText+=(se.runtimeStyle.cssText?";":"")+rules[i].style.cssText;
}

function igtbl_adjustLeft(e)
{
	return igtbl_getLeftPos(e);
}

function igtbl_adjustTop(e)
{
	return igtbl_getTopPos(e);
}

function igtbl_clientWidth(e)
{
	return e.clientWidth;
}

function igtbl_clientHeight(e)
{
	return e.clientHeight;
}

function igtbl_getInnerText(sourceElement)
{
	return sourceElement.innerText;
}

function igtbl_setInnerText(sourceElement,str)
{
	sourceElement.innerText=str;
}

function igtbl_showColButton(gn,se)
{
	var b=igtbl_getElementById(gn+"_bt");
	if(!b)
		return;
	var gs=igtbl_getGridById(gn);
	var bandNo=se.parentNode.parentNode.parentNode.getAttribute("bandNo");
	var columnNo=igtbl_getColumnNo(gn,se);
	var column=gs.Bands[bandNo].Columns[columnNo];
	var md;
	if(gs.UseFixedHeaders)
		md=igtbl_getElementById(gn+"_divscr");
	else
		md=igtbl_getElementById(gn+"_div");
	var gb=md.parentNode.parentNode.previousSibling;
	b.style.display="";
	b.style.left=igtbl_getLeftPos(se)-igtbl_getLeftPos(gs.Element);
	var t=igtbl_getTopPos(se)-igtbl_getTopPos(gs.Element);
	if(gb && md && md.style.overflow!="auto")
		t+=gb.offsetHeight;
	b.style.top=t;
	b.style.width=igtbl_clientWidth(se);
	b.style.height=igtbl_clientHeight(se);
	b.className=column.ButtonClass;
	if(se.innerHTML==igtbl_getNullText(gn,bandNo,columnNo))
		b.value=" ";
	else
		b.value=igtbl_getInnerText(se);
	b.setAttribute("srcElement",se.id);
	b.focus();
}

function igtbl_getDocumentElement(elemID)
{
	var obj;
	if(document.all)
		obj=document.all[elemID];
	else
		obj=document.getElementById(elemID);
	return obj;
}

function igtbl_initEvent(se)
{
	this.srcElement=se;
}

function igtbl_onScroll(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	if(!gs) return;
	var vl=igtbl_getElementById(gn+"_vl");
	if(vl && !vl.getAttribute("noOnBlur"))
		igtbl_hideEdit(gn);
	gs.alignStatMargins();
	gs.endEditTemplate();
	if(gs.Node && !gs.AllowPaging && gs.RowsServerLength>gs.Rows.length)
		igtbl_onScrollXml(evnt,gn);
	var sl,st
	if(gs.UseFixedHeaders)
	{
		if(typeof(gs.fhOldScrollLeft)=="undefined" && typeof(gs.fhOldScrollTop)=="undefined" || gs.fhOldScrollLeft!=gs.scrElem.scrollLeft || gs.fhOldScrollTop!=gs.scrElem.scrollTop)
		{
			gs.fhOldScrollLeft=gs.scrElem.scrollLeft;
			gs.fhOldScrollTop=gs.scrElem.scrollTop;
			if(gs.FixedColumnScrollType==2)
			{
				if(gs.alignDivsTimeoutID)
					window.clearTimeout(gs.alignDivsTimeoutID);
				gs.alignDivsTimeoutID=window.setTimeout("igtbl_doAlignDivs('"+gn+"')",250);
			}
			else
				gs.alignDivs();
		}
		sl=gs.scrElem.scrollLeft;
		st=gs.scrElem.scrollTop;
	}
	else
	{
		sl=gs.DivElement.scrollLeft;
		st=gs.DivElement.scrollTop;
	}
	gs.removeChange("ScrollLeft",gs);
	gs.recordChange("ScrollLeft",gs,sl);
	gs.removeChange("ScrollTop",gs);
	gs.recordChange("ScrollTop",gs,st);
}

function igtbl_doAlignDivs(gn)
{
	var gs=igtbl_getGridById(gn);
	gs.alignDivsTimeoutID=null;
	gs.alignDivs();
}
