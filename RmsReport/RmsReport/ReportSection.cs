namespace RmsReport
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ReportSection runat=server></{0}:ReportSection>"), DefaultProperty("Text")]
    public class ReportSection : Panel
    {
        protected Unit m_Height = Unit.Pixel(100);
        protected e_SectionType m_SectionType;
        protected Unit m_Width = Unit.Percentage(100);
        public double WidthMm;

        public double HeightMm()
        {
            return ReportControl.GetMm(this.m_Height);
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.Width = Unit.Percentage(100);
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
            }
        }

        public e_SectionType SectionType
        {
            get
            {
                return this.m_SectionType;
            }
            set
            {
                this.m_SectionType = value;
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
                this.WidthMm = ReportControl.GetMm(this.m_Width);
            }
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public enum e_SectionType
        {
            PageHead,
            Detail,
            PageFooter
        }
    }
}

