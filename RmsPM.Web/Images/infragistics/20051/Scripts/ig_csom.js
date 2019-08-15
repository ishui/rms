/* 
Infragistics Common Script 
Version 5.1.20051.37
Copyright (c) 2001-2004 Infragistics, Inc. All Rights Reserved.

The JavaScript functions in this file are intended for the internal use of the Intragistics Web Controls only.

*/

// ig_csom class implements the basic browser independent functionality
function ig_initcsom() 
{
	// Public Properties
	this.ScriptVersion		="3.1.20042.4"; 
	
	// navigator.userAgent is not being validated because ig_csom should not be used for unknown browsers.
	this.AgentName			=navigator.userAgent.toLowerCase();
	this.MajorVersionNumber =parseInt(navigator.appVersion);
	this.IsDom				=(document.getElementById)?true:false;
	this.IsNetscape			=(document.layers?true:false);
	this.IsNetscape6		=(this.IsDom&&navigator.appName=="Netscape");
	this.IsOpera			=this.AgentName.indexOf('opera')!=-1;
	this.IsMac				=(this.AgentName.indexOf("mac")!=-1);
	this.IsIE				=(document.all?true:false);
	this.IsIE4				=(document.all&&!this.IsDom)?true:false;
	this.IsIE4Plus			=(this.IsIE && this.MajorVersionNumber >= 4)?true:false;
	this.IsIE5				=(document.all&&this.IsDom)?true:false;
	this.IsIE50				=this.IsIE5&&(this.AgentName.indexOf("msie 5.0")!=-1);
	this.IsWin				=((this.AgentName.indexOf("win")!=-1) || (this.AgentName.indexOf("16bit")!=-1));
	this.IsIE55				=((navigator.userAgent.indexOf("MSIE 5.5") != -1) && (navigator.userAgent.indexOf("Windows") != -1)); 
	this.IsIEWin			=(this.IsIE && this.IsWin); 
	this.IsIE6				=((navigator.userAgent.indexOf("MSIE 6.0") != -1) && (navigator.userAgent.indexOf("Windows") != -1)); 
	this.IsIE55Plus			=(this.IsIE55 || this.IsIE6); 
	this.IsSafari			=this.AgentName.indexOf('safari')!=-1;
	// Obtains an element object based on its Id
	this.getElementById = function (tagName)
	{
		if(this.IsIE)
			return document.all[tagName];
		else
			return document.getElementById(tagName);
	}

	this.isArray = function(a) {
		return a!=null && a.length!=null;
	}
	
	this.isEmpty = function(o) {
		return !(this.isArray(o) && o.length>0);
	}
	
	this.notEmpty = function(o) {
		return (this.isArray(o) && o.length>0);
	}

	// Adds an event listener to an html element.
	this.addEventListener = function(obj,eventName,callbackFunction,flag)
	{ 
		if (obj.addEventListener) {
			obj.addEventListener(eventName,callbackFunction,flag);
		}
		else if (obj.attachEvent) {
			obj.attachEvent("on"+eventName,callbackFunction);
		}
		else {
			eval("obj.on"+eventName+"="+callbackFunction);
		}
	}
	
	// Obtains the proper source element in relation to an event
	this.getSourceElement = function (evnt, o)
	{
		if(evnt.target) // This does not appear to be working for Netscape
			return evnt.target;
		else 
		if(evnt.srcElement)
			return evnt.srcElement;
		else
			return o;
	}
	
	this.getText = function (e){
		if(e.innerHTML)
			return e.innerHTML;
	}
	
	this.setText = function (e, text)
	{
		if(e.innerHTML)
			e.innerHTML = text;
	}
	this.setEnabled = function (e, bEnabled)
	{
		if(this.IsIE)
			e.disabled = !bEnabled;
	}
	this.getEnabled = function (e){
		if(this.IsIE)
			return !e.disabled;
	}

	this.navigateUrl =	function (targetUrl, targetFrame)
	{
		if(targetUrl == null || targetUrl.length == 0)
			return;
		var newUrl=targetUrl.toLowerCase();
		if(newUrl.indexOf("javascript") != -1)
			eval(targetUrl);
		else 
		if(targetFrame != null && targetFrame!="")	{
			if(ig.getElementById(targetFrame) != null) 
				ig.getElementById(targetFrame).src = targetUrl;
			else {
				var oFrame = ig_searchFrames(top, targetFrame);
				if(oFrame != null)
					oFrame.location=targetUrl;
				else 
				if(targetFrame == "_self" 
					|| targetFrame == "_parent"
					|| targetFrame == "_media"
					|| targetFrame == "_top"
					|| targetFrame == "_blank"
					|| targetFrame == "_search")
					window.open(targetUrl, targetFrame);
				else
					window.open(targetUrl);
			}
		}
		else {
			try {
				location.href = targetUrl;
			}
			catch (x) {
			}
		}
	}
	
	function ig_searchFrames(frame, targetFrame) {
		if(frame.frames[targetFrame] != null)
			return frame.frames[targetFrame];
		var i;
		for(i=0; i<frame.frames.length; i++) {
			var subFrame = ig_searchFrames(frame.frames[i], targetFrame);
			if(subFrame != null)
				return subFrame; 
		}
		return null;
	}
	
	this.findControl=function(startElement,idList,closestMatch){
		var item;
		var searchString="";
		var i=0;
		var partialId=idList.split(":");
		while(partialId[i+1]!=null&&partialId[i+1].length>0){
			searchString+=partialId[i]+".*";
			i++;
		}
		searchString+=partialId[i]+"$";
		var searchExp=new RegExp(searchString);
		var curElement;
		if(startElement != null)
			curElement=startElement.firstChild;
		else
			curElement = window.document.firstChild;
		while(curElement!=null){
			if(curElement.id!=null&&(curElement.id.search(searchExp))!=-1){
				//ig_delete(searchExp);
				ig_dispose(searchExp);
				return curElement;
			}
			item=this.findControl(curElement,idList);
			if(item!=null){
				//ig_delete(searchExp);
				ig_dispose(searchExp);
				return item;
			}
			curElement=curElement.nextSibling;		
		}
		//ig_delete(searchExp);
		ig_dispose(searchExp);
		if(closestMatch)
			return findClosestMatch(startElement,partialId);
		else return null;
	}
	this.createTransparentPanel=function (){
		if(!this.IsIE)return null;
		var transLayer=document.createElement("IFRAME");
		transLayer.style.zIndex=1000;
		transLayer.frameBorder="no";
		transLayer.scrolling="no";
		transLayer.style.filter="progid:DXImageTransform.Microsoft.Alpha(Opacity=0);";
		transLayer.style.visibility='hidden';
		transLayer.style.display='none';
		transLayer.style.position="absolute";
		transLayer.src='javascript:new String("<html></html>")';
		var e = document.body.firstChild;
		document.body.insertBefore(transLayer, e);
		return new ig_TransparentPanel(transLayer);
	}
}
// protect delete

function ig_delete(o)
{
	// redirect to ig_dispose
	ig_dispose(o);
	//if(o != null) try{delete o;}catch(e){try{delete(o);}catch(e1){}}
}

// cancel response of browser on event
function ig_cancelEvent(e)
{
	if(e == null) if((e = window.event) == null) return;
	if(e.stopPropagation != null) e.stopPropagation();
	if(e.preventDefault != null) e.preventDefault();
	e.cancelBubble = true;
	e.returnValue = false;
}
function ig_TransparentPanel(transLayer){
	this.Element=transLayer;
	this.show=function(){
		this.Element.style.visibility="visible";
		this.Element.style.display="";
	}
	this.hide=function(){
		this.Element.style.visibility="hidden";
		this.Element.style.display="none";
	}
	this.setPosition=function(top,left,width,height){
		this.Element.style.top=top;
		this.Element.style.left=left;
		this.Element.style.width=width;
		this.Element.style.height=height;
	}
}
var ig_csom = new ig_initcsom();

//Emulate 'apply' if it doesn't exist.
if ((typeof Function != 'undefined')&&
    (typeof Function.prototype != 'undefined')&&
    (typeof Function.apply != 'function')) {
    Function.prototype.apply = function(obj, args){
        var result, fn = 'ig_apply'
        while(typeof obj[fn] != 'undefined') fn += fn;
        obj[fn] = this;
        var length=(((ig_csom.isArray(args))&&(typeof args == 'object'))?args.length:0);
		switch(length){
		case 0:
			result = obj[fn]();
			break;
		default:
			for(var item=0, params=''; item<args.length;item++){
			if(item!=0) params += ',';
			params += 'args[' + item +']';
			}
			result = eval('obj.'+fn+'('+params+');');
			break;
		}
        //ig_delete(obj[fn]);
        ig_dispose(obj[fn]);
        return result;
    };
}
// deprecated
var ig = ig_csom;

function findClosestMatch(startElement,partialId){
	var item;
	var searchString="";
	var i=0;
	while(partialId[i+1]!=null&&partialId[i+1].length>0){
		searchString+="("+partialId[i]+")?";
		i++;
	}
	searchString+=partialId[i]+"$";
	var searchExp=new RegExp(searchString);
	var curElement=startElement.firstChild;
	while(curElement!=null){
		if(curElement.id!=null&&(curElement.id.search(searchExp))!=-1){
			return curElement;
		}
		item=findClosestMatch(curElement,partialId);
		if(item!=null)return item;
		curElement=curElement.nextSibling;		
	}
	return null;
}

function ig_EventObject(){
	this.event=null;
	this.cancel=false;
	this.cancelPostBack=false;
	this.needPostBack=false;
	this.reset=ig_resetEvent;
}
function ig_resetEvent(){
	this.event=null;
	this.cancel=false;
	this.cancelPostBack=false;
	this.needPostBack=false;
}
/***
* This Function should be called when an event needs to be fired.
* The Event should be created using the ig_EventObject function above.
* @param oControl - the javascript object representation of your control.
* @param eventName - the name of the function that should handle this event.
* Other parameters should be appended as needed when calling this function.
* The last parameter should always be the Event object created by the ig_EventObject function.
****/
function ig_fireEvent(oControl,eventName)
{
	if(!eventName||oControl==null) return false;

	var sEventArgs = eventName + "(oControl";
	
	for (i = 2; i < ig_fireEvent.arguments.length; i++)
		sEventArgs += ", ig_fireEvent.arguments[" + i + "]";
	sEventArgs += ");";
	try{eval(sEventArgs);}
	catch(ex){window.status = "Can't eval " + sEventArgs; return false;}
	return true;

}

function ig_dispose(obj)
{
	if (ig.IsIE && ig.IsWin)	
		for(var item in obj)
		{
			if(typeof(obj[item])!="undefined" && obj[item]!=null && !obj[item].tagName && !obj[item].disposing && typeof(obj[item])!="string")
			{
				try {
					obj[item].disposing=true;
					ig_dispose(obj[item]);
				} catch(e1) {;}
			}
			try{delete obj[item];}catch(e2){;}
		}
}

function ig_initClientState(){
	this.XmlDoc=document;
	this.createRootNode=function(){
		if(!ig.IsIE){
			var str ='<?xml version="1.0"?><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"> <html xmlns="http://www.w3.org/1999/xhtml"><ClientState id="vs"></ClientState></html>';
			var p = new DOMParser();
			var doc = p.parseFromString(str,"text/xml");
			this.XmlDoc=doc;
			return doc.getElementById("vs");
		}
		if(ig.IsIE50)this.XmlDoc=new ActiveXObject("Microsoft.XMLDOM");
		return this.createNode("ClientState");
	}
	this.setPropertyValue=function(element,name,value){
		if(element!=null)element.setAttribute(name,escape(value));
	}
	this.getPropertyValue=function(element,name){
		if(element==null)return "";
		return unescape(element.getAttribute(name));
	}
	this.addNode=function(element,nodeName){
		var newNode=this.createNode(nodeName);
		if(element!=null)element.appendChild(newNode);
		return newNode;
	}
	this.removeNode=function(element,nodeName){
		var nodeToRemove=this.findNode(element,nodeName);
		if(element!=null)
			return element.removeChild(nodeToRemove);
		return null;
	}
	this.createNode=function(nodeName){
		return this.XmlDoc.createElement(nodeName);
	}
	this.findNode=function(element,node){
		if(element==null)return null;
		var curElement=element.firstChild;
		while(curElement!=null){
			if(curElement.nodeName==node || curElement==node){
				return curElement;
			}
			var item=this.findNode(curElement,node);
			if(item!=null)return item;
			curElement=curElement.nextSibling;		
		}
		return null;
	}
	this.getText=function(element){
		if(element==null)return "";
		if(ig.IsIE55Plus)return escape(element.innerHTML);
		return escape(this.XmlToString(element));
	}
	this.XmlToString=function(startElem){
		var str="";
		if(!startElem)return "";
		var curElement=startElem.firstChild;
		while(curElement!=null){
			str+="<"+curElement.tagName+" ";

			for(var i=0; i<curElement.attributes.length;i++)
			{
				var attrib=curElement.attributes[i];
				str+=attrib.nodeName+"=\""+attrib.nodeValue+"\" ";
			}

			str+=">";
			str+=this.XmlToString(curElement);
			str+="</"+curElement.tagName+">";
			curElement=curElement.nextSibling;		
		}
		return str;
	}
}
//
function ig_xmlNode(name)
{
	this.name = name;
	this.getText = function(){return escape(this.toString());}
	this.childNodes = new Array();
	this.toString = function()
	{
		var i, s = (this.name == null) ? "" : "<" + this.name;
		if(this.props != null) for(i = 0; i < this.props.length; i++)
			s += " " + this.props[i].name + "=\"" + this.props[i].value + "\"";
		if(this.name != null) s += ">";
		for(i = 0; i < this.childNodes.length; i++)
			s += this.childNodes[i].toString();
		if(this.name != null) s += "</" + this.name + ">";
		return s;
	}
	this.addNode = function(node, unique)
	{
		if(node == null) return null;
		if(unique == true) if((unique = this.findNode(node)) != null) return unique;		
		if(node.name == null) node = new ig_xmlNode(node);
		node.parentNode = this;
		return this.childNodes[this.childNodes.length] = node;
	}
	this.appendChild = this.addNode;
	this.setAttribute = function(name, value)
	{
		if(name == null) return;
		if(this.props == null) this.props = new Array();
		var prop, i = this.props.length;
		value = (value == null) ? "" : value;
		while(i-- > 0)
		{
			prop = this.props[i];
			if(prop.name == name){prop.value = value; return;}
		}
		prop = new Object();
		prop.name = name;
		prop.value = value;
		this.props[this.props.length] = prop;
	}
	this.setPropertyValue = function(name, value){this.setAttribute(name, (value == null) ? value : escape(value));}
	this.findNode = function(node, descendants)
	{
		if(node != null) for(var i = 0; i < this.childNodes.length; i++)
		{
			var n = this.childNodes[i];
			if(n != null)
			{
				if(n.name == node || n == node)
				{
					n.index = i;
					return n;
				}
				if(descendants == true && (n = n.findNode(node)) != null) return n;
			}
		}
		return null;
	}
	this.removeNode = function(node)
	{
		var n = this.findNode(node);
		if(n != null)
			n.parentNode.childNodes.splice(n.index, 1);
		return n;
	}
	this.getPropertyValue = function(name)
	{
		var i = (this.props == null) ? 0 : this.props.length;
		while(i-- > 0)
			if(this.props[i].name == name)
				return unescape(this.props[i].value);
		return null;
	}
}
function ig_xmlNodeStatic()
{
	this.createRootNode = function(){return new ig_xmlNode(null);}
	this.addNode = function(e, n){return (e == null) ? (new ig_xmlNode(n)) : e.addNode(n);}
	this.removeNode = function(e, n){return (e == null) ? e : e.removeNode(n);}
	this.findNode = function(e, n){return (e == null) ? e : e.findNode(n);}
	this.setPropertyValue = function(e, n, v){if(e != null)e.setPropertyValue(n, v);}
	this.getPropertyValue = function(e, n){return (e == null) ? "" : e.getPropertyValue(n);}
	this.getText = function(e)
	{
		var s = "", i = (e == null) ? 0 : e.childNodes.length;
		for(var j = 0; j < i; j++) s += e.childNodes[j].getText();
		return s;
	}
}

var ig_ClientState=null;
if (!ig.IsIE55Plus) ig_ClientState = new ig_xmlNodeStatic();
else ig_ClientState=new ig_initClientState();
