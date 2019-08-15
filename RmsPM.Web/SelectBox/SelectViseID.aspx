<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectViseID.aspx.cs" Inherits="SelectBox_SelectViseID" %>

<%@ Register Src="../UserControls/DictionaryItem.ascx" TagName="DictionaryItem" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设计变更/签证 编号选择</title>
    <LINK href="../Images/index.css" type="text/css" rel="stylesheet">
    <style>
 .bButton
		{
			width: 100px;
			height: 20px;
			border: 1px;
			background-image: url(../bBtuttonBg.gif);
			background-repeat: no-repeat;
			background-color: #fff;			
			color: #666666;
			vertical-align: middle;
		}				
 </style>
</head>
<body onload="ConfigureDialog();" style=" padding:10px" >
    <form id="form1" runat="server">
    设计变更/签证  编号选择：<hr />
        <input id="P1"  runat="server"  readonly="readonly" style="width: 65px" class="input"/>-
        <input id="P2"  runat="server"  readonly="readonly" style="width: 65px" class="input"/>-
        <input id="P3"  runat="server"  readonly="readonly" style="width: 65px" class="input"/>-
        <input id="P4"  runat="server"  readonly="readonly" style="width: 65px" class="input"/>(<input id="P5"  runat="server"  readonly="readonly" style="width: 65px" class="input"/>)-
        <asp:Label ID="labID" runat="server" Text="####"></asp:Label><br />        
    <div>
        <uc1:DictionaryItem id="DictionaryItem1" runat="server"  Title="一" RepeatColumns=5>
        </uc1:DictionaryItem>
    </div>
    <div>
        <uc1:DictionaryItem id="DictionaryItem2" runat="server"  Title="二" RepeatColumns=5>
        </uc1:DictionaryItem>
    </div>
    <div>
        <uc1:DictionaryItem id="DictionaryItem3" runat="server" Title="三" RepeatColumns=5>
        </uc1:DictionaryItem>
    </div>
    <div>
        <uc1:DictionaryItem id="DictionaryItem4" runat="server"  Title="四" RepeatColumns=5>
        </uc1:DictionaryItem>
    </div>
    <div>   
        <uc1:DictionaryItem ID="DictionaryItem5" runat="server"  Title="五" RepeatColumns=5 />
        </div>
        
    </form>
    <center>	
		<button onclick="OK_Clicked(); return false;" class="bButton">确定</button> &nbsp;&nbsp;		
		<button onclick="Cancel_Clicked(); return false;" class="bButton">取消</button>	
			</center>
    <script type="text/javascript">
			//This code is used to provide a reference to the radwindow "wrapper"
			function GetRadWindow()
			{
				var oWindow = null;
				if (window.radWindow) oWindow = window.radWindow;
				else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
				return oWindow;
			}
			function ConfigureDialog()
			{
				var oWindow = GetRadWindow();
				var oArg = oWindow.Argument;
				document.getElementById("P1").value=oArg.p1;
				document.getElementById("P2").value=oArg.p2;
				document.getElementById("P3").value=oArg.p3;
				if(document.getElementById("P4")!=null){	document.getElementById("P4").value=oArg.p4;}
				if(!document.getElementById("P5")!=null){document.getElementById("P5").value=oArg.p5;}
									
			}
						
			function OK_Clicked()
			{
				var oWindow = GetRadWindow();
				var arg=new Object();
				arg.p1 = document.getElementById("P1").value;
				arg.p2 = document.getElementById("P2").value;
				arg.p3 = document.getElementById("P3").value;
				if(document.getElementById("P4")!=null)arg.p4 = document.getElementById("P4").value;	
				if(document.getElementById("P5")!=null)arg.p5 = document.getElementById("P5").value;				
				oWindow.CallBack(arg);
				oWindow.Close();						
						
			}
			
			function Cancel_Clicked()
			{
				var oWindow = GetRadWindow();			
				oWindow.Close();
			}				
		</script>	
</body>
</html>
