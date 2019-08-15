/* 
Infragistics Web Combo Script 
Version 5.1.20051.37
Copyright (c) 2001-2004 Infragistics, Inc. All Rights Reserved.
*/

var igcmbo_displaying;
var igcmbo_currentDropped;

// private - hides all dropdown select controls for the document.
var igcmbo_hidden=false;
function igcmbo_hideDropDowns(bHide) { 
	 if(igcmbo_dropDowns == null)
		return;
     if(bHide)
     {
		if(igcmbo_hidden)
			return;
		igcmbo_hidden = true;
         for (i=0; i<igcmbo_dropDowns.length;i++)
              igcmbo_dropDowns[i].style.visibility='hidden';
     }
     else
     {
         for (i=0; i<igcmbo_dropDowns.length;i++)
         {
              igcmbo_dropDowns[i].style.visibility='visible';
         }
         igcmbo_hidden = false;
     }
}

var wccounter=0;
function igcmbo_onmousedown(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	var src = igcmbo_srcElement(evnt);
	oCombo.highlightText();
	if(oCombo.Editable && src.id == id + "_input") return;
	ig_inCombo = true;	
	oCombo.Element.setAttribute("noOnBlur",true);
	if(igcmbo_currentDropped != null && igcmbo_currentDropped != oCombo)
		igcmbo_currentDropped.setDropDown(false);
	if(oCombo.getDropDown() == true) {
		oCombo.setDropDown(false);
		igcmboObject = oCombo;
		if(document.all)
			setTimeout('igcmbo_focusEdit()', 10);
	}
	else {
		igcmbo_swapImage(oCombo, 2);
		oCombo.setDropDown(true);
	}
	window.setTimeout("igtbl_cancelNoOnBlurDD()",100);
}

var igcmboObject = null;
function igcmbo_focusEdit() {
	igcmboObject.setFocusTop();

}

function igcmbo_onmouseup(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	if(oCombo.Dropped == true) {
		igcmbo_swapImage(oCombo, 1);
	}
	else {
	}
}

function igcmbo_onmouseout(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	if(oCombo.Dropped == true) {
		igcmbo_swapImage(oCombo, 1);
	}
	else {
	}
}

function igcmbo_swapImage(combo, imageNo) {
	var img = igcmbo_getElementById(combo.ClientUniqueId + "_img");
	if(imageNo == 1) img.src = combo.DropImage1;
	else img.src = combo.DropImage2;
}

function igcmbo_ondblclick(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	if(oCombo.getDropDown() == true) {
		oCombo.setDropDown(false);
	}
}


function igcmbo_onKeyDown(evnt) {
	if(evnt.keyCode == 40) { // down arrow
	}
}

// public - Retrieves the server-side unique id of the combo
function igcmbo_getUniqueId(comboName) {
	var combo = igcmbo_comboState[comboName];
	if(combo != null)
		return combo.UniqueId;
	return null;
}
function igcmbo_getElementById(id) {
        if(document.all)
			return document.all[id];
        else 
			return document.getElementById(id);
}

// public - returns the combo object for the Item Id
function igcmbo_getComboById(itemId) {
	var id = igcmbo_comboIdById(itemId);  
	return igcmbo_comboState[id];
}

// public - returns the combo object from an Item element
function igcmbo_getComboByItem(item) {
	var id = igcmbo_comboIdById(item.id);  
	return igcmbo_comboState[id];
}

// public - returns the combo Name from an itemId
function igcmbo_comboIdById(itemId) {
   var comboName = itemId;
   var strArray = comboName.split("_");
   return strArray[0];
}

function igcmbo_getLeftPos(e) {
	x = e.offsetLeft;
	if(e.style.position=="absolute")
		return x;
	tmpE = e.offsetParent;
	while (tmpE != null) {
		if(tmpE.style.overflowX && tmpE.style.overflowX!="visible" || tmpE.style.overflow && tmpE.style.overflow!="visible")
			break;
		if((tmpE.style.position!="relative") && (tmpE.style.position!="absolute"))
			x += tmpE.offsetLeft;
		tmpE = tmpE.offsetParent;
	}
	return x;
}

function igcmbo_getTopPos(e) {
	y = e.offsetTop;
	if(e.style.position=="absolute")
		return y;
	tmpE = e.offsetParent;
	while (tmpE != null) {
		if(tmpE.style.overflowY && tmpE.style.overflowY!="visible" || tmpE.style.overflow && tmpE.style.overflow!="visible")
			break;
		if((tmpE.style.position!="relative") && (tmpE.style.position!="absolute"))
			y += tmpE.offsetTop;
		tmpE = tmpE.offsetParent;
	}
	return y;
}

// Warning: Private functions for internal component usage only
// The functions in this section are not intended for general use and are not supported
// or documented.

// private - Fires an event to client-side script and then to the server is necessary
function igcmbo_fireEvent(id,eventObj,eventString){
	var oCombo=igcmbo_comboState[id];
	var result=false;
	if(eventObj[0]!="")
		result=eval(eventObj[0]+eventString);
	if(oCombo.Loaded && result!=true && eventObj[1]==1 && !oCombo.CancelPostBack)
		oCombo.NeedPostBack = true;
	oCombo.CancelPostBack=false;
	return result;
}

// private - Performed on page initialization
function igcmbo_initialize() 
{
	if(typeof(window.igcmbo_initialized)=="undefined")
	{
		if(typeof(ig_csom)=="undefined" || ig_csom==null)
			return;
		ig_csom.addEventListener(document, "mousedown", igcmbo_mouseDown, false);
		ig_csom.addEventListener(document, "mouseup", igcmbo_mouseUp, false);

		window.igcmbo_initialized=true;
		ig_currentCombo = null;
	}
}

var igcmbo_comboState=[];
var igcmbo_dropDowns;

// private - initializes the combo object on the client
function igcmbo_initCombo(comboId) {

   var comboElement = igcmbo_getElementById(comboId+"_Main");
   var oCombo = new igcmbo_combo(comboElement,eval("igcmbo_"+comboId+"_Props"));
   igcmbo_comboState[comboId] = oCombo;
   igcmbo_fireEvent(comboId,oCombo.Events.InitializeCombo,"(\""+comboId+"\");");
   if(document.all != null && oCombo.HideDropDowns==true && igcmbo_dropDowns==null) {
		igcmbo_dropDowns = document.all.tags("SELECT");
   }
   oCombo.Loaded = true;
   return oCombo;
}

// private - constructor for the combo object
function igcmbo_combo(comboElement,comboProps) 
{
	igcmbo_initialize();

	this.Id=comboElement.id;
	this.Element=comboElement;
	this.Type="WebCombo";
	this.UniqueId=comboProps[0];
	this.DropDownId=comboProps[1];
	this.DropDownId=this.DropDownId.replace(":", "");
	this.DropDownId=this.DropDownId.replace("_", "x")+ "_main";
	this.DropImage1=comboProps[2];
	this.DropImage2=comboProps[3];
	this.ForeColor=comboProps[9];
	this.BackColor=comboProps[10];
	this.SelForeColor=comboProps[11];
	this.SelBackColor=comboProps[12];
	this.DataTextField=comboProps[13];
	this.DataValueField=comboProps[15];
	this.HideDropDowns=comboProps[17];
	this.Editable=comboProps[18];
	this.ClassName=comboProps[19];
	this.Prompt=comboProps[20];
	this.ComboTypeAhead=comboProps[22];
    
   	var uniqueId = igcmbo_getClientUniqueId(this.UniqueId);
	this.ClientUniqueId = uniqueId;
    
	this.Events= new igcmbo_events(eval("igcmbo_"+uniqueId+"_Events"));
	this.ExpandEffects = new igcmbo_expandEffects(comboProps[4], comboProps[5], comboProps[6], comboProps[7], comboProps[8], comboProps[9]);

	this.Loaded=false;
	this.Dropped = false;
	this.NeedPostBack=false;
	this.CancelPostBack=false;
	this.TopHoverStarted=false;
	
	this.getDropDown = igcmbo_getDropDown;
	this.setDropDown = igcmbo_setDropDown;
	this.getDisplayValue = igcmbo_getDisplayValue;
	this.setDisplayValue = igcmbo_setDisplayValue;
	this.getDataValue = igcmbo_getDataValue;
	this.setDataValue = igcmbo_setDataValue;
	this.setWidth = igcmbo_setWidth;
	this.getWidth = igcmbo_getWidth;
	this.getSelectedIndex = igcmbo_getSelectedIndex;
	this.setSelectedIndex = igcmbo_setSelectedIndex;
	this.selectedIndex = comboProps[21];
	this.setFocusTop = igcmbo_setFocusTop;
	this.updateValue = igcmbo_updateValue;
	this.updatePostField = igcmbo_updatePostField;
	this.setSelectedRow = igcmbo_setSelectedRow;
	this.grid = grid;
	var grid = igtbl_getElementById(this.ClientUniqueId + "xGrid");
	if(grid!=null)
		grid.setAttribute("igComboId", this.ClientUniqueId);
	grid = igtbl_getGridById(this.ClientUniqueId + "xGrid");
	this.grid = grid;
	this.getGrid = igcmbo_getGrid;
	var innerctl;
	
	innerctl = igcmbo_getElementById(this.ClientUniqueId + "_input");
	this.displayValue = innerctl.value;	 
	this.setWidth(this.Element.offsetWidth);
	
	// begin - editor control support
	igcmbo_getElementById(this.UniqueId).Object=this;
	this.getVisible = igcmbo_getVisible;
	this.setVisible = igcmbo_setVisible;
	this.getValue = igcmbo_getValue;
	this.setValue = igcmbo_setValue;
	this.eventHandlers=new Object();
	this.addEventListener=igcmbo_addEventListener;
	this.removeEventListener=igcmbo_removeEventListener;
	// end - editor control support
	
	this.keyCount=0;
	this.typeAheadTimeout=null;
	this.highlightText=igcmbo_highlightText;
}

// public - sets the width of the WebCombo to the passed in value
function igcmbo_setWidth(width) {
	var innerctl;
	if(width==0)
		return;
	var border = 6;
	if(document.all)
		border = this.Element.offsetWidth - this.Element.clientWidth;
	var image = igcmbo_getElementById(this.ClientUniqueId + "_img");	
	innerctl = igcmbo_getElementById(this.ClientUniqueId + "_input");
	innerctl.style.width =  width - image.offsetWidth - border > 0 ? width - image.offsetWidth - border : width - image.offsetWidth;
	this.Element.style.width = width;
}

// public - returns the CSS width of the combo element.
function igcmbo_getWidth() {return this.Element.style.width;}

// private - event initialization for menu object
function igcmbo_expandEffects(duration, opacity, type, shadowColor, shadowWidth, delay){
	this.Duration=duration;
	this.Opacity=opacity;
	this.Type=type;
	this.ShadowColor=shadowColor;
	this.ShadowWidth=shadowWidth;
	this.Delay=delay;
}

// private - event initialization for combo object
function igcmbo_getDropDown(){return this.Dropped;}

// private - event initialization for combo object
function igcmbo_setDropDown(bDrop){
	if(this.Element.style.display=="none")
		return;
	if(bDrop == true) {
		if(this.Dropped == true)
			return;
		var grid = igcmbo_getElementById(this.ClientUniqueId + "_container");
		var type=this.ExpandEffects.Type;
		var duration=this.ExpandEffects.Duration;
		duration = duration/1000;
		var shadowWidth=this.ExpandEffects.ShadowWidth;
		var opacity=this.ExpandEffects.Opacity;
		var shadowColor=this.ExpandEffects.ShadowColor;
		
		var cTop=igcmbo_getTopPos(this.Element);
		grid.style.top=cTop+this.Element.offsetHeight;
		grid.style.left=igcmbo_getLeftPos(this.Element);
 		if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.BeforeDropDown,"(\""+this.ClientUniqueId+"\");")) {
 			return;
		}
		if(this.HideDropDowns) igcmbo_hideDropDowns(true);
		if(document.all  && this.ExpandEffects.Type!="NotSet") 
		{
			var type=this.ExpandEffects.Type;
			var duration=this.ExpandEffects.Duration;
			duration = duration/1000;
			var shadowWidth=this.ExpandEffects.ShadowWidth;
			var opacity=this.ExpandEffects.Opacity;
			var shadowColor=this.ExpandEffects.ShadowColor;


			if(type != 'NotSet')
				grid.style.filter = "progid:DXImageTransform.Microsoft."+type+"(duration="+duration+");"
			if(shadowWidth > 0) {
				var s = " progid:DXImageTransform.Microsoft.Shadow(Direction=135, Strength="+shadowWidth+",color='"+shadowColor+"')";
				grid.style.filter += s;
			}
			if(opacity < 100)
				grid.style.filter += " progid:DXImageTransform.Microsoft.Alpha(Opacity="+opacity+");"
			try{
			if(grid.filters[0] != null)
	       		grid.filters[0].apply();
	       	}catch(ex){}
	    }
		grid.style.visibility = 'visible'; 
		grid.style.display = ""; 
		
		var tw=grid.offsetWidth;
		var bw=document.body.clientWidth;
		if(grid.offsetLeft+tw>bw+document.body.scrollLeft)
			if(bw-tw+document.body.scrollLeft>0)
				grid.style.left=bw-tw+document.body.scrollLeft;
			else
				grid.style.left=0;
		var th=grid.offsetHeight;
		var bh=document.body.clientHeight;
		if(grid.offsetTop+th>bh+document.body.scrollTop && cTop-th>0)
			grid.style.top=cTop-th;
		try{
		if(document.all && grid.filters[0]!=null)
			grid.filters[0].play();
		}catch(ex){}				
				
		var dropdowngrid = igcmbo_getElementById(this.ClientUniqueId + "xGrid_main");
		if(document.all && dropdowngrid != null) {
			if(this.webGrid)
				this.webGrid.Element.setAttribute("noOnResize",true);
			igtbl_activate(this.ClientUniqueId + "xGrid");
			if(this.webGrid)
				this.webGrid.Element.removeAttribute("noOnResize");
		}
		this.Element.style.backgroundColor='white';
		this.Dropped = true;
		if(this.grid.getActiveRow())
			igtbl_scrollToView(this.grid.Id,this.grid.getActiveRow().Element);
		igcmbo_currentDropped = this;
 		igcmbo_fireEvent(this.ClientUniqueId,this.Events.AfterDropDown,"(\""+this.ClientUniqueId+"\");");
 		this._internalDrop = true;
 		setTimeout(igcmbo_clearInternalDrop, 100);
	}
	else {
		if(this.Dropped == false)
			return;
		var grid = igcmbo_getElementById(this.ClientUniqueId + "_container");
 		if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.BeforeCloseUp,"(\""+this.ClientUniqueId+"\");")) {
 			return;
		}
		if(this.webGrid)
			this.webGrid.Element.setAttribute("noOnResize",true);
		grid.style.visibility = 'hidden'; 
		grid.style.display = "none"; 
		this.Dropped = false;		
		if(this.HideDropDowns) igcmbo_hideDropDowns(false);
		var inputbox = igcmbo_getElementById(this.ClientUniqueId + "_input");
		igcmbo_currentDropped = null;
 		igcmbo_fireEvent(this.ClientUniqueId,this.Events.AfterCloseUp,"(\""+this.ClientUniqueId+"\");");
		if(this.webGrid){
			igcmbo_wgNoResize=this.webGrid;
	 		setTimeout(igcmbo_clearnoOnResize, 100);
		}
	}
}
function igcmbo_clearInternalDrop() {
	if(igcmbo_currentDropped) igcmbo_currentDropped._internalDrop = null;
}
var igcmbo_wgNoResize=null;
function igcmbo_clearnoOnResize() {
	if(igcmbo_wgNoResize){
		igcmbo_wgNoResize.Element.removeAttribute("noOnResize");
		igcmbo_wgNoResize=null;
	}
}

function igcmbo_editkeydown(evnt,comboId) {
	var oCombo = igcmbo_getComboById(comboId);
	if(oCombo && oCombo.Loaded) {
		var keyCode = (evnt.keyCode);
		var newValue = igcmbo_srcElement(evnt).value;
    	if(igcmbo_fireEvent(oCombo.ClientUniqueId,oCombo.Events.EditKeyDown,"('"+oCombo.ClientUniqueId+"','"+newValue+"',"+keyCode+");"))
    		return igtbl_cancelEvent(evnt);
		if(oCombo.eventHandlers["keydown"] && oCombo.eventHandlers["keydown"].length>0){
			var ig_event=new ig_EventObject();
			ig_event.event=evnt;
			for(var i=0;i<oCombo.eventHandlers["keydown"].length;i++)
				if(oCombo.eventHandlers["keydown"][i].fListener)
				{
					if(keyCode==9 || keyCode==13 || keyCode==27)
						oCombo.setDisplayValue(newValue,false);
					oCombo.eventHandlers["keydown"][i].fListener(oCombo,ig_event,oCombo.eventHandlers["keydown"][i].oThis);
					if(ig_event.cancel)
						return igtbl_cancelEvent(evnt);
				}
		}				
		if (oCombo.ComboTypeAhead!=0 && igcmbo_isCountableKey(keyCode) )
			oCombo.keyCount++;
		if (!oCombo.Editable&&oCombo.ComboTypeAhead==1)
		{
			if(oCombo.DataTextField.length>0)column=oCombo.getGrid().Bands[0].getColumnFromKey(oCombo.DataTextField)			
			else column=oCombo.getGrid().Bands[0].Columns[0];
			var s=String.fromCharCode(evnt.keyCode);			
			if (igcmbo_isCountableKey(evnt.keyCode)){
				var cell=null;
				var row;
				cell = igcmbo_typeaheadFindCell(oCombo,s, column, oCombo.lastKey);
				if(cell){
					oCombo.lastKey = s;
					text = igcmbo_processTypeAhead(oCombo,oCombo.getGrid(),cell);
					newValue=text;
				}
			}
			else{
				var oText=igcmbo_ProcessNavigationKey(oCombo,column,evnt.keyCode,evnt);
				if (oText) newValue=oText;
			}
		}
		oCombo.updatePostField(newValue);
		oCombo.displayValue = newValue;
		if(keyCode==38 || keyCode==40)
			return igtbl_cancelEvent(evnt);
	}
}

// private function
// used to determine what keys will trigger type ahead counter increment/decrements
function igcmbo_isCountableKey(keyCode){		
	if (keyCode<32)
		return false;
	switch(keyCode){
		//end//right//home//left
		case 35: case 39: case 36: case 37:
		//back//del
		case 8: case 46:
		//up//down
		case 38: case 40:
			return false;
			break;
	}	
	return true;
}
// private function
function igcmbo_arrowKeyNavigation(oCombo, oGrid, oRow, column){
	var text = null;
	if(oRow != null){
		oGrid.setActiveRow(oRow);
		oGrid.clearSelectionAll();
		oRow.setSelected(true);
		oCombo.selectedIndex = oRow.getIndex();
		var cell = oRow.getCell(column.Index);
		text = cell.getValue(true);
		oCombo.updateValue(text, true);		
		if(oCombo.DataValueField) oCombo.dataValue=oRow.getCellFromKey(oCombo.DataValueField).getValue();
		igtbl_updatePostField(oGrid.Id);
	}
	return text;
}
// private function
function igcmbo_highlightText(){
	var oInput = document.getElementById(this.ClientUniqueId + "_input");
	if (null==oInput)return;
	var oInTextRange= oInput.createTextRange?oInput.createTextRange():null;
	if (this.Editable){
		if (oInTextRange){
			oInTextRange.moveStart("character",this.ComboTypeAhead==2&&this.lastKey?this.lastKey.length:0);
			oInTextRange.moveEnd("textedit");
			oInTextRange.select();
		}
		else if (oInput.selectionStart){
			oInput.selectionStart =  this.lastKey?this.lastKey.length:0;
			oInput.selectionEnd = oInput.value.length;
		}
	}
	else{
		oInput.style.backgroundColor = this.SelBackColor;
		oInput.style.color = this.SelForeColor;
	}
}
// private function
function igcmbo_typeAheadReset(comboId){
	var oCombo = igcmbo_getComboById(comboId);
	if (oCombo){
		oCombo.keyCount=0;
		oCombo.typeAheadTimeout=null;
		if (2==oCombo.ComboTypeAhead)
			oCombo.lastKey="";
	}
}
// private
function igcmbo_typeaheadFindCell(oCombo,charFromCode, column, lastKey){
		var cell=null;
		var re=new RegExp("^"+igtbl_getRegExpSafe(charFromCode),"gi");
		if(lastKey!=charFromCode) cell=column.find(re);
		else if(cell==null){
			cell=column.findNext();
			if(cell==null) cell=column.find(re);
		}
		return cell;
}
//private
function igcmbo_processTypeAhead(oCombo,oGrid,oCell){
	var text=null;
	text=oCell.getValue(true);
	var oRow=oGrid.getActiveRow();
	oGrid.clearSelectionAll();
	if(oRow) oRow.setSelected(false);
	oRow=oCell.getRow();
	oGrid.setActiveRow(oRow);
	oRow.setSelected(true);
	oCombo.selectedIndex=oRow.getIndex();
	oCombo.updateValue(text, true);
	if(oCombo.DataValueField) oCombo.dataValue=oRow.getCellFromKey(oCombo.DataValueField).getValue();
	igtbl_updatePostField(oGrid.Id);
	oCombo.highlightText();								
	return text;
}
	
//private
function igcmbo_ProcessNavigationKey(oCombo,column,keyCode,evnt){
		var oRow=null;
		var oGrid=oCombo.getGrid();
		var oText=null;
		switch(keyCode){
		case 8: case 46:
			if (oCombo.Editable){
				document.selection.createRange().text="";
				oCombo.lastKey=igcmbo_srcElement(evnt).value;
			}
			break;			
		case 40:
			oRow=oGrid.getActiveRow();
			if(oRow!=null){
				oRow.setSelected(false);
				var oRow=oRow.getNextRow();
				oText=igcmbo_arrowKeyNavigation(oCombo,oGrid,oRow,column);
			}
			else if (oGrid.Rows.length>0) oText=igcmbo_arrowKeyNavigation(oCombo,oGrid,oGrid.Rows.getRow(0),column);
			break;
		case 38:				
				oRow = oGrid.getActiveRow();
				if(oRow != null){
					oRow.setSelected(false);
					var oRow = oRow.getPrevRow();
					oText = igcmbo_arrowKeyNavigation(oCombo,oGrid,oRow,column);
				}
				else if (oGrid.Rows.length > 0) oText=igcmbo_arrowKeyNavigation(oCombo,oGrid,oGrid.Rows.getRow(oGrid.Rows.length-1),column);				
				break;
		}
	return oText;
}		
function igcmbo_editkeyup(evnt,comboId) {
	var oCombo = igcmbo_getComboById(comboId);
	if(oCombo&&oCombo.Loaded){
		var keyCode=(evnt.keyCode);
		var charFromCode=String.fromCharCode(evnt.keyCode);
		var newValue = oCombo.Editable ? igcmbo_srcElement(evnt).value:charFromCode;
    	if(igcmbo_fireEvent(oCombo.ClientUniqueId,oCombo.Events.EditKeyUp,"(\""+oCombo.ClientUniqueId+"\",\""+newValue+"\","+keyCode+");"))
    		return igtbl_cancelEvent(evnt);		
		if (0==oCombo.ComboTypeAhead) return;		
		var bCountableKey=igcmbo_isCountableKey(keyCode);
		if (bCountableKey) --oCombo.keyCount;
		var lastKey=oCombo.lastKey;		
		if (2==oCombo.ComboTypeAhead)
			if (oCombo.Editable)charFromCode=newValue;
			else{
				charFromCode=(bCountableKey?(lastKey?lastKey:"")+newValue:null);
				oCombo.lastKey=charFromCode;
			}
		else
			oCombo.lastKey = charFromCode;				
		if (oCombo.keyCount==0){			
			var oGrid=oCombo.getGrid();
			if(oGrid==null) return;
			var column=null;
			if(oCombo.DataTextField.length>0)column=oGrid.Bands[0].getColumnFromKey(oCombo.DataTextField)			
			else {
				var colNo=0;
				column=oGrid.Bands[0].Columns[colNo];
			}
			if(column==null) return;			
			var text;
			var cell;
			var oCurrentRow=null;				
			if(charFromCode&&bCountableKey){				
				cell=igcmbo_typeaheadFindCell(oCombo,charFromCode,column,lastKey);
				if(cell!=null){
					oCombo.lastKey=charFromCode;
					text=igcmbo_processTypeAhead(oCombo,oGrid,cell);
					oCombo.typeAheadTimeout = null;
					oCombo.typeAheadTimeout = setTimeout("igcmbo_typeAheadReset('"+oCombo.ClientUniqueId+"')",1000);					
					if (!oCombo.Editable) newValue=text;
				}
				else {				
					var oEditor=document.getElementById(oCombo.ClientUniqueId + "_input");
					if (!oCombo.Editable){						
						var oActRow=oGrid.getActiveRow();												
						if (oActRow) oEditor.value= oCombo.DataTextField!=null && oCombo.DataTextField!="" ? oActRow.getCellFromKey(oCombo.DataTextField).getValue() : oActRow.getCell(0).getValue(); 
						newValue=oEditor.value;										
						oCombo.highlightText();						
					}
					else  // if editable and no row is found we should move off all rows since this may be a new value
					{
						oGrid.clearSelectionAll();
						oGrid.setActiveRow(null);
						oCombo.selectedIndex = -1;						
			
						//oCombo.updateValue(charFromCode, true);
						//igtbl_updatePostField(oGrid.Id);
						newValue=charFromCode;
								
					}
					oCombo.typeAheadTimeout=setTimeout("igcmbo_typeAheadReset('"+oCombo.ClientUniqueId+"')",1000);
				}
			}
			else{				
				var oText=igcmbo_ProcessNavigationKey(oCombo,column,evnt.keyCode,evnt);
				if (null!=oText) newValue=oText;
			}
		}
		else
			oCombo.typeAheadTimeout=setTimeout("igcmbo_typeAheadReset('"+oCombo.ClientUniqueId+"')",500);
		oCombo.updatePostField(newValue);
		oCombo.displayValue=newValue;
	}
}
function igcmbo_onfocus(evnt,comboId) {
	var oCombo = igcmbo_getComboById(comboId);
	if(!oCombo)
		return;
	oCombo.setFocusTop();
	oCombo.highlightText();
}

function igcmbo_onblur(evnt,comboId) {
	var oCombo = igcmbo_getComboById(comboId);
		
	if(!oCombo || !oCombo.Loaded) 
		return;
		
	// moved this outside the loop 
	var inputbox = igcmbo_getElementById(oCombo.ClientUniqueId + "_input");		
	if (inputbox)
	{
		if (!oCombo.Editable){		
			inputbox.style.backgroundColor = oCombo.BackColor;
			inputbox.style.color = oCombo.ForeColor;
		}
		else{
			var oGrid = oCombo.getGrid();
			var oEditor=document.getElementById(oCombo.ClientUniqueId + "_input");
			var oActRow=oGrid.getActiveRow();
			if (oActRow)
			{			
				var oCellValue = oCombo.DataTextField!=null && oCombo.DataTextField!="" ? oActRow.getCellFromKey(oCombo.DataTextField).getValue():oActRow.getCell(0).getValue(); 
				if (oEditor.value!=oCellValue)				
				{
					oGrid.clearSelectionAll();
					oGrid.setActiveRow(null);	
					oCombo.selectedIndex = -1;
					oCombo.updateValue(oEditor.value, true);
					igtbl_updatePostField(oGrid.Id);
							
				}
			}
			oCombo.updatePostField(oEditor.value);	
		}
	}
	
	if(oCombo!=igcmbo_displaying) 
		return;
	
	
	if (document.all && oCombo.Element.contains(evnt.toElement)) {		
    }
    else {	
		if(oCombo.webGrid != null) {
			var container = igcmbo_getElementById(oCombo.ClientUniqueId + "_container");
			if(oCombo._internalDrop || oCombo.Element.getAttribute("noOnBlur"))
				return;
			if(oCombo.eventHandlers["blur"] && oCombo.eventHandlers["blur"].length>0)
			{
				var ig_event=new ig_EventObject();
				ig_event.event=evnt;
				for(var i=0;i<oCombo.eventHandlers["blur"].length;i++)
					if(oCombo.eventHandlers["blur"][i].fListener)
					{
						oCombo.eventHandlers["blur"][i].fListener(oCombo,ig_event,oCombo.eventHandlers["blur"][i].oThis);
						if(ig_event.cancel)
							return igtbl_cancelEvent(evnt);
					}
			}
		}
    }
}
function igcmbo_setFocusTop() {
	var inputbox = igcmbo_getElementById(this.ClientUniqueId + "_input");
	if(this.Editable)
		inputbox.select();
	else{
		inputbox.style.backgroundColor=this.SelBackColor;
		inputbox.style.color=this.SelForeColor;
	}	
	try{
		if(document.all)inputbox.focus();
	}
	catch(e){}
}

// private - event initialization for combo object
function igcmbo_events(events){
	this.InitializeCombo=events[0];
	this.EditKeyDown=events[1];
	this.EditKeyUp=events[2];
	this.BeforeDropDown=events[3];
	this.AfterDropDown=events[4];
	this.BeforeCloseUp=events[5];
	this.AfterCloseUp=events[6];
	this.BeforeSelectChange=events[7];
	this.AfterSelectChange=events[8];
}

function igcmbo_gridmouseover(gridName, itemId) {
	var grid = igtbl_getGridById(gridName);
	var cell = igtbl_getCellById(itemId);
	if(cell == null)
		return;
	igtbl_clearSelectionAll(gridName);
	igtbl_selectRow(gridName,cell.Row.Element.id);
}

function igcmbo_gridkeydown(gridName, itemId, keyCode) {
	igtbl_clearSelectionAll(gridName);
	var oCombo = igcmbo_currentDropped;
	if(keyCode == 27 || keyCode == 10) {
		oCombo.setDropDown(false);
		oCombo.setFocusTop();
	}
}

function igcmbo_gridrowactivate(gridName, itemId) {	
	var oCombo = igcmbo_getComboByGridName(gridName);
	//var oCombo = igcmbo_currentDropped;
	var row = igtbl_getRowById(itemId);
	if(oCombo == null || row == null)
		return;
	if(oCombo.DataTextField.length > 0) {
		cell = row.getCellFromKey(oCombo.DataTextField);
	}
	else
		cell = row.getCell(0);
	if(cell != null) {
		var v = cell.getValue(true);
		oCombo.selectedIndex = row.getIndex();
		oCombo.updateValue(v, true);
	}
}

function igcmbo_setSelectedRow(row) {
	var cell = null;
	if(this.DataValueField.length > 0) 
	{
		cell = row.getCellFromKey(this.DataValueField);
		this.setDataValue(cell, false);
		if(this.Element.style.display!="none")
			this.setFocusTop();
	}
}

function igcmbo_gridmouseup(gridName, itemId) {
	var grid = igtbl_getGridById(gridName);
	var row = igtbl_getRowById(itemId);
	if(row == null)
		return;
	var cell = igtbl_getCellById(itemId);
	if(cell == null)
		return;

	var oCombo = igcmbo_currentDropped;
	if(oCombo != null) {
		oCombo.setSelectedRow(row);
		oCombo.setDropDown(false);
	}
}

function igcmbo_getSelectedIndex() {
	return this.selectedIndex;
}

function igcmbo_setSelectedIndex(index)
{
	if(index>=0 && index<this.grid.Rows.length)
		this.setSelectedRow(this.grid.Rows.getRow(index));
}

function igcmbo_getVisible() {
	if(this.Element.style.display == "none" || this.Element.style.visibility == "hidden")
		return false;
	else
		return true;
}

function igcmbo_setVisible(bVisible,left,top,width,height){
	if(bVisible){
		this.Element.style.display = "";
		this.Element.style.visibility = "visible";
		igcmbo_displaying=this;
		if(arguments.length>=3)
		{
			this.Element.style.top=top;
			this.Element.style.left=left;
		}
		if(arguments.length>=5)
		{
			this.Element.style.height=height;
			this.setWidth(width);
		}
		
		if(this.Element.focus && false)
		{
			try{
				this.Element.focus();
			}catch(e){}
		}
	}
	else
	{
		if(this.Dropped)
			this.setDropDown(false);
		this.Element.style.display = "none";
		this.Element.style.visibility = "hidden";
		igcmbo_displaying=null;
	}
}

function igcmbo_getDisplayValue()
{
	return this.displayValue;
}

function igcmbo_getDataValue()
{
	return this.dataValue;
}

function igcmbo_setDisplayValue(newValue, bFireEvent)
{
	var cell=newValue;
	if(cell==null || typeof(cell)!="object")
	{
		this.updateValue(newValue, bFireEvent);
		var re = new RegExp("^"+igtbl_getRegExpSafe(newValue)+"$", "g");
		var column = null;
		if(this.DataTextField.length > 0) {
			column = this.grid.Bands[0].getColumnFromKey(this.DataTextField)
		}
		else {
			var colNo = 0;
			column = this.grid.Bands[0].Columns[colNo];
		}
		if(column == null)
			return;
		cell = column.find(re);
	}
	else
		this.updateValue(cell.getValue(), bFireEvent);
	if(cell != null) {
		if(this.DataValueField)
			this.dataValue=cell.Row.getCellFromKey(this.DataValueField).getValue();
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(cell.Row);
		cell.Row.setSelected(true);
		this.selectedIndex = cell.Row.getIndex();
		igtbl_updatePostField(this.grid.Id);
		this.updatePostField(newValue,false);
	}
	else
	{
		this.dataValue=null;
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(null);
		this.selectedIndex = -1;
		igtbl_updatePostField(this.grid.Id);
		this.updatePostField(newValue,false);
	}
	return this.selectedIndex;
}

function igcmbo_setDataValue(newValue, bFireEvent)
{
	var cell=newValue;
	if(cell==null || typeof(cell)!="object")
	{
		this.dataValue=newValue;
		var re = new RegExp("^"+igtbl_getRegExpSafe(newValue)+"$", "g");
		var column = null;
		if(this.DataTextField.length > 0)
			column = this.grid.Bands[0].getColumnFromKey(this.DataValueField)
		else
			column = this.grid.Bands[0].Columns[0];
		if(column == null)
			return;
		cell = column.find(re);
	}
	else
		this.dataValue=cell.getValue();
	if(cell != null)
	{
		if(this.DataTextField)
			this.updateValue(cell.Row.getCellFromKey(this.DataTextField).getValue(true),bFireEvent);
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(cell.Row);
		cell.Row.setSelected(true);
		this.selectedIndex = cell.Row.getIndex();
		igtbl_updatePostField(this.grid.Id);
		if(!this.DataTextField)
			this.updatePostField(newValue,false);
	}
	else
	{
		this.dataValue=null;
		igtbl_clearSelectionAll(this.grid.Id);
		if(this.Prompt)	{
			var row=this.grid.Rows.getRow(0);
			row.activate();
			row.setSelected();					
		}
		else {
			this.grid.setActiveRow(null);
			this.selectedIndex = -1;
			igtbl_updatePostField(this.grid.Id);
			this.updatePostField(newValue,false);
		}
	}
	return this.selectedIndex;
}

function igcmbo_getValue()
{
	if(!this.Prompt || this.getSelectedIndex()>0)
		return this.dataValue;
}

function igcmbo_setValue(newValue, bFireEvent)
{
	var cell=newValue;
	if(cell==null || typeof(cell)!="object" || newValue.getMonth)
	{
		var oRegEx = newValue?newValue.toString():newValue;
		var re = new RegExp("^" + igtbl_getRegExpSafe(oRegEx), "g");
		var column = null;
		if(this.DataValueField.length > 0)
			column = this.grid.Bands[0].getColumnFromKey(this.DataValueField)
		else
			column = this.grid.Bands[0].Columns[0];
		if(column == null)
			return;
		cell = column.find(re);
	}
	var dispValue=this.Prompt;
	if(cell != null)
	{
		this.dataValue=newValue;
		if(this.DataValueField)
		{
			cellValue=cell.Row.getCellFromKey(this.DataValueField).getValue();
			if(cellValue!=newValue)
				this.dataValue=cellValue;
		}
		if(this.DataTextField)
		{
			dispValue=cell.Row.getCellFromKey(this.DataTextField).getValue(true);
			this.updateValue(dispValue, (typeof(bFireEvent)=="undefined" || bFireEvent));
		}
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(cell.Row);
		cell.Row.setSelected(true);
		this.selectedIndex = cell.Row.getIndex();
	}
	else
	{
		this.dataValue=null;
		this.displayValue=dispValue;
		var ib=igcmbo_getElementById(this.ClientUniqueId+"_input");
		if(ib)
			ib.value=dispValue;
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(null);
		this.selectedIndex = -1;
	}
	igtbl_updatePostField(this.grid.Id);
	this.updatePostField(dispValue,false);
	if(this.Prompt && this.selectedIndex==-1)
	{
		this.setSelectedIndex(0);
		return -1;
	}
	return this.selectedIndex;
}

var igtbl_pbInited=false;
function igcmbo_updateValue(newValue, bFireEvent) 
{
	if(bFireEvent == true) {
    	if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.BeforeSelectChange,"(\""+this.ClientUniqueId+"\");")) {
	    	return false;
	    }
	}	
	var inputbox = igcmbo_getElementById(this.ClientUniqueId + "_input");
	inputbox.value = newValue;	
	this.updatePostField(newValue);
	this.displayValue = newValue;
	if(bFireEvent == true) {
		if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.AfterSelectChange,"(\""+this.ClientUniqueId+"\");"))
			return;
	}
	if(this.NeedPostBack && bFireEvent == true && !igtbl_pbInited) {
		igtbl_pbInited=true;
		__doPostBack(this.UniqueId,'AfterSelChange\x02'+this.selectedIndex);
	}
	
}

var ig_inCombo=false;
// private - Handles the mouse down event
function igcmbo_mouseDown(evnt) {
	if(igcmbo_currentDropped != null)
	{			
		var grid = igcmbo_getElementById(igcmbo_currentDropped.ClientUniqueId + "_container");
		var elem = igtbl_srcElement(evnt);
		var parent = elem;
		while(true) {
			if(parent == null)
				break;
			if(parent == grid)
			{
				if(igcmbo_currentDropped.webGrid)
				{
					igtbl_lastActiveGrid=igcmbo_currentDropped.webGrid.Id;
					igcmbo_currentDropped.Element.setAttribute("noOnBlur",true);
					window.setTimeout("igtbl_cancelNoOnBlurDD()",100);
				}
				return;
			}
			parent = parent.parentNode;
		}				
		if(ig_inCombo == true) {
			ig_inCombo = false;
			return;		
		}

		if(igcmbo_currentDropped)
			igcmbo_currentDropped.setDropDown(false);

		ig_inCombo = false;			
	}
	var combo=igcmbo_currentDropped;
	if(!combo)
		combo=igcmbo_displaying;	
	if(combo && combo.eventHandlers["blur"] && combo.eventHandlers["blur"].length>0)
	{
		var ig_event=new ig_EventObject();
		ig_event.event=evnt;
		for(var i=0;i<combo.eventHandlers["blur"].length;i++)
			if(combo.eventHandlers["blur"][i].fListener)
			{
				combo.eventHandlers["blur"][i].fListener(combo,ig_event,combo.eventHandlers["blur"][i].oThis);
				if(ig_event.cancel)
					return igtbl_cancelEvent(evnt);
			}
	}
}

// private - Handles the mouse up event
function igcmbo_mouseUp(evnt) {
	return;
}

// private - Obtains the proper source element in relation to an event
function igcmbo_srcElement(evnt)
{
	var se
	if(evnt.target)
		se=evnt.target;
	else if(evnt.srcElement)
		se=evnt.srcElement;
	return se;
}

// private - Updates the PostBackData field
function igcmbo_updatePostField(value)
{
	var formControl = igcmbo_getElementById(this.UniqueId);
	if(!formControl)
		return;
	var index = this.selectedIndex;
	formControl.value = "Select\x02" + index + "\x02Value\x02" + value;
}

// private
function igcmbo_getClientUniqueId(uniqueId) {
	var u = uniqueId.replace(/:/gi, "");
	u = u.replace(/_/gi, "x");
	return u;
}
// private
function igcmbo_getGrid() {
	return this.grid;
}

function igcmbo_addEventListener(eventName,fListener,oThis)
{
	eventName=eventName.toLowerCase();
	if(!this.eventHandlers[eventName])
		this.eventHandlers[eventName]=new Array();
	var index=this.eventHandlers[eventName].length;
	if(index>=15)
		return false;
	for(var i=0;i<this.eventHandlers[eventName].length;i++)
		if(this.eventHandlers[eventName][i]["fListener"]==fListener)
			return false;
	this.eventHandlers[eventName][index]=new Object();
	this.eventHandlers[eventName][index]["fListener"]=fListener;
	this.eventHandlers[eventName][index]["oThis"]=oThis;
	return true;
}

function igcmbo_removeEventListener(eventName,fListener)
{
	if(!this.eventHandlers)
		return false;
	var eventName=eventName.toLowerCase();
	if(!this.eventHandlers[eventName] || this.eventHandlers[eventName].length==0)
		return false;
	for(var i=0;i<this.eventHandlers[eventName].length;i++)
		if(this.eventHandlers[eventName][i]["fListener"]==fListener)
		{
			delete this.eventHandlers[eventName][i]["fListener"];
			delete this.eventHandlers[eventName][i]["oThis"];
			delete this.eventHandlers[eventName][i];
			if(this.eventHandlers[eventName].pop)
				this.eventHandlers[eventName].pop();
			else
				this.eventHandlers[eventName]=this.eventHandlers[eventName].slice(0,-1);
			return true;
		}
	return false;
}

function igcmbo_getComboByGridName(gridName)
{
	var oC = null;
	if (!igcmbo_comboState) return oC;
	for (var c in igcmbo_comboState) if (igcmbo_comboState[c].grid.Id==gridName)oC=igcmbo_comboState[c];
	return oC;
}
igcmbo_initialize();
