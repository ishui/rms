var g_undefined;
var t;
var UNINITIALIZED = 1;
var INITIALIZING = 2;
var INITIALIZED = 3;
var CREATING = 4;
var CREATED = 5;
var OBTAINING = 6;
var OBTAINABLE = 7;
var LOADING = 8;
var LOADED = 9;
var STOP = 1;
var PLAY = 2;
var PAUSE = 3;
function IsUndefined(p)
{
return(p === g_undefined);
}
function IsDefined(p)
{
return(p !== g_undefined);
}
function IsDefinedAndNotNull(p)
{
return((p !== g_undefined) && (p !== null));
}
function AllWithValues()
{
return(CheckValues(false, AllWithValues.Params_()));
}
function AnyWithValues()
{
return(CheckValues(true, AnyWithValues.Params_()));
}
function CheckValues(p_fAny)
{
var params = CheckValues.Params_(1);
var f;
for (var i=params.iStart; i < params.length; i++)
{
f = IsDefinedAndNotNull(params[i]);
if (f.Value_(p_fAny))
{
return(p_fAny);
}
}
return(!p_fAny);
}
function Identity(p)
{
return(p);
}
function Dispose(o)
{
if (o)
{
if (o.Dispose)
{
o.Dispose();
}
var p;
for (p in o)
{
delete(o[p]);
}
}
}
Object.prototype.Method_ = function(p_strMethodName)
{
if (!this.methods_)
{
this.methods_ = new Object();
}
if (!this.methods_[p_strMethodName])
{
this.methods_[p_strMethodName] = this.CreateInstanceMethod_(p_strMethodName);
}
return(this.methods_[p_strMethodName]);
}
Object.prototype.CreateInstanceMethod_ = function(p_strMethodName)
{
var o = this;
var m = p_strMethodName;
var f = o[m];
var fnc = function()
{
if (!f)
{
return;
}
if (f.apply)
{
return(f.apply(o, fnc.ArgumentsArray_()));
}
else
{
var a = arguments;
switch(a.length)
{
case 0:
return(o[m]());
case 1:
return(o[m](a[0]));
case 2:
return(o[m](a[0],a[1]));
case 3:
return(o[m](a[0],a[1],a[2]));
case 4:
return(o[m](a[0],a[1],a[2],a[3]));
case 5:
return(o[m](a[0],a[1],a[2],a[3],a[4]));
default:
var strArgs = "";
for (var i=0; i < a.length; i++)
{
if (i > 0)
{
strArgs += ",";
}
strArgs += "a[" + i + "]";
}
var ret;
eval("ret = o." + m + "(" + strArgs + ")");
return(ret);
}
}
};
return(fnc);
}
Function.prototype.Inherits_ = function(p_fncBaseClass)
{
var v;
for (var i=0; i < arguments.length; i++)
{
for (p in arguments[i].prototype)
{
v = arguments[i].prototype[p];
this.prototype[p] = v;
this.prototype["base_" + p] = v;
}
this.prototype["_constructor_" + i] = arguments[i];
}
}
Function.prototype.ArgumentsArray_ = function(p_iStartIndex, p_iEndIndex, fAddArrayContents)
{
p_iStartIndex = IsUndefined(p_iStartIndex) ? 0 : p_iStartIndex;
p_iEndIndex = IsUndefined(p_iEndIndex) ? this.arguments.length : p_iEndIndex;
var iLength = Math.max(0, p_iEndIndex - p_iStartIndex);
var a = new Array(fAddArrayContents ? 0 : iLength);
for (var i=0; i < iLength; i++)
{
if (fAddArrayContents)
{
a = a.concat(this.arguments[i+p_iStartIndex]);
}
else
{
a[i] = this.arguments[i+p_iStartIndex];
}
}
return(a);
}
Function.prototype.ArgumentsAsArray_ = function(p_iStartIndex, p_iEndIndex)
{
return(this.ArgumentsArray_(p_iStartIndex, p_iEndIndex, true));
}
Function.prototype.Params_ = function(p_iStartIndex)
{
var i = IsUndefined(p_iStartIndex) ? 0 : p_iStartIndex;
var a = this.arguments;
var c = Math.max(0, a.length - i);
if (a.length >= i+1)
{
if (a[i])
{
if (a[i].fParamIndicator)
{
return(a[i]);
}
if (a[i].constructor == Array)
{
a = a[i];
i = 0;
c = a.length;
}
}
}
a.iStart = i;
a.iCount = c;
a.fParamIndicator = true;
return(a);
}
Function.ParamsToArray = function(p_params)
{
if (p_params.constructor == Array)
{
return(p_params);
}
else
{
var a = new Array(p_params.iCount);
for (var i=0; i < p_params.iCount; i++)
{
a[i] = p_params[i+p_params.iStart];
}
return(a);
}
}
Function.Invoke_ = function(p_fnc)
{
if (p_fnc)
{
return(p_fnc());
}
}
Function.BuildCallAsString = function(p_strMethodName)
{
var str = p_strMethodName + "(";
for (var i=1; i < arguments.length; i++)
{
if (i > 1)
{
str += ",";
}
str += IsDefined(arguments[i]) ? arguments[i] : "g_undefined";
}
str += ")";
return(str);
}
Boolean.prototype.Value_ = function(p_f)
{
return(p_f ? this.valueOf() : !this.valueOf());
}
Number.Compare = function(p_1, p_2)
{
return((p_1 < p_2) ? -1 : ((p_1 > p_2) ? 1 : 0));
}
Number.ConvertToInt = function(p_str)
{
var i = parseInt(p_str);
return(isNaN(i) ? null : i);
}
Array.prototype.Insert_ = function(p_iIndex)
{
var a = Array.prototype.Insert_.Params_(1);
var iCount = a.iCount;
if (iCount == 0)
{
return;
}
this.length += iCount;
for (var i = this.length-iCount-1; i >= p_iIndex; i--)
{
this[i+iCount] = this[i];
}
for (var j=0; j < iCount; j++)
{
this[j+p_iIndex] = a[j+a.iStart];
}
}
Array.prototype.Delete_ = function(p_iStartIndex, p_iEndIndex)
{
var a = Array.prototype.Delete_.Params_(2);
if (IsUndefined(p_iEndIndex))
{
p_iEndIndex = p_iStartIndex + 1;
}
var aDeleted = this.slice(p_iStartIndex, p_iEndIndex);
var iRemovalCount = aDeleted.length;
if (iRemovalCount == a.iCount)
{
for (var i=0; i < a.iCount; i++)
{
this[i+p_iStartIndex] = a[i+a.iStart];
}
}
else
{
for (var i=p_iStartIndex; (i+iRemovalCount) < this.length; i++)
{
this[i] = this[i+iRemovalCount];
}
this.length = this.length - iRemovalCount;
}
return(aDeleted);
}
Array.prototype.IndexOf_ = function(p_value)
{
for (var i=0; i < this.length; i++)
{
if (this[i] === p_value)
{
return(i);
}
}
return(-1);
}
Array.prototype.LastIndexOf_ = function(p_value)
{
for (var i=this.length-1; i >= 0; i--)
{
if (this[i] === p_value)
{
return(i);
}
}
return(-1);
}
Array.prototype.Contains_ = function(p_value)
{
return(this.IndexOf_(p_value) != -1);
}
function Xml()
{
}
Xml.GetNodeText = function(p_node)
{
return(p_node ? p_node.text : "");
}
Xml.IntAttribute = function(p_node, p_strName)
{
var value = p_node.getAttribute(p_strName);
return(IsDefinedAndNotNull(value) ? parseInt(value) : null);
}
Xml.LoadSrc = function(p_xmlElement, p_strSrc, p_fncLoadCallback)
{
p_xmlElement.fncLoadCallback = p_fncLoadCallback;
p_xmlElement.ondatasetcomplete = Xml.LoadHandler;
p_xmlElement.src = p_strSrc;
}
Xml.LoadHandler = function()
{
var xmlElement = window.event.srcElement;
if (xmlElement.fncLoadCallback)
{
xmlElement.fncLoadCallback(xmlElement.XMLDocument);
}
}
function Properties()
{
}
Properties.Set = function(p_obj)
{
var a = Properties.Set.Params_(1);
for (var i=a.iStart; i < a.length; i+=2)
{
p_obj[a[i]] = a[i+1];
}
}
Properties.Delete = function(p_obj)
{
var a = Properties.Delete.Params_(1);
for (var i=a.iStart; i < a.length; i++)
{
delete (p_obj[a[i]]);
}
}
Properties.Copy = function(p_fromObj, p_toObj)
{
var a = Properties.Copy.Params_(2);
for (var i=a.iStart; i < a.length; i++)
{
p_toObj[a[i]] = p_fromObj[a[i]];
}
}
function Data()
{
}
Data.Rectangle = function(p_iTop, p_iRight, p_iBottom, p_iLeft)
{
if (arguments.length == 3)
{
this.iTop = arguments[0].y;
this.iLeft = arguments[0].x;
this.iRight = this.iLeft + arguments[1] - 1;
this.iBottom = this.iTop + arguments[2] - 1;
}
else
{
this.iTop = p_iTop;
this.iRight = p_iRight;
this.iBottom = p_iBottom;
this.iLeft = p_iLeft;
}
}
Data.Rectangle.prototype.ContainsPoint = function(p_point)
{
return( Math2.IsBetween(this.iLeft, p_point.x, this.iRight) &&
Math2.IsBetween(this.iTop, p_point.y, this.iBottom));
}
Data.Point = function(p_x, p_y)
{
this.x = p_x;
this.y = p_y;
}
Data.Point.prototype.InRectangle = function(p_rect)
{
return(p_rect.ContainsPoint(this));
}
Data.Size = function(p_width, p_height)
{
this.width = p_width;
this.height = p_height;
}
Data.Size.prototype.Resize = Data.Size;
Data.Size.Empty = new Data.Size(0, 0);
Data.Size.prototype.Scale = function(p_flRatio, p_fRound)
{
var f = p_fRound ? Math.round : Identity;
this.width = f(this.width * p_flRatio);
this.height = f(this.height * p_flRatio);
}
Data.Size.prototype.GetContainedSize = function(p_MaxSize)
{
var size = new Data.Size(this.width, this.height);
size.Scale(this.GetScaledRatio(p_MaxSize), true);
return(size);
}
Data.Size.prototype.GetScaledRatio = function(p_MaxSize)
{
return(Math.min(1, Math.min((p_MaxSize.width / this.width), (p_MaxSize.height / this.height))));
}
function DomElement()
{
}
DomElement.AppendChildren = function(p_element)
{
for (var i=1; i < arguments.length; i++)
{
p_element.appendChild(arguments[i]);
}
}
DomElement.CreateElement = function(p_strElementName)
{
var e = window.document.createElement(p_strElementName);
Properties.Set(e.style, DomElement.CreateElement.ArgumentsArray_(1));
return(e);
}
DomElement.SetStyles = function(p_Element)
{
Properties.Set(p_Element.style, DomElement.SetStyles.ArgumentsArray_(1));
}
DomElement.CreateElementWithFilter = function(p_strElementName, p_strOldFilter, p_strNewFilter)
{
var oElement;
var strAppVersion = window.clientInformation.appVersion + "";
if (strAppVersion.indexOf("MSIE 5.0") > 0)
{
oElement = document.createElement("<" + p_strElementName + " style='filter:" + p_strOldFilter + "'>");
}
else
{
oElement = document.createElement(p_strElementName);
oElement.style.filter = p_strNewFilter;
}
return(oElement);
}
DomElement.Clone = function(p_element)
{
return(p_element == null ? null : p_element.cloneNode());
}
DomElement.FindAncestorWithProperty = function(p_element, p_strPropName, p_strPropValue)
{
while (true)
{
if (!p_element)
{
return(null);
}
else if (DomElement.ElementHasProperty(p_element, p_strPropName, p_strPropValue))
{
return(p_element);
}
else
{
p_element = p_element.parentNode;
}
}
}
DomElement.ElementHasProperty = function(p_element, p_strPropName, p_strpropValue)
{
if (IsUndefined(p_strpropValue))
{
return(p_element[p_strPropName] && true);
}
else
{
return(p_element[p_strPropName] == p_strpropValue);
}
}
function DomPositioning()
{
}
DomPositioning.StripPixelSuffix = function(p_strNumber)
{
var iIndex = p_strNumber.toLowerCase().indexOf("px");
if (iIndex >= 0)
{
p_strNumber = p_strNumber.substring(0, iIndex);
}
return(parseInt(p_strNumber));
}
DomPositioning.FindOffsetInParentElement = function(p_element, p_ancestorElement)
{
var iTop = 0;
var iLeft = 0;
var strLeft = "";
while ((p_element != p_ancestorElement) && (p_ancestorElement.contains(p_element)))
{
iTop += p_element.offsetTop;
iLeft += p_element.offsetLeft;
strLeft += iLeft + " ";
p_element = p_element.offsetParent;
}
return(new Data.Point(iLeft, iTop));
}
DomPositioning.FindOffsetInBody = function(p_element)
{
return(DomPositioning.FindOffsetInParentElement(p_element, document.body));
}
DomPositioning.VerticallyAlign = function(p_element, p_iElementHeight, p_iRegionHeight, p_iOffset)
{
p_element.style.top = Math.max(0, Math.floor((p_iRegionHeight - p_iElementHeight) / 2) + p_iOffset);
}
DomPositioning.HorizontallyAlign = function(p_element, p_iElementWidth, p_iRegionWidth, p_iOffset)
{
p_element.style.left = Math.max(0, Math.floor((p_iRegionWidth - p_iElementWidth) / 2) + p_iOffset);
}
DomPositioning.Sides = function(p_iTop, p_iRight, p_iBottom, p_iLeft)
{
this.iTop = p_iTop;
this.iRight = p_iRight;
this.iBottom = p_iBottom;
this.iLeft = p_iLeft;
}
function DomEvents()
{
}
DomEvents.Hookup = function(p_Element, p_fAttached)
{
var args = arguments;
for (var i=2; i < args.length; i += 2)
{
if (p_fAttached)
{
p_Element.attachEvent(args[i], args[i+1]);
}
else
{
p_Element.detachEvent(args[i], args[i+1]);
}
}
}
DomEvents.GetPointFromBody = function()
{
var x = window.event.clientX + document.body.scrollLeft;
var y = window.event.clientY + document.body.scrollTop;
return(new Data.Point(x, y));
}
DomEvents.HookupMouseTracking = function(p_Element, p_fAttached)
{
}
function DomStyles()
{
}
DomStyles.GetClipRectangle = function(p_element)
{
var strClip = p_element.style.clip
if (strClip)
{
var astr = (strClip.substring(strClip.indexOf("(")+1, strClip.indexOf(")"))).split(" ");
var oRect = new Data.Rectangle(
parseInt(astr[0]),
parseInt(astr[1]),
parseInt(astr[2]),
parseInt(astr[3])
);
return(oRect);
}
return(null);
}
DomStyles.SetClipRectangle = function(p_element, p_oClip)
{
if (p_element && p_oClip)
{
var strClip = "rect(" + p_oClip.iTop + " " + p_oClip.iRight + " " + p_oClip.iBottom + " " + p_oClip.iLeft + ")";
p_element.style.clip = strClip;
p_element.oClip = p_oClip;
}
}
function DomImage()
{
}
DomImage.Standardize = function(p_img)
{
if (!p_img)
{
return(null);
}
if (!p_img.unclippedSize)
{
p_img.unclippedSize = new Data.Size(p_img.width, p_img.height);
}
if (p_img.style.clip)
{
if (!p_img.oClip)
{
p_img.oClip = DomStyles.GetClipRectangle(p_img);
}
p_img.iWidth = p_img.oClip.iRight - p_img.oClip.iLeft;
p_img.iHeight = p_img.oClip.iBottom - p_img.oClip.iTop;
}
else
{
p_img.iWidth = p_img.width;
p_img.iHeight = p_img.height;
p_img.oClip = new Data.Rectangle(0, p_img.iWidth, p_img.iHeight, 0);
}
p_img.size = new Data.Size(p_img.iWidth, p_img.iHeight);
return(p_img);
}
DomImage.Scale = function(p_img, p_flRatio)
{
if (p_img.style.clip)
{
var iTop = Math.round(p_flRatio * p_img.oClip.iTop);
var iRight = Math.round(p_flRatio * p_img.oClip.iRight);
var iBottom = Math.round(p_flRatio * p_img.oClip.iBottom);
var iLeft = Math.round(p_flRatio * p_img.oClip.iLeft);
p_img.style.clip = "rect(" + iTop + " " + iRight + " " + iBottom + " " + iLeft + ")";
p_img.oClip = null;
}
p_img.width = Math.round(p_flRatio * p_img.unclippedSize.width);
p_img.height = Math.round(p_flRatio * p_img.unclippedSize.height);
return(DomImage.Standardize(p_img));
}
function DomWindow()
{
}
DomWindow.prototype.CenterWindow = function(p_iWidth, p_iHeight)
{
var p = new Data.Point();
var iUsedWidth = screen.width - screen.availWidth;
var iModifiedAvailWidth = screen.availWidth - iUsedWidth;
p.x = Math.floor((iModifiedAvailWidth - p_iWidth) / 2) + iUsedWidth;
p.y = Math.floor((screen.availHeight - p_iHeight) / 2);
return(p);
}
String.prototype.Encode_ = function()
{
return(this);
}
function Math2()
{
}
Math2.MinMax = function(p_min, p_value, p_max)
{
return(Math.min(p_max, Math.max(p_min, p_value)));
}
Math2.IsBetween = function(p_min, p_value, p_max, p_fExclusive)
{
if (p_fExclusive)
{
return((p_value > p_min) && (p_value < p_max));
}
else
{
return((p_value >= p_min) && (p_value <= p_max));
}
}
Math2.RoundFunction = function(p_fnc)
{
var fncRound = function(p_x)
{
return(Math.round(p_fnc(p_x)));
}
return(fncRound);
}
Math2.MinMaxFunction = function(p_fnc, p_min, p_max)
{
var fncMinMax = function(p_x)
{
return(Math2.MinMax(p_min, p_fnc(p_x), p_max));
}
return(fncMinMax);
}
Math2.LinearFunction = function(p_x1, p_y1, p_x2, p_y2)
{
var a = p_y1 - p_y2;
var b = p_x2 - p_x1;
var c = (p_y1 * b) - (p_x1 * (0-a));
var fncOfX = function(p_x)
{
return((b == 0) ? Number.NaN : ((c - (a * p_x)) / b))
}
var fncOfY = function(p_y)
{
return((b == 0) ? p_x1 : ((a == 0) ? Number.NaN : ((c - (b * p_y)) / a)));
}
fncOfX.fncInverse = fncOfY;
fncOfY.fncInverse = fncOfX;
return(fncOfX);
}
Math2.LogarithmicPercentFunction = function(p_min, p_max)
{
var offset = 1 - p_min;
var c = p_max - p_min;
var fncOfX = function(p_percent)
{
return(Math.pow(c, p_percent) - offset);
}
var fncOfY = function(p_value)
{
return(Math.log(p_value + offset) / Math.log(c));
}
fncOfX.fncInverse = fncOfY;
fncOfY.fncInverse = fncOfX;
return(fncOfX);
}
function Browser()
{
}
Browser.IsIE5 = function()
{
var strAppVersion = window.clientInformation.appVersion + "";
return(strAppVersion.indexOf("MSIE 5.0") > 0);
}
Browser.strMouseOverEvent = Browser.IsIE5() ? "onmouseover" : "onmouseenter";
Browser.strMouseOutEvent = Browser.IsIE5() ? "onmouseout" : "onmouseleave";
Browser.IsMsnClient = function()
{
return(Browser.UserAgentVersion("msn") != -1);
}
Browser.UserAgentVersion = function(p_strKey)
{
p_strKey = p_strKey.toLowerCase();
var strUserAgent = window.navigator.userAgent.toLowerCase();
if (strUserAgent.indexOf(p_strKey) != -1)
{
var flVersion = 0;
var re = new RegExp(p_strKey + "\\s*(\\d+\\.?\\d*)", "ig")
var astrMatches = strUserAgent.match(re)
if (astrMatches)
{
for (var i=0; i < astrMatches.length; i++)
{
flVersion = Math.max(flVersion, parseFloat(astrMatches[i].replace(re, "$1")));
}
}
return(flVersion);
}
return(-1);
}
Browser.IsMsn9Plus = function()
{
return(Browser.UserAgentVersion("msn") >= 9);
}
function Collection(
p_fncCollectionChanged
)
{
this.array = new Array();
this.length = 0;
this.fncCollectionChanged = p_fncCollectionChanged;
}
Collection.prototype.Dispose = function()
{
for (var i=0; i < this.array.length; i++)
{
Dispose(this.array[i]);
delete(this.array[i]);
}
}
Collection.prototype.Add = function()
{
this.Insert(this.length, Collection.prototype.Add.Params_());
}
Collection.prototype.Insert = function(p_iIndex)
{
var params = Collection.prototype.Insert.Params_(1);
this.InitObjects(params);
this.array.Insert_(p_iIndex, params);
this.TagObjects(p_iIndex);
this.length = this.array.length;
if (this.fncCollectionChanged)
{
var aInserted = Function.ParamsToArray(params);
aInserted.iStartIndex = p_iIndex;
this.fncCollectionChanged(aInserted, null);
}
}
Collection.prototype.Delete = function(p_iStartIndex, p_iEndIndex)
{
var params = Collection.prototype.Delete.Params_(2);
if (params.iCount > 0)
{
this.InitObjects(params);
}
else
{
params = null;
}
if (IsUndefined(p_iEndIndex))
{
p_iEndIndex = p_iStartIndex + 1;
}
var aValuesRemoved = this.array.Delete_(p_iStartIndex, p_iEndIndex, params);
aValuesRemoved.iStartIndex = p_iStartIndex;
var aInserted = null;
if (params)
{
aInserted = Function.ParamsToArray(params);
aInserted.iStartIndex = p_iStartIndex;
this.TagObjects(p_iStartIndex, p_iEndIndex);
}
else
{
this.length = this.array.length;
}
if (this.fncCollectionChanged)
{
this.fncCollectionChanged(aInserted, aValuesRemoved);
}
this.UntagObjects(aValuesRemoved);
}
Collection.prototype.Set = function(p_iIndex)
{
var params = Collection.prototype.Set.Params_(1);
this.Delete(p_iIndex, p_iIndex + params.iCount, params);
}
Collection.prototype.Get = function(p_iIndex)
{
return(this.array[p_iIndex]);
}
Collection.prototype.GetSlice = function(p_iStartIndex, p_iEndIndex)
{
if (IsUndefined(p_iStartIndex))
{
p_iStartIndex = 0;
}
if (IsUndefined(p_iEndIndex))
{
p_iEndIndex = this.array.length;
}
return(this.array.slice(p_iStartIndex, p_iEndIndex));
}
Collection.prototype.IndexOf = function(p_Value)
{
return(p_Value.iIndex_);
}
Collection.IndexOf = function(p_Value)
{
return(p_Value === null ? -1 : p_Value.iIndex_);
}
Collection.prototype.InitObjects = function(p_params)
{
for (var i=p_params.iStart; i < p_params.length; i++)
{
p_params[i] = new Object(p_params[i]);
}
}
Collection.Clean = function(p_item)
{
var i = p_item.iIndex_;
for (var p in p_item)
{
delete(p_item[p]);
}
p_item.iIndex_ = i;
}
Collection.prototype.UntagObjects = function(p_aObjects)
{
for (var i=0; i < p_aObjects.length; i++)
{
delete(p_aObjects[i].iIndex_);
delete(p_aObjects[i].oCollection_);
}
}
Collection.prototype.TagObjects = function(p_iStartIndex, p_iEndIndex)
{
if (IsUndefined(p_iEndIndex))
{
p_iEndIndex = this.array.length;
}
for (var i=p_iStartIndex; i < p_iEndIndex; i++)
{
this.array[i].iIndex_ = i;
this.array[i].oCollection_ = this;
}
}
function Event(p_obj)
{
this.obj = p_obj;
this.callbacks = new Array();
}
Event.prototype.Attach = function(p_fnc)
{
var i = this.callbacks.length;
this.callbacks[i] = p_fnc;
return(i);
}
Event.prototype.Detach = function(p_id)
{
delete(this.callbacks[p_id]);
}
Event.prototype.Fire = function(p_src)
{
for (var i=0; i < this.callbacks.length; i++)
{
if (this.callbacks[i])
{
this.callbacks[i](p_src ? p_src : this.obj);
}
}
}
function CenteredSpan(p_strFontFamily, p_strFontWeight, p_iFontSize, p_strFontColor, p_fCenterWhenWrapped)
{
this.spanOuter = document.createElement("span");
DomElement.SetStyles(this.spanOuter,
"position", "absolute",
"overflow", "hidden"
);
if (p_iFontSize)
{
this.spanOuter.style.fontSize = p_iFontSize;
}
this.spanInner = document.createElement("span");
DomElement.SetStyles(this.spanInner,
"position", "relative",
"height", 1
);
if (p_strFontWeight)
{
this.spanInner.style.fontWeight = p_strFontWeight;
}
if (p_strFontFamily)
{
this.spanInner.style.fontFamily = p_strFontFamily;
}
if (p_strFontColor)
{
this.spanInner.style.color = p_strFontColor;
}
this.spanInner.attachEvent("onresize", CenteredSpan.Center);
this.innerElement = this.spanInner;
this.spanOuter.appendChild(this.spanInner);
this.elemCenter = null;
if (p_fCenterWhenWrapped)
{
this.elemCenter = document.createElement("center");
this.spanInner.appendChild(this.elemCenter);
this.innerElement = this.elemCenter;
}
this.element = this.spanOuter;
}
CenteredSpan.prototype.Dispose = function()
{
delete(this.elemCenter);
delete(this.innerElement);
delete(this.spanInner);
delete(this.spanOuter);
}
CenteredSpan.Center = function()
{
var element = window.event.srcElement;
DomPositioning.HorizontallyAlign(element, element.clientWidth, element.parentNode.style.pixelWidth, 0);
DomPositioning.VerticallyAlign(element, element.clientHeight, element.parentNode.style.pixelHeight, 0);
}
CenteredSpan.prototype.Center = function()
{
DomPositioning.HorizontallyAlign(this.spanInner, this.spanInner.clientWidth, this.spanOuter.style.pixelWidth, 0);
DomPositioning.VerticallyAlign(this.spanInner, this.spanInner.clientHeight, this.spanOuter.style.pixelHeight, 0);
}
CenteredSpan.prototype.SetInnerText = function(p_strText)
{
var elem = (this.elemCenter) ? this.elemCenter : this.spanInner;
elem.innerText = p_strText;
this.Center();
}
CenteredSpan.prototype.GetInnerText = function()
{
var elem = (this.elemCenter) ? this.elemCenter : this.spanInner;
return(elem.innerText);
}
function StopWatch()
{
this.iMillisecondsElapsed = 0;
this.lastStartTime = null;
}
StopWatch.prototype.Start = function()
{
if (this.lastStartTime == null)
{
this.lastStartTime = new Date();
}
}
StopWatch.prototype.Stop = function()
{
if (this.lastStartTime != null)
{
this.iMillisecondsElapsed += (new Date() - this.lastStartTime);
this.lastStartTime = null;
}
}
StopWatch.prototype.Reset = function()
{
this.iMillisecondsElapsed = 0;
this.lastStartTime = null;
}
StopWatch.prototype.Elapsed = function()
{
if (this.lastStartTime == null)
{
return(this.iMillisecondsElapsed);
}
else
{
return(this.iMillisecondsElapsed + (new Date() - this.lastStartTime));
}
}
function ToolBar(
p_iHeight,
p_eAlign
)
{
this.eAlign = IsUndefined(p_eAlign) ? ToolBar.COMPACT : p_eAlign;
this.nodes = new Collection(this.Method_("NodesChanged"));
this.aPrioritizedNodes = null;
this.size = new Data.Size(0, IsDefined(p_iHeight) ? p_iHeight : 0);
this.reserveSize = new Data.Size(0, this.size.height);
this.fAutoHeight = (p_iHeight == 0);
this.onSizeChanged = new Event();
this.element = DomElement.CreateElement("span");
this.fAllocated = false;
}
t = ToolBar;
t.COMPACT = 0;
t.MIN = t.LEFT = t.TOP = 1;
t.CENTER = 2;
t.MAX = t.RIGHT = t.BOTTOM = 3;
t.JUSTIFY = 4;
t.STRETCH = 5;
t = null;
ToolBar.prototype.SetEnabled = function(p_fEnabled)
{
this.fEnabled = p_fEnabled;
var c;
for (var i=0; i < this.nodes.array.length; i++)
{
c = this.nodes.array[i].ctrl;
if (c.SetEnabled)
{
c.SetEnabled(p_fEnabled);
}
}
}
ToolBar.prototype.SetDimmer = function(p_fDim)
{
var c;
for (var i=0; i < this.nodes.array.length; i++)
{
c = this.nodes.array[i].ctrl;
if (c.SetDimmer)
{
c.SetDimmer(p_fDim);
}
}
}
ToolBar.prototype.Add = function(p_ctrl, p_iPrePadding, p_iPostPadding, p_iPriority, p_eAlign)
{
this.nodes.Add(new ToolBar.Node(p_ctrl, p_iPrePadding, p_iPostPadding, p_iPriority, p_eAlign));
}
ToolBar.prototype.Dispose = function()
{
Dispose(this.nodes);
}
ToolBar.ControlSizeChanged = function(p_ctrl)
{
var node = p_ctrl.node_;
var toolBar = node.toolBar;
toolBar.reserveSize.width -= node.ReserveWidth();
node.iMinWidth = p_ctrl.reserveSize.width;
toolBar.reserveSize.width += node.ReserveWidth();
if (toolBar.fAllocated)
{
toolBar.AllocateSize(toolBar.size.width, toolBar.size.height);
}
toolBar.onSizeChanged.Fire(toolBar);
}
ToolBar.prototype.NodesChanged = function(p_aAdded, p_aDeleted)
{
var ctrl;
var node;
if (p_aAdded)
{
for (var i=0; i < p_aAdded.length; i++)
{
node = p_aAdded[i];
node.toolBar = this;
ctrl = node.ctrl;
ctrl.element.style.position = "absolute";
this.element.appendChild(ctrl.element);
node.iMinWidth = ctrl.reserveSize.width;
this.reserveSize.width += node.ReserveWidth();
if (ctrl.onSizeChanged)
{
node.idMinSizeChangedEvent = ctrl.onSizeChanged.Attach(ToolBar.ControlSizeChanged);
}
}
}
if (p_aDeleted)
{
for (var i=0; i < p_aDeleted.length; i++)
{
node = p_aDeleted[i];
ctrl = node.ctrl;
this.element.removeChild(ctrl.element);
this.reserveSize.width -= node.ReserveWidth();
if (ctrl.onSizeChanged)
{
ctrl.onSizeChanged.Detach(node.idMinSizeChangedEvent);
}
}
}
this.aPrioritizedNodes = null;
if (this.fAllocated)
{
this.AllocateSize(this.size.width, this.size.height);
}
this.onSizeChanged.Fire(this);
}
ToolBar.prototype.GetPrioritizedNodes = function()
{
if (this.aPrioritizedNodes === null)
{
this.aPrioritizedNodes = this.nodes.GetSlice();
this.aPrioritizedNodes.sort(ToolBar.NodeComparer);
}
return(this.aPrioritizedNodes);
}
ToolBar.NodeComparer = function(p_node1, p_node2)
{
var i = Number.Compare(p_node1.iPriority, p_node2.iPriority);
if (i == 0)
{
i = Number.Compare(Collection.IndexOf(p_node1), Collection.IndexOf(p_node2));
}
return(i);
}
ToolBar.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
this.fAllocated = true;
this.size.width = p_iWidth;
if (this.fAutoHeight)
{
this.size.height = 0;
}
var iLeftover = p_iWidth - this.reserveSize.width;
var apn = this.GetPrioritizedNodes();
var iWidth;
var iHeight;
var iMinWidth;
var iReserved;
var iTotalPadding = 0;
var iNodePadding = 0;
var iVisibleNodes = 0;
var iAllocatedSize = 0;
for (var i=0; i < apn.length; i++)
{
iMinWidth = apn[i].ctrl.reserveSize.width;
iNodePadding = apn[i].iTotalPadding
iLeftover += iMinWidth;
iAllocatedSize = 0;
if ((iMinWidth > 0) || (iLeftover > iNodePadding))
{
iAllocatedSize = Math.max(iMinWidth, iLeftover);
}
if (apn[i].ctrl.AllocateSize)
{
apn[i].ctrl.AllocateSize(iAllocatedSize, p_iHeight);
}
iWidth = apn[i].ctrl.size.width;
iHeight = apn[i].ctrl.size.height;
if (iWidth > 0)
{
if (this.fAutoHeight)
{
this.size.height = Math.max(this.size.height, iHeight);
}
iLeftover -= iWidth;
if (iMinWidth == 0)
{
iLeftover -= iNodePadding;
}
iTotalPadding += iNodePadding;
iVisibleNodes++;
}
}
var iLeft = 0;
var iNumPaddingSections = 0;
switch(this.eAlign)
{
case ToolBar.CENTER:
iLeft = Math.floor(iLeftover / 2);
break;
case ToolBar.MAX:
iLeft = iLeftover;
break;
case ToolBar.JUSTIFY:
iNumPaddingSections = iVisibleNodes - 1;
break;
case ToolBar.STRETCH:
iNumPaddingSections = iVisibleNodes + 1;
break;
}
var iEqualPadding = 0;
var iPaddingDeficit = 0;
var iNodePadding = 0;
var fSkippedOne = false;
var node;
if (iNumPaddingSections > 0)
{
iEqualPadding = (iTotalPadding + iLeftover) / iNumPaddingSections;
for (var i=0; i < this.nodes.length; i++)
{
node = this.nodes.Get(i);
if (node.ctrl.size.width > 0)
{
iNodePadding += node.iPrePadding;
if ((this.eAlign == ToolBar.STRETCH) || fSkippedOne)
{
iPaddingDeficit += Math.max(0, iEqualPadding - iNodePadding);
}
iNodePadding = node.iPostPadding;
fSkippedOne = true;
}
}
if (this.eAlign == ToolBar.STRETCH)
{
iPaddingDeficit += Math.max(0, iEqualPadding - iNodePadding);
}
}
var iExtraPadding = 0
var flRemainderPadding = 0;
var flExactPadding;
fSkippedOne = false;
iNodePadding = 0;
for (var i=0; i < this.nodes.length; i++)
{
node = this.nodes.Get(i);
iWidth = node.ctrl.size.width;
if (iWidth > 0)
{
iNodePadding += node.iPrePadding;
if ((iLeftover > 0) && (iPaddingDeficit > 0) && (fSkippedOne || (this.eAlign == ToolBar.STRETCH)))
{
flExactPadding = (Math.max(0, (iEqualPadding - iNodePadding)) / iPaddingDeficit) * iLeftover;
iExtraPadding = Math.floor(flExactPadding);
flRemainderPadding += (flExactPadding - Math.floor(iExtraPadding));
if (flRemainderPadding >= 1)
{
flRemainderPadding -= 1;
iExtraPadding++;
}
}
iLeft = this.PlaceNode(node, iLeft + iNodePadding + iExtraPadding);
iNodePadding = node.iPostPadding;
fSkippedOne = true;
}
}
if (this.eAlign == ToolBar.COMPACT)
{
this.size.width = iLeft + iNodePadding;
}
Properties.Set(this.element.style,
"width", Math.max(1, this.size.width),
"height", Math.max(1, this.size.height)
);
}
ToolBar.prototype.PlaceNode = function(p_node, p_iLeft)
{
var iTop = 0;
var iDiff = this.size.height- p_node.ctrl.size.height;
switch(p_node.eAlign)
{
case ToolBar.CENTER:
iTop = Math.floor(iDiff / 2);
break;
case ToolBar.MAX:
iTop = iDiff;
break;
}
var iTopOffset = 0;
var iLeftOffset = 0;
if (p_node.ctrl.element.oClip)
{
iTopOffset = p_node.ctrl.element.oClip.iTop;
iLeftOffset = p_node.ctrl.element.oClip.iLeft;
}
Properties.Set(p_node.ctrl.element.style,
"top", iTop - iTopOffset,
"left", p_iLeft - iLeftOffset
);
return(p_iLeft + p_node.ctrl.size.width);
}
ToolBar.Node = function(
p_ctrl,
p_iPrePadding,
p_iPostPadding,
p_iPriority,
p_eAlign
)
{
this.ctrl = p_ctrl;
this.iPriority = IsUndefined(p_iPriority) ? Number.MAX_VALUE : p_iPriority;
this.iPrePadding = p_iPrePadding ? p_iPrePadding : 0;
this.iPostPadding = p_iPostPadding ? p_iPostPadding : 0;
this.eAlign = IsUndefined(p_eAlign) ? ToolBar.CENTER : p_eAlign;
this.ctrl.node_ = this;
this.iTotalPadding = this.iPrePadding + this.iPostPadding;
this.iMinWidth = 0;
}
ToolBar.Node.prototype.Dispose = function()
{
Dispose(this.ctrl);
}
ToolBar.Node.prototype.ReserveWidth = function()
{
return((this.iMinWidth > 0) ? (this.iMinWidth + this.iTotalPadding) : 0);
}
function StackedToolBar(p_iSpacerTopPad, p_iSpacerBotPad, p_strTopColor, p_strBottomColor)
{
this.size = new Data.Size(0, 0);
this.iSpacerTopPad = p_iSpacerTopPad;
this.iSpacerBotPad = p_iSpacerBotPad;
this.iSpacerHeight = p_iSpacerTopPad + p_iSpacerBotPad + 2;
this.strTopColor = p_strTopColor;
this.strBottomColor = p_strBottomColor;
this.element = document.createElement("span");
this.controls = new Array();
this.spacerSpans = new Array();
this.reserveSize = this.size;
this.onSizeChanged = new Event();
this.fEnabled = true;
}
StackedToolBar.prototype.AddControl = function(p_ctrl)
{
if (this.controls.length > 0)
{
this.AddSpacerSpan();
}
p_ctrl.element.style.position = "absolute";
this.element.appendChild(p_ctrl.element);
this.controls[this.controls.length] = p_ctrl;
this.size.height += p_ctrl.size.height;
}
StackedToolBar.prototype.AddSpacerSpan = function()
{
var span = DomElement.CreateElement("span", "position", "absolute", "height", this.iSpacerHeight);
span.topDiv = this.CreateSpacerLine(this.iSpacerTopPad, this.strTopColor);
span.appendChild(span.topDiv);
span.botDiv = this.CreateSpacerLine(this.iSpacerTopPad + 1, this.strBottomColor);
span.appendChild(span.botDiv);
this.element.appendChild(span);
this.size.height += this.iSpacerHeight;
this.spacerSpans[this.spacerSpans.length] = span;
return(span);
}
StackedToolBar.prototype.CreateSpacerLine = function(p_iTop, p_strColor)
{
var divLine = DomElement.CreateElement("div",
"position", "absolute", "height", 1, "top", p_iTop, "backgroundColor", p_strColor);
divLine.appendChild(document.createElement("span"));
return(divLine);
}
StackedToolBar.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
var iTop = 0;
this.size.Resize(p_iWidth, 0);
var span;
for (var i=0; i < this.controls.length; i++)
{
if (i > 0)
{
span = this.spacerSpans[i-1];
span.style.top = iTop;
span.topDiv.style.width = span.botDiv.style.width = span.style.width = p_iWidth;
iTop += this.iSpacerHeight;
}
this.controls[i].AllocateSize(p_iWidth, this.controls[i].height);
this.controls[i].element.style.top = iTop;
iTop += this.controls[i].size.height;
}
this.size.height = iTop;
}
StackedToolBar.prototype.SetEnabled = function(p_fEnabled)
{
this.fEnabled = p_fEnabled;
var c;
for (var i=0; i < this.controls.length; i++)
{
c = this.controls[i];
if (c.SetEnabled)
{
c.SetEnabled(p_fEnabled);
}
}
}
StackedToolBar.prototype.SetDimmer = function(p_fDim)
{
var c;
for (var i=0; i < this.controls.length; i++)
{
c = this.controls[i];
if (c.SetDimmer)
{
c.SetDimmer(p_fDim);
}
}
}
StackedToolBar.prototype.Dispose = function()
{
for (var i=0; i < this.controls.length; i++)
{
Dispose(this.controls[i]);
}
}
function ImageControl(p_img)
{
this.size = new Data.Size(p_img.iWidth, p_img.iHeight);
this.reserveSize = this.size;
this.element = p_img;
this.onSizeChanged = new Event();
}
function Pool(p_fncConstructor)
{
this.fncConstructor = p_fncConstructor;
this.pool = new Collection();
this.nextItem = null;
}
Pool.prototype.Checkout = function()
{
var ret = this.nextItem;
if (!ret)
{
ret = new this.fncConstructor();
this.pool.Add(ret);
}
ret.fUsed_ = true;
this.SetNext();
return(ret);
}
Pool.prototype.SetNext = function()
{
if (!this.nextItem)
{
return;
}
var start = Collection.IndexOf(this.nextItem);
var i = start;
var len = this.pool.array.length;
do
{
if (!this.pool.array[i].fUsed_)
{
this.nextItem = this.pool.array[i];
return;
}
i = (i + 1) % len;
}
while (i != start);
this.iNextIndex = null;
}
Pool.prototype.Return = function(p_item)
{
p_item.fUsed_ = false;
if (!this.nextItem)
{
this.nextItem = p_item;
}
}
Pool.Clean = function(p_item)
{
var f = p_item.fUsed_;
Collection.Clean(p_item);
p_item.fUsed_ = f;
}
function SmartImageControl(p_fClean)
{
this.status = UNINITIALIZED;
this.fSuccess = false;
this.iMaxAttempts = 2;
this.strSrc = null;
this.img = document.createElement("img");
this.img.control = this;
this.element = this.img;
}
SmartImageControl.UNLOAD_URL = "http:/" + "/";
SmartImageControl.prototype.DetachImage = function()
{
var img = this.img;
delete(this.img);
return(img);
}
SmartImageControl.Get = function()
{
return(SmartImageControl.pool.Checkout());
}
SmartImageControl.prototype.Return = function()
{
Pool.Clean(this);
this.constructor(true);
SmartImageControl.pool.Return(this);
}
SmartImageControl.prototype.Dispose = function()
{
this.Unload();
if (this.img)
{
delete(this.img);
}
}
SmartImageControl.prototype.SetSrc = function(p_strSrc)
{
this.strSrc = p_strSrc;
this.status = (!!p_strSrc) ? OBTAINABLE : UNINITIALIZED;
}
SmartImageControl.prototype.Load = function(p_fncLoadCallback)
{
this.fncLoadCallback = p_fncLoadCallback;
if ((this.status == LOADED) || (this.status == UNINITIALIZED))
{
if (this.fncLoadCallback)
{
this.fncLoadCallback(this);
}
return;
}
if (this.status == OBTAINABLE)
{
this.status = LOADING;
if (this.img.parentNode)
{
this.fAddedToBody = false;
}
else
{
this.fAddedToBody = true;
document.body.appendChild(this.img);
this.strLastPosition = this.img.style.position;
this.strLastDisplay = this.img.style.display;
this.img.style.position = "absolute";
this.img.style.display = "none";
}
this.iAttempts = 0;
this.img.attachEvent("onload", SmartImageControl.LoadHandler);
this.img.attachEvent("onerror", SmartImageControl.ErrorHandler);
this.img.src = this.strSrc;
return(true);
}
return(false);
}
SmartImageControl.prototype.Unload = function()
{
if (this.img && this.fSuccess && (this.status == LOADED))
{
this.iLastWidth = this.img.width;
this.iLastHeight = this.img.height;
this.img.src = SmartImageControl.UNLOAD_URL;
this.img.width = this.iLastWidth;
this.img.height = this.iLastHeight;
this.status = OBTAINABLE;
return(true);
}
return(false);
}
SmartImageControl.prototype.DoneLoading = function(p_fSuccess)
{
if (this.fAddedToBody)
{
this.img.removeNode();
this.img.style.position = this.strLastPosition;
this.img.style.display = this.strLastDisplay;
}
this.img.detachEvent("onload", SmartImageControl.LoadHandler);
this.img.detachEvent("onerror", SmartImageControl.ErrorHandler);
this.fSuccess = p_fSuccess;
this.status = LOADED;
if (this.fncLoadCallback)
{
this.fncLoadCallback(this);
}
}
SmartImageControl.LoadHandler = function()
{
var img = window.event.srcElement;
var control = img.control;
control.DoneLoading(true);
}
SmartImageControl.ErrorHandler = function()
{
var img = window.event.srcElement;
var control = img.control;
control.iAttempts++;
if (control.iAttempts < control.iMaxAttempts)
{
img.src = img.src;
}
else
{
control.DoneLoading(false);
}
}
function Control(p_element)
{
this.size = new Data.Size(0, 0);
this.reserveSize = this.size;
this.element = p_element;
this.onSizeChanged = new Event();
this.element.attachEvent("onresize", this.Method_("ResizeHandler"));
}
Control.prototype.ResizeHandler = function()
{
this.size.Resize(this.element.offsetWidth, this.element.offsetHeight);
this.onSizeChanged.Fire(this);
}
function TextSpanControl(p_strText, p_strClassName, p_strColor)
{
this.strClassName = p_strClassName;
this.strColor = p_strColor;
this._constructor_0(DomElement.CreateElement("span", "color", p_strColor));
this.element.className = p_strClassName;
this.element.innerHTML = p_strText;
this.fDim = false;
this.fEnabled = true;
}
TextSpanControl.Inherits_(Control);
TextSpanControl.prototype.SetDimmer = function(p_fDim)
{
this.fDim = p_fDim;
this.element.style.color = (p_fDim || !this.fEnabled) ? "#999999" : this.strColor;
}
TextSpanControl.prototype.SetEnabled = function(p_fEnabled)
{
this.fEnabled = p_fEnabled;
this.element.style.color = (this.fDim || !p_fEnabled) ? "#999999" : this.strColor;
}
function ObjectString(p_obj)
{
var s = "";
s += "class:" + p_obj.constructor + "\n";
var i=0;
for (var p in p_obj)
{
i++;
s += p + ":" + p_obj[p] + "\n";
if (i > 50)
{
break;
}
}
return(s);
}
function DebugAlert(p_str)
{
if (DebugAlert.fCont)
{
DebugAlert.fCont = confirm(p_str);
}
}
DebugAlert.fCont = true;
function Viewer(
p_div,
p_config,
p_log,
p_fncLoadedCallback
)
{
this.div = p_div;
this.config = p_config;
this.log = p_log;
this.fncLoadedCallback = p_fncLoadedCallback;
this.fReady = false;
if (!queryParams["data"])
{
queryParams["data"] = "data.xml";
}
this.log.Append("Loading Data: " + p_config.queryParams["data"]);
LoadXml(p_config.queryParams["data"], this.Method_("DataLoaded"));
this.fFullscreen = Convert.ToBool(p_config.queryParams["fullscreen"]);
}
Viewer.prototype.DataLoaded = function(p_xmlDoc)
{
if (blnIsUnloading) return;
if (!p_xmlDoc)
{
LoadingError();
return;
}
this.log.Append("Loaded Data");
this.data = new ViewerData(p_xmlDoc.documentElement);
this.fFromSlideshow = (window.opener && window.opener.viewer);
if (this.fFromSlideshow)
{
var otherLogic = window.opener.viewer.viewerLogic;
this.fFullscreen = !window.opener.viewer.fFullscreen;
this.data.player.iStartIndex = otherLogic.GetIndex();
this.data.player.fPlay = !!otherLogic.fPlayUponTransfer;
this.data.player.speed.iDefault = otherLogic.fncSpeed(1 - otherLogic.speedSlider.GetValue());
}
this.config.fFullscreen = this.fFullscreen;
var fIsFullScreenWorkAround = this.fFullscreen && (Browser.IsMsnClient() && (!Browser.IsMsn9Plus()));
if (!this.fFullscreen || fIsFullScreenWorkAround)
{
var width = this.data.player.size.iWidth;
var height = this.data.player.size.iHeight;
if (AllWithValues(width, height) || fIsFullScreenWorkAround)
{
var iUsedWidth = screen.width - screen.availWidth;
var iModifiedAvailWidth = screen.availWidth - iUsedWidth;
width = fIsFullScreenWorkAround || (width == -1) ? iModifiedAvailWidth : width;
height = fIsFullScreenWorkAround || (height == -1) ? screen.availHeight : height;
if (Browser.IsMsnClient())
{
width = width - (4 * 2);
height = height - 28;
}
var left = Math.floor((iModifiedAvailWidth - width) / 2) + iUsedWidth;
var top = Math.floor((screen.availHeight - height) / 2);
window.moveTo(left,top);
window.resizeTo(width,height);
}
}
this.Setup();
}
Viewer.prototype.Setup = function()
{
this.log.Append("Creating Layout");
this.viewerLayout = new ViewerLayout(this.Method_("LayoutReady"), this.div, this.config, this.log);
}
Viewer.prototype.LayoutReady = function()
{
if (!this.viewerLayout.fReady)
{
this.log.Append("Failed to create Layout");
this.fncLoadedCallback();
return;
}
this.log.Append("Created Layout");
this.fReady = true;
this.viewerLogic = new ViewerLogic(this.log, this.config, this.data, this.viewerLayout, this.fFullscreen);
if (this.fFromSlideshow)
{
if (this.fFullscreen)
{
this.viewerLogic.wndWindowed = window.opener;
}
else
{
this.viewerLogic.wndFullScreen = window.opener;
}
}
if (this.viewerLogic.messenger)
{
if (this.viewerLogic.messenger.users.fIsInviter)
{
SafeSetTimeout(this.Method_("InviterInit"), 1);
}
else
{
SafeSetTimeout(this.Method_("GuestInit"), 1);
}
}
else
{
this.fncLoadedCallback();
}
}
Viewer.prototype.InviterInit = function()
{
this.log.Append("Hooking up RemoteAppLoadedHandler");
this.viewerLogic.messenger.channel.fncRemoteAppLoadedHandler = this.fncLoadedCallback;
this.viewerLogic.messenger.channel.Initialize(true);
}
Viewer.prototype.GuestInit = function()
{
this.log.Append("Hooking up RemoteAppLoadedHandler");
this.viewerLogic.messenger.channel.fncRemoteAppLoadedHandler = this.Method_("InviterIsReady");
this.fncLoadedCallback();
this.viewerLogic.messenger.channel.Initialize(true);
}
Viewer.prototype.InviterIsReady = function()
{
if (!this.viewerLogic.messenger.channel.fRemoteAppAvailable)
{
this.viewerLogic.RemoteAppFailedToLoad();
}
}
Viewer.prototype.Go = function()
{
this.viewerLayout.controls.SetVisibility(false);
this.viewerLayout.controls.SetActive(true);
this.viewerLayout.Display();
this.viewerLogic.Run();
}
Viewer.prototype.Dispose = function()
{
Dispose(this.viewerLogic);
Dispose(this.viewerLayout);
}
ViewerLogic.Messages = function(p_logic)
{
this.logic = p_logic;
this.strMessage = null;
if (this.logic.messenger)
{
this.logic.messenger.channel.fncDataReceivedHandler = this.Method_("Receive");
}
}
ViewerLogic.Messages.prototype.Add = function(p_str, p_fOnlyIfInControl)
{
if (this.logic.fInControl || (!p_fOnlyIfInControl))
{
if (this.strMessage == null)
{
this.strMessage = "this.logic." + p_str;
}
else
{
this.strMessage += ";this.logic." + p_str;
}
}
}
ViewerLogic.Messages.prototype.Clear = function()
{
this.strMessage = null;
}
ViewerLogic.Messages.prototype.Send = function(p_str, p_fOnlyIfInControl)
{
if (this.logic.fInControl || (!p_fOnlyIfInControl))
{
if (p_str)
{
this.Add(p_str);
}
if (this.logic.messenger && this.logic.messenger.channel.fRemoteAppAvailable)
{
this.logic.messenger.channel.Send(this.strMessage);
}
this.strMessage = null;
}
}
ViewerLogic.Messages.prototype.Receive = function(p_strMessage)
{
eval(p_strMessage);
}
ViewerLogic.Messages.prototype.SendFile = function(p_file)
{
if (this.logic.messenger && this.logic.messenger.channel.fRemoteAppAvailable)
{
var fileSending = this.logic.messenger.channel.SendFile(p_file);
}
}
ViewerLogic.MessageSender = function(p_Messages, p_strPrefix)
{
this.messages = p_Messages;
this.strPrefix = p_strPrefix;
}
ViewerLogic.MessageSender.prototype.Add = function(p_str, p_fOnlyIfInControl)
{
this.messages.Add(this.strPrefix + p_str, p_fOnlyIfInControl);
}
ViewerLogic.MessageSender.prototype.Clear = function()
{
this.messages.Clear();
}
ViewerLogic.MessageSender.prototype.Send = function(p_str, p_fOnlyIfInControl)
{
this.messages.Send(this.strPrefix + p_str, p_fOnlyIfInControl);
}
ViewerLogic.MessageSender.prototype.SendFile = function(p_file)
{
this.messages.SendFile(p_file);
}
function ViewerLogic(p_log, p_config, p_data, p_layout, p_fFullscreen)
{
this.log = p_log;
this.config = p_config;
this.messenger = this.config.messenger;
this.data = p_data;
this.layout = p_layout;
this.fFullscreen = p_fFullscreen;
this.fHaveSlides = (this.data.slides.array.length != 0);
this.SetSlideControlsEnabled(true);
this.fInControl = true;
this.messages = new ViewerLogic.Messages(this);
if (this.messenger)
{
this.SetHaveControl(this.config.messenger.users.fIsInviter, true);
this.messenger.channel.fncRemoteAppClosedHandler = this.Method_("RemoteAppClosed");
this.messenger.channel.fncTypeChangedHandler = this.Method_("TypeChanged");
}
else
{
this.layout.controls.toggleTextButton.SetOnOff(!this.fFullscreen);
this.layout.display.SetTextVisibility(!this.fFullscreen, !this.fFullscreen);
}
this.iPassControlCount = 0;
this.fIgnoreChangeEvents = false;
this.currentSlideView = null;
this.controls = this.layout.controls;
this.controls.playerToolBar.imgStopButton.SetEnabled(false);
this.controls.playerToolBar.imgStopButton.fAllowSimulation = false;
this.stopWatch = new StopWatch();
this.speedSlider = this.layout.controls.speedSliderToolBar.slider;
this.speedSlider.SetValueRange(0, 1);
this.fncSpeed = Math2.LogarithmicPercentFunction(this.data.player.speed.iMin, this.data.player.speed.iMax);
this.speedSlider.SetValue(1 - this.fncSpeed.fncInverse(this.data.player.speed.iDefault));
this.iTimeInterval = this.data.player.speed.iDefault * 1000;
this.iMinSpeedChangeInterval = 1000;
this.playMode = STOP;
var dataHandlerMessages = new ViewerLogic.MessageSender(this.messages, "dataHandler.");
this.dataHandler = new ViewerLogic.DataHandler(this, dataHandlerMessages, this.Method_("LoadHandler"));
this.HookupEvents();
}
ViewerLogic.prototype.Dispose = function()
{
var wndFull = this.wndFullScreen;
if ((wndFull) && (!wndFull.closed))
{
wndFull.close();
}
Dispose(this.dataHandler);
Dispose(this.messages);
}
ViewerLogic.prototype.RemoteAppFailedToLoad = function()
{
this.RemoteAppClosed(true);
}
ViewerLogic.prototype.Run = function()
{
var objThis = this;
function Play(p_blnDoneFading)
{
objThis.AdjustPreviousNextButtons();
if (p_blnDoneFading)
{
if (objThis.playMode != PLAY)
{
objThis.layout.controls.playerToolBar.imgPlayPauseButton.Press();
}
}
}
function FadeAway()
{
objThis.layout.controls.playerToolBar.imgPreviousButton.SetEnabled(true);
objThis.layout.controls.playerToolBar.imgStopButton.SetEnabled(true);
objThis.layout.controls.FadeAway(Play);
}
if (this.data.slides.array.length > 300)
{
this.layout.controls.filmstrip.Hide();
}
this.dataHandler.Run();
this.layout.display.SetMarginSize(this.fFullscreen ? 0 : 10);
if (this.messenger)
{
var strText;
if (this.config.messenger.users.fIsInviter)
{
if (this.config.messenger.channel.fRemoteAppAvailable)
{
strText = this.config.strings.RES_SlideViewer_OpeningInviter;
}
else
{
this.RemoteAppFailedToLoad();
return;
}
}
else
{
strText = this.config.strings.RES_SlideViewer_OpeningGuest.replace("{0}", this.config.messenger.users.them.Name);
}
var openingSlide = new SlideView(null, null, null, strText);
this.layout.display.DisplaySlide(openingSlide);
}
else
{
this.GotoIndex(this.data.player.iStartIndex);
if (this.data.player.fPlay)
{
if (this.data.player.iStartIndex != (this.data.slides.array.length - 1))
{
}
}
}
}
ViewerLogic.prototype.GetIndex = function()
{
return(Collection.IndexOf(this.currentSlideView));
}
ViewerLogic.prototype.GotoDelayed = function()
{
this.GotoIndex(this.iGotoDelayIndex, this.fGotoDelayTransition, this.fGotoDelayShowFilmStripMovement, true, true);
}
ViewerLogic.prototype.GotoIndex = function(p_iIndex, p_fDoTransition, p_fShowFilmStripMovement, p_fNoMessage, p_fNoDelay)
{
if (!Math2.IsBetween(0, p_iIndex, this.data.slides.length - 1))
{
return;
}
if (!p_fNoMessage)
{
this.messages.Send(Function.BuildCallAsString("GotoIndex", p_iIndex, p_fDoTransition, p_fShowFilmStripMovement, true, true), true);
}
if (this.fInControl && (!p_fNoDelay))
{
this.iGotoDelayIndex = p_iIndex;
this.fGotoDelayTransition = p_fDoTransition;
this.fGotoDelayShowFilmStripMovement = p_fShowFilmStripMovement;
SafeSetTimeout(this.Method_("GotoDelayed"), 1);
return;
}
this.pendingSlideView = null;
this.ClearPlayTimer();
this.fIgnoreChangeEvents = true;
this.layout.controls.filmstrip.SetIndex(p_iIndex, p_fShowFilmStripMovement);
this.layout.controls.imageSlider.SetValue(p_iIndex);
this.fIgnoreChangeEvents = false;
this.currentSlideView = this.dataHandler.slideViews.array[p_iIndex];
this.AdjustPreviousNextButtons();
this.layout.display.DisplaySlide(this.currentSlideView, p_fDoTransition);
this.dataHandler.SetLoadFocus(p_iIndex);
}
ViewerLogic.prototype.AdjustPreviousNextButtons = function()
{
var iIndex = this.GetIndex();
if (iIndex != -1)
{
var fFirst = iIndex == 0;
var fLast = iIndex == (this.data.slides.length - 1);
this.layout.controls.playerToolBar.imgPreviousButton.SetEnabled(!fFirst && this.fInControl);
this.layout.controls.playerToolBar.imgPreviousButton.fAllowSimulation = !fFirst;
this.layout.controls.playerToolBar.imgNextButton.SetEnabled(!fLast && this.fInControl);
this.layout.controls.playerToolBar.imgNextButton.fAllowSimulation = !fLast;
}
}
ViewerLogic.prototype.ToggleFullscreen = function()
{
var strUrl = document.location.href;
var strOptions;
var strWindowName = "SlideshowViewer";
var wndOther;
if (this.fFullscreen)
{
wndOther = this.wndWindowed;
}
else
{
wndOther = this.wndFullScreen;
if (Browser.IsMsnClient() && (!Browser.IsMsn9Plus()))
{
strOptions = "left=0,top=0,status=no,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=no";
}
else
{
strOptions = "fullscreen=yes";
}
strWindowName += "FullScreen";
}
if (!this.TransferToOtherWindow(wndOther))
{
if (this.playMode == PLAY)
{
this.fPlayUponTransfer = true;
this.layout.controls.playerToolBar.imgPlayPauseButton.Press();
}
else
{
this.fPlayUponTransfer = false;
}
this.wndFullScreen = window.open(strUrl, strWindowName, strOptions);
this.wndFullScreen.focus();
if (this.fFullscreen)
{
window.self.close();
}
else
{
window.self.blur();
}
}
}
ViewerLogic.prototype.TransferToOtherWindow = function(p_wndTo)
{
if ((p_wndTo) && (!p_wndTo.closed) && (p_wndTo.viewer))
{
var fromLogic = this;
var toLogic = p_wndTo.viewer.viewerLogic;
toLogic.StopHandler();
toLogic.GotoIndex(fromLogic.GetIndex());
toLogic.speedSlider.SetValue(fromLogic.speedSlider.GetValue());
if (fromLogic.playMode == PLAY)
{
toLogic.layout.controls.playerToolBar.imgPlayPauseButton.Press();
fromLogic.layout.controls.playerToolBar.imgPlayPauseButton.Press();
}
p_wndTo.focus();
if (this.fFullscreen)
{
window.self.close();
}
else
{
window.self.blur();
}
return(true);
}
else
{
return(false);
}
}
ViewerLogic.prototype.OpenInParent = function(p_strUrl)
{
if ((this.wndChild) && (!this.wndChild.closed))
{
this.wndChild.opener = window.self;
this.wndChild.location.href = p_strUrl;
}
else
{
var iCount = this.data.slides.array.length;
var fOneRow = (iCount <= 4);
var iCols = Math.min(iCount, 4);
var iWidth = Math.min(window.screen.availWidth, iCols <= 2 ? 640 : 800);
var iHeight = Math.min(window.screen.availHeight, fOneRow ? 500 : 650);
var p = DomWindow.prototype.CenterWindow(iWidth, iHeight);
var strOptions = "top=" + p.y + ",left=" + p.x + ",height=" + iHeight + ",width=" + iWidth + ",directories=1,location=1,menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=1";
this.wndChild = window.open(p_strUrl, "ViewerChild", strOptions);
}
this.wndChild.focus();
}
ViewerLogic.prototype.HookupEvents = function()
{
this.layout.display.fncSlideDisplayedCallback = this.Method_("SlideDisplayedHandler");
this.layout.controls.filmstrip.fncClickedCallback = this.Method_("FilmStripClickedHandler");
this.layout.controls.imageSlider.fncValueChangedCallback = this.Method_("ImageSliderChangedHandler");
this.layout.controls.imageSlider.fncMouseDownCallback = this.Method_("ImageSliderDownHandler");
this.layout.controls.imageSlider.fncMouseMoveCallback = this.Method_("ImageSliderMoveHandler");
this.layout.controls.imageSlider.onMouseUpCallback.Attach(this.Method_("ImageSliderUpHandler"));
this.layout.controls.speedSliderToolBar.slider.fncValueChangedCallback = this.Method_("SpeedSliderChangedHandler");
this.layout.controls.speedSliderToolBar.slider.fncMouseDownCallback = this.Method_("SpeedSliderDownHandler");
this.layout.controls.speedSliderToolBar.slider.fncMouseMoveCallback = this.Method_("SpeedSliderMoveHandler");
this.layout.controls.speedSliderToolBar.slider.onMouseUpCallback.Attach(this.Method_("SpeedSliderUpHandler"));
this.layout.controls.playerToolBar.imgPlayPauseButton.fncOnClickHandler = this.Method_("PlayPauseHandler");
this.layout.controls.playerToolBar.imgStopButton.fncOnClickHandler = this.Method_("StopHandler");
this.layout.controls.playerToolBar.imgPreviousButton.fncOnClickHandler = this.Method_("PreviousHandler");
this.layout.controls.playerToolBar.imgNextButton.fncOnClickHandler = this.Method_("NextHandler");
document.body.attachEvent("onkeydown", this.Method_("KeyDownHandler"));
document.body.attachEvent("onkeyup", this.Method_("KeyUpHandler"));
if (this.messenger)
{
var m = this.layout.controls.messengerToolbar;
m.addPicturesButton.fncOnClickHandler = this.Method_("AddPicturesHandler");
m.haveControlButton.fncOnClickHandler = this.Method_("GiveControlHandler");
}
else
{
this.layout.controls.toggleTextButton.fncOnClickHandler = this.Method_("ToggleTextHandler");
this.layout.controls.windowButton.SetEnabled(this.data.player.size.fFullScreen);
this.layout.controls.windowButton.fncOnClickHandler = this.Method_("FullScreenHandler");
this.HookupLinkButton(this.config.btnPrint, this.controls.buttonToolBar.print);
this.HookupLinkButton(this.config.btnDownload, this.controls.buttonToolBar.download);
this.HookupLinkButton(this.config.btnOrderPrints, this.controls.buttonToolBar.fuji);
this.HookupLinkButton(this.config.btnOrderGifts, this.controls.buttonToolBar.gifts);
}
}
ViewerLogic.prototype.SetSlideControlsEnabled = function(p_fEnabled)
{
this.fSlideControlsEnabled = p_fEnabled;
var f = p_fEnabled && this.fHaveSlides;
this.layout.controls.imageSlider.SetEnabled(f);
this.layout.controls.speedSliderToolBar.SetEnabled(f);
this.layout.controls.playerToolBar.SetEnabled(f);
}
ViewerLogic.prototype.HookupLinkButton = function(p_buttonData, p_button)
{
if (p_button)
{
if (p_buttonData.fEnabled)
{
p_button.fncOnClickHandler = this.Method_("LinkButtonClickHandler");
p_button.strLinkUrl = p_buttonData.strUrl;
}
else
{
p_button.SetEnabled(false);
}
}
}
ViewerLogic.prototype.AddRemotePictures = function(p_iCount)
{
var aSlides = new Array(p_iCount);
for (var i=0; i < p_iCount; i++)
{
aSlides[i] = ViewerData.Slide.CreateAsEmpty();
}
this.data.slides.Add(aSlides);
}
ViewerLogic.prototype.AddPicturesHandler = function(p_button)
{
if (this.messenger)
{
this.layout.controls.messengerToolbar.addPicturesButton.SetEnabled(false);
this.config.GetSubscriptionStatus(this.Method_("SubscriptionCheckHandler"))
}
else
{
this.SubscriptionCheckHandler(true);
}
}
ViewerLogic.prototype.SubscriptionCheckHandler = function(p_fIsSub)
{
if (p_fIsSub)
{
GetActiveXControl(this.Method_("AddPictures"), this.config);
}
else
{
if (this.messenger)
{
this.layout.controls.messengerToolbar.addPicturesButton.SetEnabled(true);
}
var wnd = window.open(this.config.strUpsellUrl, "Upsell");
try
{
wnd.focus();
}
catch(e)
{
}
}
}
ViewerLogic.prototype.AddPictures = function(p_ctrl)
{
if (this.messenger)
{
this.layout.controls.messengerToolbar.addPicturesButton.SetEnabled(true);
}
var fileToSend;
var aSafeArrayFileInfo = null;
try
{
aSafeArrayFileInfo = p_ctrl.PickFiles();
}
catch(e)
{
return;
}
if (aSafeArrayFileInfo != null)
{
var aFileInfo = (new VBArray(aSafeArrayFileInfo)).toArray();
if (aFileInfo.length > 0)
{
var aSlides = new Array(aFileInfo.length);
for (var i=0; i < aFileInfo.length; i++)
{
aSlides[i] = ViewerData.Slide.CreateWithFile(aFileInfo[i]);
}
this.messages.Send(Function.BuildCallAsString("AddRemotePictures", aFileInfo.length));
this.data.slides.Add(aSlides);
if (!this.currentSlideView)
{
this.GotoIndex(0);
}
this.dataHandler.CreateNext();
}
}
}
ViewerLogic.prototype.TypeChanged = function(p_fConnected, p_fDirectConnection)
{
log.Append("Type Changed Handler");
if (!p_fConnected)
{
this.RemoteAppClosed(true);
}
}
ViewerLogic.prototype.RemoteAppClosed = function(p_fNoMessage)
{
log.Append("RemoteAppClosed");
this.dataHandler.RemoteAppClosed(p_fNoMessage);
this.SetHaveControl(true, true);
this.layout.controls.messengerToolbar.haveControlButton.SetEnabled(false);
}
ViewerLogic.prototype.GiveControlHandler = function()
{
this.SetHaveControl(false);
this.messages.Send(Function.BuildCallAsString("SetHaveControl", true));
var strText = this.config.strings.RES_SlideViewer_GrantedControl;
strText = strText.replace(/\{0\}/g, this.config.messenger.users.me.Name);
strText = strText.replace(/\{1\}/g, this.config.messenger.users.them.Name);
this.messenger.channel.SendIM(strText);
}
ViewerLogic.prototype.SetHaveControl = function(p_fhaveControl)
{
if (!this.fHaveSlides)
{
this.layout.display.DisplaySlide(null);
}
this.fInControl = p_fhaveControl;
this.layout.controls.messengerToolbar.haveControlButton.SetOnOff(p_fhaveControl);
this.layout.controls.filmstrip.SetEnabled(p_fhaveControl);
this.SetSlideControlsEnabled(p_fhaveControl);
this.layout.controls.messengerToolbar.haveControlButton.SetEnabled(p_fhaveControl);
this.layout.controls.messengerToolbar.addPicturesButton.SetEnabled(p_fhaveControl);
this.ClearPlayTimer();
if (p_fhaveControl)
{
this.layout.controls.SetActive(true);
this.SetPlayTimer();
}
}
ViewerLogic.prototype.LinkButtonClickHandler = function(p_button)
{
if (this.playMode == PLAY)
{
this.layout.controls.playerToolBar.imgPlayPauseButton.Press();
}
this.OpenInParent(p_button.strLinkUrl);
}
ViewerLogic.prototype.KeyDownHandler = function()
{
event.cancelBubble = true;
var fAltKey = event.altKey;
var fCtrlKey = event.ctrlKey;
var fCtrlAltKey = fAltKey && fCtrlKey;
switch(event.keyCode)
{
case 72:
if (fCtrlAltKey)
{
this.layout.controls.SetVisibility(!this.layout.controls.fAlwaysVisible);
}
break;
case 88:
break;
case 76:
}
if (!this.fInControl)
{
return;
}
switch(event.keyCode)
{
case 36: 

this.GotoIndex(0, false, false);
break;
case 35: 

this.GotoIndex(this.data.slides.length - 1, false, false);
break;
case 66: 

if (!fCtrlAltKey)
{
break;
}
case 37: 

case 40: 

this.layout.controls.playerToolBar.imgPreviousButton.Press();
break;
case 70: 

if (!fCtrlAltKey)
{
break;
}
case 38: 

case 39: 

this.layout.controls.playerToolBar.imgNextButton.Press();
break;
case 107: 

this.speedSlider.SetValue(this.speedSlider.GetValue() + .01);
this.fSpeedDirty = true;
break;
case 109: 

this.speedSlider.SetValue(this.speedSlider.GetValue() - .01);
this.fSpeedDirty = true;
break;
case 80: 

if (fCtrlAltKey)
{
this.layout.controls.playerToolBar.imgPlayPauseButton.Press();
}
break;
case 32: 

if (!this.messenger)
{
this.layout.controls.playerToolBar.imgPlayPauseButton.Press();
}
break;
case 83: 

if (fCtrlAltKey)
{
this.layout.controls.playerToolBar.imgStopButton.Press();
}
break;
case 27: 

if (!this.messenger)
{
if (this.fFullscreen)
{
this.layout.controls.windowButton.Press();
}
else
{
this.layout.controls.playerToolBar.imgStopButton.Press();
}
}
break;
case 13: 

if (fAltKey)
{
if (!this.messenger)
{
this.layout.controls.windowButton.Press();
}
}
break;
case 84: 

if (!this.messenger)
{
if (fCtrlAltKey)
{
this.layout.controls.toggleTextButton.Press();
}
}
break;
case 65: 

if (fCtrlAltKey)
{
this.AddPicturesHandler();
}
break;
case 67: 

if (fCtrlAltKey && this.messenger)
{
this.GiveControlHandler();
}
break;
}
event.cancelBubble = true;
return;
}
ViewerLogic.prototype.KeyUpHandler = function()
{
if (this.messenger)
{
if (this.fSpeedDirty)
{
this.SpeedSliderUpHandler();
this.fSpeedDirty = false;
}
}
}
ViewerLogic.prototype.SlideDisplayedHandler = function(p_slideView)
{
if (p_slideView && (p_slideView.status == LOADED))
{
if (p_slideView.fSuccess)
{
var iReloadCount = parseInt(this.config.cookies["reloadcount"]);
iReloadCount = isNaN(iReloadCount) ? 1 : iReloadCount + 1;
document.cookie = "reloadcount=" + iReloadCount;
this.stopWatch.Reset();
if (this.playMode == PLAY)
{
this.stopWatch.Start();
}
this.SetPlayTimer();
}
else
{
this.Advance();
}
}
}
ViewerLogic.prototype.FilmStripClickedHandler = function(p_iIndex)
{
if (p_iIndex != this.GetIndex())
{
this.GotoIndex(p_iIndex, false, true);
}
}
ViewerLogic.prototype.ImageSliderChangedHandler = function(p_flValue)
{
if (!this.fIgnoreChangeEvents)
{
this.layout.controls.filmstrip.SetIndex(p_flValue, false);
}
}
ViewerLogic.prototype.ImageSliderDownHandler = function()
{
this.pendingSlideView = null;
this.ClearPlayTimer();
}
ViewerLogic.prototype.ImageSliderMoveHandler = function()
{
}
ViewerLogic.prototype.ImageSliderUpHandler = function()
{
this.GotoIndex(this.layout.controls.imageSlider.GetValue(), false, false);
}
ViewerLogic.prototype.SpeedSliderChangedHandler = function(p_flValue)
{
this.iTimeInterval = this.fncSpeed(1 - this.speedSlider.GetValue()) * 1000;
this.SetPlayTimer();
}
ViewerLogic.prototype.SpeedSliderDownHandler = function()
{
}
ViewerLogic.prototype.SpeedSliderMoveHandler = function()
{
}
ViewerLogic.prototype.SpeedSliderUpHandler = function()
{
var iSpeedSliderValue = this.layout.controls.speedSliderToolBar.slider.GetValue();
this.messages.Send("layout.controls.speedSliderToolBar.slider.SetValue(" + iSpeedSliderValue + ")", true);
}
ViewerLogic.prototype.PlayPauseHandler = function(p_button, p_fPlay)
{
this.messages.Send("layout.controls.playerToolBar.imgPlayPauseButton.Press()", true);
this.layout.controls.playerToolBar.imgStopButton.SetEnabled(true && this.fInControl);
this.layout.controls.playerToolBar.imgStopButton.fAllowSimulation = true;
if (p_fPlay)
{
var lastPlayMode = this.playMode;
this.playMode = PLAY;
if (lastPlayMode == PAUSE)
{
this.stopWatch.Reset();
}
this.stopWatch.Start();
if ((lastPlayMode == STOP) && (this.GetIndex() == (this.data.slides.length - 1)))
{
this.Advance(true);
}
else
{
this.SetPlayTimer();
}
}
else
{
this.playMode = PAUSE;
this.ClearPlayTimer();
this.stopWatch.Stop();
}
}
ViewerLogic.prototype.StopHandler = function()
{
this.layout.controls.playerToolBar.imgStopButton.SetEnabled(false);
this.layout.controls.playerToolBar.imgStopButton.fAllowSimulation = false;
this.SlideShowOver();
this.messages.Send("layout.controls.playerToolBar.imgStopButton.Press()", true);
this.GotoIndex(0, false, false, true);
}
ViewerLogic.prototype.PreviousHandler = function()
{
var iIndex = this.GetIndex() - 1;
this.PlayerButtonChange("layout.controls.playerToolBar.imgPreviousButton", iIndex);
this.GotoIndex(iIndex, false, true, true);
}
ViewerLogic.prototype.NextHandler = function()
{
var iIndex = this.GetIndex() + 1;
this.PlayerButtonChange("layout.controls.playerToolBar.imgNextButton", iIndex);
this.GotoIndex(iIndex, false, true, true);
}
ViewerLogic.prototype.PlayerButtonChange = function(p_strButton, p_iIndex)
{
this.messages.Add(p_strButton + ".MouseDownHandler()", true);
this.messages.Add(Function.BuildCallAsString("GotoIndex", p_iIndex, false, true, true), true);
this.messages.Send(p_strButton + ".MouseOutHandler()", true);
}
ViewerLogic.prototype.ToggleTextHandler = function(ctrl, fOn)
{
this.layout.display.SetTextVisibility(fOn, fOn);
}
ViewerLogic.prototype.FullScreenHandler = function()
{
this.ToggleFullscreen();
}
ViewerLogic.prototype.ClearPlayTimer = function()
{
if (this.idPlayTimer)
{
window.clearTimeout(this.idPlayTimer);
this.idPlayTimer = null;
}
}
ViewerLogic.prototype.SetPlayTimer = function()
{
if ((this.playMode == PLAY) && this.fInControl && (this.layout.controls.imageSlider.eState == Slider.NORMAL))
{
this.ClearPlayTimer();
var iTimeIntervalToUse = Math.max(
Math.min(this.iTimeInterval, this.iMinSpeedChangeInterval),
this.iTimeInterval - this.stopWatch.Elapsed()
);
this.idPlayTimer = SafeSetTimeout(this.Method_("Advance"), iTimeIntervalToUse);
}
}
ViewerLogic.prototype.FindNextNonErrorSlide = function(p_iStartIndex)
{
var slideView;
for (var i=p_iStartIndex; i < this.data.slides.length; i++)
{
slideView = this.dataHandler.slideViews.array[i];
if ((slideView.status != LOADED) || (slideView.fSuccess))
{
return(i);
}
}
return(-1);
}
ViewerLogic.prototype.Advance = function(fDoLoop)
{
if ((this.playMode != PLAY) || (!this.fInControl))
{
return;
}
var fFullScreen = this.fFullscreen;
var fShowMovement = (!(fFullScreen || fDoLoop));
var iIndexToShow = this.FindNextNonErrorSlide(fDoLoop ? 0 : this.GetIndex() + 1);
if (iIndexToShow == -1)
{
this.SlideShowOver();
this.messages.Send(Function.BuildCallAsString("SlideShowOver"), true);
this.layout.controls.SetActive(true);
return;
}
var slideView = this.dataHandler.slideViews.array[iIndexToShow];
if (slideView.status == LOADED)
{
this.GotoIndex(iIndexToShow, true, fShowMovement);
}
else
{
this.pendingSlideView = slideView;
this.dataHandler.SetLoadFocus(iIndexToShow);
}
}
ViewerLogic.prototype.SlideShowOver = function()
{
this.layout.controls.playerToolBar.imgPlayPauseButton.SetOnOff(false);
this.playMode = STOP;
this.ClearPlayTimer();
this.stopWatch.Stop();
this.stopWatch.Reset();
}
ViewerLogic.prototype.LoadHandler = function(p_item, p_fSuccess)
{
if (this.currentSlideView == p_item)
{
this.layout.display.DisplaySlide(p_item, false, true);
}
if (this.pendingSlideView == p_item)
{
this.pendingSlideView = null;
this.Advance();
}
}
var THUMBNAIL = 1;
var SLIDEVIEW = 2;
ViewerLogic.DataHandler = function(p_logic, p_messages, p_fncLoadCallback)
{
this.logic = p_logic;
this.messages = p_messages;
this.layout = p_logic.layout;
this.config = p_logic.config;
this.data = p_logic.data;
this.thumbnails = this.layout.controls.filmstrip.thumbnails;
this.slideViews = new Collection();
this.filmstrip = this.layout.controls.filmstrip;
this.imageCreator = new ViewerLogic.DataHandler.ImageCreator(this.logic.log);
if (this.logic.messenger)
{
this.logic.messenger.channel.fncFileReceivedHandler = this.Method_("ReceiveFile");
}
this.fCreating = false;
this.iPendingCreates = 0;
this.fObtaining = false;
this.iPendingObtains = 0;
this.iMaxLoading = 1;
this.iNumLoading = 0;
this.iMaxAttempts = 2;
this.iPreSlide = 1;
this.iPostSlide = 1;
this.fncLoadCallback = p_fncLoadCallback;
this.iPendingLoads = 0;
this.focalSlide = null;
this.data.slides.fncCollectionChanged = this.Method_("SlideDataChanged");
}
ViewerLogic.DataHandler.prototype.Dispose = function()
{
for (var i=0; i < this.slideViews.array.length; i++)
{
if (!this.filmstrip.fHidden)
{
Dispose(this.thumbnails.array[i].smartImage);
}
Dispose(this.slideViews.array[i].smartImage);
Dispose(this.slideViews.array[i]);
}
}
ViewerLogic.DataHandler.prototype.Run = function()
{
this.SlideDataChanged(this.data.slides.array);
}
ViewerLogic.DataHandler.prototype.RemoteAppClosed = function(p_fNoMessage)
{
var strText = this.config.strings.RES_SlideViewer_Closed.replace("{0}", this.config.messenger.users.them.Name);
if (!p_fNoMessage)
{
}
this.itemSending = null;
this.fObtaining = false;
for (var i=0; i < this.thumbnails.array.length; i++)
{
this.CancelItem(this.thumbnails.array[i]);
this.CancelItem(this.slideViews.array[i]);
}
this.iPendingObtains = 0;
this.LoadNext();
}
ViewerLogic.DataHandler.prototype.CancelItem = function(p_item)
{
switch(p_item.status)
{
case UNINITIALIZED:
p_item.status = LOADED;
p_item.fSuccess = false;
if (p_item.constructor == Thumbnail)
{
p_item.Update(null, null);
}
else if (p_item.constructor == SlideView)
{
p_item.Update(null, p_item.strTitle, p_item.strDetails, null);
}
break;
case CREATED:
this.iPendingObtains--;
case OBTAINING:
this.iPendingObtains--;
this.iPendingLoads++;
p_item.status = OBTAINABLE;
break;
}
}
ViewerLogic.DataHandler.prototype.SlideDataChanged = function(p_aAdded, p_aRemoved)
{
if (p_aAdded)
{
var fSmartImage;
var strText;
var status;
var thumbnail;
var slideView;
var sd;
for (var i=0; i < p_aAdded.length; i++)
{
if (!this.logic.fHaveSlides)
{
this.logic.fHaveSlides = true;
this.logic.SetSlideControlsEnabled(this.logic.fSlideControlsEnabled);
}
sd = p_aAdded[i];
fSmartImage = false;
strText = sd.strText;
status = LOADED;
if ((sd.images && (sd.images.length > 0)) || sd.rawFile || sd.fIsEmpty)
{
fSmartImage = true;
if (sd.images && (sd.images.length > 0))
{
status = OBTAINABLE;
}
else if (sd.rawFile)
{
status = INITIALIZED;
}
else
{
status = UNINITIALIZED;
}
strText = this.config.strings.RES_SlideViewer_LoadingPleaseWait;
}
if (!this.filmstrip.fHidden)
{
thumbnail = new Thumbnail(null, this.config.strings.RES_SlideViewer_Loading);
this.InitItem(thumbnail, sd, fSmartImage, status);
this.thumbnails.Add(thumbnail);
}
slideView = new SlideView(null, sd.strTitle, sd.strDetails, strText, sd.strFrameSrc);
if (sd.strFrameSrc)
{
this.InitItem(slideView, sd, false, LOADED);
}
else
{
this.InitItem(slideView, sd, fSmartImage, status);
}
this.slideViews.Add(slideView);
}
this.logic.AdjustPreviousNextButtons();
}
this.layout.controls.imageSlider.SetValueRange(0, this.data.slides.length - 1);
}
ViewerLogic.DataHandler.prototype.InitItem = function(p_item, p_slideData, p_fCreateSmartImage, p_status)
{
p_item.slideData = p_slideData;
p_item.fSmartImage = p_fCreateSmartImage;
if (p_fCreateSmartImage)
{
if (p_status == OBTAINABLE)
{
this.iPendingLoads++;
}
else if (p_status == INITIALIZED)
{
this.iPendingCreates++;
}
}
p_item.fSuccess = !p_fCreateSmartImage;
p_item.status = p_status;
}
ViewerLogic.DataHandler.prototype.DetachDataFromItem = function(p_item)
{
if (p_item.smartImage)
{
Dispose(p_item.smartImage);
delete(p_item.smartImage);
}
}
ViewerLogic.DataHandler.prototype.AttachDataToItem = function(p_item)
{
var slideData = p_item.slideData;
var smartImage = new SmartImageControl();
p_item.smartImage = smartImage;
if (smartImage)
{
if (slideData.images.length > 0)
{
var iImageIndex = 0;
if (p_item.constructor == SlideView)
{
var iCount = slideData.images.length;
iImageIndex = (iCount > 1) ? (this.logic.fFullscreen ? (iCount - 1) : 1) : 0;
}
var sd = slideData.images[iImageIndex];
smartImage.img.width = sd.iWidth;
smartImage.img.height = sd.iHeight;
if (AnyWithValues(sd.iTop, sd.iRight, sd.iBottom, sd.iLeft))
{
var oClip = new Data.Rectangle(sd.iTop, sd.iRight, sd.iBottom, sd.iLeft);
DomStyles.SetClipRectangle(smartImage.img, oClip);
}
if (!!sd.strSrc)
{
smartImage.SetSrc(sd.strSrc);
}
}
}
}
ViewerLogic.DataHandler.prototype.SetSlideData = function(p_iIndex, p_iThumbWidth, p_iThumbHeight, p_iViewWidth, p_iViewHeight)
{
var slideView = this.slideViews.array[p_iIndex];
if (arguments.length == 1)
{
this.IndexFailed(p_iIndex);
}
else
{
var thumbImageData = new ViewerData.Slide.Image(null, p_iThumbWidth, p_iThumbHeight);
var slideImageData = new ViewerData.Slide.Image(null, p_iViewWidth, p_iViewHeight);
slideView.slideData.images = new Array(thumbImageData, slideImageData);
}
}
ViewerLogic.DataHandler.prototype.CreateNext = function()
{
if ((this.iPendingCreates > 0) && (!this.fCreating))
{
var item = this.FindNextItemByStatus(INITIALIZED, true, true);
if (item)
{
this.fCreating = true;
var iIndex = Collection.IndexOf(item);
var slideData = this.data.slides.array[iIndex];
ViewerLogic.DataHandler.slideCreating = slideData;
var slideView = this.slideViews.array[iIndex];
slideView.status = CREATING;
this.iPendingCreates--;
if (!this.filmstrip.fHidden)
{
var thumbnail = this.thumbnails.array[iIndex];
thumbnail.status = CREATING;
this.iPendingCreates--;
}
this.logic.log.Append("Creating Image: " + Collection.IndexOf(item));
this.imageCreator.Create(slideData.rawFile.Path, this.Method_("CreateDone"));
}
}
}
ViewerLogic.DataHandler.prototype.CreateDone = function(p_aCreatedFiles)
{
var slideData = ViewerLogic.DataHandler.slideCreating;
ViewerLogic.DataHandler.slideCreating = null;
var iIndex = Collection.IndexOf(slideData);
this.logic.log.Append("CreateDone: " + iIndex);
var thumbnail = this.filmstrip.fHidden ? null : this.thumbnails.array[iIndex];
var slideView = this.slideViews.array[iIndex];
if (p_aCreatedFiles)
{
this.logic.log.Append("Create Success: " + iIndex);
var thumbnailFile = p_aCreatedFiles[0];
var slideViewFile = p_aCreatedFiles[1];
var thumbImageData = new ViewerData.Slide.Image(thumbnailFile.Path, thumbnailFile.Width, thumbnailFile.Height);
var slideImageData = new ViewerData.Slide.Image(slideViewFile.Path, slideViewFile.Width, slideViewFile.Height);
slideData.images = new Array(thumbImageData, slideImageData);
if (thumbnail)
{
thumbnail.file = thumbnailFile;
}
slideView.file = slideViewFile;
this.messages.Send(Function.BuildCallAsString(
"SetSlideData",
iIndex,
thumbnailFile.Width,
thumbnailFile.Height,
slideViewFile.Width,
slideViewFile.Height
));
if (this.logic.messenger && this.logic.messenger.channel.fRemoteAppAvailable)
{
if (thumbnail)
{
thumbnail.status = CREATED;
this.iPendingObtains++;
}
slideView.status = CREATED;
this.iPendingObtains++;
SafeSetTimeout(this.Method_("ObtainNext"), 1);
}
else
{
if (thumbnail)
{
thumbnail.status = OBTAINABLE;
this.iPendingLoads++;
}
slideView.status = OBTAINABLE;
this.iPendingLoads++;
SafeSetTimeout(this.Method_("LoadNext"), 1);
}
}
else
{
this.logic.log.Append("Create Failure: " + iIndex);
this.messages.Send(Function.BuildCallAsString("SetSlideData", iIndex));
this.IndexFailed(iIndex);
SafeSetTimeout(this.Method_("CreateNext"), 1);
}
this.fCreating = false;
SafeSetTimeout(this.Method_("CreateNext"), 1);
}
ViewerLogic.DataHandler.prototype.ItemReceivingNotification = function(p_iIndex, p_iType)
{
this.logic.log.Append("ItemReceivingNotification: " + p_iIndex + ", type: " + p_iType);
var a = (p_iType == THUMBNAIL) ? this.thumbnails.array : this.slideViews.array;
this.itemReceiving = a[p_iIndex];
this.messages.Send(Function.BuildCallAsString("SendPendingFile"));
}
ViewerLogic.DataHandler.prototype.ReceiveFile = function(p_file)
{
var item = this.itemReceiving;
this.logic.log.Append("File Recieved: " + Collection.IndexOf(item));
this.itemReceiving = null;
var imageData = item.slideData.images[(item.constructor == Thumbnail) ? 0 : 1];
imageData.strSrc = p_file.Path;
item.status = OBTAINABLE;
this.messages.Send(Function.BuildCallAsString("ObtainDone"));
this.iPendingLoads++;
this.LoadNext();
}
ViewerLogic.DataHandler.prototype.ObtainNext = function()
{
if ((this.iPendingObtains > 0) && (!this.fObtaining))
{
var item = this.FindNextItemByStatus(CREATED, true, true);
if (item)
{
var iIndex = Collection.IndexOf(item);
this.fObtaining = true;
this.iPendingObtains--;
item.status = OBTAINING;
this.itemSending = item;
var iItemType = (item.constructor == Thumbnail) ? THUMBNAIL : SLIDEVIEW;
this.logic.log.Append("ObtainNext: " + iIndex + ", type: " + this.GetItemType(item));
this.messages.Send(Function.BuildCallAsString("ItemReceivingNotification", iIndex, iItemType));
}
}
}
ViewerLogic.DataHandler.prototype.GetItemType = function(p_item)
{
return((p_item.constructor == Thumbnail) ? "T" : "S");
}
ViewerLogic.DataHandler.prototype.SendPendingFile = function()
{
this.logic.log.Append("Sending file: " + Collection.IndexOf(this.itemSending));
this.messages.SendFile(this.itemSending.file);
}
ViewerLogic.DataHandler.prototype.ObtainDone = function()
{
var item = this.itemSending;
this.logic.log.Append("File Arrived: " + Collection.IndexOf(item));
this.itemSending = null;
this.fObtaining = false;
this.iPendingLoads++;
item.status = OBTAINABLE;
SafeSetTimeout(this.Method_("LoadNext"), 1);
SafeSetTimeout(this.Method_("ObtainNext"), 1);
}
ViewerLogic.DataHandler.prototype.LoadNext = function()
{
if (this.iNumLoading < this.iMaxLoading)
{
var groupToLoad = this.FindNextSlideGroupToLoad(true);
var item = null;
if (!groupToLoad)
{
groupToLoad = this.FindNextSlideGroupToLoad(false);
if (this.iPendingLoads > 0)
{
item = this.FindNextItemByStatus(OBTAINABLE, !groupToLoad, false);
}
}
if (item)
{
this.iPendingLoads--;
if (item.fSmartImage)
{
this.iNumLoading++;
item.status = LOADING;
this.AttachDataToItem(item);
item.smartImage.item = item;
this.logic.log.Append("Loading: " + Collection.IndexOf(item) + ", type: " + this.GetItemType(item));
item.smartImage.Load(this.Method_("LoadDone"));
}
}
else if (groupToLoad)
{
this.LoadDataXml(groupToLoad);
}
}
}
ViewerLogic.DataHandler.prototype.FindNextSlideGroupToLoad = function(p_fOnlyCurrent)
{
var iIndex = this.focalSlide ? Collection.IndexOf(this.focalSlide) : 0;
var group = this.data.GetSlideGroup(iIndex);
if (group)
{
if (p_fOnlyCurrent)
{
return(group.status == OBTAINABLE ? group : null);
}
var iGroupIndex = Collection.IndexOf(group);
for (var i=iGroupIndex; i < this.data.slideGroups.array.length; i++)
{
group = this.data.slideGroups.array[i];
if (group.status == OBTAINABLE)
{
return(group);
}
}
for (var i=iGroupIndex - 1; i >= 0; i--)
{
group = this.data.slideGroups.array[i];
if (group.status == OBTAINABLE)
{
return(group);
}
}
}
return(null);
}
ViewerLogic.DataHandler.prototype.LoadDataXml = function(p_slideGroup)
{
if (p_slideGroup && (!this.slideGroupLoading) && (p_slideGroup.status == OBTAINABLE))
{
this.iNumLoading++;
this.slideGroupLoading = p_slideGroup;
p_slideGroup.status == LOADING;
LoadXml(p_slideGroup.strSrc, this.Method_("DataXmlLoaded"));
}
}
ViewerLogic.DataHandler.prototype.DataXmlLoaded = function(p_xmlDoc)
{
this.iNumLoading--;
var group = this.slideGroupLoading;
this.slideGroupLoading = null;
var fSuccess = !!p_xmlDoc;
group.LoadXml(fSuccess ? p_xmlDoc.documentElement : null, this.data.slides);
var strStatus = fSuccess ? OBTAINABLE : LOADED;
var slideViewPending = null;
var iFocalSlideIndex = Collection.IndexOf(this.focalSlide);
for (var i=group.iStart; i <= group.iEnd; i++)
{
this.slideViews.array[i].status = strStatus;
this.iPendingLoads++;
if (!this.filmstrip.fHidden)
{
this.thumbnails.array[i].status = strStatus;
this.iPendingLoads++;
}
if (!fSuccess)
{
if (iFocalSlideIndex == i)
{
slideViewPending = this.slideViews.array[i];
}
this.IndexFailed(i);
}
}
if (slideViewPending)
{
this.fncLoadCallback(slideViewPending, false);
}
SafeSetTimeout(this.Method_("LoadNext"), 1);
}
ViewerLogic.DataHandler.prototype.LoadDone = function(p_smartImage)
{
var item = p_smartImage.item;
this.logic.log.Append("LoadDone: " + Collection.IndexOf(item) + ", type: " + this.GetItemType(item));
delete(p_smartImage.item);
this.iNumLoading--;
item.status = LOADED;
item.fSuccess = item.smartImage.fSuccess;
if (item.fSuccess)
{
if (item.constructor == Thumbnail)
{
item.Update(item.smartImage.img, null);
}
else if (item.constructor == SlideView)
{
item.Update(item.smartImage.img, item.slideData.strTitle, item.slideData.strDetails, null);
}
}
else
{
this.ItemFailed(item);
}
if (this.fncLoadCallback)
{
this.fncLoadCallback(item, item.fSuccess);
}
if (this.fFastPass)
{
this.Activate();
}
else
{
SafeSetTimeout(this.Method_("LoadNext"), 1);
}
}
ViewerLogic.DataHandler.prototype.IndexFailed = function(p_index)
{
this.ItemFailed(this.slideViews.array[p_index]);
if (!this.filmstrip.fHidden)
{
this.ItemFailed(this.thumbnails.array[p_index]);
}
}
ViewerLogic.DataHandler.prototype.ItemFailed = function(p_item)
{
this.logic.log.Append("ItemFailed: " + Collection.IndexOf(p_item) + ", type: " + this.GetItemType(p_item));
p_item.status = LOADED;
p_item.fSmartImage = false;
p_item.Update();
}
ViewerLogic.DataHandler.prototype.UnloadItem = function(p_item)
{
if (p_item.status == LOADED)
{
if (p_item.smartImage)
{
this.logic.log.Append("UnloadItem: " + Collection.IndexOf(p_item) + ", type: " + this.GetItemType(p_item));
p_item.status = OBTAINABLE;
p_item.fSuccess = false;
this.iPendingLoads++;
this.DetachDataFromItem(p_item);
if (p_item.constructor == Thumbnail)
{
p_item.Update(null, null);
}
else if (p_item.constructor == SlideView)
{
p_item.Update(null, p_item.strTitle, p_item.strDetails, null);
}
}
}
}
ViewerLogic.DataHandler.prototype.FindNextItemByStatus = function(p_status, p_fScanAllThumbnails, p_fScanAllSlideViews)
{
var item = null;
var iIndex = this.focalSlide ? Collection.IndexOf(this.focalSlide) : 0;
var aThumbs = this.thumbnails.array;
var aViews = this.slideViews.array;
if ((!this.filmstrip.fHidden) && (aThumbs[iIndex].status == p_status))
{
return(aThumbs[iIndex]);
}
else if (aViews[iIndex].status == p_status)
{
return(aViews[iIndex]);
}
if (this.fFastPass)
{
return(null);
}
var iLeftRange = iIndex - (this.filmstrip.fHidden ? 5 : this.filmstrip.iLeftThumbsVisible);
var iRightRange = iIndex + (this.filmstrip.fHidden ? 5 : this.filmstrip.iRightThumbsVisible);
var iLeftSlide = iIndex - (this.logic.fFullscreen ? this.iPreSlide : this.filmstrip.iLeftThumbsVisible);
var iRightSlide = iIndex + (this.logic.fFullscreen ? this.iPostSlide : this.filmstrip.iRightThumbsVisible);
if (!this.filmstrip.fHidden)
{
if (!item)
{
item = this.FindNextItem(aThumbs, p_status, iIndex + 1, iRightRange);
}
if (!item)
{
item = this.FindNextItem(aThumbs, p_status, iIndex - 1, iLeftRange);
}
}
/*
if (!item)
{
item = this.FindNextItem(aViews, p_status, iIndex + 1, iRightSlide);
}
if (!item)
{
item = this.FindNextItem(aViews, p_status, iIndex - 1, iLeftSlide);
}
if ((!this.filmstrip.fHidden) && p_fScanAllThumbnails)
{
if (!item)
{
item = this.FindNextItem(aThumbs, p_status, iRightRange + 1, aThumbs.length - 1);
}
if (!item)
{
item = this.FindNextItem(aThumbs, p_status, iLeftRange - 1, 0);
}
}
if (p_fScanAllSlideViews)
{
if (!item)
{
item = this.FindNextItem(aViews, p_status, iRightSlide + 1, aViews.length - 1);
}
if (!item)
{
item = this.FindNextItem(aViews, p_status, iLeftSlide - 1, 0);
}
}
*/
return(item);
}
ViewerLogic.DataHandler.prototype.FindNextItem = function(p_a, p_status, p_iStart, p_iEnd)
{
if (p_iStart <= p_iEnd)
{
p_iStart = Math.max(0, p_iStart);
for (var i=p_iStart; (i <= p_iEnd) && (i < p_a.length); i++)
{
if (p_a[i].status == p_status)
{
return(p_a[i]);
}
}
}
else
{
p_iStart = Math.min(p_a.length-1, p_iStart);
for (var i=p_iStart; (i >= p_iEnd) && (i >= 0); i--)
{
if (p_a[i].status == p_status)
{
return(p_a[i]);
}
}
}
}
ViewerLogic.DataHandler.prototype.SetLoadFocus = function(p_iIndex)
{
var iLastIndex = Collection.IndexOf(this.focalSlide);
this.focalSlide = this.data.slides.array[p_iIndex];
var iLeftSlide = p_iIndex - (this.logic.fFullscreen ? this.iPreSlide : this.filmstrip.iLeftThumbsVisible);
var iRightSlide = p_iIndex + (this.logic.fFullscreen ? this.iPostSlide : this.filmstrip.iRightThumbsVisible);
for (var i=0; i < this.slideViews.array.length; i++)
{
if ((i < iLeftSlide) && (i > iRightSlide) && (i != iLastIndex))
{
this.UnloadItem(this.slideViews.array[i]);
}
}
var thumb = this.filmstrip.fHidden ? null : this.thumbnails.array[p_iIndex];
var view = this.slideViews.array[p_iIndex];
var thumbStatus = thumb ? thumb.status : LOADED;
var viewStatus = view.status;
this.fFastPass = true;
if ((thumbStatus == UNINITIALIZED) || (viewStatus == UNINITIALIZED))
{
if (this.logic.messenger)
{
if (this.logic.messenger.channel.fRemoteAppAvailable)
{
this.messages.Send(Function.BuildCallAsString("SetLoadFocus", p_iIndex));
}
}
else
{
this.LoadDataXml(this.data.GetSlideGroup(p_iIndex));
}
}
else if ((thumbStatus == INITIALIZED) || (viewStatus == INITIALIZED))
{
this.CreateNext();
}
else if ((thumbStatus == CREATED) || (viewStatus == CREATED))
{
this.ObtainNext();
}
else if ((thumbStatus == OBTAINABLE) || (viewStatus == OBTAINABLE))
{
this.LoadNext();
}
else
{
this.Activate();
}
}
ViewerLogic.DataHandler.prototype.Activate = function()
{
this.fFastPass = false;
this.CreateNext();
this.ObtainNext();
this.LoadNext();
}
ViewerLogic.DataHandler.ImageCreator = function(p_log)
{
this.log = p_log;
this.strCurrentPath = null;
this.fncCallback = null;
this.fAttachedEvent = false;
}
ViewerLogic.DataHandler.ImageCreator.prototype.Create = function(p_strPath, p_fncCallback)
{
if (!p_strPath)
{
this.iAttempts++;
p_strPath = this.strCurrentPath;
p_fncCallback = this.fncCallback;
}
else if (!this.strCurrentPath)
{
this.iAttempts = 0;
}
else
{
this.CreateFailed(p_fncCallback);
return;
}
if (this.iAttempts == 3)
{
this.CreateFailed(p_fncCallback);
return;
}
this.strCurrentPath = p_strPath;
this.fncCallback = p_fncCallback;
var ctrlConverter = GetActiveXControl.ctrl;
var aWidths = CreateSafeArray(96, 500);
var aHeights = CreateSafeArray(96, 500);
var aSizes = CreateSafeArray(4000, 40000);
if (!this.fAttachedEvent)
{
this.fAttachedEvent = true;
ctrlConverter.attachEvent("OnFileConverted", this.Method_("CreateComplete"));
ctrlConverter.attachEvent("OnConvertFailed", this.Method_("Create"));
}
try
{
ctrlConverter.ConvertFile(p_strPath, aWidths, aHeights, aSizes);
}
catch(e)
{
this.log.Append("ConvertFile method failed:" + e);
this.CreateFailed(p_fncCallback);
}
}
ViewerLogic.DataHandler.ImageCreator.prototype.CreateComplete = function()
{
var aSafeArrayFileInfoConverted = GetActiveXControl.ctrl.GetConvertedFiles()
this.strCurrentPath = null;
var fnc = this.fncCallback;
this.fncCallback = null;
var aCreatedFiles = null;
if (aSafeArrayFileInfoConverted != null)
{
try
{
aCreatedFiles = (new VBArray(aSafeArrayFileInfoConverted)).toArray();
}
catch(e)
{
aCreatedFiles = null;
}
}
fnc(aCreatedFiles);
}
ViewerLogic.DataHandler.ImageCreator.prototype.CreateFailed = function(p_fncCallback)
{
this.iAttempts = 0;
this.strCurrentPath = null;
this.fncCallback = null;
p_fncCallback(null);
}
ViewerData = function(p_xml)
{
this.fLocalMode = Convert.ToBool(p_xml.getAttribute("localMode"));
this.player = new ViewerData.Player(p_xml.selectSingleNode("player"));
this.slides = new Collection();
this.slideGroups = new Collection();
var nodeSlideData = p_xml.selectSingleNode("slideData");
if (nodeSlideData)
{
for (var i=0; i < nodeSlideData.childNodes.length; i++)
{
this.AddSlideGroup(nodeSlideData.childNodes[i]);
}
}
else
{
var nodeSlides = p_xml.selectSingleNode("slides");
if (nodeSlides)
{
this.AddSlideGroup(nodeSlides);
}
}
}
ViewerData.prototype.AddSlideGroup = function(p_slideXml)
{
var slideGroup = new ViewerData.SlideGroup(p_slideXml);
this.slideGroups.Add(slideGroup);
var iSpecifiedDataCount = slideGroup.fContainsData ? p_slideXml.childNodes.length : 0;
for (var i=0; i < slideGroup.iCount; i++)
{
if (i < iSpecifiedDataCount)
{
this.slides.Add(new ViewerData.Slide(p_slideXml.childNodes[i]));
}
else
{
this.slides.Add(ViewerData.Slide.CreateAsEmpty());
}
}
}
ViewerData.prototype.GetSlideGroup = function(p_index)
{
for (var i=0; i < this.slideGroups.array.length; i++)
{
if (this.slideGroups.array[i].ContainsIndex(p_index))
{
return(this.slideGroups.array[i]);
}
}
return(null);
}
ViewerData.Player = function(p_xml)
{
this.fPlay = Convert.ToBool(p_xml.getAttribute("play"));
this.fLoop = Convert.ToBool(p_xml.getAttribute("loop"));
this.iStartIndex = parseInt(p_xml.getAttribute("startIndex"));
var nodeSpeed = p_xml.selectSingleNode("speed");
this.speed = new Object();
if (nodeSpeed)
{
this.speed.iMin = parseInt(nodeSpeed.getAttribute("min"));
this.speed.iMax = parseInt(nodeSpeed.getAttribute("max"));
this.speed.iDefault = parseInt(nodeSpeed.getAttribute("default"));
}
else
{
this.speed.iMin = 1;
this.speed.iMax = 30;
this.speed.iDefault = 5;
}
var nodeSize = p_xml.selectSingleNode("size");
this.size = new Object();
this.size.iWidth = Number.ConvertToInt(nodeSize.getAttribute("width"));
this.size.iHeight = Number.ConvertToInt(nodeSize.getAttribute("height"));
this.size.fFullScreen = Convert.ToBool(nodeSize.getAttribute("fullScreen"));
}
ViewerData.SlideGroup = function(p_slidesNode)
{
this.iStart = Xml.IntAttribute(p_slidesNode, "start");
if (this.iStart == null)
{
this.iStart = 0;
this.iEnd = p_slidesNode.childNodes.length - 1;
}
else
{
this.iEnd = Xml.IntAttribute(p_slidesNode, "end");
}
this.iCount = this.iEnd - this.iStart + 1;
this.strSrc = p_slidesNode.getAttribute("src");
this.fContainsData = (p_slidesNode.childNodes.length > 0);
this.status = this.fContainsData ? LOADED : OBTAINABLE;
}
ViewerData.SlideGroup.prototype.ContainsIndex = function(p_i)
{
return((p_i >= this.iStart) && (p_i <= this.iEnd));
}
ViewerData.SlideGroup.prototype.LoadXml = function(p_xml, p_aSlideData)
{
if (p_xml)
{
var iCount = p_xml.childNodes.length;
for (var i=0; i < this.iCount; i++)
{
if (i < iCount)
{
var slideData = p_aSlideData.array[this.iStart + i];
slideData.constructor(p_xml.childNodes[i]);
}
}
}
this.status = LOADED;
}
ViewerData.Slide = function()
{
if (arguments.length == 1)
{
var xml = arguments[0];
this.strTitle = Xml.GetNodeText(xml.selectSingleNode("title"));
this.strDetails = Xml.GetNodeText(xml.selectSingleNode("details"));
this.strText = Xml.GetNodeText(xml.selectSingleNode("text"));
if (!this.strDetails)
{
this.strDetails = " ";
}
this.strFrameSrc = Xml.GetNodeText(xml.selectSingleNode("frameSrc"));
var nodeImages = xml.selectSingleNode("images");
var iNumImages = nodeImages ? nodeImages.childNodes.length : 0;
this.images = new Array(iNumImages);
if (iNumImages > 0)
{
for (var i=0; i < this.images.length; i++)
{
this.images[i] = new ViewerData.Slide.Image(nodeImages.childNodes[i]);
}
}
}
}
ViewerData.Slide.CreateWithFile = function(p_file)
{
var slide = new ViewerData.Slide();
slide.rawFile = p_file;
return(slide);
}
ViewerData.Slide.CreateAsEmpty = function()
{
var slide = new ViewerData.Slide();
slide.fIsEmpty = true;
return(slide);
}
ViewerData.Slide.Image = function(p_strSrc, p_iWidth, p_iHeight, p_iTop, p_iLeft)
{
if (arguments.length > 1)
{
this.strSrc = p_strSrc;
this.iWidth = p_iWidth;
this.iHeight = p_iHeight;
this.iTop = p_iTop;
this.iLeft = p_iLeft;
}
else
{
var xml = arguments[0];
this.iSize = Xml.IntAttribute(xml, "size");
this.iWidth = parseInt(xml.getAttribute("width"));
this.iHeight = parseInt(xml.getAttribute("height"));
this.iTop = Xml.IntAttribute(xml, "top");
this.iRight = Xml.IntAttribute(xml, "right");
this.iBottom = Xml.IntAttribute(xml, "bottom");
this.iLeft = Xml.IntAttribute(xml, "left");
this.strSrc = xml.text;
}
}
function Slider(
p_fIsUpDown,
p_fDiscreteValues,
p_fShowValues,
p_strToolTip,
p_imgHandle,
p_imgBackground,
p_imgMinEnd,
p_imgMaxEnd,
p_imgUsed,
p_imgUnused
)
{
this.fIsUpDown = p_fIsUpDown;
this.fDiscreteValues = p_fDiscreteValues;
this.fShowValues = p_fShowValues;
this.strToolTip = p_strToolTip;
this.imgHandle = p_imgHandle;
this.imgBackground = p_imgBackground;
this.imgMinEnd = p_imgMinEnd;
this.imgMaxEnd = p_imgMaxEnd;
this.imgUsed = p_imgUsed;
this.imgUnused = p_imgUnused;
this.flValue = 0;
this.flMinValue = 0;
this.flMaxValue = 100;
this.fncValueChangedCallback = null;
this.fncMouseDownCallback = null;
this.fncMouseMoveCallback = null;
this.onMouseUpCallback = new Event(this);
this.fIsSliderStatic = this.imgBackground ? true : false;
this.fncMouseDownHandler = this.Method_("MouseDownHandler");
this.fncMouseMoveHandler = this.Method_("MouseMoveHandler");
this.fncMouseUpHandler = this.Method_("MouseUpHandler");
this.astrCaptions = new Array();
this.fInitialized = false;
this.fReady = false;
this.iNumHelperImagesNeeded = 0;
this.iNumHelperImagesLoaded = 0;
this.divSlider;
this.iMinPixelIndex = 0;
this.iMaxPixelIndex;
this.iSliderScreenPos;
this.fMouseDownOnSlider = false;
this.fMouseMoved = false;
this.fDim = false;
this.fEnabled = true;
this.size = new Data.Size(0, 0);
this.reserveSize = new Data.Size(0, 0);
this.CreateElements();
this.eState = Slider.NORMAL;
}
Slider.NORMAL = 1;
Slider.DOWN = 2;
Slider.DRAGGING = 3;
Slider.prototype.Dispose = function()
{
delete(this.imgHandle);
delete(this.imgBackground);
delete(this.imgMinEnd);
delete(this.imgMaxEnd);
delete(this.imgUsed);
delete(this.imgUnused);
}
Slider.prototype.CreateElements = function()
{
var fncImage = DomImage.Standardize;
fncImage(this.imgHandle);
fncImage(this.imgBackground);
fncImage(this.imgMinEnd);
fncImage(this.imgMaxEnd);
fncImage(this.imgUsed);
fncImage(this.imgUnused);
this.divSlider = DomElement.CreateElementWithFilter("div",
"Gray(enabled=0) alpha(Opacity=50,enabled=0)",
"progid:DXImageTransform.Microsoft.BasicImage(grayScale=1,enabled=false) progid:DXImageTransform.Microsoft.Alpha(Opacity=50,enabled=false)"
);
if (this.strToolTip)
{
this.divSlider.title = this.strToolTip;
}
this.element = this.divSlider;
var imgFixedReference = this.fIsSliderStatic ? this.imgBackground : this.imgUsed;
var oDivSliderWidth;
var oDivSliderHeight;
if (this.fIsUpDown)
{
this.size.width = imgFixedReference.iWidth;
this.size.height = (this.fIsSliderStatic) ? this.imgBackground.iHeight : 1;
}
else
{
this.size.height = imgFixedReference.iHeight;
this.size.width = (this.fIsSliderStatic) ? this.imgBackground.iWidth : 1;
}
this.reserveSize.width = this.size.width;
this.reserveSize.height = this.size.height;
this.divSlider.attachEvent("onmousedown", this.fncMouseDownHandler);
DomElement.SetStyles(this.divSlider,
"overflow", "hidden",
"width", this.size.width,
"height", this.size.height,
"cursor", "hand"
);
DomElement.SetStyles(this.imgHandle,
"position", "absolute",
"cursor", "hand",
"zIndex", 4
);
if (this.fIsUpDown)
{
this.imgHandle.style.left = Math.round((this.divSlider.style.pixelWidth - this.imgHandle.iWidth) / 2) - this.imgHandle.oClip.iLeft;
}
else
{
this.imgHandle.style.top = Math.round((this.divSlider.style.pixelHeight - this.imgHandle.iHeight) / 2) - this.imgHandle.oClip.iTop;
}
this.divSlider.appendChild(this.imgHandle);
if (this.fIsSliderStatic)
{
this.imgBackground.style.zIndex = 2;
this.divSlider.appendChild(this.imgBackground);
if (this.fIsUpDown)
{
this.imgBackground.style.left = Math.round((this.divSlider.style.pixelWidth - this.imgBackground.iWidth) / 2) - this.imgBackground.oClip.iLeft;
}
else
{
this.imgBackground.style.top = Math.round((this.divSlider.style.pixelHeight - this.imgBackground.iHeight) / 2) - this.imgBackground.oClip.iTop;
}
this.SetAbsoluteImagePlacement(this.imgBackground, 0);
}
else
{
var strProp = this.fIsUpDown ? "iHeight" : "iWidth";
this.AddImageToSlider(this.imgMinEnd);
this.AddImageToSlider(this.imgUnused);
this.AddImageToSlider(this.imgUsed);
this.AddImageToSlider(this.imgMaxEnd);
if (this.fIsUpDown)
{
this.SetAbsoluteImagePlacement(this.imgMaxEnd, 0);
this.SetAbsoluteImagePlacement(this.imgUsed, this.imgMaxEnd.iHeight);
this.SetAbsoluteImagePlacement(this.imgUnused, this.imgMaxEnd.iHeight);
this.imgUnused.style.zIndex = 3;
}
else
{
this.SetAbsoluteImagePlacement(this.imgMinEnd, 0);
this.SetAbsoluteImagePlacement(this.imgUnused, this.imgMinEnd.iWidth);
this.SetAbsoluteImagePlacement(this.imgUsed, this.imgMinEnd.iWidth);
this.imgUsed.style.zIndex = 3;
}
}
}
Slider.prototype.AddImageToSlider = function(p_img)
{
DomElement.SetStyles(p_img,
"position", "absolute",
"zIndex", 2
);
if (this.fIsUpDown)
{
p_img.pixelLeft = 0 - p_img.oClip.iLeft;
}
else
{
p_img.pixelTop = 0 - p_img.oClip.iTop;
}
this.divSlider.appendChild(p_img);
}
Slider.prototype.SetAbsoluteImagePlacement = function(p_img, p_iPixelIndex)
{
if (this.fIsUpDown)
{
p_img.style.pixelTop = p_iPixelIndex - p_img.oClip.iTop;
}
else
{
p_img.style.pixelLeft = p_iPixelIndex - p_img.oClip.iLeft;
}
}
Slider.prototype.GetValue = function()
{
return(this.flValue);
}
Slider.prototype.SetValue = function(p_flValue)
{
if (this.IsValidValue(p_flValue))
{
this.flValue = p_flValue;
this.Refresh();
this.ValueNotification();
}
}
Slider.prototype.SetValueRange = function(p_flMinValue, p_flMaxValue)
{
this.flMinValue = p_flMinValue;
this.flMaxValue = p_flMaxValue;
this.Refresh();
}
Slider.prototype.SetDimmer = function(p_fDim)
{
this.fDim = p_fDim;
this.divSlider.filters.item(1).Enabled = (p_fDim || !this.fEnabled);
}
Slider.prototype.SetEnabled = function(p_fEnabled)
{
this.fEnabled = p_fEnabled;
this.divSlider.filters.item(0).Enabled = !p_fEnabled;
this.divSlider.filters.item(1).Enabled = (this.fDim || !p_fEnabled);
this.divSlider.style.cursor = this.imgHandle.style.cursor = p_fEnabled ? "hand" : "default";
}
Slider.prototype.SetCaption = function(p_flValue, p_strCaption)
{
this.astrCaptions[p_flValue] = p_strCaption;
if (p_flValue == this.flValue)
{
this.imgHandle.title = p_strCaption;
}
}
Slider.prototype.Refresh = function()
{
this.MoveSliderToValue(this.flValue);
}
Slider.prototype.MoveSliderToPixelIndex = function(p_iPixelIndex)
{
p_iPixelIndex = Math.min(Math.max(p_iPixelIndex, this.iMinPixelIndex), this.iMaxPixelIndex);
var iNextPixelPos;
var iNextSliderPixelPos;
var strProperty;
if (this.fIsUpDown)
{
iNextPixelPos = this.divSlider.style.pixelHeight - this.imgHandle.iHeight - p_iPixelIndex;
iNextSliderPixelPos = iNextPixelPos - this.imgHandle.oClip.iTop;
strProperty = "pixelTop";
if (!this.fIsSliderStatic)
{
this.imgUnused.height = iNextPixelPos + Math.floor(this.imgHandle.iHeight / 2);
}
}
else
{
iNextPixelPos = p_iPixelIndex;
iNextSliderPixelPos = iNextPixelPos - this.imgHandle.oClip.iLeft;
strProperty = "pixelLeft";
if (!this.fIsSliderStatic)
{
this.imgUsed.width = iNextPixelPos + Math.floor(this.imgHandle.iWidth / 2);
}
}
if (this.imgHandle.style[strProperty] != iNextSliderPixelPos)
{
this.imgHandle.style[strProperty] = iNextSliderPixelPos;
}
}
Slider.prototype.MoveSliderToValue = function(p_flValue)
{
this.MoveSliderToPixelIndex(this.GetPixelIndexFromValue(p_flValue));
}
Slider.prototype.GetPixelIndexFromValue = function(p_flValue)
{
if (this.flMaxValue < this.flMinValue)
{
return(this.iMinPixelIndex);
}
else if (this.flMaxValue == this.flMinValue)
{
return(this.iMaxPixelIndex);
}
var flPercentage = ((p_flValue - this.flMinValue) / (this.flMaxValue - this.flMinValue));
flPercentage = Math.min(Math.max(0, flPercentage), 1);
var iPixelIndex = this.iMinPixelIndex + Math.round(flPercentage * (this.iMaxPixelIndex - this.iMinPixelIndex));
return(iPixelIndex);
}
Slider.prototype.GetValueFromPixelIndex = function(p_iPixelIndex)
{
if (this.iMaxPixelIndex == this.iMinPixelIndex)
{
return(this.flMinValue);
}
var flPercentage = ((p_iPixelIndex - this.iMinPixelIndex) / (this.iMaxPixelIndex - this.iMinPixelIndex));
var flValue = this.flMinValue + (flPercentage * (this.flMaxValue - this.flMinValue));
if (this.fDiscreteValues)
{
return(Math.round(flValue));
}
else
{
return(flValue);
}
}
Slider.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
p_iWidth = Math.max(1, p_iWidth);
p_iHeight = Math.max(1, p_iHeight);
if (this.fIsUpDown)
{
if (!this.fIsSliderStatic)
{
this.size.height = p_iHeight;
this.divSlider.style.pixelHeight = p_iHeight;
}
this.iMaxPixelIndex = this.divSlider.style.pixelHeight - this.imgHandle.iHeight;
}
else
{
if (!this.fIsSliderStatic)
{
this.size.width = p_iWidth;
this.divSlider.style.pixelWidth = p_iWidth;
}
this.iMaxPixelIndex = this.divSlider.style.pixelWidth - this.imgHandle.iWidth;
}
this.iMaxPixelIndex = Math.max(1, this.iMaxPixelIndex);
if (!this.fIsSliderStatic)
{
if (this.fIsUpDown)
{
this.imgUsed.height = this.imgUsed.iHeight = this.divSlider.style.pixelHeight - this.imgMaxEnd.iHeight - this.imgMinEnd.iHeight;
this.SetAbsoluteImagePlacement(this.imgMinEnd, this.imgMaxEnd.iHeight + this.imgUsed.iHeight);
}
else
{
this.imgUnused.width = this.imgUnused.iWidth = this.divSlider.style.pixelWidth - this.imgMinEnd.iWidth - this.imgMaxEnd.iWidth;
this.SetAbsoluteImagePlacement(this.imgMaxEnd, this.imgMinEnd.iWidth + this.imgUnused.iWidth);
}
}
this.Refresh();
}
Slider.FindContainingElementOffset = function(p_element, p_ancestorElement, p_strOffsetProperty)
{
var iOffset = 0;
while ((p_element != p_ancestorElement) && (p_ancestorElement.contains(p_element)))
{
iOffset += p_element[p_strOffsetProperty];
p_element = p_element.offsetParent;
}
return(iOffset);
}
Slider.prototype.MouseDownHandler = function()
{
if (!this.fEnabled)
{
return;
}
this.eState = Slider.DOWN;
Function.Invoke_(this.fncMouseDownCallback);
var iSrcElementOffsetFromSliderDiv = Slider.FindContainingElementOffset(window.event.srcElement, this.divSlider, this.fIsUpDown ? "offsetTop" : "offsetLeft");
var iEventOffset = (this.fIsUpDown) ? window.event.offsetY : window.event.offsetX;
var iPixelIndex = iSrcElementOffsetFromSliderDiv + iEventOffset;
var iEventScreenPos = (this.fIsUpDown) ? window.event.screenY : window.event.screenX;
this.iSliderScreenPos = iEventScreenPos - iPixelIndex;
if (window.event.srcElement == this.imgHandle)
{
this.fMouseDownOnSlider = true;
}
else
{
this.fMouseDownOnSlider = false;
this.UpdateFromMouseEvent();
}
this.imgHandle.setCapture();
this.imgHandle.attachEvent("onmousemove", this.fncMouseMoveHandler);
this.imgHandle.attachEvent("onmouseup", this.fncMouseUpHandler);
this.fMouseMoved = false;
}
Slider.prototype.MouseMoveHandler = function()
{
this.fMouseMoved = true;
this.eState = Slider.DRAGGING;
Function.Invoke_(this.fncMouseMoveCallback);
this.UpdateFromMouseEvent();
window.event.cancelBubble = true;
}
Slider.prototype.MouseUpHandler = function()
{
this.imgHandle.detachEvent("onmouseup", this.fncMouseUpHandler);
this.imgHandle.detachEvent("onmousemove", this.fncMouseMoveHandler);
this.imgHandle.releaseCapture();
if (this.fMouseMoved || (!this.fMouseDownOnSlider))
{
this.UpdateFromMouseEvent(this.fDiscreteValues);
}
this.fMouseMoved = false;
this.eState = Slider.NORMAL;
this.onMouseUpCallback.Fire();
}
Slider.prototype.GetPixelIndexFromMouseEvent = function()
{
var iIndex;
if (this.fIsUpDown)
{
iIndex = window.event.screenY - this.iSliderScreenPos - Math.floor(this.imgHandle.iHeight / 2);
iIndex = this.iMaxPixelIndex + this.iMinPixelIndex - iIndex;
}
else
{
iIndex = window.event.screenX - this.iSliderScreenPos - Math.floor(this.imgHandle.iWidth / 2);
}
iIndex = Math.min(this.iMaxPixelIndex, Math.max(this.iMinPixelIndex, iIndex));
return(iIndex);
}
Slider.prototype.UpdateFromMouseEvent = function(p_fSnapSlider)
{
var iPixelIndex = this.GetPixelIndexFromMouseEvent();
var flValue = this.GetValueFromPixelIndex(iPixelIndex);
if (p_fSnapSlider)
{
iPixelIndex = this.GetPixelIndexFromValue(flValue);
}
this.MoveSliderToPixelIndex(iPixelIndex);
this.UpdateValue(flValue);
}
Slider.prototype.UpdateValue = function(p_flValue)
{
if (this.flValue == p_flValue)
{
return;
}
if (this.IsValidValue(p_flValue))
{
this.flValue = p_flValue;
this.ValueNotification();
}
}
Slider.prototype.IsValidValue = function(p_flValue)
{
return((p_flValue >= this.flMinValue) && (p_flValue <= this.flMaxValue));
}
Slider.prototype.ValueNotification = function()
{
if (this.fncValueChangedCallback)
{
this.fncValueChangedCallback(this.flValue);
}
if (this.astrCaptions[this.flValue])
{
this.imgHandle.title = this.astrCaptions[this.flValue];
}
else if (this.fShowValues)
{
this.imgHandle.title = this.flValue;
}
}
function SlideDisplay(
p_fShowTitle,
p_fShowDetails,
p_strDefaultWindowTitle,
p_fncSlideDisplayedCallback
)
{
this.fShowTitle = p_fShowTitle;
this.fShowDetails = p_fShowDetails;
this.strDefaultWindowTitle = p_strDefaultWindowTitle;
this.fncSlideDisplayedCallback = p_fncSlideDisplayedCallback;
this.size = new Data.Size(0, 0);
var fnc = SlideDisplay.CreateFontFunction;
this.fncFontTitleSize = fnc(200*150, 75, 1280*960, 200, 50, 250);
this.fncFontDetailsSize = fnc(200*150, 50, 1280*960, 150, 65, 150);
this.fncFontMessageSize = fnc(80*60, 70, 1280*960, 500, 1, 500);
this.flMinImagePercentage = .6;
this.flTitleWidthPercentage = .9;
this.flDetailsWidthPercentage = .9;
this.iMarginSize = 0;
this.CreateElements();
this.oCurrentSlide = null;
}
SlideDisplay.CreateFontFunction = function(p_x1, p_y1, p_x2, p_y2, p_min, p_max)
{
return(Math2.RoundFunction(
Math2.MinMaxFunction(Math2.LinearFunction(p_x1, p_y1, p_x2, p_y2), p_min, p_max)
));
}
SlideDisplay.prototype.Dispose = function()
{
Dispose(this.ospanTitle);
Dispose(this.ospanDetails);
Dispose(this.ospanMessage);
}
SlideDisplay.prototype.CreateElements = function()
{
this.spanDisplay = DomElement.CreateElementWithFilter("div", "BlendTrans(duration=1)", "progid:DXImageTransform.Microsoft.Fade(duration=1,overlap=.5)");
this.spanDisplay.style.backgroundColor = "#FFFFFF";
this.spanDisplay.style.overflow = "hidden";
this.element = this.spanDisplay;
this.spanDisplay.onfilterchange = this.Method_("FilterChangeHandler");
this.ospanTitle = new CenteredSpan(null, null, null, null, true);
this.ospanTitle.element.className = "bold";
this.ospanDetails = new CenteredSpan(null, null, null, null);
this.ospanMessage = new CenteredSpan(null, null, null, null);
this.img = document.createElement("img");
this.img.style.position = "absolute";
this.img.style.border = "solid 1 #000000";
this.iframe = document.createElement("iframe");
this.iframe.style.position = "absolute";
this.iframe.style.visibility = "hidden";
DomElement.AppendChildren(this.spanDisplay,
this.ospanTitle.element,
this.ospanDetails.element,
this.img,
this.ospanMessage.element,
this.iframe
);
}
SlideDisplay.prototype.SetMarginSize = function(p_iSize)
{
this.iMarginSize = p_iSize;
this.AllocateSize(this.size.width, this.size.height);
}
SlideDisplay.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
p_iWidth = Math.max(1, p_iWidth);
p_iHeight = Math.max(1, p_iHeight);
this.spanDisplay.style.width = this.size.width = this.iframe.width = p_iWidth;
this.spanDisplay.style.height = this.size.height = this.iframe.height = p_iHeight;
this.iSideMargin = this.iMarginSize;
this.iAvailWidth = this.size.width - (2 * this.iSideMargin);
this.UpdateSpanWidthAndFont(this.ospanTitle, this.flTitleWidthPercentage, this.fncFontTitleSize(p_iWidth*p_iHeight)+"%");
this.UpdateSpanWidthAndFont(this.ospanDetails, this.flDetailsWidthPercentage, this.fncFontDetailsSize(p_iWidth*p_iHeight)+"%");
this.UpdateSpanWidthAndFont(this.ospanMessage, 1, this.fncFontMessageSize(p_iWidth*p_iHeight)+"%");
this.iTitleCenterLine = null;
this.iDetailsCenterLine = null;
this.iImageCenterLine = null;
this.Refresh();
}
SlideDisplay.prototype.UpdateSpanWidthAndFont = function(p_ospanText, p_flUsableWidthPercent, p_strFontSize)
{
var iWidth = Math.max(1, Math.round(p_flUsableWidthPercent * this.iAvailWidth));
p_ospanText.element.style.width = iWidth;
DomPositioning.HorizontallyAlign(p_ospanText.element, iWidth, this.iAvailWidth, this.iSideMargin);
p_ospanText.element.style.fontSize = p_strFontSize;
}
SlideDisplay.prototype.SetImageSlideVisibility = function(p_fVisible)
{
var str = p_fVisible ? "visible" : "hidden";
this.ospanDetails.element.style.visibility = str;
this.ospanTitle.element.style.visibility = str;
if (this.img)
{
this.img.style.visibility = str;
}
}
SlideDisplay.prototype.SetTextSlideVisibility = function(p_fVisible)
{
var str = p_fVisible ? "visible" : "hidden";
this.ospanMessage.element.style.visibility = str;
}
SlideDisplay.prototype.SetFrameSlideVisibility = function(p_fVisible)
{
this.iframe.style.visibility = p_fVisible ? "visible" : "hidden";
}
SlideDisplay.prototype.SetSlideVisibility = function(p_fVisible)
{
this.SetImageSlideVisibility(p_fVisible);
this.SetTextSlideVisibility(p_fVisible);
this.SetFrameSlideVisibility(p_fVisible);
}
SlideDisplay.prototype.SetSlide = function()
{
if (this.img)
{
this.spanDisplay.removeChild(this.img);
this.img = null;
}
var strTitle = "";
var strDetails = "";
if (this.oCurrentSlide)
{
if (this.oCurrentSlide.img)
{
this.img = this.oCurrentSlide.img;
this.img.style.position = "absolute";
this.img.style.border = "solid 1 #000000";
this.spanDisplay.appendChild(this.img);
}
strTitle = this.oCurrentSlide.strTitle.Encode_();
strDetails = this.oCurrentSlide.strDetails.Encode_();
}
this.ospanTitle.SetInnerText(strTitle);
this.ospanDetails.SetInnerText(strDetails);
this.Refresh();
}
SlideDisplay.prototype.Refresh = function()
{
if (!this.oCurrentSlide)
{
this.SetSlideVisibility(false);
return;
}
if (this.img)
{
this.SetImageSlideVisibility(true);
this.SetTextSlideVisibility(false);
this.SetFrameSlideVisibility(false);
}
else if (this.oCurrentSlide.strUrl)
{
this.SetImageSlideVisibility(false);
this.SetTextSlideVisibility(false);
this.SetFrameSlideVisibility(true);
if (this.iframe.src != this.oCurrentSlide.strUrl)
{
this.iframe.src = this.oCurrentSlide.strUrl;
}
return;
}
else
{
this.SetImageSlideVisibility(false);
this.SetTextSlideVisibility(true);
this.SetFrameSlideVisibility(false);
this.ospanMessage.element.style.height = this.size.height;
this.ospanMessage.SetInnerText(this.oCurrentSlide.strText.Encode_());
this.ospanMessage.Center();
return;
}
var iImageHeightAllotted = 0;
var iTitleHeightAllotted = 0;
var iDetailsHeightAllotted = 0;
var iHeightBudget = this.size.height;
var iTopMargin = this.iMarginSize;
var iBottomMargin = iTopMargin;
var iMarginCount = iTopMargin ? 2 : 0;
var iTopMarginNeeded = iTopMargin;
var iBottomMarginNeeded = iBottomMargin;
iHeightBudget -= (iBottomMargin + iTopMargin);
if (iHeightBudget < 0)
{
this.SetSlideVisibility(false);
return;
}
var scaledSize = (this.img) ? this.img.originalSize.GetContainedSize(new Data.Size(this.iAvailWidth, iHeightBudget)) : Data.Size.Empty;
var iImageHeightNeeded = scaledSize.height;
var iTitleHeightNeeded = (this.fShowTitle && (this.ospanTitle.GetInnerText() != "")) ? this.ospanTitle.element.clientHeight : 0;
var iDetailsHeightNeeded = (this.fShowDetails && (this.ospanDetails.GetInnerText() != "")) ? this.ospanDetails.element.clientHeight : 0;
var iImageHeightAllotted = Math.min(iImageHeightNeeded, Math.ceil(iHeightBudget * this.flMinImagePercentage));
iHeightBudget -= iImageHeightAllotted;
var iTitleMargin = 0;
var iTitleMarginNeeded = (iImageHeightAllotted > 0) ? this.iMarginSize : 0;
if ((iTitleHeightNeeded > 0) && (iTitleHeightNeeded + iTitleMarginNeeded <= iHeightBudget))
{
iTitleHeightAllotted = iTitleHeightNeeded;
iTitleMargin = iTitleMarginNeeded;
iHeightBudget -= (iTitleHeightAllotted + iTitleMargin);
}
iMarginCount++;
var iDetailsMargin = 0;
var iDetailsMarginNeeded = (iImageHeightAllotted + iTitleHeightAllotted > 0) ? this.iMarginSize : 0;
if ((iDetailsHeightNeeded > 0) && (iDetailsHeightNeeded + iDetailsMarginNeeded <= iHeightBudget))
{
iDetailsHeightAllotted = iDetailsHeightNeeded;
iDetailsMargin = iDetailsMarginNeeded;
iHeightBudget -= (iDetailsHeightNeeded + iDetailsMargin);
}
iMarginCount++;
iHeightBudget += iImageHeightAllotted;
iImageHeightAllotted = Math.min(iHeightBudget, iImageHeightNeeded);
iHeightBudget -= iImageHeightAllotted;
var iMarginSurplus = Math.floor(iHeightBudget / iMarginCount);
iTopMargin += (iTopMargin) ? iMarginSurplus : 0;
iBottomMargin += (iBottomMargin) ? iMarginSurplus : 0;
iTitleMargin += iMarginSurplus;
iDetailsMargin += iMarginSurplus;
iHeightBudget -= (iMarginSurplus * iMarginCount);
iImageHeightAllotted += iHeightBudget;
var iTopAllotted = iTopMargin + iTitleHeightAllotted + iTitleMargin;
var iBotAllotted = iDetailsMargin + iDetailsHeightAllotted + iBottomMargin;
var iTopNeeded = iTopMarginNeeded + iTitleHeightNeeded + iTitleMarginNeeded;
var iBotNeeded = iDetailsMarginNeeded + iDetailsHeightNeeded + iBottomMarginNeeded;
var iImageTop = iTopMargin + iTitleHeightAllotted + iTitleMargin;
var iImageCenterLine = iImageTop + Math.floor(iImageHeightAllotted / 2);
this.iImageCenterLine = this.GetCenterLine(
iImageCenterLine,
iTopAllotted,
iImageHeightAllotted,
iBotAllotted,
iTopNeeded,
iBotNeeded,
iImageTop,
this.iImageCenterLine
);
var iImageDelta = this.iImageCenterLine - iImageCenterLine;
var iTopBuffer = iTopAllotted + iImageDelta - iTitleHeightAllotted;
iTopMargin = iTopMargin ? Math.max(iTopMarginNeeded, Math.floor(iTopBuffer/2)) : 0;
iTitleMargin = iTopBuffer - iTopMargin;
var iBotBuffer = iBotAllotted - iImageDelta - iDetailsHeightAllotted;
iBottomMargin = iBottomMargin ? Math.max(iBottomMarginNeeded, Math.floor(iBotBuffer/2)) : 0;
iDetailsMargin = iBotBuffer - iBottomMargin;
var iTitleTop = iTopMargin;
var iTitleCenterLine = iTitleTop + Math.floor(iTitleHeightAllotted / 2);
this.iTitleCenterLine = this.GetCenterLine(
iTitleCenterLine,
iTopMargin,
iTitleHeightAllotted,
iTitleMargin,
iTopMarginNeeded,
iTitleMarginNeeded,
iTopMargin,
this.iTitleCenterLine
);
var iTitleDelta = this.iTitleCenterLine - iTitleCenterLine;
iTopMargin += iTitleDelta;
iTitleMargin -= iTitleDelta;
var iDetailsTop = iTopMargin + iTitleHeightAllotted + iTitleMargin + iImageHeightAllotted + iDetailsMargin;
var iDetailsCenterLine = iDetailsTop + Math.floor(iDetailsHeightAllotted / 2);
this.iDetailsCenterLine = this.GetCenterLine(
iDetailsCenterLine,
iDetailsMargin,
iDetailsHeightAllotted,
iBottomMargin,
iDetailsMarginNeeded,
iBottomMarginNeeded,
iDetailsTop,
this.iDetailsCenterLine
);
var iDetailsDelta = this.iDetailsCenterLine - iDetailsCenterLine;
iDetailsMargin += iDetailsDelta;
iBottomMargin -= iDetailsDelta;
if (this.img)
{
var fRatio = this.img.originalSize.GetScaledRatio(new Data.Size(this.iAvailWidth, iImageHeightAllotted));
this.img.size = this.img.originalSize;
this.img.oClip = this.img.originalClip;
DomImage.Scale(this.img, fRatio);
DomPositioning.VerticallyAlign(
this.img,
this.img.iHeight,
iImageHeightAllotted,
iTopMargin + iTitleHeightAllotted + iTitleMargin - this.img.oClip.iTop
);
this.img.style.left = Math.floor((this.iAvailWidth - this.img.iWidth) / 2) + this.iSideMargin - this.img.oClip.iLeft;
}
if ((iTitleHeightAllotted < iTitleHeightNeeded) || (!this.fShowTitle))
{
this.ospanTitle.element.style.visibility = "hidden";
}
else
{
this.ospanTitle.element.style.visibility = "visible";
this.ospanTitle.element.style.top = iTopMargin;
this.ospanTitle.Center();
}
if ((iDetailsHeightAllotted < iDetailsHeightNeeded) || (!this.fShowDetails))
{
this.ospanDetails.element.style.visibility = "hidden";
}
else
{
this.ospanDetails.element.style.visibility = "visible";
this.ospanDetails.element.style.top = iTopMargin + iTitleHeightAllotted + iTitleMargin + iImageHeightAllotted + iDetailsMargin;
this.ospanDetails.Center();
}
}
SlideDisplay.prototype.GetCenterLine = function(
p_iCenterLine,
p_iUpper,
p_iHeight,
p_iLower,
p_iUpperMin,
p_iLowerMin,
p_iTop,
p_iCurrent
)
{
if (p_iCurrent)
{
var iDelta = p_iCurrent - p_iCenterLine;
if (Math.abs(iDelta) <= (Math.floor((p_iUpper + p_iHeight + p_iLower) / 6)))
{
if ((p_iUpper + iDelta >= p_iUpperMin) && (p_iLower - iDelta >= p_iLowerMin))
{
return(p_iCurrent);
}
}
}
return(p_iCenterLine);
}
SlideDisplay.prototype.VerticallyCenterElement = function(p_element, p_iElementHeight, p_iRegionHeight, p_iOffset)
{
p_element.style.top = Math.max(0,((p_iRegionHeight - p_iElementHeight) / 2) + p_iOffset);
}
SlideDisplay.prototype.DisplayNotification = function()
{
if (this.fncSlideDisplayedCallback)
{
this.fncSlideDisplayedCallback(this.oCurrentSlide);
}
}
SlideDisplay.prototype.FilterChangeHandler = function()
{
if (this.spanDisplay.filters[0].status == 0)
{
if (this.oCurrentSlide === this.oSlideTransitioning)
{
this.DisplayNotification();
}
}
}
SlideDisplay.prototype.SetTextVisibility = function(p_fTitleVisible, p_fDetailsVisible)
{
this.fShowTitle = p_fTitleVisible;
this.fShowDetails = p_fDetailsVisible;
this.SetSlide();
}
SlideDisplay.prototype.DisplaySlide = function(p_oSlide, p_fDoTransition, p_fForceUpdate)
{
if ((this.oCurrentSlide == p_oSlide) && (!p_fForceUpdate))
{
this.DisplayNotification();
return;
}
this.oCurrentSlide = p_oSlide;
p_oSlide = p_oSlide ? p_oSlide : null;
if (p_oSlide && p_oSlide.strTitle)
{
window.document.title = p_oSlide.strTitle;
}
else
{
window.document.title = this.strDefaultWindowTitle;
}
if (p_fDoTransition && this.spanDisplay.filters[0])
{
if (this.spanDisplay.filters[0].status != 0)
{
var fncFilterChangeHandler = this.spanDisplay.onfilterchange;
this.spanDisplay.onfilterchange = null;
this.spanDisplay.filters[0].Stop();
this.spanDisplay.onfilterchange = fncFilterChangeHandler;
}
this.spanDisplay.filters[0].Apply();
}
this.SetSlide();
if (p_fDoTransition && this.spanDisplay.filters[0])
{
this.spanDisplay.filters[0].Play();
this.oSlideTransitioning = this.oCurrentSlide;
}
else
{
this.DisplayNotification();
}
}
function SlideView(
p_img,
p_strTitle,
p_strDetails,
p_strText,
p_strUrl
)
{
this.onChange = new Event(this);
this.Update(p_img, p_strTitle, p_strDetails, p_strText, p_strUrl);
}
SlideView.prototype.Dispose = function()
{
if (this.img)
{
this.img.src = "";
delete(this.img);
}
}
SlideView.prototype.Update = function(p_img, p_strTitle, p_strDetails, p_strText, p_strUrl)
{
this.img = p_img;
if (this.img && (!this.img.fOriginalInfoSaved))
{
DomImage.Standardize(this.img);
this.img.originalSize = this.img.size;
this.img.originalClip = this.img.oClip;
this.img.fOriginalInfoSaved = true;
}
this.strTitle = (p_strTitle) ? p_strTitle : "";
this.strDetails = (p_strDetails) ? p_strDetails : "";
this.strText = (p_strText) ? p_strText : "";
this.strUrl = (p_strUrl) ? p_strUrl : "";
this.onChange.Fire();
}
function FilmStrip(
p_sizeMaxImage,
p_sizeNode,
p_iTopBorderHeight,
p_iImageBorderSize,
p_strImageBorderColor,
p_strBackgroundColor,
p_strTrimColor,
p_fncIndexChangedCallback,
p_fncClickedCallback
)
{
this.sizeMaxImage = p_sizeMaxImage;
this.sizeNode = p_sizeNode;
this.strBackgroundColor = p_strBackgroundColor;
this.strTrimColor = p_strTrimColor;
this.strImageBorderColor = p_strImageBorderColor;
this.iImageBorderSize = p_iImageBorderSize;
this.fncIndexChangedCallback = p_fncIndexChangedCallback;
this.fncClickedCallback = p_fncClickedCallback;
this.fHidden = false;
this.onSizeChanged = new Event(this);
this.size = new Data.Size(0, this.sizeNode.height + p_iTopBorderHeight);
this.thumbnails = new Collection(this.Method_("ThumbnailsChangedHandler"));
this.spanCover = DomElement.CreateElementWithFilter("span",
"Gray(enabled=0) alpha(Opacity=65,enabled=1)",
"progid:DXImageTransform.Microsoft.BasicImage(grayScale=1,enabled=false) progid:DXImageTransform.Microsoft.Alpha(Opacity=65,enabled=true)"
);
DomElement.SetStyles(this.spanCover,
"position", "absolute",
"left", 0,
"height", this.size.height,
"backgroundColor", "#FFFFFF",
"zIndex", 10
);
this.divFilmstrip = DomElement.CreateElementWithFilter("div",
"Gray(enabled=0) alpha(Opacity=50,enabled=0)",
"progid:DXImageTransform.Microsoft.BasicImage(grayScale=1,enabled=false) progid:DXImageTransform.Microsoft.Alpha(Opacity=50,enabled=false)"
);
DomElement.SetStyles(this.divFilmstrip,
"overflow", "hidden",
"height", this.size.height,
"borderTop", p_iTopBorderHeight + " solid " + p_strTrimColor,
"backgroundColor", p_strBackgroundColor
);
this.element = this.divFilmstrip;
this.nobr = document.createElement("nobr");
this.nobr.style.position = "absolute";
this.spanLeftSpacer = document.createElement("<span style='visibility:hidden;'>");
this.spanRightSpacer = this.spanLeftSpacer.cloneNode();
this.spanNodes = document.createElement("<span style='position:relative;'>");
DomElement.AppendChildren(this.nobr, this.spanLeftSpacer, this.spanNodes, this.spanRightSpacer);
this.divFilmstrip.appendChild(this.nobr);
this.divFilmstrip.appendChild(this.spanCover);
this.iLeftThumbsVisible = 0;
this.iRightThumbsVisible = 0;
this.selectedNode = null;
this.fEnabled = true;
this.fDim = false;
}
FilmStrip.prototype.Dispose = function()
{
for (var i=this.spanNodes.childNodes.length - 1; i >= 0; i--)
{
Dispose(this.spanNodes.childNodes[i].oNode);
}
}
FilmStrip.prototype.Hide = function()
{
this.fHidden = true;
this.element.style.display = "none";
this.onSizeChanged.Fire();
}
FilmStrip.prototype.GetNode = function(p_iIndex)
{
var element = this.spanNodes.childNodes[p_iIndex];
if (element)
{
return(element.oNode);
}
else
{
return(null);
}
}
FilmStrip.prototype.Count = function()
{
return(this.spanNodes.childNodes.length);
}
FilmStrip.prototype.ThumbnailsChangedHandler = function(p_aAdded, p_aRemoved)
{
var nextNode = this.selectedNode;
if (p_aAdded)
{
var newNode;
var nodeInsertBefore = this.GetNode(p_aAdded.iStartIndex);
for (var i=0; i < p_aAdded.length; i++)
{
var newNode = new FilmStripNode(
p_aAdded[i],
this.sizeMaxImage,
this.sizeNode,
this.fEnabled,
this.strBackgroundColor,
this.strTrimColor,
this.strImageBorderColor,
this.iImageBorderSize,
this.Method_("NodeClickedHandler")
);
if (nodeInsertBefore)
{
this.spanNodes.insertBefore(newNode.element, nodeInsertBefore.element);
}
else
{
this.spanNodes.appendChild(newNode.element);
}
}
}
if (p_aRemoved)
{
var iFirstIndex = p_aRemoved.iStartIndex;
var iLastIndex = iFirstIndex + p_aRemoved.length - 1;
var iSelectedIndex = this.GetIndex();
if (Math2.IsBetween(iFirstIndex, iSelectedIndex, iLastIndex))
{
if (iLastIndex + 1 < this.Count())
{
nextNode = this.GetNode(iLastIndex + 1);
}
else if (iFirstIndex - 1 >= 0)
{
nextNode = this.GetNode(iFirstIndex - 1);
}
else
{
nextNode = null;
}
}
for (var i=0; i < p_aRemoved.length; i++)
{
this.spanNodes.removeChild(this.spanNodes.childNodes[i]);
}
}
this.spanCover.style.width = this.divFilmstrip.style.pixelWidth + (this.Count() * this.sizeNode.width);
}
FilmStrip.prototype.GetIndex = function()
{
if (this.fHidden)
{
return;
}
if (this.selectedNode)
{
return(this.selectedNode.IndexOf());
}
else
{
return(-1);
}
}
FilmStrip.prototype.SetIndex = function(p_iIndex, p_fShowMovement)
{
if (this.fHidden)
{
return;
}
this.SetSelectedNode(this.GetNode(p_iIndex), p_fShowMovement);
}
FilmStrip.prototype.SetDimmer = function(p_fDim)
{
if (this.fHidden)
{
return;
}
this.fDim = p_fDim;
this.spanCover.style.visibility = p_fDim ? "visible" : "hidden";
this.divFilmstrip.style.borderTopColor = (p_fDim || !this.fEnabled) ? "#000000" : this.strTrimColor;
this.divFilmstrip.filters.item(1).Enabled = !p_fDim && !this.fEnabled;
}
FilmStrip.prototype.SetEnabled = function(p_fEnabled)
{
if (this.fHidden)
{
return;
}
this.fEnabled = p_fEnabled;
this.divFilmstrip.filters.item(1).Enabled = !this.fDim && !p_fEnabled;
for (var i=0; i < this.Count(); i++)
{
this.GetNode(i).SetEnabled(p_fEnabled);
}
}
FilmStrip.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
if (this.fHidden)
{
this.size.width = 0;
this.size.height = 0;
}
else
{
p_iWidth = Math.max(1, p_iWidth);
p_iHeight = Math.max(1, p_iHeight);
this.size.width = p_iWidth;
this.divFilmstrip.style.pixelWidth = p_iWidth;
this.spanCover.style.width = this.divFilmstrip.style.pixelWidth + (this.Count() * this.sizeNode.width);
var iSpaceToFill = this.divFilmstrip.style.pixelWidth - this.sizeNode.width;
var iLeft = Math.max(1, Math.floor(iSpaceToFill / 2));
this.spanLeftSpacer.style.width = iLeft;
var iRight = Math.max(1, Math.ceil(iSpaceToFill / 2));
this.spanRightSpacer.style.width = iRight;
this.iLeftThumbsVisible = Math.ceil(iLeft / this.sizeNode.width);
this.iRightThumbsVisible = Math.ceil(iRight / this.sizeNode.width);
}
}
FilmStrip.prototype.NodeClickedHandler = function(p_Node)
{
if (this.fncClickedCallback && this.fEnabled)
{
this.fncClickedCallback(p_Node.IndexOf());
}
}
FilmStrip.prototype.Refresh = function()
{
this.SetSelectedNode(this.selectedNode);
}
FilmStrip.prototype.SetSelectedNode = function(p_Node, p_fShowMotion)
{
if (this.selectedNode && (this.selectedNode != p_Node))
{
this.selectedNode.SetSelection(false);
}
if (p_Node == null)
{
this.selectedNode = null;
this.IndexChangedNotify(-1);
return;
}
var iOldLeft = this.divFilmstrip.scrollLeft;
var iNewLeft = (this.sizeNode.width * p_Node.IndexOf());
if (p_fShowMotion)
{
var iTotalShift = iNewLeft - iOldLeft;
var iSignedMultiplier = (iTotalShift >= 0) ? 1 : -1;
var iAbsoluteShift = Math.abs(iTotalShift);
var fTargetPixelsPerSecond = 500;
var iPixelsShifted = 0;
var iScrollAmount = 0;
var elapsedSeconds;
var iLoopCount = 0;
var startTime = new Date();
var iTempLeft;
while ((iPixelsShifted + iScrollAmount) <= iAbsoluteShift)
{
iPixelsShifted += iScrollAmount;
iTempLeft = iOldLeft + (iPixelsShifted * iSignedMultiplier);
this.divFilmstrip.scrollLeft = iTempLeft;
iLoopCount++;
elapsedSeconds = (new Date() - startTime) / 1000;
iScrollAmount = (fTargetPixelsPerSecond * (elapsedSeconds + (elapsedSeconds / iLoopCount))) - iPixelsShifted;
}
}
this.divFilmstrip.scrollLeft = iNewLeft;
p_Node.SetSelection(true);
this.selectedNode = p_Node;
this.IndexChangedNotify(this.selectedNode.IndexOf());
}
FilmStrip.prototype.IndexChangedNotify = function(p_iIndex)
{
if (this.fncIndexChangedCallback)
{
this.fncIndexChangedCallback(p_iIndex);
}
}
function Thumbnail(p_img, p_strText)
{
this.img = p_img;
this.strText = p_strText ? p_strText : "";
this.onChange = new Event(this);
}
Thumbnail.prototype.Dispose = function()
{
if (this.img)
{
this.img.src = "";
delete(this.img);
}
}
Thumbnail.prototype.Update = function(p_img, p_strText)
{
this.img = p_img;
this.strText = p_strText ? p_strText : "";
this.onChange.Fire();
}
function FilmStripNode(
p_thumbnail,
p_sizeMaxImage,
p_sizeNode,
p_fEnabled,
p_strBackgroundColor,
p_strSelectedColor,
p_strBorderColor,
p_iBorderSize,
p_fncClickedCallback
)
{
this.thumbnail = p_thumbnail;
this.thumbnail.oNode = this;
this.thumbnail.onChange.Attach(FilmStripNode.ThumbnailChangedHandler);
this.sizeMaxImage = p_sizeMaxImage;
this.sizeNode = p_sizeNode;
this.strBackgroundColor = p_strBackgroundColor;
this.strSelectedColor = p_strSelectedColor;
this.strBorderColor = p_strBorderColor;
this.iBorderSize = p_iBorderSize;
this.fncClickedCallback = p_fncClickedCallback;
this.fSelected = false;
this.spanOuterFrame = document.createElement("span");
var s = this.spanOuterFrame;
s.style.position = "relative";
s.style.top = 0;
s.style.width = p_sizeNode.width;
s.style.height = p_sizeNode.height;
s.style.backgroundColor = this.strBackgroundColor;
this.spanOuterFrame.oNode = this;
this.element = this.spanOuterFrame;
this.fEnabled = p_fEnabled;
FilmStripNode.ThumbnailChangedHandler(this.thumbnail);
}
FilmStripNode.prototype.Dispose = function()
{
Dispose(this.thumbnail);
Dispose(this.ospanText);
}
FilmStripNode.prototype.SetEnabled = function(p_fEnabled)
{
this.fEnabled = p_fEnabled;
var strCursor = p_fEnabled ? "hand" : "default";
if (this.thumbnail.img)
{
this.spanInnerFrame.style.cursor = this.thumbnail.img.style.cursor = strCursor;
}
else
{
this.ospanText.element.style.cursor = strCursor;
}
}
FilmStripNode.ThumbnailChangedHandler = function(p_thumbnail)
{
var node = p_thumbnail.oNode;
if (node.spanInnerFrame)
{
node.spanInnerFrame.removeNode(true);
node.spanInnerFrame = null;
}
else if (node.ospanText)
{
node.ospanText.element.style.display = "none";
Dispose(node.ospanText);
node.ospanText = null;
}
if (node.thumbnail.img)
{
var img = node.thumbnail.img;
DomImage.Standardize(img);
DomImage.Scale(img, img.size.GetScaledRatio(node.sizeMaxImage));
var iInnerFrameWidth = img.iWidth + (2 * node.iBorderSize);
var iInnerFrameHeight = img.iHeight + (2 * node.iBorderSize);
var iInnerFrameLeft = Math.floor((node.sizeNode.width - iInnerFrameWidth) / 2);
var iInnerFrameTop = Math.floor((node.sizeNode.height - iInnerFrameHeight) / 2);
node.spanInnerFrame = DomElement.CreateElement("span",
"position", "absolute",
"width", iInnerFrameWidth,
"height", iInnerFrameHeight,
"left", iInnerFrameLeft,
"top", iInnerFrameTop,
"backgroundColor", node.strBackgroundColor,
"zIndex", 1
);
node.spanInnerFrame.oNode = node;
node.spanInnerFrame.onclick = FilmStripNode.MouseDownHandler;
node.spanOuterFrame.appendChild(node.spanInnerFrame);
Properties.Set(img.style,
"position", "absolute",
"left", iInnerFrameLeft + node.iBorderSize - img.oClip.iLeft,
"top", iInnerFrameTop + node.iBorderSize - img.oClip.iTop,
"zIndex", 3
);
img.oNode = node;
img.onclick = FilmStripNode.MouseDownHandler;
node.spanOuterFrame.appendChild(img);
}
else
{
node.ospanText = new CenteredSpan(
null,
"normal",
9,
node.strSelectedColor,
true
);
Properties.Set(node.ospanText.element.style,
"backgroundColor", node.strBackgroundColor,
"left", 2,
"top", 2,
"width", node.sizeNode.width - 4,
"height", node.sizeNode.height - 4,
"zIndex", 2,
"letterSpacing", 0,
"border", "1 solid " + node.strSelectedColor
);
node.ospanText.element.oNode = node;
node.ospanText.element.onclick = FilmStripNode.MouseDownHandler;
node.ospanText.innerElement.appendChild(document.createTextNode(node.thumbnail.strText));
node.spanOuterFrame.appendChild(node.ospanText.element);
}
node.SetEnabled(node.fEnabled);
if (node.fSelected)
{
node.SetSelection(true);
}
}
FilmStripNode.prototype.IndexOf = function()
{
return(Collection.IndexOf(this.thumbnail));
}
FilmStripNode.prototype.SetSelection = function(p_fSelected)
{
this.spanOuterFrame.style.backgroundColor = p_fSelected ? this.strSelectedColor : this.strBackgroundColor;
if (this.spanInnerFrame)
{
this.spanInnerFrame.style.backgroundColor = p_fSelected ? this.strBorderColor : this.strBackgroundColor;
}
this.fSelected = p_fSelected;
}
FilmStripNode.MouseDownHandler = function()
{
var element = DomElement.FindAncestorWithProperty(window.event.srcElement, "oNode");
if (element)
{
element.oNode.fncClickedCallback(element.oNode);
}
}
function RolloverImage(
p_imgNormal,
p_imgHover,
p_imgPressed,
p_strTitle,
p_fncOnClickHandler,
p_imgNormal2,
p_imgHover2,
p_imgPressed2,
p_strTitle2,
p_fOn
)
{
this.fncOnClickHandler = p_fncOnClickHandler;
this.off = {
strTitle:p_strTitle,
imgNormal:p_imgNormal,
imgHover:p_imgHover,
imgPressed:p_imgPressed
};
this.on = {
strTitle:p_strTitle2,
imgNormal:p_imgNormal2,
imgHover:p_imgHover2,
imgPressed:p_imgPressed2
};
this.fIsSwapper = AnyWithValues(p_strTitle2, p_imgNormal2, p_imgHover2, p_imgPressed2);
this.fOn = IsDefined(p_fOn) ? p_fOn : false;
this.currentSet = p_fOn ? this.on : this.off;
this.fDim = false;
this.fAllowSimulation = true;
this.fEnabled = true;
this.onSizeChanged = new Event();
this.CreateElements();
}
RolloverImage.NORMAL = 0;
RolloverImage.HOVER = 1;
RolloverImage.PRESSED = 2;
RolloverImage.prototype.Dispose = function()
{
delete(this.img);
this.DisposeImageSet(this.off);
this.DisposeImageSet(this.on);
}
RolloverImage.prototype.DisposeImageSet = function(p_set)
{
if (p_set)
{
delete(p_set.imgNormal);
delete(p_set.imgHover);
delete(p_set.imgPressed);
}
}
RolloverImage.prototype.SetOnOff = function(p_fOn)
{
if (this.fIsSwapper)
{
this.fOn = p_fOn;
this.currentSet = this.fOn ? this.on : this.off;
this.ShowImage(this.strImageMode);
if (IsDefined(this.currentSet.strTitle))
{
this.img.title = this.currentSet.strTitle;
}
}
}
RolloverImage.prototype.AddImage = function(p_img)
{
if (p_img)
{
DomImage.Standardize(p_img);
}
}
RolloverImage.prototype.AddImageSet = function(p_imageSet)
{
this.AddImage(p_imageSet.imgNormal);
this.AddImage(p_imageSet.imgHover);
this.AddImage(p_imageSet.imgPressed);
p_imageSet.imgHover = p_imageSet.imgHover ? p_imageSet.imgHover : p_imageSet.imgNormal;
p_imageSet.imgPressed = p_imageSet.imgPressed ? p_imageSet.imgPressed : p_imageSet.imgHover;
}
RolloverImage.prototype.CreateElements = function()
{
this.fncImageChangedHandler = this.Method_("ImageChangedHandler");
this.fncSimulateUp = this.Method_("SimulateUp");
this.span = document.createElement("span");
this.AddImageSet(this.on);
this.AddImageSet(this.off);
this.size = new Data.Size(this.currentSet.imgNormal.iWidth, this.currentSet.imgNormal.iHeight);
this.reserveSize = this.size;
this.span.style.width = this.size.width;
this.span.style.height = this.size.height;
this.span.style.overflow = "hidden";
this.img = DomElement.CreateElementWithFilter("img",
"Gray(enabled=0) alpha(Opacity=50,enabled=0)",
"progid:DXImageTransform.Microsoft.BasicImage(grayScale=1,enabled=false) progid:DXImageTransform.Microsoft.Alpha(Opacity=50,enabled=false)"
);
this.img.title = this.currentSet.strTitle;
DomElement.SetStyles(this.img,
"cursor", "hand",
"position", "absolute",
"zIndex", 2
);
this.img.rollover = this;
this.span.appendChild(this.img);
this.HookupEvents(true);
this.element = this.span;
this.ShowImage("imgNormal");
this.cState = RolloverImage.NORMAL;
this.onSizeChanged.Fire(this);
}
RolloverImage.prototype.HookupEvents = function(p_fAttached)
{
DomEvents.Hookup(this.img, p_fAttached,
"onmouseover", RolloverImage.prototype.MouseOverHandler,
"onmouseout", RolloverImage.prototype.MouseOutHandler,
"onmousedown", RolloverImage.prototype.MouseDownHandler,
"onmouseup", RolloverImage.prototype.MouseUpHandler,
"ondblclick", RolloverImage.prototype.DoubleClickHandler
);
}
RolloverImage.prototype.SetDimmer = function(p_fDim)
{
this.fDim = p_fDim;
this.img.filters.item(1).Enabled = (p_fDim || !this.fEnabled);
}
RolloverImage.prototype.SetEnabled = function(p_fEnabled)
{
if (p_fEnabled != this.fEnabled)
{
this.fEnabled = p_fEnabled;
if (!p_fEnabled)
{
this.MouseOutHandler();
}
this.img.filters.item(0).Enabled = !p_fEnabled;
this.img.filters.item(1).Enabled = (this.fDim || !p_fEnabled);
this.HookupEvents(p_fEnabled);
this.img.style.cursor = p_fEnabled ? "hand" : "default";
}
}
RolloverImage.prototype.SetTitle = function(p_strTitle, p_fOn)
{
var imgSet = p_fOn ? this.on : this.off;
imgSet.strTitle = p_strTitle;
if (this.fOn == p_fOn)
{
this.img.title = p_strTitle;
}
}
RolloverImage.prototype.Press = function()
{
if (this.fAllowSimulation)
{
this.MouseDownMethod();
SafeSetTimeout(this.fncSimulateUp, 100);
}
}
RolloverImage.prototype.UpdateImage = function(p_img)
{
var img = p_img;
DomImage.Standardize(img);
this.size.Resize(img.iWidth, img.iHeight);
DomElement.SetStyles(this.span,
"width", this.size.width,
"height", this.size.height
);
if (this.imgCurrent == img)
{
this.Refresh();
}
this.onSizeChanged.Fire(this);
}
RolloverImage.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
}
RolloverImage.prototype.Refresh = function()
{
if (this.img.src != this.imgCurrent.src)
{
this.img.src = this.imgCurrent.src;
}
DomStyles.SetClipRectangle(this.img, this.imgCurrent.oClip);
this.img.style.left = 0 - this.imgCurrent.oClip.iLeft;
this.img.style.top = 0 - this.imgCurrent.oClip.iTop;
}
RolloverImage.prototype.ShowImage = function(p_strImageMode)
{
var img = this.currentSet[p_strImageMode];
this.strImageMode = p_strImageMode;
if (img != this.imgCurrent)
{
this.imgCurrent = img;
this.Refresh();
}
}
RolloverImage.prototype.DisplayNormal = function()
{
this.ShowImage("imgNormal");
}
RolloverImage.prototype.DisplayHover = function()
{
this.ShowImage("imgHover");
}
RolloverImage.prototype.DisplayPressed = function()
{
this.ShowImage("imgPressed");
}
RolloverImage.prototype.DoClick = function()
{
var fSetOn = !this.fOn;
if ((this.fncOnClickHandler) && (this.fncOnClickHandler(this, fSetOn)))
{
return;
}
this.SetOnOff(fSetOn);
}
RolloverImage.GetEventObject = function(p_obj)
{
return((p_obj.constructor == RolloverImage) ? p_obj : event.srcElement.rollover);
}
RolloverImage.prototype.MouseOverHandler = function()
{
var ri = RolloverImage.GetEventObject(this);
ri.cState = RolloverImage.HOVER;
ri.DisplayHover();
}
RolloverImage.prototype.MouseOutHandler = function()
{
var ri = RolloverImage.GetEventObject(this);
ri.cState = RolloverImage.NORMAL;
ri.DisplayNormal();
}
RolloverImage.prototype.MouseDownHandler = function()
{
if (event.button != 1)
{
return;
}
var ri = RolloverImage.GetEventObject(this);
ri.cState = RolloverImage.PRESSED;
ri.DisplayPressed();
}
RolloverImage.prototype.MouseDownMethod = function()
{
var ri = RolloverImage.GetEventObject(this);
ri.cState = RolloverImage.PRESSED;
ri.DisplayPressed();
}
RolloverImage.prototype.MouseUpHandler = function()
{
if (event.button != 1)
{
return;
}
var ri = RolloverImage.GetEventObject(this);
ri.MouseUpMethod(false);
}
RolloverImage.prototype.MouseUpMethod = function(p_fBackToNormal)
{
if (this.cState == RolloverImage.PRESSED)
{
this.DoClick();
}
if (p_fBackToNormal || (!this.fEnabled))
{
this.MouseOutHandler();
}
else
{
this.MouseOverHandler();
}
}
RolloverImage.prototype.SimulateUp = function()
{
this.MouseUpMethod(true);
}
RolloverImage.prototype.DoubleClickHandler = function()
{
}
function ViewerLayout(
p_fncReadyCallback,
p_parentElement,
p_config,
p_log
)
{
this.fncReadyCallback = p_fncReadyCallback;
this.config = p_config;
this.log = p_log;
this.parentElement = p_parentElement;
this.size = new Data.Size(0, 0);
this.fReady = false;
this.onSizeChanged = new Event(this);
this.imgMega = new ViewerLayout.MegaImage(p_config.strImagesDir + "Mega.gif", this.log);
this.imgMega.PreLoad(35, this.Method_("MegaImageLoaded"));
}
ViewerLayout.prototype.MegaImageLoaded = function(p_Success)
{
if (p_Success)
{
this.CreateElements();
this.parentElement.appendChild(this.element);
this.fReady = true;
}
SafeSetTimeout(this.fncReadyCallback, 1);
}
ViewerLayout.prototype.Display = function()
{
this.fReady = true;
window.attachEvent("onresize", this.Method_("ResizeHandler"));
this.ResizeHandler();
this.spanPlayback.style.visibility = "visible";
}
ViewerLayout.prototype.ResizeHandler = function()
{
if (this.fReady)
{
this.AllocateSize(document.body.clientWidth, document.body.clientHeight);
}
}
ViewerLayout.prototype.Dispose = function()
{
Dispose(this.imgMega);
Dispose(this.display);
Dispose(this.controls);
}
ViewerLayout.prototype.CreateElements = function()
{
this.spanPlayback = DomElement.CreateElement("span",
"position", "absolute",
"visibility", "hidden"
);
this.spanPlayback.attachEvent("onmousemove", this.Method_("MouseMoveHandler"));
this.display = this.CreateSlideDisplay(this.iDisplayHeight);
this.display.element.style.height = 0;
this.controls = new ViewerControls(this.config, this.imgMega);
this.controls.onSizeChanged.Attach(this.Method_("ResizeHandler"));
this.spanPlayback.appendChild(this.display.element);
this.spanPlayback.appendChild(this.controls.element);
this.element = this.spanPlayback;
}
ViewerLayout.prototype.MouseMoveHandler = function()
{
var e = window.event;
var iY = e.offsetY;
if (this.element != window.event.srcElement)
{
var p = DomPositioning.FindOffsetInParentElement(window.event.srcElement, this.element);
iY += p.y;
}
if (iY > this.iControlsTop)
{
this.controls.MouseOverSim();
}
}
ViewerLayout.prototype.CreateSlideDisplay = function()
{
var display = new SlideDisplay(
true,
true,
this.config.strings.RES_SlideViewer_WindowTitle,
null
);
display.element.style.position = "absolute";
return(display);
}
ViewerLayout.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
p_iWidth = Math.max(1, p_iWidth);
p_iHeight = Math.max(1, p_iHeight);
this.size.Resize(p_iWidth, p_iHeight);
this.controls.AllocateSize(p_iWidth, 0);
this.iControlsHeight = this.controls.size.height;
this.iControlsTop = this.size.height - this.iControlsHeight;
this.iDisplayHeight = this.controls.fAlwaysVisible ? this.iControlsTop : this.size.height;
this.display.AllocateSize(p_iWidth, this.iDisplayHeight);
this.controls.element.style.top = this.iControlsTop;
}
ViewerLayout.CreateToolBarSeparator = function(p_imgMega)
{
return(ViewerLayout.CreateImageControl(p_imgMega, 849, 2, 24));
}
ViewerLayout.CreateImageControl = function(p_imgMega, p_iLeft, p_iWidth, p_iHeight)
{
return(new ImageControl(p_imgMega.CreateFrom(p_iLeft, p_iWidth, p_iHeight)));
}
ViewerLayout.ImageSet = function(p_imgMega, p_iWidth, p_iHeight)
{
this.imgMega = p_imgMega;
this.iWidth = p_iWidth;
this.iHeight = p_iHeight;
}
ViewerLayout.ImageSet.prototype.GetClipRect = function(p_iLeft)
{
if (IsDefinedAndNotNull(p_iLeft))
{
var oClip = new Data.Rectangle(0, p_iLeft + this.iWidth, this.iHeight, p_iLeft);
return(oClip);
}
return(null);
}
ViewerLayout.ImageSet.prototype.Create = function(p_oClip)
{
var img = null;
if (IsDefinedAndNotNull(p_oClip))
{
if (p_oClip.constructor != Data.Rectangle)
{
p_oClip = this.GetClipRect(p_oClip);
}
if (p_oClip)
{
img = this.imgMega.CreateFrom(p_oClip);
}
}
return(img);
}
ViewerLayout.CreateRolloverImage = function(
p_imgMega,
p_iWidth,
p_iHeight,
p_fncOnClickHandler,
p_strTitle,
p_strShortcut,
p_iNormalClipLeft,
p_iHoverClipLeft,
p_iPressedClipLeft,
p_strTitle2,
p_strShortcut2,
p_iNormalClipLeft2,
p_iHoverClipLeft2,
p_iPressedClipLeft2,
p_fOn
)
{
var imageSet = new ViewerLayout.ImageSet(p_imgMega, p_iWidth, p_iHeight);
var imgNormal = imageSet.Create(p_iNormalClipLeft);
var imgHover = imageSet.Create(p_iHoverClipLeft);
var imgPressed = imageSet.Create(p_iPressedClipLeft);
var imgNormal2 = imageSet.Create(p_iNormalClipLeft2);
var imgHover2 = imageSet.Create(p_iHoverClipLeft2);
var imgPressed2 = imageSet.Create(p_iPressedClipLeft2);
var rolloverImage = new RolloverImage(
imgNormal, imgHover, imgPressed, ViewerLayout.AddShortcutToText(p_strTitle, p_strShortcut), p_fncOnClickHandler,
imgNormal2, imgHover2, imgPressed2, ViewerLayout.AddShortcutToText(p_strTitle2, p_strShortcut2), p_fOn
);
return(rolloverImage);
}
ViewerLayout.CreateButtonLabel = function(
p_imgMega,
p_iWidth,
p_iHeight,
p_fncOnClickHandler,
p_iSepWidth,
p_iPaddingLeft,
p_iPaddingRight,
p_strTitle,
p_strShortCutText,
p_fCanHideText,
p_iNormalClipLeft,
p_iHoverClipLeft,
p_iPressedClipLeft,
p_strTitle2,
p_iNormalClipLeft2,
p_iHoverClipLeft2,
p_iPressedClipLeft2,
p_fOn
)
{
var img = ViewerLayout.CreateRolloverImage(
p_imgMega, p_iWidth, p_iHeight, null,
null, null, p_iNormalClipLeft, p_iHoverClipLeft, p_iPressedClipLeft,
null, null, p_iNormalClipLeft2, p_iHoverClipLeft2, p_iPressedClipLeft2, p_fOn
);
var imageLabel = new ButtonLabel(img, p_strTitle, p_strShortCutText, true, p_fCanHideText, "xsmallFont",
"#000000", 23, "#DDDDDD", "#FFFFFF", "#999999", null, "#BBBBBB",
p_fncOnClickHandler, p_iSepWidth, p_iPaddingLeft, p_iPaddingRight);
return(imageLabel);
}
ViewerLayout.AddShortcutToText = function(p_strText, p_strShortcut)
{
if (!p_strShortcut)
{
return(p_strText);
}
return(p_strText + " (" + p_strShortcut + ")");
}
ViewerLayout.MegaImage = function(p_strSrc, p_log)
{
this.log = p_log;
this.strSrc = p_strSrc;
this.aimg = new Array();
this.iLoadTarget = 0;
this.iErrorCount = 0;
this.iNextImage = 0;
}
ViewerLayout.MegaImage.prototype.Dispose = function()
{
for (var i=0; i < this.aimg.length; i++)
{
if (this.aimg[i])
{
delete(this.aimg[i]);
}
}
Dispose(this.aimg);
}
ViewerLayout.MegaImage.prototype.CreateFrom = function(p_iLeft, p_iWidth, p_iHeight)
{
var img;
if (this.iNextImage < this.aimg.length)
{
img = this.aimg[this.iNextImage];
delete(this.aimg[this.iNextImage]);
this.iNextImage++;
}
else
{
alert("tried to get:" + this.iNextImage);
return(null);
}
img.style.position = "absolute";
var oClip;
if (arguments.length == 1)
{
oClip = arguments[0];
}
else
{
oClip = new Data.Rectangle(0, p_iLeft + p_iWidth, p_iHeight, p_iLeft);
}
DomStyles.SetClipRectangle(img, oClip);
DomImage.Standardize(img);
return(img);
}
ViewerLayout.MegaImage.prototype.PreLoad = function(p_iCount, p_fncCallback)
{
this.log.Append("Preloading " + p_iCount + " megaImages");
this.iLoadTarget = p_iCount;
this.fncCallback = p_fncCallback;
this.LoadNext();
}
ViewerLayout.MegaImage.prototype.LoadNext = function()
{
if (blnIsUnloading) return;
if (this.aimg.length < this.iLoadTarget)
{
this.smartImage = new SmartImageControl();
this.smartImage.iMaxAttempts = 3;
this.smartImage.SetSrc(this.strSrc)
this.smartImage.Load(this.Method_("ImageLoaded"));
}
else
{
this.log.Append("Loaded " + this.iLoadTarget + " megaImages");
this.fncCallback(true);
}
}
ViewerLayout.MegaImage.prototype.ImageLoaded = function(p_fSuccess)
{
if (p_fSuccess)
{
this.iErrorCount = 0;
this.aimg[this.aimg.length] = this.smartImage.DetachImage();
SafeSetTimeout(this.Method_("LoadNext"), 1);
}
else
{
this.log.Append("Failed to load " + this.iLoadTarget + " megaImages");
this.fncCallback(false);
}
}
function ViewerControls(p_config, p_imgMega)
{
this.fAlwaysVisible = true;
this.config = p_config;
this.imgMega = p_imgMega;
this.onSizeChanged = new Event(this);
this.size = new Data.Size(0, 0);
this.iMargin = 6;
this.filmstrip = this.CreateFilmstrip();
this.filmstrip.element.style.position = "absolute";
this.filmstrip.onSizeChanged.Attach(this.onSizeChanged.Method_("Fire"));
this.imageSlider = this.CreateImageSlider();
this.imageSlider.element.style.position = "absolute";
this.imageSlider.element.style.left = this.iMargin;
this.imageSlider.onMouseUpCallback.Attach(this.Method_("UpdateActiveUI"));
this.toolBar = this.CreateToolBar();
this.toolBar.element.style.left = this.iMargin;
this.toolBar.element.style.position = "absolute";
this.toolBar.onSizeChanged.Attach(this.onSizeChanged.Method_("Fire"));
this.spanOuter = DomElement.CreateElementWithFilter("span", "BlendTrans(duration=7)", "progid:DXImageTransform.Microsoft.Fade(duration=3,overlap=1,enabled=false)");
this.spanOuter.style.position = "absolute";
this.blnTransitioning = false;
this.fncOnFadeDone = null;
this.spanOuter.attachEvent(Browser.strMouseOverEvent, this.Method_("MouseOverHandler"));
this.spanOuter.attachEvent(Browser.strMouseOutEvent, this.Method_("MouseOutHandler"));
this.spanControls = DomElement.CreateElement("span", "position", "absolute", "backgroundColor", "#DDDDDD");
this.spanControls.appendChild(this.filmstrip.element);
this.spanControls.appendChild(this.imageSlider.element);
this.spanControls.appendChild(this.toolBar.element);
this.spanOuter.appendChild(this.spanControls);
this.element = this.spanOuter;
this.fEnabled = true;
this.fActive = false;
}
ViewerControls.prototype.SetVisibility = function(p_fVisible)
{
if (p_fVisible != this.fAlwaysVisible)
{
var fActive = this.fActive;
this.SetActive(true);
this.fAlwaysVisible = p_fVisible;
this.SetActive(fActive);
this.onSizeChanged.Fire();
}
}
ViewerControls.prototype.StopFade = function()
{
this.spanOuter.onfilterchange = null;
var fadeFilter = this.spanOuter.filters[0];
if (this.blnTransitioning)
{
fadeFilter.Stop();
this.blnTransitioning = false;
if (this.fncOnFadeDone)
{
this.fncOnFadeDone(false);
}
}
}
ViewerControls.prototype.SetActive = function(p_fActive)
{
this.fActive = p_fActive;
if (p_fActive)
{
this.StopFade();
}
if (this.imageSlider.eState == Slider.NORMAL)
{
this.UpdateActiveUI();
}
}
ViewerControls.prototype.UpdateActiveUI = function()
{
if (this.fAlwaysVisible)
{
this.filmstrip.SetDimmer(!this.fActive);
}
else
{
this.spanControls.style.visibility = this.fActive ? "visible" : "hidden";
}
}
ViewerControls.prototype.FadeAway = function(p_fncDone)
{
this.fncOnFadeDone = p_fncDone;
var fadeFilter = this.spanOuter.filters[0];
function FilterChangeHandler()
{
if (fadeFilter.status == 0)
{
p_fncDone(true);
}
}
this.spanOuter.onfilterchange = FilterChangeHandler;
this.blnTransitioning = true;
fadeFilter.Apply();
this.SetActive(false);
fadeFilter.Play();
}
ViewerControls.prototype.MouseOverHandler = function()
{
if (this.fAlwaysVisible && this.element.contains(window.event.srcElement))
{
this.SetActive(true);
}
}
ViewerControls.prototype.MouseOverSim = function()
{
if (!this.fAlwaysVisible)
{
this.SetActive(true);
}
}
ViewerControls.prototype.MouseOutHandler = function()
{
if (this.element.contains(window.event.srcElement))
{
this.SetActive(false);
}
}
ViewerControls.prototype.SetEnabled = function(p_fEnabled)
{
this.fEnabled = p_fEnabled;
this.toolBar.SetEnabled(p_fEnabled);
this.filmstrip.SetEnabled(p_fEnabled);
this.imageSlider.SetEnabled(p_fEnabled);
}
ViewerControls.prototype.Dispose = function()
{
Dispose(this.filmstrip);
Dispose(this.imageSlider);
Dispose(this.toolBar);
this.spanOuter.onfilterchange = null;
}
ViewerControls.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
p_iWidth = Math.max(1, p_iWidth);
p_iHeight = Math.max(1, p_iHeight);
this.size.width = p_iWidth;
this.spanControls.style.width = this.spanOuter.style.width = p_iWidth;
this.filmstrip.AllocateSize(p_iWidth, 0);
this.imageSlider.AllocateSize(p_iWidth - (2 * this.iMargin), 0);
this.toolBar.AllocateSize(p_iWidth - (2 * this.iMargin), 0);
var iFilmstripHeight = this.filmstrip.size.height + this.iMargin;
var iImageSliderHeight = this.imageSlider.element.style.pixelHeight + this.iMargin;
var iBottomControlsHeight = this.toolBar.size.height + this.iMargin;
var iControlsHeight = iFilmstripHeight + iImageSliderHeight + iBottomControlsHeight;
this.spanControls.style.height = this.spanOuter.style.height = iControlsHeight;
this.size.height = iControlsHeight;
this.filmstrip.element.style.top = 0;
this.imageSlider.element.style.top = iFilmstripHeight;
this.toolBar.element.style.top = iFilmstripHeight + iImageSliderHeight;
}
ViewerControls.prototype.CreateImageSlider = function()
{
var iHeight = 11;
var imgHandle = this.imgMega.CreateFrom(384, 23, iHeight);
var imgMinEnd = this.imgMega.CreateFrom(1033, 4, iHeight);
var imgMaxEnd = this.imgMega.CreateFrom(1037, 5, iHeight);
var imgUsed = document.createElement("img");
var imgUsedSrc = this.config.strImagesDir + "prggrn.gif";
imgUsed.src = imgUsedSrc;
imgUsed.height = iHeight;
var imgUnused = document.createElement("img");
var imgUnusedSrc = this.config.strImagesDir + "prgemp.gif";
imgUnused.src = imgUnusedSrc;
imgUnused.height = iHeight;
var imageSlider = new Slider
(
false,
true,
false,
"",
imgHandle,
null,
imgMinEnd,
imgMaxEnd,
imgUsed,
imgUnused
);
return(imageSlider);
}
ViewerControls.prototype.CreateFilmstrip = function()
{
var iHeight = 57;
var filmstrip = new FilmStrip(
new Data.Size(48, 48),
new Data.Size(iHeight-1, iHeight-1),
1,
2,
"#FFCC00",
"#666666",
"#999999",
null,
null
);
return(filmstrip);
}
ViewerControls.prototype.CreateToolBar = function()
{
var stackedToolar = new StackedToolBar(4, 1, "#92A5C5", "#FFFFFF");
var toolbar = new ToolBar(0, this.config.messenger ? ToolBar.LEFT : ToolBar.LEFT);
this.playerToolBar = new ViewerControls.PlayToolBar(this.config, this.imgMega);
this.speedSliderToolBar = new ViewerControls.SpeedToolBar(this.config, this.imgMega);
toolbar.Add(this.playerToolBar, 0, 0);
toolbar.Add(ViewerLayout.CreateToolBarSeparator(this.imgMega), 15, 15);
toolbar.Add(this.speedSliderToolBar, 0, 0);
if (!this.config.messenger)
{
this.toggleTextButton = ViewerLayout.CreateButtonLabel(this.imgMega, 13, 13, null, 3, 5, 5, this.config.strings.RES_SlideViewer_ShowTitles, this.config.strings.RES_ToggleText_ShortcutKey, false, 867, 880, null, null, 893, 906, null, true);
this.buttonToolBar = this.CreateButtonToolBar(this.config);
this.windowButton = ViewerLayout.CreateRolloverImage(this.imgMega, 19, 19, null, this.config.strings[this.config.fFullscreen ? "RES_SlideViewer_WindowedMode" : "RES_SlideViewer_FullScreen"], this.config.strings.RES_Window_ShortcutKey, 919, 957, 995);
toolbar.Add(ViewerLayout.CreateToolBarSeparator(this.imgMega), 15, 10);
toolbar.Add(this.toggleTextButton, 5, 0, 0);
}
stackedToolar.AddControl(toolbar);
if (this.config.messenger)
{
this.messengerToolbar = this.CreateMessengerToolBar();
stackedToolar.AddControl(this.messengerToolbar);
}
return(stackedToolar);
}
ViewerControls.prototype.CreateMessengerToolBar = function()
{
var toolbar = new ToolBar(0, ToolBar.LEFT);
toolbar.addPicturesButton = ViewerLayout.CreateButtonLabel(this.imgMega, 16, 16, null, 3, 5, 5, this.config.strings.RES_SlideViewer_AddPictures, true, 851);
var strControlText = this.config.strings.RES_SlideViewer_GiveControl.replace("{0}", this.config.messenger.users.them.Name);
toolbar.haveControlButton = ViewerLayout.CreateButtonLabel(this.imgMega, 13, 15, null, 5, 5, 5, strControlText, true, 1142, 1155);
toolbar.haveControlButton.fCanClip = true;
toolbar.Add(toolbar.addPicturesButton, 0, 0, 1);
toolbar.Add(ViewerLayout.CreateToolBarSeparator(this.imgMega), 5, 5);
toolbar.Add(toolbar.haveControlButton, 0, 0, 2);
return(toolbar);
}
function ViewerControls.PlayToolBar(p_config, p_imgMega)
{
this._constructor_0(0);
this.imgPlayPauseButton = ViewerLayout.CreateRolloverImage(p_imgMega, 25, 25, null, p_config.strings.RES_SlideViewer_Play, p_config.strings.RES_PlayPause_ShortcutKey, 0, 117, 234, p_config.strings.RES_SlideViewer_Pause, p_config.strings.RES_PlayPause_ShortcutKey, 25, 142, 259);
this.imgStopButton = ViewerLayout.CreateRolloverImage(p_imgMega, 19, 19, null, p_config.strings.RES_SlideViewer_Stop, p_config.strings.RES_Stop_ShortcutKey, 50, 167, 284);
this.imgPreviousButton = ViewerLayout.CreateRolloverImage(p_imgMega, 24, 23, null, p_config.strings.RES_SlideViewer_Previous, p_config.strings.RES_Previous_ShortcutKey, 69, 186, 303);
this.imgNextButton = ViewerLayout.CreateRolloverImage(p_imgMega, 24, 23, null, p_config.strings.RES_SlideViewer_Next, p_config.strings.RES_Next_ShortcutKey, 93, 210, 327);
this.Add(this.imgPlayPauseButton, 0, 6);
this.Add(this.imgStopButton, 0, 6);
this.Add(this.imgPreviousButton);
this.Add(this.imgNextButton);
}
ViewerControls.PlayToolBar.Inherits_(ToolBar);
function ViewerControls.SpeedToolBar(p_config, p_imgMega)
{
this._constructor_0(0);
var strClassName = "xsmallFont";
var strColor = "#333333";
var imgHandle = p_imgMega.CreateFrom(351, 11, 21);
var imgBackground = p_imgMega.CreateFrom(1042, 100, 21);
this.slider = new Slider(false, false, false, p_config.strings.RES_Speed_ShortcutKey, imgHandle, imgBackground);
this.Add(new TextSpanControl(p_config.strings.RES_SlideViewer_Slow, strClassName, strColor), 0, 0);
this.Add(this.slider, 7, 7);
this.Add(new TextSpanControl(p_config.strings.RES_SlideViewer_Fast, strClassName, strColor), 0, 0);
}
ViewerControls.SpeedToolBar.Inherits_(ToolBar);
ViewerControls.prototype.CreateButtonToolBar = function(p_config)
{
var toolbar = new ToolBar(0);
if (p_config.btnPrint.fVisible)
{
toolbar.print = ViewerLayout.CreateButtonLabel(this.imgMega, 22, 22, null, 0, 1, 5, p_config.strings.RES_SlideViewer_Print, null, true, 453);
toolbar.Add(toolbar.print, 0, 0, 4);
}
if (p_config.btnDownload.fVisible)
{
toolbar.download = ViewerLayout.CreateButtonLabel(this.imgMega, 22, 22, null, 0, 1, 5, p_config.strings.RES_SlideViewer_Download, null, true, 475);
toolbar.Add(toolbar.download, 0, 0, 1);
}
if (p_config.btnOrderPrints.fVisible)
{
toolbar.fuji = ViewerLayout.CreateButtonLabel(this.imgMega, 22, 22, null, 0, 1, 5, p_config.strings.RES_SlideViewer_OrderPrints, null, true, 519);
toolbar.Add(toolbar.fuji, 0, 0, 2);
}
if (p_config.btnOrderGifts.fVisible)
{
toolbar.gifts = ViewerLayout.CreateButtonLabel(this.imgMega, 22, 22, null, 0, 1, 5, p_config.strings.RES_SlideViewer_OrderGifts, null, true, 541);
toolbar.Add(toolbar.gifts, 0, 0, 3);
}
return(toolbar);
}
function ButtonLabel(
p_img,
p_strText,
p_strShortCutText,
p_fToolTips,
p_fCanHideText,
p_strClassName,
p_strColor,
p_iHeight,
p_strBackgroundColor,
p_strLightColor,
p_strDarkColor,
p_strHoverColor,
p_strPressedColor,
p_fncOnClickHandler,
p_iSepWidth,
p_iPaddingLeft,
p_iPaddingRight
)
{
this.strText = p_strText;
this.strShortCutText = p_strShortCutText;
this.fToolTips = p_fToolTips;
this.fCanHideText = p_fCanHideText;
this.strClassName = p_strClassName;
this.strColor = p_strColor;
this.strBackgroundColor = p_strBackgroundColor;
this.strLightColor = p_strLightColor;
this.strDarkColor = p_strDarkColor;
this.strHoverColor = p_strHoverColor ? p_strHoverColor : p_strBackgroundColor;
this.strPressedColor = p_strPressedColor ? p_strPressedColor : p_strBackgroundColor;
this.fncOnClickHandler = p_fncOnClickHandler;
this.iSepWidth = p_iSepWidth;
this.iPaddingLeft = p_iPaddingLeft;
this.iPaddingRight = p_iPaddingRight;
this.fCanClip = false;
this.fIsMouseDown = false;
this.fIsMouseOver = false;
this.fDim = false;
this.fAllowSimulation = true;
this.fEnabled = true;
this.fTextVisible = true;
this.fImageVisible = true;
this.size = new Data.Size(0, p_iHeight);
this.reserveSize = new Data.Size(0, p_iHeight);
this.onSizeChanged = new Event();
this.CreateElements(p_img);
}
ButtonLabel.prototype.Dispose = function()
{
Dispose(this.rollover);
}
ButtonLabel.prototype.CreateElements = function(p_img)
{
this.strNormalBorder = "1 solid " + this.strBackgroundColor;
this.strLightBorder = "1 solid " + this.strLightColor;
this.strDarkColor = "1 solid " + this.strDarkColor;
this.fncRolloverResizeHandler = this.Method_("RolloverResizeHandler");
this.fncTextResizeHandler = this.Method_("TextResizeHandler");
this.fncMouseUpHandler = this.Method_("MouseUpHandler");
this.fncMouseMoveHandler = this.Method_("MouseMoveHandler");
this.fncSimulateUp = this.Method_("SimulateUp");
this.spanLabel = document.createElement("span");
DomElement.SetStyles(this.spanLabel,
"position", "absolute",
"cursor", "hand",
"border", this.strNormalBorder,
"height", this.size.height,
"backgroundColor", this.strBackgroundColor,
"overflow", "hidden"
);
this.spanLabel.oLabel = this;
this.element = this.spanLabel;
this.spanCover = document.createElement("span");
DomElement.SetStyles(this.spanCover,
"position", "absolute",
"zIndex", 2,
"height", this.size.height,
"top", 0,
"left", 0
);
this.spanCover.oLabel = this;
if (p_img)
{
if (p_img.constructor != RolloverImage)
{
this.rollover = new RolloverImage(p_img, null, null, this.strText, null);
}
else
{
this.rollover = p_img;
}
this.rollover.fncOnClickHandler = this.Method_("RolloverClickCallback");
this.rollover.img.oLabel = this;
this.rollover.iTop = Math.floor((this.size.height - this.rollover.size.height) / 2) - 1;
this.rollover.iLeft = this.iPaddingLeft;
DomElement.SetStyles(this.rollover.element,
"position", "absolute",
"top", this.rollover.iTop,
"left", this.rollover.iLeft
);
this.spanLabel.appendChild(this.rollover.element);
this.rollover.element.attachEvent("onresize", this.fncRolloverResizeHandler);
this.rollover.SetTitle("", false);
this.rollover.SetTitle("", true);
this.rollover.HookupEvents(false);
}
else
{
this.rollover = null;
}
this.spanText = document.createElement("span");
DomElement.SetStyles(this.spanText,
"position", "relative",
"color", this.strColor,
"height", 1
);
this.spanText.className = this.strClassName;
this.spanText.oLabel = this;
this.spanText.attachEvent("onresize", this.fncTextResizeHandler);
this.nobr = document.createElement("nobr");
this.spanText.appendChild(this.nobr);
this.spanLabel.appendChild(this.spanText);
this.SetText(this.strText);
this.HookupEvents(true);
}
ButtonLabel.prototype.SetOnOff = function(p_fOn)
{
if (this.rollover && this.rollover.fIsSwapper)
{
this.rollover.SetOnOff(p_fOn);
}
}
ButtonLabel.prototype.HookupEvents = function(p_fEnable, p_fCapture)
{
if (!p_fCapture)
{
DomEvents.Hookup(this.spanLabel, p_fEnable,
"onmousedown", ButtonLabel.prototype.MouseDownHandler,
"ondblclick", ButtonLabel.prototype.DoubleClickHandler
);
}
if (this.fIsMouseDown || p_fCapture)
{
DomEvents.Hookup(this.spanLabel, p_fEnable,
"onmouseup", this.fncMouseUpHandler,
"onmousemove", this.fncMouseMoveHandler
);
}
if (!this.fIsMouseDown || p_fCapture)
{
DomEvents.Hookup(this.spanLabel, ((p_fEnable && (!p_fCapture)) || (!p_fEnable && (p_fCapture))),
"onmouseover", ButtonLabel.prototype.MouseOverHandler,
"onmouseout", ButtonLabel.prototype.MouseOutHandler
);
}
}
ButtonLabel.prototype.SetDimmer = function(p_fDim)
{
this.fDim = p_fDim;
this.spanText.style.color = (p_fDim || !this.fEnabled) ? "#999999" : this.strColor;
if (this.rollover)
{
this.rollover.SetDimmer(p_fDim)
}
}
ButtonLabel.prototype.SetEnabled = function(p_fEnabled)
{
if (p_fEnabled != this.fEnabled)
{
this.fEnabled = p_fEnabled;
this.HookupEvents(p_fEnabled);
if (!p_fEnabled)
{
if (this.fIsMouseDown)
{
this.spanLabel.releaseCapture();
}
this.fIsMouseOver = false;
this.fIsMouseDown = false;
this.MouseOutHandler();
}
this.spanText.style.color = (this.fDim || !p_fEnabled) ? "#999999" : this.strColor;
if (this.rollover)
{
this.rollover.SetEnabled(p_fEnabled)
}
this.spanLabel.style.cursor = p_fEnabled ? "hand" : "default";
}
}
ButtonLabel.prototype.SetText = function(p_strText)
{
this.strText = p_strText ? p_strText : "";
this.nobr.innerHTML = this.strText;
var strDisplay = this.spanText.style.display;
this.spanText.style.display = "inline";
this.iTextSpanWidth = this.spanText.clientWidth;
this.spanText.style.display = strDisplay;
this.ResizeHandler();
}
ButtonLabel.prototype.SetTextVisibility = function(p_fVisible)
{
this.fTextVisible = p_fVisible;
this.spanText.style.display = p_fVisible ? "inline" : "none";
this.ResizeHandler();
}
ButtonLabel.prototype.SetImageVisibility = function(p_fVisible)
{
this.fImageVisible = p_fVisible;
this.rollover.img.style.display = p_fVisible ? "inline" : "none";
this.ResizeHandler();
}
ButtonLabel.prototype.ResizeHandler = function()
{
var iRolloverWidth = this.rollover ? this.rollover.size.width : 0;
var fImageDefined = (iRolloverWidth > 0);
var fTextDefined = (this.strText != "");
var iActualSepWidth = (fImageDefined && fTextDefined) ? this.iSepWidth : 0;
this.iNeitherWidth = this.iPaddingLeft + this.iPaddingRight + 2;
this.iTextOnlyWidth = this.iNeitherWidth + this.iTextSpanWidth;
this.iImageOnlyWidth = this.iNeitherWidth + iRolloverWidth;
this.iTextAndImageWidth = this.iNeitherWidth + iRolloverWidth + iActualSepWidth + this.iTextSpanWidth;
this.fShowImage = this.fImageVisible && fImageDefined;
this.fShowText = this.fTextVisible && fTextDefined;
this.reserveSize.width = this.fShowImage ?
(this.fCanHideText ? this.iImageOnlyWidth : this.iTextAndImageWidth) :
this.iTextOnlyWidth;
this.onSizeChanged.Fire(this);
}
ButtonLabel.prototype.AllocateSize = function(p_iWidth, p_iHeight)
{
p_iWidth = Math.max(1, p_iWidth);
p_iHeight = Math.max(1, p_iHeight);
var fImage = false;
var fText = false;
var fOverflowed = false;
if (this.fShowImage && this.fShowText && (this.fCanClip || (p_iWidth >= this.iTextAndImageWidth)))
{
fOverflowed = (p_iWidth < this.iTextAndImageWidth);
this.size.width = Math.min(p_iWidth, this.iTextAndImageWidth);
this.spanText.iLeft = this.iPaddingLeft + this.rollover.size.width + this.iSepWidth;
this.spanText.style.left = this.spanText.iLeft
fImage = true;
fText = true;
}
else if (this.fShowImage && (p_iWidth >= this.iImageOnlyWidth))
{
this.size.width = this.iImageOnlyWidth;
fImage = true;
}
else if (this.fShowText && (this.fCanClip || (p_iWidth >= this.iTextOnlyWidth)))
{
fOverflowed = (p_iWidth < this.iTextOnlyWidth);
this.size.width = Math.min(p_iWidth, this.iTextOnlyWidth);
this.spanText.iLeft = this.iPaddingLeft;
this.spanText.style.left = this.spanText.iLeft
fText = true;
}
else
{
this.size.width = this.iNeitherWidth;
}
this.spanCover.style.width = this.spanLabel.style.width = this.size.width;
var strTitle = "";
if (this.fToolTips)
{
if (fOverflowed || (!fText))
{
strTitle = this.strText + " (" + this.strShortCutText + ")";
}
else if (this.strShortCutText)
{
strTitle = this.strShortCutText;
}
}
this.spanLabel.title = strTitle;
if (this.rollover)
{
this.rollover.SetTitle(strTitle, false);
this.rollover.SetTitle(strTitle, true);
}
this.spanText.style.display = fText ? "inline" : "none";
if (this.rollover)
{
this.rollover.img.style.display = fImage ? "inline" : "none";
}
}
ButtonLabel.prototype.RolloverResizeHandler = function()
{
this.ResizeHandler();
}
ButtonLabel.prototype.TextResizeHandler = function()
{
this.spanText.iTop = Math.floor((this.spanLabel.clientHeight - this.spanText.clientHeight) / 2);
this.spanText.style.top = this.spanText.iTop;
this.SetText(this.strText);
}
ButtonLabel.prototype.SetBorders = function(p_strTopLeftBorder, p_strBottomRightBorder)
{
this.spanLabel.style.borderLeft = p_strTopLeftBorder;
this.spanLabel.style.borderTop = p_strTopLeftBorder;
this.spanLabel.style.borderRight = p_strBottomRightBorder;
this.spanLabel.style.borderBottom = p_strBottomRightBorder;
}
ButtonLabel.prototype.OffsetElements = function(p_iOffset)
{
if (this.rollover)
{
this.rollover.element.style.pixelTop = this.rollover.iTop + p_iOffset;
this.rollover.element.style.pixelLeft = this.rollover.iLeft + p_iOffset;
}
this.spanText.style.pixelTop = this.spanText.iTop + p_iOffset;
this.spanText.style.pixelLeft = this.spanText.iLeft + p_iOffset;
}
ButtonLabel.prototype.Press = function()
{
if (this.fAllowSimulation)
{
this.DisplayPressed();
SafeSetTimeout(this.fncSimulateUp, 100);
}
}
ButtonLabel.prototype.DisplayNormal = function()
{
if (this.rollover)
{
this.rollover.DisplayNormal();
}
this.spanLabel.style.backgroundColor = this.strBackgroundColor;
this.SetBorders(this.strNormalBorder, this.strNormalBorder);
this.OffsetElements(0);
}
ButtonLabel.prototype.DisplayHover = function()
{
if (this.rollover)
{
this.rollover.DisplayHover();
}
this.spanLabel.style.backgroundColor = this.strHoverColor;
this.SetBorders(this.strLightBorder, this.strDarkColor);
this.OffsetElements(0);
}
ButtonLabel.prototype.DisplayPressed = function()
{
if (this.rollover)
{
this.rollover.DisplayPressed();
}
this.spanLabel.style.backgroundColor = this.strPressedColor;
this.SetBorders(this.strDarkColor, this.strLightBorder);
this.OffsetElements(1);
}
ButtonLabel.prototype.RolloverClickCallback = function(p_rollover, p_fOn)
{
if (this.fncOnClickHandler)
{
return(this.fncOnClickHandler(this, p_fOn));
}
}
ButtonLabel.prototype.DoClick = function()
{
this.Method_("DoActualClick")();
}
ButtonLabel.prototype.DoActualClick = function()
{
if (this.rollover)
{
this.rollover.DoClick();
}
else if (this.fncOnClickHandler)
{
this.fncOnClickHandler(this);
}
}
ButtonLabel.GetEventObject = function(p_obj)
{
if (p_obj.constructor == ButtonLabel)
{
return(p_obj);
}
else
{
var element = DomElement.FindAncestorWithProperty(window.event.srcElement, "oLabel");
return(element ? element.oLabel : null);
}
}
ButtonLabel.prototype.MouseOverHandler = function()
{
var label = ButtonLabel.GetEventObject(this);;
label.fIsMouseOver = true;
if (label.fIsMouseDown)
{
label.DisplayPressed();
}
else
{
label.DisplayHover();
}
}
ButtonLabel.prototype.MouseOutHandler = function()
{
var label = ButtonLabel.GetEventObject(this);;
label.fIsMouseOver = false;
label.DisplayNormal();
}
ButtonLabel.prototype.MouseDownHandler = function()
{
if (event.button != 1)
{
return;
}
var label = ButtonLabel.GetEventObject(this);;
label.fIsMouseDown = true;
label.DisplayPressed();
var oOffsetPoint = DomPositioning.FindOffsetInBody(label.spanLabel);
label.oLabelRect = new Data.Rectangle(oOffsetPoint, label.spanLabel.offsetWidth, label.spanLabel.offsetHeight);
label.spanLabel.setCapture();
label.HookupEvents(true, true);
}
ButtonLabel.prototype.MouseUpHandler = function()
{
if (event.button != 1)
{
return;
}
var label = ButtonLabel.GetEventObject(this);;
label.fIsMouseDown = false;
if (label.fIsMouseOver)
{
label.DoClick();
}
if (label.IsMouseInLabel() && this.fEnabled)
{
label.MouseOverHandler();
}
else
{
label.MouseOutHandler();
}
label.HookupEvents(false, true);
label.spanLabel.releaseCapture();
}
ButtonLabel.prototype.MouseMoveHandler = function()
{
if (this.IsMouseInLabel())
{
if (!this.fIsMouseOver)
{
this.MouseOverHandler();
}
}
else if (this.fIsMouseOver)
{
this.MouseOutHandler();
}
}
ButtonLabel.prototype.IsMouseInLabel = function()
{
if (this.oLabelRect)
{
var oPoint = DomEvents.GetPointFromBody();
return(oPoint.InRectangle(this.oLabelRect));
}
}
ButtonLabel.prototype.SimulateUp = function()
{
this.DoClick();
this.MouseOutHandler();
}
ButtonLabel.prototype.DoubleClickHandler = function()
{
}
if (ScriptReady)
{
ScriptReady();
}
