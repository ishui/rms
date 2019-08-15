/* 
Infragistics Editable Tree Script 
Version 5.1.20051.37

Copyright (c) 2001-2004 Infragistics, Inc. All Rights Reserved.
Comments:
Functions marked public are for use by developers and are documented and supported.
Functions marked private are for the internal use of the UltraWebTree component.  
Functions marked private should not be used directly by developers
and are not documented for use by developers and are not supported for use by developers
*/
if(typeof(igtree_IE) != "boolean")
	var igtree_IE = (document.all) ? true : false;
if(typeof(igtree_treeState) != "object")
	var igtree_treeState=[];
if(typeof(igtree_indexState) != "object")
	var igtree_indexState=[];
// public - Obtains the Tree object using its id
function igtree_getTreeById(id) 
{
	return igtree_treeState[id];
}

// public - Obtains a Node object using its id
function igtree_getNodeById(id) 
{
	var nodeElement = igtree_getElementById(id);
	if(nodeElement == null)
		return null;
	var oNode = nodeElement.oNode;
	if(oNode)
		return oNode;	
	oNode = new igtree_initNode(nodeElement);
	nodeElement.oNode = oNode;
	return oNode;
}

// public - returns a Tree object based on a node Id 
function igtree_getTreeByNodeId(nodeId)
{
	var treeName = nodeId;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	var tree = igtree_treeState[treeName];
	return tree;
}

// public - returns the tree object from a Node element
function igtree_getTreeByNode(node) {
	return igtree_getTreeByNodeId(node.id);
}

// public - Sets the selected Node for the Tree to the passed in Node.
function igtree_setSelectedNode(tn, nodeId)
{
	var ts=igtree_treeState[tn];
	if(!ts)
		return null;
		
	var node=null;
	if(nodeId != null)
		node = igtree_getElementById(nodeId);
		
	if(node) {
		if(node.disabled)
			return;
	}
	var igtree_currentNode=igtree_selectedNode(tn);

	if(node==igtree_currentNode)
		return igtree_currentNode;

	var oldNodeId = null;
	if(igtree_currentNode!=null)
		oldNodeId=igtree_currentNode.id;

	if(ts.TreeLoaded) {
		if(igtree_fireEvent(tn,ts.Events.BeforeNodeSelectionChange,"(\""+tn+"\",\""+oldNodeId+"\",\""+nodeId+"\")"))
			return igtree_currentNode;
	}
	var className=igtree_getResolvedHiliteClass(tn,node);

	if(igtree_editControl != null && igtree_editControl.style.display!="none")
		if(igtree_endedit(true))
			return igtree_currentNode;

	if(igtree_currentNode!=null)
	{
		var nodeSpan = igtree_getNodeSpan(igtree_currentNode)
		nodeSpan.tabIndex = -1;
		var image=nodeSpan.previousSibling.previousSibling;
		if(image!=null && image.tagName=="IMG") {
			var unselectedImage = igtree_currentNode.getAttribute("igUnselImage");
			if(unselectedImage!=null && unselectedImage.length>0) {
				image.src=unselectedImage;
			}
			else
			if(ig_csom.notEmpty(ts.DefaultImage))
				image.src=ts.DefaultImage;
		}
		//var initClass = igtree_currentNode.getAttribute("igtInitClass");
		//if(igtree_currentNode.igtInitClass != null)
		nodeSpan.className=igtree_currentNode.getAttribute("igtInitClass");
	}
	if(node)
	{
		var nodeSpan=igtree_getNodeSpan(node);
		var nodeClassName=nodeSpan.className;
		if(nodeClassName!=className)
		{
			var initClass=nodeSpan.HovClass;
			if(initClass!=null)
				node.setAttribute("igtInitClass",initClass);
			else
				node.setAttribute("igtInitClass",nodeClassName);

			nodeSpan.className=className;
			
			var image=nodeSpan.previousSibling.previousSibling;
			if(image!=null && image.tagName=="IMG") {
				var igimg = image.getAttribute("igimg");
				if(igimg!=null && igimg.length>0) {
					var selectedImage = node.getAttribute("igSelImage");
					if(selectedImage==null || selectedImage.length==0)
						selectedImage=ts.DefaultSelectedImage;
					if(ig_csom.notEmpty(selectedImage)) {
						if(ts.TreeLoaded)
							node.setAttribute("igUnselImage", image.src);
						image.src=selectedImage;
					}
				}
			}
		}
		ts.selectedNodeElement = node;
		
		var oNode = igtree_getNodeById(nodeId);
		var parent = oNode.getParent();
		while(parent != null) {
			bExp = parent.getExpanded();
			if(!bExp)
				parent.setExpanded(true);
			parent = parent.getParent();
		}
		if(igtree_IE) {
			if(nodeSpan.offsetWidth != 0 && nodeSpan.offsetHeight != 0)
				nodeSpan.tabIndex = 1000;
				try {
					oNode.getElement().focus();
				}
				catch (e)
				{
				}
		}
		// clientViewState
		ts.update("SelectedNode", nodeId);
	}
	else {
		ts.update("SelectedNode", "NONE");
		ts.selectedNodeElement = null;
	}
	
	igtree_currentNode=node;

	if(!ts.TreeLoaded)
		return igtree_currentNode;
	igtree_fireEvent(tn,ts.Events.AfterNodeSelectionChange,"(\""+tn+"\",\""+nodeId+"\")");
	if(ts.NeedPostBack && igtree_clickCounter==0) {
		ts.NeedPostBack=false;
		__doPostBack(ts.UniqueId,"");
	}
	return igtree_currentNode;
}

// public - Browser independent way to retrieve the source element of an event
function igtree_getSrcElement(evnt)
{
	if(igtree_IE)
		return evnt.srcElement;
	else
		return evnt.target;
}

// public - Browser independent way to retrieve an element by its id
function igtree_getElementById(id)
{
	var el;
	if(igtree_IE)
		el = document.all[id];
	else 
		el = document.getElementById(id);
	return el;
}

// public - Retrieves the UniqueId on the server for the tree.
function igtree_getUniqueId(treeId)
{
	return igtree_treeState[treeId].UniqueId;
}

// public - Begins editing of a node in the tree.
function igtree_beginedit(tn,nodeId)
{
	var e = igtree_getElementById(nodeId);
	var disabled = e.disabled;
	if(disabled)
		return;
	disabled = e.getAttribute("nodeDisabled");
	if(disabled == "1")
		return;
	igtree_endedit(true);
	var src=igtree_getNodeSpan(igtree_getElementById(nodeId));
	var te = igtree_getTreeByNodeId(src.parentNode.id).treeElement;
	igtree_editControl=igtree_getEditControl(src);
	if(igtree_editControl) {
		if(igtree_fireEvent(tn,igtree_treeState[tn].Events.BeforeBeginNodeEdit,"(\""+tn+"\",\""+nodeId+"\")"))
			return;
		igtree_editControl.setAttribute("currentNode",nodeId);
		igtree_editControl.setAttribute("oldInnerText",src.innerText);
		if(igtree_IE)
			igtree_editControl.value=src.innerText;
		else
			igtree_editControl.value=src.innerHTML;
		igtree_editControl.style.display="";
		igtree_editControl.style.position="absolute";
		if(igtree_IE) {
    		var left;
    		var top;
    		if(te.style.width == "" && te.style.height == "")
			{
    			left = igtree_fnGetLeftPos(src);
    			top=igtree_fnGetTopPos(src); 
    		}
    		else {
    			left = igtree_fnGetLeftPos(src)-igtree_fnGetLeftPos(te)+te.scrollLeft-(te.style.borderWidth?parseInt(te.style.borderWidth,10):0);
    			top=igtree_fnGetTopPos(src)-igtree_fnGetTopPos(te)+te.scrollTop-(te.style.borderWidth?parseInt(te.style.borderWidth,10):0);
    		}
    		igtree_editControl.style.left = left;
    		igtree_editControl.style.top = top;

			igtree_editControl.style.width=src.offsetWidth+25;
			if(igtree_editControl.className == null || igtree_editControl.className.length == 0) {
				igtree_editControl.style.height=src.offsetHeight+3;
			}
		}
		else {
    		igtree_editControl.style.left=igtree_fnGetLeftPos(src)-igtree_fnGetLeftPos(te)+te.scrollLeft-(te.style.borderWidth?parseInt(te.style.borderWidth,10):0);
			igtree_editControl.style.top=igtree_fnGetTopPos(src)-igtree_fnGetTopPos(te)+te.scrollTop-(te.style.borderWidth?parseInt(te.style.borderWidth,10):0);
			igtree_editControl.style.width=src.offsetWidth+25;
			if(igtree_editControl.className == null || igtree_editControl.className.length == 0) {
				igtree_editControl.style.height=src.offsetHeight + 3;
			}
		}
		if(igtree_IE)
			igtree_editControl.focus();
		igtree_editControl.select();
		igtree_fireEvent(tn,igtree_treeState[tn].Events.AfterBeginNodeEdit,"(\""+tn+"\",\""+nodeId+"\")");
	}
}

// public - Ends editing of the current node
function igtree_endedit(accept) {
	if(!igtree_editControl || igtree_editControl.style.display=="none")
		return;
	if(igtree_editControl.endEdit)
		return;
	igtree_editControl.endEdit = true;
	
	var src=igtree_getElementById(igtree_editControl.getAttribute("currentNode"));
	src=igtree_getNodeSpan(src);

	var node=igtree_getNodeById(src.parentNode.id);
	var ts = igtree_treeState[node.getTreeId()];
	if(igtree_fireEvent(node.getTreeId(),ts.Events.BeforeEndNodeEdit,"(\""+node.getTreeId()+"\",\""+node.getElement().id+"\",\""+igtree_editControl.value+"\")"))
	{
		igtree_editControl.endEdit = false;
		return true;
	}
	if(src && accept) {
		var nodeId = node.Id;
		var treeName = nodeId;
		var strArray = treeName.split("_");
		treeName = strArray[0];
		
		var newText = igtree_editControl.value;
		if(igtree_fireEvent(treeName,ts.Events.BeforeNodeUpdate,"(\""+treeName+"\",\""+nodeId+"\",\""+newText+"\")")) {
			igtree_editControl.endEdit = false;
			return;
		}
		if(newText == "")
			newText = " ";
		node.setText(newText);
		igtree_fireEvent(treeName,ts.Events.AfterNodeUpdate,"(\""+treeName+"\",\""+nodeId+"\")");
		if(ts.NeedPostBack)
			__doPostBack(ts.UniqueId,"");
	}
	else if(src)
		src.innerText=igtree_editControl.getAttribute("oldInnerText");
	igtree_editControl.removeAttribute("currentNode");
	igtree_editControl.removeAttribute("oldInnerText");
	igtree_editControl.style.display="none";
	igtree_editControl.endEdit = false;
	igtree_editControl=null;
	if(igtree_fireEvent(node.getTreeId(),igtree_treeState[node.getTreeId()].Events.AfterEndNodeEdit,"(\""+node.getTreeId()+"\",\""+node.getElement().id+"\")")) {
		return;
	}
		
	if(ts.NeedPostBack)	{
		ts.NeedPostBack=false;
		__doPostBack(ts.UniqueId,node.element.id+":Edit");
		return;
	}

}



// public - Marks a tree for postback to the server.  At the completion of the current event, 
// the page will be posted.
function igtree_needPostBack(tn)
{
	igtree_treeState[tn].NeedPostBack=true;
}

// public - Cancels a pending postback
function igtree_cancelPostBack(tn)
{
	igtree_treeState[tn].CancelPostBack=true;
}

// private - Initializes the tree object on the client
function igtree_initTree(treeId) 
{
	var treeElement = igtree_getElementById("T_"+treeId);
	// Create the tree object and assign it to the tree variable on the html page
	var tree = new igtree_tree(treeId, treeElement,eval("igtree_"+treeId+"_Tree"));
	treeElement.igtree = tree;
	igtree_fireEvent(treeId,tree.Events.InitializeTree,"(\""+treeId+"\");");
	tree.TreeLoaded=true;
	return tree;
}
function igtree_initLevel() {

}
// private - constructor for the tree object
function igtree_tree(treeId, _treeElement,treeProps)
{
	igtree_treeState[treeId]=this;
	igtree_indexState[igtree_indexState.length] = this;
	this.treeElement = _treeElement;
	this.treeId = treeId;
	this.Id = treeId;
	
	this.update = function(propName, propValue) {
		if(this.suspendUpdates == true)
			return;
		ig_ClientState.setPropertyValue(this.treeState,propName,propValue);
		if(this.postField!=null)
			this.postField.value = ig_ClientState.getText(this.stateItems);	
	}

	this.Element = _treeElement;
	this.UniqueId=treeProps[0];
	this.HiliteClass=treeProps[1];
	this.HoverClass=treeProps[2];
	this.ExpandImage=treeProps[3];
	this.CollapseImage=treeProps[4];
	this.Selectable=treeProps[5];
	this.Editable=treeProps[6];
	this.ImageDirectory=treeProps[7];
	this.ClassicTree=treeProps[8];
	this.SingleBranchExpand=treeProps[9];
	this.LoadOnDemand=treeProps[10];
	this.RenderAnchors=treeProps[11];
	this.DefaultSelectedImage=treeProps[12];
	this.DefaultImage=treeProps[13];
	this.DisabledClass=treeProps[14];

	this.getSelectedNode=igtreem_getSelectedNode;
	this.setSelectedNode=igtreem_setSelectedNode;
	this.getNodeById=igtree_getNodeById;
	this.getNodes=igtree_getTreeNodes;
	this.clearNodes=igtree_clearNodes;
	this.getClientUniqueId=igtree_getClientUniqueId;
	this.insertRoot=function(beforeNode, text, className) {
		return this._insert(null, beforeNode, text, className);
	}
	this.addRoot=function(text, className) {
		return this._insert(null, -1, text, className);
	}
	this._insert=igtree_insertChild;
	this.endEdit=function(acceptChanges) {
		igtree_endedit(acceptChanges);
	}

	var uniqueId = this.getClientUniqueId();
	this.Events=new igtree_events(eval("igtree_"+uniqueId+"_Events"));
	this.Levels=eval("igtree_"+uniqueId+"_Levels");
	this.Levels.getItem = function(index) {
		for(i=0;i<this.length;i++) {
			if(this[i][0] == index) {
				var level = new igtree_initLevel();
				level.Index = index;
				level.LevelCheckBoxes = this[i][1]
				level.LevelClass = this[i][2]
				level.LevelHiliteClass = this[i][3]
				level.LevelHoverClass = this[i][4]
				level.LevelImage = this[i][5]
				level.LevelIslandClass = this[i][6]
				return level;
			}
		}
		return null;
	}

	var nodeId=treeProps[15];
	if(nodeId && nodeId.length>0) {
		igtree_setSelectedNode(uniqueId, nodeId);
	}
	
	this.scrollnodeId=treeProps[16];
	this.scrolltop=treeProps[17];
	this.Enabled=treeProps[18]
	this.TargetUrl=treeProps[19]; 
	this.TargetFrame=treeProps[20]; 
	this.AllowDrag=treeProps[21];
	this.AllowDrop=treeProps[22];
	this.Indentation=treeProps[23];
	this.CheckBoxes=treeProps[24];
	this.RootNodeClass=treeProps[25]; 
	this.ParentNodeClass=treeProps[26]; 
	this.LeafNodeClass=treeProps[27]; 
	this.RootNodeImageUrl=treeProps[28]; 
	this.ParentNodeImageUrl=treeProps[29]; 
	this.LeafNodeImageUrl=treeProps[30]; 
	this.Expandable=treeProps[31];
	
	this.TreeLoaded=false;
	this.NeedPostBack=false;
	this.CancelPostBack=false;
	
	if(this.AllowDrag) {
		ig_csom.addEventListener(this.Element, "mousedown", igtree_preselect, true);
	}
	// clientViewState
	this.postField	= ig_csom.getElementById(igtree_getUniqueId(treeId));	
	var agt=navigator.userAgent.toLowerCase();
	//if(!(ig_csom.IsIE5 &&	(agt.indexOf("msie 5.0")!=-1))){
		this.stateItems	= ig_ClientState.createRootNode();
		this.treeState	= ig_ClientState.addNode(this.stateItems,"WebTree");
		this.nodeState	= ig_ClientState.addNode(this.treeState,"Nodes");
	//}
	
	var clientState=eval("igtree_"+uniqueId+"_cs");
	for(i=0;i<clientState.length;i++) {
		var node = clientState[i];
		if(!node)
			break;
		var id = uniqueId + node[0];
		var action = node[1];
		switch(action) {
			case 0 :
				this.update("SelectedNode", id);
				break;
			case 1 :
				igtree_updateNodeToggle(this, id, true);
				break;
			case 2 :
				igtree_updateNodeToggle(this, id, false);
				break;
			case 3 :
				igtree_updateNodeCheck(this, id, true)
				break;
			case 4 :
				igtree_updateNodeCheck(this, id, false)
				break;
		}
	}
}

function igtree_loadcomplete() {
	for(i=0; i<igtree_indexState.length; i++) {
		var tree = igtree_indexState[i];
		var eNode = ig_csom.getElementById(tree.scrollnodeId);
		if(eNode) {
			igtree_scrollToView(tree.Element,eNode)
		}
	}
}
// private - initializes the client-side events for the Tree object
function igtree_events(events)
{
	this.AfterBeginNodeEdit=events[0];
	this.AfterEndNodeEdit=events[1];
	this.AfterNodeSelectionChange=events[2];
	this.AfterNodeUpdate=events[3];
	this.BeforeBeginNodeEdit=events[4];
	this.BeforeEndNodeEdit=events[5];
	this.BeforeNodeSelectionChange=events[6];
	this.BeforeNodeUpdate=events[7];
	this.NodeChecked=events[8];
	this.EditKeyDown=events[9];
	this.EditKeyUp=events[10];
	this.InitializeTree=events[11];
	this.KeyDown=events[12];
	this.KeyUp=events[13];
	this.NodeClick=events[14];
	this.NodeCollapse=events[15];
	this.NodeExpand=events[16];
	this.DemandLoad=events[17];
	this.Drag=events[18];
	this.DragEnd=events[19];
	this.DragEnter=events[20];
	this.DragLeave=events[21];
	this.DragOver=events[22];
	this.DragStart=events[23];
	this.Drop=events[24];
}

// private
function igtree_getNodeSpan(node)
{
	if(!node)
		return null;
	var span=node.childNodes[node.childNodes.length-1];
	while(span && span.tagName!="SPAN")
		span=span.previousSibling;
	return span;
}

function igtree_getSrcNodeElement(evnt,tn)
{
	var src=igtree_getSrcElement(evnt);
	var parent = src.parentNode;
	while(parent != null) {
		if(parent.id != null && parent.id.length > 0)
			return src;
		if(src.tagName=="IMG" && src.getAttribute("imgType")=="exp")
			return src;
		if(src.tagName=="IMG" && src.getAttribute("igimg")=="1")
			return src;
		if(src.tagName=="INPUT" || src.tagName=="SPAN")
			return src;
			
		src = parent;
		parent = parent.parentNode;
	}
	return null;
	
}
function igtree_getNodeElement(src)
{
	var parent = src;
	while(parent) {
		if(ig_csom.notEmpty(parent.id))
			return parent;
		parent = parent.parentNode;
	}
	return null;
}

function igtree_pageUnload(){
	if(ig_csom.IsIE55Plus) {
		ig_delete(igtree_treeState);
		ig_delete(igtree_indexState);
	}
}

if(typeof(ig_csom)!="undefined" && ig_csom.IsIE)
	ig_csom.addEventListener(window, "unload", igtree_pageUnload, true);

// private - toggles the expansion state of a node.
function igtree_toggle(tn, nodeId)
{
	var node=igtree_getNodeById(nodeId);
	var s = igtree_getElementById("M_"+nodeId);
	var ts=igtree_treeState[tn];
	
	if(!node.getEnabled()) 
		return;
		
	if(!s && ts.LoadOnDemand>=1) {
		node.setExpanded(true);
		return;
	}
	if(s.style.display == "none") {
		node.setExpanded(true);
	}
	else
		node.setExpanded(false);
		
	return;		
}

// private - Implements the Collapse() method for the Node object
function igtree_collapseNode(node) {
	var tn=node.getTreeId();
	var ts=igtree_treeState[tn];
	var s;
	s = igtree_getElementById("M_"+node.element.id);
	if(!s)
		return;
	var oNode = ts.getSelectedNode();
	if(oNode != null && ts.Events.AfterNodeSelectionChange[1] == 0) {
		var parent = oNode.getParent();
		while(parent != null) {
			if(parent.element.id == node.element.id)
				node.setSelected(true);
			parent = parent.getParent();
		}
	}
	var button = igtree_getNodeExpandCollapseImage(ts, node);
	if(!button)
		return;
	if(igtree_fireEvent(tn,ts.Events.NodeCollapse,"(\""+tn+"\",\""+node.element.id+"\")"))
		return;
	if(ts.NeedPostBack)	{
		if(ts._FreezeServerEvents==null)
		{
			ts.NeedPostBack=false;
			__doPostBack(ts.UniqueId,node.element.id+":Collapse");
			return;
		}
	}
	if(ts.ClassicTree){
		s.style.display = "none";
		
		// [KV 10/28/2004, 12:44 PM] replace changed to use simple expression
		// UWN945 - The plus minus images of ClassicTree do not change on expanding and collapsing
		button.src = button.src.replace("minus.gif", "plus.gif");
		// button.src = button.src.replace("ig_treefminus.gif", "ig_treefplus.gif");
		// button.src = button.src.replace("ig_treemminus.gif", "ig_treemplus.gif");
		// button.src = button.src.replace("ig_treelminus.gif", "ig_treelplus.gif");
		// button.src = button.src.replace("ig_treeominus.gif", "ig_treeoplus.gif");
	}
	else{
		image = ts.ExpandImage;
		if(image == "")
			image = "ig_treeplus.gif";
		button.src = image;
		s.style.display = "none";
	}
	igtree_updateNodeToggle(ts, s.id, false);
}

// Private - returns the expand image for a node
function igtree_getNodeExpandCollapseImage(tree, node) {
	var index = 1;
	var button
	if(tree.ClassicTree) {
		index = 0;
		button=node.element.childNodes[0].childNodes[index];
	}
	else
		button=node.element.childNodes[index];
	if(button.tagName!="IMG" || button.getAttribute("imgType")!="exp"){
		while (button!=null && (button.tagName!="IMG" || button.getAttribute("imgType")!="exp")) {
			button=button.nextSibling;
		}
		if(button==null)
			return;
	}
	return button;
}

// Private - Implements the Expand() method for the Node object
function igtree_expandNode(node) {
	var tn=node.getTreeId();
	var ts=igtree_treeState[tn];
	
	var button = igtree_getNodeExpandCollapseImage(ts, node);
	if(!button)
		return;
	var s;
	s = igtree_getElementById("M_"+node.element.id);
	if(!s) {
		var ts=igtree_treeState[tn];
		if(ts.LoadOnDemand>=1) {
				igtree_updateNodeToggle(ts, "M_"+node.element.id, true);
				if(igtree_fireEvent(tn,ts.Events.DemandLoad,"(\""+tn+"\",\""+node.element.id+"\")"))
					return;
				if(ts.NeedPostBack)	{
					__doPostBack(ts.UniqueId,node.element.id+":DemandLoad");
					return;
				}
		}
		return;
	}

	if(igtree_fireEvent(tn,ts.Events.NodeExpand,"(\""+tn+"\",\""+node.element.id+"\")"))
		return;

	if(ts.NeedPostBack){
		ts.NeedPostBack=false;
		__doPostBack(ts.UniqueId,node.element.id+":Expand");
		return;
	}

	if(ts.SingleBranchExpand) {
		ts._FreezeServerEvents = true;
		var prev=node.getPrevSibling();
		while(prev != null) {
			prev.setExpanded(false);
			prev=prev.getPrevSibling();
		}
		var next=node.getNextSibling();
		while(next != null) {
			next.setExpanded(false);
			next=next.getNextSibling();
		}
		ts._FreezeServerEvents = null;
	}
	
	if(ts.ClassicTree){
		s.style.display = "";
		// [KV 10/28/2004, 12:44 PM] replace changed to use simple expression
		// UWN945 - The plus minus images of ClassicTree do not change on expanding and collapsing
		button.src = button.src.replace("plus.gif", "minus.gif");
		// button.src = button.src.replace("ig_treefplus.gif", "ig_treefminus.gif");
		// button.src = button.src.replace("ig_treemplus.gif", "ig_treemminus.gif");
		// button.src = button.src.replace("ig_treelplus.gif", "ig_treelminus.gif");
		// button.src = button.src.replace("ig_treeoplus.gif", "ig_treeominus.gif");
	}
	else{
		image = ts.CollapseImage;
		if(image == "")
			image = "ig_treeminus.gif";
		button.src = image;
		s.style.display = "";
	}
	igtree_updateNodeToggle(ts, s.id, true);
}

// private - Handles checkbox clicking within the tree.
function igtree_checkboxClick(tn, nodeId, src)
{
	var ts=igtree_treeState[tn];
	var node=igtree_getNodeById(nodeId);
	if(src.checked) {
		if(igtree_fireEvent(tn,ts.Events.NodeChecked,"(\""+tn+"\",\""+nodeId+"\", true)")) {
			src.checked = false;
			return;
		}
		if(ts.NeedPostBack)	{
			__doPostBack(ts.UniqueId,nodeId+":Checked");
			return;
		}
		igtree_updateNodeCheck(ts, nodeId, true);
	}
	else {
		if(igtree_fireEvent(tn,ts.Events.NodeChecked,"(\""+tn+"\",\""+nodeId+"\", false)")) {
			src.checked = true;
			return;
		}
		if(ts.NeedPostBack)	{
			__doPostBack(ts.UniqueId,nodeId+":Unchecked");
			return;
		}
		igtree_updateNodeCheck(ts, nodeId, false);
	}
}

// private - Retrieves the resolved hover class for an item in the tree.
function igtree_getResolvedHoverClass(tn,node)
{
	if(node.getAttribute("HoverClass"))
		return node.getAttribute("HoverClass");
	return igtree_treeState[tn].HoverClass;
}

// private - Handles the mouse over event for the tree.
function igtree_mouseover(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	if(src.tagName!="SPAN")
		return;
	var eNode = igtree_getNodeElement(src);

	var tree=igtree_treeState[tn];
	if(igtree_IE && tree.Element.clientWidth>0 && eNode.offsetWidth>tree.Element.clientWidth) 
		if(ig_csom.isEmpty(eNode.title)) {
			src.title=src.innerText;
			eNode.igtitle = true;
		}

	var node=igtree_getNodeById(eNode.id);
	if(!node.getEnabled())
		return;

	if(eNode == igtree_selectedNode(tn))
		return;

	var className=igtree_getResolvedHoverClass(tn,eNode);
	if(className=="" || src.className == className)
	    return;
	    
	var igtxt = src.getAttribute("igtxt");	
	if(igtxt!=null && igtxt.length>0) {
		src.HovClass = src.className;
		src.hoverSet = true;
		src.className = className;
	}
	
}

// private - Handles the mouse out event for the tree
function igtree_mouseout(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	if(src.tagName!="SPAN")
		return;
	var eNode = igtree_getNodeElement(src);

	if(eNode.igtitle) {
		src.title="";
		eNode.igtitle = null;
	}
	
	var node=igtree_getNodeById(eNode.id);
	if(!node.getEnabled())
		return;

	var igtxt = src.getAttribute("igtxt");	
	if(igtxt==null || igtxt.length==0) {
		return;
	}
	if(eNode != igtree_selectedNode(tn)){
		if(src.style != null) {
			if(src.hoverSet) {
				prevClass =	src.HovClass;
				if(prevClass == null)
					prevClass = "";
				src.className = prevClass;
				src.hoverSet = null;
			}
		}
	}
}

// private - Handles the right click event for the tree.
function igtree_contextmenu(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var ts=igtree_treeState[tn];
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	if (src.tagName!="SPAN") return;

	var eNode = igtree_getNodeElement(src);
	ts.event = evnt;

	if(igtree_fireEvent(tn,ts.Events.NodeClick,"(\""+tn+"\",\""+eNode.id+"\", 2)")) {
		ig_cancelEvent(evnt);
		return false;
	}
	ts.event = null;
}

// private - Retieves the resolved HiliteClass for a node in the tree.
function igtree_getResolvedHiliteClass(tn,src)
{
	if(!src)
		return "";
	if(src.getAttribute("HiliteClass"))
		return src.getAttribute("HiliteClass");
	if(igtree_treeState[tn].HiliteClass!="")
		return igtree_treeState[tn].HiliteClass;
	return tn+"HiliteClass";
}

function igtree_navigate(tree, node) {
	if(!node.getEnabled())
		return;
	if(!node.WebTree.Enabled)
		return;

	if(node.getTargetUrl()==null)
	{
		if(igtree_fireEvent(tree.Id,tree.Events.NodeClick,"(\""+tree.Id+"\",\""+node.Id+"\",1)"))
			return;
		if(tree.NeedPostBack)
		{
			igtree_postNodeClick(tree.Id,node.Id);
			return;
		}
	}
	if(ig_csom.notEmpty(node.getTargetUrl()) && !tree.RenderAnchors) //&& 
		ig_csom.navigateUrl(node.getTargetUrl(),node.getTargetFrame());
}

// private - Handles the click event for nodes
function igtree_nodeclick(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var tree=igtree_treeState[tn];
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	

	if(!tree.Enabled)
		return;
	var eNode = igtree_getNodeElement(src);
	var igtxt = src.getAttribute("igtxt")!=null && src.getAttribute("igtxt").length>0;	
	var igimg = src.getAttribute("igimg")!=null && src.getAttribute("igimg").length>0;	
	var igchk = src.getAttribute("igchk")!=null && src.getAttribute("igchk").length>0;	

	igtree_lastActiveTree=tn;
	if(igtxt || igimg)
	{
		var node=igtree_getNodeById(eNode.id);
		if(node!=null) {
			if(node.getEnabled() == false)
				return;
		}
		if(tree.Selectable) {
			igtree_navigate(tree, node)
			igtree_setSelectedNode(tn,eNode.id);
		}
	}
	else if(src.tagName=="IMG")
		igtree_toggle(tn,eNode.id);
	else if(igchk) {
		igtree_checkboxClick(tn, eNode.id, src);
	}
	return false;
}

var igtree_treeName;
var igtree_nodeId;
var igtree_postCanceled = false;

var igtree_clickCounter = 0;

// private - Handles posting node click events to the server
function igtree_postNodeClick(treeName, nodeId)
{
	igtree_treeName = treeName;
	igtree_nodeId = nodeId;
	var src;
	src = igtree_getElementById(nodeId);
	igtree_clickCounter++;
	if(igtree_clickCounter == 1)
		setTimeout('igtree_onTimerPostNodeClick()', 300);
}

// private - Posts node click events on time expiration
function igtree_onTimerPostNodeClick() 
{
	var tree = igtree_getTreeById(igtree_treeName);	
	tree.update("SelectedNode", igtree_nodeId);
	if(igtree_postCanceled == false) {
		tree.NeedPostBack=false;
		__doPostBack(tree.UniqueId, igtree_nodeId+":Clicked");
	}
	igtree_postCanceled = false;
	igtree_clickCounter = 0;
}

// private - Handles the scrolling of the tree
function igtree_onscroll(src)
{
	var treeName = src;
	var ts = igtree_getTreeById(src);	
	var treeControl = igtree_getElementById("T_" + treeName);
	ts.update("ScrollTopPos", treeControl.scrollTop);
	return true;
}
	
// private - Updates the PostBackData field
function igtree_updatePostField(treeName, nodeId, oldNodeId)
{
	var formControl = igtree_getElementById(igtree_getUniqueId(treeName));
	if(!formControl)
		return;
	
	formControl.value = treeState;
}

// private - Handles the double click event for a node
function igtree_dblclick(evnt,tn)
{
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	var tree=igtree_treeState[tn];
	var eNode = igtree_getNodeElement(src);
	var igtxt = src.getAttribute("igtxt")!=null && src.getAttribute("igtxt").length>0;	
	var igimg = src.getAttribute("igimg")!=null && src.getAttribute("igimg").length>0;	
	if(igtxt && tree.Editable)
	{
		igtree_postCanceled = true;
		igtree_beginedit(tn,eNode.id);
		return;
	}
	if(igimg || igtxt) {
		if(!tree.Enabled)
			return;

		var node=igtree_getNodeById(eNode.id);
		if(node.getFirstChild() != null || tree.LoadOnDemand>=1) {
			igtree_toggle(tn,eNode.id);
		}
	}
}

var igtree_editControl = null;
// private - Retrieves the in-place edit control for an editable tree
function igtree_getEditControl(src)
{
	var strArray;
	if(!src)
		return null;
	if(igtree_IE)
		strArray = src.parentElement.id.split("_");
	else
		strArray = src.parentNode.id.split("_");
	var treeName = strArray[0];
	return igtree_getElementById(treeName+"_tb");
}

// private - Handles mouse clicks within the edit control
function igtree_editClickHandler()
{
	event.cancelBubble = true;
}

// private - Handles selection events for the tree
var igtree_bDragSelect = false;
function igtree_selectStart()
{
	if(window.event.srcElement.tagName == "INPUT")
		return;
	if(igtree_bDragSelect)
		return;
	window.event.cancelBubble = true; 
	window.event.returnValue = false; 
	return false;	
}

// private - Updates internal buffer for node expansion/collapse data
function igtree_updateNodeToggle(ts, nodeId, bExpanded)
{
	nodeId = nodeId.replace("M_", "");
	var node = igtree_getNodeById(nodeId);
	node.update("Expanded", bExpanded);
	
}
   
// private - Updates internal buffer for node checked status
function igtree_updateNodeCheck(ts, nodeId, bChecked){
	var node = igtree_getNodeById(nodeId);
	node.update("Checked", bChecked);

}

// private - Handles key down events
function igtree_keydown(evnt, treeID)
{
	var ts=igtree_treeState[treeID];
	var tree=ts.treeElement;
	var processed=false;
	var igtree_currentNode = igtree_selectedNode(treeID);
	if(igtree_fireEvent(treeID,ts.Events.KeyDown,"(\""+treeID+"\","+evnt.keyCode+")"))
		return;
	if(evnt.keyCode == 13){  // Enter
		if(igtree_currentNode != null)	{
			processed=true;
			igtree_navigate(ts, ts.getSelectedNode());			
		}
	}
	if(evnt.keyCode == 113){  // F2
		if(igtree_currentNode != null)	{
			processed=true;
			if(ts.Editable)
				igtree_beginedit(treeID,igtree_currentNode.id);
		}
	}
	if(evnt.keyCode == 107 || evnt.keyCode == 109 || evnt.keyCode == 37 || evnt.keyCode == 39)
	{ // plus/minus key
		if(igtree_currentNode == null)
			return;
		var ns=igtree_getElementById("M_"+igtree_currentNode.id);
		if(!ns && ts.LoadOnDemand>0) {
			if(evnt.keyCode==107 || evnt.keyCode==39)
				igtree_toggle(treeID,igtree_currentNode.id);
			processed=true;
		}
		else
		if(ns)	{
			var toggle=((evnt.keyCode == 107 || evnt.keyCode == 39) && ns.style.display=="none" || (evnt.keyCode == 109 || evnt.keyCode == 37) && ns.style.display=="");
			if(toggle)
				igtree_toggle(treeID,igtree_currentNode.id);
			processed=true;
		}
	}
	if(evnt.keyCode == 35){ // end key
		if(igtree_currentNode)	{
			var last = igtree_lastNode(treeID);
			if(last){
				last=igtree_setSelectedNode(treeID,last.id);
				igtree_scrollToView(tree,last);
				processed=true;
			}
		}
	}
	if(evnt.keyCode == 36){ // home key
		if(igtree_currentNode){
			var first = igtree_firstNode(treeID);
			if(first){
				first=igtree_setSelectedNode(treeID,first.id);
				igtree_scrollToView(tree,first);
				processed=true;
			}
		}
	}
	if(evnt.keyCode == 38)	{ // up arrow
		if(igtree_currentNode)	{
			var sibling = igtree_prevVisibleNode(treeID,igtree_currentNode);
			if(sibling)	{
				while(sibling && sibling.getAttribute("nodeDisabled"))
					sibling = igtree_prevVisibleNode(treeID,sibling);
				if(!sibling)
					return;	
				sibling=igtree_setSelectedNode(treeID,sibling.id);
				igtree_scrollToView(tree,sibling);
				processed=true;
			}
		}
	}
	if(evnt.keyCode == 40)
	{ // down arrow
		if(igtree_currentNode){
			var sibling = igtree_nextVisibleNode(treeID,igtree_currentNode);
			if(sibling)	{
				while(sibling && sibling.getAttribute("nodeDisabled"))
					sibling = igtree_nextVisibleNode(treeID,sibling);
				if(!sibling)
					return;	
				sibling=igtree_setSelectedNode(treeID,sibling.id);
				igtree_scrollToView(tree,sibling);
				processed=true;
			}
		}
	}
	if(processed)	{
		// [KV 10/29/2004, 4:27 PM] Corrected spelling mistake, Babble to Bubble
		//evnt.cancelBabble=true;
		evnt.cancelBubble=true;
		evnt.returnValue=false;
		return false;
	}
}

// private - Handles key up events
function igtree_keyup(evnt, tn)
{
	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.KeyUp,"(\""+tn+"\","+evnt.keyCode+")"))
		return;
}

// private
function igtree_nextSibling(tn,node,all) {
	var ts=igtree_treeState[tn];
	var sibling;
	if(!node)	{
		var tree=ts.treeElement;
		var first=tree.childNodes[0].childNodes[0].childNodes[0];
		while(first && first.tagName!="DIV")
			first=first.nextSibling;
		if(!first)
			return null;
		sibling=first;
		while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
			sibling=sibling.nextSibling;
		return sibling;
	}
	else
		sibling = node.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none" || all && sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.nextSibling;
		while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
			sibling=sibling.nextSibling;
		parentNode=parentNode.parentNode;
	}
	if(sibling && sibling.id.substr(0,2)=="M_")
		sibling=sibling.childNodes[0];
	while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
		sibling=sibling.nextSibling;
	return sibling;
}

// private
function igtree_nextSiblingAll(tn,node,all) {
	var ts=igtree_treeState[tn];
	var sibling;
	if(!node)	{
		var tree=ts.treeElement;
		var first=tree.childNodes[0].childNodes[0].childNodes[0];
		while(first && first.tagName!="DIV")
			first=first.nextSibling;
		if(!first)
			return null;
		sibling=first;
		while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
			sibling=sibling.nextSibling;
		return sibling;
	}
	else
		sibling = node.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || all && sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.nextSibling;
		while(sibling && (sibling.tagName!="DIV" ))
			sibling=sibling.nextSibling;
		parentNode=parentNode.parentNode;
	}
	if(sibling && sibling.id.substr(0,2)=="M_")
		sibling=sibling.childNodes[0];
	while(sibling && (sibling.tagName!="DIV" ))
		sibling=sibling.nextSibling;
	return sibling;
}

function igtree_prevSibling(tn,node,all)
{
	var ts=igtree_treeState[tn];
	var sibling;
	if(!node)	{
		var tree=ts.treeElement;
		var last=tree.childNodes[0].childNodes[tree.childNodes[0].childNodes.length-1];
		while(last && last.tagName!="DIV")
			last=last.previousSibling;
		if(!last)
			return null;
		sibling=last;
		while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
			sibling=sibling.previousSibling;
		return sibling;
	}
	else
		sibling = node.previousSibling;
	while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none" || all && sibling.id.substr(0,2)=="M_"))
		sibling=sibling.previousSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.previousSibling;
		while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
			sibling=sibling.previousSibling;
		parentNode=parentNode.parentNode;
	}
	while(sibling && sibling.id.substr(0,2)=="M_") {
		sibling=sibling.childNodes[sibling.childNodes.length-1];
		while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
			sibling=sibling.previousSibling;
	}
	return sibling;
}

function igtree_fnGetLeftPos(e) 
{
    x = e.offsetLeft;
    tmpE = e.offsetParent;
    while (tmpE != null)
    {
        x += tmpE.offsetLeft;
        if(tmpE.tagName=="DIV" && tmpE.style.borderLeftWidth)
	        x += parseInt(tmpE.style.borderLeftWidth);
        if(igtree_IE && tmpE.tagName!="BODY") {
			x-=tmpE.scrollLeft;
	    }
        tmpE = tmpE.offsetParent;
    }
    return x;
}

// Returns top position of some element
function igtree_fnGetTopPos(e) 
{
    y = e.offsetTop;
    tmpE = e.offsetParent;
    while (tmpE != null) {
        y += tmpE.offsetTop;
        if(tmpE.tagName=="DIV" && tmpE.style.borderTopWidth)
	        y += parseInt(tmpE.style.borderTopWidth);
        if(igtree_IE && tmpE.tagName!="BODY")
			y-=tmpE.scrollTop;
        tmpE = tmpE.offsetParent;
    }
    return y;
}

function igtree_scrollToView(parent,child)
{
	if(parent.scrollWidth<=parent.offsetWidth && parent.scrollHeight<=parent.offsetHeight)
		return;
	var childLeft=igtree_fnGetLeftPos(child);
	var parentLeft=igtree_fnGetLeftPos(parent);
	var childTop=igtree_fnGetTopPos(child);
	var parentTop=igtree_fnGetTopPos(parent);
	var childRight=childLeft+child.offsetWidth;
	var parentRight=parentLeft+parent.offsetWidth;
	var childBottom=childTop+child.offsetHeight;
	var parentBottom=parentTop+parent.offsetHeight;
	var hsw=(parent.scrollWidth>parent.offsetWidth?18:0);
	var vsw=(parent.scrollWidth>parent.offsetWidth || parent.scrollHeight>parent.offsetHeight?18:0);
	if(childRight>parentRight-vsw && childLeft-(childRight-parentRight)>parentLeft)
		parent.scrollLeft+=childRight-parentRight+vsw;
	if(childBottom>parentBottom-hsw && childTop-(parentTop-childTop)>parentTop)
		parent.scrollTop+=childBottom-parentBottom+hsw;
	if(childLeft<parentLeft)
		parent.scrollLeft-=parentLeft-childLeft;
	if(childTop<parentTop)
		parent.scrollTop-=parentTop-childTop;
}

function igtree_editKeyDown(evnt,tn)
{
	if(!igtree_editControl)
		return;
	var src=igtree_getElementById(igtree_editControl.getAttribute("currentNode"));
	var node=igtree_getNodeById(src.id);
	src=igtree_getNodeSpan(src);
	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.EditKeyDown,"(\""+tn+"\",\""+node.getElement().id+"\","+evnt.keyCode+")")){
		evnt.cancelBubble = true;
		evnt.returnValue = false;
		return;
	}
	if(igtree_IE) {
		evnt.cancelBubble=true;
		if(evnt.keyCode==13)
		{
			event.returnValue=false;
			igtree_endedit(true);
			return false;
		}
		else if(evnt.keyCode==27)
			igtree_endedit(false);
	}
	else {
		if(evnt.keyCode==13){
			evnt.stopPropagation();
			igtree_endedit(true);
			return false;
		}
		else if(evnt.keyCode==27)
			igtree_endedit(false);
	}
}

function igtree_editKeyUp(evnt,tn)
{
	if(!igtree_editControl)
		return;
	var src=igtree_getElementById(igtree_editControl.getAttribute("currentNode"));
	var node=igtree_getNodeById(src.id);
	src=igtree_getNodeSpan(src);
	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.EditKeyUp,"(\""+tn+"\",\""+node.getElement().id+"\","+evnt.keyCode+")"))
		return;
	if(igtree_IE)
		evnt.cancelBubble=true;
}

// private - Initializes a Node object with properties and method references
function igtree_initNode(node)
{
	this.element=node;
	this.Element=node;
	this.Id = node.id;
	this.WebTree = igtree_getTreeByNodeId(this.Element.id);
	this.getTreeId=igtree_getTreeId;
	this.getElement=igtree_getElement;
	this.getText=igtree_getText;
	this.setText=igtree_setText;
	this.getClass=igtree_getClass;
	this.setClass=igtree_setClass;
	this.getTag=igtree_getTag;
	this.setTag=igtree_setTag;
	this.getDataKey=igtree_getDataKey;
	this.getHiliteClass=igtree_getHiliteClass;
	this.setHiliteClass=igtree_setHiliteClass;
	this.getHoverClass=igtree_getHoverClass;
	this.setHoverClass=igtree_setHoverClass;
	this.getEnabled=igtree_getEnabled;
	this.setEnabled=igtree_setEnabled;
	this.getTargetFrame=igtree_getTargetFrame;
	this.setTargetFrame=igtree_setTargetFrame;
	this.getTargetUrl=igtree_getTargetUrl;
	this.setTargetUrl=igtree_setTargetUrl;
	this.hasChildren=igtree_hasChildren;
	
	this.getExpanded=igtree_getExpanded;
	this.setExpanded=igtree_setExpanded;
	this.getSelected=igtree_getSelected;
	this.setSelected=igtree_setSelected;
	this.getChecked=igtree_getChecked;
	this.setChecked=igtree_setChecked;
	this.hasCheckbox=igtree_hasCheckbox;
	
	this.getNextSibling=igtree_getNodeNextSibling;
	this.getPrevSibling=igtree_getNodePrevSibling;
	this.getFirstChild=igtree_getNodeFirstChild;
	this.getParent=igtree_getNodeParent;
	this.getChildNodes=igtree_getChildNodes;
	this.getLevel = function () {
		var nodeName=this.element.id.split("_")
		if(nodeName.length>1)
		{
			return nodeName.length - 2;			
		}
	}
	this.getIndex = function () {
		var index=0;
		var nodeName=this.element.id.split("_")
		if(nodeName.length>1)
		{
			index = parseInt(nodeName[nodeName.length-1]);
			return index-1;
		}
	}
	this.update = function (propName, propValue) {
		if(this.WebTree.suspendUpdates == true)
			return;
		if(propName == 'Remove') {
			var nodestate = ig_ClientState.addNode(this.WebTree.nodeState, this.Id);
			ig_ClientState.setPropertyValue(nodestate,propName,propValue);
		}
		else {
		if(this.nodeState == null)
			this.nodeState = ig_ClientState.addNode(this.WebTree.nodeState, this.Id);
			ig_ClientState.setPropertyValue(this.nodeState,propName,propValue);
		}
		if(this.WebTree.postField!=null)
			this.WebTree.postField.value = ig_ClientState.getText(this.WebTree.stateItems);	
	}
	this.insertChild=function(beforeIndex, text, className) {
		return this.WebTree._insert(this, beforeIndex, text, className);
	}
	this.addChild=function(text, className) {
		return this.WebTree._insert(this, -1, text, className);
	}
	this.removeChild=function(index) {
		var node = this.getNodes()[index];
		if(node)
			node.remove();
		ig_dispose(node);
	}
	this.edit=function() {
		igtree_beginedit(this.WebTree.Id,this.Id)
	}
	
	this.scrollIntoView=function() {
		igtree_scrollToView(this.WebTree.Element,this.Element)
	}

	
	this.remove=function() {
		var nodeArrayDeletionQueue=new Array();
		var index = this.getIndex();
		var nodeId = this.Element.id;
		var removeTop = false;
		var nextTop = null;
		var prevTop = null;
		var tree = this.WebTree;
		if(tree._removeProcess == null) {
			removeTop = true;
			tree._removeProcess = true;
			nextTop = this.getNextSibling();
			prevTop = this.getPrevSibling();
			this.update("Remove", "1");
		}
			
		var node = this.getFirstChild();
		while(node) {
			var next = node.getNextSibling();	
			node.remove();
			node.WebTree=null;
			nodeArrayDeletionQueue.push(node);
			node = next;
		}
		ig_dispose(nodeArrayDeletionQueue);
		var next = this.Element.nextSibling;
		
		var nParent = this.Element.parentNode;
		if (nParent) nParent.removeChild(this.Element);

		if(next && next.id.indexOf("M_")==0)
		{
			nParent = next.parentNode;
			if (nParent) nParent.removeChild(next);
		}
		var parentNode = this.getParent();
		if(removeTop == true) {
			tree._removeProcess = null;
			if(tree.ClassicTree) {
				if(nextTop)
					igtree_updateNodeLines(tree, nextTop);
				if(prevTop)
					igtree_updateNodeLines(tree, prevTop);
			}
			if(!nextTop && !prevTop) {
				if(parentNode) {
					next = parentNode.Element.nextSibling;
					if(next && next.id.indexOf("M_")==0)
					{
						nParent = next.parentNode;
						if (nParent) nParent.removeChild(next);
					}
					if(tree.ClassicTree) 
						igtree_updateNodeLines(tree, parentNode);
				}
			}
			
		}
		var nodeElements;
		if(parentNode == null) {
			nodeElements = igtree_getChildNodeElements(tree, null);
		}
		else {
			nodeElements = igtree_getChildNodeElements(tree, parentNode.Element);
			if(nodeElements.length == 0) {
				if(!tree.ClassicTree && tree.Expandable) {
					if(parentNode.Element.childNodes[1].imgType == "exp") {
						parentNode.Element.childNodes[2].innerHTML="";
						parentNode.Element.removeChild(parentNode.Element.childNodes[1]);
						parentNode.Element.removeChild(parentNode.Element.childNodes[1]);
						parentNode.Element.childNodes[0].style.width="16px";
					}
				}
			}
		}
		var parentId = this.Element.id;
		var ptr = parentId.lastIndexOf("_");
		parentId = parentId.substring(0, ptr);
		
		igtree_setChildIds(tree, nodeElements, index, parentId);
		var selNode = tree.getSelectedNode();
		if(selNode && (nodeId == selNode.Id))
			tree.setSelectedNode(null);
	}
	this.isChildOf = function (parent) {
		if(parent == null || typeof(parent) != "object")
			return false;
		if(this.Id.search(parent.Id) == 0) 
		   return true; 
		return false;
	}
}

function igtree_getElement() {
	return this.element;
}
function igtree_getTreeId() {
	var treeName = this.element.id;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	return treeName;
}
function igtree_getText() {
	var e = ig_getNodeTextElement(this);
	return e.innerHTML;
}
function igtree_setText(text) {
	var e = ig_getNodeTextElement(this);
	e.innerHTML=text;
	this.update("Text", text);
}
function igtree_getClass() {
	var e = ig_getNodeTextElement(this);
	return e.className;
}
function igtree_setClass(className) {
	var e = ig_getNodeTextElement(this);
	var oldClass = e.className;
	if(this.WebTree.getSelectedNode() == this)
		this.element.setAttribute("igtInitClass",className);
	else
		e.className=className;
}
function igtree_getTag() {
	var a = this.element.getAttribute("igTag");
	if(a!=null && a.length>0)
		return a;
	else
		return null;
}
function igtree_setTag(text) {
	this.element.setAttribute("igTag", text);
	this.update("Tag", text);
}
function igtree_getDataKey() {
	var a = this.element.getAttribute("igDataKey");
	if(a!=null && a.length>0)
		return a;
	else
		return null;
}
function igtree_getHiliteClass() {
	return this.element.getAttribute("HiliteClass")
}
function igtree_setHiliteClass(hiliteClass) {
	this.element.setAttribute("HiliteClass", hiliteClass)
}
function igtree_getHoverClass() {
	return this.element.getAttribute("HoverClass")
}
function igtree_setHoverClass(hoverClass) {
	this.element.setAttribute("HoverClass", hoverClass)
}
function igtree_getEnabled() {
	return(this.element.getAttribute("nodeDisabled")?false:true);
}
function igtree_setEnabled(enabled) {
	var i;
	var nodeSpan = null;
	var checkbox = null;
	for(i=0; i < this.element.childNodes.length; i++) {
		var attrib = this.element.childNodes[i].getAttribute("igTxt");
		var chk = this.element.childNodes[i].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			checkbox = this.element.childNodes[i];
		if(attrib=="1")
			nodeSpan = this.element.childNodes[i];
	}
	if(nodeSpan != null) {
		if(enabled == true) {
			this.element.removeAttribute("nodeDisabled");
			var oldClass = nodeSpan.getAttribute("oldEnabledClass");
			if(oldClass != null && oldClass.length > 0) {
				nodeSpan.className=oldClass;
				nodeSpan.removeAttribute("oldEnabledClass");
			}
			if(checkbox!=null)
				checkbox.disabled = false;
		}
		else {
			var ts = igtree_getTreeById(this.getTreeId());
			var disabledClass = ts.DisabledClass;
			this.element.setAttribute("nodeDisabled", "1");
			var oldClass = nodeSpan.className;
			nodeSpan.className=disabledClass;
			nodeSpan.setAttribute("oldEnabledClass", oldClass);
			this.element.removeAttribute("igtInitClass");
			if(checkbox!=null)
				checkbox.disabled = true;
		}
		this.update("Enabled", enabled);
	}
}
function igtree_getTargetFrame() {
	var frame = this.element.getAttribute("igFrame");
	if(ig_csom.notEmpty(frame))
		return frame;
	else
	if(ig_csom.notEmpty(this.WebTree.TargetFrame)) {
		return this.WebTree.TargetFrame;
	}
	else
		return null;
}
function igtree_setTargetFrame(frame) {
	this.element.setAttribute("igFrame", frame)
	this.update("TargetFrame", frame);
}
function igtree_getTargetUrl() {
	var url = this.element.getAttribute("igUrl");
	if(ig_csom.notEmpty(url))
		return url;
	else
	if(ig_csom.notEmpty(this.WebTree.TargetUrl)) {
		return this.WebTree.TargetUrl;
	}
	else
		return null;
}
function igtree_setTargetUrl(url) {
	this.element.setAttribute("igUrl", url)
	this.update("TargetUrl", url);
}
function igtree_setChecked(bChecked, bFireCSOMEvent) {
	var node = this.element;
	var ts = igtree_getTreeByNodeId(node.id);	
	var index=1;
	var count=node.childNodes.length;
	for(index=1;index<count;index++) {
		var chk = node.childNodes[index].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			break;
	}
	if(index >= count)
		return;
	eCheck = node.childNodes[index];
	if(bChecked==false) {
		eCheck.checked=false;
	}
	else {
		eCheck.checked=true;
	}
	if(bFireCSOMEvent!=false) {
		var tn = this.getTreeId();
		var nodeId = this.Id;
		if(igtree_fireEvent(tn,ts.Events.NodeChecked,"(\""+tn+"\",\""+nodeId+"\", eCheck.checked)")) {
			eCheck.checked = oldValue;
			return;
		}
	}	
	igtree_updateNodeCheck(ts, this.element.id, bChecked);
}

function igtree_getChecked() {
	var node = this.element;
	var index=1;
	var count=node.childNodes.length;
	for(index=1;index<count;index++) {
		var chk = node.childNodes[index].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			break;
	}
	return this.element.childNodes[index].checked;
}
function igtree_hasCheckbox() {
	var index;
	for(index=1;index<this.element.childNodes.length;index++) {
		var chk = this.element.childNodes[index].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			return true;
	}
	return false;
}
function igtree_hasChildren() {
	if(this.element.nextSibling && this.element.nextSibling.id == "M_"+this.element.id)
		return true;
	else
		return false;
}

function igtree_hasChildrenElements(nodeElement) {
	if(nodeElement.nextSibling && nodeElement.nextSibling.id == "M_"+nodeElement.id)
		return true;
	else
		return false;
}

function igtree_getExpanded() {
	var expEl=igtree_getElementById("M_"+this.element.id);
	if(expEl != null)
		return(expEl.style.display!="none");
	else
		return false;
}
function igtree_setExpanded(expand) {
	if(expand == true)
		igtree_expandNode(this);
	else
		igtree_collapseNode(this);
}
function igtree_getSelected() {
	var treeName = this.element.id;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	return (this.Element==igtree_selectedNode(treeName));
}
function igtree_setSelected(bSelect) {
	var treeName = this.element.id;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	if(bSelect) {
		igtree_setSelectedNode(treeName, this.element.id)
	}
	else {
		if(this.Element==igtree_selectedNode(treeName))
			igtree_setSelectedNode(treeName, null)
	}
}

// private - Initializes an event
function igtree_fireEvent(tn,eventObj,eventString)
{
	var ts=igtree_treeState[tn];
	var result=false;
	if(eventObj[0]!="")
		result=eval(eventObj[0]+eventString);
	if(ts.TreeLoaded && result!=true && eventObj[1]==1 && !ts.CancelPostBack)
		igtree_needPostBack(tn);
	ts.CancelPostBack=false;
	return result;
}

// private
var igtree_lastActiveTree="";
if(!igtree_IE)
	if(window.addEventListener)
		window.addEventListener('keydown',igtree_windowKeyDown,false);

// private
function igtree_windowKeyDown(evnt)
{
	if(igtree_lastActiveTree!="" && evnt.keyCode!=13)
		if(igtree_keydown(evnt,igtree_lastActiveTree)==true)
		{
			evnt.stopPropagation();
			evnt.preventDefault();
		}
}

// private
function igtree_initEvent(se)
{
	this.target=se;
}

// public - Returns the selected Node for the Tree 
function igtree_selectedNode(tn) 
{
	return igtree_treeState[tn].selectedNodeElement;
}

// private - Implements the setSelectedNode method for the tree
function igtreem_setSelectedNode(node)
{
	var uniqueId = this.getClientUniqueId();
	var id=null;
	if(node!=null)
		id=node.Id;
	igtree_setSelectedNode(uniqueId, id);
}

// private - Implements the getSelectedNode method for the tree
function igtreem_getSelectedNode()
{
	var uniqueId = this.getClientUniqueId();
	var node=igtree_selectedNode(uniqueId);
	if(node)
		return igtree_getNodeById(node.id);
	return null;
}

// private
function igtree_getClientUniqueId() {
	var u = this.UniqueId.replace(/:/gi, "");
	u = u.replace(/_/gi, "");
	return u;
}

// private

// Inserts a node to the tree
function igtree_insertChild(parentNode, index, text, className) {
	// obtain the Nodes collection for the operation
	var nodes;
	var nodeElements;
	var parentId;
	var insert = false;
	if(index!=-1)
		insert = true;
	
	if(parentNode == null) {
		//nodes = this.getNodes(true);
		nodes = this.getNodes(false);
		if(this.nodes)
			this.nodes = null;
		parentId = this.Id;
	}
	else {
		nodes = parentNode.getChildNodes();
		if(parentNode.nodes)
			parentNode.nodes = null;
		parentId = parentNode.Id;
		if(nodes == null || nodes.length == 0) { // create childNodes div
			var mdiv = window.document.createElement("DIV");
			mdiv.id = "M_"+parentId;
			var next = parentNode.Element.nextSibling;
			if(next != null)
				parentNode.Element.parentNode.insertBefore(mdiv, next);
			else		
				parentNode.Element.parentNode.appendChild(mdiv);		
			
			var span = parentNode.Element.firstChild;
			span.style.width = '0px';
			if(!this.ClassicTree && this.Expandable) {
				var ig = window.document.createElement("IMG");
				ig.imgType = "exp";
				ig.src = this.CollapseImage;
				parentNode.Element.insertBefore(ig, parentNode.Element.childNodes[1]);
				ig = window.document.createElement("SPAN");
				ig.style.width="5px";
				ig.innerHTML="&nbsp;";
				parentNode.Element.insertBefore(ig, parentNode.Element.childNodes[2]);
			}
			igtree_updateNodeToggle(this, parentId, true);
			if(ig_csom.notEmpty(this.ParentNodeClass))
				if(parentNode.getParent() != null || ig_csom.isEmpty(this.RootNodeClass))
					parentNode.setClass(this.ParentNodeClass);
		}
	}
	if(index == null || index == -1 || index > nodes.length)
		index = nodes.length;
		
	// create the node DOM structure
	var div = window.document.createElement("DIV");

	var mrgn = 0;
	var parentLevel = null;
	var levelIndex;
	if(parentNode) {
		levelIndex = parentNode.getLevel();
		parentLevel = this.Levels.getItem(levelIndex);
		levelIndex++;
		mrgn = levelIndex * this.Indentation;
	}
	else {
		levelIndex = 0;
	}
	var level = this.Levels.getItem(levelIndex);
		
	var html;
	if(this.ClassicTree) {
		marginHtml = "<span igl='1'></span>";
	}
	else {
		var marginHtml = "<span style='width:16px;margin-left:%MARGIN%;'></span>"; 
		marginHtml = marginHtml.replace("%MARGIN%", mrgn);
	}
	html = marginHtml;
	
	var bCheck = false;
	var checkboxHtml = "<input class='igt_align' type='checkbox' igchk='1'>";
	if(level && level.LevelCheckBoxes==2)
		bCheck = true;
	else
	if(level && level.LevelCheckBoxes==1)
		bCheck = false;
	else
		bCheck = this.CheckBoxes;
	if(bCheck)
		html += checkboxHtml;
	
	var img = "<img igimg='1' style='margin-right:4px' align='absmiddle' src='%IMAGE%'>";
	var imgurl = "";
	if(level && ig_csom.notEmpty(level.LevelImage))
		imgurl = level.LevelImage;
	else
	if(parentNode == null && ig_csom.notEmpty(this.RootNodeImageUrl)) { // Root Node
		imgurl = this.RootNodeImageUrl;
	}
	else
	if(ig_csom.notEmpty(this.LeafNodeImageUrl)) {
		imgurl = this.LeafNodeImageUrl;
	}
	else
	if(ig_csom.notEmpty(this.DefaultImage)) {
		imgurl = this.DefaultImage;
	}
	if(imgurl.length > 0) {
		img = img.replace("%IMAGE%", imgurl);
		html += img;
	}
	
	var cls = null;
	if(className != null)
		cls = className;
	else
	if(level && level.ClassName != null)
		cls = level.ClassName;
	else
	if(levelIndex == 0 && ig_csom.notEmpty(this.RootNodeClass))
		cls = this.RootNodeClass;
	else
	if(ig_csom.notEmpty(this.LeafNodeClass))
		cls = this.LeafNodeClass;
		
	var txt = "<span %CLASS%igtxt='1'>%TEXT%</span>";
	if(ig_csom.notEmpty(cls))
		txt = txt.replace("%CLASS%", "class='"+cls+"' ");
	else
		txt = txt.replace("%CLASS%", "");
	
	txt = txt.replace("%TEXT%", text);
	html += txt;
	
	div.innerHTML = html;
	var parentElem = ig_csom.getElementById("M_"+parentId);
	if(parentElem == null) return;
	
	if(insert && parentElem.childNodes.length <= index)  // convert to an Add operation
		insert = false;
		
	// construct the nodeId
	div.id = parentId + '_' + (index+1);
	var nodeId = div.id;
	// insert the node into the tree DOM
	if(insert) { // Perform insert
		if(parentNode == null) {
			nodeElements = igtree_getChildNodeElements(this, null);
		}
		else {
			nodeElements = igtree_getChildNodeElements(this, parentNode.Element);
		}
		var beforeElem = nodeElements[index];


		div.id = this.Id + "_ig_temp";
		parentElem.insertBefore(div, beforeElem);		
		if(parentNode == null) {
			nodeElements = igtree_getChildNodeElements(this, null);
		}
		else {
			nodeElements = igtree_getChildNodeElements(this, parentNode.Element);
		}
		div.id = nodeId;
		
		var parId = div.id;
		var ptr = parId.lastIndexOf("_");
		parId = parId.substring(0, ptr);
		igtree_setChildIds(this, nodeElements, index+1, parId);
	}
	else  
		parentElem.appendChild(div);	
			
	var node = igtree_getNodeById(nodeId);
	if(this.ClassicTree) {
		if(parentNode)
			igtree_updateNodeLines(this, parentNode, null);
		igtree_updateNodeLines(this, node, levelIndex);
	}
	if(node.WebTree == null)
		node.WebTree = this;
		
	if(insert) 
		node.update("Add", index.toString());
	else
		node.update("Add", "-1");
	if(text)
		node.update("Text", text);
	if(className)
		node.update("CssClass", className);
	return node;
}

function igtree_setChildIds(tree, nodeElements, index, parentId) {
	var len = nodeElements.length;
	if(index==null || index>=len)
		index=0;	
	var i;
	for(i=index;i<len;i++) {
		var nodeElement = nodeElements[i];
		var id = parentId;
		x = i+1;
		id += "_" + x.toString();
		var childNodeElements;
		var bSetChildren = igtree_hasChildrenElements(nodeElement);
		if(bSetChildren)
			childNodeElements = igtree_getChildNodeElements(tree, nodeElement);
			
		nodeElement.id = id;
		if(nodeElement.oNode != null)
			nodeElement.oNode.Id = id;
		if(bSetChildren) {
			igtree_setChildIds(tree, childNodeElements, 0, id);
		}
		var nextSibling = nodeElement.nextSibling;
		if(nextSibling != null && nextSibling.id.substr(0,2)=="M_") {
			nextSibling.id = "M_" + id;
		}
	}
}

function igtree_getChildNodeElements(tree, parentNodeElement) {
	elements=new Array();
	var nodeCount=0;
	var childNodeElement=igtree_getFirstChildNodeElement(tree, parentNodeElement);
	while(childNodeElement)	{
		elements[nodeCount++]=childNodeElement;
		childNodeElement=igtree_getNextChildNodeElement(tree, childNodeElement);
	}
	return elements;
}

function igtree_getFirstChildNodeElement(tree,parentNodeElement) {
	var sibling;
	if(!parentNodeElement)	{
		sibling=tree.treeElement.childNodes[0].childNodes[0].childNodes[0];
		return sibling;
	}
	sibling = parentNodeElement.nextSibling;
	if(!sibling)
		return null;
	if(sibling.id.substr(0,2)!="M_")
		return null;
	var firstChild = sibling.firstChild;	
	return firstChild;
}

function igtree_getNextChildNodeElement(tree,nodeElement) {
	if(!nodeElement)
		return;
	var sibling = nodeElement.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	return sibling;
}

function igtree_updateNodeLines(tree, node, levelIndex) {
	var parent = node.getParent();
	if(levelIndex == null)
		levelIndex = node.getLevel();
	if(parent)
		igtree_updateNodeDescendantLines(tree, parent)
	igtree_updateLines(tree, node, levelIndex);
	igtree_updateNodeDescendantLines(tree, node)
	var prev = node.getPrevSibling();
	if(prev) {
		igtree_updateLines(tree, prev, levelIndex);
		igtree_updateNodeDescendantLines(tree, prev)
	}
	var next = node.getNextSibling();
	if(next) {
		igtree_updateLines(tree, next, levelIndex);
		igtree_updateNodeDescendantLines(tree, next)
	}
}

function igtree_updateNodeDescendantLines(tree, parentNode) {
	var node = parentNode.getFirstChild();
	while(node) {
		igtree_updateLines(tree, node, node.getLevel());
		if(node.getFirstChild())
			igtree_updateNodeDescendantLines(tree, node) 
		node = node.getNextSibling();	
	}
}

// private
function igtree_updateLines(tree, node, levelIndex) {
	var eLines = igtree_getLinesElement(node);
	var html = igtree_WriteLines(tree, node, levelIndex);
	eLines.innerHTML = html;
}

// private
function igtree_getLinesElement(node) {
	var e = node.Element;
	var eLine = e.firstChild;
	if(eLine.igl == '1')
		return eLine;
}

// private
function igtree_WriteLines(tree, node, level) {
	// write the level line images
	var s;
	if(node.getFirstChild() && tree.Expandable) {
		s = igtree_WriteLineLevelImage(tree, node, level);
		if(node.getExpanded())
			s += igtree_WriteCollapseImage(tree, node) 
		else
			s += igtree_WriteExpandImage(tree, node) 
	}
	else {
		s = igtree_WriteLineLevelImage(tree, node, level);
		s += igtree_WriteJoinerImage(tree, node);
	}
	return s;
}

// private
function igtree_WriteLineLevelImage(tree, node, level) {
	var list = new Array();

	var parent = node.getParent();
	var s = "";
	var i = 0;
	while(parent != null) {
		list[i++] = parent;
		parent = parent.getParent();
	}
	for(j = list.length-1; j >= 0; j--) {
		parent = list[j];
		s += "<img align='absmiddle' ";
		s += "src='";
		if(parent.getNextSibling() != null && !parent.getNextSibling().Hidden)
			s += igtree_resolveImage(tree, "ig_treeI.gif");
		else
			s += igtree_resolveImage(tree, "ig_treewhite.gif");
		s += "'>";
	}
	return s;
}

function igtree_WriteCollapseImage(tree, node) {
	var s = "<img style='vertical-align:middle;' src='";
	if(node.getParent() == null) {
		if((node.getPrevSibling() == null) &&
			(node.getNextSibling() == null)) {
			s += igtree_resolveImage(tree, "ig_treeominus.gif");
		}
		else
			if(node.getPrevSibling() == null) {
			s += igtree_resolveImage(tree, "ig_treefminus.gif");
		}
		else
			if(node.getNextSibling() == null) 
			s += igtree_resolveImage(tree, "ig_treelminus.gif");
		else
			s += igtree_resolveImage(tree, "ig_treemminus.gif");
	}
	else
	if(node.getPrevSibling() == null) {
		if(node.getNextSibling() == null) 
			s += igtree_resolveImage(tree, "ig_treelminus.gif");
		else
			s += igtree_resolveImage(tree, "ig_treemminus.gif");
	}
	else
	if(node.getNextSibling() == null) 
		s += igtree_resolveImage(tree, "ig_treelminus.gif");
	else
		s += igtree_resolveImage(tree, "ig_treemminus.gif");
	s += "' imgType='exp'>";
	return s;
}

function igtree_WriteExpandImage(tree, node) {
	var s = "<img style='vertical-align:middle;' src='";
	if(node.getParent() == null) {
		if((node.getPrevSibling() == null || node.getPrevSibling().Hidden) &&
			(node.getNextSibling() == null || node.getNextSibling().Hidden) ) {
			s += igtree_resolveImage(tree, "ig_treeoplus.gif");
		}
		else
			if(node.getPrevSibling() == null || node.getPrevSibling().Hidden) {
			if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
				s += igtree_resolveImage(tree, "ig_treelplus.gif");
			else
				s += igtree_resolveImage(tree, "ig_treefplus.gif");
		}
		else
			if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
			s += igtree_resolveImage(tree, "ig_treelplus.gif");
		else
			s += igtree_resolveImage(tree, "ig_treemplus.gif");
	}
	else
		if(node.getPrevSibling() == null || node.getPrevSibling().Hidden) {
		if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
			s += igtree_resolveImage(tree, "ig_treelplus.gif");
		else
			s += igtree_resolveImage(tree, "ig_treemplus.gif");
	}
	else
		if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
		s += igtree_resolveImage(tree, "ig_treelplus.gif");
	else
		s += igtree_resolveImage(tree, "ig_treemplus.gif");
	s += "' imgType='exp'>";
	return s;
}

function igtree_WriteJoinerImage(tree, node) {
	var s = "<img style=\"vertical-align:middle;\" src='";
	// The image to render can be either for the first node, a middle node, or the last node of a collection
	if (null==node.getParent()&&null==node.getPrevSibling()&&null==node.getNextSibling()&&null==node.getChildNodes())
		s += igtree_resolveImage(tree, "ig_treeS.gif");
	else
	if(node.getParent() == null && node.getPrevSibling() == null)
		s += igtree_resolveImage(tree, "ig_treeF.gif");
	else
		if(node.getNextSibling() == null)
		s += igtree_resolveImage(tree, "ig_treeL.gif");
	else
		if(node.getPrevSibling() == null)
		s += igtree_resolveImage(tree, "ig_treeT.gif");
	else
		s += igtree_resolveImage(tree, "ig_treeT.gif");
	s += "'>";
	return s;
}

// private
function igtree_resolveImage(tree, image) {
    if(tree.treeElement.clr2 != null)
        return "Infragistics.WebUI.UltraWebNavigator.IMG." + image;
    else
	    return tree.ImageDirectory + image;
}

// private
function igtree_getTreeNodes(all){
	if(all == undefined)
		all = false;
	var nodes=new Array();
	var nodeCount=0;
	var uniqueId = this.getClientUniqueId();	
	var node=igtree_nextSibling(uniqueId,null, !all);
	while(node)	{
		nodes[nodeCount++]=igtree_getNodeById(node.id);
		node=igtree_nextSibling(uniqueId,node, !all);
	}
	return nodes;
}

function igtree_nextNode(tn,node,all) {
	var ts=igtree_treeState[tn];
	var sibling;
	sibling = node.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || all && sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.nextSibling;
		while(sibling && (sibling.tagName!="DIV" ))
			sibling=sibling.nextSibling;
		parentNode=parentNode.parentNode;
	}
	if(sibling && sibling.id.substr(0,2)=="M_")
		sibling=sibling.childNodes[0];
	while(sibling && (sibling.tagName!="DIV" ))
		sibling=sibling.nextSibling;
	return sibling;
}

// private - Implements getNextSibling for the Node object
function igtree_getNodeNextSibling(){
	var node=igtree_nextSibling(this.getTreeId(),this.element,true);
	if(node)
		node=igtree_getNodeById(node.id);
	return node;
}

// private - Implements GetPrevSibling for the Node object
function igtree_getNodePrevSibling(){
	var node=igtree_prevSibling(this.getTreeId(),this.element,true);
	if(node)
		node=igtree_getNodeById(node.id);
	return node;
}

// private - Implements FirstChild for the Node object
function igtree_getNodeFirstChild(){
	var node=null;
	if(this.hasChildren()) {
		var expEl=this.element.nextSibling;
		if(expEl) {
			var child = expEl.firstChild;
			if(child) {
				var id = child.id;
				node=igtree_getNodeById(id);
			}
		}
	}
	return node;
}

// private - Implements the Parent property for the Node object
function igtree_getNodeParent(){
	var node=null;
	var nodeName=this.element.id.split("_")
	if(nodeName.length>2)
	{
		var parentName=this.element.id.substr(0,this.element.id.length-nodeName[nodeName.length-1].length-1);
		node=igtree_getNodeById(parentName);
	}
	return node;
}

// private - Implements Nodes collection property for the Node object
function igtree_getChildNodes(){
	var nodes=new Array();
	var nodeCount=0;
	var node=this.getFirstChild();
	while(node)	{
		nodes[nodeCount++]=node;
		node=node.getNextSibling();
	}
	return nodes;
}

// private
function igtree_firstNode(tn){
	return igtree_nextSibling(tn,null);
}

// private
function igtree_lastNode(tn) {
	return igtree_prevSibling(tn,null);
}

// private
function igtree_nextNode(tn,node){
	return igtree_nextSibling(tn,node,true);
}

// private
function igtree_prevNode(tn,node) {
	return igtree_prevSibling(tn,node,true);
}

// private
function igtree_nextVisibleNode(tn,node) {
	return igtree_nextSibling(tn,node);
}

// private
function igtree_prevVisibleNode(tn,node) {
	return igtree_prevSibling(tn,node);
}
//private
function igtree_clearNodes() {
	var nodes = this.getNodes();
	var len = nodes.length;
	var i;
	for(i=0;i<len;i++) {
		var node=nodes[i];
		node.remove();
		node.WebTree=null;
		ig_dispose(node);
	}
	ig_dispose(nodes);
	return this.nodes = null;
}
// private
var ig_currDropNode;
function igtree_dragstart(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	var node = tree.getNodeById(eNode.id);
	if(!tree.AllowDrag || !node.getEnabled()) {
		ig_cancelEvent(evnt);
		return false;
	}
	ig_dataTransfer = new ig_DataTransferObject(evnt.dataTransfer, treeId, node);
	ig_csom.dataTransfer = ig_dataTransfer;
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragStart,node,ig_dataTransfer,evnt);
}

// [KV 10/29/2004, 3:39 PM] add selected text range
var ig_treeSelectedTextRange = null;

function igtree_preselect() {
	var e;
	e = window.event.srcElement;
	if(!e.igtxt) return;
	var eNode = e.parentNode;
	var node = igtree_getNodeById(eNode.id);
	if(!node) return;
	if(!node.WebTree.AllowDrag)
		return;
	if(!node.getEnabled())
		return;
	igtree_bDragSelect = true;
	r = document.body.createTextRange();
	r.moveToElementText(e);
	r.select();
	
	// [KV 10/29/2004, 3:40 PM] save the selection, so that we unselect it later.
	ig_treeSelectedTextRange = r;
  
	window.event.cancelBubble = true;
	igtree_bDragSelect = false;
}

function igtree_drag(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	var node = tree.getNodeById(eNode.id);
	var oEvent = igtree_fireEvent1(tree,tree.Events.Drag,node,ig_dataTransfer,evnt);
}
function igtree_dragend(evnt, treeId) 
{
	igtree_clearCurrDropNode();
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
		
	// Fix for UWN892
	// [KV 10/29/2004, 1:23 PM] ig_currDropNode should be reset to default class style.
	if(ig_currDropNode && ig_currDropNode != src) {
		var e = ig_getNodeTextElement(ig_currDropNode);
		if(e && e.igdrop != null) {
			e.className = e.igdrop;
		}
		else
		if(e)
			e.className = "";
	}
	// [KV 10/29/2004, 3:41 PM] Unselect the selected range.
	if (ig_treeSelectedTextRange) 
	{
		ig_treeSelectedTextRange.execCommand("Unselect");
		delete ig_treeSelectedTextRange;
		ig_treeSelectedTextRange = null;
	}
	
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	var node = tree.getNodeById(eNode.id);
	evnt.returnValue = false;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragEnd,node,ig_dataTransfer,evnt);
}

function igtree_clearCurrDropNode() {
	if(ig_currDropNode) {
		var e = ig_getNodeTextElement(ig_currDropNode);
		if(e && e.igdrop != null) 
			e.className = e.igdrop;
	}
}

function igtree_dragenter(evnt, treeId) 
{
	if(!ig_dataTransfer)
		ig_dataTransfer = new ig_DataTransferObject(evnt.dataTransfer, null, null);

	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	if(!tree.AllowDrop) {
		return;
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	if(eNode.id.indexOf(tree.Id) == -1)
		return;
	var node = tree.getNodeById(eNode.id);
	if(ig_currDropNode != node) {
		igtree_clearCurrDropNode();
		ig_currDropNode = node;
		var selClass = tree.HiliteClass;
		e = ig_getNodeTextElement(node);
		if(e) {
			e.igdrop = e.className;
			e.className = selClass;
		}
	}
	evnt.returnValue = false;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragEnter,node,ig_dataTransfer,evnt);
}
function igtree_dragover(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	if(eNode.id.indexOf(tree.Id) == -1)
		return;
	if(!tree.AllowDrop) {
		return;
	}
	var node = tree.getNodeById(eNode.id);
	if(ig_dataTransfer.sourceObject == node) {
		evnt.dataTransfer.effectAllowed = "none";
		return;
	}
	if(!node.getEnabled()) {
		evnt.dataTransfer.effectAllowed = "none";
		return false;
	}
	evnt.returnValue = false;  
	
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragOver,node,ig_dataTransfer,evnt);
}
function igtree_dragleave(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	if(eNode.id.indexOf(tree.Id) == -1)
		return;
	var node = tree.getNodeById(eNode.id);
	if(ig_currDropNode != node) {
		var e = ig_getNodeTextElement(node);
		if(e && e.igdrop != null) {
			e.className = e.igdrop;
		}
		else
		if(e)
			e.className = "";
	}
	evnt.returnValue = false;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragLeave,node,ig_dataTransfer,evnt);
}
function igtree_drop(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	if(!tree.AllowDrop) {
		return;
	}
	var node;
	if(src.id == "T_" + treeId)
		node = null;
	else {
		var eNode = igtree_getNodeElement(src);
		if(!eNode)	return;
		node = tree.getNodeById(eNode.id);
	}
	if(ig_dataTransfer.sourceObject == node)
		return;
	evnt.returnValue = true;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.Drop,node,ig_dataTransfer,evnt);
	if(tree.NeedPostBack && node) {
		tree.NeedPostBack=false;
		// [KV 10/29/2004, 7:07 PM] ref:UWN870, fix the ":" splitting problem, escape the data before sending.
		__doPostBack(tree.UniqueId,node.Id+":Dropped:"+ig_dataTransfer.sourceName+":"+ escape(ig_dataTransfer.dataTransfer.getData("Text")) );
		//__doPostBack(tree.UniqueId,node.Id+":Dropped:"+ig_dataTransfer.sourceName+":"+ ig_dataTransfer.dataTransfer.getData("Text"));
	}
	ig_currDropNode = null;
	if(node) {
		var e = ig_getNodeTextElement(node);
		if(e) {
			if(e.igdrop != null) {
				e.className = e.igdrop;
			}
			else
				e.className = "";
		}
	}
	if(ig_dataTransfer) {
		ig_dataTransfer = null;
	}
}
function igtree_fireEvent1(oTree,eventObj,oNode,dataTransfer,evnt) {
	var oEvent = new ig_EventObject();
	oEvent.event = evnt;
	ig_fireEvent(oTree,eventObj[0],oNode,dataTransfer,oEvent);
	if(oTree.TreeLoaded && eventObj[1]==1 && !oTree.CancelPostBack)
		oTree.NeedPostBack = true;
	oTree.CancelPostBack=false;
	return oEvent;
}
var ig_dataTransfer;
function ig_DataTransferObject(dataTransfer, sourceName, sourceObject) {
	this.dataTransfer = dataTransfer;
	this.sourceName = sourceName;
	this.sourceObject = sourceObject;
}

function ig_getNodeTextElement(node) {
	var i;
	for(i=0; i < node.element.childNodes.length; i++) {
		var attrib = node.element.childNodes[i].getAttribute("igTxt");
		if(attrib=="1")
			return node.element.childNodes[i];
	}
	return null;
}


if (window.addEventListener) {
	window.addEventListener("onload",igtree_loadcomplete,false);
}
else if (window.attachEvent) {
	window.attachEvent("onload",igtree_loadcomplete);
}

