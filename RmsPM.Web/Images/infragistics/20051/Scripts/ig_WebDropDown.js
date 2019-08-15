/* 
Infragistics WebDropDown script. 
Version 5.1.20051.37
js version 1.2.2005.6
Copyright (c) 2003-2005 Infragistics, Inc. All Rights Reserved.
Functions marked public are for use by developers and are documented and supported.
All other functions are for the internal use only.
*/
// private - hides all dropdown select controls for the document.
var igdrp_hidden=false;
function igdrp_hideDropDowns(bHide)
{ 
	if(igdrp_dropDowns==null || bHide==igdrp_hidden)return;
	igdrp_hidden=bHide;
	for(var i=0;i<igdrp_dropDowns.length;i++)igdrp_dropDowns[i].style.visibility=bHide?'hidden':'visible';
}
// public - Retrieves the server-side unique id of the combo
function igdrp_getUniqueId(comboName)
{
	var combo=igdrp_getComboById(comboName);
	return (combo==null)?null:combo.UniqueId;
}
function igdrp_getElementById(id){return ig_csom.getElementById(id);}
// public - returns the combo object for the Item Id
function igdrp_getComboById(id,e)
{
	var o=null,i=0;
	while(e!=null && ig_csom.isEmpty(id))
	{
		try{if(e.getAttribute!=null)id=e.getAttribute("ig_drp");}catch(ex){}
		if(++i>6)return null;
		e=e.parentNode;
	}
	if(!ig_csom.isEmpty(id))if((e=igdrp_all)!=null)if((o=e[igdrp_comboIdById(id)])==null)
		for(i in e){if((o=e[i])!=null)if(o.Id==id || o.ClientUniqueId==id || o.UniqueId==id)break;else o=null;}
	return o;
}
// public - returns the combo object from an Item element
function igdrp_getComboByItem(item){return igdrp_all[igdrp_comboIdById(item.id)];}
// public - returns the combo Name from an itemId
function igdrp_comboIdById(itemId){return itemId.split("_")[0];}
// Fires an event to client-side script and then to the server is necessary
function igdrp_fireEvent(oCombo,eventName,param)
{
	var oEvent=new ig_EventObject();
	ig_fireEvent(oCombo,eventName,param,oEvent);
	return oEvent;
}
// Performed on page initialization
if(typeof igdrp_all!="object")
	var igdrp_all=new Object();
igdrp_all._once=false;
var igdrp_dropDowns;
// initializes the combo object on the client
function igdrp_initCombo(comboId)
{
   var elem=igdrp_getElementById(comboId);
   var oCombo=new igdrp_combo(elem,eval("igdrp_"+comboId+"_Props"));
   ig_fireEvent(oCombo,oCombo.Events.InitializeCombo[0],oCombo,comboId);
   if(ig_csom.IsIE && oCombo.HideDropDowns==true && igdrp_dropDowns==null)igdrp_dropDowns=document.all.tags("SELECT");
   oCombo.Loaded=true;
   return oCombo;
}
function igdrp_button(props)
{
	this.ImageUrl1=props[0];
	this.ImageUrl2=props[1];
	this.DefaultStyleClassName=props[2];
	this.HoverStyleClassName=props[3];
}
function igdrp_evt(e)
{
	if(e==null)if((e=window.event)==null)return;
	var o,t=e.type,src=e.srcElement;
	if(t=="unload"){ig_dispose(igdrp_all);return;}
	if(src==null)if((src=e.target)==null)src=this;
	if((o=igdrp_getComboById(null,src))==null || o.doEvt==null)return;
	o.doEvt(e,t,src);
}
// constructor for the combo object
function igdrp_combo(elem,comboProps)
{
	this.Id=elem.id;
	igdrp_all[this.Id]=this;
	this.Element=elem;
	elem.setAttribute("ig_drp",this.Id);
	ig_csom.addEventListener(elem,"mousedown",igdrp_evt);
	ig_csom.addEventListener(elem,"mouseup",igdrp_evt);
	ig_csom.addEventListener(elem,"mouseout",igdrp_evt);
	ig_csom.addEventListener(elem,"mouseover",igdrp_evt);
	if(!igdrp_all._once)
	{
		ig_csom.addEventListener(window,"unload",igdrp_evt);
		ig_csom.addEventListener(document,"mousedown",igdrp_mouseDown);
		if(document.captureEvent)document.captureEvent(Event.MOUSEDOWN);
	}
	igdrp_all._once=true;
	this.Element.Object=this;
	this.UniqueId=comboProps[0];
	this.DropButton=new igdrp_button(comboProps[1])
	this.EditStyleClassName=comboProps[3];
	this.DropDownStyleClassName=comboProps[4];
	this.HideDropDowns=comboProps[5];
	this.editable=comboProps[6];
	this.readOnly=comboProps[7]
	this.dropDownAlignment=comboProps[8];
	this.getDropDownAlignment=function(){return this.dropDownAlignment;}
	this.setDropDownAlignment=function(align){this.dropDownAlignment=align;ig_ClientState.setPropertyValue(this.stateNode,"DropDownAlignment",align);this.updatePostField();}
	this.autoCloseUp=comboProps[9];
	this.Dropped=comboProps[10];
	this.getAutoCloseUp=function(){return this.autoCloseUp;}
	this.setAutoCloseUp=function(auto){this.autoCloseUp=auto;ig_ClientState.setPropertyValue(this.stateNode,"AutoCloseUp",auto);this.updatePostField();}
	this.isEnabled=function(){return !this.inputBox.disabled;}
	this.setEnabled=function(enable){this.inputBox.disabled=!enable;ig_ClientState.setPropertyValue(this.stateNode,"Enabled",enable);this.updatePostField();}
	this.ClientUniqueId=this.UniqueId.replace(/:/gi,"x").replace(/_/gi,"x");
	var box=this.inputBox=igdrp_getElementById(this.ClientUniqueId+"_input");
	var h=box.offsetHeight;
	if(h<8)if((h=box.parentNode.offsetHeight)>7)box.style.height=h;
	this.DropButton.Image=igdrp_getElementById(this.ClientUniqueId+"_img");
	this.postField=igdrp_getElementById(this.UniqueId+"_hidden");
	this.elemCal=this.container=igdrp_getElementById(this.ClientUniqueId+"_container");
	this.ForeColor=box.style.color;
	this.BackColor=box.style.backgroundColor;
	this.Events=new igdrp_events(eval("igdrp_"+this.ClientUniqueId+"_Events"));
	this.Loaded=false;
	this.TopHoverStarted=false;
	this.focus=function(){try{this.inputBox.focus();}catch(e){}}
	this.isEditable=function(){return this.editable;}
	this.setEditable=function(editable)
	{
		this.editable=editable;
		this.inputBox.readOnly=(!editable)?!editable:this.isReadOnly();
		ig_ClientState.setPropertyValue(this.stateNode,"Editable",editable);
		this.updatePostField();
	}
	this.isReadOnly=function(){return this.readOnly;}
	this.setReadOnly=function(readOnly)
	{
		this.readOnly=readOnly;
		this.inputBox.readOnly=(readOnly==true?readOnly:!this.isEditable());
		ig_ClientState.setPropertyValue(this.stateNode,"ReadOnly",readOnly);
		this.updatePostField();
	}
	this.getValue=function(){return this.value;}
	this.getText=function(){return this.inputBox.value;}
	this.setText=function(newValue,bFireEvent){this.updateValue(newValue,bFireEvent);}
	this.updatePostField=function(value){if(this.postField!=null)this.postField.value=ig_ClientState.getText(this.stateItems);}
	this.fireServerEvent=function(eventName,data){if(!this.posted){this.posted=true;__doPostBack(this.UniqueId,eventName+":"+data);}}
	this.isDropDownVisible=function(){return this.Dropped;}
	this.setDropDownVisible=function(bDrop)
	{
		var evt,tPan=this.transPanel,pan=this.container,edit=this.Element;
		if(pan==null || this.isReadOnly()|| this.Dropped==bDrop)return;
		if(bDrop)
		{
			evt=igdrp_fireEvent(this,this.Events.BeforeDropDown[0],pan);
			if(evt.cancel){delete evt;return;}
			this.focus();
			if(this.Calendar!=null)this.Calendar.setSelectedDate(this.getValue());
			var editH=edit.offsetHeight,editW=edit.offsetWidth,elem=edit,x=pan.parentNode,body=window.document.body;
			if(editH==null)editH=0;
			if(x.tagName!="FORM"&&x.tagName!="BODY"){ig_csom._skipNew=true;x.removeChild(pan);body.appendChild(pan);ig_csom._skipNew=false;}
			pan.style.visibility="visible";
			pan.style.display="";
			var panH=pan.offsetHeight,panW=pan.offsetWidth,z=0;
			if((x=this.elemCal.offsetHeight)>panH)panX=x;
			if((x=this.elemCal.offsetWidth)>panW)panW=x;
			if(tPan==null && this.HideDropDowns && ig_csom.IsIE)
			{	
				this.transPanel=tPan=ig_csom.createTransparentPanel();
				if(tPan!=null)
				{
					while((edit=edit.parentNode)!=null)if(edit.style!=null)if(edit.style.zIndex>z)z=edit.style.zIndex;
					if(++z>tPan.Element.style.zIndex)tPan.Element.style.zIndex=z;
					pan.style.zIndex=tPan.Element.style.zIndex+1;
				}
			}
			var y=editH;x=0;
			while(elem!=null)
			{
				if(elem.offsetLeft!=null)x+=elem.offsetLeft;
				if(elem.offsetTop!=null)y+=elem.offsetTop;
				if(elem.nodeName=="HTML"&&elem.clientHeight>body.clientHeight)body=elem;
				elem=elem.offsetParent;
			}
			if((z=this.dropDownAlignment)==1)x-=(panW-editW)/2;
			else if(z==2)x-=panW-editW;
			if((y-body.scrollTop)*2>body.clientHeight+editH)y-=(panH+editH);
			if(x<(z=body.scrollLeft))x=z;
			else if(x+panW>(z+=body.clientWidth))x=z-panW;
			pan.style.left=x;
			pan.style.top=y;
			this.ExpandEffects.applyFilter();
			if(tPan!=null){tPan.setPosition(y-1,x-1,panW+2,panH+2);tPan.show();}
			this.Dropped=true;
			igdrp_all._droped=this;
			igdrp_fireEvent(this,this.Events.AfterDropDown[0],pan);
		}
		else 
		{
			if((evt=this.editor)!=null)if((evt=evt.elem.Validators)!=null)
				for(var i=0;i<evt.length;i++)try{ValidatorValidate(evt[i]);}catch(ex){}
			evt=igdrp_fireEvent(this,this.Events.BeforeCloseUp[0],pan);
			if(evt.cancel){delete evt;return;}
			pan.style.visibility="hidden";
			pan.style.display="none";
			if(tPan!=null)tPan.hide();
			this.Dropped=false;
			igdrp_all._droped=null;
			igdrp_fireEvent(this,this.Events.AfterCloseUp[0],pan);
			evt=null;
			if(this.changed){this.changed=false;this.updateValue();}
		}
		ig_ClientState.setPropertyValue(this.stateNode,"ShowDropDown",bDrop);this.updatePostField();
	}
	this.getVisible=function(){return (this.Element.style.display!="none" && this.Element.style.visibility!="hidden");}
	this.setVisible=function(show,left,top,width,height)
	{
		var w=width,h=height,bdr=-1,e=this.Element,im=this.DropButton.Image,edit=this.inputBox;
		var s=e.style,t=edit.value;
		if(show)edit.value="";else this.setDropDownVisible(false);
		s.display=show?"":"none";
		s.visibility=show?"visible":"hidden";
		if(top!=null&&show)
		{
			s.position="absolute";
			s.top=top;
			s.left=left;
			if(e.clientWidth)bdr=e.offsetWidth-e.clientWidth;
			if(!(bdr>=0&&bdr<7))bdr=s.borderWidth*2;if(!(bdr>=0&&bdr<7))bdr=0;
			if((h-=bdr)<1)h=1;edit.style.height=im.style.height=h;
			s.height=height;
			if((w-=im.offsetWidth+bdr)<1)w=1;edit.style.width=w;
			s.width=width;
			this.setFocusTop();
		}
		if(show)this.inputBox.value=t;
	}
	this.setFocusTop=function(){this.inputBox.select();this.focus();}
	this.updateValue=function(newValue,suppressEvent)
	{
		if(this.editor!=null)
		{
			var date=this.editor.getDate();
			if(date!=null)newValue=date.getFullYear()+','+(date.getMonth()+1)+','+date.getDate();
		}
		ig_ClientState.setPropertyValue(this.stateNode,"Value",(newValue!=null&&newValue.length>0?newValue:(this.getNullDateLabel!=null&&this.getNullDateLabel().length>0?this.getNullDateLabel():" ")));
		this.updatePostField();
		if(suppressEvent)return;
		var evt=igdrp_fireEvent(this,this.Events.ValueChanged[0],this.getValue());
		if((evt.needPostBack||this.Events.ValueChanged[1])&& !evt.cancelPostBack){
			if(this.getAutoCloseUp())this.setDropDownVisible(false);
			this.fireServerEvent('ValueChanged',this.editor.getText());
		}
		delete evt;
	}
	this.stateItems=ig_ClientState.createRootNode();
	this.stateNode=ig_ClientState.addNode(this.stateItems,"DateChooser");
	this.ExpandEffects=new igdrp_expandEffects(this.container,comboProps[2],this);
	this.onImgKeyDown=function(e)
	{
		var me=igdrp_getComboById(e.srcElement.parentElement.parentElement.parentElement.parentElement.id);
		if(!me || !me.Loaded)return;
		var keyCode=(e.keyCode);
		var bOpening=false;
		if(keyCode==40 || (!me.Dropped && (keyCode==32 || keyCode==13)))
			bOpening=true;
		else if(keyCode==27 || (me.Dropped && (keyCode==32 || keyCode==13)))
			bOpening=false;
		else return;
		if(bOpening)
		{
			me.setDropDownVisible(true);
			me.Calendar.setDefaultTabableDateCell(true);
		}
		else
		{
			me.setDropDownVisible(false);
			me.updateValue(me.getText(),false);
		}
	}
	this.onKeyDown=function(oEditor,value,oEvent)
	{
		var me=oEditor.parent,evnt=oEvent.event;
		if(!me.Loaded)return;
		var keyCode=evnt.keyCode;
		switch(keyCode)
		{
			case 40://DownArrow
				if(evnt.altKey){me.setDropDownVisible(true);return false;}break;
			case 27://Esc
			case 13:me.setDropDownVisible(false);me.updateValue(me.getText(),false);break;
		}
    	ig_fireEvent(me,me.Events.EditKeyDown[0],keyCode,oEvent);
 		me.fireMulticastEvent("keydown",oEvent);
    	if(oEvent.cancel)ig_cancelEvent(evnt);
	}
	this.onKeyUp=function(oEditor,value,oEvent)
	{
		var me=oEditor.parent;
		if(me.Loaded)ig_fireEvent(me,me.Events.EditKeyUp[0],oEvent.event.keyCode,oEvent);
	}
	this.onBlur=function(oEditor,value,oEvent)
	{
		var me=oEditor.parent;
		if(!me.Loaded)return;		
		if(oEditor.changed)if(me.Dropped)me.changed=true;else me.updateValue();
		if(!me.Dropped)
		{
			if(me.endEditCell!=null)me.endEditCell();
			ig_fireEvent(me,me.Events.OnBlur[0],me,oEvent);
			me.fireMulticastEvent("blur",oEvent);
		}
	}
	if(this.Dropped && window.setTimeout!=null)
	{
		window.setTimeout('igdrp_getComboById(\"'+this.Id+'\").setFocusTop();',10);
		window.setTimeout('igdrp_getComboById(\"'+this.Id+'\").setDropDownVisible('+this.Dropped+');',100);
	}
	this.handlers=new Array(11);
	this.removeEventListener=function(name,fref)
	{
		var evtName=name.toLowerCase();
		if(this.handlers==null)return;
		if(this.handlers[evtName]==null||!(this.handlers[evtName].length))return;
		for(i=0;i<this.handlers[evtName].length;i++)
		{
			var listener=this.handlers[evtName][i];
			if(listener!=null)if(listener.handler==fref)this.handlers[evtName][i]=null;
		}
	}
	this.addEventListener=function(name,fref,oParent)
	{
		var evtName=name.toLowerCase();
		var handlerArray=(this.handlers[evtName]);
		if(handlerArray==null)this.handlers[evtName]=handlerArray=new Array();
		handlerArray[handlerArray.length]=new ig_EventListener(fref,oParent);
	}
	this.fireMulticastEvent=function(evtName,evt)
	{
		if(this.handlers==null)return;
		evtName=evtName.toLowerCase();
		if(evt==null)evt=new ig_EventObject();
		try{if(this.handlers.length&&this.handlers[evtName]!=null&&this.handlers[evtName].length){
			for(i=0;i<this.handlers[evtName].length;i++)
			{
				var listener=this.handlers[evtName][i];
				if(listener!=null)
				{
					listener.handler.apply(listener.callBackObject,[this,evt,listener.callBackObject]);
					if(evt!=null&&evt.cancel)return;
				}
			}
		}}catch(ex){}
	}
	this.swapImage=function(imageNo){this.DropButton.Image.src=(imageNo==1?this.DropButton.ImageUrl1:this.DropButton.ImageUrl2);}
	this.doMouseover=function(){this.DropButton.Image.className=this.DropButton.HoverStyleClassName;}
	this.doMouseup=function(){if(this.Loaded && !this.isReadOnly())this.swapImage(1);}
	this.doMousedown=function(evt,src)
	{
		if(src.id!=this.Id+"_img" && this.isEditable())return;
		if(!this.Loaded || this.isReadOnly())return;
		if(igdrp_all._droped!=null && igdrp_all._droped!=this)igdrp_all._droped.setDropDownVisible(false);
		if(this.Dropped)
		{
			this.setDropDownVisible(false);
			igdrp_all._old=this;
			if(window.setTimeout!=null)window.setTimeout("igdrp_all._old.setFocusTop()",10);
		}
		else
		{
			this.swapImage(2);
			this.setDropDownVisible(true);
		}
		ig_cancelEvent(evt);
	}
	this.doMouseout=function()
	{
		if(!this.Loaded || this.isReadOnly())return;
		if(this.Dropped)this.swapImage(1);
		this.DropButton.Image.className=this.DropButton.DefaultStyleClassName;
	}
	this.doEvt=function(evt,type,src)
	{
		if(type=="resize")this.inputBox.style.width="100%";
		else if(this.isEnabled())switch(type)
		{
			case "mousedown": this.doMousedown(evt,src);return;
			case "mouseup": this.doMouseup();return;
			case "mouseout": this.doMouseout();return;
			case "mouseover": this.doMouseover();return;
		}
	}
}
function ig_EventListener(fref,oCallBackObject){this.handler=fref;this.callBackObject=oCallBackObject;}
// event initialization for menu object
function igdrp_expandEffects(element,props,oDateChooser)
{
	this._expandEffectSupported	= !((navigator.userAgent.toLowerCase().indexOf("win98")!=-1) || (navigator.userAgent.toLowerCase().indexOf("windows 98")!=-1));
	this.updatePostField=function(){oDateChooser.updatePostField();}
	this.stateNode=ig_ClientState.addNode(oDateChooser.stateNode,"ExpandEffects");
	this.Element=element;
	this.duration=props[0];
	this.opacity=props[1];
	this.type=props[2];
	this.shadowColor=props[3];
	this.shadowWidth=props[4];
	this.delay=props[5];
	this.getDuration=function(){return this.duration;}
	this.getOpacity=function(){return this.opacity;}
	this.getType=function(){return this.type;}
	this.getShadowColor=function(){return this.shadowColor;}
	this.getShadowWidth=function(){return this.shadowWidth;}
	this.getDelay=function(){return this.delay;}
	this.setDuration=function(value){this.duration=value;ig_ClientState.setPropertyValue(this.stateNode,"Duration",value);this.updatePostField();}
	this.setOpacity=function(value){this.opacity=value;ig_ClientState.setPropertyValue(this.stateNode,"Opacity",value);this.updatePostField();}
	this.setType=function(value){this.type=value;ig_ClientState.setPropertyValue(this.stateNode,"Type",value);this.updatePostField();}
	this.setShadowColor=function(value){this.shadowColor=value;ig_ClientState.setPropertyValue(this.stateNode,"ShadowColor",value);this.updatePostField();}
	this.setShadowWidth=function(value){this.shadowWidth=value;ig_ClientState.setPropertyValue(this.stateNode,"ShadowWidth",value);this.updatePostField();}
	this.setDelay=function(value){this.delay=value;ig_ClientState.setPropertyValue(this.stateNode,"Delay",value);this.updatePostField();}
	this.applyFilter=function()
	{
		if(!ig_csom.IsIEWin || !this._expandEffectSupported)return;
		if(this.Type!='NotSet' && this.Element!=null)
			this.Element.style.filter="progid:DXImageTransform.Microsoft."+this.type+"(duration="+(this.duration/1000)+");"
		if(this.shadowWidth>0)
		{
			var s=" progid:DXImageTransform.Microsoft.Shadow(Direction=135, Strength="+this.shadowWidth+",color='"+this.shadowColor+"')";
			this.Element.style.filter+=s;
		}
		if(this.opacity<100)
			this.Element.style.filter+=" progid:DXImageTransform.Microsoft.Alpha(Opacity="+this.opacity+");"
		try{if(this.Element.filters[0]!=null)this.Element.filters[0].apply();}catch(ex){}
		this.Element.style.visibility='visible';
		this.Element.style.display="";
		try{if(this.Element.filters[0]!=null)this.Element.filters[0].play();}catch(ex){}
	}
}
// event initialization for combo object
function igdrp_events(events)
{
	this.eventArray=events;
	this.InitializeCombo=events[0];
	this.BeforeDropDown=events[1];
	this.AfterDropDown=events[2];
	this.BeforeCloseUp=events[3];
	this.AfterCloseUp=events[4];
	this.EditKeyDown=events[5];
	this.EditKeyUp=events[6];
	this.ValueChanged=events[7];
	this.TextChanged=events[8];
	this.OnBlur=events[9];
	this.InvalidDateEntered=events[10];
}
// global mouse down event
function igdrp_mouseDown(evnt)
{
	if(!evnt)evnt=window.event;
	if(!evnt || igdrp_all._droped==null)return;
	var container=igdrp_all._droped.ClientUniqueId+"_container";
	var elem=evnt.srcElement;
	if(elem==null)if((elem=evnt.target)==null)elem=this;
	while(elem!=null)
	{
		if(elem.id==container)return;
		elem=elem.parentNode;
	}
	igdrp_all._droped.setDropDownVisible(false);
}
function igdc_dateChooser(_dcElement,dcProps,calID)
{
	var me=new igdrp_combo(_dcElement,dcProps);
	var info=new ig_DateFormatInfo(eval("igdrp_"+me.Id+"_DateFormatInfo"));
	var dateParts=dcProps[11];
	var date=ig_csom.isEmpty(dateParts)?null:new Date(dateParts[0],dateParts[1]-1,dateParts[2]);
	var editor=new igmask_date(me.inputBox,info,date,dcProps[15]!=0,dcProps[14],dcProps[16]);
	if(!ig_csom.isEmpty(dateParts=dcProps[12]))editor.setMinValue(new Date(dateParts[0],dateParts[1]-1,dateParts[2]));
	if(!ig_csom.isEmpty(dateParts=dcProps[13]))editor.setMaxValue(new Date(dateParts[0],dateParts[1]-1,dateParts[2]));
	editor.parent=me;
	var tab=dcProps[17];
	if(tab&&tab>-1)editor.elem.tabIndex=me.DropButton.Image.tabIndex=tab;
	editor.addEventHandler('keyDown',me.onKeyDown);
	editor.addEventHandler('keyup',me.onKeyUp);
	editor.addEventHandler('blur',me.onBlur);
	if(document.all!=null)ig_csom.addEventListener(me.Element,"resize",igdrp_evt);
	ig_csom.addEventListener(me.DropButton.Image,'keypress',me.onImgKeyDown);
	me.getAllowNull=function(){return this.editor.allowNull;}
	me.setAllowNull=function(bAllowNull){this.editor.allowNull=bAllowNull;}
	me.onInvalidValue=function(o,val,oEvent,oEventArgs){if((o=o.parent)!=null)ig_fireEvent(o,o.Events.InvalidDateEntered[0],oEventArgs,oEvent);}
	editor.addEventHandler("invalidvalue",me.onInvalidValue);
	me.onTextChange=function(o,val,oEvent){if((o=o.parent)!=null)ig_fireEvent(o,o.Events.TextChanged[0],val,oEvent);}
	editor.addEventHandler("x",me.onTextChange);
	me.editor=editor;
	me.setMaxDate=function(date)
	{
		this.editor.setMaxValue(date);
		if(this.Calendar)this.Calendar.MaxDate=date;
		var text=(date!=null)?date.getFullYear()+','+(date.getMonth()+1)+','+date.getDate():'null';
		ig_ClientState.setPropertyValue(this.stateNode,"MaxDate",text);
		this.updatePostField();
	}
	me.getMaxDate=function(){return this.editor.getMaxValue();}
	me.setMinDate=function(date)
	{
		this.editor.setMinValue(date);
		if(this.Calendar)this.Calendar.MinDate=date;
		var text=(date!=null)?date.getFullYear()+','+(date.getMonth()+1)+','+date.getDate():'null';
		ig_ClientState.setPropertyValue(this.stateNode,"MinDate",text);
		this.updatePostField();
	}
	me.getMinDate=function(){return this.editor.getMinValue();}
	me.getNullDateLabel=function(){return this.editor.getNullText();}
	me.setNullDateLabel=function(text)
	{
		this.editor.setNullText(text);
		ig_ClientState.setPropertyValue(this.stateNode,"NullDateLabel",text);
		this.updatePostField();
	}
	me.showCalendar=function(){this.setDropDownVisible(true);}
	me.hideCalendar=function(){this.setDropDownVisible(false);}
	me.onDateSelected=function(cal,date)
	{
		var me=cal.ownerDC;
		if(!me.Dropped)return;
		me.setValue(date,true);
		if(me.getAutoCloseUp())me.setDropDownVisible(false,true);
		if(!me.isEditable()|| me.getAutoCloseUp())me.setFocusTop();
	}
	me.setValue=function(date,fireEvent){this.editor.setDate(date);this.updateValue(this.editor.getText(),!fireEvent);}
	me.getValue=function(){return this.editor.getDate();}
	me.Events.InitializeDateChooser=me.Events.InitializeCombo;
	var cal=me.Calendar=ig_csom.isEmpty(calID)?null:igcal_getCalendarById(calID);
	if(cal!=null){cal.ownerDC=me;cal.onValueChanged=me.onDateSelected;me.elemCal=cal.element;}
	ig_fireEvent(me,me.Events.InitializeDateChooser[0],me,me.Id);
	me.Loaded=true;
	return me;
}
function igdc_initDateChooser(id,calID){return igdc_dateChooser(ig_csom.getElementById(id),eval("igdrp_"+id+"_Props"),calID);}
function ig_DateFormatInfo(a)
{
	this.DayNames=a[0];
	this.AbbreviatedDayNames=a[1];
	this.MonthNames=a[2];
	this.AbbreviatedMonthNames=a[3];
	this.FullDateTimePattern=a[4];
	this.LongDatePattern=a[5];
	this.LongTimePattern=a[6];
	this.MonthDayPattern=a[7];
	this.RFC1123Pattern=a[8];
	this.ShortDatePattern=a[9];
	this.ShortTimePattern=a[10];
	this.SortableDateTimePattern=a[11];
	this.UniversalSortableDateTimePattern=a[12];
	this.YearMonthPattern=a[13];
	this.AMDesignator=a[14];
	this.PMDesignator=a[15];
	this.DateSeparator=a[16];	
	this.TimeSeparator=a[17];
}
//<INPUT>,ig_DateFormatInfo,date,longDateFormat
function igmask_date(e,di,v,lf,nullTxt,nullable)
{
	if(e==null)return;
	this.changed=false;
	this.extra=new Object();
	this.nullText=nullTxt;
	this.allowNull=nullable;
	this.repaint0=function(fire)
	{
		if((this.k0==null)||(this.changed && this.elem.value==this.text))return;
		this.elem.value=this.text;
		if(!fire)return;
		this.changed=true;
		this.fireEvt(10,null);
	}
	var id=e.id;
	ig_csom.addEventListener(e,"keydown",igmask_event,false);
	ig_csom.addEventListener(e,"keypress",igmask_event,false);
	ig_csom.addEventListener(e,"keyup",igmask_event,false);
	ig_csom.addEventListener(e,"focus",igmask_event,false);
	ig_csom.addEventListener(e,"blur",igmask_event,false);
	ig_csom.addEventListener(e,"mousedown",igmask_event,false);
	ig_csom.addEventListener(e,"mouseup",igmask_event,false);
	ig_csom.addEventListener(e,"mousemove",igmask_event,false);
	ig_csom.addEventListener(e,"mouseover",igmask_event,false);
	ig_csom.addEventListener(e,"mouseout",igmask_event,false);
	this.id=id;
	e.setAttribute("maskID",id);
	this.elem=e;
	if(e.createTextRange!=null)this.tr=e.createTextRange();
	this.getElement=function(){return this.elem;}
	this.k1=0;
	this.fixKey=0;
	this.useLastGoodValue=true;
	this.getEnabled=function(){return this.elem.disabled!=true;}
	this.getReadOnly=function(){return this.elem.readOnly;}
	this.getText=function(){return this.text;}
	this.getMaxValue=function(){return this.max;}
	this.setMaxValue=function(v){this.max=v;}
	this.getMinValue=function(){return this.min;}
	this.setMinValue=function(v){this.min=v;}
	this.getNullText=function(){return this.nullText;}
	this.setNullText=function(v){this.nullText=v;}
	this.delta=1;
	//
	this.doKey=function(e,a)
	{
		if(a==1 && (e.ctrlKey || e.altKey))return;
		var k=e.keyCode;
		if(k==0 || k==null)if((k=e.which)==null)return;
		if(k<32 && k!=8)return;
		if(a==1)this.k1=k;
		var t0=this.text,t1=this.elem.value;
		var i=t1.length;
		if(a==2)
		{
			this.k1=0;
			if(this.k0<32)return;
			if(t0!=t1)
			{
				this.changed=true;
				if(this.fixKey>0 || i==1)this.afterKey(k,this.fixKey++==1);
				else if(this.fixKey==0)if(i--==0){this.fixKey=2;return;}
			}
			this.k0=-2;
			return;
		}
		switch(k)
		{
			//end//right//home//left
			case 35:case 39:case 36:case 37:if(this.k1==k)return;break;
			//back//del
			case 8:case 46:if(this.k1==k)return;break;
			//up//down
			case 38:case40:
				if(a==1 && this.delta!=0 && !e.shiftKey)this.spin((k==38)?this.delta:-this.delta);
				if(this.k1==k)return;break;
		}
		if(a==1)
		{
			t0=this.getSelectedText();
			if(t0.length>0 || this.sel0<i)this.fixKey=0;
			else if(this.fixKey==0 && this.sel0==i)this.fixKey=1;
			return;
		}
		// fast typing!
		if(this.k0>0)
		{
			if(t0!=t1)this.changed=true;
			if(this.fixKey>0)
				this.afterKey(this.k0,this.fixKey>0);
		}
		var newK=this.filterKey(k,this.fixKey>0);
		if(newK!=k && this.tr==null)newK=0;
		if(newK==0)ig_cancelEvent(e);
		else if(newK!=k && this.tr!=null)e.keyCode=newK;
		this.k0=newK;
	}
	this.stoi=function(s)
	{
		switch(s.toLowerCase())
		{
			case "keypress":return 0;
			case "keydown":return 1;
			case "keyup":return 2;
			case "mousedown":return 3;
			case "mouseup":return 4;
			case "mousemove":return 5;
			case "mouseover":return 6;
			case "mouseout":return 7;
			case "focus":return 8;
			case "blur":return 9;
			case "invalidvalue":return 11;
		}
		return 10;//valuechanged
	}
	this.doEvtM=function(e)
	{
		if(e==null || !this.getEnabled())return;
		var v=!this.getReadOnly(),a=this.stoi(e.type);
		if(a<8)this.fireEvt(a,e);
		if(a<3 && v)this.doKey(e,a);
		if(a>=8)
		{
			if((a==8)==this.foc)return;
			this.foc=(a==8);
			if(a==9 && v)
			{
				if(!this.changed)this.changed=this.text!=this.elem.value;
				if(this.changed)
				{
					this.text=this.elem.value;
					if(this.elem.onchange!=null)this.elem.onchange();
				}
			}
			if(a==8 && v)
			{
				if(this.useLastGoodValue)this.setGood();
				if((v=this.elem.value)!=this.text){this.paste(v);return;}
			}
			this.repaint(a==9 && this.changed);
			this.fireEvt(a,e);
			if(this.foc){this.changed=false;this.select();}
			return;
		}
		if((v=this.elem.value)!=this.text && (this.k1==0 || a<4))
		{
			this.changed=true;
			if(a>3 && this.k1==0)this.paste(v);
			else this.text=v;
			this.fireEvt(10,e);
		}
	}
	this.events=new Array(11);
	this.evtH=function(n,f,add)
	{
		n=this.stoi(n);
		var e=this.events[n];
		if(e==null){if(add)e=this.events[n]=new Array();else return;}
		n=e.length;
		while(n-->0)if(e[n]==f){if(!add)e[n]=null;return;}
		if(add)e[e.length]=f;
	}
	this.removeEventHandler=function(name,fref){this.evtH(name,fref,false);}
	this.addEventHandler=function(name,fref){this.evtH(name,fref,true);}
	this.fireEvt=function(id,e)
	{
		var evts=this.events[id];
		var i=(evts==null)?0:evts.length;
		if(i==0)return false;
		var evt=this.Event;
		if(evt==null)evt=this.Event=new ig_EventObject();
		var cancel=false;
		while(i-->0)
		{
			if(evts[i]==null)continue;
			evt.reset();
			evt.event=e;
			evts[i](this,this.elem.value,evt,this.extra);
			if(evt.cancel)cancel=true;
		}
		return cancel;
	}
	this.select=function(s0,s1)
	{
		var i=this.elem.value.length;
		if(s1==null)if((s1=s0)==null){s0=0;s1=i;}
		if(s1>=i)s1=i;
		else if(s1<s0)s1=s0;
		if(s0>s1)s0=s1;
		if(this.elem.selectionStart!=null)
		{
			this.elem.selectionStart=this.sel0=s0;
			this.elem.selectionEnd=this.sel1=s1;
		}
		if(this.tr==null)return;
		this.sel0=s0;this.sel1=s1;
		s1-=s0;
		this.tr.move("textedit",-1);
		this.tr.move("character",s0);
		if(s1>0)this.tr.moveEnd("character",s1);
		this.tr.select();
	}
	this.getSelectedText=function()
	{
		var r="";
		this.sel0=this.sel1=-1;
		if(this.elem.selectionStart!=null)
		{
			if((this.sel0=this.elem.selectionStart)<(this.sel1=this.elem.selectionEnd))
				r=this.elem.value.substring(this.sel0,this.sel1);
			return r;
		}
		if(this.tr==null)return r;
		var sel=document.selection.createRange();
		r=sel.duplicate();
		r.move("textedit",-1);
		this.sel0=0;
		try{while(r.compareEndPoints("StartToStart",sel)<0)
		{
			if(this.sel0++>1000)break;
			r.moveStart("character",1);
		}}catch(ex){}
		r=sel.text;
		this.sel1=this.sel0+r.length;
		return r;
	}
	//date==>>
	// 1-d,2-m,3-y
	// 00007-1st,00070-2nd,00700-3rd,01000-dd,02000-mm,04000-yyyy
	this.order=7370;//2|(1<<3)|(3<<6)|(1<<10)|(1<<11)|(1<<12)
	this.sepCh="/";
	this.sep=47;
	this.autoCentury=true;
	this.setLongFormat=function(v){this.longFormat=v;}
	this.getLongFormat=function(){return this.longFormat;}
	this.getDateInfo=function(){return this.info;}
	this.setDateInfo=function(v)
	{
		this.info=v;
		var sep0=null;
		if(v!=null)
		{
			v=this.info.ShortDatePattern;
			if(ig_csom.isEmpty(sep0=this.info.DateSeparator))sep0=null;
		}
		if(v==null || v.length<3)v="MM/dd/yyyy";
		var ii=v.length;
		var y=0,m=0,d=0,sep=0,i=-1,o=0;
		while(++i<ii)
		{
			var ch=v.charAt(i);
			if(ch=='d')
			{
				if(d++>0){o |= 1024;continue;}
				o |= 1<<(sep++*3);
			}
			else if(ch=='m' || ch=='M')
			{
				if(m++>0){o |= 2048;continue;}
				o |= 2<<(sep++*3);
			}
			else if(ch=='y')
			{
				if(y++>0){if(y>2)o |= 4096;continue;}
				o |= 3<<(sep++*3);
			}
			else if(sep==1 && sep0==null)sep0=ch;
		}
		if(sep0!=null)
		{
			this.sepCh=sep0;
			this.sep=sep0.charCodeAt(0);
		}
		if(m==0)o |= 1<<(sep++*3);
		if(d==0)o |= 2<<(sep++*3);
		if(y==0)o |= 3<<(sep++*3);
		this.order=o;
		this.mask=v;
	}
	this.setGood=function()
	{
		var d=this.date;
		if(d==null)
		{
			if(this.elem.value.length>0)d=this.toDate();
			if(d==null && !this.allowNull)d=new Date();
			this.date=d;
		}
		this.good=d;
	}
	this.focusText=function()
	{
		var v,i=-1,t="",d=this.date;
		if(d==null){if(this.allowNull)return t;this.date=d=new Date();}
		while(++i<3)
		{
			if((v=(this.order >> i*3)& 3)==0)break;
			if(i>0)t+=this.sepCh;
			switch(v)
			{
				case 1:v=d.getDate();if(v<10 && (this.order & 1024)!=0)t+=0;break;
				case 2:v=d.getMonth()+1;if(v<10 && (this.order & 2048)!=0)t+=0;break;
				case 3:v=d.getFullYear();if((this.order & 4096)==0)v%=100;if(v<10)t+=0;break;
			}
			t+=v;
		}
		return t;
	}
	this.setText=function(v){this.setDate(this.toDate(v));}
	this.toDate=function(t,inv)
	{
		if(t==null){if(this.getReadOnly()||this.k0==null)return this.date;t=this.elem.value;}
		var ii=t.length;
		if(ii>12)return this.date;
		if(ii==0 && this.allowNull)return null;
		var y=-1,m=-1,d=-1,sep=0,i=-1,f=0;
		while(++i<=ii)
		{
			var ch=(i<ii)?t.charCodeAt(i):this.sep;
			if(ch==this.sep)
			{
				if(i+1==ii)break;
				switch((this.order >> sep*3)& 3)
				{
					case 1:d=f;break;
					case 2:m=f;break;
					case 3:y=f;break;
				}
				sep++;
			}
			ch-=48;
			if(ch>=0 && ch<=9)f=f*10+ch;
			else f=0;
		}
		f=null;
		i=0;
		this.extra.year=y;this.extra.month=m;this.extra.day=d;this.extra.reason=(ii>0)?2:1;
		if(sep!=3)i++;
		else
		{
			if(d<1 || d>31 || m<1 || m>12 || y<0 || y>9998)i++;
			else
			{
				if(m==2 && d>29)i=d=29;
				if(this.autoCentury){if(y<37)y+=2000;else if(y<100)y+=1900;}
				f=new Date(y,m-1,d);
				if(y<100 && f.setFullYear!=null)f.setFullYear(y);
				if(f.getDate()!=d)
				{
					f=new Date(i=y,m-1,d-1);
					if(y<100 && f.setFullYear!=null)f.setFullYear(y);
				}
				d=f.getTime();
				if((m=this.max)!=null)if(d>m.getTime()){f=m;if(i++==0)this.extra.reason=0;}
				if((m=this.min)!=null)if(d<m.getTime()){f=m;if(i++==0)this.extra.reason=0;}
			}
		}
		this.extra.date=f;
		if(inv && i>0)if(this.fireEvt(11,null))f=this.date;
		return f;
	}
	this.spin=function(v)
	{
		var d=this.toDate();
		if(d==null)d=new Date();
		this.setDate(new Date(d.getFullYear(),d.getMonth(),d.getDate()+v));
	}
	this.isValid=function(){return this.toDate()!=null;}
	this.repaint=function(fire)
	{
		var t=null;
		if(!this.getReadOnly()&& this.getEnabled())
		{
			if(this.foc)t=this.focusText();
			else if(this.changed)
			{
				var d=this.toDate(null,true);
				if(d!=null || this.allowNull)this.date=d;
			}
		}
		this.text=(t==null)?this.staticText():t;
		this.repaint0(fire);
	}
	this.staticText=function()
	{
		if(this.date==null)
		{
			if(this.useLastGoodValue && this.good!=null && this.text.length>0)this.date=this.good;
			else{if(this.allowNull)return this.nullText;this.date=new Date();}
		}
		var t=this.info;
		if(t!=null)t=this.longFormat?t.LongDatePattern:t.ShortDatePattern;
		else if(this.longFormat && this.date.toLocaleDateString!=null)return this.date.toLocaleDateString();
		if(t==null || t.length<2)t=this.mask;
		var f="yyyy",v=this.date.getFullYear();
		if(t.indexOf(f)<0)
		{
			if(t.indexOf(f="yy")<0)v=-1;
			else if((v%=100)<10)v="0"+v;
		}
		if(v!=-1)t=t.replace(f,v);
		f="MMM";
		v=this.date.getMonth()+1;
		var mm=null,dd=null;
		if(t.indexOf(f)<0)
		{
			if(t.indexOf(f="MM")<0){if(t.indexOf(f="M")<0)v=-1;}
			else if(v<10)v="0"+v;
			if(v!=-1)t=t.replace(f,v);
		}
		else
		{
			if(t.indexOf("MMMM")>=0)f="MMMM";
			if(this.info!=null)mm=(f.length==4)?this.info.MonthNames:this.info.AbbreviatedMonthNames;
			if(mm!=null)mm=(mm.length>=v)?mm[v-1]:null;
			t=t.replace(f,(mm==null)?(""+v):"[]");
		}
		f="ddd";
		v="";
		if(t.indexOf(f)>=0)
		{
			if(t.indexOf("dddd")>=0)f="dddd";
			if(this.info!=null)dd=(f.length==4)?this.info.DayNames:this.info.AbbreviatedDayNames;
			v+=this.date.getDay();
			if(dd!=null)dd=(dd.length>=v)?dd[v]:null;
			t=t.replace(f,(dd==null)?v:"()");
		}
		f="dd";
		v=this.date.getDate();
		if(t.indexOf(f)<0){if(t.indexOf(f="d")<0)v=-1;}
		else if(v<10)v="0"+v;
		if(v!=-1)t=t.replace(f,v);
		if(mm!=null)t=t.replace("[]",mm);
		if(dd!=null)t=t.replace("()",dd);
		return t;
	}
	this.getDate=function(){return this.foc?this.toDate():this.date;}
	this.setDate=function(v)
	{
		if(v!=null && v.length!=null)v=(v.length<3)?null:this.toDate(v);
		if(v==null && !this.allowNull)if((v=this.date)==null)v=new Date();
		if(v!=null)
		{
			var m,d=v.getTime();
			if((m=this.max)!=null)if(d>m.getTime())v=m;
			if((m=this.min)!=null)if(d<m.getTime())v=m;
		}
		else this.good=null;
		var fire=this.date!=v;
		this.date=v;
		this.text=this.foc?this.focusText():this.staticText();
		this.repaint0(fire);
	}
	// return char that can be added or -1
	this.canAdd=function(k,t)
	{
		var ii=t.length-1;
		if(ii<0)return (k==this.sep)?-1:k;
		if(t.charCodeAt(ii)==this.sep)
			return (k==this.sep)?-1:k;
		var f=0,sep=0,i=-1,n=0;
		while(++i<=ii)
		{
			var ch=t.charCodeAt(i);
			if(ch==this.sep){if(sep++>1)return -1;n=f=0;continue;}
			n++;
			f=f*10+ch-48;
		}
		if(sep>1 && k==this.sep)return -1;
		i=(this.order >> sep*3)& 3;
		if(i==1){if(n>1 || f*10+k-48>31)n=4;}
		if(i==2){if(n>1 || f*10+k-48>12)n=4;}
		return (n<4)?k:((sep>1)?-1:this.sep);
	}
	this.afterKey=function(k,fix)
	{
		var t=this.elem.value;
		if(fix)
		{
			var sep=0,i=-1,f=0,i0=0,ii=t.length,tt="";
			while(++i<=ii)
			{
				var ch=(i<ii)?t.charCodeAt(i):this.sep;
				if(ch==this.sep)
				{
					switch((this.order>>sep*3)& 3)
					{
						case 1:if(f>31){while(f>31)f=Math.floor(f/10);}else f=-1;break;
						case 2:if(f>12){while(f>12)f=Math.floor(f/10);}else f=-1;break;
						case 3:if(f<9999)f=-1;else while(f>9999)f=Math.floor(f/10);break;
					}
					if(f<0)tt+=t.substring(i0,i);
					else tt+=f;
					if(i<ii)tt+=this.sepCh;
					sep++;
					i0=i+1;
				}
				ch-=48;
				if(ch>=0 && ch<=9)f=f*10+ch;
				else f=0;
			}
			t=tt;
		}
		if(this.k0>0)if(this.canAdd(48,t)==this.sep)t+=this.sepCh;
		this.elem.value=t;
	}
	this.filterKey=function(k,fix)
	{
		if(k!=this.sep && (k<48 || k>57))
			// check for -_\/space.,:;"%
			if(this.tr!=null && this.isSep(k))k=this.sep;
			else return 0;
		if(k==this.sep && this.sel0==0)return 0;
		if(fix && this.canAdd(k,this.elem.value)!=k)k=0;
		return k;
	}
	this.isSep=function(k){return k==this.sep || k==45 || k==92 || k==95 || k==47 || k==32 || k==46 || k==44 || k==58 || k==59;}
	this.paste=function(old)
	{
		var ch,sep=true,v="",f=0;
		for(var i=0;i<old.length;i++)
		{
			ch=old.charCodeAt(i);
			if(ch>=48 && ch<=57)sep=false;
			else{if(!this.isSep(ch))continue;if(f>1)break;if(sep)continue;sep=true;f++;}
			v+=sep?this.sepCh:old.charAt(i);
		}
		this.text="";
		this.setText(v);
	}
	this.setLongFormat(lf);
	this.setDateInfo(di);
	this.setDate(v);
	this.init0=function()
	{
		var e=this.parent.Element,w=this.elem.offsetWidth,img=this.parent.DropButton.Image;
		var w0=e.offsetWidth,wi=img.offsetWidth;
		if(w0==null||w0<5)e.style.width=w0=120;
		if(w==null||w<5){if(wi==null)wi=17;if((w=w0-wi)<5)w=5;}
		this.elem.style.width=w;
		this.k0=-2;
		this.elem.value=this.text;
	}
	igdrp_all[id]=this;
}
function igmask_event(e)
{
	var o,c=null,id=null,i=0;
	if(e==0){for(id in igdrp_all)if((o=igdrp_all[id])!=null)if(o.init0!=null)o.init0();return;}
	if(e==null)if((e=window.event)==null)return;
	if((o=e.srcElement)==null)if((o=e.target)==null)o=this;
	while(true)
	{
		if(o==null || i++>2)return;
		try{if(o.getAttribute!=null)id=o.getAttribute("maskID");}catch(ex){}
		if(!ig_csom.isEmpty(id)){c=igdrp_all[id];break;}
		if((c=o.parentNode)!=null)o=c;
		else o=o.parentElement;
	}
	if(c!=null && c.doEvtM!=null)c.doEvtM(e);
}
