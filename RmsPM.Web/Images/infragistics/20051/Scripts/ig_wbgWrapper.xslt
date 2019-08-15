<?xml version="1.0" encoding="UTF-8"?>
<!-- 
Infragistics UltraWebGrid Script 
Version 4.3.20043.27
Copyright (c) 2001-2004 Infragistics, Inc. All Rights Reserved.
-->
<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	
	xmlns:wbg="http://schemas.infragistics.com/WebGrid" 
	xmlns:lit="http://schemas.infragistics.com/WebGrid/Literal"
	xmlns="http://schemas.infragistics.com/WebGrid"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >

	<xsl:output omit-xml-declaration="yes" method="html" indent="no"/>
	<xsl:variable name='uwgID' select='/wbg:UltraWebGrid/@id' />

<xsl:template match='/'>
<table 
	border='0'
	cellpadding='0'
	cellspacing='0'
	id='{$uwgID}_main'
	style="{wbg:UltraWebGrid/wbg:Header/wbg:Layout/@frameStyle}" 
	onfocus="igtbl_activate('{$uwgID}');" 
	onresize="igtbl_onResize('{$uwgID}');" 
	onmousemove="igtbl_tableMouseMove(event,'{$uwgID}');"
	onmouseup="igtbl_tableMouseUp(event,'{$uwgID}');" >

<xsl:apply-templates select="wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:GroupByBox[@where='top']" />
<xsl:apply-templates select="wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:AddNewBox[@where='top']" />

<tr>
<td>

<div id="{$uwgID}_div" style="{wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:Frame/@style}{wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:Frame/@overflow}" >
	<xsl:apply-templates select='wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:Frame/@lit:*' />

	<table>
		<xsl:apply-templates select="wbg:UltraWebGrid/wbg:Header/wbg:Layout" mode="band" />
		<tbody>
		</tbody>
	</table>
</div>

</td>
</tr>

<xsl:apply-templates select="wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:GroupByBox[@where='bottom']" />
<xsl:apply-templates select="wbg:UltraWebGrid/wbg:Header/wbg:Layout/wbg:AddNewBox[@where='bottom']" />

</table>
</xsl:template>

<xsl:template match="wbg:Layout" mode="band">
	<xsl:param name="num">1</xsl:param>
	
	<xsl:apply-templates select="wbg:Bands/wbg:Band[$num]" />

</xsl:template>

<xsl:template match="wbg:GroupByBox" >
	<tr style="height:{@height};">
		<td>
			<xsl:apply-templates select="@lit:*" />
			<xsl:choose>
				<xsl:when test="wbg:Item[1]">
					<table 
							border='0' 
							cellpadding='0' 
							cellspacing='0' 
							onmousedown="igtbl_headerClickDown(event, '{$uwgID}')" 
							onmouseup="igtbl_headerClickUp(event,'{$uwgID}')" 
							onmouseout="igtbl_headerMouseOut(event,'{$uwgID}')" 
							onmousemove="igtbl_headerMouseMove(event,'{$uwgID}')" 
							onmouseover="igtbl_headerMouseOver(event,'{$uwgID}')" >
							
						<xsl:variable name="iMax" select="count(wbg:Item)" />
						<xsl:for-each select="wbg:Item" >
							<tr>
								<xsl:call-template name="doGroupByItem" >
									<xsl:with-param name="len" select="$iMax" />
									<xsl:with-param name="idx" select="position()" />
									<xsl:with-param name="ctr" select="1" />
									<xsl:with-param name="item" select="." />
								</xsl:call-template>
							</tr>
						</xsl:for-each> 
					</table>
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates select="@prompt" />&#xa0;
				</xsl:otherwise>
			</xsl:choose>
		</td>
	</tr>
</xsl:template>

<xsl:template name="doGroupByItem">
<xsl:param name="len" />
<xsl:param name="idx" />
<xsl:param name="ctr" />
<xsl:param name="item" />
	<xsl:choose>
		<xsl:when test="(number($idx)-1)=number($ctr)">
			<td>
				<xsl:if test="$item/../@btnConnStyle">
					<xsl:variable name='btnStroke' select="$item/../@btnConnColor" />
					<table width='100%' height='100%' border='0' cellpadding='0' cellspacing='0' style='font-size:4pt;' >
						<tr height='50%'><td width='50%'></td>
						<xsl:choose>
							<xsl:when test="$btnStroke">
								<td style='border-style:{$item/../@btnConnStyle};border-color:{$btnStroke};border-left-width:1;border-top-width:0;border-right-width:0;border-bottom-width:1;'>&#xa0;</td>
							</xsl:when>
							<xsl:otherwise>
								<td style='border-style:{$item/../@btnConnStyle};border-left-width:1;border-top-width:0;border-right-width:0;border-bottom-width:1;'>&#xa0;</td>
							</xsl:otherwise>
						</xsl:choose>
						</tr><tr><td>&#xa0;</td><td></td></tr>
					</table>
				</xsl:if>
			</td>
			<xsl:call-template name="doGroupByItem">
				<xsl:with-param name="len" select="$len" />
				<xsl:with-param name="idx" select="$idx" />
				<xsl:with-param name="ctr" select="$ctr+1" />
				<xsl:with-param name="item" select="$item" />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="number($ctr) &lt; number($idx)">
			<td></td>
			<xsl:call-template name="doGroupByItem">
				<xsl:with-param name="len" select="$len" />
				<xsl:with-param name="idx" select="$idx" />
				<xsl:with-param name="ctr" select="$ctr+1" />
				<xsl:with-param name="item" select="$item" />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="number($idx) = number($ctr)" >
			<td style="padding-right:5;">
				<div>
					<xsl:apply-templates select="$item/@lit:*" />
					<xsl:choose>
						<xsl:when test="$item/@bandText">
							&#xa0;<xsl:value-of select="@bandText" disable-output-escaping="yes" />&#xa0;
						</xsl:when>
						<xsl:otherwise>
							<xsl:copy-of select="$item/wbg:*|text()" />
						</xsl:otherwise>
					</xsl:choose>
				</div>
			</td>
			<xsl:call-template name="doGroupByItem">
				<xsl:with-param name="len" select="$len" />
				<xsl:with-param name="idx" select="$idx" />
				<xsl:with-param name="ctr" select="$ctr+1" />
				<xsl:with-param name="item" select="$item" />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="number($ctr) &lt;= number($len)" >
			<xsl:call-template name="doGroupByItem">
				<xsl:with-param name="len" select="$len" />
				<xsl:with-param name="idx" select="$idx" />
				<xsl:with-param name="ctr" select="$ctr+1" />
				<xsl:with-param name="item" select="$item" />
			</xsl:call-template>
			<td></td>
		</xsl:when>
	</xsl:choose>
</xsl:template>

<xsl:template match="wbg:Band">
	<xsl:apply-templates select="wbg:Wrapper" />
	<colgroup>
		<xsl:apply-templates select="wbg:RowSelector" mode="colgroup" />
		<xsl:apply-templates select="wbg:Columns/wbg:Column[@grouped]" mode="colgroup" />
	</colgroup>
	<thead>
		<tr>
			<xsl:apply-templates select="@lit:*" />
			<xsl:apply-templates select="wbg:RowSelector" mode="th" />
			<xsl:apply-templates select="wbg:Columns/wbg:Column[@grouped]" mode="th" />
		</tr>
	</thead>
</xsl:template>

<xsl:template match="wbg:RowSelector" mode="colgroup">
	<xsl:if test="@indentation">
		<col width="{@indentation}" />
	</xsl:if>
	<xsl:if test="@width">
		<col width="{@width}" />
	</xsl:if>
</xsl:template>

<xsl:template match="wbg:Column" mode="colgroup">
	<col width="{wbg:Header/@lit:width}" />
</xsl:template>

<xsl:template match="wbg:RowSelector" mode="th">
	<xsl:if test="@indentation">
		<th>
			<xsl:apply-templates select="@lit:*" />
			<img src="{wbg:img/@src}" border="{wbg:img/@border}" imgType="blank" alt="" />
		</th>
	</xsl:if>
	<xsl:if test="@width">
		<th>
			<xsl:apply-templates select="@lit:*" />
			<img src="{wbg:img/@src}" border="{wbg:img/@border}" imgType="blank" alt="" />
		</th>
	</xsl:if>
</xsl:template>

<xsl:template match="wbg:Column" mode="th">
	<th>
		<xsl:apply-templates select="wbg:Header/@lit:*" mode="wex">
			<xsl:with-param name="exclude">
				<ex>width </ex>
				<ex>height </ex>
			</xsl:with-param>
		</xsl:apply-templates>
		<xsl:value-of select="wbg:Header/wbg:Content/text()" disable-output-escaping="yes" />
	</th>
</xsl:template>

<xsl:template match="wbg:Wrapper">
	<xsl:apply-templates select='@lit:*' />
	<xsl:apply-templates select='wbg:TableEvents/@lit:*' />
</xsl:template>

<xsl:template match='@lit:*' mode="wex" >
	<xsl:param name="exclude"></xsl:param>
	<xsl:if test="contains($exclude,concat(local-name(),' ')) = false">
		<xsl:attribute name='{local-name()}'><xsl:value-of select='.' /></xsl:attribute>
	</xsl:if>
</xsl:template>

<xsl:template match='@lit:*' >
		<xsl:attribute name='{local-name()}'><xsl:value-of select='.' /></xsl:attribute>
</xsl:template>

</xsl:stylesheet>
