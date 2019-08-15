<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDocumentTemplate.aspx.cs"
    Inherits="SelectBox_SelectDocumentTemplate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择文件模板</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script src="TreeView.js" type="text/javascript"></script>

    <script type="text/javascript">
    function OnTreeNodeChecked() 
    { 
        var element = element = window.event.srcElement; 
        if (!IsCheckBox(element)) 
            return; 

        var isChecked = element.checked; 
        var tree = TV2_GetTreeById("TreeView1"); 
        var node = TV2_GetNode(tree,element); 

        TV2_SetChildNodesCheckStatus(node,isChecked); 

        var parent = TV2_GetParentNode(tree,node); 
        TV2_NodeOnChildNodeCheckedChanged(tree,parent,isChecked); 

    } 

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
                <tr>
                    <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                        选择模板</td>
                </tr>
                <tr height="100%">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td valign="top" width="150" id="tdFrameLeft">
                                    <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" Width="100%"
                                        ImageSet="XPFileExplorer" NodeIndent="15" OnTreeNodeDataBound="TreeView1_TreeNodeDataBound"
                                        ShowCheckBoxes="All" OnClick="OnTreeNodeChecked();">
                                        <DataBindings>
                                            <asp:TreeNodeBinding DataMember="root" TextField="Text" ValueField="Code" Target="frameMain" />
                                            <asp:TreeNodeBinding DataMember="node" TextField="Text" ValueField="Code" Target="frameMain" />
                                        </DataBindings>
                                        <ParentNodeStyle Font-Bold="False" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                            VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px" />
                                    </asp:TreeView>
                                    <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="False"></asp:XmlDataSource>
                                    <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="ChooseButton" runat="server" OnClick="Button1_Click" Text="确定" CssClass="Button" Visible="false" />
                                        <input id="CloseButton" type="button" value="关闭" onclick="javascript:window.close();" runat="server"
                                            class="button" visible="false" />
                                    </td>
                                </tr>
                            </table>
                                    
                                </td>
                                <td valign="top" align="left">
                                    <iframe id="frameMain" name="frameMain" src='SelectDocumentTemplatelist.aspx?ReturnFunction=<%=ViewState["ReturnFunc"]%>'
                                        width="100%" scrolling="auto" height="100%"></iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
