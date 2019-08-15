namespace Codefresh.PhotoBrowserLibrary
{
    using System;

    [Serializable]
    public class Comment : PhotoObjectBase
    {
        private string comment;
        private DateTime dateAdded;
        private string name;

        public Comment(string name, string comment)
        {
            this.name = name;
            this.comment = comment;
            this.dateAdded = DateTime.Now;
        }

        internal Comment(SessionToken token, int id, string name, string comment, DateTime dateAdded) : this(name, comment)
        {
            this.dateAdded = dateAdded;
            base.SetId(id);
        }

        public string CommentText
        {
            get
            {
                return this.comment;
            }
        }

        public DateTime DateAdded
        {
            get
            {
                return this.dateAdded;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}

