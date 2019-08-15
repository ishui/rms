/* 
 * Infragistics Web Chart Common Script 
 * Author: Kamal Verma
 * Version 5.1.20051.37
 * Last modified: Mon Jul 28 09:57:31  2003
 * Copyright(c) 2001-2004 Infragistics, Inc. All Rights Reserved.
 * The JavaScript functions in this file are intended for the internal use of the Intragistics Web Controls only.
 */

// Global array to relate Elements with Objects.
var ID2OBJECT = new Array();

// IGBrowser class implements the basic browser independent functionality
function IGBrowser() 
{
	// Public Properties
	this.ScriptVersion		="5.1.20051.37"; 
	this.AgentName			=navigator.userAgent.toLowerCase();
	this.MajorVersionNumber =parseInt(navigator.appVersion);
	this.IsDom				=(document.getElementById)?true:false;
	this.IsSafari				=navigator.userAgent.toLowerCase().indexOf("safari")!=-1;
	this.IsNetscape			=(document.layers?true:false);
	this.IsNetscape4Plus	=(this.IsNetscape && this.MajorVersionNumber >=4)?true:false;
	this.IsNetscape6		=!this.IsSafari&&(this.IsDom&&navigator.appName=="Netscape");
	this.IsOpera			=this.AgentName.indexOf('opera')!=-1;
	this.IsMac				=(this.AgentName.indexOf("mac")!=-1);
	this.IsIE				=(document.all?true:false);
	this.IsIE4				=(document.all&&!this.IsDom)?true:false;
	this.IsIE4Plus			=(this.IsIE && this.MajorVersionNumber >= 4)?true:false;
	this.IsIE5				=(document.all&&this.IsDom)?true:false;
	this.IsWin				=((this.AgentName.indexOf("win")!=-1) || (this.AgentName.indexOf("16bit")!=-1));
	this.ID					="IGB";

	//Emulate 'apply' if it doesn't exist.
	if ((typeof Function != 'undefined')&&(typeof Function.prototype != 'undefined')&&(typeof Function.apply != 'function'))
	{
	    Function.prototype.apply = function(obj, args)
	    {
			var result, fn = 'ig_apply';
			while(typeof obj[fn] != 'undefined') fn += fn;
			obj[fn] = this;
			var length=(((ig_csom.isArray(args))&&(typeof args == 'object'))?args.length:0);
			switch(length)
			{
				case 0:
					result = obj[fn]();
					break;
				default:
					for(var item=0, params=''; item<args.length;item++)
					{
						if(item!=0) params += ',';
						params += 'args[' + item +']';
					}
					result = eval('obj.'+fn+'('+params+');');
					break;
			}
			this.Dispose(obj[fn]);
			return result;
		}
	}
	this.Dispose = function(obj)
	{
		if (this.IsIE && this.IsWin)	
			for(var item in obj)
			{
				if(typeof(obj[item])!="undefined" && obj[item]!=null && !obj[item].tagName && !obj[item].disposing && typeof(obj[item])!="string")
				{
					try 
					{
						obj[item].disposing=true;
						ig_dispose(obj[item]);
					} 
					catch(e1) {;}
			}
			try{delete obj[item];}catch(e2){;}
		}
	}

	this.GetObject = function(id, doc) 
	{
		var i,x;  
		if (!doc) doc=document; 
		if (!(x=doc[id])&&doc.all) x=doc.all[id]; 
		//for(i=0;!x&&i<doc.forms.length;i++) x=doc.forms[i][id];
		//for(i=0;!x&&doc.layers&&i<doc.layers.length;i++) x=this.GetObject(id,doc.layers[i].document);
		if (!x && document.getElementById) x=document.getElementById(id); 
		return x;
	}

	this.GetHeight = function(obj) 
	{ 
		var h=0; 
		if (this.IsNetscape) 
		{ 
			h=(obj.height)? obj.height:obj.clip.height; 
			return h; 
		} 
		h=(this.IsOpera)? obj.style.pixelHeight:obj.offsetHeight; 
		return h; 
	}

	this.SetHeight = function(obj,h) 
	{ 
		if (this.IsNetscape) 
		{
			if (obj.clip) obj.clip.bottom=h; 
		} else if (this.IsOpera) obj.style.pixelHeight=h;
		else obj.style.height=h; 
	}

	this.GetWidth = function(obj) 
	{ 
		var w=0; 
		if (this.IsNetscape) 
		{ 
			w=(obj.width)? obj.width:obj.clip.width; 
			return w; 
		} 
		w=(this.IsOpera)? obj.style.pixelWidth:obj.offsetWidth; 
		return w; 
	}

	this.SetWidth = function(obj,w) 
	{ 
		if (this.IsNetscape) 
		{
			if (obj.clip) obj.clip.right=w;
		}else if (this.IsOpera)obj.style.pixelWidth=w;
		else obj.style.width=w; 
	}

	this.GetX = function(obj) 
	{ 
		var x=(this.IsNetscape)? obj.left:(this.IsOpera)? obj.style.pixelLeft:obj.offsetLeft; 
		return x;
	}

	this.SetX = function(obj, x) 
	{ 
		
		(this.IsNetscape)? obj.left=x:(this.IsOpera)? obj.style.pixelLeft=x:obj.style.left=x; 
	}

	this.GetY = function(obj) 
	{  
		var y=(this.IsNetscape)? obj.top:(this.IsOpera)? obj.style.pixelTop:obj.offsetTop; 
		return y;
	}

	this.SetY = function(obj, y) 
	{ 
		(this.IsIE||this.IsDom)? obj.style.top=y:(this.IsNetscape)? obj.top=y:obj.style.pixelTop=y; 
	}

	this.GetPageX = function(obj) 
	{ 
		if (this.IsNetscape) 
		{ 
			var x=(obj.pageX)? obj.pageX:obj.x; return x; 
		} else if (this.IsOpera) 
		{  
			var x=0; 
			while(eval(obj)) 
			{ 
				x+=obj.style.pixelLeft; 
				obj=obj.offsetParent; 
			} 
			return x; 
		} 
		else 
		{ 
			var x=0; 
			while(eval(obj)) 
			{ 
				x+=obj.offsetLeft; 
				obj=obj.offsetParent; 
			} 
			return x; 
		} 
	}

	this.GetPageY = function(obj) 
	{ 
		if (this.IsNetscape) 
		{ 
			var y=(obj.pageY)? obj.pageY:obj.y; 
			return y; 
		} 
		else if (this.IsOpera) 
		{  
			var y=0; 
			while(eval(obj)) 
			{ 
				y+=obj.style.pixelTop; 
				obj=obj.offsetParent; 
			} 
			return y; 
		} 
		else 
		{ 
			var y=0; 
			while(eval(obj)) 
			{ 
				y+=obj.offsetTop; 
				obj=obj.offsetParent; 
			} 
			return y; 
		} 
	}

	this.SetPos = function(obj,x,y) 
	{ 
		this.SetX(obj,parseInt(x));
		this.SetY(obj,parseInt(y)); 
	}
	
	this.SetPosRelative = function(obj,x,y) 
	{ 
		this.SetX(obj,parseInt(this.GetPageX(obj))+parseInt(x));
		this.SetY(obj,parseInt(this.GetPageY(obj))+parseInt(y)); 
	}

	this.SetZValue = function(obj,z) 
	{ 
		if (this.IsNetscape)obj.zIndex=z;
		else obj.style.zIndex=z; 
	}
	

	this.ShowObject = function(obj,disp) 
	{ 
		(this.IsNetscape)? '':(!disp)? obj.style.display="inline":obj.style.display=disp;
		(this.IsNetscape)? obj.visibility='show':obj.style.visibility='visible';  
	}

	this.HideObject = function(obj,disp) 
	{ 
		(this.IsNetscape)? '':(arguments.length!=2)? obj.style.display="none":obj.style.display=disp;
		(this.IsNetscape)? obj.visibility='hide':obj.style.visibility='hidden';  
	}

	this.SetStyle = function(obj,s,v) 
	{ 
		if (this.IsIE5||this.IsDom) eval("obj.style."+s+" = '" + v +"'"); 
	}

	this.GetStyle = function(obj,s) 
	{ 
		if (this.IsIE5||this.IsDom) return eval("obj.style."+s); 
	}

	this.AddEventListener = function (o,e,f,c)
	{ 
		if(o.addEventListener)o.addEventListener(e,f,c);
		else if(o.attachEvent)o.attachEvent("on"+e,f);else eval("o.on"+e+"="+f)
	}
	
	this.AddEventListener = function(obj,eventName,callbackFunction,flag)
	{ 
		
		if (obj.addEventListener) 
		{
			obj.addEventListener(eventName,callbackFunction,flag);
		}
		else if (obj.attachEvent) 
		{
			obj.attachEvent("on"+eventName,callbackFunction);
		}
		else 
		{
			eval("obj.on"+eventName+"="+callbackFunction);
		}
	}
	
	this.WriteHTML = function(obj,html) 
	{
		
		if (this.IsNetscape)
		{
			var doc=obj.document;
			doc.write(html);
			doc.close();
			return false;
		}
		if (obj.innerHTML) obj.innerHTML=html; 
	}

	// region WBC 494
	this.SetXClientOverflowSafe=function(obj, x)
	{
		var objW = IGB.GetWidth(obj);
		var objR = objW + x;
		var clientW = IGB.GetClientWidth();
		if ( (clientW - objR ) > (x-objW-5) )
		//if (objR <= clientW || (x-objW-5<0) )
			this.SetX(obj, x);
		else
			this.SetXScrollContainerSafe(obj, x - objW - 5);
//			this.SetXScrollContainerSafe(obj, x - (objR - clientW) - 25);
	}
	this.SetXOverflowSafe=function(obj, x, container)
	{
		var objR = IGB.GetWidth(obj) + x;
		var containerW = IGB.GetWidth(container);
		if (objR <= containerW)
			this.SetXScrollContainerSafe(obj, x);
		else
			this.SetXScrollContainerSafe(obj, x - (objR - containerW));
	}
	
	this.SetYClientOverflowSafe=function(obj, y)
	{
		var objH = IGB.GetHeight(obj);
		var objT = objH + y + 23; // plus 23, as 20 offset was already added to y
		var clientH = IGB.GetClientHeight();
		if ( (clientH - objT ) > (y-objH-25) )
		//if (objT <= clientH)
			this.SetYScrollContainerSafe(obj, y);
		else
			this.SetYScrollContainerSafe(obj, y - objH - 25 );
//			this.SetYScrollContainerSafe(obj, y - (objT - clientH) );
	}
	this.SetYOverflowSafe=function(obj, y, container)
	{
		var objT = IGB.GetHeight(obj) + y;
		var containerH = IGB.GetHeight(container);
		if (objT <= containerH)
			this.SetYScrollContainerSafe(obj, y);
		else
			this.SetYScrollContainerSafe(obj, y - (objT - containerH));
	}

	this.GetClientWidth = function()
	{
		var w=(this.IsIE)? window.document.body.clientWidth:window.innerWidth;
		return w;
	}
	this.GetClientHeight = function()
	{
		var w=(this.IsIE)? window.document.body.clientHeight:window.innerHeight;
		return w;
	}
	// end region
	// region WBC202
	
	this.SetXScrollContainerSafe = function(obj, x)
	{
		var hSC = this.GetHScrolledContainer(obj);
		if (hSC != null)
			this.SetXScrollContainerAdjusted(obj, x, hSC);
		else
			this.SetX(obj, x);
	}
	this.SetYScrollContainerSafe = function(obj, y)
	{
		var vSC = this.GetVScrolledContainer(obj);
		if (vSC != null)
			this.SetYScrollContainerAdjusted(obj, y, vSC);
		else
			this.SetY(obj, y);
	}
	this.GetVScrolledContainer = function(obj)
	{
		if (obj.scrollTop > 0 && obj.tagName != 'BODY')
			return obj;
		else if (obj.offsetParent != null)
			return this.GetVScrolledContainer(obj.offsetParent);
		else
			return null;
	}
	this.GetHScrolledContainer = function(obj)
	{
		if (obj.scrollLeft > 0 && obj.tagName != 'BODY')
			return obj;
		else if (obj.offsetParent != null)
			return this.GetHScrolledContainer(obj.offsetParent);
		else
			return null;
	}
	this.SetXScrollContainerAdjusted = function(obj, x, container)
	{
		this.SetX(obj, x + container.scrollLeft);
	}
	this.SetYScrollContainerAdjusted = function(obj, y, container)
	{
		this.SetY(obj, y + container.scrollTop);
	}
	// endregion
	// where = beforeBegin,afterBegin,beforeEnd,afterEnd

	this.InsertHTML = function(obj,html,where) 
	{
		
		if (this.IsOpera) return;
		if (obj.insertAdjacentHTML) 
		{ 
			obj.insertAdjacentHTML(where,html);
			return;
		}
		if (this.IsNetscape) 
		{
			this.WriteHTML(obj,html);
			return;
		}
		
		// Mozilla
		var ref = obj.ownerDocument.createRange();
		ref.setStartBefore(obj);
		
		var fragment = ref.createContextualFragment(html);
		
		this.DOMInsertObj(obj,where,fragment);	
	}

	this.DOMInsertObj  = function(obj, where, node) 
	{
		
		switch (where)
		{
			case 'beforeBegin':
				obj.parentNode.insertBefore(node,obj)
				break;
			case 'afterBegin':
				obj.insertBefore(node,obj.firstChild);
				break;
			case 'beforeEnd':
				obj.appendChild(node);
				break;
			case 'afterEnd':
				if (obj.nextSibling)
				{
					obj.parentNode.insertBefore(node,obj.nextSibling);
				} 
				else 
				{
					obj.parentNode.appendChild(node)
				}
				break;
		}
	}

	// Common Global Infrastructure related begin
	
	ID2OBJECT["IGB"] = this;

	this.Listener	  = new Array();
	this.AddListener  = function(type, function_ref) 
	{
		this.Listener[type] = function_ref;
	}
	// Common Global Infrastructure related end

	this.CurrentX = 0;
	this.CurrentY = 0;

	// Common golbal infrastructure related. -WORK IN PROGRESS.
	this.GlobalHandleMouseMove = function(evt)
	{
		if (this.IsNetscape4Plus)
		{
			this.CurrentX=evt.pageX;
			this.CurrentY=evt.pageY;
		}
		else if (this.IsNetscape6)
		{
			this.CurrentX=evt.clientX;
			this.CurrentY=evt.clientY;
		}
		else if (this.IsIE5)
		{
			this.CurrentX=event.clientX;
			this.CurrentY=event.clientY;
		}

		IGProcessEventsObjects("onmousemove", this);

		return false;
	}

	// Gets the function name from the function reference.
	this.FunctionName = function(f)
	{

		if (f==null)
		{
			return "annonymous";
		}
		var s=f.toString().match(/function (\w*)/)[1];
		if((s==null)|| (s.length==0)) return "annonymous";
		return s;
	}

	// DecodeArguments, spilt and url-decode it.
	// string is split at "&" and url decoded, all the items are put in an array.
	this.DecodeArguments=function(inputString) 
	{
		var splitArray = inputString.split('&');
		for (i=0; i<splitArray.length; i++) splitArray[i] = unescape(splitArray[i]);
		return splitArray;
	}
}

// Util objects
function IGRectangle(x, y, width, height)
{
	this.X=x;
	this.Y=y;
	this.Width=width;
	this.Height=height;

	this.Inside = function(x,y)
	{
		return (x >=this.X && y >= this.Y && x <=(this.X+this.Width) && y <=(this.Y+this.Height));
	}
}

function IGPoint(x, y) 
{
	this.X = x;
	this.Y = y;
} 

// Common Global Infrastructure begin
// TODO: this requires work. Either the event handling can be replaced and or modified to suit better
// Please use "apply" and Delegate Object instances or reference to handle this -KV.

// Declare global variable
var IGB = new IGBrowser();

function IGProcessEventsObjects(type, sender_object)
{
	if (eval("sender_object.Listener")) 
	{
		var function_ref = sender_object.Listener[type];
		if (function_ref != null) 
		{
			function_ref(type, null, sender_object);
		}
	}
}

function IGProcessEvents(type, sender_element)
{
	var sender_object = ID2OBJECT[sender_element.id];
	IGBubbleEvent(type, sender_element, sender_object);

	if (eval("sender_object.Listener")) 
	{
		var function_ref = sender_object.Listener[type];
		if (function_ref != null) 
		{
			function_ref(type, sender_element, sender_object);
		}
	}
}
function IGBubbleEvent(type, sender_element, sender_object)
{
	if (eval("sender_object."+type)) 
	{
		if (eval("sender_object."+type+"(sender_element, sender_object)"))
		{
			var parent_ref = sender_object.Parent;
			if (parent_ref != null) 
			{
				IGBubbleEvent(type, sender_element, parent_ref);
			}
		}
	}
}

// Common Global Infrastructure related end

// Repeating infrastructure related. This used to implement repeating functionality in controls such as scrollbar and fader.
// Please note that this repeating logic can only handle one repeating at a time. Call to Cancel repeating will cancel any
// other repeating in progress. This logic can be extended to handle more than one repeating but since chart uses are limited
// not implemented. -KV
var RepeatingDelegate=null;
var DelegateParameter=null;
var DelegateeObject=null;
var TimerId= null;

// Linear decay repeating
//    ^
//    |   unit: msec
// 70 +
//    | \
//    |   \
//  5 +     \____
//    +-----+---->
//    0     552 

function GetDelay(nextTimeOut)
{
	if (nextTimeOut == -1)
	{
		nextTimeOut = 70;
	}
	else
	{
		nextTimeOut-=5;
		if (nextTimeOut<5) nextTimeOut = 5;
	}
	return nextTimeOut;
}

// Repeating handler, called upon each timeout
function RepeatingHandler(nextTimeOut) 
{
	nextTimeOut = GetDelay(nextTimeOut);

	TimerId=setTimeout("RepeatingHandler("+nextTimeOut+")", nextTimeOut);

	if (RepeatingDelegate!=null) 
	{
		RepeatingDelegate.apply(DelegateeObject, DelegateParameter);
	}
}

// setup start and stop repeating
function Repeating(trueToStartfalseToEnd, delegate, parameters, ThisObject) 
{
	if (trueToStartfalseToEnd == true) 
	{
		RepeatingDelegate = delegate;
		DelegateParameter = parameters;
		DelegateeObject   = ThisObject;
		RepeatingHandler(-1); 
	}
	else 
	{
		if (TimerId)
		{
			clearTimeout(TimerId);
			TimerId = null;
		}

		RepeatingDelegate = null;
		DelegateParameter = null;
	}
}

// Fader class is used to create fading effect on given element. 
// This animates the Opacity Style in given interval.
// Very Generic Object can be used on pretty much any element ref.
function Fader()
{
	// Common Global Infrastructure related begin
	ID2OBJECT[this.ID]= this; // save id's ref.
	this.Parent		  = null; // for parent child relationship
	this.Listener	  = new Array();
	this.AddListener  = function(type, function_ref) 
	{
		// nothing supported yet
	}
	// Common Global Infrastructure related end

	// Private variables
	this.FaderOpacity = 0; 

	this.FaderStep=function(id_ref, min, max, delta)
	{
		if( (this.FaderOpacity<=max) && (this.FaderOpacity>=min) )
		{
			this.FaderOpacity += delta;
			if (IGB.IsIE4 || IGB.IsIE5) id_ref.style.filter="alpha(opacity="+this.FaderOpacity+")";
			if (IGB.IsNetscape6) id_ref.style.MozOpacity=this.FaderOpacity/100;
		} 
		else
		{
			Repeating(false);
		}
	}

	// Starts the fader
	// animates the opacity of an element from [min..max] with delta.
	// make sure delta is not equal to zero other wise it will never stop repeating

	this.Start=function(id_ref, min, max, delta)
	{
		this.FaderOpacity = min;
		Repeating(true, this.FaderStep, [id_ref, min, max, delta], this);
	}

	this.End=function()
	{
		Repeating(false);
	}
}


/// Bounce event to right object.
/// event - object
/// id - string
/// func_name - string
function Bounce(evt, id, func_name, paramArray) 
{
	var this_ref = ID2OBJECT[id];  
	var fn = func_name;
	if (this_ref)
	{
		if (fn)
		{
			fn = func_name;
		}
		else
		{
			fn = "on"+evt.type;
		}

		eval("this_ref."+fn+"(evt, id, paramArray)");
	}
}

