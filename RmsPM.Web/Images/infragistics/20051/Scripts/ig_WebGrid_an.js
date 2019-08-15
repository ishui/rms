/* 
Infragistics UltraWebGrid Script 
Version 5.1.20051.37
Copyright (c) 2001-2005 Infragistics, Inc. All Rights Reserved.
*/

function igtbl_addNewClickDown(evnt,gn) 
{
	if(igtbl_button(gn,evnt)!=0)
		return;
	var se=igtbl_srcElement(evnt);
	if(se.tagName=="TD" && se.getAttribute("addNew") && !igtbl_isDisabled(se))
		igtbl_changeStyle(gn,se,gn+'_SelHeadClass');
	igtbl_activate(gn);
}

function igtbl_addNewClickUp(evnt,gn) 
{
	if(igtbl_button(gn,evnt)!=0)
		return;
	var se=igtbl_srcElement(evnt);
	if(se.tagName=="TD" && se.getAttribute("addNew") && !igtbl_isDisabled(se))
	{
		igtbl_changeStyle(gn,se,null);
		igtbl_addNew(gn,parseInt(se.getAttribute("bandNo"),10));
	}
	igtbl_activate(gn);
}

function igtbl_addNew(gn,bandNo)
{
	var g=igtbl_getGridById(gn);
	if(g.Bands[bandNo].AllowAddNew==2 || g.Bands[bandNo].AllowAddNew==0 && g.AllowAddNew!=1 || g.Bands[bandNo].IsGrouped)
		return null;
	var table=g.Bands[bandNo].curTable;
	var fac=g.Bands[bandNo].firstActiveCell;
	var clickRow=null;
	if(g.ActiveCell!="")
		clickRow=igtbl_getRowById(g.ActiveCell);
	else if(g.ActiveRow!="")
		clickRow=igtbl_getRowById(g.ActiveRow);
	if(!clickRow && bandNo==0)
		clickRow=g.Rows.getRow(0);
	var row=null,rowObj=null;
	if(g.Rows.Node)
	{
		g.noMoreRows=true;
		window.setTimeout("igtbl_cancelNoMoreRows('"+g.Id+"')",500);
		var rows=g.Rows;
		if(clickRow)
		{
			if(bandNo==clickRow.Band.Index+1)
			{
				if(!clickRow.Rows)
				{
					if(g.LoadOnDemand==3 && !clickRow.HiddenElement)
						clickRow.requestChildRows();
					else
						clickRow.Rows=new igtbl_Rows(clickRow.Node.selectSingleNode("Rows"),g.Bands[clickRow.Band.Index+1],clickRow);
				}
				rows=clickRow.Rows;
			}
			else if(bandNo>clickRow.Band.Index+1)
				return null;
			else
			{
				var pr=clickRow;
				while(pr && pr.Band.Index!=bandNo)
					pr=pr.ParentRow;
				if(!pr)
					return null;
				rows=pr.OwnerCollection;
			}
		}
		else if(bandNo!=0)
			return null;
		rowObj=rows.addNew();
		if(!rowObj)
			return null;
		row=rowObj.Element;
	}
	else
	{
		if(!table)
		{
			var hidRow=document.createElement("tr");
			var rn=clickRow.Element.id.split("_");
			rn[0]=gn+"rh";
			hidRow.id=rn.join("_");
			hidRow.setAttribute("hiddenRow",true);
			var ec=document.createElement("td");
			ec.className=igtbl_getExpAreaClass(gn,bandNo-1);
			ec.style.borderWidth=0;
			ec.style.textAlign="center";
			ec.style.padding=0;
			ec.style.cursor="default";
			ec.innerHTML="&nbsp;";
			hidRow.appendChild(ec);
			if(igtbl_getRowSelectors(gn,bandNo-1)==1)
			{
				var rsc=document.createElement("td");
				rsc.className=igtbl_getRowLabelClass(gn,bandNo-1);
				rsc.innerHTML="&nbsp;";
				hidRow.appendChild(rsc);
			}
			var majCell=document.createElement("td");
			majCell.style.overflow="auto";
			majCell.style.border=0;
			majCell.style.width="100%";
			majCell.colSpan=g.Bands[bandNo-1].VisibleColumnsCount;
			hidRow.appendChild(majCell);
			table=document.createElement("table");
			rn[0]=gn+"t";
			table.id=rn.join("_");
			table.border=0;
			table.cellPadding=g.Element.cellPadding;
			table.cellSpacing=g.Element.cellSpacing;
			table.setAttribute("bandNo",bandNo);
			table.style.cssText=g.Element.style.cssText;
			table.style.position="";
			table.style.width="";
			var colGr=document.createElement("colgroup");
			var col;

			col=document.createElement("col");
			col.width=g.Bands[bandNo].Indentation;
			colGr.appendChild(col);

			if(igtbl_getRowSelectors(gn,bandNo)==1)
			{
				col=document.createElement("col");
				col.width=(g.Bands[bandNo].RowLabelWidth?g.Bands[bandNo].RowLabelWidth:"22px");
				colGr.appendChild(col);
			}
			for(var i=0;i<g.Bands[bandNo].Columns.length;i++)
			{
				var clmn=g.Bands[bandNo].Columns[i];
				if(!clmn.getHidden() && clmn.hasCells())
				{
					col=document.createElement("col");
					col.style.width=clmn.Width;
					colGr.appendChild(col);
				}
			}
			for(var i=0;i<g.Bands[bandNo].Columns.length;i++)
				if(g.Bands[bandNo].Columns[i].getHidden())
				{
					col=document.createElement("col");
					col.style.width=0;
					colGr.appendChild(col);
				}
			table.appendChild(colGr);
			var tHead=document.createElement("thead");
			igtbl_addEventListener(tHead,"mousedown",igtbl_headerClickDown);
			igtbl_addEventListener(tHead,"mouseup",igtbl_headerClickUp);
			igtbl_addEventListener(tHead,"mouseout",igtbl_headerMouseOut);
			igtbl_addEventListener(tHead,"mousemove",igtbl_headerMouseMove);
			igtbl_addEventListener(tHead,"mouseover",igtbl_headerMouseOver);
			igtbl_addEventListener(tHead,"contextmenu",igtbl_headerContextMenu);
			var tr=document.createElement("tr");
			var th;
			var img;

			th=document.createElement("th");
			th.className=g.Bands[bandNo].NonSelHeaderClass;
			th.height=g.Bands[bandNo].DefaultRowHeight;
			img=document.createElement("img");
			img.src=g.BlankImage;
			img.border=0;
			th.appendChild(img);
			tr.appendChild(th);

			if(igtbl_getRowSelectors(gn,bandNo)==1)
			{
				th=document.createElement("th");
				th.className=g.Bands[bandNo].NonSelHeaderClass;
				th.height=g.Bands[bandNo].DefaultRowHeight;
				img=document.createElement("img");
				img.src=g.BlankImage;
				img.border=0;
				th.appendChild(img);
				tr.appendChild(th);
			}
			for(var i=0;i<g.Bands[bandNo].Columns.length;i++)
			{
				var column=g.Bands[bandNo].Columns[i];
				if(!column.Hidden && column.hasCells())
				{
					th=document.createElement("th");
					th.id=gn+"c"+"_"+bandNo+"_"+i.toString();
					th.className=column.getHeadClass();
					th.setAttribute("columnNo",i);
					th.innerHTML=column.HeaderText;
					tr.appendChild(th);
				}
			}
			tHead.appendChild(tr);
			if(g.Bands[bandNo].ColHeadersVisible!=1)
				tHead.style.display="none";
			table.appendChild(tHead);
			var tBody=document.createElement("tbody");
			table.appendChild(tBody);
			if(g.Bands[bandNo].ColFootersVisible==1)
			{
				var tFoot=document.createElement("tfoot");
				var tr=document.createElement("tr");
				var th;

				th=document.createElement("th");
				th.className=igtbl_getExpAreaClass(gn,bandNo);
				th.innerHTML="&nbsp;";
				tr.appendChild(th);

				if(igtbl_getRowSelectors(gn,bandNo)==1)
				{
					th=document.createElement("th");
					th.className=igtbl_getRowLabelClass(gn,bandNo);
					th.innerHTML="&nbsp;";
					tr.appendChild(th);
				}
				for(var i=0;i<g.Bands[bandNo].Columns.length;i++)
				{
					var column=g.Bands[bandNo].Columns[i];
					if(!column.Hidden && column.hasCells())
					{
						th=document.createElement("th");
						th.id=gn+"f"+"_"+bandNo+"_"+i.toString();
						th.className=igtbl_getFooterClass(gn,bandNo,i);
						th.innerHTML="&nbsp;";
						tr.appendChild(th);
					}
				}
				tFoot.appendChild(tr);
				table.appendChild(tFoot);
			}
			majCell.appendChild(table);
			clickRow.Element.childNodes[0].oldInnerHTML=clickRow.Element.childNodes[0].innerHTML;
			clickRow.Element.childNodes[0].innerHTML="<img src="+igtbl_getCollapseImage(gn,bandNo-1)+" border=0 onclick=\"igtbl_toggleRow('"+gn+"','"+clickRow.Element.id+"');\">";
			clickRow.Element.parentNode.insertBefore(hidRow,clickRow.Element.nextSibling);
			g.ExpandedRows[clickRow.Element.id]=true;
			clickRow.HiddenElement=hidRow;
		}
		row=document.createElement("tr");
		var rows=table.tBodies[0].rows;
		var lastRow,lrObj,altRow;
		if(rows.length>0)
			lastRow=rows[rows.length-1];
		if(lastRow)
		{
			if(lastRow.parentNode.tagName=="TFOOT")
				lastRow=lastRow.previousSibling;
			if(lastRow.getAttribute("hiddenRow"))
				lastRow=lastRow.previousSibling;
			lrObj=igtbl_getRowById(lastRow.id);
			altRow=lastRow.getAttribute("alt")!="true" && (g.Bands[bandNo].AltClass!="" || g.AltClass!="");
			var rLastRowId=lrObj.OwnerCollection.getLastRowId();
			if(lastRow.id!=rLastRowId)
			{
				var l1=igtbl_getRowById(rLastRowId);
				if(l1)
				{
					lrObj=l1;
					lastRow=l1.Element;
				}
			}
			var rn=rLastRowId.split("_");
			rn[rn.length-1]=parseInt(rn[rn.length-1],10)+1;
			row.id=rn.join("_");
			lrObj.OwnerCollection.setLastRowId(row.id);
		}
		else
		{
			if(!clickRow)
				row.id=gn+"r_"+(g.AllowPaging && g.EIRM?(g.CurrentPageIndex-1)*g.PageSize:0)
			else
				row.id=clickRow.Element.id+"_0";
		}
		if(g.Bands[bandNo].DefaultRowHeight!="")
			row.style.height=g.Bands[bandNo].DefaultRowHeight;
		if(igtbl_fireEvent(gn,g.Events.BeforeRowInsert,"(\""+gn+"\",\""+row.id+"\")")==true)
		{
			if(!g.Bands[bandNo].curTable && typeof(clickRow)!="undefined" && clickRow!=null)
			{
				clickRow.Element.childNodes[0].innerHTML=clickRow.Element.childNodes[0].oldInnerHTML;
				clickRow.Element.parentNode.removeChild(table.parentNode.parentNode);
			}
			if(g.NeedPostBack)
				igtbl_doPostBack(gn,"");
			return null;
		}
		if(lastRow)
		{
			lrObj.OwnerCollection.rows[lrObj.OwnerCollection.length]=null;
			lrObj.OwnerCollection.length++;
			if(lrObj.ParentRow)
			{
				lrObj.ParentRow.ChildRowsCount++;
				lrObj.ParentRow.VisChildRowsCount++;
			}
		}
		if(altRow)
			row.setAttribute("alt","true");
		if(lastRow && rows[rows.length-1].parentNode.tagName=="TFOOT")
			table.tBodies[0].insertBefore(row,rows[rows.length-1]);
		else
			table.tBodies[0].appendChild(row);
		if(!clickRow)
		{
			delete g.Rows;
			g.Rows=new igtbl_Rows(null,g.Bands[0],null);
			g.Rows.length=1;
			g.Rows.rows[0]=null;
		}
		else if(!lastRow)
		{
			clickRow.ChildRowsCount=1;
			clickRow.VisChildRowsCount=1;
			clickRow.Rows=new igtbl_Rows(null,g.Bands[clickRow.Band.Index+1],clickRow);
			clickRow.Rows.length=1;
			clickRow.Rows.rows[0]=null;
			clickRow.Expandable=true;
		}
		var phCellNo=0;
		for(var i=0;i<g.Bands[bandNo].Columns.length+fac;i++)
		{
			if(i>=fac && !g.Bands[bandNo].Columns[i-fac].hasCells())
				continue;
			var cell=document.createElement("td");
			var cn;
			if(lastRow)
			{
				cn=row.id.split("_");
				if(i>=fac)
				{
					cn[0]=gn+"rc";
					cn[cn.length-1]--;
					cn[cn.length]=i-fac;
				}
				else if(lastRow.cells[phCellNo].id)
				{
					cn[0]=gn+"l";
					cn[cn.length-1]--;
				}
			}
			else
			{
				if(clickRow)
				{
					cn=clickRow.Element.id.split("_");
					cn[0]=gn+"rc";
					cn[cn.length]=-1;
					cn[cn.length]=i-fac;
				}
				else
				{
					cn=new Array();
					cn[0]=gn+"rc";
					cn[1]=(g.AllowPaging && g.EIRM?(g.CurrentPageIndex-1)*g.PageSize:0)-1;
					cn[2]=i-fac;
				}
			}
			if(g.Bands[bandNo].DefaultRowHeight!="")
				cell.height=g.Bands[bandNo].DefaultRowHeight;
			if(g.Bands.length>1 && phCellNo==0)
			{
				cell.className=igtbl_getExpAreaClass(gn,bandNo);
				cell.style.borderWidth=0;
				cell.style.textAlign="center";
				cell.style.padding=0;
				cell.style.cursor="default";
				cell.innerHTML="<img src='"+g.BlankImage+"' border=0 imgType='blank' style='visibility:hidden;'>";
			}
			else if(igtbl_getRowSelectors(gn,bandNo)==1 && (g.Bands.length>1 && phCellNo==1 || phCellNo==0))
			{
				cell.className=igtbl_getRowLabelClass(gn,bandNo);
				if(lastRow)
				{
					cn[cn.length-1]++;
					cell.id=cn.join("_");
				}
				else
				{
					if(clickRow)
					{
						cn=clickRow.Element.id.split("_");
						cn[0]=gn+"l";
						cn[cn.length]=0;
						cell.id=cn.join("_");
					}
					else
						cell.id=gn+"l_"+(g.AllowPaging && g.EIRM?(g.CurrentPageIndex-1)*g.PageSize:0);
				}
				cell.innerHTML="<img src='"+g.BlankImage+"' border=0 imgType='blank' style='visibility:hidden;'>";
				cell.style.textAlign="center";
			}
			else
			{
				var columnNo=i-fac;
				var column=g.Bands[bandNo].Columns[columnNo];
				if(column.ServerOnly)
				{
					phCellNo++;
					delete cn;
					continue;
				}
				cn[cn.length-2]++;
				cell.id=cn.join("_");
				if(lastRow && lastRow.getAttribute("level"))
				{
					var cl=lastRow.getAttribute("level").split("_");
					cl[cl.length]=i-fac;
					cl[cl.length-2]=parseInt(cl[cl.length-2],10)+1;
					cell.setAttribute("level",cl.join("_"));
				}
				var cwk=cell;
				if(g.UseFixedHeaders && !column.Fixed)
				{
					var div=document.createElement("div");
					div.className=g.Id+"-cds";
					cell.appendChild(div);
					cwk=div;
					div=document.createElement("div");
					cwk.appendChild(div);
					cwk=div;
				}
				if(altRow)
					cwk.className=g.Bands[bandNo].getAltClass();
				else
					cwk.className=g.Bands[bandNo].getItemClass();
				if(column.CssClass)
					cwk.className+=(cwk.className.length>0?" ":"")+column.CssClass;
				if(column.Style)
					cwk.style.cssText=column.Style;
				if(g.Bands[bandNo].Columns[i-fac].Hidden)
					cell.style.display='none';					
				var it_str="";
				if(!column.Wrap)
					it_str+="<nobr>";
				switch(column.ColumnType)
				{
					case 3:
						it_str+="<input type=checkbox"+(igtbl_getAllowUpdate(gn,bandNo,columnNo)==1?"":" disabled")+" on"+(ig_csom.IsIE?"property":"")+"change='igtbl_chkBoxChange(event,\""+gn+"\");'>";
						break;
					case 7:
						var bc=g.Bands[bandNo].Columns[columnNo].ButtonClass;
						var bs=g.Bands[bandNo].Columns[columnNo].ButtonStyle;
						if(column.CellButtonDisplay==1)
							it_str+="<input type=button style='"+bs+"' onclick=\"igtbl_colButtonClick(event,'"+gn+"');\""+(bc==""?"":" class='"+bc+"'")+">";
						else
							it_str+="&nbsp;";
						break;
					case 9:
						it_str+="<a href=''>&nbsp;</a>";
						break;
					default:
						it_str+="&nbsp;";
						break;
				}
				if(!column.Wrap)
					it_str+="</nobr>";
				cwk.innerHTML=it_str;
			}
			row.appendChild(cell);
			phCellNo++;
			delete cn;
		}
		rowObj=igtbl_getRowById(row.id);
		if(lastRow && lastRow.getAttribute("level"))
		{
			var rl=lastRow.getAttribute("level").split("_");
			rl[rl.length-1]=parseInt(rl[rl.length-1],10)+1;
			row.setAttribute("level",rl.join("_"));
		}
		else if(clickRow && clickRow.Element.getAttribute("level"))
		{
			var rl=clickRow.Element.getAttribute("level").split("_");
			rl[rl.length]=0;
			row.setAttribute("level",rl.join("_"));
		}
		var parRow=table.parentNode.parentNode.previousSibling;
		if(parRow && parRow.childNodes[0].childNodes.length>0 && parRow.childNodes[0].childNodes[0].tagName=="IMG" && parRow.childNodes[0].childNodes[0].style.display=="none")
			parRow.childNodes[0].childNodes[0].style.display="";
		g.recordChange("AddedRows",rowObj);
		for(var i=0;i<rowObj.Band.Columns.length;i++)
		{
			var cellObj=rowObj.getCell(i);
			cellObj.setValue(cellObj.Column.getValueFromString(cellObj.Column.DefaultValue));
		}
		if(bandNo>0 && table.parentNode.parentNode.style.display=="none")
			igtbl_toggleRow(gn,table.parentNode.parentNode.previousSibling.id,table.parentNode.parentNode.id);
		igtbl_setActiveRow(gn,row);
		igtbl_setNewRowImg(gn,row);
		g._calculateStationaryHeader();
		if(g.UseFixedHeaders)
			g.alignDivs();		
		igtbl_scrollToView(gn,row);
		igtbl_fireEvent(gn,g.Events.InitializeRow,"(\""+gn+"\",\""+row.id+"\");");
		igtbl_fireEvent(gn,g.Events.AfterRowInsert,"(\""+gn+"\",\""+row.id+"\");");
	}
	if(g.NeedPostBack)
		igtbl_doPostBack(gn,"");
	if(g._recalcRowNumbers)g._recalcRowNumbers(rowObj);	
	return rowObj;
}

function igtbl_addNewMouseOut(evnt,gn) 
{
	var se=igtbl_srcElement(evnt);
	if(se.tagName == "TD" && se.getAttribute("addNew"))
		igtbl_changeStyle(gn,se,null);
}

function igtbl_updateAddNewStatus()
{
}

function igtbl_updateAddNewBox(gn)
{
	var grid=igtbl_getGridById(gn);
	if(!grid.AddNewBoxVisible)
		return;
	var curBandNo=-1;
	var expandable=false;
	var curRow=null;
	var curRowObj=null;
	if(grid.ActiveCell!="")
	{
		var cell=grid.getActiveCell();
		curRowObj=cell.Row;
		curRow=curRowObj.Element;
		curBandNo=curRowObj.Band.Index;
		if(curRowObj.Expandable && curRowObj.HiddenElement)
			expandable=true;
	}
	else if(grid.ActiveRow!="")
	{
		curRowObj=grid.getActiveRow();
		curRow=curRowObj.Element;
		curBandNo=curRowObj.Band.Index;
		if(curRowObj.Expandable && curRowObj.HiddenElement)
			expandable=true;
	}
	else
	{
		curRowObj=grid.Rows.getRow(0);
		if(curRowObj)
			curRow=curRowObj.Element;
	}
	for(var i=0;i<grid.Bands.length;i++)
	{
		if(grid.Bands[i].AllowAddNew==2 || grid.Bands[i].AllowAddNew==0 && grid.AllowAddNew!=1 || grid.Bands[i].IsGrouped)
			igtbl_setDisabled(grid.Bands[i].addNewElem,true);
		else if(curBandNo==-1 && i==0 || i<=curBandNo || i==curBandNo+1 && !grid.Bands[i].IsGrouped && !(grid.Bands[curBandNo].IsGrouped && curRowObj.GroupByRow) && (grid.Bands[curBandNo].getExpandable()==1 || curRowObj.getExpanded()) && (!curRow.getAttribute("showExpand") || curRowObj.HiddenElement))
		{
			if(i==curBandNo+1)
			{
				if(expandable)
 					grid.Bands[i].curTable=curRow.nextSibling.childNodes[grid.Bands[i-1].firstActiveCell].childNodes[0];
 				else if(i==0)
 					grid.Bands[i].curTable=grid.Element;
 				else
 					grid.Bands[i].curTable=null;
			}
			else
			{
				var cr=curRowObj;
				for(var j=curBandNo;j>=i;j--)
				{
					grid.Bands[j].curTable=cr?cr.Element.parentNode.parentNode:null;					
					if(cr)
					do
					{						
						cr=cr.ParentRow;
					}while(cr && cr.GroupByRow)
				}
			}
			igtbl_setDisabled(grid.Bands[i].addNewElem,false);
		}
		else
			igtbl_setDisabled(grid.Bands[i].addNewElem,true);
	}
}

function igtbl_cancelNoMoreRows(gn)
{
	var g=igtbl_getGridById(gn);
	if(!g) return;
	g.noMoreRows=false;
}
