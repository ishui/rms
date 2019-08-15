namespace RmsPM.BLL
{
    using System;
    using System.Configuration;
    using System.Web.Mail;

    public class MailRule
    {
        private string _Addressee;
        private string _Body = "";
        private string _SendName;
        private string _ShowMode = "";
        private string _SmtpServer = "";
        private string _SmtpServerPwd = "";
        private string _SmtpServerUid = "";
        private string _Title = "";
        private string _ToMail = "";

        private void BiddingType()
        {
            if (this.ShowMode == "1")
            {
                this.Body = " " + this.Addressee + ":" + this.Body;
            }
        }

        public void sendMail()
        {
            try
            {
                if ((ConfigurationSettings.AppSettings["MailServer"] != "false") && (this.ToMail.Trim() != ""))
                {
                    if (this._SmtpServer == "")
                    {
                        this.SmtpServer = ConfigurationSettings.AppSettings["MailServer"];
                    }
                    if (this._SmtpServerUid == "")
                    {
                        this.SmtpServerUid = ConfigurationSettings.AppSettings["MailUser"];
                    }
                    if (this._SmtpServerPwd == "")
                    {
                        this.SmtpServerPwd = ConfigurationSettings.AppSettings["MailPwd"];
                    }
                    MailMessage message = new MailMessage();
                    message.From = this._SmtpServerUid;
                    message.Subject = this._Title;
                    message.Priority = MailPriority.Low;
                    message.BodyFormat = MailFormat.Text;
                    message.Priority = MailPriority.Normal;
                    this.BiddingType();
                    message.Body = this._Body;
                    message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                    message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", this._SmtpServerUid);
                    message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", this._SmtpServerPwd);
                    if (ConfigurationSettings.AppSettings["SmtpPort"] != "25")
                    {
                        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", ConfigurationSettings.AppSettings["SmtpPort"]);
                    }
                    if (ConfigurationSettings.AppSettings["SmtpUseSSL"] == "true")
                    {
                        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "1");
                    }
                    SmtpMail.SmtpServer = this._SmtpServer;
                    message.BodyFormat = MailFormat.Html;
                    if (this.ToMail.Contains(";"))
                    {
                        foreach (string text in this.ToMail.Split(new char[] { ';' }))
                        {
                            if (text != "")
                            {
                                message.To = text;
                                SmtpMail.Send(message);
                            }
                        }
                    }
                    else
                    {
                        message.To = this.ToMail;
                        SmtpMail.Send(message);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string Addressee
        {
            get
            {
                return this._Addressee;
            }
            set
            {
                this._Addressee = value;
            }
        }

        public string Body
        {
            get
            {
                return this._Body;
            }
            set
            {
                this._Body = value;
            }
        }

        public string SendName
        {
            get
            {
                return this._SendName;
            }
            set
            {
                this._SendName = value;
            }
        }

        public string ShowMode
        {
            get
            {
                return this._ShowMode;
            }
            set
            {
                this._ShowMode = value;
            }
        }

        public string SmtpServer
        {
            get
            {
                return this._SmtpServer;
            }
            set
            {
                this._SmtpServer = value;
            }
        }

        public string SmtpServerPwd
        {
            get
            {
                return this._SmtpServerPwd;
            }
            set
            {
                this._SmtpServerPwd = value;
            }
        }

        public string SmtpServerUid
        {
            get
            {
                return this._SmtpServerUid;
            }
            set
            {
                this._SmtpServerUid = value;
            }
        }

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public string ToMail
        {
            get
            {
                return this._ToMail;
            }
            set
            {
                this._ToMail = value;
            }
        }
    }
}

