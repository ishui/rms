/* 
Infragistics UltraWebGrid Script 
Version 5.1.20051.37
Copyright (c) 2001-2005 Infragistics, Inc. All Rights Reserved.
*/

var igtbl_lastActiveGrid="";

function igtbl_initGrid(gridId) 
{
	var xml=ig_csom.getElementById(gridId+"_xml");
	var gridElement=igtbl_getElementById("G_"+gridId);
	var grid=new igtbl_Grid(gridElement,xml);
	var expRowsIds=grid.AddnlProps[3];
	for(var i=0;i<expRowsIds.length;i++)
		igtbl_toggleRow(grid.Id,expRowsIds[i],true);
	var selRowsIds=grid.AddnlProps[4];
	for(i=0;i<selRowsIds.length;i++)
		igtbl_selectRow(grid.Id,selRowsIds[i]);
	var selCellsIds=grid.AddnlProps[5];
	for(i=0;i<selCellsIds.length;i++)
		igtbl_selectCell(grid.Id,selCellsIds[i]);
	var activeCellId=grid.AddnlProps[6];
	var activeRowId=grid.AddnlProps[7];
	var sortedColsIds=grid.AddnlProps[8];
	if(sortedColsIds)
		grid.addSortColumn(sortedColsIds);
	var de=grid.DivElement;
	if(grid.scrElem)
		de=grid.scrElem;
	var scrollLeft=grid.AddnlProps[9];
	if(scrollLeft && !grid.UseFixedHeaders)
		de.scrollLeft=scrollLeft;
	grid.alignDivs(scrollLeft);
	var scrollTop=grid.AddnlProps[10];
	if(scrollTop)
		de.scrollTop=scrollTop;
	if(activeCellId)
	{
		grid.setActiveCell(igtbl_getCellById(activeCellId));
		var cell=grid.oActiveCell;
		if(cell)
		{
			cell.scrollToView();
			if(cell.Band.getSelectTypeCell()==3)
				grid.Element.setAttribute("startPointCell",cell.Element.id);
		}
	}
	else if(activeRowId)
	{
		grid.setActiveRow(igtbl_getRowById(activeRowId));
		var row=grid.oActiveRow;
		if(row)
		{
			row.scrollToView();
			if(row.Band.getSelectTypeRow()==3)
				grid.Element.setAttribute("startPointRow",row.Element.id);
		}
	}
	grid.GridIsLoaded=true;
	igtbl_fireEvent(grid.Id,grid.Events.InitializeLayout,'("'+grid.Id+'");');
	return grid;
}

/* use igcsom.getElementById wherever is possible */
function igtbl_getElementById(tagId) 
{
	var obj=ig_csom.getElementById(tagId);
	if(obj && obj.length && typeof(obj.tagName)=="undefined")
	{
		var i=0;
		while(i<obj.length && (obj[i].id!=tagId || !igtbl_isVisible(obj[i]))) i++;
		if(i<obj.length) obj=obj[i];
		else obj=obj[0];
	}
	return obj;
}

function igtbl_getGridById(gridId) 
{
	if(typeof(igtbl_gridState)=="undefined")
		return null;
	var grid=igtbl_gridState[gridId];
	if(!grid)
		for(var gId in igtbl_gridState)
			if(igtbl_gridState[gId].UniqueID==gridId || igtbl_gridState[gId].ClientID==gridId)
			{
				grid=igtbl_gridState[gId];
				break;
			}
	return grid;
}

function igtbl_getBandById(tagId) 
{
	if(!tagId)
		return null;
	var parts = tagId.split("_");
	var bandIndex = parts.length - 2;
	var gridId = parts[0];
	var el=igtbl_getElementById(tagId);
	if((gridId.charAt(gridId.length-3)=="g" && gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="c" || gridId.charAt(gridId.length-3)=="s" && gridId.charAt(gridId.length-2)=="g" && gridId.charAt(gridId.length-1)=="r") && el && el.getAttribute("groupRow"))
	{
		gridId=gridId.substr(0,gridId.length-3);
		bandIndex--;
	}
	else if(el && (gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="c" && el.tagName=="TD" || gridId.charAt(gridId.length-2)=="g" && gridId.charAt(gridId.length-1)=="r" && el.getAttribute("groupRow") || gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="h" && el.getAttribute("hiddenRow")))
	{
		gridId=gridId.substr(0,gridId.length-2);
		bandIndex--;
	}
	else if(gridId.charAt(gridId.length-1)=="r" && el && el.tagName=="TR")
		gridId=gridId.substr(0,gridId.length-1);
	else if(gridId.charAt(gridId.length-1)=="c" && el && el.tagName=="TH")
	{
		gridId=gridId.substr(0,gridId.length-1);
		bandIndex=parts[1];
	}
	else
		return null;
	if(!igtbl_getGridById(gridId))
		return null;
	var grid = igtbl_getGridById(gridId);
	return grid.Bands[bandIndex];
}

function igtbl_getColumnById(tagId) 
{
	if(!tagId)
		return null;
	var parts = tagId.split("_");
	var bandIndex = parts.length - 2;
	var gridId = parts[0];
	var el=igtbl_getElementById(tagId);
	if(gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="c" && el && el.tagName=="TD")
	{
		gridId=gridId.substr(0,gridId.length-2);
		bandIndex=el.parentNode.parentNode.parentNode.getAttribute("bandNo");
	}
	else if(gridId.charAt(gridId.length-1)=="c")
	{
		if(el && el.tagName!="TH")
			return null;
		gridId=gridId.substr(0,gridId.length-1);
		bandIndex=parts[1];
	}
	else if(gridId.charAt(gridId.length-2)=="c" && gridId.charAt(gridId.length-1)=="g")
	{
		if(el && el.tagName!="DIV")
			return null;
		gridId=gridId.substr(0,gridId.length-2);
		bandIndex=parts[1];
	}
	else
		return null;
	if(!igtbl_getGridById(gridId))
		return null;
	var grid = igtbl_getGridById(gridId);
	var band = grid.Bands[bandIndex];
	var colIndex = parts[parts.length - 1];
	return band.Columns[colIndex];
}

function igtbl_getRowById(tagId) 
{
	if(!tagId)
		return null;
	var parts = tagId.split("_");
	var gridId = parts[0];
	var row=null;
	var isGrouped=false;
	if(gridId.charAt(gridId.length-3)=="g" && gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="c")
	{
		row=igtbl_getElementById(tagId);
		if(typeof(row)!="undefined" && row)
			row=row.parentNode;
		if(!row || !row.getAttribute("groupRow"))
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-3);
		isGrouped=true;
	}
	if(row==null && gridId.charAt(gridId.length-3)=="s" && gridId.charAt(gridId.length-2)=="g" && gridId.charAt(gridId.length-1)=="r")
	{
		row=igtbl_getWorkRow(igtbl_getElementById(tagId));
		if(!row || !row.getAttribute("groupRow"))
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-3);
		isGrouped=true;
	}
	if(row==null && gridId.charAt(gridId.length-2)=="g" && gridId.charAt(gridId.length-1)=="r")
	{
		row=igtbl_getElementById(tagId);
		if(!row || !row.getAttribute("groupRow"))
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-2);
		isGrouped=true;
	}
	if(row==null && gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="h")
	{
		row=igtbl_getElementById(tagId);
		if(typeof(row)!="undefined" && row)
			row=row.previousSibling;
		if(!row || !row.getAttribute("hiddenRow"))
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-2);
	}
	if(row==null && gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="c")
	{
		row=igtbl_getElementById(tagId);
		if(typeof(row)!="undefined" && row)
			row=row.parentNode;
		if(!row || row.tagName!="TR")
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-2);
	}
	if(row==null && gridId.charAt(gridId.length-1)=="r")
	{
		row=igtbl_getElementById(tagId);
		if(!row || row.tagName!="TR")
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-1);
	}
	if(row==null && gridId.charAt(gridId.length-1)=="l")
	{
		row=igtbl_getElementById(tagId);
		if(typeof(row)!="undefined" && row)
			row=row.parentNode;
		if(!row || row.tagName!="TR")
			row=null;
		else
			gridId=gridId.substr(0,gridId.length-1);
	}
	if(row==null)
		return null;
	var gs=igtbl_getGridById(gridId);
	if(!gs)
		return null;
	if(typeof(row.Object)!="undefined")
		return row.Object;
	else
	{
		parts=new Array();
		while(true)
		{
			row=igtbl_getWorkRow(row);
			var level=-1;
			if(gs.Bands.length==1 && !gs.Bands[0].IsGrouped)
				level=row.sectionRowIndex;
			else
				for(var i=0;i<row.parentNode.childNodes.length;i++)
				{
					if(!row.parentNode.childNodes[i].getAttribute("hiddenRow"))
						level++;
					if(row.parentNode.childNodes[i]==row)
						break;
				}
			parts[parts.length]=level;
			if(row.parentNode.parentNode==gs.Element)
				break;
			row=row.parentNode.parentNode.parentNode.parentNode.previousSibling;
		}
		parts=parts.reverse();
		var rows=gs.Rows;
		for(var i=0;i<parts.length;i++)
		{
			row=rows.getRow(parseInt(parts[i],10),row.Element?null:row);
			if(row && row.Expandable && i<parts.length-1)
				rows=row.Rows;
			else if(i<parts.length-1)
			{
				row=null;
				break;
			}
		}
		if(!row)
			return null;
		delete parts;
		row.Element.Object=row;
		return row;
	}
}

function igtbl_getCellById(tagId) 
{
	if(!tagId)
		return null;
	var parts = tagId.split("_");
	var gridId = parts[0];
	if(!(gridId.charAt(gridId.length-2)=="r" && gridId.charAt(gridId.length-1)=="c"))
		return null;
	gridId=gridId.substr(0,gridId.length-2);
	var gs=igtbl_getGridById(gridId);
	if(!gs)
		return null;
	var cellObj=igtbl_getElementById(tagId);
	if(!cellObj || cellObj.tagName!="TD")
		return null;
	if(cellObj.Object)
		return cellObj.Object;
	var row=igtbl_getRowById(cellObj.parentNode.id);
	if(!row)
		return null;
	var column=row.Band.Columns[parseInt(parts[parts.length-1],10)];
	return row.getCellByColumn(column);
}

function igtbl_needPostBack(gn)
{
	igtbl_getGridById(gn).NeedPostBack=true;
}

function igtbl_cancelPostBack(gn)
{
	igtbl_getGridById(gn).CancelPostBack=true;
}

function igtbl_getCollapseImage(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getCollapseImage();
}

function igtbl_getExpandImage(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getExpandImage();
}

function igtbl_getCellClickAction(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getCellClickAction();
}

function igtbl_getSelectTypeCell(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.SelectTypeCell;
	if(g.Bands[bandNo].SelectTypeCell!=0)
		res=g.Bands[bandNo].SelectTypeCell;
	return res;
}

function igtbl_getSelectTypeColumn(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.SelectTypeColumn;
	if(g.Bands[bandNo].SelectTypeColumn!=0)
		res=g.Bands[bandNo].SelectTypeColumn;
	return res;
}

function igtbl_getSelectTypeRow(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.SelectTypeRow;
	if(g.Bands[bandNo].SelectTypeRow!=0)
		res=g.Bands[bandNo].SelectTypeRow;
	return res;
}

function igtbl_getHeaderClickAction(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.HeaderClickAction;
	var band=g.Bands[bandNo];
	var column=band.Columns[columnNo];
	if(column.HeaderClickAction!=0)
		res=column.HeaderClickAction;
	else if(band.HeaderClickAction!=0)
		res=band.HeaderClickAction;
	if(res>1)
	{
		if(band.AllowSort!=0)
		{
			if(band.AllowSort==2)
				res=0;
		}
		else if(g.AllowSort==0 || g.AllowSort==2)
			res=0;
	}	
	return res;
}

function igtbl_getAllowUpdate(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.AllowUpdate;
	if(g.Bands[bandNo].AllowUpdate!=0)
		res=g.Bands[bandNo].AllowUpdate;
	if(typeof(columnNo)!="undefined" && g.Bands[bandNo].Columns[columnNo].AllowUpdate!=0)
		res=g.Bands[bandNo].Columns[columnNo].AllowUpdate;
	return res;
}

function igtbl_getAllowColSizing(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.AllowColSizing;
	if(g.Bands[bandNo].AllowColSizing!=0)
		res=g.Bands[bandNo].AllowColSizing;
	if(g.Bands[bandNo].Columns[columnNo].AllowColResizing!=0)
		res=g.Bands[bandNo].Columns[columnNo].AllowColResizing;
	return res;
}

function igtbl_getRowSizing(gn,bandNo,row)
{
	var g=igtbl_getGridById(gn);
	var res=g.RowSizing;
	if(g.Bands[bandNo].RowSizing!=0)
		res=g.Bands[bandNo].RowSizing;
	if(row.getAttribute("sizing"))
		res=parseInt(row.getAttribute("sizing"),10);
	return res;
}

function igtbl_getRowSelectors(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getRowSelectors();
}

function igtbl_getNullText(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	if(g.Bands[bandNo].Columns[columnNo].NullText!="")
		return g.Bands[bandNo].Columns[columnNo].NullText;
	if(g.Bands[bandNo].NullText!="")
		return g.Bands[bandNo].NullText;
	return g.NullText;
}

function igtbl_getEditCellClass(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	if(g.Bands[bandNo].EditCellClass!="")
		return g.Bands[bandNo].EditCellClass;
	return g.EditCellClass;
}

function igtbl_getFooterClass(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getFooterClass();
}

function igtbl_getGroupByRowClass(gn,bandNo)
{
	return g.Bands[bandNo].getGroupByRowClass();
}

function igtbl_getHeadClass(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].Columns[columnNo].getHeadClass();
}

function igtbl_getRowLabelClass(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getRowLabelClass();
}

function igtbl_getSelGroupByRowClass(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getSelGroupByRowClass();
}

function igtbl_getSelHeadClass(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	if(g.Bands[bandNo].Columns[columnNo].SelHeadClass!="")
		return g.Bands[bandNo].Columns[columnNo].SelHeadClass;
	if(g.Bands[bandNo].SelHeadClass!="")
		return g.Bands[bandNo].SelHeadClass;
	return g.SelHeadClass;
}

function igtbl_getSelCellClass(gn,bandNo,columnNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].Columns[columnNo].getSelClass();
}

function igtbl_getExpAreaClass(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	return g.Bands[bandNo].getExpAreaClass();
}

function igtbl_getCurrentRowImage(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	var res=g.CurrentRowImage;
	var band=g.Bands[bandNo];
	if(band.CurrentRowImage!="")
		res=band.CurrentRowImage;
	var au=igtbl_getAllowUpdate(gn,band.Index);
	if(band.RowTemplate!="" && (au==1 || au==3))
	{
		res=g.CurrentEditRowImage;
		if(band.CurrentEditRowImage!="")
			res=band.CurrentEditRowImage;
	}
	return res;
}

function igtbl_toggleRow() 
{
	var srcRow,expand;
	if(arguments.length==1)
	{
		var evnt=arguments[0];
		var se=igtbl_srcElement(evnt);
		if(!se || se.tagName!="IMG")
			return;
		srcRow=se.parentNode.parentNode.id;
	}
	else
	{
		srcRow=arguments[1];
		expand=arguments[2];
	}
	var sr = igtbl_getRowById(srcRow);
	if(!sr) return;
	igtbl_lastActiveGrid=sr.gridId;
	if(!sr.getExpanded() && expand!=false) 
		sr.setExpanded(true);
	else if(expand!=true)
		sr.setExpanded(false);
}

function igtbl_selectStart(evnt) 
{
	var se=igtbl_srcElement(evnt);
	if(se.tagName=="TD" || se.tagName=="NOBR" || se.tagName=="TH" || se.tagName=="A")
	{
		evnt.cancelBubble = true;
		evnt.returnValue = false;
	}
}

function igtbl_getBandFAC(gn,elem)
{
	var gs=igtbl_getGridById(gn);
	var bandNo=null;
	if(elem.tagName=="TD" || elem.tagName=="TH")
		bandNo=elem.parentNode.parentNode.parentNode.getAttribute("bandNo");
	else if(elem.tagName=="TR")
		bandNo=elem.parentNode.parentNode.getAttribute("bandNo");
	else if(elem.tagName=="TABLE")
		bandNo=elem.getAttribute("bandNo");
	if(bandNo)
		return gs.Bands[bandNo].firstActiveCell;
	return null;
}

function igtbl_headerClickDown(evnt,gn) 
{
	if(!evnt && event)
		evnt=event;
	if(!gn && igtbl_lastActiveGrid)
		gn=igtbl_lastActiveGrid;
	if(!gn || !evnt)
		return false;
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	igtbl_lastActiveGrid=gn;
	var te=gs.Element;
	te.setAttribute("mouseDown","1");
	var se=igtbl_srcElement(evnt);
	if(se && se.tagName=="IMG" && se.getAttribute("imgType")=="group")
		return;
	while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn) && (se.tagName!="DIV" || !se.getAttribute("groupInfo")))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName == "TH")
	{
		var colObj=igtbl_getColumnById(se.id);
		if(!colObj) return;
		if(igtbl_fireEvent(gn,gs.Events.MouseDown,"(\""+gn+"\",\""+se.id+"\","+igtbl_button(gn,evnt)+")")==true)
			return true;
		if(igtbl_button(gn,evnt)!=0)
			return;
		var bandNo=colObj.Band.Index;
		var band=colObj.Band;
		if(se.cellIndex>=band.firstActiveCell && igtbl_getOffsetX(evnt,se)>igtbl_clientWidth(se)-4 && igtbl_getAllowColSizing(gn,bandNo,colObj.Index)==2)
		{
			te.setAttribute("elementMode", "resize");
			te.setAttribute("resizeColumn", se.id);
			igtbl_lineupHeaders(se.id,band);
			var div,divr;
			if(!document.body.igtbl_resizeDiv)
			{
				div=document.createElement("DIV");
				document.body.appendChild(div);
				div.style.zIndex=10000;
				div.style.position="absolute";
				div.style.left=0;
				div.style.top=0;
				if(div.addEventListener)
				{
					div.addEventListener("mouseup",igtbl_resizeDivMouseUp,false);
					div.addEventListener("mousemove",igtbl_resizeDivMouseMove,false);
					div.addEventListener("selectstart",igtbl_resizeDivSelectStart,false);
				}
				else
				{
					div.onmouseup=igtbl_resizeDivMouseUp;
					div.onmousemove=igtbl_resizeDivMouseMove;
					div.onselectstart=igtbl_resizeDivSelectStart;
				}
				document.body.igtbl_resizeDiv=div;
				divr=document.createElement("DIV");
				div.appendChild(divr);
				divr.style.position="absolute";
				divr.style.borderWidth=1;
				divr.style.borderColor="black";
				divr.style.borderStyle="solid";
				divr.style.width=2;
			}
			else
			{
				div=document.body.igtbl_resizeDiv;
				divr=div.firstChild;
			}
			div.style.display="";
			div.style.cursor="w-resize";
			div.style.width=document.body.clientWidth;
			div.style.height=document.body.clientHeight;
			div.style.backgroundColor="transparent";
			divr.style.top=igtbl_getTopPos(te.parentNode);
			divr.style.left=evnt.clientX;
			divr.style.height=te.parentNode.offsetHeight;
			div.column=colObj;
			div.initX=evnt.clientX;
			return true;
		}
		se.setAttribute("justClicked",true);
		if(se.cellIndex>band.firstActiveCell-1)
		{
			if(igtbl_getHeaderClickAction(gn,bandNo,colObj.Index)==1 && (gs.SelectedColumns[se.id]!=true || gs.ViewType!=2 || igtbl_getSelectTypeColumn(gn,bandNo)==3))
			{
				if(igtbl_getSelectTypeColumn(gn,bandNo)<2)
					return true;
				te.setAttribute("elementMode", "select");
				te.setAttribute("selectMethod", "column");
				if(!(igtbl_getSelectTypeColumn(gn,bandNo)==3 && evnt.ctrlKey))
					igtbl_clearSelectionAll(gn);
				if(te.getAttribute("shiftSelect") && evnt.shiftKey)
				{
					te.setAttribute("lastSelectedColumn","");
					igtbl_selectColumnRegion(gn,se);
					te.removeAttribute("shiftSelect");
				}
				else
				{
					te.setAttribute("startColumn", se.id);
					if(gs.SelectedColumns[se.id] && evnt.ctrlKey)
						igtbl_selectColumn(gn,se.id,false);
					else
						igtbl_selectColumn(gn,se.id);
					te.removeAttribute("shiftSelect", true);
					if(!evnt.ctrlKey)
						te.setAttribute("shiftSelect",true);
				}
			}
		}
		return true;
	}
	else if(se.tagName=="DIV" && se.getAttribute("groupInfo"))
	{
		if(igtbl_button(gn,evnt)!=0)
			return;
		if(igtbl_fireEvent(gn,gs.Events.MouseDown,"(\""+gn+"\",\""+se.id+"\","+igtbl_button(gn,evnt)+")")==true)
			return;
		var groupInfo=se.getAttribute("groupInfo").split(":");
		if(groupInfo[0]!="band")
			igtbl_changeStyle(gn,se,igtbl_getSelHeadClass(gn,groupInfo[1],groupInfo[2]));
		se.setAttribute("justClicked",true);
		return true;
	}
}

function igtbl_resizeDivMouseUp(evnt)
{
	if(!evnt) evnt=event;
	if(!evnt) return;
	var se=document.body.igtbl_resizeDiv;
	se.style.display="none";
	if(se.initX!=evnt.clientX)
	{
		var col=se.column;
		var oldWidth=parseInt(col.Width,10);
		var newWidth=oldWidth+evnt.clientX-se.initX;
		if(newWidth<=0)
			newWidth=1;
		if(oldWidth!=newWidth)
			col.setWidth(newWidth);
	}
}

function igtbl_resizeDivMouseMove(evnt)
{
	if(!evnt) evnt=event;
	if(!evnt) return;
	var se=document.body.igtbl_resizeDiv;
	se.style.cursor="w-resize";
	if(!se.firstChild)
		se=se.parentNode;
	if(se.initX!=evnt.clientX)
	{
		var col=se.column;
		if(parseInt(col.Width,10)+evnt.clientX-se.initX>0)
			se.firstChild.style.left=evnt.clientX+document.body.scrollLeft;
	}
}

function igtbl_resizeDivSelectStart(evnt)
{
	if(!evnt) evnt=event;
	if(!evnt) return;
	return igtbl_cancelEvent(evnt);
}

function igtbl_lineupHeaders(colId,band)
{
	var gs=band.Grid;
	var te=gs.Element;
	var cg=new Array();
	var stat=false;
	if(band.Index==0 && !band.IsGrouped && gs.StationaryMargins>0)
	{
		cg[0]=te.childNodes[0];
		if(gs.StatHeader)
			cg[1]=gs.StatHeader.Element.firstChild;
		if(gs.StatFooter)
			cg[cg.length]=gs.StatFooter.Element.firstChild;
		stat=true;
	}
	else
	{
		var e=igtbl_getDocumentElement(colId);
		if(e && e.length)
			for(var i=0;i<e.length;i++)
				cg[i]=e[i].parentNode.parentNode.previousSibling;
		else if(e)
			cg[0]=e.parentNode.parentNode.previousSibling;
	}
	if(cg.length>0)
	{
		for(var j=0;j<cg.length;j++)
		{
			for(var i=0;i<cg[j].childNodes.length;i++)
			{
				var w=cg[j].childNodes[i].width;
				if(!w || w.substr(w.length-1)=="%")
					cg[j].childNodes[i].oldWidth=cg[j].childNodes[i].offsetWidth;
			}
			if(j>0 && stat)
				cg[j].parentNode.parentNode.style.width="";
			else
				cg[j].parentNode.style.width="";
			for(var i=0;i<cg[j].childNodes.length;i++)
			{
				if(cg[j].childNodes[i].oldWidth)
				{
					if(cg[j].nextSibling)
					{
						var column=igtbl_getColumnById(igtbl_getElemVis(cg[j].nextSibling.firstChild.childNodes,i).id);
						if(column)
							column.Width=cg[j].childNodes[i].oldWidth;
					}
					cg[j].childNodes[i].style.width="";
					cg[j].childNodes[i].width=cg[j].childNodes[i].oldWidth;
					cg[j].childNodes[i].oldWidth=null;
				}
			}
		}
	}
	igtbl_dispose(cg);
	delete cg;
}

function igtbl_headerClickUp(evnt,gn) 
{
	if(!evnt && event)
		evnt=event;
	if(!gn && igtbl_lastActiveGrid)
		gn=igtbl_lastActiveGrid;
	if(!gn || !evnt)
		return false;
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	if(igtbl_button(gn,evnt)==2)
		return;
	var te=gs.Element;
	te.removeAttribute("mouseDown");
	var se=igtbl_srcElement(evnt);
	while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn) && (se.tagName!="DIV" || !se.getAttribute("groupInfo")))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName == "TH")
	{
		var column=igtbl_getColumnById(se.id);
		if(!column) return;
		var bandNo=column.Band.Index;
		var columnNo=column.Index;
		var mode=te.getAttribute("elementMode");
		if(mode!="resize")
			igtbl_fireEvent(gn,gs.Events.ColumnHeaderClick,"(\""+gn+"\",\""+se.id+"\","+igtbl_button(gn,evnt)+")");
		if(igtbl_fireEvent(gn,gs.Events.MouseUp,"(\""+gn+"\",\""+se.id+"\","+igtbl_button(gn,evnt)+")")==true)
			return true;
		if(se.cellIndex>column.Band.firstActiveCell-1)
		{
			if(igtbl_getHeaderClickAction(gn,bandNo,columnNo)!=1)
				igtbl_changeStyle(gn,se,null);
			te.removeAttribute("elementMode");
			te.removeAttribute("resizeColumn");
			te.removeAttribute("selectMethod");
			if(!te.getAttribute("shiftSelect"))
				te.removeAttribute("startColumn");
			if(mode!="resize" && (igtbl_getHeaderClickAction(gn,bandNo,columnNo)==2 || igtbl_getHeaderClickAction(gn,bandNo,columnNo)==3) && column.SortIndicator!=3)
			{
				if(gs.Bands[bandNo].ClientSortEnabled)
				{
					gs.startHourGlass();
					gs.sortingColumn=se;
					gs.oldColCursor=se.style.cursor;
					se.style.cursor="wait";
					window.setTimeout("igtbl_gridSortColumn('"+gn+"','"+se.id+"',"+evnt.shiftKey+")",1);
				}
				else
					gs.sortColumn(se.id,evnt.shiftKey);
				if(gs.NeedPostBack)
					igtbl_doPostBack(gn,evnt.shiftKey?"shiftKey:true":"");
			}
			else
			{
				if(mode=="resize")
					igtbl_resizeDivMouseUp(evnt);
				if((mode=="resize" || mode=="select") && gs.NeedPostBack)
					igtbl_doPostBack(gn);
				te.removeAttribute("elementMode");
			}
		}
	}
	else if(se.tagName=="DIV" && se.getAttribute("groupInfo"))
	{
		igtbl_fireEvent(gn,gs.Events.ColumnHeaderClick,"(\""+gn+"\",\""+se.id+"\","+igtbl_button(gn,evnt)+")");
		if(igtbl_fireEvent(gn,gs.Events.MouseUp,"(\""+gn+"\",\""+se.id+"\","+igtbl_button(gn,evnt)+")")==true)
			return;
		var groupInfo=se.getAttribute("groupInfo").split(":");
		if(groupInfo[0]!="band")
		{
			igtbl_changeStyle(gn,se,null);
			var bandNo=igtbl_bandNoFromColId(se.id);
			var columnNo=igtbl_colNoFromColId(se.id);
			var column=gs.Bands[bandNo].Columns[columnNo];
			if((igtbl_getHeaderClickAction(gn,bandNo,columnNo)==2 || igtbl_getHeaderClickAction(gn,bandNo,columnNo)==3) && column.SortIndicator!=3)
			{
				if(gs.Bands[bandNo].ClientSortEnabled)
				{
					gs.startHourGlass();
					gs.sortingColumn=se;
					gs.oldColCursor=se.style.cursor;
					se.style.cursor="wait";
					window.setTimeout("igtbl_gridSortColumn('"+gn+"','"+se.id+"',true)",1);
				}
				else
					gs.sortColumn(se.id,evnt.shiftKey);
				if(gs.NeedPostBack)
					igtbl_doPostBack(gn,evnt.shiftKey?"shiftKey:true":"");
			}
		}
	}
	if(gs.NeedPostBack)
		igtbl_doPostBack(gn,'HeaderClick:'+se.id);
	return true;
}

function igtbl_headerContextMenu(evnt,gn) 
{
	if(!evnt && event)
		evnt=event;
	if(!gn && igtbl_lastActiveGrid)
		gn=igtbl_lastActiveGrid;
	if(!gn || !evnt)
		return false;
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	if(igtbl_button(gn,evnt)==2)
		return;
	var te=gs.Element;
	te.removeAttribute("mouseDown");
	var se=igtbl_srcElement(evnt);
	while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn) && (se.tagName!="DIV" || !se.getAttribute("groupInfo")))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName == "TH" || se.tagName == "DIV")
	{
		var column=igtbl_getColumnById(se.id);
		if(se.tagName=="TH" && !column) return;
		igtbl_fireEvent(gn,gs.Events.ColumnHeaderClick,"(\""+gn+"\",\""+se.id+"\",2)");
		if(igtbl_fireEvent(gn,gs.Events.MouseUp,"(\""+gn+"\",\""+se.id+"\",2)")==true)
		{
			evnt.cancelBubble=true;
			evnt.returnValue=false;
			return false;
		}
	}
}

function igtbl_gridSortColumn(gn,colId,shiftKey)
{
	var gs=igtbl_getGridById(gn);
	gs.sortColumn(colId,shiftKey);
	if(gs.sortingColumn && gs.oldColCursor);
		gs.sortingColumn.style.cursor=gs.oldColCursor;
	gs.stopHourGlass();
	if(gs.NeedPostBack)
		igtbl_doPostBack(gn,"shiftKey:"+shiftKey.toString());
}

function igtbl_headerMouseOut(evnt,gn) 
{
	if(!evnt && event)
		evnt=event;
	if(!gn && igtbl_lastActiveGrid)
		gn=igtbl_lastActiveGrid;
	if(!gn || !evnt)
		return false;
	var gs=igtbl_getGridById(gn);
	var se=igtbl_srcElement(evnt);
	if(!gs || !se)
		return;
	if(se.tagName=="NOBR" && se.title)
		se.title="";
	while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn) && (se.tagName!="DIV" || !se.getAttribute("groupInfo")))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName == "TH")
	{
		var column=igtbl_getColumnById(se.id);
		if(!column) return;
		var sep=se.parentNode;
		if(gs.Element.getAttribute("elementMode")=="select")
			return true;
		if(igtbl_fireEvent(gn,gs.Events.MouseOut,"(\""+gn+"\",\""+se.id+"\",1)")==true)
			return true;
		if(se.cellIndex>column.Band.firstActiveCell-1 && (igtbl_getHeaderClickAction(gn,column.Band.Index,column.Index)!=1))
			igtbl_changeStyle(gn,se,null);
		return true;
	}
	else if(se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		if(igtbl_fireEvent(gn,gs.Events.MouseOut,"(\""+gn+"\",\""+se.id+"\",1)")==true)
			return true;
		var groupInfo=se.getAttribute("groupInfo").split(":");
		if(groupInfo[0]!="band")
			igtbl_changeStyle(gn,se,null);
		return true;
	}
}

function igtbl_headerMouseOver(evnt,gn)
{
	if(!evnt && event)
		evnt=event;
	if(!gn && igtbl_lastActiveGrid)
		gn=igtbl_lastActiveGrid;
	if(!gn || !evnt)
		return false;
	var gs=igtbl_getGridById(gn);
	var se=igtbl_srcElement(evnt);
	if(!gs || !se)
		return;
	if(se.tagName=="NOBR")
	{
		var col=igtbl_getColumnById(se.parentNode.id);
		if(col)
		{
			var nobr=se;
			if(nobr.offsetWidth>se.parentNode.offsetWidth || nobr.offsetHeight>se.parentNode.offsetHeight)
				nobr.title=col.HeaderText;
		}
	}
	while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn) && (se.tagName!="DIV" || !se.getAttribute("groupInfo")))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName!="DIV")
	{
		while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
			se=se.parentNode;
		if(!se)
			return;
	}
	if(se.tagName == "TH")
	{
		var column=igtbl_getColumnById(se.id);
		if(!column) return;
		if(igtbl_fireEvent(gn,gs.Events.MouseOver,"(\""+gn+"\",\""+se.id+"\",1)")==true)
			return;
	}
	else if(se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		if(igtbl_fireEvent(gn,gs.Events.MouseOver,"(\""+gn+"\",\""+se.id+"\",1)")==true)
			return;
	}
}

function igtbl_headerMouseMove(evnt,gn)
{
	if(!evnt && event)
		evnt=event;
	if(!gn && igtbl_lastActiveGrid)
		gn=igtbl_lastActiveGrid;
	if(!gn || !evnt)
		return false;
	var gs=igtbl_getGridById(gn);
	var se=igtbl_srcElement(evnt);
	if(!gs || !se)
		return false;
	while(se && (se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn) && (se.tagName!="DIV" || !se.getAttribute("groupInfo")))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName == "TH")
	{
		var column=igtbl_getColumnById(se.id);
		if(!column) return;
		var bandNo=column.Band.Index;
		var columnNo=column.Index;
		if(igtbl_button(gn,evnt)==0)
		{
			var mode = gs.Element.getAttribute("elementMode");
			if(mode!=null && mode=="resize") 
				igtbl_resizeDivMouseMove(evnt);
			else if(mode=="select" && se.cellIndex>column.Band.firstActiveCell-1 && igtbl_getHeaderClickAction(gn,bandNo,columnNo)==1 && !evnt.ctrlKey) 
				igtbl_selectColumnRegion(gn,se);
			else
			{
				var cursorName = se.getAttribute("oldCursor");
				if(cursorName != null)
				{
					se.style.cursor=cursorName;
					se.removeAttribute("oldCursor");
				}
				if(se.cellIndex>column.Band.firstActiveCell-1 && (igtbl_getHeaderClickAction(gn,bandNo,columnNo)!=1 || gs.SelectedColumns[se.id] || igtbl_getSelectTypeColumn(gn,bandNo)<2))
					if(column.AllowGroupBy==1 && gs.ViewType==2 && gs.GroupByBox.Element || column.Band.AllowColumnMoving>1)
					{
						if(se.getAttribute("justClicked"))
						{
							if(typeof(igtbl_headerDragStart)!="undefined")
								igtbl_headerDragStart(gn,se,evnt);
						}
						else
							igtbl_changeStyle(gn,se,null);
					}
			}
		}
		else 
		{
			var te=gs.Element;
			te.removeAttribute("elementMode");
			te.removeAttribute("resizeColumn");
			te.removeAttribute("selectMethod");
			if(!te.getAttribute("shiftSelect"))
				te.removeAttribute("startColumn");
			if(se.cellIndex>=column.Band.firstActiveCell && igtbl_getOffsetX(evnt,se)>igtbl_clientWidth(se)-4 && igtbl_getAllowColSizing(gn,bandNo,columnNo)==2)
			{
				var cursorName = se.getAttribute("oldCursor");
				if(cursorName == null)
					se.setAttribute("oldCursor", se.style.cursor);
				se.style.cursor="w-resize";
			}
			else if(se.cellIndex>=column.Band.firstActiveCell)
			{
				var cursorName = se.getAttribute("oldCursor");
				if(cursorName != null)
				{
					se.style.cursor=cursorName;
					se.removeAttribute("oldCursor");
				}
			}
		}
		if(se.getAttribute("justClicked"))
			se.removeAttribute("justClicked");
		evnt.cancelBubble=true;
		evnt.returnValue=false;
		return true;
	}
	else if(se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		var groupInfo=se.getAttribute("groupInfo").split(":");
		if(groupInfo[0]!="band")
		{
			if(igtbl_button(gn,evnt)==0)
			{
				var cursorName = se.getAttribute("oldCursor");
				if(cursorName != null)
				{
					se.style.cursor=cursorName;
					se.removeAttribute("oldCursor");
				}
				igtbl_changeStyle(gn,se,null);
				if(gs.ViewType==2 && se.getAttribute("justClicked") && typeof(igtbl_headerDragStart)!="undefined")
					igtbl_headerDragStart(gn,se,evnt);
			}
		}
		if(se.getAttribute("justClicked"))
			se.removeAttribute("justClicked");
		return true;
	}
	return false;
}

function igtbl_tableMouseMove(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	var se=igtbl_srcElement(evnt);
	if(!gs || !se)
		return false;
	var te=gs.Element;
	if(igtbl_button(gn,evnt)==0 && te.getAttribute("elementMode")=="resize")
	{
		if((se.id==gn+"_div" || se.id==gn+"_hdiv" || se.tagName=="TABLE" && se.parentNode.parentNode.getAttribute("hiddenRow") || se.tagName=="TD" && se.parentNode.getAttribute("hiddenRow")) && te.getAttribute("resizeColumn"))
		{
			if(typeof(te.parentNode.oldCursor)!="string")
			{
				te.parentNode.oldCursor=te.parentNode.style.cursor;
				te.parentNode.style.cursor="w-resize";
				if(gs.StatHeader)
					gs.StatHeader.Element.parentNode.parentNode.style.cursor="w-resize";
			}
			var column=te.getAttribute("resizeColumn");
			var resCol=igtbl_getElementById(column);
			var cg=se.childNodes[0];
			if(se.id==gn+"_div" || se.tagName=="TD")
				cg=cg.childNodes[0];
			else if(se.id==gn+"_hdiv")
				cg=cg.childNodes[0].childNodes[0];
			if(!cg)
				return false;
			var co=cg.childNodes[resCol.cellIndex];
			var c1w=evnt.clientX-igtbl_getLeftPos(resCol);
			igtbl_resizeColumn(gn,resCol.id,c1w);
			if(evnt.cancelBubble)
				evnt.cancelBubble=true;
			if(evnt.returnValue)
				evnt.returnValue=false;
			return false;
		}
		else if(te.getAttribute("resizeRow") && (se.id==gn+"_div" || se.tagName=="TH" && se.parentNode.parentNode.tagName=="TFOOT" || se.tagName=="TD" && se.parentNode.getAttribute("hiddenRow")))
		{
			if(typeof(te.parentNode.oldCursor)!="string")
			{
				te.oldCursor=te.style.cursor;
				te.style.cursor="n-resize";
			}
			var rowId=te.getAttribute("resizeRow");
			var row=igtbl_getElementById(rowId);
			if(!row || row.getAttribute("hiddenRow"))
				return;
			var r1h=row.offsetHeight+(evnt.clientY-(igtbl_getTopPos(row)+row.offsetHeight));
			igtbl_resizeRow(gn,rowId,r1h);
			if(evnt.cancelBubble)
				evnt.cancelBubble=true;
			if(evnt.returnValue)
				evnt.returnValue=false;
			return false;
		}
	}
	else if(typeof(te.parentNode.oldCursor)=="string")
	{
		te.parentNode.style.cursor=te.parentNode.oldCursor;
		if(gs.StatHeader)
			gs.StatHeader.Element.parentNode.parentNode.style.cursor=te.parentNode.oldCursor;
		te.parentNode.oldCursor=null;
	}
}

function igtbl_tableMouseUp(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return false;
	var se=igtbl_srcElement(evnt);
	if(se.id==gn+"_div" && gs.Element.getAttribute("elementMode")=="resize")
	{
		gs.Element.removeAttribute("elementMode");
		gs.Element.removeAttribute("resizeColumn");
	}
}

function igtbl_resizeColumn(gn,colId,width)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return false;
	var col=igtbl_getColumnById(colId);
	if(!col)
		return false;
	return col.setWidth(width);
}

function igtbl_selectColumnRegion(gn,se)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	var te=gs.Element;
	var lastSelectedColumn=te.getAttribute("lastSelectedColumn");
	var selMethod=te.getAttribute("selectMethod");
	if(selMethod=="column" && se.id!=lastSelectedColumn)
	{
		var startColumn=igtbl_getColumnById(te.getAttribute("startColumn"));
		if(startColumn==null)
			startColumn=igtbl_getColumnById(se.id);
		var endColumn=igtbl_getColumnById(se.id);
		if(igtbl_getSelectTypeColumn(gn,se.parentNode.parentNode.parentNode.getAttribute("bandNo"))==3)
			gs.selectColRegion(startColumn,endColumn);
		else
		{
			igtbl_clearSelectionAll(gn);
			igtbl_selectColumn(gn,se.id);
		}
		gs.Element.setAttribute("lastSelectedColumn",se.id);
	}
}

function igtbl_resizeRow(gn,rowId,height)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	var row=igtbl_getRowById(rowId);
	if(!row)
		return;
	if(height>0)
	{
		var cancel=false;
		if(igtbl_fireEvent(gn,gs.Events.BeforeRowSizeChange,"(\""+gn+"\",\""+row.Element.id+"\","+height+")")==true)
			cancel=true;
		if(!cancel)
		{
			var rowLabel=null;
			if(!row.GroupByRow && igtbl_getRowSelectors(gn,row.Band.Index)!=2)
				rowLabel=row.Element.cells[row.Band.firstActiveCell-1];
			row.Element.style.height=height;
			gs.removeChange("ResizedRows",row);
			gs.recordChange("ResizedRows",row,height);
			if(rowLabel)
				rowLabel.style.height=height;
			if(gs.UseFixedHeaders)
			{
				var i=0;
				while(i<row.Band.Columns.length && row.Band.Columns[i].getFixed()) i++;
				while(i<row.Band.Columns.length)
				{
					var column=row.Band.Columns[i];
					if(column.hasCells())
					{
						var cell=row.getCellByColumn(column);
						cell.getElement().style.height=height;
					}
					i++;
				}
			}
			gs.alignGrid();
			igtbl_fireEvent(gn,gs.Events.AfterRowSizeChange,"(\""+gn+"\",\""+row.Element.id+"\","+height+")");
		}
	}
}

function igtbl_cellClickDown(evnt,gn) 
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	igtbl_lastActiveGrid=gn;
	gs.Element.setAttribute("mouseDown","1");
	var se=igtbl_srcElement(evnt);
	if(se.id==gn+"_vl" || se.id==gn+"_tb" || se.id==gn+"_ta")
		return;
	var sel=igtbl_getElementById(gn+"_vl");
	if(sel && sel.style.display=="" && sel.getAttribute("noOnBlur"))
		return igtbl_cancelEvent(evnt);
	while(se && (se.tagName!="TD" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName == "TD") 
	{
		var row;
		var id=se.id;
		var cell=igtbl_getCellById(id);
		if(cell)
		{
			row=cell.Row;
			id=cell.Element.id;
		}
		else row=igtbl_getRowById(id);
		if(!row && !cell) return;
		var fac=row.Band.firstActiveCell;
		if(igtbl_fireEvent(gn,gs.Events.MouseDown,"(\""+gn+"\",\""+id+"\","+igtbl_button(gn,evnt)+")")==true)
		{
			evnt.cancelBubble=true;
			return true;
		}
		var band=row.Band;
		var bandNo=band.Index;
		if(igtbl_button(gn,evnt)==0 && se.cellIndex==fac-1 && igtbl_getOffsetY(evnt,se)>igtbl_clientHeight(se)-4 && igtbl_getRowSizing(gn,bandNo,se.parentNode)==2 && !se.getAttribute("groupRow"))
		{
			gs.Element.setAttribute("elementMode", "resize");
			gs.Element.setAttribute("resizeRow", se.parentNode.id);
			se.parentNode.style.height=se.parentNode.offsetHeight;
		}
		else if(se.cellIndex>=fac-1 || se.getAttribute("groupRow"))
		{
			var te=gs.Element;
			var workTableId;
			if(se.getAttribute("groupRow"))
				workTableId=se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
			else
				workTableId=se.parentNode.parentNode.parentNode.id;
			if(igtbl_button(gn,evnt)!=0)
				return;
			if(workTableId=="")
				return;
			if(!se.getAttribute("groupRow") && se.cellIndex==fac-1 && se.parentNode.cells[fac].childNodes.length>0 && se.parentNode.cells[fac].childNodes[0].tagName=="TABLE")
				return;
			te.removeAttribute("lastSelectedCell");
			var prevSelRow=gs.SelectedRows[igtbl_getWorkRow(se.parentNode).id];
			if(prevSelRow && igtbl_getLength(gs.SelectedRows)>1)
				prevSelRow=false;
			var selPresent=igtbl_getLength(gs.SelectedRows)>0 || igtbl_getLength(gs.SelectedCells)>0 || igtbl_getLength(gs.SelectedCols)>0;
			if(se.getAttribute("groupRow") || se.cellIndex==fac-1 || igtbl_getCellClickAction(gn,bandNo)==2)
			{
				if(!(igtbl_getSelectTypeRow(gn,bandNo)==3 && evnt.ctrlKey) && !(row.getSelected() && igtbl_getLength(gs.SelectedRows)==1))
					igtbl_clearSelectionAll(gn);
			}
			else
			{
				if(!(igtbl_getSelectTypeCell(gn,bandNo)==3 && evnt.ctrlKey) && !(cell.getSelected() && igtbl_getLength(gs.SelectedCells)==1))
					igtbl_clearSelectionAll(gn);
			}
			gs.Element.setAttribute("elementMode", "select");
			if(se.getAttribute("groupRow"))
			{
				te.setAttribute("selectTable", se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id);
				te.setAttribute("selectMethod", "row");
			}
			else
			{
				te.setAttribute("selectTable", se.parentNode.parentNode.parentNode.id);
				if(se.cellIndex==fac-1 || igtbl_getCellClickAction(gn,bandNo)==2)
					te.setAttribute("selectMethod", "row");
				else
					te.setAttribute("selectMethod", "cell");
			}
			if(te.getAttribute("shiftSelect") && evnt.shiftKey)
			{
				igtbl_selectRegion(gn,se);
				te.removeAttribute("shiftSelect");
			}
			else
			{
				if(se.cellIndex==fac-1 || igtbl_getCellClickAction(gn,bandNo)==2 || se.getAttribute("groupRow"))
				{
					var seRow=igtbl_getRowById(se.parentNode.id);
					if(gs.SelectedRows[se.parentNode.id] && evnt.ctrlKey)
					{
						igtbl_selectRow(gn,seRow,false);
						gs.setActiveRow(seRow);
					}
					else
					{
						var showEdit=true;
						if(!gs.exitEditCancel)
						{
							if(gs.Activation.AllowActivation)
							{
								var ar=gs.oActiveRow;
								if(ar!=seRow)
								{
									gs.setActiveRow(seRow);
									showEdit=false;
								}
								else
									showEdit=true;
							}
							if(igtbl_getSelectTypeRow(gn,bandNo)>1)
								igtbl_selectRow(gn,seRow,true,!prevSelRow);
							if(showEdit && !se.getAttribute("groupRow") && se.cellIndex==fac-1)
								igtbl_getRowById(se.parentNode.id).editRow();
						}
					}
				}
				else
				{
					if(cell.getSelected() && evnt.ctrlKey)
					{
						cell.select(false);
						cell.activate();
					}
					else
					{
						if(band.getSelectTypeCell()>1 && band.getCellClickAction()>=1 && !gs.exitEditCancel)
							cell.select();
						else if(selPresent)
							igtbl_fireEvent(gn,gs.Events.AfterSelectChange,"(\""+gn+"\",\""+id+"\");");
						cell.activate();
					}
				}
				if(se.getAttribute("groupRow"))
					te.setAttribute("startPointRow", se.parentNode.parentNode.parentNode.parentNode.parentNode.id);
				else
					te.setAttribute("startPointRow", se.parentNode.id);
				te.setAttribute("startPointCell", id);
				te.removeAttribute("shiftSelect", true);
				if(!evnt.ctrlKey)
					te.setAttribute("shiftSelect", true);
			}
		}
	}
	if(typeof(igtbl_currentEditTempl)!="undefined" && igtbl_currentEditTempl!=null)
		igtbl_gRowEditMouseDown(evnt);
	if(typeof(igcmbo_currentDropped)!="undefined" && igcmbo_currentDropped!=null)
		igcmbo_mouseDown(evnt);
	return igtbl_cancelEvent(evnt);
}

function igtbl_cellClickUp(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	if(igtbl_button(gn,evnt)==2)
		return;
	gs.Element.removeAttribute("mouseDown");
	var se=igtbl_srcElement(evnt);
	if(se.id==gn+"_vl" || se.id==gn+"_tb" || se.id==gn+"_ta")
		return;
	var sel=igtbl_getElementById(gn+"_vl");
	if(sel && sel.style.display=="" && sel.getAttribute("noOnBlur"))
		return igtbl_cancelEvent(evnt);
	while(se && (se.tagName!="TD" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName != "TD")
		return;
	if(se.id == "")
		return;
	var row;
	var id=se.id;
	var cell=igtbl_getCellById(id);
	if(cell)
	{
		row=cell.Row;
		id=cell.Element.id;
	}
	else row=igtbl_getRowById(id);
	if(!row && !cell) return;
	var te=gs.Element;
	var mode=gs.Element.getAttribute("elementMode");
	gs.Element.removeAttribute("elementMode");
	te.removeAttribute("selectTable");
	te.removeAttribute("selectMethod");
	te.removeAttribute("resizeRow");
	if(!te.getAttribute("shiftSelect"))
	{
		te.removeAttribute("startPointRow");
		te.removeAttribute("startPointCell");
	}
	var bandNo=row.Band.Index;
	var fac=row.Band.firstActiveCell;
	if(cell && se.cellIndex>fac-1 && igtbl_getCellClickAction(gn,bandNo)==1)
	{
		if(igtbl_getAllowUpdate(gn,bandNo,cell.Column.Index)==3)
			row.editRow(true);
		else
			cell.beginEdit();
	}
	if((mode=="resize" || mode=="select") && gs.NeedPostBack)
	{
		se=igtbl_srcElement(evnt);
		if(!(se.tagName=="INPUT" && se.type=="checkbox"))
			igtbl_doPostBack(gn);
		return;
	}
	if(!se.getAttribute("groupRow") && mode!="resize")
	{
		if(se.cellIndex==fac-1)
			igtbl_fireEvent(gn,gs.Events.RowSelectorClick,"(\""+gn+"\",\""+se.parentNode.id+"\","+igtbl_button(gn,evnt)+")");
		else
			igtbl_fireEvent(gn,gs.Events.CellClick,"(\""+gn+"\",\""+id+"\","+igtbl_button(gn,evnt)+")");
	}
	gs.noCellChange=false;
	if(igtbl_fireEvent(gn,gs.Events.MouseUp,"(\""+gn+"\",\""+id+"\","+igtbl_button(gn,evnt)+")")==true)
	{
		evnt.cancelBubble=true;
		return true;
	}
	if(gs.NeedPostBack && se.cellIndex==fac-1)
		igtbl_doPostBack(gn,'RowClick:'+se.parentNode.id+(se.parentNode.getAttribute("level")?"\x05"+se.parentNode.getAttribute("level"):""));
	else if(gs.NeedPostBack && igtbl_getCellClickAction(gn,bandNo)==2)
		igtbl_doPostBack(gn,'RowClick:'+se.parentNode.id+(se.parentNode.getAttribute("level")?"\x05"+se.parentNode.getAttribute("level"):""));
	else if(gs.NeedPostBack)
		igtbl_doPostBack(gn,'CellClick:'+id+(cell.Element.getAttribute("level")?"\x05"+cell.Element.getAttribute("level"):""));
	return igtbl_cancelEvent(evnt);
}

function igtbl_cellContextMenu(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	var te=gs.Element;
	te.removeAttribute("mouseDown");
	te.removeAttribute("elementMode");
	te.removeAttribute("resizeColumn");
	te.removeAttribute("selectMethod");
	if(!te.getAttribute("shiftSelect"))
		te.removeAttribute("startColumn");
	var se=igtbl_srcElement(evnt);
	if(se.id==gn+"_vl" || se.id==gn+"_tb" || se.id==gn+"_ta")
		return;
	while(se && (se.tagName!="TD" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName != "TD")
		return;
	if(se.id == "")
		return;
	var row;
	var id=se.id;
	var cell=igtbl_getCellById(id);
	if(cell)
	{
		row=cell.Row;
		id=cell.Element.id;
	}
	else row=igtbl_getRowById(id);
	if(!row && !cell) return;
	if(!se.getAttribute("groupRow"))
	{
		if(se.cellIndex==row.Band.firstActiveCell-1)
			igtbl_fireEvent(gn,gs.Events.RowSelectorClick,"(\""+gn+"\",\""+se.parentNode.id+"\",2)");
		else
			igtbl_fireEvent(gn,gs.Events.CellClick,"(\""+gn+"\",\""+id+"\",2)");
	}
	if(igtbl_fireEvent(gn,gs.Events.MouseUp,"(\""+gn+"\",\""+id+"\",2)")==true)
		return igtbl_cancelEvent(evnt);
}

function igtbl_cellMouseOver(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	var se=igtbl_srcElement(evnt);
	if(!gs || !se)
		return;
	if(se.tagName=="NOBR")
	{
		var cell=igtbl_getCellById(se.parentNode.id);
		if(cell)
		{
			var nobr=cell.Element.childNodes[0];
			if(cell.Element.title)
				nobr.title=cell.Element.title;
			else if(nobr.offsetWidth>cell.Element.offsetWidth || nobr.offsetHeight>cell.Element.offsetHeight)
			{
				if(igtbl_trim(cell.MaskedValue))
					nobr.title=cell.MaskedValue;
				else
					nobr.title=cell.getValue(true);
			}
		}
		se=se.parentNode;
	}
	while(se && (se.tagName!="TD" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se || se.tagName!="TD" || se.id=="")
		return;
	var row;
	var id=se.id;
	var cell=igtbl_getCellById(id);
	if(cell)
	{
		row=cell.Row;
		id=cell.Element.id;
	}
	else row=igtbl_getRowById(se.id);
	if(!row && !cell) return;
	var te=gs.Element;
	if(evnt.shiftKey && row.Band.getSelectTypeRow()==3 && !te.getAttribute("shiftSelect"))
		te.setAttribute("shiftSelect",true);
	if(igtbl_fireEvent(gn,gs.Events.MouseOver,"(\""+gn+"\",\""+id+"\",0)")==true)
		return;
}

function igtbl_cellMouseMove(evnt,gn)
{
	var se=igtbl_srcElement(evnt);
	var gs=igtbl_getGridById(gn);
	if(!gs || !se)
		return;
	var te=gs.Element;
	if(se.id==gn+"_vl" || se.id==gn+"_tb" || se.id==gn+"_ta")
		return;
	if(te.getAttribute("resizeRow") && (se.tagName=="TH" && se.parentNode.parentNode.tagName=="TFOOT" || se.tagName=="TD" && se.parentNode.getAttribute("hiddenRow")))
		return igtbl_tableMouseMove(evnt,gn);
	while(se && (se.tagName!="TD" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName != "TD")
		return;
	if(se.id == "")
		return;
	var row;
	var id=se.id;
	var cell=igtbl_getCellById(id);
	if(cell)
	{
		row=cell.Row;
		if(!cell) return;
		id=cell.Element.id;
	}
	else row=igtbl_getRowById(se.id);
	if(!row && !cell) return;
	var bandNo=row.Band.Index;
	var fac=row.Band.firstActiveCell;
	if(igtbl_button(gn,evnt)==0)
	{
		var mode = te.getAttribute("elementMode");
		if(mode && mode=="resize") 
		{
			if(se.cellIndex!=fac-1)
				return;
			var rowID = te.getAttribute("resizeRow");
			var rowEl=igtbl_getElementById(rowID);
			if(!rowEl || rowEl.getAttribute("hiddenRow"))
				return;
			var r1h=rowEl.offsetHeight+(evnt.clientY-(igtbl_getTopPos(rowEl)+rowEl.offsetHeight-(rowEl.clientTop?rowEl.clientTop:0)));
			igtbl_resizeRow(gn,rowID,r1h);
			var cursorName = se.getAttribute("oldCursor");
			if(cursorName==null)
				se.setAttribute("oldCursor", se.style.cursor);
			se.style.cursor="n-resize";
		}
		else
		{
			if(se.cellIndex==fac-1)
			{
				var cursorName = se.getAttribute("oldCursor");
				if(cursorName!=null)
				{
					se.style.cursor=cursorName;
					se.removeAttribute("oldCursor");
				}
			}
			if(mode && mode=="select" && !evnt.ctrlKey) 
			{
				var lsc=te.getAttribute("lastSelectedCell");
				if(!lsc || lsc!=se.id)
					igtbl_selectRegion(gn,se);
				te.setAttribute("lastSelectedCell",id);
			}
		}
	}
	else if(igtbl_getOffsetY(evnt,se)>igtbl_clientHeight(se)-4 && se.cellIndex==fac-1 && igtbl_getRowSizing(gn,bandNo,se.parentNode)==2)
	{
		var cursorName = se.getAttribute("oldCursor");
		if(cursorName==null)
			se.setAttribute("oldCursor", se.style.cursor);
		se.style.cursor="n-resize";
		igtbl_colButtonMouseOut(gn);
	}
	else if(se.cellIndex==fac-1)
	{
		var cursorName = se.getAttribute("oldCursor");
		if(cursorName!=null)
		{
			se.style.cursor=cursorName;
			se.removeAttribute("oldCursor");
		}
		igtbl_colButtonMouseOut(gn);
	}
	else 
	{
		var column=(cell?cell.Column:null);
		if(column && !se.parentNode.getAttribute("groupRow") && column.ColumnType==7 && column.CellButtonDisplay==0)
			igtbl_showColButton(gn,cell.Element);
		else
			igtbl_colButtonMouseOut(gn);
	}
	return false;
}

// Event handler for mouse out from cell
function igtbl_cellMouseOut(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	var se=igtbl_srcElement(evnt);
	if(!gs || !se)
		return;
	if(se.tagName=="NOBR")
	{
		var cell=igtbl_getCellById(se.parentNode.id);
		if(cell)
			cell.Element.childNodes[0].title="";
		se=se.parentNode;
	}
	while(se && (se.tagName!="TD" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se || se.tagName!="TD" || se.id=="")
		return;
	var row;
	var id=se.id;
	var cell=igtbl_getCellById(id);
	if(cell)
	{
		row=cell.Row;
		if(!cell) return;
		id=cell.Element.id;
	}
	else row=igtbl_getRowById(se.id);
	if(!row && !cell) return;
	if(igtbl_fireEvent(gn,gs.Events.MouseOut,"(\""+gn+"\",\""+id+"\",0)")==true)
		return;
}

function igtbl_cellDblClick(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	var se=igtbl_srcElement(evnt);
	if(se.id==gn+"_vl" || se.id==gn+"_tb" || se.id==gn+"_ta")
		return;
	while(se && (se.tagName!="TD" && se.tagName!="TH" || se.id.length<gn.length || se.id.substr(0,gn.length)!=gn))
		se=se.parentNode;
	if(!se)
		return;
	if(se.tagName!="TD" && se.tagName!="TH")
		return;
	var row;
	var id=se.id;
	var cell=igtbl_getCellById(id);
	if(cell)
	{
		row=cell.Row;
		id=cell.Element.id;
	}
	else row=igtbl_getRowById(se.id);
	var column=igtbl_getColumnById(se.id);
	if(!row && !cell && !column) return;
	if(se.tagName=="TD")
	{
		if(se.getAttribute("groupRow"))
		{
			igtbl_toggleRow(gn,se.parentNode.id);
			return;
		}
		if(se.cellIndex<row.Band.firstActiveCell-1)
			return;
		if(igtbl_fireEvent(gn,gs.Events.DblClick,"(\""+gn+"\",\""+id+"\")")==true)
			return;
		if(se.cellIndex==row.Band.firstActiveCell-1)
		{
			if(gs.NeedPostBack)
				igtbl_doPostBack(gn,'RowDblClick:'+se.parentNode.id+(se.parentNode.getAttribute("level")?"\x05"+se.parentNode.getAttribute("level"):""));
			return;
		}
		var bandNo=row.Band.Index;
		if(gs.NeedPostBack)
		{
			if(igtbl_getCellClickAction(gn,bandNo)==2)
				igtbl_doPostBack(gn,'RowDblClick:'+se.parentNode.id+(se.parentNode.getAttribute("level")?"\x05"+se.parentNode.getAttribute("level"):""));
			else
				igtbl_doPostBack(gn,'CellDblClick:'+id+(cell.Element.getAttribute("level")?"\x05"+cell.Element.getAttribute("level"):""));
			return;
		}
		if(igtbl_getCellClickAction(gn,bandNo)==0)
			return;
		if(!gs.exitEditCancel)
		{
			if(cell.Column.getAllowUpdate()==3)
				row.editRow(true);
			else
				cell.beginEdit();
		}
	}
	else
	{
		if(se.cellIndex<column.Band.firstActiveCell)
			return;
		if(igtbl_fireEvent(gn,gs.Events.DblClick,"(\""+gn+"\",\""+se.id+"\")")==true)
			return;
		if(gs.NeedPostBack)
			igtbl_doPostBack(gn,'HeaderDblClick:'+se.id);
	}
}

function igtbl_selectRegion(gn,se)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	var rowContainer;
	if(!se)
		return;
	if(se.getAttribute("groupRow"))
		rowContainer=se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
	else
		rowContainer=se.parentNode.parentNode;
	var te=gs.Element;
	var selTableId = te.getAttribute("selectTable");
	var workTableId;
	if(se.getAttribute("groupRow"))
		workTableId=se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
	else
		workTableId=se.parentNode.parentNode.parentNode.id;
	if(workTableId=="")
		return;
	var bandNo=igtbl_getElementById(workTableId).getAttribute("bandNo");
	if(selTableId==workTableId)
	{
		var selMethod = te.getAttribute("selectMethod");
		if(selMethod=="row" && (se.cellIndex==igtbl_getBandFAC(gn,se)-1 || igtbl_getCellClickAction(gn,bandNo)==2 && se.cellIndex>igtbl_getBandFAC(gn,se)-1 || se.getAttribute("groupRow")))
		{
			var selRow=igtbl_getRowById(te.getAttribute("startPointRow"));
			var rowId;
			if(se.getAttribute("groupRow"))
				rowId=se.parentNode.parentNode.parentNode.parentNode.parentNode.id;
			else
				rowId=se.parentNode.id;
			var curRow=igtbl_getRowById(rowId);
			if(selRow && igtbl_getSelectTypeRow(gn,bandNo)==3 && igtbl_getCellClickAction(gn,bandNo)>0)
			{
				gs.selectRowRegion(selRow,curRow);
				igtbl_setActiveRow(gn,curRow.getFirstRow());
			}
			else
			{
				if(!(curRow.getSelected() && igtbl_getLength(gs.SelectedRows)==1))
				{
					igtbl_clearSelectionAll(gn);
					if(se.getAttribute("groupRow"))
						rowId=igtbl_getWorkRow(se.parentNode).id;
					if(igtbl_getSelectTypeRow(gn,bandNo)>1 && igtbl_getCellClickAction(gn,bandNo)>0)
						igtbl_selectRow(gn,curRow);
				}
				igtbl_setActiveRow(gn,igtbl_getFirstRow(igtbl_getElementById(rowId)));
			}
		}
		else if(selMethod=="cell" && (se.cellIndex>igtbl_getBandFAC(gn,se)-1))
		{
			var selCell=igtbl_getCellById(te.getAttribute("startPointCell"));
			var curCell=igtbl_getCellById(se.id);
			if(igtbl_getSelectTypeCell(gn,bandNo)==3 && igtbl_getCellClickAction(gn,bandNo)>0 && selCell)
			{
				gs.selectCellRegion(selCell,curCell);
				curCell.activate();
			}
			else
			{
				if(!(curCell.getSelected() && igtbl_getLength(gs.SelectedRows)==1))
				{
					igtbl_clearSelectionAll(gn);
					if(igtbl_getSelectTypeCell(gn,bandNo)>1 && igtbl_getCellClickAction(gn,bandNo)>0)
						igtbl_selectCell(gn,curCell);
				}
				igtbl_setActiveCell(gn,se);
			}
		}
	}
}

function igtbl_setSelectedRowImg(gn,row,hide)
{
	var gs=igtbl_getGridById(gn);
	if(!gs) return;
	if(row)
		igtbl_getRowById(row.id).setSelectedRowImg(hide);
}

function igtbl_setNewRowImg(gn,row)
{
	var gs=igtbl_getGridById(gn);
	if(!gs) return;
	gs.setNewRowImg(row?igtbl_getRowById(row.id):null);
}

function igtbl_clearSelectionAll(gn)
{
	var gs=igtbl_getGridById(gn);
	if(igtbl_fireEvent(gn,gs.Events.BeforeSelectChange,"(\""+gn+"\",\"\")")==true)
		return;
	var row,column,cell;
	gs.noCellChange=false;
	for(var row in gs.SelectedRows)
		igtbl_selectRow(gn,row,false,false);
	for(var column in gs.SelectedColumns)
		igtbl_selectColumn(gn,column,false,false);
	for(var row in gs.SelectedCellsRows)
	{
		for(var cell in gs.SelectedCellsRows[row])
			delete gs.SelectedCellsRows[row][cell];
		delete gs.SelectedCellsRows[row];
	}
	for(var cell in gs.SelectedCells)
		igtbl_selectCell(gn,cell,false,false);
}

function igtbl_selectCell(gn,cellID,selFlag,fireEvent)
{
	var cell=cellID;
	if(typeof(cell)=="string")
		cell=igtbl_getCellById(cellID);
	else
		cellID=cell.Element.id;
	if(!cell)
		return;
	cell.select(selFlag,fireEvent);
}

function igtbl_selectRow(gn,rowID,selFlag,fireEvent)
{
	var rowObj=rowID;
	if(typeof(rowObj)=="string")
		rowObj=igtbl_getRowById(rowID);
	else
		rowID=rowObj.Element.id;
	if(!rowObj)
		return false;
	return rowObj.select(selFlag,fireEvent);
}

function igtbl_selColRI(gn,column,bandNo,colNo)
{
	for(var i=1;i<column.parentNode.parentNode.parentNode.rows.length;i++)
	{
		var row=column.parentNode.parentNode.parentNode.rows[i];
		if(!row.getAttribute("hiddenRow") && row.parentNode.tagName!="TFOOT")
			igtbl_changeStyle(gn,igtbl_getElemVis(row.cells,column.cellIndex),igtbl_getSelCellClass(gn,bandNo,colNo));
	}
	igtbl_changeStyle(gn,column,igtbl_getSelHeadClass(gn,bandNo,colNo));
}

function igtbl_selectColumn(gn,columnID,selFlag,fireEvent)
{
	var column=igtbl_getElementById(columnID);
	var bandNo=column.parentNode.parentNode.parentNode.getAttribute("bandNo");
	if(igtbl_getSelectTypeColumn(gn,bandNo)<2)
		return;
	var colNo=igtbl_colNoFromColId(columnID);
	var gs=igtbl_getGridById(gn);
	if(gs.exitEditCancel || gs.noCellChange)
		return;
	if(fireEvent!=false)
		if(igtbl_fireEvent(gn,gs.Events.BeforeSelectChange,"(\""+gn+"\",\""+columnID+"\")")==true)
			return;
	if(selFlag!=false)
	{
		var cols=igtbl_getDocumentElement(columnID);
		if(cols.length)
			for(var j=0;j<cols.length;j++)
				igtbl_selColRI(gn,cols[j],bandNo,colNo);
		else
			igtbl_selColRI(gn,column,bandNo,colNo);
		gs.recordChange("SelectedColumns",gs.Bands[bandNo].Columns[colNo]);
		igtbl_getColumnById(columnID).Selected=true;
		gs.Element.setAttribute("lastSelectedColumn",columnID);
	}
	else
	{
		var cols=igtbl_getDocumentElement(columnID);
		if(cols.length)
			for(var j=0;j<cols.length;j++)
			{
				igtbl_changeStyle(gn,cols[j],null);
				for(var i=1;i<cols[j].parentNode.parentNode.parentNode.rows.length;i++)
				{
					var row=cols[j].parentNode.parentNode.parentNode.rows[i];
					var cell=igtbl_getElemVis(row.cells,column.cellIndex);
					if(!row.getAttribute("hiddenRow") && !gs.SelectedRows[row.id] && !gs.SelectedCells[cell.id])
						igtbl_changeStyle(gn,cell,null);
				}
			}
		else
		{
			igtbl_changeStyle(gn,column,null);
			for(var i=1;i<column.parentNode.parentNode.parentNode.rows.length;i++)
			{
				var row=column.parentNode.parentNode.parentNode.rows[i];
				var cell=igtbl_getElemVis(row.cells,column.cellIndex);
				if(!row.getAttribute("hiddenRow") && !gs.SelectedRows[row.id] && !gs.SelectedCells[cell.id])
					igtbl_changeStyle(gn,cell,null);
			}
		}
		gs.removeChange("SelectedColumns",gs.Bands[bandNo].Columns[colNo]);
		igtbl_getColumnById(columnID).Selected=false;
	}
	if(fireEvent!=false)
	{
		igtbl_fireEvent(gn,gs.Events.AfterSelectChange,"(\""+gn+"\",\""+columnID+"\");");
		if(gs.NeedPostBack)
			igtbl_moveBackPostField(gn,"SelectedColumns");
	}
}

//Expands/collapses row in internal row structure
function igtbl_stateExpandRow(gn,row,expandFlag)
{
	var gs=igtbl_getGridById(gn);
	if(!gs)
		return;
	if(expandFlag)
	{
		gs.recordChange("ExpandedRows",row,row);
		if(gs.CollapsedRows[row.Element.id])
			gs.removeChange("CollapsedRows",row);
	}
	else
	{
		gs.recordChange("CollapsedRows",row,row);
		gs.removeChange("ExpandedRows",row);
	}
}

function igtbl_arrayHasElements(array)
{
	if(!array)
		return false;
	for(element in array)
		if(array[element]!=null)
			return true;
	return false;
}

function igtbl_getLeftPos(e,cc,oe)
{
	return igtbl_getAbsolutePos("Left",e,cc,oe);
}

function igtbl_getTopPos(e,cc,oe) 
{
	return igtbl_getAbsolutePos("Top",e,cc,oe);
}

function igtbl_getAbsolutePos(where,e,cc,oe)
{
	var offs="offset"+where,cl="client"+where,bw="border"+where+"Width",sl="scroll"+where;
    var crd=e[offs];
    if(e[cl] && cc!=false)
		crd+=e[cl];
	if(typeof(oe)=="undefined")
		oe=null;
    var tmpE=e.offsetParent, cSb=true;
    while(tmpE!=null && tmpE!=oe)
    {
		crd+=tmpE[offs];
		if((tmpE.tagName=="DIV" || tmpE.tagName=="TD") && tmpE.style[bw])
		{
			var bwv=parseInt(tmpE.style[bw],10);
			if(!isNaN(bwv))
				crd+=bwv;
		}
		if(cSb && typeof(tmpE[sl])!="undefined")
		{
			var op=tmpE.offsetParent,t=tmpE;
			while(t && t!=op && t.tagName!="BODY")
			{
				if(t[sl])
					crd-=t[sl];
				t=t.parentNode;
			}
			if(tmpE.tagName=="DIV")
				cSb=false;
		}
		if(tmpE[cl] && cc!=false)
			crd+=tmpE[cl];
        tmpE=tmpE.offsetParent;
    }
	if(tmpE && tmpE[cl] && cc!=false)
		crd+=tmpE[cl];
    return crd;
}

function igtbl_getRelativePos(gn,e,where)
{
	var g=igtbl_getGridById(gn);
	var mainGrid=igtbl_getElementById(gn+"_main");
	var passedMainGrid=false;
	var offs="offset"+where,bw="border"+where+"Width";
	var ovfl="overflow",ovflC=ovfl+(where=="Left"?"X":"Y");
	var crd=e[offs];
	var parent=e.offsetParent;
	while((parent!=null && parent.tagName!="BODY" && (parent.style.position!="relative" || (parent.style.position=="relative" && parent.id=="G_"+gn))))
	{
		passedMainGrid=passedMainGrid||igtbl_isAChildOfB(mainGrid,parent);
		if(passedMainGrid && (parent.style.position=="absolute" || parent.style[ovflC] && parent.style[ovflC]!="visible" || parent.style[ovfl] && parent.style[ovfl]!="visible"))
			break;
		crd+=parent[offs];
		if(ig_csom.IsIE && (parent.tagName=="DIV" || parent.tagName=="TD" || parent.tagName=="FIELDSET") && parent.style[bw])
		{
			var bwv=parseInt(parent.style[bw],10);
			if(!isNaN(bwv))
				crd+=bwv;
		}
		if(parent==mainGrid)
			passedMainGrid=true;
		parent=parent.offsetParent;
	}
	return crd-g.Element.offsetParent["scroll"+where];
}
function igtbl_isAChildOfB(a,b){
	if(a==null||b==null)return false;
	while(a!=null){
		a=a.parentNode;
		if(a==b)return true;
	}
	return false;
}
function igtbl_moveBackPostField(gn,param)
{
	var gs=igtbl_getGridById(gn);
	gs.moveBackPostField=param;
}

function igtbl_updatePostField(gn,param)
{
}

function igtbl_isVisible(elem)
{
	while(elem && elem.tagName!="BODY")
	{
		if(elem.style && elem.style.display=="none")
			return false;
		elem=elem.parentNode;
	}
	return true;
}

var igtbl_dontHandleChkBoxChange=false;

function igtbl_chkBoxChange(evnt,gn)
{
	if(igtbl_dontHandleChkBoxChange||evnt.propertyName=="disabled")
		return false;
	var se=igtbl_srcElement(evnt);
	var c=se.parentNode;
	while(c && !(c.tagName=="TD" && c.id!=""))
		c=c.parentNode;
	if(!c) return;
	var s=se;
	var cell=igtbl_getCellById(c.id);
	if(!cell) return;
	var column=cell.Column;
	var gs=igtbl_getGridById(gn);
	var oldValue=!s.checked;
	if(gs.exitEditCancel || !cell.isEditable() || igtbl_fireEvent(gn,gs.Events.BeforeCellUpdate,"(\""+gn+"\",\""+c.id+"\",\""+s.checked+"\")"))
	{
		igtbl_dontHandleChkBoxChange=true;
		s.checked=oldValue;
		igtbl_dontHandleChkBoxChange=false;
		return true;
	}
	cell.Row._dataChanged|=2;
	if(typeof(cell._oldValue)=="undefined")
		cell._oldValue=oldValue;
	igtbl_saveChangedCell(gs,cell,s.checked.toString());
	if(!c.getAttribute("oldValue"))
		c.setAttribute("oldValue",s.checked);
	c.setAttribute("chkBoxState",s.checked.toString());
	var cca=igtbl_getCellClickAction(gn,column.Band.Index);
	if(cca==1 || cca==3)
		igtbl_setActiveCell(gn,c);
	else if(cca==2)
		igtbl_setActiveRow(gn,c.parentNode);
		
	if(cell.Node)
	{
		cell.Node.selectSingleNode("Value").text=!s.checked?"False":"True";
		gs.invokeXmlHttpRequest(gs.eReqType.UpdateCell,cell,s.checked);
	}		
	igtbl_fireEvent(gn,gs.Events.AfterCellUpdate,"(\""+gn+"\",\""+c.id+"\",\""+s.checked+"\")");
	if(gs.LoadOnDemand==3)
		gs.NeedPostBack=false;
	if(gs.NeedPostBack)
		igtbl_doPostBack(gn);
	return false;
}

function igtbl_colButtonClick(evnt,gn)
{
	var se=null;
	var b=igtbl_getElementById(gn+"_bt");
	if(b)
		se=igtbl_getElementById(b.getAttribute("srcElement"));
	if(typeof(se)=="undefined" || !se)
	{
		se=igtbl_srcElement(evnt).parentNode;
		if(se && se.tagName=="NOBR")
			se=se.parentNode;
	}
	var gs=igtbl_getGridById(gn);
	if(gs.exitEditCancel || typeof(se)=="undefined" || !se || se.id=="")
		return;
	igtbl_fireEvent(gn,gs.Events.ClickCellButton,"(\""+gn+"\",\""+se.id+"\")");
	if(gs.NeedPostBack)		
		igtbl_doPostBack(gn,'CellButtonClick:'+se.id+"\x05"+igtbl_getCellById(se.id).getLevel(true));
		
}

function igtbl_colButtonMouseOut(gn)
{
	var b=igtbl_getElementById(gn+"_bt");
	if(!b)
		return;
	if(b.getAttribute("noOnBlur"))
		return;
	if(b.style.display=="")
	{
		b.setAttribute("noOnBlur",true);
		b.style.display="none";
		b.removeAttribute("srcElement");
		var gs=igtbl_getGridById(gn);
		if(!gs.Activation.AllowActivation)
			return;
		if(gs.oActiveCell)
		{
			if(!gs.oActiveCell.Row.GroupByRow && gs.oActiveCell.Column.ColumnType==7 && gs.oActiveCell.Column.CellButtonDisplay==0)
				igtbl_showColButton(gn,gs.oActiveCell.Element);
		}
		window.setTimeout("igtbl_clearNoOnBlurBtn('"+gn+"')",100);
	}
}

function igtbl_clearNoOnBlurBtn(gn)
{
	var b=igtbl_getElementById(gn+"_bt");
	b.removeAttribute("noOnBlur");
}

function igtbl_getColumnNo(gn,cell)
{
	if(cell)
	{
		var column=igtbl_getColumnById(cell.id);
		if(column)
			return column.Index;
		else
			return -1;
	}
}

function igtbl_getBandNo(gn,cell)
{
	if(cell)
		return parseInt(cell.parentNode.parentNode.parentNode.getAttribute("bandNo"));
}

function igtbl_getFirstRow(row)
{
	if(row.getAttribute("groupRow"))
		return row.childNodes[0].childNodes[0].childNodes[0].rows[0];
	else
		return row;
}

function igtbl_getWorkRow(row)
{
	if(!row) return;
	if(row.getAttribute("groupRow"))
	{
		var id=row.id.split("_");
		if(id[0].length>3 && id[0].substr(id[0].length-3)=="sgr")
			return row.parentNode.parentNode.parentNode.parentNode;
		else
			return row;
	}
	else
		return row;
}

function igtbl_getCurCell(se)
{
	var c=null;
	while(se && !c) 
		if(se.tagName=="TD")
			c=se;
		else
			se=se.parentNode;
	return c;
}

function igtbl_getColumnByCellId(cellID)
{
	var cell=igtbl_getCellById(cellID);
	if(!cell)
		return null;
	if(cell.Band.Index==0 && !cell.Band.IsGrouped && cell.Band.ColHeadersVisible==1 && (cell.Band.Grid.StationaryMargins==1 || cell.Band.Grid.StationaryMargins==3))
		return igtbl_getElemVis(cell.Band.Grid.StatHeader.Element.rows[0].cells,cell.Element.cellIndex);
	if(cell.Element.parentNode.parentNode.parentNode.childNodes[1].tagName=="THEAD")
		return igtbl_getElemVis(cell.Element.parentNode.parentNode.parentNode.childNodes[1].rows[0].cells,cell.Element.cellIndex);
	return null;
}

function igtbl_colButtonEvent(evnt,gn)
{
}

function igtbl_isCell(itemName)
{
	var parts = itemName.split("_");
	if(parts[0].charAt(parts[0].length-2)=="r" && parts[0].charAt(parts[0].length-1)=="c")
		return true;
	return false;
}

function igtbl_isColumnHeader(itemName)
{
	var parts = itemName.split("_");
	if(parts[0].charAt(parts[0].length-1)=="c" && !igtbl_isCell(itemName))
		return true;
	return false;
}

function igtbl_isRowLabel(itemName)
{
	var parts = itemName.split("_");
	if(parts[0].charAt(parts[0].length-1)=="l")
		return true;
	return false;
}

function igtbl_fireEvent(gn,eventObj,eventString)
{
	var gs=igtbl_getGridById(gn);
	if(!gs || !gs.GridIsLoaded) return;
	var result=false;
	if(eventObj[0]!="")
		result=eval(eventObj[0]+eventString);
	if(gs.GridIsLoaded && result!=true && eventObj[1]==1 && !gs.CancelPostBack)
		igtbl_needPostBack(gn);
	gs.CancelPostBack=false;
	return result;
}

function igtbl_dropDownChange(evnt,gn)
{
	var sel=igtbl_getElementById(gn+"_vl");
	if(sel && sel.style.display=="")
		igtbl_fireEvent(gn,igtbl_getGridById(gn).Events.ValueListSelChange,"(\""+gn+"\",\""+gn+"_vl\",\""+sel.getAttribute("currentCell")+"\");");
}

function igtbl_inEditMode(gn)
{
	var g = igtbl_getGridById(gn);
	if(g.editorControl && g.editorControl.getVisible())
		return true;
	var sel=igtbl_getElementById(gn+"_vl");
	if(sel && sel.style.display=="")
		return true;
	var tb=igtbl_getElementById(gn+"_tb");
	if(tb && tb.style.display=="")
		return true;
	var ta=igtbl_getElementById(gn+"_ta");
	if(ta && ta.style.display=="")
		return true;
	return false;
}

function igtbl_bandNoFromColId(colId)
{
	var s=colId.split("_");
	if(s.length<3)
		return null;
	return parseInt(s[s.length-2]);
}

function igtbl_colNoFromColId(colId)
{
	var s=colId.split("_");
	if(s.length<3)
		return null;
	return parseInt(s[s.length-1]);
}

function igtbl_colNoFromId(id)
{
	if(!id)
		return null;
	var s=id.split("_");
	if(s.length==0)
		return null;
	return parseInt(s[s.length-1]);
}

function igtbl_doPostBack(gn,args)
{
	var gs=igtbl_getGridById(gn);
	if(gs.GridIsLoaded && !gs.CancelPostBack)
	{
		gs.GridIsLoaded=false;
		if(!args)
			args="";
		__doPostBack(gs.UniqueID,args);
	}
}

function igtbl_scrollToView(gn,child,childWidth,nfWidth)
{
	if(!child)
		return;
	var gs=igtbl_getGridById(gn);
	var parent=gs.Element.parentNode;
	var scrParent=parent;
	if(gs.UseFixedHeaders)
		scrParent=gs.scrElem;
	if(scrParent.scrollWidth<=scrParent.offsetWidth && scrParent.scrollHeight<=scrParent.offsetHeight)
		return;
	var childLeft=igtbl_getLeftPos(child);
	var parentLeft=igtbl_getLeftPos(parent);
	var childTop=igtbl_getTopPos(child);
	var parentTop=igtbl_getTopPos(parent);
	var childRight=childLeft+child.offsetWidth;
	var parentRight=parentLeft+parent.offsetWidth;
	var childBottom=childTop+child.offsetHeight;
	var parentBottom=parentTop+parent.offsetHeight;
	var hsw=(parent.scrollWidth>parent.offsetWidth && typeof(nfWidth)=="undefined"?18:0)
	var vsw=((parent.scrollWidth>parent.offsetWidth || parent.scrollHeight>parent.offsetHeight) && typeof(nfWidth)=="undefined"?18:0)
	if(childBottom>parentBottom-hsw && childTop-(parentTop-childTop)>parentTop)
		scrParent.scrollTop+=childBottom-parentBottom+hsw;
	if(childTop<parentTop)
		scrParent.scrollTop-=parentTop-childTop;
	if(typeof(nfWidth)!="undefined" && (childLeft==childRight || childRight-childLeft<childWidth))
	{
		scrParent.scrollLeft=nfWidth;
		return;
	}
	if(childRight>parentRight-vsw && childLeft-(childRight-parentRight)>parentLeft)
		scrParent.scrollLeft+=childRight-parentRight+vsw;
	if(childLeft<parentLeft)
		scrParent.scrollLeft-=parentLeft-childLeft;
}

function igtbl_unloadGrid(gn)
{
	igtbl_gridState[gn].disposing=true;
	igtbl_dispose(igtbl_gridState[gn]);
	delete igtbl_gridState[gn];
}

function igtbl_dispose(obj)
{
	if(ig_csom.IsNetscape || ig_csom.IsNetscape6)
		return;
	for(var item in obj)
	{
		if(typeof(obj[item])!="undefined" && obj[item]!=null && !obj[item].tagName && !obj[item].disposing && typeof(obj[item])!="string")
		{
			try {
				obj[item].disposing=true;
				igtbl_dispose(obj[item]);
			} catch(exc1) {;}
		}
		try {
			delete obj[item];
		} catch(exc2) {
			return;
		}
	}
}

function igtbl_getElemVis(cols,index)
{
	var i=0,j=-1;
	while(cols && cols[i] && j!=index)
	{
		if(cols[i].style.display!="none")
			j++;
		i++;
	}
	return cols[i-1];
}

function igtbl_trim(s)
{
	if(!s)
		return s;
	s=s.toString();
	var result=s;
	for(var i=0;i<s.length;i++)
		if(s.charAt(i)!=' ')
			break;
	result=s.substr(i,s.length-i);
	for(var i=result.length-1;i>=0;i--)
		if(result.charAt(i)!=' ')
			break;
	result=result.substr(0,i+1);
	return result;
}

function igtbl_cancelNoOnBlurTB(gn)
{
	var textBox=igtbl_getElementById(gn+"_tb");
	if(textBox && textBox.style.display=="")
		textBox.removeAttribute("noOnBlur");
	var sel=igtbl_getElementById(gn+"_vl");
	if(sel && sel.style.display=="")
		sel.removeAttribute("noOnBlur");
}

function igtbl_cancelNoOnBlurDD(gn)
{
	if(arguments.length==0)
		gn=igtbl_lastActiveGrid;
	var gs=igtbl_getGridById(gn);
	if(gs && gs.editorControl)
		gs.editorControl.Element.removeAttribute("noOnBlur");
}

function igtbl_getChildRows(gn,row)
{
	var rows;
	if(row.getAttribute("groupRow"))
		rows=row.childNodes[0].childNodes[0].childNodes[0].rows[1].childNodes[0].childNodes[0].tBodies[0].rows;
	else
	{
		if(row.nextSibling && row.nextSibling.getAttribute("hiddenRow"))
			rows=row.nextSibling.childNodes[igtbl_getBandFAC(gn,row)].childNodes[0].tBodies[0].rows;
		else
			rows=null;
	}
	return rows;
}

function igtbl_rowsCount(rows)
{
	var i=0,j=0;
	while(j<rows.length)
		if(!rows[j++].getAttribute("hiddenRow"))
			i++;
	return i;
}

function igtbl_visRowsCount(rows)
{
	var i=0,j=0;
	while(j<rows.length)
	{
		if(!rows[j].getAttribute("hiddenRow") && rows[j].style.display=="")
			i++;
		j++;
	}
	return i;
}

var igtbl_oldOnUnload;
var igtbl_bInsideOldOnUnload=false;
function igtbl_unload()
{
	if(igtbl_oldOnUnload && !igtbl_bInsideOldOnUnload)
	{
		igtbl_bInsideOldOnUnload=true;
		igtbl_oldOnUnload();
		igtbl_bInsideOldOnUnload=false;
	}
	for(var gridId in igtbl_gridState)
	{
		var p=igtbl_getElementById(gridId);
		p.value=ig_ClientState.getText(igtbl_gridState[gridId].ViewState);
		if(igtbl_gridState[gridId].unloadGrid)
			igtbl_gridState[gridId].unloadGrid();
		else
			delete igtbl_gridState[gridId];
	}
}

if(window.addEventListener)
	window.addEventListener('unload',igtbl_unload,false);
else if(window.onunload!=igtbl_unload)
{
	igtbl_oldOnUnload=window.onunload;
	window.onunload=igtbl_unload;
}

function igtbl_addEventListener(obj,eventName,fRef)
{
	if(obj.addEventListener)
		obj.addEventListener(eventName,fRef,true);
	else
		eval("obj.on"+eventName+"=fRef;");
}

/* obsolete */
/* use igcsom.cancelEvent instead */
function igtbl_cancelEvent(evnt)
{
	ig_cancelEvent(evnt);
	return false;
}

function igtbl_getRegExpSafe(val)
{
	if(typeof(val)=="undefined" || val==null)
		return "";
	var res=val.toString().replace("\\","\\\\");
	res=res.replace("^","\\^");
	res=res.replace("*","\\*");
	res=res.replace("$","\\$");
	res=res.replace("+","\\+");
	res=res.replace("?","\\?");
	res=res.replace(",","\\,");
	res=res.replace(".","\\.");
	res=res.replace(":","\\:");
	res=res.replace("=","\\=");
	res=res.replace("-","\\-");
	res=res.replace("!","\\!");
	res=res.replace("|","\\|");
	res=res.replace("(","\\(");
	res=res.replace(")","\\)");
	res=res.replace("[","\\[");
	res=res.replace("]","\\]");
	res=res.replace("{","\\{");
	res=res.replace("}","\\}");
	return res;
}

function igtbl_saveChangedCell(gs,cell,value)
{
	if(typeof(gs.ChangedRows[cell.Row.Element.id])=="undefined")
		gs.ChangedRows[cell.Row.Element.id]=[];
	if(cell.Element)
		gs.ChangedRows[cell.Row.Element.id][cell.Element.id]=true;
	//gs.removeChange("ChangedCells",cell);
	gs.recordChange("ChangedCells",cell,value);
}

function igtbl_endCustomEdit()
{
	if(arguments.length<3)
		return;
	var oEditor=arguments[0];
	var oEvent=arguments[arguments.length-2];
	var oThis=arguments[arguments.length-1];
	if(oEvent && typeof(oEvent.event)!="undefined" && oEvent.event.keyCode!=9 && oEvent.event.keyCode!=13 && oEvent.event.keyCode!=27 && oEvent.event.keyCode!=0)
		return;
	var se=null;
	if(oEditor.Element)
		se=oEditor.Element;
	if(se!=null)
	{
		if(se.getAttribute("noOnBlur"))
			return igtbl_cancelEvent(oEvent.event);
		if(se.getAttribute("editorControl"))
		{
			if(!oEditor.getVisible())
				return;
			var cell=igtbl_getElementById(se.getAttribute("currentCell"));
			if(!cell)
				return;
			var gs=oThis;
			var cellObj=igtbl_getCellById(cell.id);
			if(oEvent && typeof(oEvent.event)!="undefined" && oEvent.event.keyCode==27)
				oEditor.setValue(cellObj.getValue(),false);
			if(typeof(oEditor.getValue())!="undefined")
				cellObj.setValue(oEditor.getValue());
			if(igtbl_fireEvent(gs.Id,gs.Events.BeforeExitEditMode,"(\""+gs.Id+"\",\""+cell.id+"\")")==true)
			{
				if(!gs.exitEditCancel && !gs.insideSetActive)
				{
					gs.insideSetActive=true;
					igtbl_setActiveCell(gs.Id,cell);
					gs.insideSetActive=false;
				}
				gs.exitEditCancel=true;
				return;
			}
			oEditor.setVisible(false);
			oEditor.removeEventListener("blur",igtbl_endCustomEdit);
			oEditor.removeEventListener("keydown",igtbl_endCustomEdit);
			gs.exitEditCancel=false;
			se.removeAttribute("currentCell");
			se.removeAttribute("oldInnerText");
			igtbl_fireEvent(gs.Id,gs.Events.AfterExitEditMode,"(\""+gs.Id+"\",\""+cell.id+"\");");
			gs.editorControl = null;
			se.removeAttribute("editorControl");
			if(oEvent && typeof(oEvent.event)!="undefined" && (oEvent.event.keyCode==9 || oEvent.event.keyCode==13))
			{
				var res=null;
				if(typeof(igtbl_ActivateNextCell)!="undefined")
				{
					if(oEvent.event.shiftKey && oEvent.event.keyCode==9)
						res=igtbl_ActivatePrevCell(gs.Id);
					else
						res=igtbl_ActivateNextCell(gs.Id);
				}
				if(res && igtbl_getCellClickAction(gs.Id,cellObj.Column.Band.Index)==1)
					igtbl_EnterEditMode(gs.Id);
				igtbl_activate(gs.Id);
				oEvent.cancel=true;
			}
			else
				gs.alignGrid();
			igtbl_blur(gs.Id);
			if(gs.NeedPostBack)
				igtbl_doPostBack(gs.Id);
		}
	}
}

function igtbl_getLength(obj)
{
	var count=0;
	for(var item in obj)
		count++;
	return count;
}

function igtbl_setActiveCell(gn,cell,force)
{
	var g=igtbl_getGridById(gn);
	if(g)
		g.setActiveCell(cell?igtbl_getCellById(cell.id):null,force);
	return;
}

function igtbl_setActiveRow(gn,row,force)
{
	var g=igtbl_getGridById(gn);
	if(g)
		g.setActiveRow(row?igtbl_getRowById(row.id):null,force);
	return;
}

function igtbl_sortGroupedRows(rows,bandNo,colId)
{
	if(rows.Band.Index==bandNo && rows.getRow(0).Element.getAttribute("groupRow")==colId)
	{
		rows.sort();
		return;
	}
	for(var i=0;i<rows.length;i++)
	{
		var row=rows.getRow(0);
		if(row.Rows && row.Rows.length>0)
			igtbl_sortGroupedRows(row.Rows,bandNo,colId);
	}
}

function igtbl_fixedClick(evnt)
{
	var se=igtbl_srcElement(evnt);
	var pn=se.parentNode;
	while(pn && pn.tagName!="TH") pn=pn.parentNode;
	if(!pn || !pn.id) return;
	var column=igtbl_getColumnById(pn.id);
	if(column.Band.Grid.UseFixedHeaders)
	{
		if(column.getFixed())
			igtbl_doPostBack(column.Band.Grid.Id,"Unfix:"+column.Band.Index+":"+column.Index);
		else
			igtbl_doPostBack(column.Band.Grid.Id,"Fix:"+column.Band.Index+":"+column.Index);
		return igtbl_cancelEvent(evnt);
	}
}

function igtbl_mouseWheel(evnt,gn)
{
	var gs=igtbl_getGridById(gn);
	if(!gs || !gs.scrElem) return;
	if(evnt.wheelDelta)
		gs.scrElem.scrollTop-=evnt.wheelDelta/3;
}

function igtbl_onScrollFixed(evnt,gn)
{
	var g=igtbl_getGridById(gn)
	if(!g || !g.scrElem) return;
	var s=g.Element.parentNode.scrollTop;
	g.Element.parentNode.scrollTop=0;
	g.scrElem.scrollTop=s;
}

function igtbl_splitUrl(url)
{
	var targetFrame=null;
	if(url.substr(0,1)=="@")
	{
		targetFrame="_blank";
		url=url.substr(1);
		var cb=-1;
		if(url.substr(0,1)=="[" && (cb=url.indexOf("]"))>1)
		{
			targetFrame=url.substr(1,cb-1);
			url=url.substr(cb+1);
		}
	}
	return [url,targetFrame];
}

function igtbl_navigateUrl(url)
{
	var urls=igtbl_splitUrl(url);
	ig_csom.navigateUrl(urls[0],urls[1]);
	igtbl_dispose(urls);
}

function igtbl_isChild(gn,e)
{
	if(!e) return false;
	var ge=igtbl_getElementById(gn+"_main");
	var p=e.parentNode;
	while(p && p!=ge)
		p=p.parentNode;
	return p!=null;
}

function igtbl_blur(gn)
{
	window.setTimeout("igtbl_blurTimeout('"+gn+"')",100);
}

function igtbl_blurTimeout(gn)
{
	var g=igtbl_getGridById(gn);
	if(!g) return;
	var ar=g.getActiveRow();
	if(!ar || !ar.processUpdateRow || igtbl_inEditMode(gn) || igtbl_isChild(gn,document.activeElement)) return;
	ar.processUpdateRow();
}
