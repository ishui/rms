<%@ Reference Control="~/cashflow/rptfiniochartline.ascx" %>
<%@ Register TagPrefix="igchartprop" Namespace="Infragistics.UltraChart.Resources.Appearance" Assembly="Infragistics.UltraChart.Resources.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igchart" Namespace="Infragistics.WebUI.UltraWebChart" Assembly="Infragistics.WebUI.UltraWebChart.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.CashFlow.RptFinIOChartColumn" CodeFile="RptFinIOChartColumn.ascx.cs" %>
								<igchart:ultrachart id="UltraChart1" runat="server" Width="700px" BackColor="White" SplineChart-EndStyle="DiamondAnchor"
									SplineChart-SplineTension="0.3" SplineChart-HighLightLines="False" SplineChart-StartStyle="DiamondAnchor"
									SplineChart-MidPointAnchors="True" SplineChart-NullHandling="Zero" SplineChart-Thickness="3" SplineChart-DrawStyle="Solid"
									Height="420px" Transform3D-ZRotation="0" Transform3D-Scale="65" Transform3D-XRotation="144" Transform3D-YRotation="12"
									EnableCrossHair="False" ChartType="ColumnChart" JavaScriptFileName="../Images/infragistics/20051/scripts/ig_webchart.js"
									ChartImagesPath="../Temp/ChartImages">
									<ColorModel ColorBegin="DarkGoldenrod" ColorEnd="Navy" AlphaLevel="150" ModelStyle="CustomSkin"
										Grayscale="False" Scaling="None">
										<Skin ApplyRowWise="False">
											<PEs>
												<igchartprop:PaintElement FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
													Fill="Red" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent"
													StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
												<igchartprop:PaintElement FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
													Fill="Blue" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent"
													StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
												<igchartprop:PaintElement FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
													Fill="SpringGreen" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent"
													StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
											</PEs>
										</Skin>
									</ColorModel>
									<Border CornerRadius="15" DrawStyle="Solid" Raised="False" Color="Black" Thickness="1"></Border>
									<DeploymentScenario Scenario="FileSystem" ImageURL="../Temp/ChartImages/Chart_#SEQNUM(100).jpg" ImageType="Png"
										FilePath="../Temp/ChartImages"></DeploymentScenario>
									<Tooltips BorderThickness="1" Overflow="None" FormatString="&lt;DATA_VALUE:00.00&gt;" EnableFadingEffect="False"
										Format="Custom" FontColor="Black" BorderColor="Black" Display="MouseMove" BackColor="AntiqueWhite"
										Padding="0"></Tooltips>
									<Legend Font="Microsoft Sans Serif, 7.8pt" Visible="False" AlphaLevel="150" BorderThickness="1"
										BorderStyle="Solid" SpanPercentage="25" BorderColor="Navy" FontColor="Black" BackgroundColor="FloralWhite"
										DataAssociation="DefaultData" Location="Right" FormatString="&lt;ITEM_LABEL&gt;">
										<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
									</Legend>
									<ColumnChart SeriesSpacing="1" ColumnSpacing="0" NullHandling="Zero"></ColumnChart>
									<TitleBottom Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Copyright Infragistics Inc."
										FontSizeBestFit="False" Orientation="Horizontal" WrapText="False" Extent="26" FontColor="Black"
										HorizontalAlign="Far" VerticalAlign="Center" Location="Bottom">
										<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
									</TitleBottom>
									<Data DataMember="" SwapRowsAndColumns="True" UseMinMax="False" UseRowLabelsColumn="False"
										MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="True" MaxValue="1.7976931348623157E+308"></Data>
									<TitleLeft Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Component Market" FontSizeBestFit="False"
										Orientation="VerticalLeftFacing" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Near"
										VerticalAlign="Center" Location="Left">
										<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
									</TitleLeft>
									<TitleTop Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Infragistics UltraChart &lt;TODAY_DATE:MM/dd/yy&gt;"
										FontSizeBestFit="False" Orientation="Horizontal" WrapText="False" Extent="33" FontColor="Black"
										HorizontalAlign="Near" VerticalAlign="Center" Location="Top">
										<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
									</TitleTop>
									<TitleRight Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="UltraChart for .NET is #1 !"
										FontSizeBestFit="False" Orientation="VerticalRightFacing" WrapText="False" Extent="26" FontColor="Black"
										HorizontalAlign="Near" VerticalAlign="Center" Location="Right">
										<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
									</TitleRight>
									<Axis BackColor="Cornsilk">
										<Y LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Black"
											RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
											RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
											<StripLines Interval="2" Visible="False">
												<PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill"
													Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit"
													FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1"
													ImageWrapMode="Tile" TextureApplication="Normal"></PE>
											</StripLines>
											<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
											<Labels ItemFormatString="&lt;DATA_VALUE:00.00&gt;" VerticalAlign="Center" WrapText="False"
												FontSizeBestFit="False" SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
												Flip="False" ItemFormat="DataValue" FontColor="Black" Orientation="Horizontal" Visible="True"
												OrientationAngle="0" HorizontalAlign="Far">
												<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Center" FontSizeBestFit="False"
													ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="VerticalLeftFacing" WrapText="False"
													Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
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
													FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1"
													ImageWrapMode="Tile" TextureApplication="Normal"></PE>
											</StripLines>
											<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
											<Labels ItemFormatString="&lt;DATA_VALUE:00.00&gt;" VerticalAlign="Center" WrapText="False"
												FontSizeBestFit="False" SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
												Flip="False" ItemFormat="DataValue" FontColor="Black" Orientation="Horizontal" Visible="True"
												OrientationAngle="0" HorizontalAlign="Near">
												<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" FontSizeBestFit="False"
													ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="Horizontal" WrapText="False"
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
													FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1"
													ImageWrapMode="Tile" TextureApplication="Normal"></PE>
											</StripLines>
											<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
											<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" VerticalAlign="Center" WrapText="False" FontSizeBestFit="False"
												SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
												Flip="False" ItemFormat="ItemLabel" FontColor="Black" Orientation="VerticalLeftFacing"
												Visible="True" OrientationAngle="0" HorizontalAlign="Far">
												<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Far" FontSizeBestFit="False"
													ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="VerticalLeftFacing" WrapText="False"
													Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
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
													FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1"
													ImageWrapMode="Tile" TextureApplication="Normal"></PE>
											</StripLines>
											<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
											<Labels ItemFormatString="" VerticalAlign="Center" WrapText="False" FontSizeBestFit="False"
												SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
												Flip="False" ItemFormat="None" FontColor="Black" Orientation="Horizontal" Visible="True"
												OrientationAngle="0" HorizontalAlign="Near">
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
													FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1"
													ImageWrapMode="Tile" TextureApplication="Normal"></PE>
											</StripLines>
											<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
											<Labels ItemFormatString="&lt;DATA_VALUE:00.00&gt;" VerticalAlign="Center" WrapText="False"
												FontSizeBestFit="False" SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
												Flip="False" ItemFormat="DataValue" FontColor="Black" Orientation="Horizontal" Visible="True"
												OrientationAngle="0" HorizontalAlign="Near">
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
													FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1"
													ImageWrapMode="Tile" TextureApplication="Normal"></PE>
											</StripLines>
											<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
											<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" VerticalAlign="Near" WrapText="False" FontSizeBestFit="False"
												SeriesFormatString="&lt;SERIES_LABEL&gt;" ClipText="True" Font="Microsoft Sans Serif, 7.8pt"
												Flip="False" ItemFormat="ItemLabel" FontColor="Black" Orientation="Horizontal" Visible="False"
												OrientationAngle="0" HorizontalAlign="Center">
												<SeriesLabels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Center" FontSizeBestFit="False"
													ClipText="True" FormatString="&lt;SERIES_LABEL&gt;" Orientation="Horizontal" WrapText="False"
													Flip="False" FontColor="Black" VerticalAlign="Center" OrientationAngle="0"></SeriesLabels>
											</Labels>
											<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
											<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
											<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
										</X>
									</Axis>
								</igchart:ultrachart>
