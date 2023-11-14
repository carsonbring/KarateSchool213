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
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\carso\\OneDrive\\Desktop\\KarateSchool213\\carson.bring_Assignment4\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext context;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                context = new KarateSchoolDataContext(connString);
                string firstname = Session["FirstName"] as string;
                string lastname = Session["LastName"] as string;
                int userid = Convert.ToInt32(Session["UserID"]);

                Name.Text = firstname + " " + lastname;

                var query = from section in context.Sections
                            join member in context.Members
                            on section.Member_ID equals member.Member_UserID
                            where section.Instructor_ID == userid
                            select new
                            {
                                section.SectionName,
                                MemberName = member.MemberFirstName + " " + member.MemberLastName
                                
                            };
                
                
                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
            }
        }
    }
}