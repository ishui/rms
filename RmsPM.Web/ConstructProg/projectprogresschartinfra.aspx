<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.ProjectProgressChartInfra" CodeFile="ProjectProgressChartInfra.aspx.cs" %>
<%@ Register TagPrefix="igchart" Namespace="Infragistics.WebUI.UltraWebChart" Assembly="Infragistics.WebUI.UltraWebChart.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igchartprop" Namespace="Infragistics.UltraChart.Resources.Appearance" Assembly="Infragistics.UltraChart.Resources.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋工程进度</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<style>chart { FONT-SIZE: 12px; FONT-FAMILY: "Tahoma","宋体" }
		</style>
		<script language="javascript">

function MyChartClientEvent1()
{
	var chart = document.all("RxActImg_UltraChart1");
	var objItem=document.createElement("area");
	objItem.setAttribute("coords","195,26,205,31",0);
	objItem.setAttribute("shape", "rect", 0);
	objItem.setAttribute("row", "1", 0);
	objItem.setAttribute("col", "2", 0);

	objItem.attachEvent("onmouseover", MyChartMouseOver);
	objItem.attachEvent("onmouseout", MyChartMouseOut);
	objItem.attachEvent("onclick", MyChartClick);

	chart.appendChild(objItem);
}

function MyChartMouseOver(obj)
{
	var row = obj.srcElement.getAttribute("row");
	var col = obj.srcElement.getAttribute("col");
	UltraChart1_pRcEv(event, obj, row, col, 'MOUSE_OVER', '27');
}

function MyChartMouseOut(obj)
{
	var row = obj.srcElement.getAttribute("row");
	var col = obj.srcElement.getAttribute("col");
	UltraChart1_pRcEv(event, obj, row, col, 'MOUSE_OUT', '27');
}

function MyChartClick(obj)
{
	var row = obj.srcElement.getAttribute("row");
	var col = obj.srcElement.getAttribute("col");
	UltraChart1_pRcEv(event, obj, row, col, 'MOUSE_CLICKED', '27');
}

function UltraChart1_ClientOnMouseClick(this_ref, row, column, value, row_label, column_label, evt_type, layer_id)
{
	var id = column;
	//alert(id);
	var WBSCode = GetValueByChart(id, "txtChartWBSCode");
	if (WBSCode != "") ShowWBS(WBSCode);
}

function UltraChart1_ClientOnShowTooltip(text, tooltip_ref)
{
	var id = GetChartColumnID(text);
	
	tooltip_ref.innerHTML = RemoveChartColumnID(text)
//		+ "<br>状　　态：" + GetValueByChart(id, "txtChartStatusName")
//		+ "<br>当前进度：" + GetValueByChart(id, "txtChartCompletePercent") + "%"
		;
		
}

function GetChartColumnID(text)
{
	var id = "";
	
	if (text != "")
	{
		var pos = text.indexOf("@");
		
		if (pos >= 0)
		{
			id = text.substring(0, pos);
		}
	}
	
	return id;
}

function RemoveChartColumnID(text)
{
	var val = "";
	
	if (text != "")
	{
		var pos = text.indexOf("@");
		
		if (pos >= 0)
		{
			val = text.substring(pos + 1);
		}
	}
	
	return val;
}

		</script>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr>
					<td class="intopic" width="200">工作进度</td>
				</tr>
				<tr height="100%">
					<td>
						<table style="BORDER-RIGHT: #a3afb8 1px solid; PADDING-RIGHT: 9px; BORDER-TOP: #a3afb8 1px solid; PADDING-LEFT: 9px; PADDING-BOTTOM: 9px; BORDER-LEFT: #a3afb8 1px solid; PADDING-TOP: 9px; BORDER-BOTTOM: #a3afb8 1px solid"
							height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td>
									<table height="100%" width="100%">
										<tr>
											<td>
												<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
													<table id="tbList" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr>
															<td><igchart:ultrachart id="UltraChart1" runat="server" ChartType="GanttChart" Width="700px" Height="492px"
																	CssClass="chart" JavaScriptFileName="../images/infragistics/20051/scripts/ig_webchart.js" ChartImagesPath="../Temp/ChartImages">
																	<ColorModel ColorBegin="LimeGreen" ColorEnd="Salmon" AlphaLevel="150" ModelStyle="CustomSkin"
																		Grayscale="False" Scaling="None">
																		<Skin ApplyRowWise="False">
																			<PEs>
																				<igchartprop:PaintElement FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Salmon" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent"
																					StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
																				<igchartprop:PaintElement FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="LimeGreen" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent"
																					StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
																			</PEs>
																		</Skin>
																	</ColorModel>
																	<Border CornerRadius="0" DrawStyle="Solid" Raised="False" Color="Black" Thickness="0"></Border>
																	<DeploymentScenario Scenario="FileSystem" ImageURL="../Temp/ChartImages/Chart_#SEQNUM(100).jpg" ImageType="Png"
																		FilePath="../Temp/ChartImages"></DeploymentScenario>
																	<Tooltips BorderThickness="1" Overflow="ClientArea" FormatString="&lt;DATA_COLUMN&gt;@&lt;SERIES_LABEL&gt;(&lt;ITEM_LABEL&gt;)&lt;br&gt;&lt;DATA_VALUE:00.00&gt;"
																		EnableFadingEffect="False" Format="Custom" FontColor="Black" BorderColor="Black" Display="MouseMove"
																		BackColor="AntiqueWhite" Padding="0"></Tooltips>
																	<Effects>
																		<Effects>
																			<igchartprop:GradientEffect Style="VerticalBump" Enabled="False" Coloring="Lighten"></igchartprop:GradientEffect>
																		</Effects>
																	</Effects>
																	<Legend Font="Microsoft Sans Serif, 7.8pt" Visible="False" AlphaLevel="150" BorderThickness="1"
																		BorderStyle="Solid" SpanPercentage="25" BorderColor="Navy" FontColor="Black" BackgroundColor="FloralWhite"
																		DataAssociation="DefaultData" Location="Right" FormatString="&lt;ITEM_LABEL&gt;">
																		<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
																	</Legend>
																	<GanttChart LinkLineColor="Black">
																		<LinkLineStyle MidPointAnchors="False" EndStyle="ArrowAnchor" DrawStyle="Solid" StartStyle="NoAnchor"></LinkLineStyle>
																		<CompletePercentagesPE FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient"
																			Fill="Gold" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Yellow" StrokeOpacity="255"
																			ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></CompletePercentagesPE>
																		<OwnersLabelStyle Font="Tahoma, 11pt" Dy="0" HorizontalAlign="Center" FontSizeBestFit="False" ClipText="True"
																			Dx="0" RotationAngle="0" Orientation="Horizontal" WrapText="False" Flip="False" FontColor="Black"
																			VerticalAlign="Center"></OwnersLabelStyle>
																		<EmptyPercentagesPE FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="150" ElementType="Gradient"
																			Fill="White" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="WhiteSmoke"
																			StrokeOpacity="255" ImagePath="" Stroke="Transparent" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></EmptyPercentagesPE>
																	</GanttChart>
																	<TitleBottom Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="" FontSizeBestFit="False"
																		Orientation="Horizontal" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Far"
																		VerticalAlign="Center" Location="Bottom">
																		<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
																	</TitleBottom>
																	<Data DataMember="" SwapRowsAndColumns="False" UseMinMax="False" UseRowLabelsColumn="False"
																		MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="False" MaxValue="1.7976931348623157E+308"></Data>
																	<TitleLeft Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Component Market" FontSizeBestFit="False"
																		Orientation="VerticalLeftFacing" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Near"
																		VerticalAlign="Center" Location="Left">
																		<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
																	</TitleLeft>
																	<TitleTop Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="" FontSizeBestFit="False"
																		Orientation="Horizontal" WrapText="False" Extent="33" FontColor="Black" HorizontalAlign="Near"
																		VerticalAlign="Center" Location="Top">
																		<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
																	</TitleTop>
																	<TitleRight Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="UltraChart for .NET is #1 !"
																		FontSizeBestFit="False" Orientation="VerticalRightFacing" WrapText="False" Extent="26" FontColor="Black"
																		HorizontalAlign="Near" VerticalAlign="Center" Location="Right">
																		<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
																	</TitleRight>
																	<Axis BackColor="Cornsilk">
																		<Y LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Black"
																			RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="140" LogBase="10"
																			RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																			<StripLines Interval="2" Visible="False">
																				<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
																					FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Transparent"
																					StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
																			</StripLines>
																			<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																			<Labels ItemFormatString="" VerticalAlign="Center" WrapText="False" FontSizeBestFit="False"
																				SeriesFormatString="" ClipText="True" Font="宋体, 9pt" Flip="False" ItemFormat="None"
																				FontColor="Black" Orientation="Horizontal" Visible="False" OrientationAngle="0" HorizontalAlign="Far">
																				<SeriesLabels Font="宋体, 5.25pt" Visible="False" HorizontalAlign="Center" FontSizeBestFit="False"
																					ClipText="True" FormatString="" Orientation="Horizontal" WrapText="False" Flip="False" FontColor="Black"
																					VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
																			</Labels>
																			<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																			<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																			<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
																		</Y>
																		<Y2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
																			RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																			RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																			<StripLines Interval="2" Visible="False">
																				<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
																					FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Transparent"
																					StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
																			</StripLines>
																			<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																			<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" VerticalAlign="Center" WrapText="False" FontSizeBestFit="False"
																				SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="宋体, 9pt" Flip="False"
																				ItemFormat="ItemLabel" FontColor="Black" Orientation="Horizontal" Visible="False" OrientationAngle="0"
																				HorizontalAlign="Far">
																				<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Far" FontSizeBestFit="False"
																					ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="VerticalLeftFacing" WrapText="False"
																					Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
																			</Labels>
																			<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																			<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																			<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
																		</Y2>
																		<X2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
																			RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																			RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																			<StripLines Interval="2" Visible="False">
																				<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
																					FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Transparent"
																					StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
																			</StripLines>
																			<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																			<Labels ItemFormatString="&lt;ITEM_LABEL:MM-dd-yy&gt;" VerticalAlign="Center" WrapText="False"
																				FontSizeBestFit="False" SeriesFormatString="" ClipText="True" Font="宋体, 9pt" Flip="False"
																				ItemFormat="Custom" FontColor="Black" Orientation="Horizontal" Visible="True" OrientationAngle="0"
																				HorizontalAlign="Near">
																				<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" FontSizeBestFit="False"
																					ClipText="True" FormatString="" Orientation="Horizontal" WrapText="False" Flip="False" FontColor="Black"
																					VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
																			</Labels>
																			<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																			<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																			<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
																		</X2>
																		<Z2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
																			RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																			RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																			<StripLines Interval="2" Visible="False">
																				<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
																					FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Transparent"
																					StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
																			</StripLines>
																			<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																			<Labels ItemFormatString="" VerticalAlign="Center" WrapText="False" FontSizeBestFit="False"
																				SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="宋体, 9pt" Flip="False"
																				ItemFormat="None" FontColor="Black" Orientation="Horizontal" Visible="True" OrientationAngle="0"
																				HorizontalAlign="Near">
																				<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" FontSizeBestFit="False"
																					ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="Horizontal" WrapText="False"
																					Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
																			</Labels>
																			<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																			<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																			<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
																		</Z2>
																		<Z LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
																			RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																			RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																			<StripLines Interval="2" Visible="False">
																				<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
																					FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Transparent"
																					StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
																			</StripLines>
																			<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																			<Labels ItemFormatString="" VerticalAlign="Center" WrapText="False" FontSizeBestFit="False"
																				SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="宋体, 9pt" Flip="False"
																				ItemFormat="None" FontColor="Black" Orientation="Horizontal" Visible="True" OrientationAngle="0"
																				HorizontalAlign="Near">
																				<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" FontSizeBestFit="False"
																					ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="Horizontal" WrapText="False"
																					Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
																			</Labels>
																			<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																			<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																			<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
																		</Z>
																		<X LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Black"
																			RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																			RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																			<StripLines Interval="2" Visible="False">
																				<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
																					Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
																					FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Transparent"
																					StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
																			</StripLines>
																			<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																			<Labels ItemFormatString="&lt;ITEM_LABEL:yy-MM-dd&gt;" VerticalAlign="Center" WrapText="False"
																				FontSizeBestFit="False" SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
																				Flip="False" ItemFormat="Custom" FontColor="Black" Orientation="VerticalLeftFacing"
																				Visible="True" OrientationAngle="0" HorizontalAlign="Near">
																				<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" FontSizeBestFit="False"
																					ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="Horizontal" WrapText="False"
																					Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
																			</Labels>
																			<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																			<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																			<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
																		</X>
																	</Axis>
																	<ClientSideEvents ClientOnShowTooltip="UltraChart1_ClientOnShowTooltip" ClientOnMouseClick="UltraChart1_ClientOnMouseClick"></ClientSideEvents>
																</igchart:ultrachart></td>
														</tr>
													</table>
													<table cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr align="center">
															<td noWrap><span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 50px; BORDER-BOTTOM: black 1px outset; HEIGHT: 5px; BACKGROUND-COLOR: #76d769"></span>&nbsp;&nbsp;计划&nbsp;&nbsp;&nbsp;&nbsp;
																<span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 50px; BORDER-BOTTOM: black 1px outset; HEIGHT: 5px; BACKGROUND-COLOR: red">
																</span>&nbsp;&nbsp;实际
															</td>
														</tr>
													</table>
													<div id="MyLabelY" style="LEFT: 0px; WIDTH: 140px; POSITION: absolute; TOP: 0px">
														<table id="pnLabelY" style="MARGIN-TOP: 15px; MARGIN-LEFT: 10px" height="345" cellSpacing="0"
															cellPadding="0" width="100%" border="0">
															<asp:repeater id="dgLabelY" Runat="server">
																<ItemTemplate>
																	<tr>
																		<td nowrap align="right">
																			<a style='display:<%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.ChildNodesCount"))==0?"none":"" %>' href="#" onclick="GotoTask(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>' hint='<%# DataBinder.Eval(Container, "DataItem.TaskHint") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();">
																				<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.TaskName"), 8) %>
																				(<%# DataBinder.Eval(Container, "DataItem.CompletePercent") %>%)</a> <span style='display:<%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.ChildNodesCount"))==0?"":"none" %>' hint='<%# DataBinder.Eval(Container, "DataItem.TaskHint") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();">
																				<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.TaskName"), 8) %>
																				(<%# DataBinder.Eval(Container, "DataItem.CompletePercent") %>%)</span>
																		</td>
																	</tr>
																</ItemTemplate>
															</asp:repeater></table>
													</div>
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 80px"
							myDiv="" myOffsetBottom="0" myOffsetRight="40" myOffsetTop="0px">
							<table id="joyboxTable" style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; BORDER-LEFT: black 1px outset; BORDER-BOTTOM: black 1px outset"
								height="80" cellSpacing="0" cellPadding="0" width="180">
								<tr>
									<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" noWrap bgColor="#ffffcc"><label id="linktitle"></label></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
			<input id="txtChartWBSCode" type="hidden" name="txtChartWBSCode" runat="server"><input id="txtChartCompletePercent" type="hidden" name="txtChartCompletePercent" runat="server">
			<input id="txtChartStatusName" type="hidden" name="txtChartStatusName" runat="server">
			<input id="txtChartRowHeight" type="hidden" name="txtChartRowHeight" runat="server" value="26"><input id="txtChartDataHeight" type="hidden" name="txtChartDataHeight" runat="server">
			<input id="txtChartTopHeight" type="hidden" name="txtChartTopHeight" runat="server" value="15"><input id="txtChartBottomHeight" type="hidden" name="txtChartBottomHeight" runat="server"
				value="85">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function ShowWBS(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

function GotoTask(WBSCode)
{
	window.parent.GotoTask(WBSCode);
}

function GetValueByChart(id, name)
{
	var val = "";
	var vals = document.all(name).value;
	var arr = vals.split(";");
	
	if (arr.length > id) val = arr[id];
	
//	var arr2;
	
	/*
	var l = arr.length;
	for(var i=0;i<l;i++)
	{
		if (arr[i] != "")
		{
			arr2 = 	arr[i].split("=");
			if (arr2.length == 2)
			{
				if (arr2[0] == id)
				{
					WBSCode = arr2[1];
					return WBSCode;
				}
			}
		}
	}
	*/
	
	return val;
}

//alert(document.all("UltraChart1_igWindowVuer").style["z-index"]);
//document.all("UltraChart1_igWindowVuer").style.z-index = 2;

if ((Form1.txtChartDataHeight.value != "") && (Form1.txtChartDataHeight.value != "0"))
{
	document.all("pnLabelY").height = Form1.txtChartDataHeight.value;
}

//-->

		</SCRIPT>
	</body>
</HTML>
