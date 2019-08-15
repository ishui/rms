<%@ Page language="c#" Inherits="RmsPM.Web.PicGroup.PicXMLData" CodeFile="PicXMLData.aspx.cs" %><?xml version="1.0" encoding="utf-8"?>
<data localMode="False">
	<player play="True" startIndex="0" loop="False">
		<speed min="1" max="30" default="5" />
		<size fullScreen="True" />
	</player>
	<slideData>
		<slides start="0" end="<%=strSlidesEnd%>">
			<%=strSlides%>
		</slides>
	</slideData>
</data>