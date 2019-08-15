<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserGroupList.aspx.cs" Inherits="RmsOA_UserGroupList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <title>用户分组列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" style="height:100%" border="0" cellpadding="0">
    <tr style="height:100%;">
    <td id="frameLeft" style="width:150px;vertical-align:top;">
    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Contacts" NodeIndent="10" DataSourceID="XmlDataSource1" OnTreeNodeDataBound="TreeView1_TreeNodeDataBound" CollapseImageToolTip="折叠 ">
            <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
            <HoverNodeStyle Font-Underline="False" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
                <DataBindings>
                <asp:TreeNodeBinding Target="frameMain" TextField="Name" ValueField="Value" />
                </DataBindings>
        </asp:TreeView>
    </td>
    <td>
     <iframe src="UserGroupUserEdit.aspx" name="frameMain" scrolling="auto" width="100%" height="100%"></iframe>
    </td>
    </tr>
    </table>
        <asp:XmlDataSource EnableCaching="false" ID="XmlDataSource1" runat="server"/>
    </form>
</body>
</html>
