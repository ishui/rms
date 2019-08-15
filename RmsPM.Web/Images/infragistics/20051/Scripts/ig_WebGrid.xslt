<?xml version="1.0"?>
<!-- 
Infragistics UltraWebGrid Script 
Version 5.1.20051.37
Copyright (c) 2001-2005 Infragistics, Inc. All Rights Reserved.
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wbg="http://schemas.infragistics.com/WebGrid" xmlns:lit="http://schemas.infragistics.com/WebGrid/Literal">
<xsl:output omit-xml-declaration="yes" method="html"/>
<xsl:param name="gridName"/>
<xsl:param name="fac"/>
<xsl:param name="rs"/>
<xsl:param name="expAreaClass"/>
<xsl:param name="expandImage"/>
<xsl:param name="rowLabelClass"/>
<xsl:param name="blankImage"/>
<xsl:param name="itemClass"/>
<xsl:param name="altClass"/>
<xsl:param name="selClass"/>
<xsl:param name="grpClass"/>
<xsl:param name="parentRowLevel"/>
<xsl:param name="rowHeight"/>
<xsl:param name="rowToStart"/>
<xsl:param name="cellDivScr"/>

<xsl:key name="columnIndex" match="wbg:Column" use="@index"/>
<xsl:key name="cellIndex" match="wbg:Cell" use="position()"/>

<xsl:template match="/">
	<xsl:apply-templates select="wbg:Rows" />
</xsl:template>

<xsl:template match="wbg:Rows">
	<table>
		<tbody>
			<xsl:apply-templates select="wbg:Row" />
			<xsl:apply-templates select="wbg:Group" />
		</tbody>
	</table>
</xsl:template>

<xsl:template match="wbg:Row">
	<xsl:variable name="rowIndex">
		<xsl:value-of select="@i"/>
	</xsl:variable>
	<tr id="{$gridName}r_{$parentRowLevel}{$rowIndex}" level="{$parentRowLevel}{$rowIndex}">
		<xsl:attribute name="style">
			<xsl:if test="@lit:hidden">
				display:none;
			</xsl:if>
			<xsl:choose>
				<xsl:when test="@lit:height">
					<xsl:value-of select="concat('height:',@lit:height,';')" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="concat('height:',$rowHeight,';')" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:attribute>
		<xsl:if test="$fac>1 or $rs=2 and $fac=1">
			<td class="{$expAreaClass}" style="border-width:0px;text-align:center;padding:0px;cursor:default;">
				<xsl:choose>
					<xsl:when test="@lit:showExpand">
						<xsl:value-of select="$expandImage" disable-output-escaping="yes" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$blankImage" disable-output-escaping="yes" />
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</xsl:if>
		<xsl:if test="$fac>0 and $rs!=2">
			<td id="{$gridName}l_{$parentRowLevel}{$rowIndex}" class="{$rowLabelClass}" style="text-align:center;vertical-align:middle;">
				<xsl:choose>
					<xsl:when test="@lit:rowNumber">
						<xsl:value-of select="@lit:rowNumber" disable-output-escaping="yes" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$blankImage"  disable-output-escaping="yes" />
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</xsl:if>
		<xsl:apply-templates select="wbg:Cells">
			<xsl:with-param name="rowIndex">
				<xsl:value-of select="$rowIndex" />
			</xsl:with-param>
			<xsl:with-param name="row" select="." />
			<xsl:with-param name="rowHeight" >
				<xsl:choose>
					<xsl:when test="@lit:height">
						<xsl:value-of select="@lit:height" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$rowHeight" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:with-param>
		</xsl:apply-templates>
	</tr>
</xsl:template>

<xsl:template match="wbg:Cells">
	<xsl:param name="rowIndex" />
	<xsl:param name="row" />
	<xsl:param name="rowHeight" />
	<xsl:for-each select="../../wbg:Columns/wbg:Column">
		<xsl:if test="not(@grouped) and not(@serverOnly) and not(@hidden)">
			<xsl:variable name="columnIndex">
				<xsl:value-of select="@cellIndex"/>
			</xsl:variable>
			<xsl:variable name="cell" select="$row/wbg:Cells/wbg:Cell[number($columnIndex)]"/>
			<xsl:choose>
				<xsl:when test="$rowIndex mod 2 = 1">
					<xsl:call-template name="cellTemplate">
						<xsl:with-param name="cell" select="$cell" />
						<xsl:with-param name="rowIndex">
							<xsl:value-of select="$rowIndex" />
						</xsl:with-param>
						<xsl:with-param name="className">
							<xsl:value-of select="$altClass" />
						</xsl:with-param>
						<xsl:with-param name="rowHeight" select="$rowHeight" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="cellTemplate">
						<xsl:with-param name="cell" select="$cell" />
						<xsl:with-param name="rowIndex">
							<xsl:value-of select="$rowIndex" />
						</xsl:with-param>
						<xsl:with-param name="className">
							<xsl:value-of select="$itemClass" />
						</xsl:with-param>
						<xsl:with-param name="rowHeight" select="$rowHeight" />
					</xsl:call-template>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
	</xsl:for-each>
</xsl:template>

<xsl:template name="cellTemplate">
	<xsl:param name="cell" />
	<xsl:param name="rowIndex" />
	<xsl:param name="className" />
	<xsl:param name="rowHeight" />
	<xsl:variable name="cellIndex">
		<xsl:value-of select="position()-1"/>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test="./@nonfixed">
			<td id="{$gridName}rc_{$parentRowLevel}{$rowIndex}_{$cellIndex}"><div class="{$cellDivScr}"><div>
				<xsl:if test="$className or ./@class or $cell/@class">
					<xsl:attribute name="class">
						<xsl:value-of select="concat($className,' ',./@class,' ',$cell/@class)" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="./@style or $cell/@style or $rowHeight">
					<xsl:attribute name="style">
						<xsl:if test="./@style or $cell/@style">
							<xsl:value-of select="concat(./@style,$cell/@style)" />
						</xsl:if>
						<xsl:if test="$rowHeight">
							<xsl:value-of select="concat('height:',$rowHeight,';')" />
						</xsl:if>
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@allowedit">
					<xsl:attribute name="allowedit">
						<xsl:value-of select="$cell/@allowedit" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@unmaskedValue">
					<xsl:attribute name="unmaskedValue">
						<xsl:value-of select="$cell/@unmaskedValue" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@igCellText">
					<xsl:attribute name="igCellText">
						<xsl:value-of select="$cell/@igCellText" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@igDataValue">
					<xsl:attribute name="igDataValue">
						<xsl:value-of select="$cell/@igDataValue" />
					</xsl:attribute>
				</xsl:if>
				<xsl:value-of select="$cell/wbg:Content" disable-output-escaping="yes" />
			</div></div></td>
		</xsl:when>
		<xsl:otherwise>
			<td id="{$gridName}rc_{$parentRowLevel}{$rowIndex}_{$cellIndex}">
				<xsl:if test="$className or ./@class or $cell/@class">
					<xsl:attribute name="class">
						<xsl:value-of select="concat($className,' ',./@class,' ',$cell/@class)" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="./@style or $cell/@style">
					<xsl:attribute name="style">
						<xsl:value-of select="concat(./@style,$cell/@style)" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@allowedit">
					<xsl:attribute name="allowedit">
						<xsl:value-of select="$cell/@allowedit" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@unmaskedValue">
					<xsl:attribute name="unmaskedValue">
						<xsl:value-of select="$cell/@unmaskedValue" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@igCellText">
					<xsl:attribute name="igCellText">
						<xsl:value-of select="$cell/@igCellText" />
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="$cell/@igDataValue">
					<xsl:attribute name="igDataValue">
						<xsl:value-of select="$cell/@igDataValue" />
					</xsl:attribute>
				</xsl:if>
				<xsl:value-of select="$cell/wbg:Content" disable-output-escaping="yes" />
			</td>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="wbg:Group">
	<xsl:variable name="rowIndex">
		<xsl:value-of select="@i"/>
	</xsl:variable>
	<tr id="{$gridName}gr_{$parentRowLevel}{$rowIndex}" level="{$parentRowLevel}{$rowIndex}" groupRow="{@lit:groupRow}" style="height:{$rowHeight};">
		<td>
			<table width='100%' height='100%' border='0' cellpadding='0' cellspacing='0' bgcolor='{@lit:bgcolor}' bandNo=''>
				<tr id="{$gridName}sgr_{$parentRowLevel}{$rowIndex}" level="{$parentRowLevel}{$rowIndex}" groupRow="{@lit:groupRow}">
					<td id="{$gridName}grc_{$parentRowLevel}{$rowIndex}" groupRow="{@lit:groupRow}" class='{$grpClass}'>
						<xsl:attribute name="cellValue">
							<xsl:value-of select="wbg:Value" disable-output-escaping="yes" />
						</xsl:attribute>
						<xsl:value-of select="$expandImage" disable-output-escaping="yes" />
						<xsl:value-of select="wbg:Content" disable-output-escaping="yes" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</xsl:template>

</xsl:stylesheet>
