using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.MobileControls.Adapters;
using System.Web.UI.WebControls;

namespace chapter_04_gridview
{
    public class LookupLabel : ListControl
    {
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Label;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(this.SelectedItem.Text);
        }

        [Themeable(false), DefaultValue(""), TypeConverter(typeof(AssociatedControlConverter)), IDReferenceProperty]
        public virtual string AssociatedControlID
        {
            get
            {
                string text1 = (string)this.ViewState["AssociatedControlID"];
                if (text1 != null)
                {
                    return text1;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["AssociatedControlID"] = value;
            }
        }

        internal bool AssociatedControlInControlTree
        {
            get
            {
                object obj1 = this.ViewState["AssociatedControlNotInControlTree"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }
            set
            {
                this.ViewState["AssociatedControlNotInControlTree"] = value;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            string text1 = this.AssociatedControlID;
            if (text1.Length != 0)
            {
                if (this.AssociatedControlInControlTree)
                {
                    Control control1 = this.FindControl(text1);
                    if (control1 == null)
                    {
                        if (!DesignMode)
                        {
                            throw new HttpException(SR.GetString("LabelForNotFound", new object[] { text1, this.ID }));
                        }
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.For, control1.ClientID);
                    }
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.For, text1);
                }
            }
            base.AddAttributesToRender(writer);
        }
    }
}