using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool
{
    public partial class logon : System.Web.UI.Page
    {
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\carso\\OneDrive\\Desktop\\KarateSchool213\\carson.bring_Assignment4\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string userName = Login1.UserName;
            string password = Login1.Password;

            using (KarateSchoolDataContext context = new KarateSchoolDataContext(connString))
            {
                var user = context.NetUsers.SingleOrDefault(u => u.UserName == userName && u.UserPassword == password);

                if (user != null)
                {
                    // Credentials are correct
                    FormsAuthentication.RedirectFromLoginPage(userName, true);
                }
                else
                {
                    // Credentials are incorrect
                    Response.Redirect("logon.aspx", true);
                }
            }
        }
    }
}