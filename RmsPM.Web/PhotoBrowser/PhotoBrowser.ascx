<%@ Control Language="c#" Inherits="Codefresh.PhotosBrowser.PhotoBrowser" CodeFile="PhotoBrowser.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="squishyWARE.WebComponents.squishyTREE" Assembly="squishyTREE" %>

<LINK REL=STYLESHEET TYPE="text/css" HREF="PhotoBrowserRes\PhotoBrowser.css">

<style type="text/css">

DIV#tipDiv { BORDER-RIGHT: #336 1px solid; PADDING-RIGHT: 4px; BORDER-TOP: #336 1px solid; PADDING-LEFT: 4px; FONT-SIZE: 11px; Z-INDEX: 10000; LEFT: 0px; VISIBILITY: hidden; PADDING-BOTTOM: 4px; BORDER-LEFT: #336 1px solid; COLOR: #000; LINE-HEIGHT: 1.2; PADDING-TOP: 4px; BORDER-BOTTOM: #336 1px solid; POSITION: absolute; TOP: 0px; BACKGROUND-COLOR: #dee7f7 }

</style>

<script type="text/javascript">

	function doTooltip(e, msg)
	{
		if ( typeof Tooltip == "undefined" || !Tooltip.ready ) return;
		Tooltip.show(e, msg);
	}

	function hideTip()
	{
		if ( typeof Tooltip == "undefined" || !Tooltip.ready ) return;
		Tooltip.hide();
	}

	</script>

<P/>
<TABLE id=Table2 style="WIDTH: 100%; HEIGHT: 80%" cellSpacing=0 cellPadding=0 
border=0 bgColor="#e4eff6">
  <TBODY>
  <TR>
    <TD style="WIDTH: 210px" vAlign=top >
      <TABLE id=Table4 cellSpacing=0 cellPadding=0 border=0>
        <TR>
          <TD style="WIDTH: 300px; HEIGHT: 290px" vAlign=top 
          ><cc1:treeview id=tvwMain 
            Width="238px" runat="server" onselectednodechanged="tvwMain_SelectedNodeChanged"></cc1:treeview><br />             
                  当前路径：<asp:Label ID="CurDir" runat="server" Text=" " Width="202px"></asp:Label>
                  &nbsp;&nbsp;
                  <div id="DirMaintain" runat="server">
                  <br /> [<a href=# onclick="JavaScript:AddChildDir();" runat="server" id="hrefAddChildDir">新增子目录</a>]
                  <!--<li><a href=# onclick="JavaScript:DelDir();">删除目录</a></li>
                  <li><a href=# onclick="JavaScript:DirRename();">更改目录名</a></li>-->
                  <br /><input type=hidden id="txtDirName" runat="server"/>
                  <input type=hidden id="DirMaintainType"  runat="server"/>&nbsp;<br />
                  </div>
              <p/> 
              
                      &nbsp;
          </TD>
          <TD style="WIDTH: 300px; HEIGHT: 290px" vAlign=top 
          >&nbsp;&nbsp;&nbsp; </TD></TR>
        <TR>
          <TD style="display=none;WIDTH: 300px;"><asp:panel 
            id=pnlComments runat="server" 
            Visible="False"><SPAN class=smallText/>
            <TABLE id=Table3 cellSpacing=0 cellPadding=0 border=0>
              <TR>
                <TD height=10>
                  <HR align=left width="80%" SIZE=1>
                </TD></TR>
              <TR>
                <TD>
                  <TABLE id=Table5 cellSpacing=0 cellPadding=0 border=0>
                    <TR>
                      <TD style="HEIGHT: 20px">Comments:</TD></TR>
                    <TR>
                      <TD><asp:Literal id=litPhotoComments runat="server"></asp:Literal></TD></TR>
                    <TR>
                      <TD>
                        <TABLE id=Table6 cellSpacing=1 cellPadding=1 border=0>
                          <TR>
                            <TD width=40>Name</TD>
                            <TD><asp:TextBox id=txtCommentName runat="server" Width="131px" MaxLength="20" CssClass="text"></asp:TextBox></TD></TR>
                          <TR>
                            <TD style="HEIGHT: 26px" width=50>Comment</TD>
                            <TD style="HEIGHT: 26px"><asp:TextBox id=txtCommentText runat="server" Width="100%" MaxLength="150" CssClass="text"></asp:TextBox></TD></TR>
                          <TR>
                            <TD><asp:Button id=btnAddComment runat="server" Text="Add" onclick="btnAddComment_Click"></asp:Button></TD>
                            <TD><asp:RequiredFieldValidator id=RequiredFieldValidator1 runat="server" ControlToValidate="txtCommentName" ErrorMessage="Name is required"></asp:RequiredFieldValidator><BR><asp:RequiredFieldValidator id=RequiredFieldValidator2 runat="server" ControlToValidate="txtCommentText" ErrorMessage="Comment is required"></asp:RequiredFieldValidator></TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE></asp:panel></TD>
          <TD style="WIDTH: 300px"></TD></TR></TABLE></TD>
    <TD vAlign=top width="85%"><asp:panel 
      id=pnlPhotoGridContents Width="393px" Height="196px" 
      runat="server" Visible="False" HorizontalAlign="Left">
      <TABLE id=tblPhotoGridContents cellSpacing=0 cellPadding=0 border=0>
        <TR>
          <TD class=pageNavCell style="HEIGHT: 25px" align=center>
            <TABLE align=center border=0>
              <TR>
                <TD class=pageNavCell style="WIDTH: 23px"><asp:HyperLink id=hylPreviousPage1 runat="server" ToolTip="Previous Page" ImageUrl="PhotoBrowserResprevious.gif"></asp:HyperLink></TD>
                <TD class=pageNavCell>
                    当前页:&nbsp; <asp:PlaceHolder id=plhPageLinks1 runat="server"></asp:PlaceHolder><SPAN 
                  class=currentPageNum>&nbsp; </SPAN></TD>
                <TD class=pageNavCell align=center><asp:HyperLink id=hylNextPage1 runat="server" CssClass="pageNavCell" ToolTip="Next Page" ImageUrl="PhotoBrowserResnext.gif"></asp:HyperLink></TD></TR></TABLE></TD></TR>
        <TR>
          <TD><asp:Table id=tblPhotos runat="server" CellSpacing="10" BorderStyle="None" CellPadding="4" EnableViewState="False"></asp:Table></TD></TR>
        <TR>
          <TD class=pageNavCell align=center>
            <TABLE align=center border=0>
              <TR>
                <TD class=pageNavCell><asp:HyperLink id=hylPreviousPage2 runat="server" ToolTip="Previous Page" ImageUrl="PhotoBrowserResprevious.gif"></asp:HyperLink></TD>
                <TD class=pageNavCell>
                    当前页:&nbsp; <asp:PlaceHolder id=plhPageLinks2 runat="server"></asp:PlaceHolder><SPAN 
                  class=currentPageNum>&nbsp; </SPAN></TD>
                <TD class=pageNavCell align=center><asp:HyperLink id=hylNextPage2 runat="server" CssClass="pageNavCell" ToolTip="Next Page" ImageUrl="PhotoBrowserResnext.gif"></asp:HyperLink></TD></TR></TABLE></TD></TR></TABLE>
      <DIV>
      <DIV>&nbsp;</DIV></DIV></asp:panel><asp:panel id=pnlPhotoContents 
       Width="389px" runat="server" Visible="False">
      <TABLE id=Table1 cellSpacing=0 cellPadding=0 border=0>
        <TR>
          <TD>
            <TABLE id=Table7 cellPadding=5 align=center border=0>
              <TR>
                <TD align=center width="33%"><asp:HyperLink id=hlkPreviousImage runat="server" ToolTip="Previous Page" ImageUrl="PhotoBrowserResprevious.gif"></asp:HyperLink><BR>
                    上张
                </TD>
                <TD vAlign=bottom align=center width="34%">
                  <DIV align=center><SPAN class=smallText><asp:HyperLink id=hlkReturnToThumbnails1 runat="server" ToolTip="Return to Thumbnails" ImageUrl="PhotoBrowserRes/index.gif"></asp:HyperLink><BR>
                      回到列表</SPAN></DIV></TD>
                <TD align=center width="33%"><asp:HyperLink id=hlkNextImage runat="server" CssClass="pageNavCell" ToolTip="Next Page" ImageUrl="PhotoBrowserResnext.gif"></asp:HyperLink><BR><SPAN 
                  class=smallText>下张</SPAN></TD></TR></TABLE></TD></TR>
        <TR>
          <TD align=left>
            <TABLE>
              <TR>
                <TD><asp:Image id=imgPhoto runat="server"></asp:Image></TD></TR></TABLE></TD></TR>
        <TR>
          <TD align=center height=40><asp:Label id=lblViewedCount runat="server" Visible="False">Viewed Count</asp:Label><a href=# onclick="JavaScript:DelPhoto();" runat="server" id="btnDelPhoto">[删除]</a></TD></TR>
        <TR>
          <TD><asp:Panel id=pnlImageNavigationBottom runat="server">
            <TABLE id=Table9 width="100%">
              <TR><!-- Previous thumbnail -->
                <TD style="WIDTH: 150px" align=left width=150 height="100%" 
                rowSpan=2>
                  <TABLE id=Table8>
                    <TR>
                      <TD class=tdNav align=center>
                        <DIV><asp:HyperLink id=hlkPreviousImagePhoto runat="server" CssClass="navImg" ToolTip="Previous Image">Previous Image</asp:HyperLink></DIV>
                        <DIV><asp:HyperLink id=hlkPreviousImageName runat="server">Name</asp:HyperLink></DIV></TD></TR></TABLE></TD><!-- Current image comments -->
                <TD style="HEIGHT: 20px" vAlign=top align=center 
                width="34%"></TD><!-- Next thumbnail -->
                <TD align=right width="33%" height="100%" rowSpan=2>
                  <TABLE id=Table10>
                    <TR>
                      <TD class=tdNav align=center>
                        <DIV><asp:HyperLink id=hlkNextImagePhoto runat="server" ToolTip="Next Image">Next Image</asp:HyperLink><BR><asp:HyperLink id=hlkNextImageName runat="server">Name</asp:HyperLink></DIV></TD></TR></TABLE></TD></TR>
              <TR>
                <TD vAlign=bottom align=center width="34%">
                  <DIV align=center><SPAN class=smallText><asp:HyperLink id=hlkReturnToThumbnails2 runat="server" ToolTip="Return to Thumbnails" ImageUrl="PhotoBrowserRes/index.gif"></asp:HyperLink><BR>
                      回到列表</SPAN></DIV></TD></TR></TABLE></asp:Panel></TD></TR></TABLE></asp:panel></TD></TR>
                      <tr><td colspan=2>
                  </td></tr>
                      </TBODY></TABLE>
                      <div id="divUpload" runat="server" align=center style ="background-color: #e4eff6">上传文件:<asp:FileUpload ID="FileUpload1" runat="server" Width="500px" />
                  <asp:Button ID="btnMaintain" runat="server" Text="上传" /><br /><asp:Label ID="labReport" runat="server" Text="."></asp:Label></div>
        <script language="javascript">
     function AddChildDir()
        {
        var maintaintype;
        maintaintype=document.getElementById ('PhotoBrowser1:DirMaintainType');
        maintaintype.value="AddChildDir";
        var dirname=document.getElementById ('PhotoBrowser1:txtDirName');
        dirname.style.display="";
        var name=window.prompt("请输入新文件夹名称");
        if (name!=null && name!="")
        {
            dirname.value=name;
            Form1.submit();
        }
        return false;
      }
      function DelPhoto()
        {
        var maintaintype;
        maintaintype=document.getElementById ('PhotoBrowser1:DirMaintainType');
        maintaintype.value="DelPhoto";
        Form1.submit();
      }
        
         function DelDir()
        {
        var maintaintype=document.getElementById ('PhotoBrowser1:DirMaintainType');
        maintaintype.value="DelDir";        
        //var btnMaintain=document.getElementById("btnMaintain");
       // btnMaintain.style.display="";
        return false;
        }
         function DirRename()
        {
        var maintaintype=document.getElementById ('PhotoBrowser1:DirMaintainType');
        maintaintype.value="DirRename";
        var dirname=document.getElementById ('PhotoBrowser1:DirName');
        dirname.style.display="";
       // var btnMaintain=document.getElementById("btnMaintain");
        //btnMaintain.style.display="";
        return false;
        }
        </script>
