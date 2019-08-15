var timeStart = new Date();
function SafeSetTimeout(p_fnc, p_iTime)
{
 function Safe()
 {
 if (!blnIsUnloading)
 {
 p_fnc();
 }
 }
 window.setTimeout(Safe, p_iTime);
}
function Channel_OnDataReceived()
{
 var str = window.external.Channel.Data;
 // if fRemoteAppAvailable is undefined
 if (messenger.channel.fRemoteAppAvailable == (void 0))
 {
 log.Append("Received Init Message: " + str);
 // this message must be true or false
 //messenger.channel.fRemoteAppInitialized = true;
 messenger.channel.fRemoteAppAvailable = Convert.ToBool(str);
 if (messenger.channel.fncRemoteAppLoadedHandler)
 {
 log.Append("Calling RemoteAppLoadedHandler: " + str);
 messenger.channel.fncRemoteAppLoadedHandler();
 }
 }
 else if (messenger.channel.fncDataReceivedHandler)
 {
 messenger.channel.fncDataReceivedHandler(str);
 }
}
function Channel_OnIMReceived()
{
}
function Channel_OnFileReceived()
{
 if (messenger.channel.fncFileReceivedHandler)
 {
 messenger.channel.fncFileReceivedHandler(window.external.Channel.FileInfo);
 }
}
function Channel_OnFileProgress()
{
}
function Channel_OnSendFileCancelled()
{
}
function Channel_OnSendFileProgress()
{
}
function Channel_OnReceiveFileProgress()
{
}
function Channel_OnDataError()
{
 log.Append("Channel_OnDataError");
}
function Channel_OnTypeChange()
{
 var t = window.external.Channel.Type;
 var fConnected = (t != window.external.ConnectType.Disconnected);
 var fDirectConnection = (t == window.external.ConnectType.Direct);
 log.Append("Channel_OnTypeChange: connected:" + fConnected + ", direct:" + fDirectConnection);
 if (messenger.channel.fncTypeChangedHandler)
 {
 messenger.channel.fncTypeChangedHandler(fConnected, fDirectConnection);
 }
}
function Channel_OnAppClose()
{
}
function Channel_OnRemoteAppLoaded()
{
 log.Append("RemoteAppLoaded");
 messenger.channel.fRemoteAppInitialized = true;
 messenger.channel.SendStartMessage();
/*
 messenger.channel.fRemoteAppAvailable = true;
 if (messenger.channel.fncRemoteAppLoadedHandler)
 {
 messenger.channel.fncRemoteAppLoadedHandler();
 }
*/
}
function Channel_OnRemoteAppClosed()
{
 log.Append("Channel_OnRemoteAppClosed");
 messenger.channel.fRemoteAppAvailable = false;
 if (messenger.channel.fncRemoteAppClosedHandler)
 {
 messenger.channel.fncRemoteAppClosedHandler();
 }
}
function Messenger()
{
 this.ext_ = window.external;
 this.channel_ = this.ext_.Channel;
 this.users_ = this.ext_.Users;
 this.channel = new Messenger.Channel(this.channel_);
 this.users = new Messenger.Users(this.users_ );
}
Messenger.Users = function(p_users)
{
 this.users_ = p_users;
 this.me = this.users_.Me;
 this.fIsInviter = (this.me == this.users_.Inviter);
 for (var i=0; i < this.users_.Count; i++)
 {
 if (this.users_.Item(i) != this.me)
 {
 this.them = this.users_.Item(i);
 }
 }
}
Messenger.Channel = function(p_channel)
{
 this.channel_ = p_channel;
 this.fAvailable = false;
 this.fInitialized = false;
 this.fRemoteAppAvailable;
 this.fRemoteAppInitialized = false;
 this.fSentStartMessage = false;
}
Messenger.Channel.prototype.Send = function(p_var)
{
 this.channel_.SendData(p_var);
}
Messenger.Channel.prototype.SendFile = function(p_file)
{
 this.channel_.SendFile(p_file);
}
Messenger.Channel.prototype.SendIM = function(p_str)
{
 this.channel_.SendIM(p_str);
}
Messenger.Channel.prototype.Initialize = function(p_fAvailable)
{
 this.fAvailable = !!p_fAvailable;
 //Messenger.Channel.fAppAvailable = p_fAvailable;
 this.fInitialized = true;
 log.Append("Sending Initialize Message");
 this.channel_.Initialize();
 this.SendStartMessage();
 //window.setTimeout(Messenger.Channel.SendInitMessage, 1);
 //window.setTimeout(Messenger.Channel.SendInitMessage, !messenger.users.fIsInviter ? 1 : 5000);
}
Messenger.Channel.prototype.SendStartMessage = function()
{
 if (!this.fSentStartMessage)
 {
 if (this.fInitialized && this.fRemoteAppInitialized)
 {
 this.fSentStartMessage = true;
 log.Append("Sending Startup Message: " + this.fAvailable);
 this.Send(this.fAvailable);
 }
 }
}
/*
Messenger.Channel.SendInitMessage = function()
{
 log.Append("Sending Startup Message: " + Messenger.Channel.fAppAvailable);
 messenger.channel.Send(Messenger.Channel.fAppAvailable ? 1 : 0);
}
*/
var messenger = null;
///////////////////////////
// Convert Class
///////////////////////////
function Convert()
{
}
// Convert the given value to a boolean
Convert.ToBool = function(p_Value)
{
 if (p_Value == null)
 {
 return(false);
 }
 switch (typeof(p_Value))
 {
 case "undefined":
 return(false);
 case "number":
 return(p_Value != 0);
 case "string":
 return((p_Value != "") && (p_Value != "0") && (p_Value.toLowerCase() != "false"));
 case "boolean":
 return(p_Value);
 case "object":
 return(true);
 case "function":
 return(true);
 }
}
///////////////////////////
// Log Class
///////////////////////////
function Log()
{
 this.str = "";
}
Log.prototype.Append = function(p_str)
{
 this.str += "[" + (new Date()) + "]" + p_str + "\n";
}
///////////////////////////
// ObjectWrapper Class
///////////////////////////
// Wrap the given object inside the outer object
// useful when the inner object needs to be deleted
function ObjectWrapper(p_obj)
{
 this.obj = p_obj;
}
ObjectWrapper.prototype.Dispose = function()
{
 delete(this.obj);
}
///////////////////////////
// CreateSafeArray Class
///////////////////////////
function CreateSafeArray()
{
 var sa = CreateSafeArrayVB(arguments.length - 1);
 for (var i=0; i < arguments.length; i++)
 {
 sa = SetSafeArrayElementVB(sa, i, arguments[i]);
 }
 return(sa);
}
// Creates an object where each property is a querystring parameter
// If the same name is defined twice, then the last one defined is used
function NameValuePairs(p_str, p_strItemSep, p_strPairSep)
{
 var astrItems = p_str.split(p_strPairSep);
 var iItemSep;
 var str;
 var obj = new Object();
 for (var i=0; i < astrItems.length; i++)
 {
 str = astrItems[i];
 iItemSep = str.indexOf(p_strItemSep);
 if (iItemSep >= 0)
 {
 this[unescape(str.toLowerCase().substring(0, iItemSep))] = unescape(str.substring(iItemSep+1));
 }
 else
 {
 this[str.toLowerCase()] = null;
 }
 }
}
// Creates an object where each property is a querystring parameter
// If the same name is defined twice, then the last one defined is used
function QueryParams()
{
 this.fncNameValuePairs = NameValuePairs;
 this.fncNameValuePairs(document.location.search.substring(1), "=", "&");
 delete(this.fncNameValuePairs);
}
// Creates an object where each property is a querystring parameter
// If the same name is defined twice, then the last one defined is used
function Cookies()
{
 this.fncNameValuePairs = NameValuePairs;
 this.fncNameValuePairs(document.cookie, "=", ";");
 delete(this.fncNameValuePairs);
}
// calculates the base Url, which is everything from the beginning
// of the current URL up until, and including, the last slash /
function BaseUrl()
{
 var strUrl = document.location.href;
 var i = strUrl.indexOf('?');
 if (i >= 0)
 {
 strUrl = strUrl.substring(0, i);
 }
 i = strUrl.lastIndexOf('/');
 strUrl = strUrl.substring(0, i+1);
 return(strUrl);
}
///////////////////////////
// Config Class
///////////////////////////
// This class contains the configuration information for the slideshow viewer
function Config(p_xml)
{
 this.strVersion = p_xml.getAttribute("version");
 this.fExpired = Convert.ToBool(p_xml.getAttribute("expired"));
 this.strImagesDir = this.GetUrl(p_xml, "paths/images");
 this.strScriptDir = this.GetUrl(p_xml, "paths/scripts");
 this.strControlCodebase = this.GetUrl(p_xml, "paths/controlCodebase");
 this.strStringsFile = this.GetUrl(p_xml, "locale/stringsFile");
 this.strUpsellUrl = this.GetUrl(p_xml, "paths/upsell");
 this.strErrorUrl = this.GetUrl(p_xml, "paths/error");
 this.userCheckUrl = this.GetUrl(p_xml, "paths/userCheck");
 this.strCssUrl = this.GetUrl(p_xml, "paths/css");
 this.RequestTrackingImages(p_xml);
 this.SetSubscriptionStatus(p_xml);
 this.btnPrint = new Config.Button(p_xml.selectSingleNode("links/print"));
 this.btnDownload = new Config.Button(p_xml.selectSingleNode("links/download"));
 this.btnOrderPrints = new Config.Button(p_xml.selectSingleNode("links/orderPrints"));
 this.btnOrderGifts = new Config.Button(p_xml.selectSingleNode("links/orderGifts"));
}
Config.prototype.RequestTrackingImages = function(p_xml)
{
 var subNode = p_xml.selectSingleNode("trackingImageUrls");
 if (subNode && subNode.childNodes)
 {
 var img;
 for (var i=0; i < subNode.childNodes.length; i++)
 {
 img = document.createElement("img");
 img.src = subNode.childNodes[i].text;
 }
 }
}
Config.prototype.SetSubscriptionStatus = function(p_xml)
{
 var subNode = p_xml.selectSingleNode("subscription");
 if (subNode)
 {
 this.fIsSubscriber = Convert.ToBool(subNode.getAttribute("subscriber"));
 var fIsSubSimMode = Convert.ToBool(subNode.getAttribute("sim"));
 if (messenger && fIsSubSimMode)
 {
 this.fIsSubscriber = (messenger.users.me.Name.toLowerCase().indexOf("reach") == -1);
 }
 }
}
Config.prototype.GetSubscriptionStatus = function(p_fncCallback)
{
 if (this.fIsSubscriber == (void 0))
 {
 this.fncCallback = p_fncCallback;
 Config.singleton = this;
 var strUrl = this.userCheckUrl;
 strUrl = AddParam(strUrl, "t", queryParams["t"]);
 strUrl = AddParam(strUrl, "p", queryParams["p"]);
 strUrl = AddParam(strUrl, "did", queryParams["did"]);
 LoadXml(strUrl, Config.SubscriptionStatusCallback);
 }
 else
 {
 p_fncCallback(this.fIsSubscriber);
 }
}
Config.SubscriptionStatusCallback = function(p_xml)
{
 if (!p_xml)
 {
 Config.singleton.fIsSubscriber = false;
 }
 else
 {
 Config.singleton.SetSubscriptionStatus(p_xml.documentElement);
 }
 Config.singleton.fncCallback(Config.singleton.fIsSubscriber);
 Config.singleton = null;
}
// given, the xml node, and a path from that node, returns the URL
// defined in that tag. If the attribute rel is
Config.prototype.GetUrl = function(p_xml, p_strPath)
{
 var urlNode = p_xml.selectSingleNode(p_strPath);
 if (urlNode)
 {
 if (Convert.ToBool(urlNode.getAttribute("rel")))
 {
 return(strBaseUrl + urlNode.text);
 }
 else
 {
 return(urlNode.text);
 }
 }
 else
 {
 return("");
 }
}
// a config button contains an url and a boolean specifying whether it
// is enabled
Config.Button = function(urlNode)
{
 this.strUrl = urlNode.text;
 this.fEnabled = !!this.strUrl;
 this.fVisible = Convert.ToBool(urlNode.getAttribute("visible"))
}
///////////////////////////
// LocStrings Class
///////////////////////////
// Creates an object where each property is a string ID and the
// value is the localized string
function LocStrings(p_xml)
{
 if (!p_xml)
 {
 return;
 }
 var node;
 for (var i=0; i < p_xml.childNodes.length; i++)
 {
 node = p_xml.childNodes[i];
 if (node.nodeName == "Loc")
 {
 this[node.getAttribute("_locID")] = node.text;
 }
 }
}
// Load the specified xml document and call the function specifed upon success
function LoadXml(p_strUrl, p_fncLoadCallback, p_iAttempt)
{
 if (arguments.length == 0)
 {
 p_strUrl = LoadXml.strUrl;
 p_fncLoadCallback = LoadXml.fncLoadCallback;
 p_iAttempt = LoadXml.iAttempt;
 }
 log.Append("Loading XML: " + p_strUrl);
 var xmlElement = new ObjectWrapper(xmlToLoad);
 //var xmlElement = new ObjectWrapper(document.createElement("xml"));
 xmlElement.iLoadAttempts = p_iAttempt ? p_iAttempt : 1;
 var fnc = function()
 {
 if (xmlElement.obj.XMLDocument.readyState == 4)
 {
 if (xmlElement.obj.XMLDocument.documentElement)
 {
 log.Append("Loaded XML");
 p_fncLoadCallback(xmlElement.obj.XMLDocument);
 xmlElement.Dispose();
 }
 else
 {
 log.Append("Failed to load XML");
 xmlElement.Dispose();
 if (xmlElement.iLoadAttempts < 3)
 {
 LoadXml.strUrl = p_strUrl;
 LoadXml.fncLoadCallback = p_fncLoadCallback;
 LoadXml.iAttempt = xmlElement.iLoadAttempts + 1;
 window.setTimeout(LoadXml, 1000);
 //LoadXml(p_strUrl, p_fncLoadCallback, xmlElement.iLoadAttempts + 1);
 }
 else
 {
 log.Append("Giving up on XML: " + p_strUrl);
 p_fncLoadCallback(null);
 }
 }
 }
 }
 xmlElement.obj.onreadystatechange = fnc;
 //document.body.appendChild(xmlElement.obj);
 xmlElement.obj.src = p_strUrl;
}
// called by each of the scripts that gets included
function ScriptReady()
{
 ScriptReady.iCount++;
}
ScriptReady.iCount = 0;
// Load the specified javascript file and call the function specifed upon success
function LoadScript(p_strSrc, p_fncLoadCallback, p_iAttempt)
{
 log.Append("Loading script: " + p_strSrc);
 var scriptElement = new ObjectWrapper(document.createElement("script"));
 scriptElement.iLoadAttempts = p_iAttempt ? p_iAttempt : 1;
 var iPreviousScriptsReady = ScriptReady.iCount;
 var fnc = function()
 {
 if ((scriptElement.obj.readyState == "complete") || (scriptElement.obj.readyState == "loaded"))
 {
 if (ScriptReady.iCount == (iPreviousScriptsReady + 1))
 {
 // success
 log.Append("Loaded script");
 scriptElement.obj.detachEvent("onreadystatechange", fnc);
 p_fncLoadCallback(scriptElement.obj);
 scriptElement.Dispose();
 }
 else
 {
 log.Append("Failed to load script");
 scriptElement.Dispose();
 if (scriptElement.iLoadAttempts < 3)
 {
 LoadScript(p_strSrc, p_fncLoadCallback, scriptElement.iLoadAttempts + 1);
 }
 else
 {
 log.Append("Giving up on script: " + p_strSrc);
 p_fncLoadCallback(null);
 }
 }
 }
 }
 scriptElement.obj.language = "JScript";
 scriptElement.obj.attachEvent("onreadystatechange", fnc);
 scriptElement.obj.src = p_strSrc;
 document.body.appendChild(scriptElement.obj);
}
function AddParam(p_strSrc, p_name, p_value)
{
 var strParam = p_name + "=" + p_value;
 if (p_strSrc && p_value)
 {
 if (p_strSrc.indexOf("?") == -1)
 {
 return(p_strSrc + "?" + strParam);
 }
 else
 {
 return(p_strSrc += "&" + strParam);
 }
 }
 return(p_strSrc);
}
var scripts = ["slideshow"];
var strBaseUrl;
var queryParams;
var cookies;
var config;
var viewer;
var DEBUG = false;
var log = new Log();
function LoadingError(p_strMessage)
{
 cellLoading.innerHTML = "";
 cellLoading.style.removeExpression("fontSize");
 cellLoading.style.setExpression("fontSize", "document.body.clientWidth / 30");
 if (messenger)
 {
 messenger.channel.Initialize(false);
 }
 else if (config)
 {
 if (p_strMessage)
 {
 cellLoading.innerHTML = p_strMessage;
 }
 else if (config.strings)
 {
 cellLoading.innerHTML = config.strings.RES_ErrorMessage;
 }
 else if (config.strErrorUrl)
 {
 window.location.href = config.strErrorUrl;
 }
 }
}
function Start()
{
 if (blnIsUnloading) return;
 log.Append("BodyLoaded");
 window.status = " ";
 document.all.frameControl.src = "";
 strBaseUrl = BaseUrl();
 log.Append("strBaseUrl: " + strBaseUrl);
 queryParams = new QueryParams();
 cookies = new Cookies();
 /*
 var strCookies = "";
 for (var c in cookies)
 {
 strCookies += c + ":" + cookies[c] + "\n";
 }
 alert(strCookies);
 */
 log.Append("ReloadCount:" + cookies["reloadcount"]);
 DEBUG = Convert.ToBool(queryParams["debug"]);
 if (!queryParams["config"])
 {
 queryParams["config"] = "config.xml";
 }
queryParams["data"]=thePicGroupXMLData;
//queryParams["config"] = "config.xml"
 if (window.external && window.external.channel)
 {
 messenger = new Messenger();
 var strConfig = queryParams["config"];
 strConfig = AddParam(strConfig, "t", queryParams["t"]);
 strConfig = AddParam(strConfig, "p", queryParams["p"]);
 strConfig = AddParam(strConfig, "did", queryParams["did"]);
 strConfig = AddParam(strConfig, "inviter", messenger.users.fIsInviter ? "1" : "0");
 queryParams["config"] = strConfig;
 }
 log.Append("loading config file: " + queryParams["config"]);
 LoadXml(queryParams["config"], ConfigLoaded);
}
function ConfigLoaded(p_xml)
{
 if (!p_xml)
 {
 LoadingError();
 return;
 }
 log.Append("loaded config file");
 config = new Config(p_xml.documentElement);
 config.messenger = messenger;
 config.fIsLocal = (document.location.protocol == "file:");
 config.strBaseUrl = strBaseUrl;
 config.queryParams = queryParams;
 config.cookies = cookies;
 locCss.href = config.strCssUrl;
 var strStringsFile = config.strStringsFile.replace("[locale]", queryParams["locale"]);
 //strStringsFile = AddParam(strStringsFile, "version", config.strVersion);
 log.Append("loading strings file: " + strStringsFile);
 LoadXml(strStringsFile, StringsLoaded);
}
function StringsLoaded(p_xml)
{
 if (!p_xml)
 {
 LoadingError();
 return;
 }
 log.Append("loaded strings file");
 config.strings = new LocStrings(p_xml ? p_xml.documentElement : null);
 if (config.fExpired)
 {
 alert(config.strings.RES_ExpiredMessage)
 LoadingError(config.strings.RES_ExpiredMessage);
 return;
 }
 cellLoading.innerHTML = config.strings.RES_SlideViewer_LoadingPleaseWait;
 scripts.iNumLoaded = 0;
 if (scripts.length > 0)
 {
 var strSrc = config.strScriptDir + scripts[0] + config.strVersion + ".js";
 log.Append("loading script file: " + strSrc);
 LoadScript(strSrc, ScriptLoaded);
 }
}
function ScriptLoaded(p_script)
{
 if (!p_script)
 {
 LoadingError();
 return;
 }
 log.Append("loaded script file");
 scripts.iNumLoaded++;
 if (scripts.iNumLoaded == scripts.length)
 {
 // All the scripts are loaded
 log.Append("Creating viewer object");
 viewer = new Viewer(divMain, config, log, ViewerLoaded);
 }
 else
 {
 var strSrc = config.strScriptDir + scripts[scripts.iNumLoaded] + config.strVersion + ".js";
 log.Append("loading script file: " + strSrc);
 LoadScript(strSrc, ScriptLoaded);
 }
}
function ViewerLoaded()
{
 if (!viewer.fReady)
 {
 log.Append("Viewer object failed to load");
 LoadingError();
 return;
 }
 log.Append("Viewer object loaded");
 log.Append("loading time:" + (new Date() - timeStart));
 if (messenger && messenger.users.fIsInviter)
 {
 GetActiveXControl(ControlDone, config);
 }
 else
 {
 ControlDone();
 }
}
function ControlDone()
{
 cellLoading.style.removeExpression("fontSize");
 tblLoading.removeNode(true);
 viewer.Go();
}
function Finish()
{
 if (GetActiveXControl.ctrl)
 {
 try
 {
 GetActiveXControl.ctrl.DeleteTempFiles();
 }
 catch(e)
 {
 }
 }
 if (viewer)
 {
 Dispose(viewer);
 }
 divMain.removeNode(true);
}
var iDebugClickCount = 0;
var iLastX = Number.MIN_VALUE;
var iLastY = Number.MIN_VALUE;
function DebugClicked()
{
 var e = window.event;
 if ((e.clientX > iLastX) && (e.clientY > iLastY))
 {
 iDebugClickCount++;
 if (iDebugClickCount >= 10)
 {
 var wnd = window.open("", "DebugWindow");
 wnd.document.open();
 wnd.document.write("<pre>" + log.str + "</pre>");
 wnd.document.close();
 //alert(log.str);
 }
 }
 else
 {
 iDebugClickCount = 1;
 }
 iLastX = e.clientX;
 iLastY = e.clientY;
}
function FrameLoaded(p_window)
{
 log.Append("ActiveX control frame loaded");
 if (GetActiveXControl.fncCallback)
 {
 GetActiveXControl.ctrl = p_window.document.all["objPhotoPicker"];
 if (!GetActiveXControl.ctrl.Ready)
 {
 GetActiveXControl.ctrl = null;
 }
 log.Append("Making ActiveX control loaded callback");
 GetActiveXControl.fncCallback(GetActiveXControl.ctrl);
 }
}
function GetActiveXControl(p_fncCallback, p_config)
{
 if (GetActiveXControl.ctrl == null)
 {
 log.Append("Getting ActiveX control");
 GetActiveXControl.fncCallback = p_fncCallback;
 document.all.frameControl.src = "ActiveXControl.htm?clsid=6E2D6932-3885-4FA2-8DD4-DB63FFE33797&id=objPhotoPicker&codebase=" + escape(p_config.strControlCodebase) + "&fnc=FrameLoaded";
 alert(document.all.frameControl.src)
 }
 else
 {
 p_fncCallback(GetActiveXControl.ctrl);
 }
}
GetActiveXControl.ctrl = null;
BodyLoaded();
