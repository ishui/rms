<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentManage.aspx.cs" Inherits="RmsDM_DocumentManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文档管理</title>
     <LINK href="../Images/index.css" type="text/css" rel="stylesheet">
     <script type="text/javascript" language="javascript" >
     function ShowWindow()
     {
        //window.open("DocumentFileList.aspx?NodeValue=0","main");
     }
     </script>
</head>
<body onload="ShowWindow();">
    <form id="form1" runat="server">
    <div>
    <table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">文档管理
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" width="150" id="tdFrameLeft">									
                     
                                    <asp:TreeView ID="CommonTreeView" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                        Width="100%" DataSourceID="XmlDataSource1"
                                        OnTreeNodeDataBound="CommonTreeView_TreeNodeDataBound">
                                        <DataBindings>
                                            <asp:TreeNodeBinding DataMember="root" 
                                                TextField="Text" ValueField="Code"
                                                 />
                                             <asp:TreeNodeBinding DataMember="node" 
                                                TextField="Text" ValueField="Code"
                                                 Target="main"/>    
                                        </DataBindings>
                                        <ParentNodeStyle Font-Bold="False" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                            VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px" />
                                    </asp:TreeView>
                                    <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="False"></asp:XmlDataSource>
                                </TD>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
    </div>
    </form>
</body>
</html>
