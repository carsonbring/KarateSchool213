using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool
{
    public partial class _Default : Page
    {
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\carso\\OneDrive\\Desktop\\KarateSchool213\\carson.bring_Assignment4\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        protected void Login1_Authenticate1(object sender, AuthenticateEventArgs e)
        {
            string userName = Login1.UserName;
            string password = Login1.Password;

            using (KarateSchoolDataContext context = new KarateSchoolDataContext(connString))
            {
                var user = context.NetUsers.SingleOrDefault(u => u.UserName == userName && u.UserPassword == password);

                if (user != null)
                {
                    // Redirect based on UserType
                    if (user.UserType == "Member")
                    {
                        var member = context.Members.SingleOrDefault(m => m.Member_UserID == user.UserID);
                        Session["FirstName"] = member.MemberFirstName;
                        Session["LastName"] = member.MemberLastName;
                        Session["UserID"] = member.Member_UserID;
                        Response.Redirect("mywork/Member.aspx", true);
                    }
                    else if (user.UserType == "Instructor")
                    {
                        var instructor = context.Instructors.SingleOrDefault(i => i.InstructorID == user.UserID);
                        Session["FirstName"] = instructor.InstructorFirstName;
                        Session["LastName"] = instructor.InstructorLastName;
                        Session["UserID"] = instructor.InstructorID;
                        Response.Redirect("mywork/Instructor.aspx", true);
                    }
                    else if (user.UserType == "Administrator")
                    {
                        var admin = context.Instructors.SingleOrDefault(a => a.InstructorID == user.UserID);

                        Response.Redirect("mywork/Administrator.aspx", true);
                    }

                }
                else
                {
                    // Credentials are incorrect
                    Response.Redirect("Default.aspx", true);
                }
            }
        }
    }
}