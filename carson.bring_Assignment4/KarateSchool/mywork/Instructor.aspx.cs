using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool.mywork
{
    public partial class Instructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string firstname = Session["FirstName"] as string;
            string lastname = Session["LastName"] as string;
            Name.Text = firstname + " " + lastname;
        }
    }
}