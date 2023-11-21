using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool.mywork
{
    public partial class Member : System.Web.UI.Page
    {
        //Initializing connString and delcaring context
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\carso\\OneDrive\\Desktop\\KarateSchool213\\carson.bring_Assignment4\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext context;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Displaying first and last name from session variables
                context = new KarateSchoolDataContext(connString);
                string firstname = Session["FirstName"] as string;
                string lastname = Session["LastName"] as string;
                int userid = Convert.ToInt32(Session["UserID"]);

                Name.Text = firstname + " " + lastname;
                //querying for Members section information to bind to the members grid view
                var query = from section in context.Sections
                            join instructor in context.Instructors
                            on section.Instructor_ID equals instructor.InstructorID
                            where section.Member_ID == userid
                            select new
                            {
                                section.SectionName,
                                InstructorName = instructor.InstructorFirstName + " " + instructor.InstructorLastName,
                                section.SectionStartDate,
                                section.SectionFee
                            };
                decimal totalSectionFee = query.Sum(x => x.SectionFee);
                TotalFees.Text = totalSectionFee.ToString();
                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
            }
        }
    }
}