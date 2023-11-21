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
        //declaring and initializing connString
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\carso\\OneDrive\\Desktop\\KarateSchool213\\carson.bring_Assignment4\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        //Login authentication method
        protected void Login1_Authenticate1(object sender, AuthenticateEventArgs e)
        {
            //retreiving Login credentials and putting into local variable
            string userName = Login1.UserName;
            string password = Login1.Password;
            //using the data context
            using (KarateSchoolDataContext context = new KarateSchoolDataContext(connString))
            {
                //Seeing if the username and password exist in the database
                var user = context.NetUsers.SingleOrDefault(u => u.UserName == userName && u.UserPassword == password);

                if (user != null)
                {
                    //If the user exists in the database
                    if (user.UserType == "Member")
                    {
                        //Get the associated member and set the Session variables to display name in Member page
                        var member = context.Members.SingleOrDefault(m => m.Member_UserID == user.UserID);
                        Session["FirstName"] = member.MemberFirstName;
                        Session["LastName"] = member.MemberLastName;
                        Session["UserID"] = member.Member_UserID;
                        Response.Redirect("mywork/Member.aspx", true);
                    }
                    else if (user.UserType == "Instructor")
                    {
                        //Get the associated instrucot and set the Session variables to display name in Instructor page
                        var instructor = context.Instructors.SingleOrDefault(i => i.InstructorID == user.UserID);
                        Session["FirstName"] = instructor.InstructorFirstName;
                        Session["LastName"] = instructor.InstructorLastName;
                        Session["UserID"] = instructor.InstructorID;
                        Response.Redirect("mywork/Instructor.aspx", true);
                    }
                    else if (user.UserType == "Administrator")
                    {
                        var admin = context.Instructors.SingleOrDefault(a => a.InstructorID == user.UserID);
                        //redirect to admin page since Session variables arent used for admin
                        Response.Redirect("mywork/Administrator.aspx", true);
                    }

                }
                else
                {
                    //Credentials are incorrect so we redirect to the same page
                    Response.Redirect("Default.aspx", true);
                }
            }
        }
    }
}