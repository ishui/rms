<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateType.aspx.cs" Inherits="RmsDM_FileTemplateType" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文档文档模版类别管理</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
</head>
<body>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">数据资料>文档模版类别
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
                                  <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" Width="100%" ImageSet="XPFileExplorer" NodeIndent="15" OnTreeNodeDataBound="TreeView1_TreeNodeDataBound">
                                        <DataBindings>
                                        <asp:TreeNodeBinding DataMember="root" TextField="Text" ValueField="Code" Target="frameMain"/>
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
								</TD>
								<TD vAlign="top" align="left">
									<iframe id="frameMain" name="frameMain" src='FileTemplateTypeList.aspx' width="100%"
													scrolling="auto" height="100%"></iframe>
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
