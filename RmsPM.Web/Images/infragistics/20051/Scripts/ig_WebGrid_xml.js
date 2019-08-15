/* 
Infragistics UltraWebGrid Script 
Version 5.1.20051.37
Copyright (c) 2001-2005 Infragistics, Inc. All Rights Reserved.
*/

function igtbl_onReadyStateChange()
{
	var gn;
	if(arguments.length==1)
		gn=arguments[0];
	else
		gn=arguments[1];
	if(typeof(gn)!="string")
		gn=gn.target.igtbl_currentGrid;
	var g=igtbl_getGridById(gn);
	var r=g.RowToQuery;
	if(g.CallBack || g.XmlHttp.readyState==4)
	{
		g.responseText="";
		if(arguments.length>1)
			g.responseText=arguments[0];
		else if(g.XmlHttp)
			g.responseText=g.XmlHttp.responseText;
		var xmlRespObj = new Object();
		g.XmlResponseObject=xmlRespObj;
		xmlRespObj.ResponseStatus=g.eError.Ok;
		xmlRespObj.ReqType=g.ReqType;
		if(g.responseText=="")
			xmlRespObj.ResponseStatus=g.eError.LoadFailed;
		else if(ig_csom.IsIE)
		{
			var start=g.responseText.indexOf("<xml");
			var end=g.responseText.indexOf("</xml>")+6;
			g.XmlResp.loadXML(g.responseText.substr(start,end-start));
			var node=g.XmlResp.selectSingleNode("xml/UltraWebGrid/XmlHTTPResponse");
			if(node)
			{
				xmlRespObj.StatusMessage = unescape(node.selectSingleNode("StatusMessage").text);
				xmlRespObj.Tag = unescape(node.selectSingleNode("Tag").text);
				xmlRespObj.XmlResp=g.XmlResp;
				if(node.getAttribute("ResponseStatus")!=0)
					xmlRespObj.ResponseStatus=g.eError.LoadFailed;
			}
			else
			{
				xmlRespObj.StatusMessage=g.responseText;
				xmlRespObj.ResponseStatus=g.eError.LoadFailed;
			}
		}
		if(g.fireEvent(g.Events.XmlHTTPResponse,[g.Id,r?r.Element.id:"",g.XmlResponseObject]) || xmlRespObj.ResponseStatus==g.eError.LoadFailed)
		{
			if(g.Events.XmlHTTPResponse[1]==1)
				g.NeedPostBack=false;
			g.ReadyState=g.eReadyState.Ready;
			g.Error=g.eError.LoadFailed;
			return;
		}
		if(g.Events.XmlHTTPResponse[1]==1)
			g.NeedPostBack=false;
		switch(g.ReqType)
		{
			case g.eReqType.ChildRows:
				igtbl_requestChildRowsComplete(gn);
				break;
			case g.eReqType.MoreRows:
				igtbl_requestMoreRowsComplete(gn);
				break;
			case g.eReqType.Sort:
				igtbl_requestSortComplete(gn);
				break;
			case g.eReqType.UpdateRow:
				igtbl_requestUpdateRowComplete(gn);
				break;
			default:
				igtbl_requestComplete(gn);
				break;
		}
		xmlRespObj.XmlResp=null;
		g.XmlResponseObject=null;
		igtbl_dispose(xmlRespObj);
		g.ReqType=g.eReqType.None;
		g.ReadyState=g.eReadyState.Ready;
		g.Error=g.eError.Ok;
		g.RowToQuery=null;
	}
}

function igtbl_requestChildRowsComplete(gn)
{
	var g=igtbl_getGridById(gn);
	var r=g.RowToQuery;
	if(!ig_csom.IsIE)
	{
		g.innerObj.innerHTML=g.responseText.substring(0);
		var rows=g.innerObj.getElementsByTagName("tr");
		var i=0,row=rows[i];
		while(row && row.id!=r.Element.id)
			row=rows[++i];
		if(row && row.nextSibling)
		{
			if(r.Element.nextSibling)
				r.Element.parentNode.insertBefore(row.nextSibling,r.Element.nextSibling);
			else
				r.Element.parentNode.appendChild(row.nextSibling);
			r.HiddenElement=r.Element.nextSibling;
			r.ChildRowsCount=igtbl_rowsCount(igtbl_getChildRows(gn,r.Element));
			r.VisChildRowsCount=igtbl_visRowsCount(igtbl_getChildRows(gn,r.Element));
			r.Rows=new igtbl_Rows(null,r.Band.Grid.Bands[r.Band.Index+1],r);
			r.FirstChildRow=r.Rows.getRow(0);
		}
	}
	else
	{
		var rowsNode=g.XmlResp.selectSingleNode("form");
		if(!rowsNode)
			rowsNode=g.XmlResp;
		rowsNode=rowsNode.selectSingleNode("xml/UltraWebGrid/Body/Rows/Row/Rows");
		for(var i=0;i<r.Band.Index && rowsNode;i++)
			rowsNode=rowsNode.selectSingleNode("Row/Rows");
		if(rowsNode==null)
			return;
		r.Node.appendChild(rowsNode);
		if(!r.Rows)
			r.Rows=new igtbl_Rows(r.Node.selectSingleNode("Rows"),r.Band.Grid.Bands[r.Band.Index+1],r);
		r.prerenderChildRows();
		r.Rows.render();
	}
}

function igtbl_onScrollXml(evnt,gn)
{
	var g=igtbl_getGridById(gn);
	var de=g.DivElement;
	if(g.scrElem)
		de=g.scrElem;
	if(de.getAttribute("noOnScroll"))
	{
		if(de.getAttribute("oldST"))
			de.scrollTop=parseInt(de.getAttribute("oldST"));
		return igtbl_cancelEvent(evnt);
	}
	if(g.noMoreRows)
		return;
	if(de && de.scrollHeight==de.scrollTop+de.clientHeight && g.RowsRange>0)
	{
		if(g.RowsServerLength>g.Rows.length)
		{
			g.invokeXmlHttpRequest(g.eReqType.MoreRows);
			return igtbl_cancelEvent(evnt);
		}
	}
}

function igtbl_cancelNoOnScroll(gn)
{
	var g=igtbl_getGridById(gn);
	var de=g.DivElement;
	if(g.scrElem)
		de=g.scrElem;
	de.removeAttribute("noOnScroll");
	de.removeAttribute("oldST");
}

function igtbl_requestMoreRowsComplete(gn)
{
	var g=igtbl_getGridById(gn);
	if(ig_csom.IsIE)
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;
		node=node.selectSingleNode("xml/UltraWebGrid/Body/Rows");
		var strTransform=g.Rows.applyXslToNode(node,g.Rows.SelectedNodes.length);
		if(strTransform)
		{
			g.innerObj.innerHTML=strTransform;
			var nodes=node.selectNodes("Row");
			g.Rows.length+=nodes.length;
			g.RowsRetrieved+=nodes.length;
			for(var i=0;i<nodes.length;i++)
			{
				g.Rows.Node.appendChild(nodes[i]);
				g.Rows.Element.appendChild(g.innerObj.firstChild.rows[0]);
			}
			g.Rows.SelectedNodes=g.Rows.Node.selectNodes("Row");
			g.alignDivs(0,true);
		}
	}
	else
	{
		g.innerObj.innerHTML=g.responseText.substring(0);
		var rows=g.innerObj.getElementsByTagName("tr");
		var i=0,row=rows[i];
		while(row && !row.id)
			row=rows[++i];
		var length=-1,pr=g.Rows.getRow(0).Element.parentNode;
		while(row)
		{
			length++;
			var ns=row.nextSibling;
			pr.appendChild(row);
			row=ns;
		}
		if(length>=0)
		{
			g.Rows.length+=length;
			g.RowsRetrieved+=length;
			g.alignDivs(0,true);
		}
	}
	g.Rows.setLastRowId(g.Rows.getRow(g.Rows.length-1).Id);
}

function igtbl_isArLess(a1,a2)
{
	if(a1.length<a2.length)
		return true;
	if(a1.length>a2.length)
		return false;
	for(var i=0;i<a1.length;i++)
	{
		if(a1[i]<a2[i])
			return true;
		if(a1[i]>a2[i])
			return false;
	}
	return false;
}

function igtbl_sortRowIdsByClctn(rc)
{
	var ar=new Array(),i=0;
	for(var rowId in rc)
		ar[i++]=rowId.split('_').slice(1);
	for(var i=0;i<ar.length;i++)
		for(var j=0;j<ar[i].length;j++)
			ar[i][j]=parseInt(ar[i][j],10);
	var sorted=false;
	while(!sorted)
	{
		sorted=true;
		for(var i=0;i<ar.length-1;i++)
			if(igtbl_isArLess(ar[i],ar[i+1]))
			{
				var a=ar[i];
				ar[i]=ar[i+1];
				ar[i+1]=a;
				sorted=false;
			}
	}
	return ar;
}

function igtbl_requestSortComplete(gn)
{
	var g=igtbl_getGridById(gn);
	if(ig_csom.IsIE)
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;
		node=node.selectSingleNode("xml/UltraWebGrid/Body/Rows");
		var rows=g.Rows;
		if(g.RowToQuery)
		{
			rows=g.RowToQuery.Rows;
			for(var i=0;i<rows.Band.Index;i++)
				node=node.selectSingleNode("Row/Rows")
		}
		rows.Node.parentNode.replaceChild(node,rows.Node);
		rows.Node=node;
		rows.SelectedNodes=node.selectNodes("Row");
		var arIndex=-1,acColumn=null,acrIndex=-1,aRows=null;
		if(g.oActiveRow && g.oActiveRow.OwnerCollection==rows)
			arIndex=g.oActiveRow.getIndex();
		if(g.oActiveRow && g.oActiveRow.Band.Index>=rows.Band.Index)
			g.setActiveRow(null);
		if(g.oActiveCell && g.oActiveCell.Row.OwnerCollection==rows)
		{
			acColumn=g.oActiveCell.Column;
			acrIndex=g.oActiveCell.Row.getIndex();
		}
		if(g.oActiveCell && g.oActiveCell.Band.Index>=rows.Band.Index)
			g.setActiveCell(null);
		rows.dispose();
		rows.length=rows.SelectedNodes.length;
		rows.render();
		if(arIndex!=-1)
			rows.getRow(arIndex).activate();
		if(acColumn)
			rows.getRow(acrIndex).getCellByColumn(acColumn).activate();
		g.RowsRetrieved=rows.length;
		if(rows.Band.Index==0)
		{
			if(g.scrElem)
			{
				g.scrElem.scrollTop=0;
				g.alignDivs();
			}
			else
				g.DivElement.scrollTop=0;
		}
	}
}

function igtbl_requestUpdateRowComplete(gn)
{
	var g=igtbl_getGridById(gn);
	var r=g.RowToQuery;
	if(ig_csom.IsIE)
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)node=g.XmlResp;
		node=node.selectSingleNode("xml/UltraWebGrid/XmlHTTPResponse");
		if (node)
		{
			var cellsNode=node.selectSingleNode("Row/Cells");
			if(cellsNode)
				for(var i=0;i<cellsNode.childNodes.length;i++)
				{
					var cell=r.getCellFromKey(unescape(cellsNode.childNodes[i].getAttribute("lit:key")));
					if(cell)
					{
						var value=unescape(cellsNode.childNodes[i].selectSingleNode("Value").text);
						var oldValue=unescape(cell.Node.selectSingleNode("Value").text);
						if(value!=oldValue)
							cell.setValue(value,false);
						if(typeof(cell._oldValue)!="undefined")
							delete cell._oldValue;
					}
				}
		}
	}
	g.fireEvent(g.Events.AfterRowUpdate,[g.Id,r.Element.id]);
	if(g.Events.AfterRowUpdate[1]==1)
		g.NeedPostBack=false;
}

function igtbl_requestComplete(gn)
{
	var g=igtbl_getGridById(gn);
	g.ReqType=g.eReqType.None;
	if(g.CallBack || g.XmlHttp.readyState==4)
		g.ReadyState=g.eReadyState.Ready;
}
