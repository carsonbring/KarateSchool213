using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool

{
    public partial class Administrator : System.Web.UI.Page
    {
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\carso\\OneDrive\\Desktop\\KarateSchool213\\carson.bring_Assignment4\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        
        KarateSchoolDataContext context;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMembers();
                LoadInstructors();
            }

        }

        // load members into GridView
        private void LoadMembers()
        {
            using (KarateSchoolDataContext dbContext = new KarateSchoolDataContext(connString))
            {
                var members = from m in dbContext.Members
                              select new
                              {
                                  m.MemberFirstName,
                                  m.MemberLastName,
                                  m.MemberPhoneNumber,
                                  m.MemberDateJoined
                              };

                GridView3.DataSource = members.ToList();
                GridView3.DataBind();
            }
        }

        // load instructors into GridView 
        private void LoadInstructors()
        {
            using (KarateSchoolDataContext dbContext = new KarateSchoolDataContext(connString))
            {
                var instructors = from i in dbContext.Instructors
                                  select new
                                  {
                                      i.InstructorFirstName,
                                      i.InstructorLastName,
                                
                                  };

                GridView2.DataSource = instructors.ToList();
                GridView2.DataBind();
            }
        }



        protected void btnAddMember_Click(object sender, EventArgs e)
        {
           
            int userId= Convert.ToInt32(MemID.Text.Trim());
            string firstname = MemFirstName.Text.Trim();
            string lastname = MemLastName.Text.Trim();
            DateTime today = DateTime.Now;
            string phonenumber = MemPhone.Text.Trim();
            string email = MemEmail.Text.Trim();


            
            using (context = new KarateSchoolDataContext(connString))
            {
                Member newMember = new Member
                {
                    Member_UserID = userId,
                    MemberFirstName = firstname,
                    MemberLastName = lastname,
                    MemberDateJoined = today,
                    MemberPhoneNumber = phonenumber,
                    MemberEmail = email,


                };
                
                context.Members.InsertOnSubmit(newMember);
                context.SubmitChanges();
                LoadMembers();
            }
            

           
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the primary key value of the row to be deleted
            int primaryKeyValue = Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Values["Member_UserID"]);
            
            // Assuming you have a data context called YourDataContext
            using (context = new KarateSchoolDataContext(connString))
            {
                // Retrieve the entity to be deleted
                var entityToDelete = context.Members.SingleOrDefault(x => x.Member_UserID == primaryKeyValue);

                if (entityToDelete != null)
                {
                    // Remove the entity from the DataContext
                    context.Members.DeleteOnSubmit(entityToDelete);

                    // Save changes to the database
                    context.SubmitChanges();

                    // Rebind the GridView to refresh the data
                    GridView3.DataBind();
                }
            }
        }


        protected void btnAssignMember_Click(object sender, EventArgs e)
        {

            string sectionName = RadioButtonList1.SelectedValue;
            int memid = Convert.ToInt32(assignMemId.Text.Trim());
            int instructorid = Convert.ToInt32((assignInstructorId.Text.Trim()));  
            decimal sectionfee = Convert.ToDecimal(SectionFee.Text.Trim());
            DateTime today = DateTime.Now;
            Section newSection = new Section
            {
                SectionName = sectionName,
                SectionStartDate = today,
                Instructor_ID = instructorid,
                Member_ID   = memid,
                SectionFee = sectionfee

            };

            context.Sections.InsertOnSubmit(newSection);
            context.SubmitChanges();
            
        }
    

        protected void btnAddInstructor_Click(object sender, EventArgs e)
        {

            int userId = Convert.ToInt32(InstID.Text.Trim());
            string firstname =  InstFirst.Text.Trim();
            string lastname = InstLast.Text.Trim();
            DateTime today = DateTime.Now;
            string phonenumber = InstPhone.Text.Trim();
            Instructor newInstructor = new Instructor
            {
                InstructorID = userId,
                InstructorFirstName = firstname,
                InstructorLastName = lastname,
                InstructorPhoneNumber = phonenumber

            };

            context.Instructors.InsertOnSubmit(newInstructor);
            context.SubmitChanges();
            LoadInstructors();
        }

       

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the primary key value of the row to be deleted
            int primaryKeyValue = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values["InstructorID"]);

            // Assuming you have a data context called YourDataContext
            using (context = new KarateSchoolDataContext(connString))
            {
                // Retrieve the entity to be deleted
                var entityToDelete = context.Instructors.SingleOrDefault(x => x.InstructorID == primaryKeyValue);

                if (entityToDelete != null)
                {
                    // Remove the entity from the DataContext
                    context.Instructors.DeleteOnSubmit(entityToDelete);

                    // Save changes to the database
                    context.SubmitChanges();

                    // Rebind the GridView to refresh the data
                    GridView3.DataBind();
                }
            }
        }
    }
    
}