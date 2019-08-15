namespace ZL.WebControls.DateTimeBox
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Designer("ZL.WebControls.DateTimeBox.DateTimeBoxDesigner,ZL.WebControls.DateTimeBox"), ValidationProperty("Text"), DefaultProperty("Date"), ToolboxData("<{0}:DateTimeBox runat=server></{0}:DateTimeBox>")]
    public class DateTimeBox : WebControl, INamingContainer
    {
        private bool g_bReadOnly = true;
        private TextAlign g_DateTextAlign = TextAlign.Center;
        private Language g_Language = Language.English;
        private string g_strCssClass = "";
        private string g_strImageUrl = "/ZL_Client/WebControls/DateTimeBox/DateTimeBox.gif";
        private string g_strJSUrl = "/ZL_Client/WebControls/DateTimeBox/DateTimeBox.js";
        private string g_strText = "";
        private TextBox g_TextBox = new TextBox();

        public event EventHandler TextChanged;

        protected override void CreateChildControls()
        {
            base.Style.Add("position", "statis");
            Table child = new Table();
            child.BorderWidth = Unit.Pixel(0);
            child.CellPadding = 0;
            child.CellSpacing = 0;
            child.Width = base.Width;
            child.Height = base.Height;
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.VerticalAlign = VerticalAlign.Middle;
            TableCell cell2 = new TableCell();
            cell2.Width = Unit.Pixel(1);
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.VerticalAlign = VerticalAlign.Middle;
            TextBox box = this.g_TextBox;
            box.ID = base.ID;
            box.CssClass = this.g_strCssClass;
            box.Enabled = base.Enabled;
            box.ToolTip = base.ToolTip;
            box.Width = Unit.Percentage(100);
            box.Height = base.Height;
            box.Text = this.Text;
            box.Attributes.Add("defaultValue", this.Text);
            if (this.g_bReadOnly)
            {
                box.ReadOnly = this.ReadOnly;
            }
            box.Style.Add("cursor", "hand");
            if (this.DateTextAlign == TextAlign.Left)
            {
                box.Style.Add("text-align", "left");
            }
            else if (this.DateTextAlign == TextAlign.Right)
            {
                box.Style.Add("text-align", "right");
            }
            else if (this.DateTextAlign == TextAlign.Center)
            {
                box.Style.Add("text-align", "center");
            }
            if (this.TextChanged != null)
            {
                box.Attributes.Add("onchange", this.Page.GetPostBackEventReference(this));
            }
            box.Attributes.Add("ondblclick", "__DateBoxOnClick(this,'" + this.CalendarLanguage.ToString() + "');");
            box.Attributes.Add("onblur", "__DateBoxOnBlur(this,'" + this.CalendarLanguage.ToString() + "');");
            Image image = new Image();
            if (this.ImageUrl == "")
            {
                image.ImageUrl = "/ZL_Client/Web/UI/DateTimeBox.gif";
            }
            else
            {
                image.ImageUrl = this.ImageUrl;
            }
            image.BorderWidth = Unit.Pixel(0);
            image.Style.Add("cursor", "hand");
            if (!base.Enabled)
            {
                image.Attributes.Add("disabled", "true");
            }
            image.Attributes.Add("onclick", "__DateBoxOnClick(this.parentElement.parentElement.children[0].children[0],'" + this.CalendarLanguage.ToString() + "');");
            string text = "";
            if (!this.Page.IsClientScriptBlockRegistered("DateTimeBoxScript"))
            {
                text = ((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((text + "<Script Language=\"JavaScript\">" + "var __DateTimeBoxSelectedObject=null;") + "var __DateBoxDateNowLanguage;" + "var __DateTimeBoxEnglish=new Array(\"iso-8859-1\",\"S\",\"M\",\"T\",\"W\",\"T\",\"F\",\"S\",\"/\",\"\",\"Today\",\"Please input Date!\",\"Clear\");") + "var __DateTimeBoxChinese=new Array(\"gb2312\",\"日\",\"一\",\"二\",\"三\",\"四\",\"五\",\"六\",\"年\",\"月\",\"今天\",\"请输入正确日期格式的数据！\\n例：1900-01-01\",\"清除\");" + "var __DateTimeBoxChineseBig5=new Array(\"big5\",\"ら\",\"\",\"\",\"\",\"\",\"き\",\"せ\",\"\",\"る\",\"さぱ\",\"叫块タ絋ら戳Α计沮!\\nㄒ: 1900-01-01\",\"睲埃\");") + "function __DateBoxOnClick(obj,lng){" + "__DateTimeBoxSelectedObject=obj;") + "__DateBoxDateNowLanguage=eval(\"__DateTimeBox\"+lng);" + "__DateBoxDateInit(obj.value);") + "var obj1=document.all(\"__DateTimeBoxDiv\");" + "var obj2=obj1.children[0];") + "obj1.style.xpos = window.event.x+document.body.scrollLeft-10; " + "obj1.style.ypos = window.event.y-10+document.body.scrollTop;") + "if((parseInt(window.event.x)+parseInt(obj2.width))>=parseInt(document.body.offsetWidth)){" + "obj1.style.xpos=(parseInt(document.body.scrollWidth)-parseInt(obj2.width));") + "}" + "if((parseInt(window.event.y)+parseInt(obj2.height))>=parseInt(document.body.offsetHeight)){ ") + "obj1.style.ypos=(parseInt(document.body.offsetHeight)-parseInt(obj2.height)+parseInt(document.body.scrollTop));" + "}") + "obj1.style.left = obj1.style.xpos;" + "obj1.style.top = obj1.style.ypos;") + "document.all(\"__DateTimeBoxDiv\").style.display=\"\";" + "}") + "function __DateTimeBoxSizeTo(x,y){" + "document.all(\"__DateTimeIFrame\").style.pixelWidth=x;") + "document.all(\"__DateTimeIFrame\").style.pixelHeight=y;" + "}") + "function __DateTimeBoxAllHidden(){" + "document.all(\"__DateTimeBoxDiv\").style.display=\"none\";") + "}" + "function __DateTimeBoxDivMouseOut(){") + "__DateTimeBoxAllHidden();" + "}") + "function __DateTimeBoxDivMouseOver(){" + "}") + "function __DateTimeBoxInit(){" + "var str=\"\";") + "str+=\"<span id=\\\"__DateTimeBoxDiv\\\" onmouseover=\\\"__DateTimeBoxDivMouseOver();\\\" onmouseout=\\\"__DateTimeBoxDivMouseOut();\\\" style=\\\"display:none; position:absolute; left:; top:; width:; Height:; z-index:998\\\" class=\\\"\\\">\";" + "str+=\"<iframe id=\\\"__DateTimeIFrame\\\" name=\\\"__DateTimeIFrame\\\" src=\\\"about:blank\\\" frameborder=\\\"0\\\" width=\\\"184\\\" height=\\\"221\\\" scrolling=\\\"no\\\"></iframe>\";") + "str+=\"</span>\";" + "document.write(str);") + "}" + "function __DateBoxOnBlur(obj,lng){") + "if(!__DateBoxCheckDate(obj.value)){" + "obj.value=obj.defaultValue.toString();") + "alert(eval(\"__DateTimeBox\"+lng)[11]);" + "}") + "}" + "function __DateBoxCheckDate(bdate){") + "if (bdate.length == 0) return true;" + @"var re=/^(([1-2]\d{3})\-(0?[1|3|5|7|8]|12|10)\-([1-2]?[0-9]|0[1-9]|30|31)|([1-2]\d{3})\-(0?[4|6|9]|11)\-([1-2]?[0-9]|0[1-9]|30)|([1-2]\d{3})\-(0?[2])\-([1-2]?[0-9]|0[1-9]))$/;") + "if (re.test(bdate)){" + "if ((parseInt(bdate.split(\"-\")[1])==2)&&(parseInt(bdate.split(\"-\")[2])==29)){") + "if (!(parseInt(bdate.split(\"-\")[0])%4==0)&&(!parseInt(bdate.split(\"-\")[0])%10==0)|(parseInt(bdate.split(\"-\")[0])%40==0)){" + "return false;") + "}" + "}") + "}" + "return re.test(bdate);") + "}" + "function __DateBoxDateInit(dates){") + "var str=\"<html>\";" + "str+=\"<head><meta http-equiv=\\\"Content-Type\\\" content=\\\"text/html; charset=\"+__DateBoxDateNowLanguage[0]+\"\\\">\";") + "str+=\"<style type=\\\"text/css\\\"><!--\";" + "str+=\".DateTimeBoxTextStyle { font-family: \\\"Arial\\\"; font-size: 9pt; line-height: 15pt; text-decoration: none; cursor: hand}\";") + "str+=\".DateTimeBoxArrowStyle { font-family: \\\"Arial\\\"; font-size: 6pt; text-decoration: none; cursor: hand}\";" + "str+=\".DateTimeBoxTextMouseOverStyle { font-family: \\\"Arial\\\"; font-size: 9pt; line-height: 15pt; cursor: hand; text-decoration: none;border-right-width: 1px;border-bottom-width: 1px;border-left-width: 1px;border-top-style: solid;border-right-style: solid;border-bottom-style: solid;border-left-style: solid;border-top-color: #000000;border-right-color: #000000;border-bottom-color: #000000;border-left-color: #000000;background-color: #E3E3E3;}\";") + "str+=\".DateTimeBoxTextMouseOutStyle { font-family: \\\"Arial\\\"; font-size: 9pt; line-height: 15pt; cursor: hand; text-decoration: none;border-right-width: 1px;border-bottom-width: 1px;border-left-width: 1px;border-top-style: solid;border-right-style: solid;border-bottom-style: solid;border-left-style: solid;border-top-color: ;border-right-color: ;border-bottom-color: ;border-left-color: ;background-color: ;}\";" + "str+=\"--></style>\";") + "str+=\"</head>\";" + "str+=\"<body leftmargin=\\\"0\\\" topmargin=\\\"0\\\" marginwidth=\\\"0\\\" marginheight=\\\"0\\\" bgcolor=\\\"ff0000\\\" oncontextmenu=\\\"window.event.returnValue=false\\\" onkeypress=\\\"window.event.returnValue=false\\\" onkeydown=\\\"window.event.returnValue=false\\\" onkeyup=\\\"window.event.returnValue=false\\\" ondragstart=\\\"window.event.returnValue=false\\\" onselectstart=\\\"event.returnValue=false\\\">\";") + "str+=\"<table id=\\\"__DateBoxDateTable\\\" width=\\\"100%\\\" height=\\\"100%\\\" border=\\\"1\\\" cellspacing=\\\"0\\\" cellpadding=\\\"0\\\" align=\\\"center\\\" bgcolor=\\\"#FFFFFF\\\"><tr><td valign=\\\"top\\\">\";" + "str+=\"<table width=\\\"100%\\\" border=\\\"1\\\" bordercolor=\\\"#DDDDDD\\\" cellspacing=\\\"0\\\" cellpadding=\\\"0\\\" align=\\\"center\\\" bgcolor=\\\"#DDDDDD\\\">\";") + "str+=\"<tr><td align=\\\"center\\\" width=\\\"15\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\">&nbsp;</td>\";" + "str+=\"<td align=\\\"center\\\" width=\\\"15\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" onClick=\\\"__DateBoxDateChangeYear(-1);return false;\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\">\";") + "str+=\"<b>&lt;&lt;</b></td><td align=\\\"center\\\" width=\\\"15\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" onClick=\\\"__DateBoxDateChangeMonth(-1);return false;\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\">\";" + "str+=\"<b>&lt;</b></td><td align=\\\"center\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" id=\\\"__DateBoxDateNowDate\\\">\";") + "str+=\"</td><td align=\\\"center\\\" width=\\\"15\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" onClick=\\\"__DateBoxDateChangeMonth(+1);return false;\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\">\";" + "str+=\"<b>&gt;</b></td><td align=\\\"center\\\" width=\\\"15\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" onClick=\\\"__DateBoxDateChangeYear(+1);return false;\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\">\";") + "str+=\"<b>&gt;&gt;</b><td align=\\\"center\\\" width=\\\"15\\\" bgcolor=\\\"#DDDDDD\\\" class=\\\"DateTimeBoxTextStyle\\\" onclick=\\\"__DateTimeBoxClose();\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\">\";" + "str+=\"<b>X</b></td></tr></table>\";") + "str+=\"<table width=\\\"100%\\\" border=\\\"1\\\" bordercolor=\\\"#FFFFFF\\\" cellspacing=\\\"0\\\" cellpadding=\\\"0\\\" align=\\\"center\\\" bgcolor=\\\"#FFFFFF\\\">\";" + "str+=\"<tr align=\\\"center\\\">\";") + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#FF3333\\\">\"+__DateBoxDateNowLanguage[1]+\"</font></b></td>\";" + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#666666\\\">\"+__DateBoxDateNowLanguage[2]+\"</font></b></td>\";") + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#666666\\\">\"+__DateBoxDateNowLanguage[3]+\"</font></b></td>\";" + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#666666\\\">\"+__DateBoxDateNowLanguage[4]+\"</font></b></td>\";") + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#666666\\\">\"+__DateBoxDateNowLanguage[5]+\"</font></b></td>\";" + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#666666\\\">\"+__DateBoxDateNowLanguage[6]+\"</font></b></td>\";") + "str+=\"<td width=\\\"25\\\" class=\\\"DateTimeBoxTextStyle\\\"><b><font color=\\\"#3333FF\\\">\"+__DateBoxDateNowLanguage[7]+\"</font></b></td>\";" + "str+=\"</tr>\";") + "str+=\"<tr align=\\\"center\\\" bgcolor=\\\"#FF6600\\\"><td colspan=\\\"7\\\" height=\\\"1\\\"></td></tr><tr align=\\\"center\\\">\";" + "for(var lp=0;lp<42;lp++){") + "str+=\"<td id=\\\"__DateBoxDateItem\"+(lp+1)+\"\\\" width=\\\"25\\\" style=\\\"cursor:hand;\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\" onClick=\\\"__DateBoxDateSelected(this);\\\" class=\\\"DateTimeBoxTextStyle\\\"></td>\";" + "if((lp+1)%7==0&&lp<42){") + "str+=\"</tr><tr align=\\\"center\\\">\";" + "}") + "}" + "str+=\"</tr></table></td></tr><tr><td valign=\\\"bottom\\\"><table width=\\\"100%\\\" border=\\\"0\\\" cellspacing=\\\"0\\\" cellpadding=\\\"0\\\"><tr><td>\";") + "str+=\"<table width=\\\"100%\\\" border=\\\"1\\\" bordercolor=\\\"#FFFFFF\\\" cellspacing=\\\"0\\\" cellpadding=\\\"0\\\"><tr>\";" + "str+=\"<td align=\\\"center\\\" class=\\\"DateTimeBoxTextStyle\\\" bgcolor=\\\"#FFFFFF\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\" style=\\\"cursor:hand;\\\" onClick=\\\"__DateBoxDateIsNow();\\\">\"+__DateBoxDateNowLanguage[10]+\"</td>\";") + "str+=\"</tr></table></td><td>\";" + "str+=\"<table width=\\\"100%\\\" border=\\\"1\\\" bordercolor=\\\"#FFFFFF\\\" cellspacing=\\\"0\\\" cellpadding=\\\"0\\\"><tr>\";") + "str+=\"<td align=\\\"center\\\" class=\\\"DateTimeBoxTextStyle\\\" bgcolor=\\\"#FFFFFF\\\" onmouseover=\\\"__DateBoxDateOnMouseOver(this);\\\" onmouseout=\\\"__DateBoxDateOnMouseOut(this);\\\" style=\\\"cursor:hand;\\\" onClick=\\\"__DateBoxDateIsClear();\\\">\"+__DateBoxDateNowLanguage[12]+\"</td>\";" + "str+=\"</tr></table>\";") + "str+=\"</td></tr></table></td></tr></table>\";" + "str+=\"</body>\";") + "str+=\"</html>\";" + Environment.NewLine) + "str+=\"<Script Language=\\\"JavaScript\\\">\";" + "str+=\"var __DateTimeNowDateTime=new Date();\";") + "str+=\"var __DateBoxDateMonthArray=[31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];\";" + "str+=\"var __DateBoxDateNowLanguage;\";") + "str+=\"var __DateTimeBoxEnglish=new Array(\\\"iso-8859-1\\\",\\\"S\\\",\\\"M\\\",\\\"T\\\",\\\"W\\\",\\\"T\\\",\\\"F\\\",\\\"S\\\",\\\"/\\\",\\\"\\\",\\\"Today\\\",\\\"Please input Date!\\\",\\\"Clear\\\");\";" + "str+=\"var __DateTimeBoxChinese=new Array(\\\"gb2312\\\",\\\"日\\\",\\\"一\\\",\\\"二\\\",\\\"三\\\",\\\"四\\\",\\\"五\\\",\\\"六\\\",\\\"年\\\",\\\"月\\\",\\\"今天\\\",\\\"请输入正确日期格式的数据！\\\\n例：1900-01-01\\\",\\\"清除\\\");\";") + "str+=\"function __DateBoxDateFlash(strNowDate){\";" + "str+=\"__DateTimeNowDateTime=new Date();\";") + "str+=\"if(strNowDate!=\\\"\\\"){\";" + "str+=\"if(strNowDate.indexOf(\\\"-\\\")>0){\";") + "str+=\"__DateTimeNowDateTime.setYear(strNowDate.split(\\\"-\\\")[0]);\";" + "str+=\"__DateTimeNowDateTime.setMonth(strNowDate.split(\\\"-\\\")[1]-1);\";") + "str+=\"__DateTimeNowDateTime.setDate(strNowDate.split(\\\"-\\\")[2]);\";" + "str+=\"}\";") + "str+=\"}\";" + "str+=\"var nowYear=__DateTimeNowDateTime.getYear();\";") + "str+=\"if(nowYear<100){\";" + "str+=\"nowYear+=1900;\";") + "str+=\"}\";" + "str+=\"var nowMonth=__DateTimeNowDateTime.getMonth();\";") + "str+=\"var firstDay=new Date(nowYear,nowMonth,1);\";" + "str+=\"var nowDay=firstDay.getDay();\";") + "str+=\"if((nowYear%4==0&&nowYear%100!=0)||(nowYear%400==0)){\";" + "str+=\"__DateBoxDateMonthArray[1]=29;\";") + "str+=\"}else{\";" + "str+=\"__DateBoxDateMonthArray[1]=28;\";") + "str+=\"}\";" + "str+=\"for(var i=1;;i++){\";") + "str+=\"if(document.all(\\\"__DateBoxDateItem\\\"+i)){\";" + "str+=\"document.all(\\\"__DateBoxDateItem\\\"+i).innerHTML=\\\"\\\";\";") + "str+=\"}else{\";" + "str+=\"break;\";") + "str+=\"}\";" + "str+=\"}\";") + "str+=\"for(var i=0;i<=__DateBoxDateMonthArray[nowMonth]+nowDay;i++){\";" + "str+=\"if(i>nowDay){\";") + "str+=\"if((i-nowDay)<10){\";" + "str+=\"document.all(\\\"__DateBoxDateItem\\\"+i).innerHTML=\\\"0\\\"+(i-nowDay);\";") + "str+=\"}else{\";" + "str+=\"document.all(\\\"__DateBoxDateItem\\\"+i).innerHTML=(i-nowDay);\";") + "str+=\"}\";" + "str+=\"}\";") + "str+=\"}\";" + "str+=\"document.all(\\\"__DateBoxDateNowDate\\\").innerHTML=nowYear+__DateTimeBoxEnglish[8]+(nowMonth+1)+__DateTimeBoxEnglish[9];\";") + "str+=\"}\";" + "str+=\"function __DateBoxDateChangeYear(_iYearNumber){\";") + "str+=\"__DateTimeNowDateTime.setYear(__DateTimeNowDateTime.getYear()+_iYearNumber);\";" + "str+=\"var nowYear=__DateTimeNowDateTime.getYear();\";") + "str+=\"var nowMonth=__DateTimeNowDateTime.getMonth()+1;\";" + "str+=\"var nowDate=__DateTimeNowDateTime.getDate();\";") + "str+=\"if(nowYear<1000){\";" + "str+=\"nowYear+=1900;\";") + "str+=\"}\";" + "str+=\"__DateBoxDateFlash(nowYear+\\\"-\\\"+nowMonth+\\\"-\\\"+nowDate);\";") + "str+=\"}\";" + "str+=\"function __DateBoxDateChangeMonth(_iMonthNumber){\";") + "str+=\"__DateTimeNowDateTime=new Date(__DateTimeNowDateTime.getYear(),__DateTimeNowDateTime.getMonth()+(_iMonthNumber),__DateTimeNowDateTime.getDate());\";" + "str+=\"var nowYear=__DateTimeNowDateTime.getYear();\";") + "str+=\"var nowMonth=__DateTimeNowDateTime.getMonth()+1;\";" + "str+=\"var nowDate=__DateTimeNowDateTime.getDate();\";") + "str+=\"if(nowYear<1000){\";" + "str+=\"nowYear+=1900;\";") + "str+=\"}\";" + "str+=\"__DateBoxDateFlash(nowYear+\\\"-\\\"+nowMonth+\\\"-\\\"+nowDate);\";") + "str+=\"}\";" + "str+=\"function __DateTimeBoxClose(){\";") + "str+=\"window.frames.parent.__DateTimeBoxDivMouseOut();\";" + "str+=\"}\";") + "str+=\"function __DateBoxDateOnMouseOver(obj){\";" + "str+=\"if(obj.innerText!=\\\"\\\"){\";") + "str+=\"obj.className=\\\"DateTimeBoxTextMouseOverStyle\\\";\";" + "str+=\"}\";") + "str+=\"}\";" + "str+=\"function __DateBoxDateOnMouseOut(obj){\";") + "str+=\"obj.className=\\\"DateTimeBoxTextMouseOutStyle\\\";\";" + "str+=\"}\";") + "str+=\"function __DateBoxDateIsClear(){\";" + "str+=\"window.frames.parent.__DateTimeBoxFlashString(\\\"\\\");\";") + "str+=\"}\";" + "str+=\"function __DateBoxDateIsNow(){\";") + "str+=\"__DateTimeNowDateTime=new Date();\";" + "str+=\"var nowYear=__DateTimeNowDateTime.getYear();\";") + "str+=\"var nowMonth=__DateTimeNowDateTime.getMonth()+1;\";" + "str+=\"var nowDate=__DateTimeNowDateTime.getDate();\";") + "str+=\"if(nowYear<1000){\";" + "str+=\"nowYear+=1900;\";") + "str+=\"}\";" + "str+=\"if(nowMonth<10){\";") + "str+=\"nowMonth=\\\"0\\\"+nowMonth.toString();\";" + "str+=\"}\";") + "str+=\"if(nowDate<10){\";" + "str+=\"nowDate=\\\"0\\\"+nowDate.toString();\";") + "str+=\"}\";" + "str+=\"window.frames.parent.__DateTimeBoxFlashString(nowYear+\\\"-\\\"+nowMonth+\\\"-\\\"+nowDate);\";") + "str+=\"}\";" + "str+=\"function __DateBoxDateSelected(obj){\";") + "str+=\"if(obj.innerText!=\\\"\\\"){\";" + "str+=\"var nowYear=__DateTimeNowDateTime.getYear();\";") + "str+=\"var nowMonth=__DateTimeNowDateTime.getMonth()+1;\";" + "str+=\"if(nowYear<1000){\";") + "str+=\"nowYear+=1900;\";" + "str+=\"}\";") + "str+=\"if(nowMonth<10){\";" + "str+=\"nowMonth=\\\"0\\\"+nowMonth.toString();\";") + "str+=\"}\";" + "str+=\"window.frames.parent.__DateTimeBoxFlashString(nowYear+\\\"-\\\"+nowMonth+\\\"-\\\"+obj.innerText);\";") + "str+=\"}\";" + "str+=\"}\";") + "str+=\"<\\/Script>\";" + "var BoxWin=window.open(\"\",\"__DateTimeIFrame\");") + "BoxWin.document.clear();" + "BoxWin.document.open();") + "BoxWin.document.write(str);" + "BoxWin.document.write(\"<Script Language=\\\"JavaScript\\\">__DateBoxDateFlash('\"+dates+\"');<\\/Script>\");") + "BoxWin.document.close();" + "}") + "__DateTimeBoxInit();" + "function __DateTimeBoxFlashString(str){") + "if(__DateTimeBoxSelectedObject){" + "__DateTimeBoxSelectedObject.value=str;";
                if (this.TextChanged != null)
                {
                    text = text + this.Page.GetPostBackEventReference(this) + ";";
                }
                text = text + "__DateTimeBoxAllHidden();}}" + "</Script>";
                this.Page.RegisterClientScriptBlock("DateTimeBoxScript", "");
            }
            cell.Controls.Add(box);
            cell2.Controls.Add(image);
            row.Controls.Add(cell);
            row.Controls.Add(cell2);
            child.Controls.Add(row);
            this.Controls.Add(child);
            this.Controls.Add(new LiteralControl(text));
        }

        protected override void OnLoad(EventArgs e)
        {
            this.EnsureChildControls();
            this.Text = this.g_TextBox.Text;
            base.OnLoad(e);
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            this.TextChanged(this, e);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(EventArgs.Empty);
        }

        [Category("Appearance"), DefaultValue(0), Description("日历的语言")]
        public Language CalendarLanguage
        {
            get
            {
                return this.g_Language;
            }
            set
            {
                this.g_Language = value;
            }
        }

        [Category("Appearance"), Description("样式表"), DefaultValue(true)]
        public override string CssClass
        {
            get
            {
                return this.g_strCssClass;
            }
            set
            {
                this.g_strCssClass = value;
            }
        }

        [DefaultValue(""), Browsable(false), Description("时间值"), Category("Data")]
        public DateTime Date
        {
            get
            {
                try
                {
                    DateTime.Parse(this.Text);
                }
                catch (Exception exception)
                {
                    throw new Exception("Date format error!" + exception.ToString());
                }
                return DateTime.Parse(this.Text);
            }
        }

        [Description("日期对齐"), DefaultValue(2), Category("Appearance")]
        public TextAlign DateTextAlign
        {
            get
            {
                return this.g_DateTextAlign;
            }
            set
            {
                this.g_DateTextAlign = value;
            }
        }

        [DefaultValue("/ZL_Client/WebControls/DateTimeBox/DateTimeBox.gif"), Description("日历图片的链接的URL"), Category("Appearance")]
        public string ImageUrl
        {
            get
            {
                return this.g_strImageUrl;
            }
            set
            {
                this.g_strImageUrl = value;
            }
        }

        [Browsable(false), Category("Data"), Description("日期框是否为空")]
        public bool IsEmpty
        {
            get
            {
                try
                {
                    DateTime.Parse(this.Text);
                }
                catch
                {
                    return true;
                }
                return false;
            }
        }

        [DefaultValue(true), Description("是否只读"), Category("Behavior")]
        public bool ReadOnly
        {
            get
            {
                return this.g_bReadOnly;
            }
            set
            {
                this.g_bReadOnly = value;
            }
        }

        [Description("日期的文本值"), Browsable(true), DefaultValue(""), Category("Data")]
        public string Text
        {
            get
            {
                return this.g_strText;
            }
            set
            {
                try
                {
                    string s = value;
                    if (s != "")
                    {
                        try
                        {
                            DateTime time = DateTime.Parse(s);
                            s = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
                        }
                        catch
                        {
                        }
                    }
                    this.g_strText = s;
                }
                catch
                {
                    this.g_strText = "";
                }
            }
        }

        public enum Language
        {
            English,
            Chinese,
            ChineseBig5
        }

        public enum TextAlign
        {
            Left,
            Right,
            Center
        }
    }
}

