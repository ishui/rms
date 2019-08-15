/* 
Infragistics UltraWebGrid Script 
Version 5.1.20051.37
Copyright (c) 2001-2005 Infragistics, Inc. All Rights Reserved.
*/

var igtbl_reqType=new Object();
igtbl_reqType.None=0;
igtbl_reqType.ChildRows=1;
igtbl_reqType.MoreRows=2;
igtbl_reqType.Sort=3;
igtbl_reqType.UpdateCell=4;
igtbl_reqType.AddNewRow=5;
igtbl_reqType.DeleteRow=6;
igtbl_reqType.UpdateRow=7;
igtbl_reqType.Custom=8;

var igtbl_readyState=new Object();
igtbl_readyState.Ready=0;
igtbl_readyState.Loading=1;

var igtbl_error=new Object();
igtbl_error.Ok=0;
igtbl_error.LoadFailed=1;

/* General object. Where it all starts. */
function igtbl_Object(type)
{
	if(arguments.length>0)
		this.init(type);
}
igtbl_Object.prototype.init=function(type)
{
	this.Type=type;
}

/* Web object. The one with an HTML element attached. */
igtbl_WebObject.prototype=new igtbl_Object();
igtbl_WebObject.prototype.constructor=igtbl_WebObject;
igtbl_WebObject.base=igtbl_Object.prototype;
function igtbl_WebObject(type,element,node)
{
	if(arguments.length>0)
		this.init(type,element,node);
}
igtbl_WebObject.prototype.init=function(type,element,node,viewState)
{
	igtbl_WebObject.base.init.apply(this,[type]);
	if(element)
	{
		this.Id=element.id;
		this.Element=element;
	}
	if(node)
		this.Node=node;
	if(viewState)
		this.ViewState=viewState;
}
igtbl_WebObject.prototype.get=function(name)
{
	if(this.Node)
		return this.Node.getAttribute(name);
	if(this.Element)
		return this.Element.getAttribute(name);
	return null;
}
igtbl_WebObject.prototype.set=function(name,value)
{
	if(this.Node)
		this.Node.setAttribute(name,value);
	else if(this.Element)
		this.Element.setAttribute(name,value);
	if(this.ViewState)
		ig_ClientState.setPropertyValue(this.ViewState,name,value);
}

/* Grid object */
igtbl_Grid.prototype=new igtbl_WebObject();
igtbl_Grid.prototype.constructor=igtbl_Grid;
igtbl_Grid.base=igtbl_WebObject.prototype;
function igtbl_Grid(element,node)
{
	if(arguments.length>0)
		this.init(element,node);
}
var igtbl_ptsGrid=[
"init",
function(element,node)
{
	igtbl_Grid.base.init.apply(this,["grid",element,node]);
	if(node)
	{
		this.XmlNS="http://schemas.infragistics.com/WebGrid";
		this.Xml=node;
		this.Node=this.Xml.selectSingleNode("UltraWebGrid/Header/UltraGridLayout");
	}
	this.ViewState=ig_ClientState.addNode(ig_ClientState.createRootNode(),"UltraWebGrid");
	this.ViewState=ig_ClientState.addNode(this.ViewState,"DisplayLayout");
	this.StateChanges=ig_ClientState.addNode(this.ViewState,"StateChanges");

	this.Id=this.Id.substr(2);

/* Initialize properties */

	this.Changes=new Array();
	
	this.SelectedRows=[];
	this.SelectedColumns=[];
	this.SelectedCells=[];
	this.SelectedCellsRows=[];
	this.ExpandedRows=[];
	this.CollapsedRows=[];
	this.ResizedColumns=[];
	this.ResizedRows=[];
	this.ChangedRows=[];
	this.ChangedCells=[];
	this.AddedRows=[];
	this.DeletedRows=[];

/*** OBSOLETE ***/
	this.ActiveCell="";
	this.ActiveRow="";
	this.grid=this;
	this.activeRect=null;
	this.SuspendUpdates=false;
/*** END OBSOLETE ***/
	
	this.lastSelectedRow="";
	this.ScrollPos=0;
	this.currentTriImg=null;
	this.newImg=null;
	
	this.NeedPostBack=false;
	this.CancelPostBack=false;
	this.GridIsLoaded=false;
	
	this.exitEditCancel=false;
	this.noCellChange=false;
	this.insideSetActive=false;
	this.CaseSensitiveSort=false;

	var defaultProps=new Array("AddNewBoxVisible","AddNewBoxView","AllowAddNew","AllowColSizing","AllowDelete","AllowSort",
					"ItemClass","AltClass","AllowUpdate","CellClickAction","EditCellClass","Expandable","FooterClass",
					"GroupByRowClass","GroupCount","HeaderClass","HeaderClickAction","Indentation","NullText",
					"ExpAreaClass","RowLabelClass","SelGroupByRowClass","SelHeadClass","SelCellClass","RowSizing",
					"SelectTypeCell","SelectTypeColumn","SelectTypeRow","ShowBandLabels","ViewType","AllowPaging",
					"PageCount","CurrentPageIndex","PageSize","CollapseImage","ExpandImage","CurrentRowImage",
					"CurrentEditRowImage","NewRowImage","BlankImage","SortAscImg","SortDscImg","Activation",
					"cultureInfo","RowSelectors","UniqueID","StationaryMargins","LoadOnDemand","RowLabelBlankImage",
					"EIRM","TabDirection","ClientID","DefaultCentury","UseFixedHeaders","FixedHeaderIndicator",
					"FixedHeaderOnImage","FixedHeaderOffImage","FixedColumnScrollType","AllowRowNumbering","ClientSideRenumbering");
	this.Bands=new Array();
	var props;
	try{props=eval("igtbl_"+this.Id+"_GridProps");}catch(e){}
	if(props)
	{
		for(var i=0;i<defaultProps.length;i++)
			this[defaultProps[i]]=props[i];
		this.Activation=new igtbl_initActivation(this.Activation);
		this.cultureInfo=this.cultureInfo.split("|");
	}
	if(this.UseFixedHeaders)
		this.scrElem=this.Element.parentNode.previousSibling;
	var xmlProps=eval("igtbl_"+this.Id+"_XmlGridProps");
	this.AddnlProps=xmlProps;
	this.RowsServerLength=xmlProps[0];
	this.RowsRange=xmlProps[1];
	this.RowsRetrieved=xmlProps[2];
	if(!node)
	{
		var bandsArray=eval("igtbl_"+this.Id+"_Bands");
		var bandCount=bandsArray.length;
		for(var i=0;i<bandCount;i++) 
			this.Bands[i]=new igtbl_Band(this,null,i);
	}
	else
	{
		this.Bands.Node=this.Node.selectSingleNode("Bands");
		var bandNodes=this.Bands.Node.selectNodes("Band");
		for(var i=0;i<bandNodes.length;i++)
			this.Bands[i]=new igtbl_Band(this,bandNodes[i],i);
	}
	igtbl_dispose(defaultProps);

	igtbl_gridState[this.Id]=this;
	
	if(!this.Bands[0].IsGrouped)
	{
		if(this.Bands[0].ColHeadersVisible!=2 && (this.StationaryMargins==1 || this.StationaryMargins==3))
			this.StatHeader=new igtbl_initStatHeader(this);
		if(this.Bands[0].ColFootersVisible==1 && (this.StationaryMargins==2 || this.StationaryMargins==3))
			this.StatFooter=new igtbl_initStatFooter(this);
	}
	this.Events=new igtbl_Events(this);
	this.Rows=new igtbl_Rows((this.Node?this.Xml.selectSingleNode("UltraWebGrid/Body/Rows"):null),this.Bands[0],null);
	this.regExp=null;
	this.backwardSearch=false;
	this.lastSearchedCell=null;
    this.lastSortedColumn="";
    if(this.AllowRowNumbering==2)this.CurrentRowNumber=0;
	this.GroupByBox=new igtbl_initGroupByBox(this);
	this.DivElement=igtbl_getElementById(this.Id+"_div");
	this.eReqType=igtbl_reqType;
	this.eReadyState=igtbl_readyState;
	this.eError=igtbl_error;
	if(this.Node || !ig_csom.IsIE && this.LoadOnDemand==3)
	{		
		this.ReqType=this.eReqType.None;
		this.ReadyState=this.eReadyState.Ready;
		this.Error=this.eError.Ok;

		this.innerObj=document.createElement("div");

    	this.CallBack=xmlProps[12];

		this.QueryString="";
		if(ig_csom.IsIE)
		{
			this.Url=document.URLUnencoded;
			this.Xslt=new ActiveXObject("Msxml2.FreeThreadedDOMDocument");
			this.Xslt.async=false;
			this.Xslt.load(this.AddnlProps[11]);
			
    		if(!this.CallBack)
			    this.XmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
			this.XmlResp=new ActiveXObject("Microsoft.XMLDOM");
			this.XslTemplate=new ActiveXObject("Msxml2.XSLTemplate");
			this.XslTemplate.stylesheet=this.Xslt;
			this.XslProcessor=this.XslTemplate.createProcessor();
		}
		else
		{
			this.Url=document.URL;
			this.Xslt=new XSLTProcessor();
			this.XmlHttp=new XMLHttpRequest();
			this.XmlResp=new DOMParser();
			this.XmlHttp.open("GET",this.AddnlProps[11],false);
			this.XmlHttp.send(null);
			this.Xslt.importStylesheet(this.XmlResp.parseFromString(this.XmlHttp.responseText,"text/xml"));
		}

		if(node)
			this.Rows.render();
	}
	this._calculateStationaryHeader();
	var thisForm=this.Element.parentNode;
	while(thisForm && thisForm.tagName!="FORM")
		thisForm=thisForm.parentNode;
	if(thisForm)
	{
		this.thisForm=thisForm;
		if(thisForm.igtblGrid)
			this.oldIgtblGrid=thisForm.igtblGrid;
		else
		{
			if(thisForm.addEventListener)
				thisForm.addEventListener('submit',igtbl_submit,false);
			else if(thisForm.onsubmit!=igtbl_submit)
			{
				thisForm.oldOnSubmit=thisForm.onsubmit;
				thisForm.onsubmit=igtbl_submit;
			}
			if(thisForm.submit!=igtbl_formSubmit)
			{
				thisForm.oldSubmit=thisForm.submit;
				thisForm.submit=igtbl_formSubmit;
			}
			window.__doPostBackOld=window.__doPostBack;
			window.__doPostBack=igtbl_submit;
			window.__thisForm=thisForm;
		}
		thisForm.igtblGrid=this;
	}
},
"sortColumn",
function(colId,shiftKey)
{
	var bandNo=igtbl_bandNoFromColId(colId);
	var band=this.Bands[bandNo];
	var colNo=igtbl_colNoFromColId(colId);
	if(band.Columns[colNo].SortIndicator==3)
		return;
	var headClk=igtbl_getHeaderClickAction(this.Id,bandNo,colNo);
	if(headClk==2 || headClk==3)
	{
		var gs=igtbl_getGridById(this.Id);
		if(!band.ClientSortEnabled)
			gs.NeedPostBack=true;
		var eventCanceled=igtbl_fireEvent(this.Id,this.Events.BeforeSortColumn,"(\""+this.Id+"\",\""+colId+"\")");
		if(eventCanceled && band.ClientSortEnabled)
			return;
		if(!eventCanceled)
			this.addSortColumn(colId,(headClk==2 || !shiftKey));
		else
			gs.NeedPostBack=false;
		if(!eventCanceled && band.ClientSortEnabled)
		{
			var el=igtbl_getDocumentElement(colId);
			if(!el.length && el.tagName!="TH")
				igtbl_sortGroupedRows(this.Rows,bandNo,colId);
			else
			{
				if(!el.length)
				{
					el=new Array();
					el[0]=igtbl_getElementById(colId);
				}
				for(var i=0;i<el.length;i++)
				{
					var rows=el[i].parentNode;
					while(rows && rows.tagName!="TABLE") rows=rows.parentNode;
					if(rows && rows.tBodies[0]) rows=rows.tBodies[0];
					if(!rows || !rows.Object) continue;
					rows.Object.sort();
				}
			}
			igtbl_hideEdit(this.Id);
			igtbl_fireEvent(this.Id,this.Events.AfterSortColumn,"(\""+this.Id+"\",\""+colId+"\")");
		}
	}
},
"addSortColumn",
function(colId,clear)
{
	var colAr=colId.split(";");
	if(colAr.length>1)
	{
		for(var i=0;i<colAr.length;i++)
			if(colAr[i]!="")
			{
				var band=this.Bands[igtbl_bandNoFromColId(colAr[i])];
				band.SortedColumns[band.SortedColumns.length]=colAr[i];
			}
	}
	else
	{
		var band=this.Bands[igtbl_bandNoFromColId(colId)];
		var colNo=igtbl_colNoFromColId(colId);
		if(band.Columns[colNo].SortIndicator==3)
			return;
		if(clear)
		{
			var scLen=band.SortedColumns.length;
			for(var i=scLen-1;i>=0;i--)
			{
				var cn=igtbl_colNoFromColId(band.SortedColumns[i]);
				if(cn!=colNo && band.Columns[cn].SortIndicator!=3 && !band.Columns[cn].IsGroupBy)
				{
					band.Columns[cn].SortIndicator=0;
					if(band.ClientSortEnabled)
					{
						var colEl=igtbl_getDocumentElement(band.SortedColumns[i]);
						if(!colEl.length)
							colEl=[colEl];
						for(var j=0;j<colEl.length;j++)
						{
							var img=null;
							var el=colEl[j];
							if(this.UseFixedHeaders && !band.Columns[cn].getFixed() && band.Columns[cn].hasCells())
								el=el.firstChild.firstChild;
							if(el.firstChild && el.firstChild.tagName=="NOBR")
								el=el.firstChild;
							if(el.childNodes.length && el.childNodes[el.childNodes.length-1].tagName=="IMG" && el.childNodes[el.childNodes.length-1].getAttribute("imgType")=="sort")
								img=el.childNodes[el.childNodes.length-1];
							if(img)
								el.removeChild(img);
						}
					}
				}
				if(band.Columns[cn].IsGroupBy)
					break;
				band.SortedColumns=band.SortedColumns.slice(0,-1);
				this.removeChange("SortedColumns",band.Columns[cn]);
			}
		}
		if(band.Columns[colNo].SortIndicator==1)
			band.Columns[colNo].SortIndicator=2;
		else
			band.Columns[colNo].SortIndicator=1;
		this.recordChange("SortedColumns",band.Columns[colNo],clear.toString()+":"+band.Columns[colNo].SortIndicator);
		band.Grid.lastSortedColumn=colId;
		if(band.ClientSortEnabled)
		{
			var colEl=igtbl_getDocumentElement(colId);
			if(!colEl.length)
				colEl=[colEl];
			for(var i=0;i<colEl.length;i++)
			{
				var img=null;
				var el=colEl[i];
				if(this.UseFixedHeaders && !band.Columns[colNo].getFixed() && band.Columns[colNo].hasCells())
					el=el.firstChild.firstChild;
				if(el.firstChild && el.firstChild.tagName=="NOBR")
					el=el.firstChild;
				if(el.childNodes.length && el.childNodes[el.childNodes.length-1].tagName=="IMG" && el.childNodes[el.childNodes.length-1].getAttribute("imgType")=="sort")
					img=el.childNodes[el.childNodes.length-1];
				else
				{
					img=document.createElement("img");
					img.border="0";
					img.setAttribute("imgType","sort");
					el.appendChild(img);
				}
				if(band.Columns[colNo].SortIndicator==1)
					img.src=this.SortAscImg;
				else
					img.src=this.SortDscImg;
			}
		}
		if(!band.Columns[colNo].IsGroupBy)
		{
			for(var i=0;i<band.SortedColumns.length;i++)
				if(band.SortedColumns[i]==colId)
					break;
			if(i==band.SortedColumns.length)
			{
				band.Columns[colNo].ensureWebCombo();
				band.SortedColumns[band.SortedColumns.length]=colId;
			}
		}
	}
},
"getActiveCell",
function()
{
	return this.oActiveCell;
},
"setActiveCell",
function(cell,force)
{
	if(!this.Activation.AllowActivation || this.insideSetActive)
		return;
	if(!cell || !cell.Element || cell.Element.tagName!="TD")
		cell=null;
	if(!force && (cell && this.oActiveCell==cell || this.exitEditCancel))
	{
		this.noCellChange=true;
		return;
	}
	if(!cell)
	{
		this.ActiveCell="";
		this.ActiveRow="";
		var row=null;
		if(this.oActiveCell)
			row=this.oActiveCell.Row;
		else if(this.oActiveRow)
			row=this.oActiveRow;
		if(row)
			row.setSelectedRowImg(true);
		if(this.oActiveCell)
			this.oActiveCell.renderActive(false);
		if(this.oActiveRow)
			this.oActiveRow.renderActive(false);
		this.oActiveCell=null;
		this.oActiveRow=null;
		if(this.AddNewBoxVisible)
			this.updateAddNewBox();
		return;
	}
	var change=true;
	var oldACell=this.oActiveCell;
	var oldARow=this.oActiveRow;
	if(!oldARow && oldACell)
		oldARow=oldACell.Row;
	this.endEdit();
	
	if(this.exitEditCancel || this.fireEvent(this.Events.BeforeCellChange,[this.Id,cell.Element.id])==true)
		change=false;
	if(change && cell.Row!=oldARow)
	{
		if(oldARow)
			oldARow.processUpdateRow();
		if(this.exitEditCancel || this.fireEvent(this.Events.BeforeRowActivate,[this.Id,cell.Row.Element.id])==true)
			change=false;
	}
	if(!change)
	{
		this.noCellChange=true;
		return;
	}
	this.noCellChange=false;
	if(this.oActiveCell)
		this.oActiveCell.renderActive(false);
	if(this.oActiveRow)
		this.oActiveRow.renderActive(false);
	this.oActiveCell=cell;
	this.ActiveCell=cell.Element.id;
	this.oActiveRow=null;
	this.ActiveRow="";
	this.oActiveCell.renderActive();
	if(this.oActiveCell.Row!=oldARow)
		this.setNewRowImg(null);
	this.oActiveCell.Row.setSelectedRowImg();
	this.colButtonMouseOut();
	if(this.AddNewBoxVisible)
		this.updateAddNewBox();
	igtbl_activate(this.Id);
	this.fireEvent(this.Events.CellChange,[this.Id,this.oActiveCell.Element.id]);
	if(this.oActiveCell.Row!=oldARow)
		this.fireEvent(this.Events.AfterRowActivate,[this.Id,this.oActiveCell.Row.Element.id]);
},
"getActiveRow",
function()
{
	if(this.oActiveRow!=null)
		return this.oActiveRow;
	if(this.oActiveCell!=null)
		return this.oActiveCell.Row;
	return null;
},
"setActiveRow",
function(row,force,fireEvents)
{
	if(!this.Activation.AllowActivation || this.insideSetActive)
		return;
	if(typeof(fireEvents)=="undefined")
		fireEvents=true;
	if(!row || !row.Element || row.Element.tagName!="TR")
		row=null;
	if(!force && (row && this.oActiveRow==row || this.exitEditCancel))
	{
		this.noCellChange=true;
		return;
	}
	if(!row)
	{
		this.ActiveCell="";
		this.ActiveRow="";
		var row=null;
		if(this.oActiveCell)
			row=this.oActiveCell.Row;
		else if(this.oActiveRow)
			row=this.oActiveRow;
		if(row)
			row.setSelectedRowImg(true);
		if(this.oActiveCell)
			this.oActiveCell.renderActive(false);
		if(this.oActiveRow)
			this.oActiveRow.renderActive(false);
		this.oActiveCell=null;
		this.oActiveRow=null;
		if(this.AddNewBoxVisible)
			this.updateAddNewBox();
		return;
	}
	var change=true;
	var oldACell=this.oActiveCell;
	var oldARow=this.oActiveRow;
	if(!oldARow && oldACell)
		oldARow=oldACell.Row;
	this.endEdit();

	if(fireEvents && row!=oldARow && oldARow)
		oldARow.processUpdateRow();
	if(this.exitEditCancel || fireEvents && this.fireEvent(this.Events.BeforeRowActivate,[this.Id,row.Element.id])==true)
		change=false;
	if(!change)
	{
		this.noCellChange=true;
		return;
	}
	this.noCellChange=false;
	if(this.oActiveCell)
		this.oActiveCell.renderActive(false);
	if(this.oActiveRow)
		this.oActiveRow.renderActive(false);
	this.oActiveRow=row;
	this.ActiveRow=row.Element.id;
	this.oActiveCell=null;
	this.ActiveCell="";
	this.oActiveRow.renderActive();
	this.oActiveRow.setSelectedRowImg();
	this.colButtonMouseOut();
	if(this.AddNewBoxVisible)
		this.updateAddNewBox();
	igtbl_activate(this.Id);
	if(fireEvents)
		this.fireEvent(this.Events.AfterRowActivate,[this.Id,row.Element.id]);
},
"deleteSelectedRows",
function()
{
	igtbl_deleteSelRows(this.Id);
	igtbl_activate(this.Id);
	this._recalcRowNumbers();	
},
"unloadGrid",
function()
{
	if(this.Id)
		igtbl_unloadGrid(this.Id);
},
"beginEditTemplate",
function()
{
	var row=this.getActiveRow();
	if(row)
		row.editRow();
},
"endEditTemplate",
function(saveChanges)
{
	var row=this.getActiveRow();
	if(row)
		row.endEditRow(saveChanges);
},
"find",
function(re,back)
{
	var g=this;
	if(re)
		g.regExp=re;
	if(!g.regExp)
		return null;
	g.lastSearchedCell=null;
	if(back==true || back==false)
		g.backwardSearch=back;
	var row=null;
	if(!g.backwardSearch)
	{
		row=g.Rows.getRow(0);
		if(row && row.getHidden())
			row=row.getNextRow();
		while(row && row.find()==null)
			row=row.getNextTabRow(false,true);
	}
	else
	{
		var rows=g.Rows;
		while(rows)
		{
			row=rows.getRow(rows.length-1);
			if(row && row.getHidden())
				row=row.getPrevRow();
			if(row && row.Expandable)
				rows=row.Rows;
			else
			{
				if(!row)
					row=rows.ParentRow;
				rows=null;
			}
		}
		while(row && row.find()==null)
			row=row.getNextTabRow(true,true);
	}
	return g.lastSearchedCell;
},
"findNext",
function(re,back)
{
	var g=this;
	if(!g.lastSearchedCell)
		return this.find(re,back);
	if(re)
		g.regExp=re;
	if(!g.regExp)
		return null;
	if(back==true || back==false)
		g.backwardSearch=back;
	var row=g.lastSearchedCell.Row;
	while(row && row.findNext()==null)
		row=row.getNextTabRow(g.backwardSearch,true);
	return g.lastSearchedCell;
},
"alignStatMargins",
function()
{
	if(this.StatHeader)
		this.StatHeader.ScrollTo(this.Element.parentNode.scrollLeft);
	if(this.StatFooter)
		this.StatFooter.ScrollTo(this.Element.parentNode.scrollLeft);
},
"selectCellRegion",
function(startCell,endCell)
{
	var sCol=startCell.Column,eCol=endCell.Column;
	if(sCol.Index>eCol.Index)
	{
		var c=sCol;
		sCol=eCol;
		eCol=c;
	}
	var sRow=startCell.Row,sRowIndex=sRow.getIndex(),eRow=endCell.Row,eRowIndex=eRow.getIndex();
	if(sRowIndex>eRowIndex)
	{
		var c=sRow;
		sRow=eRow;
		eRow=c;
		var i=sRowIndex;
		sRowIndex=eRowIndex;
		eRowIndex=i;
	}
	var pc=sRow.OwnerCollection;
	var band=sCol.Band;
	var selArray=new Array();
	for(var i=sRowIndex;i<=eRowIndex;i++)
	{
		var row=pc.getRow(i);
		if(!row.getHidden())
			for(var j=sCol.Index;j<=eCol.Index;j++)
			{
				var col=band.Columns[j];
				if(col.getVisible())
				{
					var cell=row.getCellByColumn(col);
					if(cell)
						selArray[selArray.length]=cell.Element.id;
				}
			}
	}
	if(selArray.length>0)
		igtbl_gSelectArray(this.Id,0,selArray);
	delete selArray;
},
"selectRowRegion",
function(startRow,endRow)
{
	var sRowIndex=startRow.getIndex(),eRowIndex=endRow.getIndex();
	if(sRowIndex>eRowIndex)
	{
		var r=startRow;
		startRow=endRow;
		endRow=r;
		var i=sRowIndex;
		sRowIndex=eRowIndex;
		eRowIndex=i;
	}
	var pc=startRow.OwnerCollection;
	var selArray=new Array();
	for(var i=sRowIndex;i<=eRowIndex;i++)
	{
		var row=pc.getRow(i);
		if(!row.getHidden())
			selArray[selArray.length]=row.Element.id;
	}
	if(selArray.length>0)
		igtbl_gSelectArray(this.Id,1,selArray);
	delete selArray;
},
"selectColRegion",
function(startCol,endCol)
{
	if(startCol.Index>endCol.Index)
	{
		var c=startCol;
		startCol=endCol;
		endCol=c;
	}
	var band=startCol.Band;
	var selArray=new Array();
	for(var i=startCol.Index;i<=endCol.Index;i++)
	{
		var col=band.Columns[i];
		if(col.getVisible())
			selArray[selArray.length]=col.Id;
	}
	if(selArray.length>0)
		igtbl_gSelectArray(this.Id,2,selArray);
	delete selArray;
},
"startHourGlass",
function()
{
	if(!igtbl_waitDiv)
	{
		igtbl_waitDiv=document.createElement("div");
		document.body.appendChild(igtbl_waitDiv);
		igtbl_waitDiv.style.zIndex=10000;
		igtbl_waitDiv.style.position="absolute";
		igtbl_waitDiv.style.left=0;
		igtbl_waitDiv.style.top=0;
		igtbl_waitDiv.style.backgroundColor="transparent";
	}
	igtbl_waitDiv.style.display="";
	igtbl_waitDiv.style.width=document.body.clientWidth;
	igtbl_waitDiv.style.height=document.body.clientHeight;
	igtbl_waitDiv.style.cursor="wait";
	igtbl_wndOldCursor=document.body.style.cursor;
	document.body.style.cursor="wait";
},
"stopHourGlass",
function()
{
	if(igtbl_waitDiv)
	{
		igtbl_waitDiv.style.cursor="";
		igtbl_waitDiv.style.display="none";
		document.body.style.cursor=igtbl_wndOldCursor;
	}
},
"clearSelectionAll",
function()
{
	igtbl_clearSelectionAll(this.Id);
},
/*** OBSOLETE ***/
"alignGrid",
function(){},
"suspendUpdates",
function(suspend)
{
	if(suspend==false)
	{
		this.SuspendUpdates=false;
	}
	else
		this.SuspendUpdates=true;
},
/*** END OBSOLETE ***/
"beginEdit",
function()
{
	if(this.activeCell)
		this.activeCell.beginEdit();
},
"endEdit",
function()
{
	igtbl_hideEdit(this.Id);
},
"fireEvent",
function(eventObj,args)
{
	if(!this.GridIsLoaded) return;
	var result=false;
	if(eventObj[0]!="")
		result=eval(eventObj[0]).apply(this,args);
	if(this.GridIsLoaded && result!=true && eventObj[1]==1 && !this.CancelPostBack)
		this.NeedPostBack=true;
	this.CancelPostBack=false;
	return result;
},
"setNewRowImg",
function(row)
{
	var gs=this;
	if(row)
		row.setSelectedRowImg(true);
	if(gs.newImg!=null)
	{
		gs.lastSelectedRow=null;
		var imgObj;
		imgObj=document.createElement("img");
		imgObj.src=gs.BlankImage;
		imgObj.border="0";
		imgObj.setAttribute("imgType","blank");
		gs.newImg.parentNode.appendChild(imgObj);
		gs.newImg.parentNode.removeChild(gs.newImg);
		var oRow = igtbl_getRowById(imgObj.parentNode.parentNode.id);
		if (oRow)gs._recalcRowNumbers(oRow);
		gs.newImg=null;
	}
	if(!row || row.Band.getRowSelectors()==2||row.Band.AllowRowNumbering>1)
		return;
	var imgObj;
	imgObj=document.createElement("img");
	imgObj.src=gs.NewRowImage;
	imgObj.border="0";
	imgObj.setAttribute("imgType","newRow");
	var cell=row.Element.cells[row.Band.firstActiveCell-1];
	cell.innerHTML="";
	cell.appendChild(imgObj);
	gs.newImg=imgObj;
},
"colButtonMouseOut",
function()
{
	igtbl_colButtonMouseOut(this.Id);
},
"sort",
function()
{
	if(igtbl_sortGrid)
		igtbl_sortGrid.apply(this);
},
"updateAddNewBox",
function()
{
	igtbl_updateAddNewBox(this.Id);
},
"update",
function()
{
	var p=igtbl_getElementById(this.Id);
	if(!p) return;
	if(this.oActiveCell)
	{
		this.removeChange("ActiveCell",this.oActiveCell);
		this.recordChange("ActiveCell",this.oActiveCell);
	}
	else if(this.oActiveRow)
	{
		this.removeChange("ActiveRow",this.oActiveRow);
		this.recordChange("ActiveRow",this.oActiveRow);
	}
	if(this.Element.parentNode.scrollLeft)
		ig_ClientState.setPropertyValue(this.ViewState,"ScrollLeft",this.Element.parentNode.scrollLeft.toString());
	if(this.Element.parentNode.scrollTop)
		ig_ClientState.setPropertyValue(this.ViewState,"ScrollTop",this.Element.parentNode.scrollTop.toString());
	p.value=ig_ClientState.getText(this.ViewState.parentNode);
},
"goToPage",
function(page)
{
	if(!this.AllowPaging || this.CurrentPage==page || page<1 || page>this.PageCount)
		return;
	igtbl_doPostBack(this.Id,"Page:"+page.toString());
},
"getRowByLevel",
function(level)
{
	if(typeof(level)=="string")
		level=level.split("_");
	var rows=this.Rows;
	for(var i=0;i<level.length-1;i++)
		rows=rows.getRow(level[i]).Rows;
	return rows.getRow(level[level.length-1]);
},
"xmlHttpRequest",
function(type)
{
	if(this.ReadyState!=0)
		return;
	this.ReqType=type;
	this.ReadyState=this.eReadyState.Loading;
	if(this.CallBack)
	{
		var arg=this.QueryString;
		eval(this.CallBack);
	}
	else
	{
		if(ig_csom.IsIE)
		{
			this.XmlHttp.open("POST", this.Url, false);
			this.XmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			this.XmlHttp.onreadystatechange=new Function("igtbl_onReadyStateChange('"+this.Id+"')");
			this.XmlHttp.send("__EVENTTARGET="+this.UniqueID+"&__EVENTARGUMENT=XmlHttpRequest&"+this.UniqueID+"="+this.QueryString);
		}
		else
		{
			this.XmlHttp.open("POST",this.Url,false);
			this.XmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			this.XmlHttp.igtbl_currentGrid=this.Id;
			this.XmlHttp.addEventListener("load",igtbl_onReadyStateChange,false);
			this.XmlHttp.send("__EVENTTARGET="+this.UniqueID+"&__EVENTARGUMENT=XmlHttpRequest&"+this.UniqueID+"="+this.QueryString);
		}
	}
},
"recordChange",
function(type,obj,value)
{
	new igtbl_StateChange(type,this,obj,value);
	if(typeof(this[type])!="undefined")
	{
		var id=obj.Element?obj.Element.id:obj.Id;
		if(typeof(value)!="undefined")
			this[type][id]=value;
		else
			this[type][id]=true;
	}
},
"removeChange",
function(type,obj)
{
	if(obj.Changes[type])
	{
		obj.Changes[type].remove();
		if(typeof(this[type])!="undefined")
		{
			var id=obj.Element?obj.Element.id:obj.Id;
			delete this[type][id];
		}
	}
},
"alignDivs",
function(scrollLeft,force)
{
	if(!this.UseFixedHeaders) return;
	var divs=this.scrElem;
	var divf=this.Element.parentNode;
	var isInit=false;
	if(!divs.firstChild.style.width)
	{
		divs.firstChild.style.width=this.Element.offsetWidth + (this.GroupCount==1?this.Indentation:0);
		isInit=true;
	}
	divs.firstChild.style.height=this.Element.offsetHeight;
	this.Element.setAttribute("noOnResize",true);
	var mainGrid=igtbl_getElementById(this.Id+"_main");
	if(!mainGrid.style.width)
		divs.style.width=mainGrid.clientWidth;
	if(!mainGrid.style.height)
		divs.style.height=divf.firstChild.offsetHeight;
	var relOffs=false;
	if(ig_csom.IsIE)
	{
		while(mainGrid && mainGrid.tagName!="BODY" && !relOffs)
		{
			relOffs=mainGrid.style.position!="" && mainGrid.style.position!="static";
			if(!relOffs) mainGrid=mainGrid.parentNode;
		}
	}
	divf.style.left=igtbl_getLeftPos(divs,false,relOffs?mainGrid:null);
	divf.style.top=igtbl_getTopPos(divs,false,relOffs?mainGrid:null);
	this.Element.removeAttribute("noOnResize");
	
	divf.style.width=igtbl_clientWidth(divs);
	divf.style.height=igtbl_clientHeight(divs);
	if(divf.firstChild.style.left=="")
		divf.firstChild.style.left=0;
	if(divf.firstChild.style.top=="")
		divf.firstChild.style.top=0;
	if(!scrollLeft)
		scrollLeft=divs.scrollLeft;
	else
	{
		this.UseFixedHeaders=false;
		divs.scrollLeft=scrollLeft;
		this.UseFixedHeaders=true;
	}
	var doHoriz=false;
	if(this._oldScrollLeftAlign!=scrollLeft)
	{
		this._oldScrollLeftAlign=scrollLeft;
		doHoriz=true;
	}
	if(parseInt(divf.firstChild.style.top,10)!=-divs.scrollTop)
	{
		divf.firstChild.style.top=-divs.scrollTop;
		if(this.StatHeader || this.StateFooter)
			doHoriz=true;
	}
	if(doHoriz || force)
	{
		for(var i=0;i<this.Bands.length;i++)
		{
			var cols=this.Bands[i].Columns;
			var fac=this.Bands[i].firstActiveCell;
			var nfColNo=0,realColNo=fac;
			while(nfColNo<cols.length && (cols[nfColNo].getFixed() || !cols[nfColNo].getVisible()))
			{
				if(nfColNo<cols.length && cols[nfColNo].getVisible() && cols[nfColNo].hasCells())
					realColNo++;
				nfColNo++;
			}
			if(nfColNo==cols.length)
				continue;
			igtbl_lineupHeaders(cols[nfColNo].Id,this.Bands[i]);
			if(i==0 && isInit)
			{
				divs.firstChild.style.width=this.Element.offsetWidth + (this.GroupCount==1?this.Indentation:0);
				divs.firstChild.style.height=this.Element.offsetHeight;
			}
			var mColNo=nfColNo,cw=scrollLeft;
			while(mColNo<cols.length)
			{
				var col=cols[mColNo];
				if(col.getVisible() && col.hasCells())
				{
					var w=col.getWidth();
					var c=igtbl_getDocumentElement(col.Id),c1=null;
					if(col.Band.ColFootersVisible==1 && (this.StationaryMargins==2 || this.StationaryMargins==3))
						c1=igtbl_getDocumentElement(col.fId);
					if(w<=cw)
						igtbl_setColVisible([c,c1],realColNo==fac?"":"none",realColNo==fac?1:0,realColNo);
					else if(cw>0 && w>cw)
					{
						if(this.FixedColumnScrollType!=1 && ig_csom.IsIE)
							igtbl_setColVisible([c,c1],"",w-cw,realColNo);
						else
						{
							if(cw>w/2||((parseInt(divf.scrollWidth,10)-parseInt(divf.offsetWidth,10))<w))
								igtbl_setColVisible([c,c1],realColNo==fac?"":"none",realColNo==fac?1:0,realColNo);
							else
								igtbl_setColVisible([c,c1],"",w,realColNo);
						}
					}
					else
						igtbl_setColVisible([c,c1],"",w,realColNo);
					if(cw>0)
					{
						cw-=w;
						if(cw<0) cw=0;
					}
					realColNo++;
				}
				mColNo++;
			}
		}
	}
},
"_recalcRowNumbers",
function(row)
{
	if(this.AllowRowNumbering<2 || this.ClientSideRenumbering!=1) return;
	
	for(var i=0; i<this.Bands.length;i++)
		this.Bands[i]._currentRowNumber=0;
	
	if (!row) 
		igtbl_RecalculateRowNumbers(this.Rows,1,this.Bands[0],this.Rows.Node);
	else
		switch(row.Band.AllowRowNumbering)
		{
			case(2):
			case(4):
				igtbl_RecalculateRowNumbers(this.Rows,1,this.Bands[0],this.Rows.Node);
				break;
			case(3):
				var rc = row.ParentRow?row.ParentRow.Rows:this.Rows;					
				igtbl_RecalculateRowNumbers(rc,1,rc.Band,rc.Node);
				break;
		}
},
"_calculateStationaryHeader",
function()
{

	if (this.StatHeader&&(this.StationaryMargins==1 || this.StationaryMargins==3))
	{
		if (this.Rows && this.Rows.length>0)
		{
			this.StatHeader.Element.parentNode.parentNode.parentNode.parentNode.style.display="";			
			this.DivElement.firstChild.children[1].style.display="none";			
		}
		else
		{
			this.StatHeader.Element.parentNode.parentNode.parentNode.parentNode.style.display="none";			
			this.DivElement.firstChild.children[1].style.display="";			
		}		
	}
},
"invokeXmlHttpRequest",
function(type,object,data)
{
	var g=this;
	if(!g.Node || g.LoadOnDemand!=3) return;
	switch(type)
	{
		case g.eReqType.UpdateCell:
		{
			var cell=object;
			if(g.LoadOnDemand==3 && (typeof(g.Events.AfterRowUpdate)=="undefined" || g.Events.AfterRowUpdate[1]==0 && (g.Events.XmlHTTPResponse[1]==1 || g.Events.AfterCellUpdate[1]==1)))
			{
				g.QueryString="UpdateCell\x01"+cell.Band.Index+"\x02"+cell.Column.Index+"\x02"+cell.Row.getIndex()+"\x02"+cell.Row.DataKey+"\x02"+data+"\x02"+cell.getLevel(true);
				g.xmlHttpRequest(type);
			}
			break;
		}
		case g.eReqType.AddNewRow:
		{
			var rows=object;
			if((typeof(g.Events.AfterRowUpdate)=="undefined" || g.Events.AfterRowUpdate[1]==0 && g.Events.XmlHTTPResponse[1]==1))
			{
				g.QueryString="AddNewRow\x01"+rows.Band.Index+":"+(rows.ParentRow?rows.ParentRow.getIndex()+":"+rows.ParentRow.DataKey:":");
				g.xmlHttpRequest(type);
			}
			break;
		}
		case g.eReqType.Sort:
		{
			var rows=object;
			rows.sortXml();
			break;
		}
		case g.eReqType.ChildRows:
		{
			var row=object;
			row.requestChildRows();
			break;
		}
		case g.eReqType.DeleteRow:
		{
			if(g.LoadOnDemand==3 && (!g.Events.XmlHTTPResponse || g.Events.XmlHTTPResponse[1] || g.Events.AfterRowDeleted[1]))
			{
				var row = object;
				var cellInfo = row._generateUpdateRowSemaphore();
				g.QueryString="DeleteRow\x01"+row.Band.Index+":"+row.getIndex()+":"+row.DataKey+":"+row.getLevel(true)+":"+row.DataKey+":"+g.RowsRetrieved+"\x04"+(cellInfo.length>0?"CellValues\x02"+cellInfo+"\x04":"");
				g.RowToQuery=row;
				g.xmlHttpRequest(type);
			}
			break;
		}
		case g.eReqType.UpdateRow:
		{
			var row=object;
			var cellInfo="";
			if(row._dataChanged&1)
				g.QueryString="AddNewRow\x02"+(row.ParentRow?row.ParentRow.getLevel(true)+":"+row.ParentRow.DataKey:":")+(g.QueryString.length>0?"\x04":"")+g.QueryString;
			else
				cellInfo=row._generateUpdateRowSemaphore();
			g.QueryString="UpdateRow\x01"+row._dataChanged+":"+row.Band.Index+":"+row.getLevel(true)+":"+row.DataKey+":"+g.RowsRetrieved+"\x04"+(cellInfo.length>0?"CellValues\x02"+cellInfo+"\x04":"")+g.QueryString;
			g.RowToQuery=row;
			g.xmlHttpRequest(type);
			break;
		}
		case g.eReqType.MoreRows:
		{
			var de=g.DivElement;
			de.setAttribute("oldST",de.scrollTop.toString());
			if(g.RowsServerLength>g.Rows.length)
			{
				g.QueryString="NeedMoreRows\x01"+g.RowsRetrieved+"\x02"+g.Rows.length.toString();
				var sortOrder="";
				for(var i=0;i<g.Bands[0].SortedColumns.length;i++)
				{
					var col=igtbl_getColumnById(g.Bands[0].SortedColumns[i]);
					sortOrder+=col.Key+(col.SortIndicator==2?" DESC":"")+(i<g.Bands[0].SortedColumns.length-1?",":"");
				}
				if(sortOrder)
					g.QueryString+="\x02"+sortOrder;
				g.xmlHttpRequest(g.eReqType.MoreRows);
				de.setAttribute("noOnScroll","true");
				window.setTimeout("igtbl_cancelNoOnScroll('"+g.Id+"')",100);
			}
			break;
		}
		case g.eReqType.Custom:
		{
			g.QueryString="Custom\x01"+data;
			g.xmlHttpRequest(g.eReqType.Custom);
			break;
		}
	}
}
];
for(var i=0;i<igtbl_ptsGrid.length;i+=2)
	igtbl_Grid.prototype[igtbl_ptsGrid[i]]=igtbl_ptsGrid[i+1];

var igtbl_waitDiv=null;
var igtbl_wndOldCursor="";

/* Band object */
igtbl_Band.prototype=new igtbl_WebObject();
igtbl_Band.prototype.constructor=igtbl_Band;
igtbl_Band.base=igtbl_WebObject.prototype;
function igtbl_Band(grid,node,index)
{
	if(arguments.length>0)
		this.init(grid,node,index);
}
var igtbl_ptsBand=[
"init",
function(grid,node,index)
{
	igtbl_Band.base.init.apply(this,["band",null,node]);

	this.Grid=grid;
	this.Index=index;
	var defaultProps=new Array("Key","AllowAddNew","AllowColSizing","AllowDelete","AllowSort","ItemClass","AltClass","AllowUpdate",
								"CellClickAction","ColHeadersVisible","ColFootersVisible","CollapseImage","CurrentRowImage",
								"CurrentEditRowImage","DefaultRowHeight","EditCellClass","Expandable","ExpandImage",
								"FooterClass","GroupByRowClass","GroupCount","HeaderClass","HeaderClickAction","Visible",
								"IsGrouped","ExpAreaClass","NonSelHeaderClass","RowLabelClass","SelGroupByRowClass","SelHeadClass",
								"SelCellClass","RowSizing","SelectTypeCell","SelectTypeColumn","SelectTypeRow","RowSelectors",
								"NullText","RowTemplate","ExpandEffects","AllowColumnMoving","ClientSortEnabled","Indentation",
								"RowLabelWidth","DataKeyField","FixedHeaderIndicator","AllowRowNumbering");
	this.VisibleColumnsCount=0;
	this.Columns = new Array();
	var bandArray;
	try{bandArray=eval("igtbl_"+grid.Id+"_Bands["+index.toString()+"]");}catch(e){}
	var bandCount=0;
	if(bandArray)
	{
		bandCount=eval("igtbl_"+grid.Id+"_Bands").length;
		for(var i=0;i<bandArray.length;i++)
			this[defaultProps[i]]=bandArray[i];
		if(this.RowTemplate!="")
			this.ExpandEffects=new igtbl_expandEffects(this.ExpandEffects);
	}	
	else
		bandCount=this.Node.parentNode.selectNodes("Band").length;	
	var colsArray=eval("igtbl_"+grid.Id+"_Columns_"+index.toString());
	if(!node)
	{
		for(var i=0;i<colsArray.length;i++)
		{
			this.Columns[i]=new igtbl_Column(null,this,i);
			if(!this.Columns[i].Hidden)
				this.VisibleColumnsCount++;
		}
	}
	else
	{
		this.Columns.Node=this.Node.selectSingleNode("Columns");
		var columNodes=this.Columns.Node.selectNodes("Column");
		var nodeIndex=0;
		for(var i=0;i<columNodes.length;i++)
		{
			this.Columns[i]=new igtbl_Column(columNodes[i],this,i,nodeIndex);
			if(!this.Columns[i].Hidden && this.Columns[i].hasCells())
				this.VisibleColumnsCount++;
			if(!colsArray[i][33])
				nodeIndex++;
		}
	}
	igtbl_dispose(defaultProps);

	if(grid.AddNewBoxVisible)
	{
		if(this.Index==0)
			this.curTable=grid.Element;
		var addNew=igtbl_getElementById(grid.Id+"_addBox");
		if(grid.AddNewBoxView==0)
			this.addNewElem = addNew.childNodes[0].rows[0].cells[1].childNodes[0].rows[this.Index].cells[this.Index];
		else
			this.addNewElem = addNew.childNodes[0].rows[0].cells[1].childNodes[0].rows[0].cells[this.Index*2];
	}
	this.SortedColumns=new Array();

	var rs=this.getRowSelectors();
	if(bandCount==1)
	{
		if(rs==2)
			this.firstActiveCell=0;
		else
			this.firstActiveCell=1;
	}
	else
	{
		if(rs==2)
			this.firstActiveCell=1;
		else
			this.firstActiveCell=2;
	}
},
"getSelectTypeRow",
function()
{
	var res=this.Grid.SelectTypeRow;
	if(this.SelectTypeRow!=0)
		res=this.SelectTypeRow;
	return res;
},
"getSelectTypeCell",
function()
{
	var res=this.Grid.SelectTypeCell;
	if(this.SelectTypeCell!=0)
		res=this.SelectTypeCell;
	return res;
},
"getSelectTypeColumn",
function()
{
	var res=this.Grid.SelectTypeColumn;
	if(this.SelectTypeColumn!=0)
		res=this.SelectTypeColumn;
	return res;
},
"getColumnFromKey",
function(key)
{
	var column=null;
	for(var i=0;i<this.Columns.length;i++)
		if(this.Columns[i].Key==key)
		{
			column=this.Columns[i];
			break;
		}
	return column;
},
"getExpandImage",
function()
{
	var ei=this.Grid.ExpandImage;
	if(this.ExpandImage!="")
		ei=this.ExpandImage;
	return ei;
},
"getCollapseImage",
function()
{
	var ci=this.Grid.CollapseImage;
	if(this.CollapseImage!="")
		ci=this.CollapseImage;
	return ci;
},
"getRowStyleClassName",
function()
{
	if(this.ItemClass!="")
		return this.ItemClass;
	return this.Grid.ItemClass;
},
"getRowAltClassName",
function()
{
	if(this.AltClass!="")
		return this.AltClass;
	return this.Grid.AltClass;
},
"getExpandable",
function()
{
	if(this.Expandable!=0)
		return this.Expandable;
	else return this.Grid.Expandable;
},
"getCellClickAction",
function()
{
	var res=this.Grid.CellClickAction;
	if(this.CellClickAction!=0)
		res=this.CellClickAction;
	return res;
},
"getExpAreaClass",
function()
{
	if(this.ExpAreaClass!="")
		return this.ExpAreaClass;
	return this.Grid.ExpAreaClass;
},
"getRowLabelClass",
function()
{
	if(this.RowLabelClass!="")
		return this.RowLabelClass;
	return this.Grid.RowLabelClass;
},
"getItemClass",
function()
{
	if(this.ItemClass!="")
		return this.ItemClass;
	return this.Grid.ItemClass;
},
"getAltClass",
function()
{
	if(this.AltClass!="")
		return this.AltClass;
	else if(this.Grid.AltClass!="")
		return this.Grid.AltClass;
	else if(this.ItemClass!="")
		return this.ItemClass;
	return this.Grid.ItemClass;
},
"getSelClass",
function()
{
	if(this.SelCellClass!="")
		return this.SelCellClass;
	return this.Grid.SelCellClass;
},
"getFooterClass",
function()
{
	if(this.FooterClass!="")
		return this.FooterClass;
	return this.Grid.FooterClass;
},
"getGroupByRowClass",
function()
{
	if(this.GroupByRowClass!="")
		return this.GroupByRowClass;
	return this.Grid.GroupByRowClass;
},
"addNew",
function()
{
	if(typeof(igtbl_addNew)=="undefined")
		return null;
	return igtbl_addNew(this.Grid.Id,this.Index);
},
"getHeadClass",
function()
{
	if(this.HeaderClass!="")
		return this.HeaderClass;
	return this.Grid.HeaderClass;
},
"getRowSelectors",
function()
{
	var res=this.Grid.RowSelectors;
	if(this.RowSelectors!=0)
		res=this.RowSelectors;
	return res;
},
"removeColumn",
function(index)
{
	if(!this.Node) return;
	var column=this.Columns[index];
	if(!column)
		return;
	var elem=igtbl_getDocumentElement(column.Id),fElem;
	elem=igtbl_getArray(elem);
	if(column.fId)
	{
		fElem=igtbl_getDocumentElement(column.fId);
		fElem=igtbl_getArray(fElem);
	}
	for(var i=elem.length-1;i>=0;i--)
	{
		var cg=elem[i].parentNode.parentNode.previousSibling;
		if(cg)
			cg.removeChild(cg.childNodes[elem[i].cellIndex]);
		elem[i].parentNode.removeChild(elem[i]);
		if(fElem)
			fElem[i].parentNode.removeChild(fElem[i]);
	}
	column.colElem=elem;
	if(fElem)
		column.colFElem=fElem;
	if(column.Node)
		column.Node.parentNode.removeChild(column.Node);
	if(this.Columns.splice)
		this.Columns.splice(index,1);
	else
		this.Columns=this.Columns.slice(0,index).concat(this.Columns.slice(index+1));
	this.reIdColumns();
	return column;
},
"insertColumn",
function(column,index)
{
	if(!this.Node || !column || !column.Node || index<0 || index>this.Columns.length)
		return;
	var eAr,fAr;
	var column1=this.Columns[index];
	while(column1 && (!column1.getVisible() || !column1.hasCells()))
		column1=this.Columns[column1.Index+1];
	if(column1)
	{
		this.Columns.Node.insertBefore(column.Node,this.Columns[index].Node);
		if(this.Columns.splice)
			this.Columns.splice(index,0,column);
		else
			this.Columns=this.Columns.slice(0,index).concat(column,this.Columns.slice(index));
		eAr=igtbl_getDocumentElement(column1.Id);
		if(column1.fId)
			fAr=igtbl_getDocumentElement(column1.fId);
	}
	else
	{
		this.Columns.Node.appendChild(column.Node);
		this.Columns[this.Columns.length]=column;
		var i=0;
		while(!eAr && i<this.Columns.length)
			eAr=igtbl_getDocumentElement(this.Columns[i++].Id);
		if(!eAr)
			return;
		if(this.Columns[i-1].fId)
			fAr=igtbl_getDocumentElement(this.Columns[i-1].fId);
	}
	eAr=igtbl_getArray(eAr);
	fAr=igtbl_getArray(fAr);
	if(column.colElem && eAr.length==column.colElem.length)
		for(var i=0;i<eAr.length;i++)
		{
			var col=document.createElement("COL");
			col.width=column.Width;
			var tr=eAr[i].parentNode;
			var cg=tr.parentNode.previousSibling;
			if(column1)
			{
				if(cg)
					cg.insertBefore(col,cg.childNodes[eAr[i].cellIndex]);
				tr.insertBefore(column.colElem[i],eAr[i]);
			}
			else
			{
				if(cg)
					cg.appendChild(col);
				tr.appendChild(column.colElem[i]);
			}
			if(fAr)
			{
				var tr=fAr[i].parentNode;
				if(column1)
					tr.insertBefore(column.colFElem[i],fAr[i]);
				else
					tr.appendChild(column.colFElem[i]);
			}
		}
	igtbl_dispose(eAr);
	igtbl_dispose(fAr);
	this.reIdColumns();
	return column;
},
"reIdColumns",
function()
{
	if(!this.Node) return;
	var elem=igtbl_getDocumentElement(this.Columns[0].Id),fElem;
	if(this.Columns[0].fId)
		fElem=igtbl_getDocumentElement(this.Columns[0].fId);
	var cn=1;
	while(!elem && cn<this.Columns.length)
	{
		elem=igtbl_getDocumentElement(this.Columns[cn].Id);
		if(this.Columns[cn].fId)
			fElem=igtbl_getDocumentElement(this.Columns[cn].fId);
		cn++;
	}
	if(!elem) return;
	var eAr=elem;
	eAr=igtbl_getArray(eAr);
	fElem=igtbl_getArray(fElem);
	var ri=0;
	for(var i=0;i<this.Columns.length;i++)
	{
		this.Columns[i].Id=this.Grid.Id+"c_"+this.Index.toString()+"_"+i.toString();
		this.Columns[i].Index=i;
		if(this.ColFootersVisible)
			this.Columns[i].fId=this.Grid.Id+"f_"+this.Index.toString()+"_"+i.toString();
		if(!this.Columns[i].IsGroupBy && !(this.Node && this.Columns[i].getHidden()))
		{
			for(var j=0;j<eAr.length;j++)
			{
				var tr=eAr[j].parentNode;
				var c=tr.childNodes[ri+this.firstActiveCell];
				c.id=this.Columns[i].Id;
				c.setAttribute("columnNo",i.toString());
			}
			if(fElem)
				for(var j=0;j<fElem.length;j++)
				{
					var tr=fElem[j].parentNode;
					var c=tr.childNodes[ri+this.firstActiveCell];
					c.id=this.Columns[i].fId;
				}
			ri++;
		}
	}
	igtbl_dispose(eAr);
	igtbl_dispose(fElem);
},
"getSelGroupByRowClass",
function()
{
	if(this.SelGroupByRowClass!="")
		return this.SelGroupByRowClass;
	return this.Grid.SelGroupByRowClass;
},
"getBorderCollapse",
function()
{
	if(this.get("BorderCollapse")=="Separate")
		return "";
	if(this.Grid.get("BorderCollapseDefault")=="Separate")
		return "";
	return "collapse";
}
];
for(var i=0;i<igtbl_ptsBand.length;i+=2)
	igtbl_Band.prototype[igtbl_ptsBand[i]]=igtbl_ptsBand[i+1];

/* Column object */
igtbl_Column.prototype=new igtbl_WebObject();
igtbl_Column.prototype.constructor=igtbl_Column;
igtbl_Column.base=igtbl_WebObject.prototype;
function igtbl_Column(node,band,index,nodeIndex)
{
	if(arguments.length>0)
		this.init(node,band,index,nodeIndex);
}
var igtbl_ptsColumn=[
"init",
function(node,band,index,nodeIndex)
{
	igtbl_Column.base.init.apply(this,["column",null,node]);

	this.Band=band;
	this.Index=index;
	this.Id=band.Grid.Id+"c_"+band.Index.toString()+"_"+index.toString();
	if(band.ColFootersVisible)
		this.fId=band.Grid.Id+"f_"+band.Index.toString()+"_"+index.toString();
	var defaultProps=new Array("Key","HeaderText","DataType","CellMultiline","Hidden","AllowGroupBy","AllowColResizing","AllowUpdate",
								"Case","FieldLength","CellButtonDisplay","HeaderClickAction","IsGroupBy","MaskDisplay","Selected",
								"SortIndicator","NullText","ButtonClass","SelCellClass","SelHeadClass","ColumnType","ValueListPrompt",
								"ValueList","ValueListClass","EditorControlID","DefaultValue","TemplatedColumn","Validators",
								"CssClass","Style","Width","AllowNull","Wrap","ServerOnly","HeaderClass","ButtonStyle","Fixed","FooterClass",
								"FixedHeaderIndicator","FooterText","HeaderStyle","FooterStyle");
	var columnArray;
	try{columnArray=eval("igtbl_"+band.Grid.Id+"_Columns_"+band.Index.toString()+"["+index.toString()+"]");}catch(e){}
	if(columnArray)
		for(var i=0;i<columnArray.length;i++)
			this[defaultProps[i]]=columnArray[i];
	this.ensureWebCombo();
	if(node)
	{
		this.Node.setAttribute("index",index+1);
		this.Node.setAttribute("cellIndex",nodeIndex+1);
	}
	igtbl_dispose(defaultProps);
	if(this.EditorControlID)
	{
		this.editorControl=igtbl_getElementById(this.EditorControlID);
		if(this.editorControl) this.editorControl=this.editorControl.Object;
	}

	if(this.Validators && this.Validators.length>0 && typeof(Page_Validators)!="undefined")
	{
		for(var i=0;i<this.Validators.length;i++)
		{
			var val=igtbl_getElementById(this.Validators[i]);
			if(val)
				val.enabled=false;
		}
	}
	this.Changes=[];
},
"getAllowUpdate",
function()
{
	var g=this.Band.Grid;
	var res=g.AllowUpdate;
	if(this.Band.AllowUpdate!=0)
		res=this.Band.AllowUpdate;
	if(this.AllowUpdate!=0)
		res=this.AllowUpdate;
	if(this.TemplatedColumn)
		res=2;
	return res;
},
"setAllowUpdate",
function (value)
{	
	this.AllowUpdate=value;
	switch (this.DataType)
	{
		case 11:  // check box column
			igtbl_AdjustCheckboxDisabledState(this, this.Band.Index,this.Band.Grid.Rows,this.getAllowUpdate());
			break;			
	}
},
"getHidden",
function()
{
	return this.Hidden;
},
"setHidden",
function(h)
{
	if(this.Band.Index==0)
	{
		if(this.Band.Grid.StatHeader)
		{
			var el=this.Band.Grid.StatHeader.getElementByColumn(this);
			el.style.display=(h?"none":"");
		}
		if(this.Band.Grid.StatFooter)
		{
			var el=this.Band.Grid.StatFooter.getElementByColumn(this);
			el.style.display=(h?"none":"");
		}
	}
	igtbl_hideColumn(this.Band.Grid.Rows,this,h);
	this.Hidden=h;
	if(this.Band.Index==0)
		this.Band.Grid.alignStatMargins();
	var ac=this.Band.Grid.getActiveCell();
	if(ac && ac.Column==this && h)
		this.Band.Grid.setActiveCell(null);
	else
		this.Band.Grid.alignGrid();
},
"getVisible",
function()
{
	return !this.getHidden() && !this.IsGroupBy;
},
"hasCells",
function()
{
	return !this.ServerOnly && !this.IsGroupBy;
},
"getNullText",
function()
{
	return igtbl_getNullText(this.Band.Grid.Id,this.Band.Index,this.Index);
},
"find",
function(re,back)
{
	var g=this.Band.Grid;
	if(re)
		g.regExp=re;
	if(!g.regExp || this.IsGroupBy)
		return null;
	g.lastSearchedCell=null;
	if(back==true || back==false)
		g.backwardSearch=back;
	var row=null;
	if(!g.backwardSearch)
	{
		row=g.Rows.getRow(0);
		if(row && row.getHidden())
			row=row.getNextRow();
		while(row && (row.Band!=this.Band || row.getCellByColumn(this).getValue(true).search(g.regExp)==-1))
			row=row.getNextTabRow(false,true);
	}
	else
	{
		var rows=g.Rows;
		while(rows)
		{
			row=rows.getRow(rows.length-1);
			if(row && row.getHidden())
				row=row.getPrevRow();
			if(row && row.Expandable)
				rows=row.Rows;
			else
			{
				if(!row)
					row=rows.ParentRow;
				rows=null;
			}
		}
		while(row && (row.Band!=this.Band || row.getCellByColumn(this).getValue(true).search(g.regExp)==-1))
			row=row.getNextTabRow(true,true);
	}
	g.lastSearchedCell=(row?row.getCellByColumn(this):null);
	return g.lastSearchedCell;
},
"findNext",
function(re,back)
{
	var g=this.Band.Grid;
	if(!g.lastSearchedCell || g.lastSearchedCell.Column!=this)
		return this.find(re,back);
	if(re)
		g.regExp=re;
	if(!g.regExp)
		return null;
	if(back==true || back==false)
		g.backwardSearch=back;
	var row=g.lastSearchedCell.Row.getNextTabRow(g.backwardSearch,true);
	while(row && (row.Band!=this.Band || row.getCellByColumn(this).getValue(true).search(g.regExp)==-1))
		row=row.getNextTabRow(g.backwardSearch,true);
	g.lastSearchedCell=(row?row.getCellByColumn(this):null);
	return g.lastSearchedCell;
},
"getFooterText",
function()
{
	var fId=this.Band.Grid.Id+"f_"+this.Band.Index+"_"+this.Index;
	var foot=igtbl_getElementById(fId);
	if(foot)
		return igtbl_getInnerText(foot);
	return "";
},
"setFooterText",
function(value)
{
	var fId=this.Band.Grid.Id+"f_"+this.Band.Index+"_"+this.Index;
	var foot=igtbl_getDocumentElement(fId);
	if(foot)
	{
		if(igtbl_trim(value)=="")
			value="&nbsp;";
		if(foot.length)
		{
			if(foot[0].childNodes.length>0 && foot[0].childNodes[0].tagName=="NOBR")
				value="<nobr>"+value+"</nobr>";
			for(var i=0;i<foot.length;i++)
				foot[i].innerHTML=value;
		}
		else
		{
			if(foot.childNodes.length>0 && foot.childNodes[0].tagName=="NOBR")
				value="<nobr>"+value+"</nobr>";
			foot.innerHTML=value;
		}
	}
},
"getSelClass",
function()
{
	if(this.SelCellClass!="")
		return this.SelCellClass;
	return this.Band.getSelClass();
},
"getHeadClass",
function()
{
	if(this.HeaderClass!="")
		return this.HeaderClass;
	return this.Band.getHeadClass();
},
"getFooterClass",
function()
{
	if(this.FooterClass!="")
		return this.FooterClass;
	return this.Band.getFooterClass();
},
"compareRows",
function(row1,row2)
{
	if(igtbl_columnCompareRows)
		return igtbl_columnCompareRows.apply(this,[row1,row2]);
	return 0;
},
"compareCells",
function(cell1,cell2)
{
	if(igtbl_columnCompareCells)
		return igtbl_columnCompareCells.apply(this,[cell1,cell2]);
	return 0;
},
"move",
function(toIndex)
{
	if(!this.Node) return;
	oldIndex=this.Index;
	this.Band.Grid.recordChange("ColumnMove",this,toIndex);
	this.Band.insertColumn(this.Band.removeColumn(this.Index),toIndex);
	igtbl_swapCells(this.Band.Grid.Rows,this.Band.Index,oldIndex,toIndex);
},
"getLevel",
function(s)
{
	var l=new Array();
	l[0]=this.Band.Index;
	l[1]=this.Index;
	if(s)
	{
		s=l.join("_");
		igtbl_dispose(l);
		delete l;
		return s;
	}
	return l;
},
"getFixed",
function()
{
	return !this.Band.Grid.UseFixedHeaders || this.Fixed;
},
"setFixed",
function(fixed)
{
	this.Fixed=fixed;
},
"getWidth",
function()
{
	if(typeof(this.Width)!="string")
		return this.Width;
	var e=igtbl_getElementById(this.Id);
	if(!e || !e.offsetWidth)
		return parseInt(this.Width,10);
	if(typeof(this.Width)=="string")
	{
		this.Width=e.offsetWidth;
		var gs=this.Band.Grid,gn=gs.Id;
		if(gs.UseFixedHeaders && !this.getFixed())
		{
			var style=igtbl_getStyleSheet("DIV."+gn+"-cdf-"+this.Band.Index+"-"+this.Index);
			if(style)
				style.width=e.offsetWidth;
			style=igtbl_getStyleSheet("DIV."+gn+"-hdf-"+this.Band.Index+"-"+this.Index);
			if(style)
				style.width=e.offsetWidth;
			style=igtbl_getStyleSheet("DIV."+gn+"-fdf-"+this.Band.Index+"-"+this.Index);
			if(style)
				style.width=e.offsetWidth;
		}
	}
	return this.Width;
},
"setWidth",
function(width)
{
	var gs=this.Band.Grid,gn=gs.Id;
	var colObj=igtbl_getElementById(this.Id);
	var fac=this.Band.firstActiveCell;
	var c1w=width;
	if(c1w>0 && !igtbl_fireEvent(gn,gs.Events.BeforeColumnSizeChange,"(\""+gn+"\",\""+colObj.id+"\","+c1w+")"))
	{
		var fixed=(gs.UseFixedHeaders && !this.getFixed());
		if(fixed)
		{
			var style=igtbl_getStyleSheet("DIV."+gs.Id+"-cdf-"+this.Band.Index+"-"+this.Index);
			if(style)
				style.width=c1w;
			style=igtbl_getStyleSheet("DIV."+gs.Id+"-hdf-"+this.Band.Index+"-"+this.Index);
			if(style)
				style.width=c1w;
			style=igtbl_getStyleSheet("DIV."+gn+"-fdf-"+this.Band.Index+"-"+this.Index);
			if(style)
				style.width=c1w;
		}
		var columns=igtbl_getDocumentElement(this.Id);
		if(!columns.length)
			columns=[columns];
		for(var i=0;i<columns.length;i++)
		{
			var cg;
			if(this.Band.Index==0 && !this.Band.IsGrouped && (gs.StationaryMargins==1 || gs.StationaryMargins==3))
				cg=gs.Element.childNodes[0];
			else
				cg=columns[i].parentNode.parentNode.previousSibling;
			var c;
			if(cg)
				c=cg.childNodes[columns[i].cellIndex];
			else
				c=columns[i];
			if(c.width) c.width="";
			if(columns[i].width) columns[i].width="";
			c.style.width=c1w;
			if(fixed)
			{
				var d=c.style.display;
				c.style.display="none";
				c.style.display=d;
			}
		}
		if(gs.UseFixedHeaders)
		{
			var scrw=gs.scrElem.firstChild.offsetWidth+c1w-this.getWidth();
			if(scrw>=0)
				gs.scrElem.firstChild.style.width=scrw;
		}
		this.Width=c1w;
		if(this.Band.Index==0)
		{
			if(gs.StatHeader)
				gs.StatHeader.ScrollTo(gs.Element.parentNode.scrollLeft);
			if(gs.StatFooter)
			{
				gs.StatFooter.Resize(colObj.cellIndex,c1w);
				gs.StatFooter.ScrollTo(gs.Element.parentNode.scrollLeft);
			}
		}
		gs.alignDivs(0,true);
		gs.removeChange("ResizedColumns",this);
		gs.recordChange("ResizedColumns",this,c1w);
		igtbl_fireEvent(gn,gs.Events.AfterColumnSizeChange,"(\""+gn+"\",\""+colObj.id+"\","+c1w+")");
		if(gs.NeedPostBack)
			igtbl_doPostBack(gn);
		return true;
	}
	return false;
},
"ensureWebCombo",
function()
{
	if(typeof(igcmbo_getComboById)!="undefined" && igcmbo_getComboById(this.EditorControlID) && !this.WebComboId)
		this.WebComboId=this.EditorControlID;
},
"getRealIndex",
function(row)
{
	if(this.IsGroupBy)
		return -1;
	var ri=-1;
	var colspan=1;
	var cell=null;
	if(row)
		cell=row.Element.cells[row.Band.firstActiveCell];
	var i=0;
	while(i<this.Index+1 && (!this.Band.Columns[i].hasCells() || this.Node && this.Band.Columns[i].getHidden()))
		i++;
	if(i>this.Index)
		return ri;
	ri=0;
	for(;i<this.Index;i++)
	{
		if(!this.Band.Columns[i].hasCells() || this.Node && this.Band.Columns[i].getHidden())
			continue;
		if(row)
		{
			if(colspan>1)
			{
				colspan--;
				continue;
			}
			var cellSplit;
			cellSplit=cell.id.split("_");
			if(parseInt(cellSplit[cellSplit.length-1],10)>i)
				ri--;
			else
			{
				cell=cell.nextSibling;
				if(cell)
					colspan=cell.colSpan;
			}
		}
		ri++;
	}
	return ri;
},
"getFixedHeaderIndicator",
function()
{
	if(this.FixedHeaderIndicator!=0)
		return this.FixedHeaderIndicator;
	if(this.Band.FixedHeaderIndicator!=0)
		return this.Band.FixedHeaderIndicator;
	return this.Band.Grid.FixedHeaderIndicator;
},
"getValueFromString",
function(value)
{
	if(value==null || typeof(value)=="undefined")
		return null;
	value=value.toString();
	if(this.AllowNull && value==this.getNullText())
		return null;
	return igtbl_valueFromString(value,this.DataType);
}
];
for(var i=0;i<igtbl_ptsColumn.length;i+=2)
	igtbl_Column.prototype[igtbl_ptsColumn[i]]=igtbl_ptsColumn[i+1];

/* Client events object */
igtbl_Events.prototype=new igtbl_WebObject();
igtbl_Events.prototype.constructor=igtbl_Events;
igtbl_Events.base=igtbl_WebObject.prototype;
function igtbl_Events(grid)
{
	if(arguments.length>0)
		this.init(grid);
}
var igtbl_ptsEvents=[
"init",
function(grid)
{
	igtbl_Events.base.init.apply(this,["events",null,grid.Node?grid.Node.selectSingleNode("ClientSideEvents"):null]);

	var defaultProps=new Array("AfterCellUpdate","AfterColumnMove","AfterColumnSizeChange","AfterEnterEditMode","AfterExitEditMode",
								"AfterRowActivate","AfterRowCollapsed","AfterRowDeleted","AfterRowTemplateClose","AfterRowTemplateOpen",
								"AfterRowExpanded","AfterRowInsert","AfterRowSizeChange","AfterSelectChange","AfterSortColumn",
								"BeforeCellChange","BeforeCellUpdate","BeforeColumnMove","BeforeColumnSizeChange","BeforeEnterEditMode",
								"BeforeExitEditMode","BeforeRowActivate","BeforeRowCollapsed","BeforeRowDeleted","BeforeRowTemplateClose",
								"BeforeRowTemplateOpen","BeforeRowExpanded","BeforeRowInsert","BeforeRowSizeChange","BeforeSelectChange",
								"BeforeSortColumn","ClickCellButton","CellChange","CellClick","ColumnDrag","ColumnHeaderClick","DblClick",
								"EditKeyDown","EditKeyUp","InitializeLayout","InitializeRow","KeyDown","KeyUp","MouseDown","MouseOver",
								"MouseOut","MouseUp","RowSelectorClick","TemplateUpdateCells","TemplateUpdateControls","ValueListSelChange",
								// V20043a
								"BeforeRowUpdate","AfterRowUpdate","XmlHTTPResponse");
	var eventsArray;
	try{eventsArray=eval("igtbl_"+grid.Id+"_Events");}catch(e){}
	if(eventsArray)
		for(var i=0;i<eventsArray.length;i++)
			this[defaultProps[i]]=eventsArray[i];
	igtbl_dispose(defaultProps);
}];
for(var i=0;i<igtbl_ptsEvents.length;i+=2)
	igtbl_Events.prototype[igtbl_ptsEvents[i]]=igtbl_ptsEvents[i+1];

/* Rows collection object */
igtbl_Rows.prototype=new igtbl_WebObject();
igtbl_Rows.prototype.constructor=igtbl_Rows;
igtbl_Rows.base=igtbl_WebObject.prototype;
function igtbl_Rows(node,band,parentRow)
{
	if(arguments.length>0)
	{
		var element=null;
		if(band.Index==0 && !parentRow)
			element=band.Grid.Element.tBodies[0];
		else if(parentRow && parentRow.Element)
		{
			if(parentRow.GroupByRow)
			{
				var tb=parentRow.Element.childNodes[0].childNodes[0].tBodies[0];
				if(tb.childNodes.length>1)
					this.Element=tb.childNodes[1].childNodes[0].childNodes[0].tBodies[0];
			}
			else if(parentRow.Element.nextSibling && parentRow.Element.nextSibling.getAttribute("hiddenRow"))
				this.Element=parentRow.Element.nextSibling.childNodes[parentRow.Band.firstActiveCell].childNodes[0].tBodies[0];
		}
		this.init(element,node,band,parentRow);
	}
}
var igtbl_ptsRows=[
"init",
function(element,node,band,parentRow)
{
	igtbl_Rows.base.init.apply(this,["rows",element,node]);
	
	this.Grid=band.Grid;
	this.Band=band;
	this.ParentRow=parentRow;
	this.rows=new Array();
	this.length=0;
	if(node)
	{
		this.SelectedNodes=node.selectNodes("Row");
		if(!this.SelectedNodes.length)
			this.SelectedNodes=node.selectNodes("Group");
		this.length=this.SelectedNodes.length;
	}
	else
	{
		if(parentRow)
			this.length=parentRow.ChildRowsCount;
		else
		{
			this.length=this.Element.childNodes.length;
			for(var i=0;i<this.Element.childNodes.length;i++)
				if(this.Element.childNodes[i].getAttribute("hiddenRow"))
					this.length--;
		}
	}
	if(this.Element)
		this.Element.Object=this;
	this.lastRowId="";
},
"getRow",
function(rowNo,rowElement)
{
	if(rowNo<0 || !this.Element || !this.Element.childNodes)
		return null;
	if(rowNo>=this.length)
	{
		if(this.length>this.rows.length)
			this.rows[this.length-1]=null;
		return null;
	}
	if(rowNo>=this.rows.length)
		this.rows[this.length-1]=null;
	if(!this.rows[rowNo])
	{
		var row=rowElement;
		if(!row)
		{
			var cr=0;
			if(this.Grid.Bands.length==1 && !this.Grid.Bands[0].IsGrouped)
				row=this.Element.childNodes[rowNo];
			else
				for(var i=0;i<this.Element.childNodes.length;i++)
					if(!this.Element.childNodes[i].getAttribute("hiddenRow"))
					{
						if(rowNo==cr)
						{
							row=this.Element.childNodes[i];
							break;
						}
						cr++;
					}
		}
		if(!row)
			return null;
		this.rows[rowNo]=new igtbl_Row(row,(this.Node?this.SelectedNodes[rowNo]:null),this,rowNo);
	}
	return this.rows[rowNo];
},

"getRowById",
function(rowId)
{
	for(var i=0;i<this.length;i++)
	{
		var row=this.getRow(i);
		if(row.Element.id==rowId)
			return row;
	}
	return null;
},
"getColumn",
function(colNo)
{
	var thead=this.Element.previousSibling;
	if(!thead || thead.tagName!="THEAD")
		return;
	var j=-1;
	for(var i=0;i<this.Band.Columns.length;i++)
	{
		if(this.Band.Columns[i].hasCells())
			j++;
		if(i==colNo)
			break;
	}
	if(j<0 || j>=this.Band.Columns.length)
		return null;
	return thead.firstChild.cells[j+this.Band.firstActiveCell];
},
"indexOf",
function(row)
{
	if(row.Node)
		return parseInt(row.Node.getAttribute("i"),10);
	if(this.Grid.Bands.length==1 && !this.Grid.Bands[0].IsGrouped)
		return row.Element.sectionRowIndex;
	var level=-1;
	var rId=row.Element.id,rows=this.Element.rows;
	for(var i=0;i<rows.length;i++)
	{
		var r=rows[i];
		if(!r.getAttribute("hiddenRow"))
			level++;
		else
			continue;
		if(r.id==rId)
			return level;
	}
	return -1;
},
"insert",
function(row,rowNo)
{
	var row1=this.getRow(rowNo);
	if(row1)
	{
		if(this.rows.splice)
			this.rows.splice(rowNo,0,row);
		else
			this.rows=this.rows.slice(0,rowNo).concat(row,this.rows.slice(rowNo));
		this.Element.insertBefore(row.Element,row1.Element);
		if(row.Expandable && row.HiddenElement && !row.GroupByRow)
			this.Element.insertBefore(row.HiddenElement,row1.Element);
		if(this.Node)
			this.Node.insertBefore(row.Node,row1.Node);
	}
	else
	{
		this.rows[this.rows.length]=row;
		this.Element.appendChild(row.Element);
		if(row.Expandable && row.HiddenElement && !row.GroupByRow)
			this.Element.appendChild(row.HiddenElement);
		if(this.Node)
			this.Node.appendChild(row.Node);
	}
	this.length++;
},
"remove",
function(rowNo)
{
	var row=this.getRow(rowNo);
	if(!row)
		return;
	this.Element.removeChild(row.Element);
	if(row.Expandable && row.HiddenElement && !row.GroupByRow)
		this.Element.removeChild(row.HiddenElement);
	if(row.Node)
		row.Node.parentNode.removeChild(row.Node);
	if(this.rows.splice)
		this.rows.splice(rowNo,1);
	else
		this.rows=this.rows.slice(0,rowNo).concat(this.rows.slice(rowNo+1));
	this.length--;
	return row;
},
"sort",
function(sortedCols)
{
	if(igtbl_clctnSort)
		igtbl_clctnSort.apply(this,[sortedCols]);
},
"getFooterText",
function(columnKey)
{
	var tFoot;
	if(this.Band.Index==0 && this.Grid.StatFooter)
		tFoot=this.Grid.StatFooter.Element;
	else
		tFoot=this.Element.nextSibling;
	var col=this.Band.getColumnFromKey(columnKey);
	if(tFoot && tFoot.tagName=="TFOOT" && col)
	{
		var fId=this.Grid.Id+"f_"+this.Band.Index+"_"+col.Index;
		for(var i=0;i<tFoot.rows[0].childNodes.length;i++)
			if(tFoot.rows[0].childNodes[i].id==fId)
				return igtbl_getInnerText(tFoot.rows[0].childNodes[i]);
	}
	return "";
},
"setFooterText",
function(columnKey,value)
{
	var tFoot;
	if(this.Band.Index==0 && this.Grid.StatFooter)
		tFoot=this.Grid.StatFooter.Element;
	else
		tFoot=this.Element.nextSibling;
	var col=this.Band.getColumnFromKey(columnKey);
	if(tFoot && tFoot.tagName=="TFOOT" && col)
	{
		var fId=this.Grid.Id+"f_"+this.Band.Index+"_"+col.Index;
		for(var i=0;i<tFoot.rows[0].childNodes.length;i++)
			if(tFoot.rows[0].childNodes[i].id==fId)
			{
				if(igtbl_trim(value)=="")
					value="&nbsp;";
				if(tFoot.rows[0].childNodes[i].childNodes.length>0 && tFoot.rows[0].childNodes[i].childNodes[0].tagName=="NOBR")
					value="<nobr>"+value+"</nobr>";
				tFoot.rows[0].childNodes[i].innerHTML=value;
				break;
			}
	}
},
"render",
function()
{
	var strTransform=this.applyXslToNode(this.Node);
	if(strTransform)
	{
		this.Grid.innerObj.innerHTML=strTransform;
		var tbl=this.Element.parentNode;
		tbl.replaceChild(this.Grid.innerObj.firstChild.firstChild,this.Element);
		this.Element=tbl.tBodies[0];
		this.Element.Object=this;
		for(var i=0;i<this.Band.Columns.length;i++)
			if(this.Band.Columns[i].Selected && this.Band.Columns[i].hasCells())
			{
				var col=this.getColumn(i);
				if(col)
					igtbl_selColRI(this.Grid.Id,col,this.Band.Index,i);
			}
	}
},
"applyXslToNode",
function(node,rowToStart)
{
	if(!node) return "";
	if(typeof(rowToStart)=="undefined")
		rowToStart=0;
	var xslProc=this.Grid.XslProcessor;

	var oldColumns=node.selectSingleNode("Columns");
	if(oldColumns)
		node.removeChild(oldColumns);
	node.appendChild(this.Band.Columns.Node.cloneNode(true));

	xslProc.input=node;
	xslProc.addParameter("gridName",this.Grid.Id);
	if(!this.SelectedNodes.length || this.SelectedNodes[0].nodeName!="Group")
	{
		var fac=this.Band.firstActiveCell;
		xslProc.addParameter("fac",fac);
		var rs=this.Band.getRowSelectors();
		xslProc.addParameter("rs",rs);
		if(fac>1 || rs==2 && fac==1)
		{
			xslProc.addParameter("expAreaClass",this.Band.getExpAreaClass());
			xslProc.addParameter("expandImage","<img src="+this.Band.getExpandImage()+" border='0' onclick=\"igtbl_toggleRow(event);\">");
		}
		if(fac>0 && rs!=2)
		{
			xslProc.addParameter("rowLabelClass",this.Band.getRowLabelClass());
			xslProc.addParameter("blankImage","<img src='"+this.Grid.BlankImage+"' border=0 imgType='blank' style='visibility:hidden;'>");
		}
		xslProc.addParameter("itemClass",this.Band.getItemClass());
		xslProc.addParameter("altClass",this.Band.getAltClass());
		xslProc.addParameter("selClass",this.Band.getSelClass());
		if(this.Grid.UseFixedHeaders)
			xslProc.addParameter("cellDivScr",this.Grid.Id+"-cds");
	}
	else
	{
		xslProc.addParameter("grpClass",this.Band.getGroupByRowClass());
		xslProc.addParameter("expandImage","<img src="+this.Band.getExpandImage()+" border='0' onclick=\"igtbl_toggleRow(event);\" onmousedown=\"return igtbl_cancelEvent(event);\" onmouseup=\"return igtbl_cancelEvent(event);\">");
	}
	var prL="";
	if(this.ParentRow)
		prL=this.ParentRow.Element.id.split("_").slice(1).join("_")+"_";
	xslProc.addParameter("parentRowLevel",prL);
	xslProc.addParameter("rowHeight",this.Band.DefaultRowHeight);
	xslProc.addParameter("rowToStart",rowToStart);
	xslProc.transform();
	return xslProc.output;
},
"addNew",
function()
{
	var g=this.Grid;
	var doc=g.Xml.XMLDocument;
	var xmlns=g.XmlNS;
	if(igtbl_fireEvent(g.Id,g.Events.BeforeRowInsert,"(\""+g.Id+"\",\""+(this.ParentRow?this.ParentRow.Element.id:"")+"\")")==true)
		return null;
	if(this.ParentRow && !this.ParentRow.getExpanded())
		this.ParentRow.setExpanded(true);
	var toExisting=(typeof(this.Node)!="undefined");
	if(!toExisting)
	{
		this.Node=doc.createNode(1,"Rows",xmlns);
		var pr=this.ParentRow;
		pr.Node.appendChild(this.Node);
		pr.Expandable=true;
		pr.Element.childNodes[0].innerHTML="<img src="+pr.Band.getExpandImage()+" border=0 onclick=\"igtbl_toggleRow(event);\">";
	}
	var rows=doc.createNode(1,"Rows",xmlns);
	g.Node.appendChild(rows);
	var row=doc.createNode(1,"Row",xmlns);
	var lrId=this.getLastRowId();
	if(!lrId)
		row.setAttribute("i",this.length);
	else
	{
		var lr=lrId.split("_");
		row.setAttribute("i",parseInt(lr[lr.length-1],10)+1);
	}
	rows.appendChild(row);
	var cells=doc.createNode(1,"Cells",xmlns);
	row.appendChild(cells);
	for(var i=0;i<this.Band.Columns.length;i++)
	{
		if(this.Band.Columns[i].ServerOnly) continue;
		var cell=doc.createNode(1,"Cell",xmlns);
		cells.appendChild(cell);
		var content=doc.createNode(1,"Content",xmlns);
		cell.appendChild(content);
		var cdata=doc.createNode(4,"",xmlns);
		content.appendChild(cdata);
		var value=doc.createNode(1,"Value",xmlns);
		cell.appendChild(value);
	}
	for(var i=0;i<this.Band.Columns.length;i++)
	{
		if(this.Band.Columns[i].ServerOnly) continue;
		var selCells=cells.selectNodes("Cell");
		var column=this.Band.Columns[i];
		var cellNode=selCells[column.get("index")-1];
		var st=column.Style;
		if(column.CssClass)
			cellNode.setAttribute("class",column.CssClass);
		if(column.Hidden)
			st+="display:none;";
		if(st)
			cellNode.setAttribute("style",st);
		var it_str="";
		if(!column.Wrap)
			it_str+="<nobr>";
		switch(column.ColumnType)
		{
			case 3:
				it_str+="<input type=checkbox"+(column.getAllowUpdate()==1?"":" disabled")+" on"+(ig_csom.IsIE?"property":"")+"change='igtbl_chkBoxChange(event,\""+g.Id+"\");'>";
				break;
			case 7:
				var bc=column.ButtonClass;
				var bs=column.ButtonStyle;
				if(column.CellButtonDisplay==1)
					it_str+="<input type=button style='"+bs+"' onclick=\"igtbl_colButtonClick(event,'"+g.Id+"');\""+(bc==""?"":" class='"+bc+"'")+">";
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
		cellNode.firstChild.firstChild.text=it_str;
	}
	if(toExisting)
	{
		var strTransform="";
		strTransform=this.applyXslToNode(rows,this.length);
		g.Node.removeChild(rows);
		if(strTransform)
		{
			this.Node.appendChild(row);
			this.length++;
			this.SelectedNodes=this.Node.selectNodes("Row");
			g.innerObj.innerHTML=strTransform;
			this.Element.appendChild(g.innerObj.firstChild.rows[0]);
		}
	}
	else
	{
		this.length++;
		this.Node.appendChild(row);
		this.SelectedNodes=this.Node.selectNodes("Row");
		this.ParentRow.setExpanded(true);
	}
	var rowObj=this.getRow(this.length-1);
	this.setLastRowId(rowObj.Id);
	rowObj.Node.setAttribute("i",this.length-1);
	if(g.LoadOnDemand==3)
	{
		rowObj._dataChanged|=1;
		g.invokeXmlHttpRequest(g.eReqType.AddNewRow,this);
	}
	else
		g.recordChange("AddedRows",rowObj);
	for(var i=0;i<rowObj.Band.Columns.length;i++)
	{
		var cellObj=rowObj.getCell(i);
		cellObj.setValue(cellObj.Column.getValueFromString(cellObj.Column.DefaultValue));
	}
	rowObj.activate();
	g.setNewRowImg(rowObj);
	igtbl_fireEvent(g.Id,g.Events.InitializeRow,"(\""+g.Id+"\",\""+rowObj.Element.id+"\");");
	igtbl_fireEvent(g.Id,g.Events.AfterRowInsert,"(\""+g.Id+"\",\""+rowObj.Element.id+"\");");
	if(g.LoadOnDemand==3)
		g.NeedPostBack=false;
	var de=g.DivElement;
	if(g.scrElem)
		de=g.scrElem;
	de.setAttribute("noOnScroll","true");
	window.setTimeout("igtbl_cancelNoOnScroll('"+g.Id+"')",100);
	g.alignDivs();
	rowObj.scrollToView();
	return rowObj;
},
"dispose",
function(self)
{
	for(var i=0;i<this.rows.length;i++)
	{
		if(this.rows[i])
		{
			if(this.rows[i].Rows)
				this.rows[i].Rows.dispose(true);
			if(this.rows[i].cells)
				for(var j=0;j<this.rows[i].cells.length;j++)
				{
					var cell=this.rows[i].cells[j];
					if(cell)
					{
						cell.Column=null;
						cell.Band=null;
						cell.Row=null;
						for(var change in cell.Changes)
						{
							var ch=cell.Changes[change];
							ch.Grid=null;
							ch.Object=null;
						}
						if(cell.Element)
							cell.Element.Object=null;
					}
				}
			if(this.rows[i].Changes)
				for(var change in this.rows[i].Changes)
				{
					var ch=this.rows[i].Changes[change];
					ch.Grid=null;
					ch.Object=null;
				}
			this.rows[i].OwnerCollection=null;
			this.rows[i].Band=null;
			this.rows[i].ParentRow=null;
			this.rows[i].Element.Object=null;
		}
	}
	igtbl_dispose(this.rows);
	delete this.rows;
	if(self)
	{
		this.Grid=null;
		this.Band=null;
		this.ParentRow=null;
		igtbl_dispose(this);
	}
	else
		this.rows=new Array();
},
"reIndex",
function(sRow)
{
	for(var i=sRow;i<this.length;i++)
		this.getRow(i).Node.setAttribute("i",i.toString());
},
"repaint",
function()
{
	var strTransform=this.applyXslToNode(this.Node);
	if(strTransform)
	{
		this.Grid.innerObj.innerHTML=strTransform;
		var tbl=this.Element.parentNode;
		var newEl=this.Grid.innerObj.firstChild.firstChild;
		for(var i=this.rows.length-1;i>=0;i--)
			if(this.rows[i])
			{
				if(this.rows[i].HiddenElement)
				{
					if(i==newEl.rows.length-1)
						newEl.appendChild(this.rows[i].HiddenElement);
					else
						newEl.insertBefore(this.rows[i].HiddenElement,newEl.rows[i+1]);
					var img=newEl.rows[i].firstChild;
					if(this.rows[i].getExpanded() && img)
					{
						img=newEl.rows[i].firstChild.firstChild;
						if(img && img.tagName=="IMG")
							img.src=this.Band.getCollapseImage();
					}
				}
				var row=this.rows[i];
				row.Element=newEl.rows[i];
				row.Element.Object=row;
				for(var j=0;row.cells && j<row.cells.length;j++)
				{
					var cell=row.cells[j];
					if(cell)
					{
						cell.Column=this.Band.Columns[j];
						if(cell.Column.hasCells())
						{	
							cell.Element=row.Element.cells[cell.Column.getRealIndex()+this.Band.firstActiveCell];							
							if(this.Grid.UseFixedHeaders && !cell.Column.getFixed() && cell.Element)
								cell.scrElem=cell.Element.firstChild.firstChild;
							cell.Element.Object=cell;
							cell.Id=cell.Element.id;
							if(cell.getSelected() || row.getSelected())
								cell.selectCell();
						}
						else
							cell.Element=null;
					}
				}
			}
		tbl.replaceChild(newEl,this.Element);
		this.Element=tbl.tBodies[0];
		this.Element.Object=this;
	}
},
"sortXml",
function(sortedCols)
{
	if(this.Band.SortedColumns.length==0)
		return;
	var g=this.Grid;
	var row=this.ParentRow;
	g.QueryString="Sort\x01";
	if(row)
		g.QueryString+=row.getLevel(true);
	var sqlWhere="";
	var sortOrder="";
	for(var i=0;i<=this.Band.Index;i++)
	{
		var cr=row;
		while(cr && cr.Band!=g.Bands[i])
			cr=cr.ParentRow;
		if(g.Bands[i].DataKeyField && cr.get("lit:DataKey"))
			sqlWhere+=cr.Band.DataKeyField+"="+cr.get("lit:DataKey");
		sqlWhere+=(i==this.Band.Index?"":";");
	}
	for(var i=0;i<g.Bands.length;i++)
	{
		var so="";
		for(var j=0;j<g.Bands[i].SortedColumns.length;j++)
		{
			var col=igtbl_getColumnById(g.Bands[i].SortedColumns[j]);
			so+=col.Key+(col.SortIndicator==2?" DESC":"")+(j<g.Bands[i].SortedColumns.length-1?",":"");
		}
		sortOrder+=so+(i==g.Bands.length-1?"":";");
	}
	g.QueryString+="\x02"+sqlWhere;
	g.QueryString+="\x02"+sortOrder;
	g.RowToQuery=this.ParentRow;
	g.xmlHttpRequest(g.eReqType.Sort);
},
"getLastRowId",
function()
{
	if(!this.lastRowId)
		this.setLastRowId();
	return this.lastRowId;
},
"setLastRowId",
function(lrId)
{
	if(arguments.length==0 && !this.lastRowId)
	{
		if(this.length>0)
			this.lastRowId=this.getRow(this.length-1).Element.id;
	}
	else if(lrId)
		this.lastRowId=lrId;
}];
for(var i=0;i<igtbl_ptsRows.length;i+=2)
	igtbl_Rows.prototype[igtbl_ptsRows[i]]=igtbl_ptsRows[i+1];

/* Row object */
igtbl_Row.prototype=new igtbl_WebObject();
igtbl_Row.prototype.constructor=igtbl_Row;
igtbl_Row.base=igtbl_WebObject.prototype;
function igtbl_Row(element,node,rows,index)
{
	if(arguments.length>0)
		this.init(element,node,rows,index);
}
var igtbl_ptsRow=[
"init",
function(element,node,rows,index)
{
	igtbl_Row.base.init.apply(this,["row",element,node]);

	var gs=rows.Band.Grid;
	var gn=gs.Id;
	this.gridId=gs.Id;
	var row=this.Element;
	row.Object=this;
	this.OwnerCollection=rows;
	this.Band=this.OwnerCollection.Band;
	this.GroupByRow=false;
	this.GroupColId=null;
	if(row.getAttribute("groupRow"))
	{
		this.GroupByRow=true;
		this.GroupColId=row.getAttribute("groupRow");
		var sTd=row.childNodes[0].childNodes[0].tBodies[0].childNodes[0].childNodes[0];
		this.MaskedValue=sTd.getAttribute("cellValue");
		this.Value=this.MaskedValue;
		if(sTd.getAttribute("unmaskedValue"))
			this.Value=sTd.getAttribute("unmaskedValue");
		this.Value=igtbl_getColumnById(this.GroupColId).getValueFromString(this.Value);
	}
	var fr=igtbl_getFirstRow(row);
	this.Expandable=((fr.nextSibling && fr.nextSibling.getAttribute("hiddenRow") || this.Element.getAttribute("showExpand")));
	this.ChildRowsCount=0;
	this.VisChildRowsCount=0;
	if(this.Expandable)
	{
		if(fr.nextSibling && fr.nextSibling.getAttribute("hiddenRow"))
		{
			this.HiddenElement=fr.nextSibling;
			if(this.getExpanded())
				gs.ExpandedRows[this.Element.id]=this;
			this.ChildRowsCount=igtbl_rowsCount(igtbl_getChildRows(gn,row));
			this.VisChildRowsCount=igtbl_visRowsCount(igtbl_getChildRows(gn,row));
			this.Rows=new igtbl_Rows((this.Node?this.Node.selectSingleNode("Rows"):null),gs.Bands[rows.Band.Index+(this.GroupByRow?0:1)],this);
/* OBSOLETE*/
			this.FirstChildRow=this.Rows.getRow(0);
/***********/
		}
	}
	this.FirstRow=fr;
	if(this.OwnerCollection)
		this.ParentRow=this.OwnerCollection.ParentRow;

	if(!this.GroupByRow)
		this.cells=new Array(this.Band.Columns.length);
	if(rows.Node)
	{
		if(!this.Expandable)
			this.Expandable=this.Node.selectSingleNode("Rows")!=null || this.Node.getAttribute("lit:showExpand")=="true";
	}
	if(this.Node)
	{
		var dataKey=this.get("lit:DataKey");
		this.DataKey=dataKey?dataKey:"";
	}
	this.Expanded=this.getExpanded();
	this.Changes=[];
	this._dataChanged=0;
},
"getIndex",
function()
{
	if(this.Node)
		return parseInt(this.Node.getAttribute("i"),10);
	else if(this.OwnerCollection)
		return this.OwnerCollection.indexOf(this);
	return -1;
},
"toggleRow",
function()
{
	this.setExpanded(!this.getExpanded());
},
"getExpanded",
function(expand)
{
	return (this.Expandable && this.HiddenElement && this.HiddenElement.style.display=="");
},
"setExpanded",
function(expand)
{
	if(this.Band.getExpandable()!=1 || !this.Expandable)
		return;
	if(expand!=false)
		expand=true;
	if(expand==this.getExpanded()) return;
	var gn=this.gridId;
	var gs=igtbl_getGridById(gn);
	if(gs.LoadOnDemand==3 && !this.HiddenElement)
		this.requestChildRows();
	if(this.Node)
	{
		var rsn=this.Node.selectSingleNode("Rows");
		if(rsn)
		{
			if(!this.Rows)
				this.Rows=new igtbl_Rows(rsn,gs.Bands[this.Band.Index+(this.GroupByRow?0:1)],this);
			if(!this.HiddenElement)
			{
				this.prerenderChildRows();
				this.Rows.render();
			}
		}
		else if(gs.LoadOnDemand!=1 && gs.LoadOnDemand!=2)
			return;
	}
	var srcRow=this.getFirstRow().id;
	var sr=igtbl_getElementById(srcRow);
	var hr=this.HiddenElement;
	var cancel=false;
	if(expand!=false) 
	{
		if(gs.LoadOnDemand==0 && (!this.Rows || !this.Rows.length)) return;
		if(igtbl_fireEvent(gn,gs.Events.BeforeRowExpanded,"(\""+gn+"\",\""+srcRow+"\");")==true)
			cancel=true;
		if(!cancel)
		{
			if(!gs.NeedPostBack || gs.LoadOnDemand!=0 && this.Rows && this.Rows.length>0)
			{
				gs.NeedPostBack=false;
				if(hr)
					hr.style.display="";
				sr.childNodes[0].childNodes[0].src=this.Band.getCollapseImage();
			}
			igtbl_stateExpandRow(gn,this,true);
			if(!gs.NeedPostBack)
				igtbl_fireEvent(gn,gs.Events.AfterRowExpanded,"(\""+gn+"\",\""+srcRow+"\");");
		}
	}
	else
	{
		if(igtbl_fireEvent(gn,gs.Events.BeforeRowCollapsed,"(\""+gn+"\",\""+srcRow+"\")")==true)
			cancel=true;
		if(!cancel)
		{
			if(!gs.NeedPostBack)
			{
				if(hr)
					hr.style.display="none";
				sr.childNodes[0].childNodes[0].src=this.Band.getExpandImage();
			}
			igtbl_stateExpandRow(gn,this,false);
			if(!gs.NeedPostBack)
				igtbl_fireEvent(gn,gs.Events.AfterRowCollapsed,"(\""+gn+"\",\""+srcRow+"\");");
		}
	}
	if(!cancel)
	{
		if(gs.NeedPostBack)
		{
			if(expand!=false) 
				igtbl_moveBackPostField(gn,"ExpandedRows");
			else
				igtbl_moveBackPostField(gn,"CollapsedRows");
		}
	}
	gs.alignDivs();
	if(gs.NeedPostBack)
		igtbl_doPostBack(gn);
},
"getFirstRow",
function()
{
	return igtbl_getFirstRow(this.Element);
},
"requestChildRows",
function()
{
	if(this.Rows)
		if(this.Node)
		{
			if(this.Rows.Node)
				return;
		}
		else
			return;
	var g=this.Band.Grid;
	if(this.Node && this.Node.selectSingleNode("Rows"))
		return;
	g.QueryString="LODXml\x01"+this.getLevel(true);
	var sqlWhere="";
	var sortOrder="";
	for(var i=0;i<=this.Band.Index;i++)
	{
		var cr=this;
		while(cr && cr.Band!=g.Bands[i])
			cr=cr.ParentRow;
		if(g.Bands[i].DataKeyField && cr.get("lit:DataKey"))
			sqlWhere+=cr.Band.DataKeyField+"="+cr.get("lit:DataKey");
		sqlWhere+=(i==this.Band.Index?"":";");
	}
	for(var i=0;i<g.Bands.length;i++)
	{
		var so="";
		for(var j=0;j<g.Bands[i].SortedColumns.length;j++)
		{
			var col=igtbl_getColumnById(g.Bands[i].SortedColumns[j]);
			so+=col.Key+(col.SortIndicator==2?" DESC":"")+(j<g.Bands[i].SortedColumns.length-1?",":"");
		}
		sortOrder+=so+(i==g.Bands.length-1?"":";");
	}
	var band=g.Bands[this.Band.Index+1],sCols;
	if(band)
	{
		sCols=band.Index;
		for(var i=0;i<band.SortedColumns.length;i++)
		{
			var col=igtbl_getColumnById(band.SortedColumns[i]);
			sCols+="|"+col.Index;
			sCols+=":"+col.IsGroupBy.toString();
			sCols+=":"+col.SortIndicator;
		}
	}
	g.QueryString+="\x02"+sqlWhere;
	g.QueryString+="\x02"+sortOrder;
	g.QueryString+="\x02"+sCols;
	g.RowToQuery=this;
	g.xmlHttpRequest(g.eReqType.ChildRows);
},
"prerenderChildRows",
function()
{
	if(!this.HiddenElement)
	{
		var hidRow=document.createElement("tr");
		var rn=this.Element.id.split("_");
		rn[0]=this.gridId+"rh";
		hidRow.id=rn.join("_");
		hidRow.setAttribute("hiddenRow",true);
		var majCell;
		
		if(this.GroupByRow)
		{
			var majCell=document.createElement("td");
			majCell.style.paddingLeft=this.Band.Indentation;
		}
		else
		{
			var ec=document.createElement("td");
			ec.className=this.Band.getExpAreaClass();
			ec.style.borderWidth=0;
			ec.style.textAlign="center";
			ec.style.padding=0;
			ec.style.cursor="default";
			ec.innerHTML="&nbsp;";
			hidRow.appendChild(ec);
			if(this.Band.getRowSelectors()==1)
			{
				var rsc=document.createElement("td");
				rsc.className=this.Band.getRowLabelClass();
				rsc.innerHTML="&nbsp;";
				hidRow.appendChild(rsc);
			}
			majCell=document.createElement("td");
			majCell.style.overflow="auto";
			majCell.style.width="100%";
			majCell.style.border=0;
			majCell.colSpan=this.Band.VisibleColumnsCount;
		}
		
		hidRow.appendChild(majCell);
		table=document.createElement("table");
		rn[0]=this.gridId+"t";
		table.id=rn.join("_");
		table.border=0;
		table.cellPadding=this.Band.Grid.Element.cellPadding;
		table.cellSpacing=this.Band.Grid.Element.cellSpacing;
		table.setAttribute("bandNo",this.Rows.Band.Index);
		table.style.borderCollapse=this.Band.getBorderCollapse();
		table.style.tableLayout=this.Band.Grid.Element.style.tableLayout;
		
		var tBody;

		if(this.Rows.SelectedNodes[0].nodeName=="Group")
		{
			table.width="100%";
			var tHead=document.createElement("thead");
			var tr=document.createElement("tr");
			var th=document.createElement("th");
			th.innerHTML="&nbsp;";
			tr.appendChild(th);
			tHead.appendChild(tr);
			tHead.style.display="none";
			table.appendChild(tHead);
			tBody=document.createElement("tbody");
			table.appendChild(tBody);
		}
		else
		{
			var colGr=document.createElement("colgroup");
			var col;
			var tableWidth=0;
			if(this.Band.Grid.Bands.length>1)
			{
				col=document.createElement("col");
				col.width=this.Rows.Band.Indentation;
				if(col.width)
					tableWidth+=parseInt(col.width,10);
				colGr.appendChild(col);
			}

			if(this.Rows.Band.getRowSelectors()==1)
			{
				col=document.createElement("col");
				col.width=(this.Rows.Band.RowLabelWidth?this.Rows.Band.RowLabelWidth:"22px");
				if(col.width)
					tableWidth+=parseInt(col.width,10);
				colGr.appendChild(col);
			}
			for(var i=0;i<this.Rows.Band.Columns.length;i++)
			{
				var co=this.Rows.Band.Columns[i];
				if(!co.getHidden() && co.hasCells())
				{
					col=document.createElement("col");
					col.style.width=co.Width;
					if(col.width)
						tableWidth+=parseInt(col.width,10);
					colGr.appendChild(col);
				}
			}
			for(var i=0;i<this.Rows.Band.Columns.length;i++)
				if(this.Rows.Band.Columns[i].getHidden())
				{
					col=document.createElement("col");
					col.style.width=0;
					colGr.appendChild(col);
				}
			table.appendChild(colGr);
			table.style.width=tableWidth;
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

			if(this.Band.Grid.Bands.length>1)
			{
				th=document.createElement("th");
				th.className=this.Rows.Band.NonSelHeaderClass;
				th.height=this.Rows.Band.DefaultRowHeight;
				img=document.createElement("img");
				img.src=this.Band.Grid.BlankImage;
				img.border=0;
				th.appendChild(img);
				tr.appendChild(th);
			}

			if(this.Rows.Band.getRowSelectors()==1)
			{
				th=document.createElement("th");
				th.className=this.Rows.Band.NonSelHeaderClass;
				th.height=this.Rows.Band.DefaultRowHeight;
				img=document.createElement("img");
				img.src=this.Band.Grid.BlankImage;
				img.border=0;
				th.appendChild(img);
				tr.appendChild(th);
			}
			for(var i=0;i<this.Rows.Band.Columns.length;i++)
			{
				var column=this.Rows.Band.Columns[i];
				if(!column.getHidden() && column.hasCells())
				{
					th=document.createElement("th");
					th.id=this.gridId+"c"+"_"+this.Rows.Band.Index+"_"+i.toString();
					th.setAttribute("columnNo",i);
					var ht=column.HeaderText;
					switch(column.SortIndicator)
					{
						case 1:
							ht+="&nbsp;<img src='"+this.Band.Grid.SortAscImg+"' border='0' imgType='sort'>";
							break;
						case 2:
							ht+="&nbsp;<img src='"+this.Band.Grid.SortDscImg+"' border='0' imgType='sort'>";
							break;
					}
					if(!column.Wrap)
						ht="<nobr>"+(ht?ht:"&nbsp;")+"</nobr>";
					if(this.Band.Grid.UseFixedHeaders && column.getFixedHeaderIndicator()==2)
						ht+="&nbsp;<img src='"+(column.Fixed?this.Band.Grid.FixedHeaderOnImage:this.Band.Grid.FixedHeaderOffImage)+"' border='0' width='12' height='12' imgType='fixed' onmouseup='igtbl_fixedClick(event)'>";
					if(column.get("nonfixed"))
					{
						var div1=document.createElement("div");
						div1.className=this.gridId+"-cds";
						var div2=document.createElement("div");
						div2.className=column.getHeadClass();
						if(column.HeaderStyle)
							div2.style.cssText=column.HeaderStyle;
						div2.innerHTML=ht;
						div1.appendChild(div2);
						th.appendChild(div1)
					}
					else
					{
						th.className=column.getHeadClass();
						if(column.HeaderStyle)
							th.style.cssText=column.HeaderStyle;
						th.innerHTML=ht;
					}
					tr.appendChild(th);
				}
			}
			tHead.appendChild(tr);
			if(this.Rows.Band.ColHeadersVisible!=1)
				tHead.style.display="none";
			table.appendChild(tHead);
			tBody=document.createElement("tbody");
			table.appendChild(tBody);
			var footersNode=this.Rows.Node.selectSingleNode("Footers");
			if(this.Rows.Band.ColFootersVisible==1 && footersNode)
			{
				var tFoot=document.createElement("tfoot");
				var tr=document.createElement("tr");
				var th;

				if(this.Band.Grid.Bands.length>1)
				{
					th=document.createElement("th");
					th.className=this.Band.getExpAreaClass();
					th.innerHTML="&nbsp;";
					tr.appendChild(th);
				}

				if(this.Rows.Band.getRowSelectors()==1)
				{
					th=document.createElement("th");
					th.className=this.Band.getRowLabelClass();
					th.innerHTML="&nbsp;";
					tr.appendChild(th);
				}
				var footers=footersNode.selectNodes("Footer");
				for(var i=0;i<this.Rows.Band.Columns.length;i++)
				{
					var column=this.Rows.Band.Columns[i];
					if(!column.Hidden && column.hasCells())
					{
						th=document.createElement("th");
						th.id=this.gridId+"f"+"_"+this.Rows.Band.Index+"_"+i.toString();
						var ht=footers[i].firstChild.text;//column.FooterText;
						//if(!column.Wrap)
						//	ht="<nobr>"+(ht?ht:"&nbsp;")+"</nobr>";
						if(column.get("nonfixed"))
						{
							var div1=document.createElement("div");
							div1.className=this.gridId+"-cds";
							var div2=document.createElement("div");
							div2.className=column.getFooterClass();
							if(column.FooterStyle)
								div2.style.cssText=column.FooterStyle;
							div2.innerHTML=ht;
							div1.appendChild(div2);
							th.appendChild(div1)
						}
						else
						{
							th.className=column.getFooterClass();
							if(column.FooterStyle)
								th.style.cssText=column.FooterStyle;
							th.innerHTML=ht;
						}
						tr.appendChild(th);
					}
				}
				tFoot.appendChild(tr);
				table.appendChild(tFoot);
			}
		}
		majCell.appendChild(table);

		this.Rows.Element=tBody;
		tBody.Object=this.Rows;
		this.HiddenElement=hidRow;
	}
	if(!this.GroupByRow)
	{
		if(this.Element.nextSibling)
			this.Element.parentNode.insertBefore(this.HiddenElement,this.Element.nextSibling);
		else
			this.Element.parentNode.appendChild(this.HiddenElement);
	}
	else
		this.getFirstRow().parentNode.appendChild(this.HiddenElement);
},
"getLevel",
function(s)
{
	var l=new Array();
	l[0]=this.getIndex();
	var pr=this.ParentRow;
	while(pr)
	{
		l[l.length]=pr.getIndex();
		pr=pr.ParentRow;
	}
	l=l.reverse();
	if(s)
	{
		s=l.join("_");
		igtbl_dispose(l);
		delete l;
		return s;
	}
	return l;
},
"getCell",
function(index)
{
	if(index<0 || !this.cells || index>this.cells.length)
		return null;
	if(!this.cells[index])
	{
		var cell=null;
		if(this.Band.Columns[index].hasCells())
		{
			var ri=this.Band.Columns[index].getRealIndex(this);
			if(ri>=0)
				cell=this.Element.cells[this.Band.firstActiveCell+ri];
		}
		var node=null;
		if(this.Node)
			node=this.Node.selectSingleNode("Cells").childNodes[this.Band.Columns[index].Node.firstChild.getAttribute("lit:columnNo")];
		this.cells[index]=new igtbl_Cell(cell,node,this,index);
	}
	return this.cells[index];
},
"getCellByColumn",
function(col)
{
	return this.getCell(col.Index);
},
"getCellFromKey",
function(key)
{
	var cell=null;
	var col=this.Band.getColumnFromKey(key);
	if(col)
		cell=this.getCellByColumn(col);
	return cell;
},
"getChildRow",
function(index)
{
	if(!this.Expandable)
		return null;
	if(index<0 || index>=this.ChildRowsCount || !this.FirstChildRow)
		return null;
	var i=0;
	var r=this.FirstChildRow.Element;
	while(i<index && r)
	{
		r=igtbl_getNextSibRow(this.gridId,r);
		i++;
	}
	if(!r)
		return null;
	return igtbl_getRowById(r.id);
},
"compare",
function(row)
{
	if(this.OwnerCollection!=row.OwnerCollection)
		return 0;
	if(this.GroupByRow)
		return igtbl_getColumnById(this.GroupColId).compareRows(this,row);
	else
	{
		var sc=this.OwnerCollection.Band.SortedColumns;
		for(var i=0;i<sc.length;i++)
		{
			var col=igtbl_getColumnById(sc[i]);
			if(col.hasCells())
			{
				var cell1=this.getCellByColumn(col);
				var cell2=row.getCellByColumn(col);
				var res=col.compareCells(cell1,cell2);
				if(res!=0)
				{
					return res;
				}
			}
		}
	}
	return 0;
},
"remove",
function()
{
	return this.OwnerCollection.remove(this.OwnerCollection.indexOf(this));
},
"getNextTabRow",
function(shift,ignoreCollapse)
{
	var row=null;
	if(shift)
	{
		row=this.getPrevRow();
		if(row)
		{
			while(row.Rows && (row.getExpanded() || ignoreCollapse && row.Expandable))
				row=row.Rows.getRow(row.Rows.length-1);
		}
		else if(this.ParentRow)
			row=this.ParentRow;
	}
	else
	{
		if(this.Rows && (this.getExpanded() || ignoreCollapse && this.Expandable))
			row=this.Rows.getRow(0);
		else
		{
			row=this.getNextRow();
			if(!row && this.ParentRow)
			{
				var pr=this.ParentRow;
				while(!row && pr)
				{
					row=pr.getNextRow();
					pr=pr.ParentRow;
				}
			}
		}
	}
	return row;
},
"getSelected",
function()
{
	if(this.Changes["SelectedRows"])
		return true;
	return false;
},
"setSelected",
function(select)
{
	var str=this.Band.getSelectTypeRow();
	if(str>1)
	{
		if(str==2)
			this.Band.Grid.clearSelectionAll();
		igtbl_selectRow(this.gridId,this,select);
	}
},
"getNextRow",
function()
{
	var nr=this.getIndex()+1;
	while(nr<this.OwnerCollection.length && this.OwnerCollection.getRow(nr).getHidden())
		nr++;
	if(nr<this.OwnerCollection.length)
		return this.OwnerCollection.getRow(nr);
	return null;
},
"getPrevRow",
function()
{
	var pr=this.getIndex()-1;
	while(pr>=0 && this.OwnerCollection.getRow(pr).getHidden())
		pr--;
	if(pr>=0)
		return this.OwnerCollection.getRow(pr);
	return null;
},
"activate",
function(fireEvents)
{
	this.Band.Grid.setActiveRow(this,false,fireEvents);
},
"isActive",
function()
{
	return this.Band.Grid.getActiveRow()==this;
},
"scrollToView",
function()
{
	igtbl_scrollToView(this.gridId,this.Element);
},
"deleteRow",
function()
{
	var gs=igtbl_getGridById(this.gridId);
	var del=false;
	var rowId=this.Element.id;
	if(this.Band.AllowDelete==1 || this.Band.AllowDelete==0 && gs.AllowDelete==1)
	{
		var rows=this.OwnerCollection;
		if(igtbl_inEditMode(this.gridId))
		{
			igtbl_hideEdit(this.gridId);
			if(igtbl_inEditMode(this.gridId))
				return false;
		}
		if(igtbl_fireEvent(this.gridId,gs.Events.BeforeRowDeleted,"(\""+this.gridId+"\",\""+rowId+"\")")==true)
			return false;
		this.OwnerCollection.setLastRowId();
		gs.Element.parentNode.scrollLeft=0;
		del=true;
		var prevAdded=typeof(gs.AddedRows[rowId])!="undefined";
		if(this.getExpanded())
			this.toggleRow();
		igtbl_clearRowChanges(gs,this);
		gs.recordChange("DeletedRows",this);
		if(!prevAdded)
			gs.invokeXmlHttpRequest(gs.eReqType.DeleteRow,this);
		else
			this.Changes["DeletedRows"].setFireEvent(false);
		for(var rid in gs.AddedRows)
			if(rid.substr(0,rowId.length)==rowId)
				igtbl_clearRowChanges(gs,igtbl_getRowById(rid));
		if(!rows.deletedRows)
			rows.deletedRows=new Array();
		var ar=this.Band.Grid.getActiveRow();
		var needPB=false;
		this.Element.setAttribute("deleted",true);
		if(typeof(this.Node)=="undefined")
		{
			for(var i=0;i<this.Band.Columns.length;i++)
			{
				var cell=this.getCellByColumn(this.Band.Columns[i]);
				if(!cell && this.Band.Columns[i].hasCells())
				{
					var row=this;
					while(row.getPrevRow() && !cell)
					{
						row=row.getPrevRow();
						cell=row.getCellByColumn(this.Band.Columns[i]);
					}
					if(row==this || !cell || cell.Element && cell.Element.rowSpan==1)
					{
						needPB=true;
						break;
					}
				}
				else if(cell && cell.Element && cell.Element.rowSpan>1)
				{
					needPB=true;
					break;
				}
				if(cell && cell.Element && cell.Element.rowSpan>1)
					cell.Element.rowSpan--;
			}
		}
		if(!needPB)
		{
			rows.deletedRows[rows.deletedRows.length]=this.remove();
			var pr=this.ParentRow;
			if(pr)
			{
				pr.VisChildRowsCount--;
				pr.ChildRowsCount--;
			}
			while(pr)
			{
				if(pr.Expandable && pr.Rows.length==0)
				{
					pr.setExpanded(false);
					if(pr.GroupByRow)
					{
						gs.DeletedRows[pr.Element.id]=true;
						pr.Element.setAttribute("deleted",true);
						rows.deletedRows[rows.deletedRows.length]=pr.remove();
						delete gs.SelectedRows[pr.Element.id];
					}
					else
						pr.Element.childNodes[0].childNodes[0].style.display="none";
				}
				pr=pr.ParentRow;
			}
			if(this.Node && !gs.isDeletingSelected)
				rows.reIndex(this.getIndex());
			if(ar==this)
				this.Band.Grid.setActiveRow(null);
			else
			{
				var ac=this.Band.Grid.getActiveCell();
				if(ac && ac.Row==this)
					this.Band.Grid.setActiveCell(null);
			}
		}
		else
			igtbl_needPostBack(this.gridId);
		gs._calculateStationaryHeader();			
		igtbl_fireEvent(this.gridId,gs.Events.AfterRowDeleted,"(\""+this.gridId+"\",\""+rowId+"\");");
		if(gs.LoadOnDemand==3)
			gs.NeedPostBack=false;
	}
	return del;
},
"getLeft",
function(offsetElement)
{
	return igtbl_getLeftPos(igtbl_getElemVis(this.Element.cells,igtbl_getBandFAC(this.gridId,this.Element)),true,offsetElement);
},
"getTop",
function(offsetElement)
{
	var t=igtbl_getTopPos(this.Element,true,offsetElement);
	return t;
},
"editRow",
function(force)
{
	var au=igtbl_getAllowUpdate(this.gridId,this.Band.Index);
	if(igtbl_currentEditTempl!="" || !force && au!=1 && au!=3)
		return;
	var editTempl=igtbl_getElementById(this.Band.RowTemplate);
	if(!editTempl)
		return;
	if(igtbl_fireEvent(this.gridId,igtbl_getGridById(this.gridId).Events.BeforeRowTemplateOpen,"(\""+this.gridId+"\",\""+this.Element.id+"\",\""+this.Band.RowTemplate+"\")"))
		return;
	try
	{
		if(editTempl.style.filter!=null && this.Band.ExpandEffects)
		{
			var ee=this.Band.ExpandEffects;
			if(ee.EffectType!='NotSet')
			{
				editTempl.style.filter="progid:DXImageTransform.Microsoft."+ee.EffectType+"(duration="+ee.Duration/1000+");"
				if(ee.ShadowWidth>0)
					editTempl.style.filter+=" progid:DXImageTransform.Microsoft.Shadow(Direction=135, Strength="+ee.ShadowWidth+",color="+ee.ShadowColor+");"
				if(ee.Opacity<100)
					editTempl.style.filter+=" progid:DXImageTransform.Microsoft.Alpha(Opacity="+ee.Opacity+");"
				if(editTempl.filters[0]!=null)
					editTempl.filters[0].apply();
				if(editTempl.filters[0]!=null)
					editTempl.filters[0].play();
			}
			else
			{
				if(ee.ShadowWidth>0)
					editTempl.runtimeStyle.filter="progid:DXImageTransform.Microsoft.Shadow(Direction=135, Strength="+ee.ShadowWidth+",ee.Color="+ee.ShadowColor+");"
				if(ee.Opacity<100)
					editTempl.runtimeStyle.filter+=" progid:DXImageTransform.Microsoft.Alpha(Opacity="+ee.Opacity+");"
			}
		}
	}
	catch(ex){}
	editTempl.style.display="";
	editTempl.setAttribute("noHide",true);
	editTempl.style.left=igtbl_getRelativePos(this.gridId,this.Element,"Left");
	var tw=igtbl_clientWidth(editTempl);
	var bw=document.body.clientWidth;
	var gdw = igtbl_getGridById(this.gridId).Element.parentNode.scrollLeft
	editTempl.style.left=editTempl.offsetLeft+gdw;
	if(editTempl.offsetLeft+tw-document.body.scrollLeft>bw)
	//if(editTempl.offsetLeft+tw>bw)
		if(bw-tw+document.body.scrollLeft-gdw>0)
			editTempl.style.left=bw-tw+document.body.scrollLeft-gdw;
		else
			editTempl.style.left=0;
	editTempl.style.top=igtbl_getRelativePos(this.gridId,this.Element,"Top")+this.Element.offsetHeight/*-1*/;
	var th=igtbl_clientHeight(editTempl);
	var bh=document.body.clientHeight;
	if(editTempl.offsetTop+th>bh)
		if(bh-th+document.body.scrollTop>0)
			editTempl.style.top=bh-th+document.body.scrollTop;
		else
			editTempl.style.top=0;
	editTempl.setAttribute("editRow",this.Element.id);
	igtbl_fillEditTemplate(this,editTempl.childNodes);
	if(igtbl_focusedElement && igtbl_isVisible(igtbl_focusedElement))
	{
		igtbl_focusedElement.focus();
		if(igtbl_focusedElement.select)
			igtbl_focusedElement.select();
		igtbl_focusedElement=null;
	}
	igtbl_currentEditTempl=this.Band.RowTemplate;
	igtbl_oldMouseDown=document.onmousedown;
	document.onmousedown=igtbl_gRowEditMouseDown;
	igtbl_justAssigned=true;
	window.setTimeout(igtbl_resetJustAssigned,100);
	editTempl.removeAttribute("noHide");
	igtbl_fireEvent(this.gridId,igtbl_getGridById(this.gridId).Events.AfterRowTemplateOpen,"(\""+this.gridId+"\",\""+this.Element.id+"\")");
},
"endEditRow",
function(saveChanges)
{
	if(arguments.length==0 || typeof(saveChanges)=="undefined")
		saveChanges=false;
	var gs=igtbl_getGridById(this.gridId);
	var editTempl=igtbl_getElementById(this.Band.RowTemplate);
	if(!editTempl || editTempl.style.display!="")
		return;
	if(editTempl.getAttribute("noHide"))
		return;
	if(igtbl_fireEvent(this.gridId,gs.Events.BeforeRowTemplateClose,"(\""+this.gridId+"\",\""+this.Element.id+"\","+saveChanges.toString()+")"))
		return;
	editTempl.style.display="none";
	igtbl_currentEditTempl="";
	document.onmousedown=igtbl_oldMouseDown;
	if(saveChanges)
		igtbl_unloadEditTemplate(this,editTempl.childNodes);
	igtbl_fireEvent(this.gridId,gs.Events.AfterRowTemplateClose,"(\""+this.gridId+"\",\""+this.Element.id+"\","+saveChanges.toString()+")");
	if(gs.NeedPostBack)
		igtbl_doPostBack(gs.Id);
},
"getHidden",
function()
{
	return (this.Element.style.display=="none");
},
"setHidden",
function(h)
{
	this.Element.style.display=(h?"none":"");
	if(this.ParentRow)
		this.ParentRow.VisChildRowsCount+=(h?-1:1);
	var ac=this.Band.Grid.getActiveCell();
	if(ac && ac.Row==this && h)
		this.Band.Grid.setActiveCell(null);
	else
	{
		var ar=this.Band.Grid.getActiveRow();
		if(ar && ar==this && h)
			this.Band.Grid.setActiveRow(null);
		else
			this.Band.Grid.alignGrid();
	}
},
"find",
function(re,back)
{
	var g=this.Band.Grid;
	if(re)
		g.regExp=re;
	if(!g.regExp)
		return null;
	g.lastSearchedCell=null;
	if(back==true || back==false)
		g.backwardSearch=back;
	var cell=null;
	if(!g.backwardSearch)
	{
		cell=this.getCell(0);
		if(cell && !cell.Column.getVisible())
			cell=cell.getNextCell();
		while(cell && cell.getValue(true).search(g.regExp)==-1)
			cell=cell.getNextCell();
	}
	else
	{
		cell=this.getCell(this.cells.length-1);
		if(cell && !cell.Column.getVisible())
			cell=cell.getPrevCell();
		while(cell && cell.getValue(true).search(g.regExp)==-1)
			cell=cell.getPrevCell();
	}
	if(cell)
		g.lastSearchedCell=cell;
	return g.lastSearchedCell;
},
"findNext",
function(re,back)
{
	var g=this.Band.Grid;
	if(!g.lastSearchedCell || g.lastSearchedCell.Row!=this)
		return this.find(re,back);
	if(re)
		g.regExp=re;
	if(!g.regExp)
		return null;
	if(back==true || back==false)
		g.backwardSearch=back;
	var cell=null;
	if(!g.backwardSearch)
	{
		cell=g.lastSearchedCell.getNextCell();
		while(cell && cell.getValue(true).search(g.regExp)==-1)
			cell=cell.getNextCell();
	}
	else
	{
		cell=g.lastSearchedCell.getPrevCell();
		while(cell && cell.getValue(true).search(g.regExp)==-1)
			cell=cell.getPrevCell();
	}
	if(cell)
		g.lastSearchedCell=cell;
	else
		g.lastSearchedCell=null;
	return g.lastSearchedCell;
},
"setSelectedRowImg",
function(hide)
{
	var gs=this.Band.Grid;
	if (gs.AllowRowNumbering>=2) return;
	var row=this.Element;
	if(gs.currentTriImg!=null)
	{
		gs.lastSelectedRow=null;
		var imgObj;
		imgObj=document.createElement("img");
		imgObj.setAttribute("imgType","blank");
		imgObj.border="0";
		if(gs.RowLabelBlankImage)
			imgObj.src=gs.RowLabelBlankImage;
		else
		{
			imgObj.src=gs.BlankImage;
			imgObj.style.visibility="hidden";
		}
		gs.currentTriImg.parentNode.appendChild(imgObj);
		gs.currentTriImg.parentNode.removeChild(gs.currentTriImg);
		gs.currentTriImg=null;
	}
	if(!hide && row && !row.getAttribute("deleted") && !row.getAttribute("groupRow") && this.Band.getRowSelectors()!=2)
	{
		var rl=row.cells[this.Band.firstActiveCell-1];
		if(rl.childNodes.length==0 || !(rl.childNodes[0].tagName=="IMG" && rl.childNodes[0].getAttribute("imgType")=="newRow"))
		{
			var imgObj;
			imgObj=document.createElement("img");
			imgObj.src=igtbl_getCurrentRowImage(this.gridId,this.Band.Index);
			imgObj.border="0";
			imgObj.setAttribute("imgType","tri");
			var cell=row.cells[this.Band.firstActiveCell-1];
			cell.innerHTML="";
			cell.appendChild(imgObj);
			gs.currentTriImg=imgObj;
		}
		gs.lastSelectedRow=row.id;
	}
},
"renderActive",
function(render)
{
	var g=this.Band.Grid;
	if(!g.Activation.AllowActivation)
		return;
	if(this.GroupByRow)
	{
		var fr=this.getFirstRow();
		if(ig_csom.IsNetscape || ig_csom.IsNetscape6)
		{
			igtbl_changeBorder(g,fr.firstChild.style,this,"Left",render);
			igtbl_changeBorder(g,fr.firstChild.style,this,"Top",render);
			igtbl_changeBorder(g,fr.firstChild.style,this,"Right",render);
			igtbl_changeBorder(g,fr.firstChild.style,this,"Bottom",render);
		}
		else
			igtbl_changeBorder(g,fr.firstChild.runtimeStyle,this,"",render);
	}
	else
	{
		if(this.cells.length==0)
			return;
 		if(render==false)
		{
			var i=0;
			var cell=this.getCell(i);
			while(cell && !cell.Column.getVisible() && i<this.cells.length)
				cell=this.getCell(++i);
			if(i<this.cells.length)
				cell.renderActiveLeft(false);
			for(i=0;i<this.cells.length;i++)
			{
				cell=this.getCell(i);
				cell.renderActiveTop(false);
				cell.renderActiveBottom(false);
			}
			i=this.cells.length-1;
			cell=this.getCell(i);
			while(cell && !cell.Column.getVisible() && i>=0)
				cell=this.getCell(--i);
			if(i>=0)
				cell.renderActiveRight(false);
		}
		else
		{
			var i=0;
			var cell=this.getCell(i);
			while(cell && !cell.Column.getVisible() && i<this.cells.length)
				cell=this.getCell(++i);
			if(i<this.cells.length)
				cell.renderActiveLeft();
			for(var i=0;i<this.cells.length;i++)
			{
				cell=this.getCell(i);
				cell.renderActiveTop();
				cell.renderActiveBottom();
			}
			i=this.cells.length-1;
			cell=this.getCell(i);
			while(cell && !cell.Column.getVisible() && i>=0)
				cell=this.getCell(--i);
			if(i>=0)
				cell.renderActiveRight();
		}
	}
},
"select",
function(selFlag,fireEvent)
{
	var gs=this.Band.Grid;
	if(this.Band.getSelectTypeRow()<2)
		return false;
	if(gs.exitEditCancel || gs.noCellChange)
		return false;
	if(fireEvent!=false)
		if(igtbl_fireEvent(gs.Id,gs.Events.BeforeSelectChange,"(\""+gs.Id+"\",\""+this.Element.id+"\")")==true)
			return false;
	if(!this.GroupByRow)
		for(var i=0;i<this.cells.length;i++)
			this.getCell(i).selectCell(selFlag);
	else if(selFlag!=false)
		igtbl_changeStyle(gs.Id,this.FirstRow.cells[0],this.Band.getSelGroupByRowClass());
	else
		igtbl_changeStyle(gs.Id,this.FirstRow.cells[0],null);
	if(selFlag!=false)
		gs.recordChange("SelectedRows",this);
	else if(gs.SelectedRows[this.Element.id])
			gs.removeChange("SelectedRows",this);
	if(this==gs.oActiveRow)
		this.renderActive();
	if(fireEvent!=false)
	{
		igtbl_fireEvent(gs.Id,gs.Events.AfterSelectChange,"(\""+gs.Id+"\",\""+this.Element.id+"\");");
		if(gs.NeedPostBack)
			igtbl_moveBackPostField(gs.Id,"SelectedRows");
	}
	return true;
},
"processUpdateRow",
function()
{
	return this._processUpdateRow();
},
"_processUpdateRow",
function()
{
	var result=false;
	var g=this.Band.Grid;
	if(!this._dataChanged || typeof(g.Events.BeforeRowUpdate)=="undefined")
		return result;
	for(var i=0;(this._dataChanged&2) && i<this.cells.length;i++)
		if(typeof(this.getCell(i)._oldValue)!="undefined")
			break;
	if(i<this.cells.length)
	{
		g.QueryString="";
		result=g.fireEvent(g.Events.BeforeRowUpdate,[g.Id,this.Element.id]);
		if((this._dataChanged&2))
			for(;i<this.cells.length;i++)
			{
				var cell=this.getCell(i);
				if(typeof(cell._oldValue)!="undefined")
				{
					if(result)
						cell.setValue(cell._oldValue,false);
					else if(g.LoadOnDemand==3)
						g.QueryString+=(g.QueryString&&g.QueryString.length>0?"\x04":"")+"UpdateCell\x02"+cell.Column.Key+":"+igtbl_escape(cell.getValue(true));
				}
			}
		if(!result)
		{
			if(g.LoadOnDemand==3 && (g.Events.AfterRowUpdate[1] || g.Events.XmlHTTPResponse[1]))
				g.invokeXmlHttpRequest(g.eReqType.UpdateRow,this);
			else
			{
				g.fireEvent(g.Events.AfterRowUpdate,[g.Id,this.Element.id]);
				if(g.NeedPostBack)
					igtbl_doPostBack(g.Id);
			}
		}
		this._dataChanged=0;
	}
	return result;
},
"_getRowNumber",
function()
{
	var index = null;
	var oLbl= igtbl_getElementById(this.gridId+"l_"+this.getLevel(true));
	if (this.Band.RowSelectors==0 && this.Band.AllowRowNumbering>1 && oLbl)
			index=oLbl.innerText;
	return index;
},
"_setRowNumber",
function(value)
{	
	var oRS = this.Band.firstActiveCell-1;	
	var oLbl=-1;
	if (this.Element)
		oLbl=this.Element.childNodes[oRS];//igtbl_getElementById(this.gridId+"l_"+this.getLevel(true));
	if (this.Band.RowSelectors==0 && this.Band.AllowRowNumbering>1)
	{
		if (this.Node)this.Node.setAttribute("lit:rowNumber",value);		
		if (oLbl)oLbl.innerHTML=value;
		return value;
	}
	else
		return -1;	
},
"_generateUpdateRowSemaphore",
function()
{
	var cellInfo="";
	for(var j=0;j<this.cells.length;j++)
	{
		var cell=this.getCell(j);
		if(cell)
		{
			if(typeof(cell.getOldValue())!="undefined")
			{
				var oldValue=cell.getOldValue();
				if(oldValue==null)
					oldValue="";
				else
					oldValue=oldValue.toString();
				cellInfo+=(cellInfo.length>0?"\x03":"")+igtbl_escape(cell.Column.Key+"\x05"+cell.Column.Index+"\x05"+oldValue);
				delete cell._oldValue;
			}
			else
				cellInfo+=(cellInfo.length>0?"\x03":"")+igtbl_escape(cell.Column.Key+"\x05"+cell.Column.Index+"\x05"+cell.getValue(true));
		}
	}
	return cellInfo;
}
];
for(var i=0;i<igtbl_ptsRow.length;i+=2)
	igtbl_Row.prototype[igtbl_ptsRow[i]]=igtbl_ptsRow[i+1];

var igtbl_oldMouseDown=null;
var igtbl_currentEditTempl="";
var igtbl_justAssigned=false;
var igtbl_focusedElement=null;

/* Cell object */
igtbl_Cell.prototype=new igtbl_WebObject();
igtbl_Cell.prototype.constructor=igtbl_Cell;
igtbl_Cell.base=igtbl_WebObject.prototype;
function igtbl_Cell(element,node,row,index)
{
	if(arguments.length>0)
		this.init(element,node,row,index);
}
var igtbl_ptsCell=[
"init",
function(element,node,row,index)
{
	igtbl_Cell.base.init.apply(this,["cell",element,node]);

	var gs=row.OwnerCollection.Band.Grid;
	this.Row=row;
	this.Band=row.Band;
	this.Column=this.Band.Columns[index];
	this.Index=index;
	var cell=this.Element;
	if(gs.UseFixedHeaders && !this.Column.getFixed() && cell)
		this.scrElem=this.Element.firstChild.firstChild;
	if(cell)
	{
		cell.Object=this;
		this.NextSibling=cell.nextSibling;
		if(cell.cellIndex==this.Band.firstaActiveCell)
			this.PrevSibling=null;
		else
			this.PrevSibling=cell.previousSibling;
		if(this.Column.MaskDisplay)
			this.MaskedValue=igtbl_getInnerText(cell);
	}
	this.Changes=[];
},
"getElement",
function()
{
	if(this.scrElem)
		return this.scrElem;
	return this.Element;
},
"getValue",
function(textValue)
{
	var value;
	if(this.Node)
		value=unescape(this.Node.selectSingleNode("Value").text);
	if(this.Element)
	{
		if(!this.Node)
			value=this.Element.getAttribute("igCellText");
		if(typeof(value)!="string")
		{
			value=this.Element.getAttribute("unmaskedValue");
			if (value) value = unescape(value);			
			/*value=this.Element.getAttribute("unmaskedValue");*/
			if(typeof(value)=="undefined" || value==null)
			{
				var elem=this.Element;
				if(elem.childNodes.length>0 && elem.childNodes[0].tagName=="NOBR")
					elem=elem.childNodes[0];
				if(elem.childNodes.length>0 && elem.childNodes[0].tagName=="A")
					elem=elem.childNodes[0];
				value=igtbl_getInnerText(elem);
			}
			else if(textValue)
			{
				if(this.MaskedValue)
					value=this.MaskedValue;
				else
					value=value.toString();
			}
			var oCombo=null;
			this.Column.ensureWebCombo();
			if(this.Column.WebComboId)
				oCombo=igcmbo_getComboById(this.Column.WebComboId);
			if(oCombo)
			{
				if(!textValue)
				{
					var oCombo=igcmbo_getComboById(this.Column.WebComboId);
					if(oCombo && oCombo.DataTextField)
					{
						var re=new RegExp("^"+igtbl_getRegExpSafe(value)+"$","gi");
						var column=oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataTextField);
						var cell=column.find(re);
						if(cell && oCombo.DataValueField)
							value=cell.Row.getCellByColumn(oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataValueField)).getValue(true);
						delete re;
					}
				}
			}
			else if(this.Column.ColumnType==3 && this.Element.childNodes.length>0)
			{
				var chBox=this.Element.childNodes[0];
				while(chBox && chBox.tagName!="INPUT")
					chBox=chBox.childNodes[0];
				value=false;
				if(chBox)
					value=chBox.checked;
				if(textValue)
					value=value.toString();
			}
			else if(this.Column.ColumnType==5 && this.Column.ValueList.length>0)
			{
				if(!textValue)
					for(var i=0;i<this.Column.ValueList.length;i++)
						if(this.Column.ValueList[i][1]==value)
						{
							value=this.Column.ValueList[i][0];
							break;
						}
			}
			else if(this.Column.ColumnType==7 && this.Element.childNodes.length>0)
			{
				var button=this.Element.childNodes[0];
				while(button && button.tagName!="INPUT")
					button=button.childNodes[0];
				if(button)
					value=button.value;
			}
			if(typeof(value)=="string" && this.Column.AllowNull && igtbl_trim(value)==this.Column.getNullText())
			{
				if(textValue)
					value=this.Column.getNullText();
				else
					value=null;
			}
		}
	}
	if(typeof(value)!="undefined")
	{
		if(!textValue)
			value=this.Column.getValueFromString(value);
	}
	else if(textValue)
		value="";
	return value;
},
"setValue",
function(value,fireEvents)
{
	if(typeof(fireEvents)=="undefined")
		fireEvents=true;
	var gn=this.Row.gridId;
	var gs=igtbl_getGridById(gn);
	if(this.Column.DataType!=8 && typeof(value)=="string")
		value=igtbl_trim(value);
	if(!gs.insideBeforeUpdate)
	{
		gs.insideBeforeUpdate=true;
		var ev=value;
		if(ev==null)
			ev=this.Column.getNullText();
		else
		{
			ev=ev.toString().replace(/\r\n/g,"\\r\\n");
			ev=ev.replace(/\"/g,"\\\"");
		}
		var res=fireEvents && this.Element && igtbl_fireEvent(gn,gs.Events.BeforeCellUpdate,"(\""+gn+"\",\""+this.Element.id+"\",\""+ev+"\")");
		gs.insideBeforeUpdate=false;
		if(res==true)
			return;
	}
	var v=value;
	var oldValue=this.getValue();
	if(typeof(value)!="undefined" && value!=null)
	{
		if((oldValue && oldValue.getMonth || this.Column.DataType==7) && typeof(value)=="string")
		{
			if(this.Column.MaskDisplay.substr(0,1).toLowerCase()=="h")
				value="01/01/01 "+value;
			else
			{
				var year="";
				for(var i=value.length-1;i>=0;i--)
				{
					var y=parseInt(value.charAt(i),10);
					if(isNaN(y))
						break;
					else
						year=y.toString()+year;
				}
				if(year && year.length<3)
					value=value.substr(0,i+1)+(parseInt(year,10)+gs.DefaultCentury).toString();
			}
			value=new Date(value);
		}
		if(value.getMonth)
		{
			if(isNaN(value)) value=oldValue;
			v=value;
			if(value)
				value=(value.getMonth()+1).toString()+"/"+value.getDate().toString()+"/"+(value.getFullYear().toString().length>4?value.getFullYear().toString().substr(0,4):value.getFullYear())+" "+(value.getHours()==0?"12":(value.getHours()%12).toString())+":"+(value.getMinutes()<10?"0":"")+value.getMinutes()+":"+(value.getSeconds()<10?"0":"")+value.getSeconds()+" "+(value.getHours()<12?"AM":"PM");
		}
	}
	if(this.Element)
	{
		if(this.Element.getAttribute("igCellText")!=null)
			this.Element.setAttribute("igCellText",value);
		else 
		{
			var rendVal=null;
			if(this.Column.editorControl && this.Column.editorControl.getRenderedValue && (rendVal=this.Column.editorControl.getRenderedValue(v))!=null)
			{
				v=rendVal;
				if(value!=null)
				{	

					this.Element.setAttribute("unmaskedValue",value.toString());
				}
				else
					this.Element.removeAttribute("unmaskedValue");
				this.MaskedValue=v;
			}
			else 
			{
				if(this.Column.AllowNull && (typeof(v)=="undefined" || v==null || typeof(v)=="string" && (v=="" || v==this.Column.getNullText())))
				{
					v=this.Column.getNullText();
					value="";
				}
				else
					v=typeof(value)!="undefined" && value!=null?value.toString():"";
				if(this.Column.MaskDisplay!="")
				{
					if(this.Column.AllowNull && v==this.Column.getNullText())
					{						
						this.Element.setAttribute("unmaskedValue",null);
						this.MaskedValue=(v==""?" ":v);
					}
					else
					{
						v=igtbl_Mask(gn,v,this.Column.DataType,this.Column.MaskDisplay);
						if(v=="")
						{
							var umv=this.Element.getAttribute("unmaskedValue");
							if(typeof(umv)!="undefined" && umv!=null)
								v=igtbl_Mask(gn,umv,this.Column.DataType,this.Column.MaskDisplay);
							else
							{
								v=this.Column.getNullText();
								value="";
							}
						}
						else
						{
							if(this.Column.MaskDisplay=="MM/dd/yyyy" || this.Column.MaskDisplay=="MM/dd/yy" || this.Column.MaskDisplay=="hh:mm" || this.Column.MaskDisplay=="HH:mm" || this.Column.MaskDisplay=="hh:mm tt")
								value=v;
							this.Element.setAttribute("unmaskedValue",value);
							this.MaskedValue=v;							
						}
					}
				}
				else if(this.Element.getAttribute("unmaskedValue"))
					this.Element.setAttribute("unmaskedValue",value)
				if(!(this.Column.AllowNull && v==this.Column.getNullText()))
				{
					if(this.Column.MaskDisplay=="")
					{
						if(typeof(value)!="undefined" && value!=null && this.Column.DataType!=7)
						{
							v=this.Column.getValueFromString(value);
							if(v) v=v.toString();
						}							
						if(this.Column.FieldLength>0)
						{
							v=v.substr(0,this.Column.FieldLength);
							value=v;
						}
						if(this.Column.Case==1)
							v=v.toLowerCase();
						else if(this.Column.Case==2)
							v=v.toUpperCase();
					}
				}
			}
			var setInner=true;
			this.Column.ensureWebCombo();
			if(this.Column.WebComboId && typeof(igcmbo_getComboById)!="undefined")
			{
				var oCombo=igcmbo_getComboById(this.Column.WebComboId);
				if(oCombo && oCombo.DataValueField)
				{
					var re=new RegExp("^"+igtbl_getRegExpSafe(value)+"$","gi");
					var column=oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataValueField);
					var cell=column.find(re);
					if(cell && oCombo.Prompt && cell.Row.getIndex()==0)
						cell=column.findNext();
					if(cell && oCombo.DataTextField)
						v=cell.Row.getCellByColumn(oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataTextField)).getValue(true);
					this.Element.setAttribute("igDataValue",value);
					this.Element.setAttribute("unmaskedValue",value.toString());
					delete re;
				}
			}
			else if(this.Column.ColumnType==3 && this.Element.childNodes.length>0)
			{
				igtbl_dontHandleChkBoxChange=true;
				var chBox=this.Element.childNodes[0];
				while(chBox && chBox.tagName!="INPUT")
					chBox=chBox.childNodes[0];
				if(chBox)
				{
					if(!value || value.toString().toLowerCase()=="false")
						chBox.checked=false;
					else
						chBox.checked=true;
				}
				igtbl_dontHandleChkBoxChange=false;
				setInner=false;
			}
			else if(this.Column.ColumnType==5 && this.Column.ValueList.length>0)
			{
				for(var i=0;i<this.Column.ValueList.length;i++)
					if(this.Column.ValueList[i][0]==value)
					{
						v=this.Column.ValueList[i][1];
						this.Element.setAttribute("igDataValue",value);
						break;
					}
			}
			else if(this.Column.ColumnType==7 && this.Element.childNodes.length>0)
			{
				var button=this.Element.childNodes[0];
				while(button && button.tagName!="INPUT")
					button=button.childNodes[0];
				if(button)
				{
					button.value=v;
					setInner=false;
				}
				else
				{
					button=igtbl_getElementById(gn+"_bt");
					if(button)
						button.value=v;
				}
			}
			if(setInner)
			{
				var vs=v;
				var e=this.Element;
				if(!vs && (typeof(vs)=="undefined" || typeof(vs)=="string") && vs=="")
				//if(vs=="")
				{
					vs=" ";
					e.setAttribute("unmaskedValue","");
				}
				else if(e.getAttribute("unmaskedValue","")=="")
					e.removeAttribute("unmaskedValue");
				if(this.scrElem)
					e=this.scrElem;
				el=e;
				if(el.firstChild && el.firstChild.tagName=="NOBR")
					el=el.firstChild;
				if(el.firstChild && el.firstChild.tagName=="A")
					el=el.firstChild;
				igtbl_setInnerText(el,vs);
				if(el.tagName=="A" && this.Column.ColumnType==9)
					el.href=(v.indexOf('@')>=0?"mailto:":"")+v;
				if(this.Node)
					this.Node.selectSingleNode("Content").firstChild.text=(e.getAttribute("unmaskedValue","")=="")?"&nbsp;":vs;				
			}
		}
	}
	
	if (this.Node) this.Node.selectSingleNode("Value").text = igtbl_escape(value==null?this.Column.getNullText():value);
	var newValue=this.getValue();
	if(!((typeof(newValue)=="undefined" || newValue==null) && (typeof(oldValue)=="undefined" || oldValue==null) || newValue!=null && oldValue!=null && newValue.valueOf()==oldValue.valueOf()))
	{
		this.Row._dataChanged|=2;
		if(typeof(this._oldValue)=="undefined")
			this._oldValue=oldValue;
		igtbl_saveChangedCell(gs,this,value);
		if(this.Node)
		{
			this.Node.selectSingleNode("Value").text=value==null?"":igtbl_escape(value.toString());
			gs.invokeXmlHttpRequest(gs.eReqType.UpdateCell,this,value.toString());
		}
		if(fireEvents && this.Element)
		{
			igtbl_fireEvent(gn,gs.Events.AfterCellUpdate,"(\""+gn+"\",\""+this.Element.id+"\")");
			if(gs.LoadOnDemand==3)
				gs.NeedPostBack=false;
		}
	}
},
"getRow",
function()
{
	return this.Row;
},
"getNextTabCell",
function(shift)
{
	var g=this.Row.Band.Grid;
	var cell=null;
	switch(g.TabDirection)
	{
		case 0:
		case 1:
			if(shift && g.TabDirection==0 || !shift && g.TabDirection==1)
			{
				cell=this.getPrevCell();
				if(!cell)
				{
					var row=this.Row.getNextTabRow(true);
					if(row && !row.GroupByRow)
					{
						cell=row.getCell(row.cells.length-1);
						if(!cell.Column.getVisible())
							cell=cell.getPrevCell();
					}
				}
			}
			else
			{
				cell=this.getNextCell();
				if(!cell)
				{
					var row=this.Row.getNextTabRow(false);
					if(row && !row.GroupByRow)
					{
						cell=row.getCell(0);
						if(!cell.Column.getVisible())
							cell=cell.getNextCell();
					}
				}
			}
			break;
		case 2:
		case 3:
			if(shift && g.TabDirection==2 || !shift && g.TabDirection==3)
			{
				var row=this.Row.getPrevRow();
				if(row && row.getExpanded())
				{
					row=this.Row.getNextTabRow(true);
					cell=row.getCell(row.cells.length-1);
					if(!cell.Column.getVisible())
						cell=cell.getPrevCell();
				}
				else if(row)
					cell=row.getCell(this.Index);
				else
				{
					if(this.Index==0)
					{
						row=this.Row.getNextTabRow(true);
						if(row && !row.GroupByRow)
						{
							cell=row.getCell(row.cells.length-1);
							if(!cell.Column.getVisible())
								cell=cell.getPrevCell();
						}
					}
					else
					{
						cell=this.Row.OwnerCollection.getRow(this.Row.OwnerCollection.length-1).getCell(this.Index-1);
						if(!cell.Column.getVisible())
							cell=cell.getPrevCell();
					}
				}
			}
			else
			{
				if(this.Row.getExpanded())
				{
					cell=this.Row.Rows.getRow(0).getCell(0);
					if(!cell.Column.getVisible())
						cell=cell.getNextCell();
				}
				else
				{
					var row=this.Row.getNextRow();
					if(row)
						cell=row.getCell(this.Index);
					else if(this.Index<this.Row.cells.length-1)
					{
						cell=this.Row.OwnerCollection.getRow(0).getCell(this.Index+1);
						if(!cell.Column.getVisible())
							cell=cell.getNextCell();
					}
					else
					{
						row=this.Row.getNextTabRow(false);
						if(row && !row.GroupByRow)
						{
							cell=row.getCell(0);
							if(!cell.Column.getVisible())
								cell=cell.getNextCell();
						}
					}
				}
			}
			break;
	}
	return cell;
},
"beginEdit",
function(keyCode)
{
	igtbl_editCell((typeof(event)!="undefined"?event:null),this.Row.gridId,this.Element,keyCode);
},
"endEdit",
function()
{
	igtbl_hideEdit(this.Row.gridId);
},
"getSelected",
function()
{
	if(this.Changes["SelectedCells"])
		return true;
	return false;
},
"setSelected",
function(select)
{
	var stc=this.Band.getSelectTypeCell();
	if(stc>1)
	{
		if(stc==2)
			this.Band.Grid.clearSelectionAll();
		igtbl_selectCell(this.Row.gridId,this,select);
	}
},
"getNextCell",
function()
{
	var nc=this.Index+1;
	while(nc<this.Row.cells.length && !this.Row.getCell(nc).Column.getVisible())
		nc++;
	if(nc<this.Row.cells.length)
		return this.Row.getCell(nc);
	return null;
},
"getPrevCell",
function()
{
	var pc=this.Index-1;
	while(pc>=0 && !this.Row.getCell(pc).Column.getVisible())
		pc--;
	if(pc>=0)
		return this.Row.getCell(pc);
	return null;
},
"activate",
function()
{
	this.Row.Band.Grid.setActiveCell(this);
},
"scrollToView",
function()
{
	var g=this.Row.Band.Grid;
	if(g.UseFixedHeaders)
	{
		var c=this.Column;
		var w=0,i=0,c1=null;
		while(i<c.Index)
		{
			c1=c.Band.Columns[i++];
			if(!c1.getFixed())
				w+=c1.getWidth();
		}
		igtbl_scrollToView(g.Id,this.Element,c.getWidth(),w);
		return;
	}
	igtbl_scrollToView(g.Id,this.Element);
},
"isEditable",
function()
{
	var attr="";
	if(this.Node)
		attr=this.Node.getAttribute("lit:allowedit");
	else if(this.Element)
		attr=this.Element.getAttribute("allowedit");
	if(attr=="yes")
		return true;
	if(attr=="no")
		return false;
	return igtbl_getAllowUpdate(this.Row.gridId,this.Column.Band.Index,this.Column.Index)==1;
},
"renderActive",
function(render)
{
	var g=this.Row.Band.Grid;
	if(!g.Activation.AllowActivation || !this.Element)
		return;
	var e=this.Element;
	if(this.scrElem)
		e=this.scrElem;
	if(!(ig_csom.IsNetscape6 || ig_csom.IsNetscape))
		igtbl_changeBorder(g,e.currentStyle,e.runtimeStyle,this,"",render);
	else
	{
		this.renderActiveLeft(render);
		this.renderActiveTop(render);
		this.renderActiveRight(render);
		this.renderActiveBottom(render);
	}
},
"renderActiveLeft",
function(render)
{
	var g=this.Row.Band.Grid;
	if(!g.Activation.AllowActivation || !this.Element)
		return;
	var e=this.Element;
	if(this.scrElem)
		e=this.scrElem;
	var styleTS=e.style;
	if(!(ig_csom.IsNetscape6 || ig_csom.IsNetscape))
	{
		styleTS=e.runtimeStyle;
		igtbl_changeBorder(g,e.currentStyle,styleTS,this,"Left",render);
	}
	else
		igtbl_changeBorder(g,styleTS,styleTS,this,"Left",render);
	if(render==false && !(ig_csom.IsNetscape6 || ig_csom.IsNetscape) && styleTS.cssText.length>0)
		styleTS.cssText=styleTS.cssText.replace(/BORDER-LEFT/g,"");

},
"renderActiveTop",
function(render)
{
	var g=this.Row.Band.Grid;
	if(!g.Activation.AllowActivation || !this.Element)
		return;
	var e=this.Element;
	if(this.scrElem)
		e=this.scrElem;
	var styleTS=e.style;
	if(!(ig_csom.IsNetscape6 || ig_csom.IsNetscape))
	{
		styleTS=e.runtimeStyle;
		igtbl_changeBorder(g,e.currentStyle,styleTS,this,"Top",render);
	}
	else
		igtbl_changeBorder(g,styleTS,styleTS,this,"Top",render);
	igtbl_changeBorder(g,styleTS,this,"Top",render);
	if(render==false && !(ig_csom.IsNetscape6 || ig_csom.IsNetscape) && styleTS.cssText.length>0)
		styleTS.cssText=styleTS.cssText.replace(/BORDER-TOP/g,"");
},
"renderActiveRight",
function(render)
{
	var g=this.Row.Band.Grid;
	if(!g.Activation.AllowActivation || !this.Element)
		return;
	var e=this.Element;
	if(this.scrElem)
		e=this.scrElem;
	var styleTS=e.style;
	if(!(ig_csom.IsNetscape6 || ig_csom.IsNetscape))
	{
		styleTS=e.runtimeStyle;
		igtbl_changeBorder(g,e.currentStyle,styleTS,this,"Right",render);
	}
	else
		igtbl_changeBorder(g,styleTS,styleTS,this,"Right",render);
	igtbl_changeBorder(g,styleTS,this,"Right",render);
	if(render==false && !(ig_csom.IsNetscape6 || ig_csom.IsNetscape) && styleTS.cssText.length>0)
		styleTS.cssText=styleTS.cssText.replace(/BORDER-RIGHT/g,"");
},
"renderActiveBottom",
function(render)
{
	var g=this.Row.Band.Grid;
	if(!g.Activation.AllowActivation || !this.Element)
		return;
	var e=this.Element;
	if(this.scrElem)
		e=this.scrElem;
	var styleTS=e.style;
	if(!(ig_csom.IsNetscape6 || ig_csom.IsNetscape))
	{
		styleTS=e.runtimeStyle;
		igtbl_changeBorder(g,e.currentStyle,styleTS,this,"Bottom",render);
	}
	else
		igtbl_changeBorder(g,styleTS,styleTS,this,"Bottom",render);
	igtbl_changeBorder(g,styleTS,this,"Bottom",render);
	if(render==false && !(ig_csom.IsNetscape6 || ig_csom.IsNetscape) && styleTS.cssText.length>0)
		styleTS.cssText=styleTS.cssText.replace(/BORDER-BOTTOM/g,"");
},
"getLevel",
function(s)
{
	var l=this.Row.getLevel();
	l[l.length]=this.Column.Index;
	if(s)
	{
		s=l.join("_");
		igtbl_dispose(l);
		delete l;
		return s;
	}
	return l;
},
"selectCell",
function(selFlag)
{
	var e=this.Element;
	if(!e)
		return;
	var className=null;
	if(selFlag!=false)
		className=this.Column.getSelClass();
	if(this.scrElem)
		e=this.scrElem;
	igtbl_changeStyle(this.Row.gridId,e,className);
},
"select",
function(selFlag,fireEvent)
{
	var gs=this.Column.Band.Grid;
	var gn=gs.Id;
	var cellID=this.Element.id;
	if(gs.exitEditCancel || gs.noCellChange)
		return;
	if(this.Band.getSelectTypeCell()<2)
		return;
	if(igtbl_fireEvent(gn,gs.Events.BeforeSelectChange,"(\""+gn+"\",\""+cellID+"\")")==true)
		return;
	if(selFlag!=false)
	{
		this.selectCell();
		gs.recordChange("SelectedCells",this);
		if(!gs.SelectedCellsRows[this.Element.parentNode.id])
			gs.SelectedCellsRows[this.Element.parentNode.id]=[];
		gs.SelectedCellsRows[this.Element.parentNode.id][cellID]=true;
	}
	else
	{
		if(gs.SelectedCells[cellID])
		{
			gs.removeChange("SelectedCells",this);
			var scr=gs.SelectedCellsRows[this.Element.parentNode.id];
			if(scr && scr[cellID])
				delete scr[cellID];
		}
		if(igtbl_getLength(gs.SelectedCellsRows[this.Element.parentNode.id])==0)
			delete gs.SelectedCellsRows[this.Element.parentNode.id];
		if(!this.Column.Selected && !this.Row.getSelected())
			this.selectCell(false);
	}
	if(this==gs.oActiveCell)
		this.renderActive();
	if(fireEvent!=false)
	{
		igtbl_fireEvent(gn,gs.Events.AfterSelectChange,"(\""+gn+"\",\""+cellID+"\");");
		if(gs.NeedPostBack)
			igtbl_moveBackPostField(gn,"SelectedCells");
	}	
},
"getOldValue",
function()
{
	return this._oldValue;
},
"getTargetURL",
function()
{
	var url=null;
	if(this.Node && (url=this.Node.getAttribute("targetURL")))
		return url;
	if(this.Element && (url=this.Element.getAttribute("targetURL")))
		return url;
	if(this.Column.ColumnType==9)
		return this.getValue();
	return url;
},
"setTargetURL",
function(url)
{
	if(this.Node && this.Node.getAttribute("targetURL"))
		this.Node.setAttribute("targetURL",url);
	if(this.Element && this.Element.getAttribute("targetURL"))
		this.Element.setAttribute("targetURL",url);
	var urls=igtbl_splitUrl(url);
	var el=this.Element;
	if(el)
	{
		if(el.firstChild && el.firstChild.tagName=="NOBR")
			el=el.firstChild;
		if(el.firstChild && el.firstChild.tagName=="A")
			el=el.firstChild;
	}
	if(this.Column.ColumnType==9)
		this.setValue(urls[0]);
	if(el && el.tagName=="A")
	{
		if(this.Column.ColumnType!=9)
			el.href=urls[0];
		if(urls[1])
			el.target=urls[1];
		else
			el.target="_self";
	}
	igtbl_dispose(urls);
}
];
for(var i=0;i<igtbl_ptsCell.length;i+=2)
	igtbl_Cell.prototype[igtbl_ptsCell[i]]=igtbl_ptsCell[i+1];

/* State change object */
igtbl_StateChange.prototype=new igtbl_WebObject();
igtbl_StateChange.prototype.constructor=igtbl_StateChange;
igtbl_StateChange.base=igtbl_WebObject.prototype;
function igtbl_StateChange(type,grid,obj,value)
{
	if(arguments.length>0)
		this.init(type,grid,obj,value);
}
igtbl_StateChange.prototype.init=function(type,grid,obj,value)
{
	igtbl_StateChange.base.init.apply(this,[type]);
	this.Node=ig_ClientState.addNode(grid.StateChanges,"StateChange");
	
	this.Grid=grid;
	this.Object=obj;
	ig_ClientState.setPropertyValue(this.Node,"Type",this.Type);
	if(obj.getLevel)
		ig_ClientState.setPropertyValue(this.Node,"Level",obj.getLevel(true));
	if(typeof(value)!="undefined" && value!=null)
	{
		if(value=="" && typeof(value)=="string") value="\x01";
		ig_ClientState.setPropertyValue(this.Node,"Value",value);
	}
	this.Object.Changes[this.Type]=this;
}
igtbl_StateChange.prototype.remove=function()
{
	ig_ClientState.removeNode(this.Grid.StateChanges,this.Node);
	delete this.Object.Changes[this.Type];
	this.Grid=null;
	this.Object=null;
	igtbl_dispose(this);
}
igtbl_StateChange.prototype.setFireEvent=function(value)
{
	ig_ClientState.setPropertyValue(this.Node,"FireEvent",value.toString());
}

/**********************************************************/

var igtbl_gridState=[];

var igtbl_bInsideOldOnSubmit=false;
function igtbl_submit()
{
    var retVal=true;
	if(arguments.length==0 || (ig_csom.IsNetscape ||  ig_csom.IsNetscape6) && arguments.length==1)
	{
		if(this.oldOnSubmit && !igtbl_bInsideOldOnSubmit)
		{
			igtbl_bInsideOldOnSubmit=true;
			if(arguments.length==0)
				retVal=this.oldOnSubmit();
			else
				retVal=this.oldOnSubmit(arguments[0]);
			igtbl_bInsideOldOnSubmit=false;
		}
		igtbl_updateGridsPost(this.igtblGrid);
	}
	else if(typeof(window.__doPostBackOld)!="undefined" && !igtbl_bInsideOldOnSubmit)
	{
		igtbl_updateGridsPost(window.__thisForm.igtblGrid);
		igtbl_bInsideOldOnSubmit=true;
		retVal=window.__doPostBackOld(arguments[0],arguments[1]);
		igtbl_bInsideOldOnSubmit=false;
	}
	return retVal;
}

function igtbl_formSubmit()
{
	igtbl_updateGridsPost(this.igtblGrid);
	return this.oldSubmit();
}

function igtbl_updateGridsPost(grid)
{
	if(!grid) return;
	igtbl_updateGridsPost(grid.oldIgtblGrid);
	grid.update();
}
	
function igtbl_initActivation(aa)
{
	this.AllowActivation=aa[0];
	this.BorderColor=aa[1];
	this.BorderStyle=aa[2];
	this.BorderWidth=aa[3];
	this.BorderDetails=new Object();
	var bd=this.BorderDetails;
	bd.ColorLeft=aa[4][0];
	bd.ColorTop=aa[4][1];
	bd.ColorRight=aa[4][2];
	bd.ColorBottom=aa[4][3];
	bd.StyleLeft=aa[4][4];
	bd.StyleTop=aa[4][5];
	bd.StyleRight=aa[4][6];
	bd.StyleBottom=aa[4][7];
	bd.WidthLeft=aa[4][8];
	bd.WidthTop=aa[4][9];
	bd.WidthRight=aa[4][10];
	bd.WidthBottom=aa[4][11];
	this.getValue=function(where,what)
	{
		var res="";
		if(where)
			res=this.BorderDetails[what+where];
		if(res=="" || res=="NotSet")
			res=this["Border"+what];
		return res;
	}
}
function igtbl_deleteSelRows(gn)
{
	var gs=igtbl_getGridById(gn);
	var ar=gs.getActiveRow();
	var del=false;
	if(igtbl_inEditMode(gn))
	{
		igtbl_hideEdit(gn);
		if(igtbl_inEditMode(gn))
			return;
	}
	if(gs.Node)
	{
		var arOffs=ar?ar.getIndex():0;
		gs.isDeletingSelected=true;
		var arr=igtbl_sortRowIdsByClctn(gs.SelectedRows);
		for(var i=0;i<arr.length;i++)
		{
			var row=gs.getRowByLevel(arr[i]);
			if(row.deleteRow())
			{
				if(i==arr.length-1 || arr[i].length!=arr[i+1].length || arr[i].length>1 && arr[i][arr[i].length-2]!=arr[i+1][arr[i+1].length-2])
				{
					var rows=row.OwnerCollection;
					rows.SelectedNodes=rows.Node.selectNodes("Row");
					if(!rows.SelectedNodes.length)
						rows.SelectedNodes=rows.Node.selectNodes("Group");
					rows.reIndex(row.getIndex());
					rows.repaint();
				}
			}
		}
		if(!arr.length && ar)
		{
			var rows=ar.OwnerCollection;
			ar.deleteRow()
			rows.SelectedNodes=rows.Node.selectNodes("Row");
			if(!rows.SelectedNodes.length)
				rows.SelectedNodes=rows.Node.selectNodes("Group");
			while(rows.length==0 && rows.ParentRow && rows.ParentRow.GroupByRow)
				rows=rows.ParentRow.OwnerCollection;
			rows.reIndex(arOffs);
			rows.repaint();
		}
		if(ar && !gs.getActiveRow())
		{
			var rows=ar.OwnerCollection;
			if(arOffs<rows.length)
				rows.getRow(arOffs).activate();
			else if(rows.length>0)
				rows.getRow(rows.length-1).activate();
			else if(rows.ParentRow)
				rows.ParentRow.activate();
			ar=gs.getActiveRow();
			if(ar && ar.Band.getSelectTypeRow()==2)
				ar.setSelected();
		}
		gs.isDeletingSelected=false;
		ig_dispose(arr);
		delete arr;
	}
	else
	{
		var r=null;
		if(ar && !gs.getActiveCell())
		{
			r=ar.getNextRow();
			while(r && r.getSelected())
				r=r.getNextRow();
			if(!r)
			{
				r=ar.getPrevRow();
				while(r && r.getSelected())
					r=r.getPrevRow();
			}
			if(!r)
				r=ar.ParentRow;
		}
		for(var rowId in gs.SelectedRows)
		{
			if(gs.SelectedRows[rowId])
			{
				var row=igtbl_getRowById(rowId);
				if(row && row.deleteRow(true))
					del=true;
			}
		}
		ar=gs.getActiveRow();
		if(!del && ar && !gs.SelectedRows[ar.Element.id])
		{
			del=ar.deleteRow(true);
			if(del) ar=null;
		}
		if(del)
		{
			if(r && igtbl_getElementById(r.Element.id))
			{
				if(r.Band.getSelectTypeRow()==2)
					r.setSelected();
				r.activate();
				ar=r;
			}
			else
				ar=null;
		}
		if(!ar)
			gs.setActiveRow(null);
	}
	gs.alignStatMargins();
	if(gs.NeedPostBack)
		igtbl_doPostBack(gn);
}

function igtbl_deleteRow(gn,rowId)
{
	var row=igtbl_getRowById(rowId);
	if(!row)
		return false;
	return row.deleteRow();
}

function igtbl_gSelectArray(gn,elem,array)
{
	var gs=igtbl_getGridById(gn);
	gs.noCellChange=false;
	if(elem==0)
	{
		var oldSelCells=gs.SelectedCells;
		gs.SelectedCells=[];
		for(var i=0;i<array.length;i++)
			if(!oldSelCells[array[i]])
				igtbl_selectCell(gn,array[i]);
			else
				gs.SelectedCells[array[i]]=true;
		for(var cell in oldSelCells)
			if(!gs.SelectedCells[cell])
				igtbl_selectCell(gn,cell,false,false);
		for(var cell in oldSelCells)
			delete oldSelCells[cell];
	}
	else if(elem==1)
	{
		var oldSelRows=gs.SelectedRows;
		gs.SelectedRows=[];
		for(var i=0;i<array.length;i++)
			if(!oldSelRows[array[i]])
				igtbl_selectRow(gn,array[i]);
			else
				gs.SelectedRows[array[i]]=true;
		for(var row in oldSelRows)
			if(!gs.SelectedRows[row])
				igtbl_selectRow(gn,row,false,false);
		for(var row in oldSelRows)
			delete oldSelRows[row];
	}
	else
	{
		var oldSelCols=gs.SelectedColumns;
		gs.SelectedColumns=[];
		for(var i=0;i<array.length;i++)
			if(!oldSelCols[array[i]])
				igtbl_selectColumn(gn,array[i]);
			else
				gs.SelectedColumns[array[i]]=true;
		for(var col in oldSelCols)
			if(!gs.SelectedColumns[col])
				igtbl_selectColumn(gn,col,false,false);
		for(var col in oldSelCols)
			delete oldSelCols[col];
	}
}

function igtbl_expandEffects(values)
{
	this.Delay=values[0];
	this.Duration=values[1];
	this.Opacity=values[2];
	this.ShadowColor=values[3];
	this.ShadowWidth=values[4];
	this.EffectType=values[5];
}

function igtbl_hideColumn(rows,col,hide)
{
	var g=col.Band.Grid;
	igtbl_lineupHeaders(col.Id,col.Band);
	if(col.Band.Index==rows.Band.Index && rows.Element.previousSibling)
	{
		var tBody=rows.Element.previousSibling;
		var realIndex=-1;
		for(var i=0;i<tBody.childNodes[0].cells.length;i++)
		{
			var c=tBody.childNodes[0].cells[i];
			if(c.style.display=="")
				realIndex++;
			if(c.id==col.Id)
			{
				var h=(hide?"none":"");
				if(c.style.display==h)
					return;
				c.style.display=h;
				if(tBody.nextSibling.nextSibling)
					tBody.nextSibling.nextSibling.childNodes[0].childNodes[i].style.display=(hide?"none":"");
				var chn=tBody.previousSibling.childNodes;
				if(hide)
				{
					var ch=chn[realIndex];
					col.Width=ch.width;
					ch.parentNode.appendChild(ch);
					ch.width=1;
					ch.style.display="none";
					if(tBody.nextSibling.nextSibling)
						tBody.nextSibling.nextSibling.childNodes[0].childNodes[chn.length-1].width=col.Width;
				}
				else
				{
					var ch=chn[chn.length-1];
					if(chn[realIndex+1])
						ch.parentNode.insertBefore(ch,chn[realIndex+1])
					if(ch.style.display=="none")
						ch.style.display="";
					ch.style.cssText=col.Style;
					ch.width=col.Width;
					if(tBody.nextSibling.nextSibling)
						tBody.nextSibling.nextSibling.childNodes[0].childNodes[i].width=col.Width;
				}
				break;
			}
		}
	}
	for(var i=0;i<rows.length;i++)
	{
		var row=rows.getRow(i);
		if(col.Band.Index==rows.Band.Index && !row.GroupByRow)
		{
			var cell=row.getCellByColumn(col);
			if(hide)
			{
				cell.Element.style.display="none";
				if(col.Band.Grid.getActiveRow()==row)
				{
					if(typeof(cell.oldBorderLeftStyle)!="undefined")
					{
						cell.renderActiveLeft(false);
						for(var j=col.Index+1;j<col.Band.Columns.length;j++)
							if(col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
							{
								row.getCellByColumn(col.Band.Columns[j]).renderActiveLeft();
								break;
							}
					}
					if(typeof(cell.oldBorderRightStyle)!="undefined")
					{
						cell.renderActiveRight(false);
						for(var j=col.Index-1;j>=0;j--)
							if(col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
							{
								row.getCellByColumn(col.Band.Columns[j]).renderActiveRight();
								break;
							}
					}
				}
			}
			else
			{
				cell.Element.style.display="";
				if(col.Band.Grid.getActiveRow()==row)
				{
					var j=0;
					for(j=0;j<col.Band.Columns.length;j++)
						if(col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
							break;
					if(j>col.Index)
					{
						row.getCellByColumn(col.Band.Columns[j]).renderActiveLeft(false);
						cell.renderActiveLeft();
					}
					for(j=col.Band.Columns.length-1;j>=0;j--)
						if(col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
							break;
					if(j<col.Index)
					{
						row.getCellByColumn(col.Band.Columns[j]).renderActiveRight(false);
						cell.renderActiveRight();
					}
				}
			}
		}
		else if(col.Band.Index>=rows.Band.Index && row.Expandable)
		{
			if(row.GroupByRow || col.Band.Index>rows.Band.Index)
				igtbl_hideColumn(row.Rows,col,hide);
		}
	}
}

function igtbl_initGroupByBox(grid)
{
	this.Element=igtbl_getElementById(grid.Id+"_groupBox");
	this.pimgUp=igtbl_getElementById(grid.Id+"_pimgUp");
	if(this.pimgUp)
		this.pimgUp.style.zIndex=10000;
	this.pimgDn=igtbl_getElementById(grid.Id+"_pimgDn");
	if(this.pimgDn)
		this.pimgDn.style.zIndex=10000;
	this.postString="";
	this.moveString="";
	if(this.Element)
	{
		this.groups=new Array();
		var gt=this.Element.childNodes[0];
		if(gt.tagName=="TABLE")
			for(var i=0;i<gt.rows.length;i++)
				this.groups[i]=new igtbl_initGroupMember(gt.rows[i].cells[i]);
	}
}

function igtbl_initGroupMember(e)
{
	var d=e.childNodes[0];
	if(!d.getAttribute("groupInfo"))
		return null;
	this.Element=d;
	this.groupInfo=d.getAttribute("groupInfo").split(":");
	this.groupInfo[1]=parseInt(this.groupInfo[1],10);
	if(this.groupInfo[0]=="col")
		this.groupInfo[2]=parseInt(this.groupInfo[2],10);
}

function igtbl_initStatHeader(gs)
{
	this.Type="statHeader";

	this.gridId=gs.Id;
	this.Element=gs.Element.parentNode.parentNode.parentNode.previousSibling.childNodes[0].childNodes[0].childNodes[0].childNodes[0];
	this.Element.parentNode.parentNode.style.height=this.Element.parentNode.offsetHeight;
	var j=0;
	var comWidth=0;
	for(var i=0;i<this.Element.childNodes[0].childNodes.length;i++)
	{
		var col=this.Element.childNodes[0].childNodes[i];
		if(col.style.display=="" && gs.Element.childNodes[0].childNodes[j].offsetWidth>0)
		{
			var colW=gs.Element.childNodes[0].childNodes[j].offsetWidth;
			col.style.width=colW;
			comWidth+=colW;
		}
		if(col.getAttribute("columnNo"))
		{
			var colNo=parseInt(col.getAttribute("columnNo"));
			gs.Bands[0].Columns[colNo].Element=col;
			if(gs.Bands[0].Columns[colNo].getVisible())
				j++;
		}
		else
			j++;
	}
	this.Element.parentNode.style.width=comWidth;
	this.ScrollTo=igtbl_scrollStatHeader;
	this.getElementByColumn=igtbl_shGetElemByCol;
}

function igtbl_scrollStatHeader(scrollLeft)
{
	var gs=igtbl_getGridById(this.gridId);
	this.Element.parentNode.style.left=-scrollLeft;
	var el=gs.StatHeader.Element.childNodes[0];
	var j=0;
	var comWidth=0;
	for(var i=0;i<el.childNodes.length;i++)
	{
		var col=el.childNodes[i];
		if(col.style.display=="")
		{
			var colW=gs.Element.childNodes[0].childNodes[j].offsetWidth;
			if(col.offsetWidth!=colW)
				col.style.width=colW;
			comWidth+=colW;
			j++;
		}else if(gs.Element.childNodes[0].childNodes[i].getAttribute("scrolledOutOfView"))j++;
	}
	this.Element.parentNode.style.width=comWidth;
}

function igtbl_shGetElemByCol(col)
{
	if(!col.hasCells())
		return null;
	var j=0;
	for(var i=0;i<col.Index;i++)
	{
		if(!col.Band.Columns[i].IsGroupBy)
			j++;
	}
	return this.Element.childNodes[0].childNodes[j+col.Band.firstActiveCell];
}

function igtbl_initStatFooter(gs)
{
	this.Type="statFooter";

	this.gridId=gs.Id;
	this.Element=gs.Element.parentNode.parentNode.parentNode.nextSibling.childNodes[0].childNodes[0].childNodes[0].childNodes[0];
	this.Element.parentNode.parentNode.style.height=this.Element.parentNode.offsetHeight;
	var j=0;
	var comWidth=0;
	for(var i=0;i<this.Element.childNodes[0].childNodes.length;i++)
	{
		var col=this.Element.childNodes[0].childNodes[i];
		var colW=0;
		if(col.style.display=="")
		{
			colW=gs.Element.childNodes[0].childNodes[j++].offsetWidth;
			col.style.width=colW;
			comWidth+=colW;
		}
	}
	this.Element.parentNode.style.width=comWidth;
	this.ScrollTo=igtbl_scrollStatFooter;
	this.Resize=igtbl_resizeStatFooter;
	this.getElementByColumn=igtbl_sfGetElemByCol;
}

function igtbl_scrollStatFooter(scrollLeft)
{
	var gs=igtbl_getGridById(this.gridId);
	this.Element.parentNode.style.left=-scrollLeft;
	var el=gs.StatFooter.Element.childNodes[0];
	var j=0;
	var comWidth=0;
	for(var i=0;i<el.childNodes.length;i++)
	{
		var col=el.childNodes[i];
		if(col.style.display=="")
		{
			var colW=gs.Element.childNodes[0].childNodes[j].offsetWidth;
			if(col.offsetWidth!=colW)
				col.style.width=colW;
			comWidth+=colW;
			j++;
		}else if(gs.Element.childNodes[0].childNodes[i].getAttribute("scrolledOutOfView"))j++;
	}
	this.Element.parentNode.style.width=comWidth;
}

function igtbl_resizeStatFooter(index,width)
{
	var gs=igtbl_getGridById(this.gridId);
	var el=igtbl_getElemVis(gs.StatFooter.Element.childNodes[0].childNodes,index);
	this.Element.parentNode.style.width=this.Element.parentNode.offsetWidth+(width-el.offsetWidth);
	el.style.width=width;
}

function igtbl_sfGetElemByCol(col)
{
	if(!col.hasCells())
		return null;
	var j=0;
	for(var i=0;i<col.Index;i++)
	{
		if(col.Band.Columns[i].hasCells())
			j++;
	}
	return this.Element.childNodes[0].childNodes[j+col.Band.firstActiveCell];
}

function igtbl_rowGetValue(colId)
{
	
}

function igtbl_resetJustAssigned()
{
	igtbl_justAssigned=false;
}

function igtbl_fillEditTemplate(row,childNodes)
{
	for(var i=childNodes.length-1;i>=0;i--)
	{
		var el=childNodes[i];
		if(!el.getAttribute)
			continue;
		var colKey=el.getAttribute("columnKey");
		var column=row.Band.getColumnFromKey(colKey);
		if(column)
		{
			var cell=row.getCellByColumn(column);
			if(!cell)
			{
				if(!el.isDisabled)
				{
					el.setAttribute("disabledBefore",true);
					el.disabled=true;
				}
				el.value="";
				continue;
			}
			else if(el.isDisabled && el.getAttribute("disabledBefore"))
			{
				el.disabled=false;
				el.removeAttribute("disabledBefore");
			}
			var cellValue=cell.getValue();
			var cellText="";
			var nullText="";
			if(cellValue==null)
			{
				nullText=cell.Column.getNullText();
				cellText=nullText;
			}
			else
				cellText=cellValue.toString();
			var ect=cellText.replace(/\r\n/g,"\\r\\n");
			ect=ect.replace(/\"/g,"\\\"");
			var s="(\""+row.gridId+"\",\""+el.id+"\",\""+(cell.Element?cell.Element.id:"")+"\",\""+ect+"\")";
			if(!igtbl_fireEvent(row.gridId,igtbl_getGridById(row.gridId).Events.TemplateUpdateControls,s))
			{
				if(el.tagName=="SELECT")
				{
					for(var j=0;j<el.childNodes.length;j++)
						if(el.childNodes[j].tagName=="OPTION")
							if(el.childNodes[j].value==cellText)
							{
								el.childNodes[j].selected=true;
								break;
							}
				}
				else if(el.tagName=="INPUT" && el.type=="checkbox")
				{
					if(!cellValue || cellText.toLowerCase()=="false")
						el.checked=false;
					else
						el.checked=true;
				}
				else if(el.tagName=="DIV" || el.tagName=="SPAN")
				{
					for(var j=0;j<el.childNodes.length;j++)
					{
						if(el.childNodes[j].tagName=="INPUT" && el.childNodes[j].type=="radio")
							if(el.childNodes[j].value==cellText)
							{
								el.childNodes[j].checked=true;
								break;
							}
					}
				}
				else
					el.value=cellText;
				if(!el.isDisabled)
					igtbl_focusedElement=el;
			}
		}
		else if(el.childNodes && el.childNodes.length>0)
			igtbl_fillEditTemplate(row,el.childNodes);
	}
}

function igtbl_unloadEditTemplate(row,childNodes)
{
	for(var i=0;i<childNodes.length;i++)
	{
		var el=childNodes[i];
		if(!el.getAttribute)
			continue;
		var colKey=el.getAttribute("columnKey");
		var column=row.Band.getColumnFromKey(colKey);
		if(column)
		{
			var cell=row.getCellByColumn(column);
			if(cell && !igtbl_fireEvent(row.gridId,igtbl_getGridById(row.gridId).Events.TemplateUpdateCells,"(\""+row.gridId+"\",\""+el.id+"\",\""+(cell.Element?cell.Element.id:"")+"\")"))
			{
				if(cell.isEditable() || cell.Column.getAllowUpdate()==3)
				{
					if(el.tagName=="SELECT")
						cell.setValue(el.options[el.selectedIndex].value);
					else if(el.tagName=="INPUT" && el.type=="checkbox")
						cell.setValue(el.checked);
					else if(el.tagName=="DIV" || el.tagName=="SPAN")
					{
						for(var j=0;j<el.childNodes.length;j++)
						{
							if(el.childNodes[j].tagName=="INPUT" && el.childNodes[j].type=="radio")
								if(el.childNodes[j].checked)
								{
									cell.setValue(el.childNodes[j].value);
									break;
								}
						}
					}
					else if(typeof(el.value)!="undefined")
						cell.setValue(el.value);
				}
			}
		}
		else if(el.childNodes && el.childNodes.length>0)
			igtbl_unloadEditTemplate(row,el.childNodes);
	}
}

function igtbl_gRowEditMouseDown(evnt)
{
	if(igtbl_justAssigned)
	{
		igtbl_justAssigned=false;
		return;
	}
	if(!evnt)
		evnt=event;
	var src=igtbl_srcElement(evnt);
	var editTempl=igtbl_getElementById(igtbl_currentEditTempl);
	if(editTempl && !igtbl_contains(editTempl,src))
	{
		var rId=editTempl.getAttribute("editRow");
		var row=igtbl_getRowById(rId);
		row.endEditRow();
	}
}

function igtbl_contains(e1,e2)
{
	if(e1.contains)
		return e1.contains(e2);
	var contains=false;
	var p=e2;
	while(p && p!=e1)
		p=p.parentNode;
	return p==e1;
}

function igtbl_gRowEditButtonClick(evnt,saveChanges)
{
	if(!evnt)
		evnt=event;
	var src=igtbl_srcElement(evnt);
	var editTempl=igtbl_getElementById(igtbl_currentEditTempl);
	if(editTempl)
	{
		if(typeof(saveChanges)=="undefined")
			saveChanges=(src.id.substring(src.id.length-13)=="igtbl_reOkBtn");
		var rId=editTempl.getAttribute("editRow");
		var row=igtbl_getRowById(rId);
		row.endEditRow(saveChanges);
	}
}

function igtbl_changeBorder(g,elemFrom,elemTo,obj,attr,render)
{
	var attrStyle;
	if(attr)
		attrStyle="border"+attr+"Style";
	else
		attrStyle="borderStyle";
	var attrColor;
	if(attr)
		attrColor="border"+attr+"Color";
	else
		attrColor="borderColor";
	var attrWidth;
	if(attr)
		attrWidth="border"+attr+"Width";
	else
		attrWidth="borderWidth";
	if(render==false)
	{
		if(typeof(obj["old"+attrStyle])!="undefined")
		{
			elemTo[attrStyle]=obj["old"+attrStyle];
			delete obj["old"+attrStyle];
		}
		if(typeof(obj["old"+attrColor])!="undefined")
		{
			elemTo[attrColor]=obj["old"+attrColor];
			delete obj["old"+attrColor];
		}
		if(typeof(obj["old"+attrWidth])!="undefined")
		{
			elemTo[attrWidth]=obj["old"+attrWidth];
			delete obj["old"+attrWidth];
		}
	}
	else
	{
		if(typeof(obj["old"+attrStyle])=="undefined")
			obj["old"+attrStyle]=elemFrom[attrStyle];
		elemTo[attrStyle]=g.Activation.getValue(attr,"Style");
		if(typeof(obj["old"+attrColor])=="undefined")
			obj["old"+attrColor]=elemFrom[attrColor];
		elemTo[attrColor]=g.Activation.getValue(attr,"Color");
		if(typeof(obj["old"+attrWidth])=="undefined")
			obj["old"+attrWidth]=elemFrom[attrWidth];
		elemTo[attrWidth]=g.Activation.getValue(attr,"Width");
	}
}

function igtbl_valueFromString(value,dataType)
{
	if(typeof(value)=="undefined" || value==null)
		return value;
	switch(dataType)
	{
		case 2: // integer
		case 3:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 21:
			if(typeof(value)=="number")
				return value;
			if(typeof(value)=="boolean")
				return (value?1:0);
			value=parseInt(value.toString(),10);
			if(value.toString()=="NaN")
				value=0;
			break;
		case 4: // float
		case 5:
		case 14:
			if(typeof(value)=="float")
				return value;
			value=parseFloat(value.toString());
			if(value.toString()=="NaN")
				value=0.0;
			break;
		case 11: // boolean
			if(!value || value.toString()=="0" || value.toString().toLowerCase()=="false")
				value=false;
			else
				value=true;
			break;
		case 7: // datetime
			var d=new Date(value);
			if(d.toString()!="NaN" && d.toString()!="Invalid Date")
				value=d;
			else
				value=igtbl_trim(value.toString());
			delete d;
			break;
		case 8:
			break;
		default:
			value=igtbl_trim(value.toString());
	}
	return value;
}

function igtbl_clearRowChanges(gs,row)
{
	if(!row)return;
	if(gs.SelectedRows[row.Element.id])
		gs.removeChange("SelectedRows",row);
	if(gs.SelectedCellsRows[row.Element.id])
	{
		for(var cell in gs.SelectedCellsRows[row.Element.id])
		{
			gs.removeChange("SelectedCells",igtbl_getCellById(cell));
			delete gs.SelectedCellsRows[row.Element.id][cell];
		}
		delete gs.SelectedCellsRows[row.Element.id];
	}
	if(gs.ChangedRows[row.Element.id])
	{
		for(var cell in gs.ChangedRows[row.Element.id])
		{
			gs.removeChange("ChangedCells",igtbl_getCellById(cell));
			delete gs.ChangedRows[row.Element.id][cell];
		}
		delete gs.ChangedRows[row.Element.id];
	}
	if(gs.ResizedRows[row.Element.id])
		gs.removeChange("ResizedRows",row);
	if(gs.ExpandedRows[row.Element.id])
		gs.removeChange("ExpandedRows",row);
	if(gs.CollapsedRows[row.Element.id])
		gs.removeChange("CollapsedRows",row);
	if(typeof(gs.AddedRows[row.Element.id])!="undefined")
		row.Changes["AddedRows"].setFireEvent(false);
		//gs.removeChange("AddedRows",row);
}

function igtbl_setColVisible(c,display,width,realColNo)
{
	if(!c) return;
	if(c.length)
	{
		for(var i=0;i<c.length;i++)
			igtbl_setColVisible(c[i],display,width,realColNo);
		return;
	}
	var cg;
	if(i==0 && !this.Bands[i].IsGrouped && (this.StationaryMargins==1 || this.StationaryMargins==3))
		cg=this.Element.childNodes[0];
	else
		cg=c.parentNode.parentNode.previousSibling;
	var style;
	if(cg)
		style=cg.childNodes[realColNo].style;
	else
		style=c.style;
	if(style.display==display && parseInt(style.width,10)==width)
		return;
	if(style.display!=display)
	{
		if(cg)
			cg.childNodes[realColNo].setAttribute("scrolledOutOfView",(display=="none"?"true":"false"));
		style.display=display;
		if(!ig_csom.IsIE)
		{
			c.style.display=display;
			var cells=igtbl_getCellsByColumn(id);
			for(var i=0;i<cells.length;i++)
				cells[i].style.display=display;
			var tfoot=c.parentNode.parentNode.nextSibling.nextSibling;
			if(tfoot)
				tfoot.firstChild.cells[c.cellIndex].style.display=display;
		}
	}
	if(ig_csom.IsIE)
		style.width=width;
}

function igtbl_getStyleSheet(name)
{
	for(var i=0;i<document.styleSheets.length;i++)
		for(var j=0;j<document.styleSheets[i].rules.length;j++)
			if(document.styleSheets[i].rules[j].selectorText==name)
				return document.styleSheets[i].rules[j].style;
	return null;
}

function igtbl_swapCells(rows,bandNo,index,toIndex)
{
	if(!rows || rows.Band.Index>bandNo)
		return;
	for(var i=0;i<rows.rows.length;i++)
	{
		var row=rows.rows[i];
		if(row)
		{
			if(!row.GroupByRow && row.Band.Index==bandNo && row.cells)
			{
				var cell=row.cells[index];
				row.cells[index]=row.cells[toIndex];
				row.cells[toIndex]=cell;
			}
			igtbl_swapCells(row.Rows,bandNo,index,toIndex);
		}
	}
}

function igtbl_getCellsByColumn(columnId)
{
	var c=igtbl_getDocumentElement(columnId);
	if(!c) return;
	if(!c.length) c=[c];
	var cells=[];
	var colSplit=columnId.split("_");
	var colIndex=parseInt(colSplit[colSplit.length-1],10);
	for(var k=0;k<c.length;k++)
	{
		var tbody=c[k].parentNode;
		while(tbody && tbody.tagName!="THEAD" && tbody.tagName!="TABLE")
			tbody=tbody.parentNode;
		if(!tbody || tbody.tagName=="TABLE")
			continue;
		tbody=tbody.nextSibling;
		for(var i=0;i<tbody.rows.length;i++)
		{
			if(tbody.rows[i].getAttribute("hiddenRow"))
				continue;
			var cell=tbody.rows[i].cells[c[k].cellIndex];
			while(cell)
			{
				var cellSplit=cell.id.split("_");
				var cellIndex=parseInt(cellSplit[cellSplit.length-1],10);
				if(cellIndex==colIndex)
					break;
				cell=cell.nextSibling;
			}
			if(cell)
				cells[cells.length]=cell;
		}
	}
	return cells;
}

function igtbl_getArray(elem)
{
	if(!elem) return null;
	var a=new Array();
	if(!elem.length)
		a[0]=elem;
	else
		for(var i=0;i<elem.length;i++)
			a[i]=elem[i];
	return a;
}

function igtbl_AdjustCheckboxDisabledState(column,bandIndex,rows,value)
{
	if(!rows)return;
	if (rows.Band.Index==bandIndex)
		for (var i=0;i<rows.length;i++)
		{
			var oC=rows.getRow(i).getCellByColumn(column);
			oC=igtbl_getCheckboxFromElement(oC.Element);
			if(oC)oC.disabled=!(1==value);
		}
	else if (rows.Band.Index < bandIndex) // if the rows collection is deeper then then band we modified, don't bother going deeper
		for (var i=0;i<rows.length;i++) igtbl_AdjustCheckboxDisabledState(column, bandIndex,rows.getRow(i).Rows,value);
}

function igtbl_getCheckboxFromElement(oCellE)
{
	var oChk=null;
	for(var i=0;i<oCellE.children.length;i++)
	{
		if (oCellE.children[i].tagName=="INPUT"&&oCellE.children[i].type=="checkbox")
			oChk=oCellE.children[i];
		else
			oChk=igtbl_getCheckboxFromElement(oCellE.children[i])		
		if(oChk)break;
	}
	return oChk;
}

function igtbl_escape(text)
{
	text=escape(text);
	return text.replace(/\+/g,"%2B");
}

function igtbl_RecalculateRowNumbers(rc,startingIndex,band,xmlNode)
{
	if(rc==null&&band==null) return startingIndex;
	
	var oRow;
	var iRowLbl=-1;
	var oFAC;		
	var returnedIndex = -1;
	var workingIndex;
	var oBand = band ? band : rc.Band;

	switch(oBand.AllowRowNumbering)
	{
		case(2)://continuous
			workingIndex=startingIndex;
			break;
		case(3)://dataisland
			workingIndex=1;
			break;		
		case(4)://bybandlevel
			workingIndex=oBand._currentRowNumber+1;
			break;		
	}	

	if(null!=rc) 
	{
		for(var i=0;i<rc.length;i++)
		{
			iRowLbl = -1;
			oRow = rc.getRow(i);
			//oRS = oRow.Band.firstActiveCell - 1;
						
			if (oRow.Band.AllowRowNumbering>=2)
				iRowLbl=oRow._setRowNumber(workingIndex);
				
			if (iRowLbl>-1)
			{
				var childRows = oRow.Rows;
				var childBand = childRows ? childRows.Band : oRow.Band.Grid.Bands[oRow.Band.Index+1];
				var childXmlNode = childRows ? childRows.Node : (oRow.Node ? oRow.Node.selectSingleNode("Rows") : null);
				returnedIndex=igtbl_RecalculateRowNumbers(childRows,workingIndex+1,childBand,childXmlNode);
			}	
		
			switch(rc.Band.AllowRowNumbering)
			{
				case(2)://continuous
					workingIndex=returnedIndex;
					break;
				case(3)://dataisland
					workingIndex=++workingIndex;
					break;		
				case(4)://bybandlevel
					oRow.Band._currentRowNumber=workingIndex;
					workingIndex=++workingIndex;
					break;		
			}									
		}
	}
	else if (band!=null&&xmlNode!=null)
	{
		var oXmlRows = xmlNode.selectNodes("Row");
		for(var i=0;i<oXmlRows.length;i++)
		{
			iRowLbl = -1;
			oRow = oXmlRows[i];
			//oRS = band.firstActiveCell - 1;
						
			if (band.AllowRowNumbering>=2)			
				oRow.setAttribute("lit:rowNumber",workingIndex);
				
			var childRows = null;
			var childBand = band.Grid.Bands[band.Index+1];
			var childXmlNode = oRow.selectSingleNode("Rows");
			returnedIndex=igtbl_RecalculateRowNumbers(childRows,workingIndex+1,childBand,childXmlNode);
			
			switch(band.AllowRowNumbering)
			{
				case(2)://continuous
					workingIndex=returnedIndex;
					break;
				case(3)://dataisland
					workingIndex=++workingIndex;
					break;		
				case(4)://bybandlevel
					band._currentRowNumber=workingIndex;
					workingIndex=++workingIndex;
					break;		
			}											
		}		
	}
	return workingIndex;
}
