<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OfficeHelpLink.ascx.cs" Inherits="OfficeHelpLink" %>
<script language="javascript" type="text/javascript">

    function mousein( contrlID)
    {
        var cnt = document.getElementById("contrlID");
        
        cnt.style.borderCollapse = "collapse"
    }
    function mouseout()
    {}
    function openExcel()
    {
        var Excel, doc;
        Excel = new ActiveXObject("Excel.Application");
        Excel.Visible = true;
        doc = Excel.Workbooks.Add();
    }
    
    function openWord()
    {
        var Word,doc;
        Word = new ActiveXObject("Word.Application");
        Word.Visible = true;
        doc = Word.Documents.Add();
    }
    
    function openPowerPoint()
    {
        var PowerPoint,doc;
        PowerPoint = new ActiveXObject("PowerPoint.Application");
        PowerPoint.Visible = true;
        doc = PowerPoint.Presentations.Add(); 
    }
    function openOutLook()
    {
        var OutLook,doc
        OutLook = new ActiveXObject("Outlook.Application");
        doc = OutLook.CreateItem(0);
        doc.display(true);
    }
    function openAccess()
    {
        var Aceess;
        Access = new ActiveXObject("Access.Application");
        Access.Visible = true;
    }
</script>

<div>
    <img src="Images/officeWord.jpg" id="word" style="cursor: hand;" onclick="openWord()"
        alt="Word" />
    <img src="Images/officeExcel.jpg" id="excel" style="cursor: hand;" onclick="openExcel()"
        alt="Excel" />
    <img src="Images/officePPT.jpg" id="powerpoint" style="cursor: hand;" onclick="openPowerPoint()"
        alt="PowerPoint" />
    <img src="Images/officeOutLook.jpg" id="outlook" style="cursor: hand;" onclick="openOutLook()"
        alt="OutLook" />
    <img src="Images/officeAccess.jpg" id="access" style="cursor: hand;" onclick="openAccess()"
        alt="Access" />
</div>
