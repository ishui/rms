namespace RmsReport
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ReportControl runat=server></{0}:ReportControl>"), DefaultProperty("Text")]
    public class ReportControl : Panel
    {
        protected double HeightMm;
        protected DataTable m_DataTable;
        protected ReportSection m_Detail;
        protected Unit m_Height = Unit.Pixel(GetPixel(297));
        protected e_Orientation m_Orientation = e_Orientation.Portrait;
        protected ReportSection m_PageFooter;
        protected ReportSection m_PageHead;
        protected e_PageSize m_PageSize = e_PageSize.A4;
        private double m_UsedDetailHeight = 0;
        private double m_ValidDetailHeight = 0;
        protected Unit m_Width = Unit.Pixel(GetPixel(210));
        protected double MarginBottom = 20;
        protected double MarginTop = 20;
        protected double WidthMm;

        private void CalcValidDetailHeight()
        {
            this.m_ValidDetailHeight = (this.HeightMm - this.MarginTop) - this.MarginBottom;
            if ((this.m_PageHead != null) && this.m_PageHead.Visible)
            {
                this.m_ValidDetailHeight -= this.m_PageHead.HeightMm();
            }
            if ((this.m_PageFooter != null) && this.m_PageFooter.Visible)
            {
                this.m_ValidDetailHeight -= this.m_PageFooter.HeightMm();
            }
        }

        private void EndPage(HtmlTextWriter writer)
        {
            this.WritePageFooter(writer);
        }

        private string GetFieldValue(DataRow dr, string FieldName)
        {
            try
            {
                return dr[FieldName].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static double GetMm(Unit val)
        {
            if (val.Type == UnitType.Pixel)
            {
                return (val.Value * 0.25);
            }
            return val.Value;
        }

        public static int GetPixel(double mm)
        {
            try
            {
                double num = mm / 0.25;
                return int.Parse(num.ToString());
            }
            catch
            {
                return 200;
            }
        }

        private void NewPage(HtmlTextWriter writer)
        {
            this.EndPage(writer);
            string text = "PAGE-BREAK-BEFORE: always";
            writer.AddAttribute(HtmlTextWriterAttribute.Style, text);
            this.StartPage(writer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.ResetControl();
            this.CalcValidDetailHeight();
            this.StartPage(writer);
            this.WriteDetail(writer);
            this.EndPage(writer);
        }

        private void ResetControl()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].ToString() == "RmsReport.ReportSection")
                {
                    ReportSection section = (ReportSection) this.Controls[i];
                    switch (section.SectionType)
                    {
                        case ReportSection.e_SectionType.PageHead:
                            this.m_PageHead = section;
                            break;

                        case ReportSection.e_SectionType.Detail:
                            this.m_Detail = section;
                            break;

                        case ReportSection.e_SectionType.PageFooter:
                            this.m_PageFooter = section;
                            break;
                    }
                }
            }
        }

        protected void ResetPageSize()
        {
            switch (this.m_PageSize)
            {
                case e_PageSize.A3:
                    if (this.m_Orientation != e_Orientation.Portrait)
                    {
                        this.Width = Unit.Pixel(GetPixel(400));
                        this.Height = Unit.Pixel(GetPixel(300));
                        return;
                    }
                    this.Width = Unit.Pixel(GetPixel(300));
                    this.Height = Unit.Pixel(GetPixel(400));
                    return;
            }
            if (this.m_Orientation == e_Orientation.Portrait)
            {
                this.Width = Unit.Pixel(GetPixel(210));
                this.Height = Unit.Pixel(GetPixel(297));
            }
            else
            {
                this.Width = Unit.Pixel(GetPixel(297));
                this.Height = Unit.Pixel(GetPixel(210));
            }
        }

        private void StartPage(HtmlTextWriter writer)
        {
            this.m_UsedDetailHeight = 0;
            this.WritePageHead(writer);
        }

        private void WriteDetail(HtmlTextWriter writer)
        {
            if (((this.m_Detail != null) && this.m_Detail.Visible) && (this.m_DataTable != null))
            {
                for (int i = 0; i < this.m_DataTable.Rows.Count; i++)
                {
                    DataRow dr = this.m_DataTable.Rows[i];
                    for (int j = 0; j < this.m_Detail.Controls.Count; j++)
                    {
                        if (this.m_Detail.Controls[j].ToString() == "System.Web.UI.WebControls.Label")
                        {
                            Label label = (Label) this.m_Detail.Controls[j];
                            if (label.ID.StartsWith("field_"))
                            {
                                string fieldName = label.ID.Remove(0, "field_".Length);
                                label.Text = this.GetFieldValue(dr, fieldName);
                            }
                        }
                    }
                    if ((this.m_UsedDetailHeight + this.m_Detail.HeightMm()) > this.m_ValidDetailHeight)
                    {
                        this.NewPage(writer);
                    }
                    this.m_Detail.RenderControl(writer);
                    this.m_UsedDetailHeight += this.m_Detail.HeightMm();
                }
            }
        }

        private void WritePageFooter(HtmlTextWriter writer)
        {
            if ((this.m_PageFooter != null) && this.m_PageFooter.Visible)
            {
                this.m_PageFooter.RenderControl(writer);
                string text = "PAGE-BREAK-BEFORE: always";
                writer.AddAttribute(HtmlTextWriterAttribute.Style, text);
            }
        }

        private void WritePageHead(HtmlTextWriter writer)
        {
            if ((this.m_PageHead != null) && this.m_PageHead.Visible)
            {
                this.m_PageHead.RenderControl(writer);
            }
        }

        public DataTable DataSource
        {
            get
            {
                return this.m_DataTable;
            }
            set
            {
                this.m_DataTable = value;
            }
        }

        public override Unit Height
        {
            get
            {
                return this.m_Height;
            }
            set
            {
                this.m_Height = value;
                this.HeightMm = GetMm(this.m_Height);
            }
        }

        public e_Orientation Orientation
        {
            get
            {
                return this.m_Orientation;
            }
            set
            {
                this.m_Orientation = value;
                this.ResetPageSize();
            }
        }

        public e_PageSize PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
                this.ResetPageSize();
            }
        }

        public override Unit Width
        {
            get
            {
                return this.m_Width;
            }
            set
            {
                this.m_Width = value;
                this.WidthMm = GetMm(this.m_Width);
            }
        }

        public enum e_Orientation
        {
            Portrait,
            Landscape
        }

        public enum e_PageSize
        {
            A3,
            A4
        }
    }
}

