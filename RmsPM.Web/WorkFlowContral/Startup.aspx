<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Startup.aspx.cs" Inherits="WorkFlowContral_WorkFlowStartup" %>

<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>启动流程</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>

    <script language="javascript" type="text/javascript">
    
    
    detachEvent  ('onbeforeunload', divdisplay);
  
    
    function modifyProcedure(ProcedurePath,ProcedureCode,FileTemplateCode,DirectoryCode)
    {
        OpenFullWindow(ProcedurePath+"?ProcedureCode="+ProcedureCode+"&FileTemplateCode="+FileTemplateCode+"&DirectoryCode="+DirectoryCode+"&status=new");
       
        
    }
	function gotoDirect ( caseCode, actCode , path , applicationCode)
	{ 
		OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+'action=Sign&CaseCode='+caseCode + '&actCode=' + actCode + "&applicationCode=" + applicationCode ,'流程处理');
	}
	
    </script>

    <form id="Form1" method="post" runat="server">
       
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select groupcode,groupname from systemGroup where classcode='0902'"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select wfp.ProcedureCode,wfp.Description,wfp.ProcedureName ,wfp.Type,wfp.SysType ,wfp.Applicationpath from Workflowprocedure wfp where wfp.Activity=1 and wfp.Type=1 and systype=@sysType">
            <SelectParameters>
                <asp:QueryStringParameter Name="sysType" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        &nbsp;
    </form>
    
</body>
</html>
