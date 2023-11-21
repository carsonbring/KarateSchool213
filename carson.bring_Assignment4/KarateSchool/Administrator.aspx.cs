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
        //initializing connection string
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
                                  m.Member_UserID,
                                  m.MemberFirstName,
                                  m.MemberLastName,
                                  m.MemberPhoneNumber,
                                  m.MemberDateJoined
                              };

                GridView3.DataSource = members.ToList();
                GridView3.DataBind();
            }
        }
        private void ClearAddMember()
        {
            MemFirstName.Text = "";
            MemLastName.Text = "";
            MemPhone.Text = "";
            MemEmail.Text = "";
            MemUsername.Text = "";
            MemPassword.Text = "";

        }
        private void ClearAddInstructor()
        {
            InstFirst.Text = "";
            InstLast.Text = "";
            InstPassword.Text = "";
            InstPhone.Text = "";
            InstUsername.Text = "";
            

        }
        private void ClearAssign()
        {
            assignInstructorId.Text = "";
            assignMemId.Text = "";
            SectionFee.Text = "";
        }
        // load instructors into GridView 
        private void LoadInstructors()
        {
            using (KarateSchoolDataContext dbContext = new KarateSchoolDataContext(connString))
            {
                var instructors = from i in dbContext.Instructors
                                  select new
                                  {
                                      i.InstructorID,
                                      i.InstructorFirstName,
                                      i.InstructorLastName,
                                
                                  };

                GridView2.DataSource = instructors.ToList();
                GridView2.DataBind();
            }
        }



        protected void btnAddMember_Click(object sender, EventArgs e)
        {
           
      
            string firstname = MemFirstName.Text.Trim();
            string lastname = MemLastName.Text.Trim();
            DateTime today = DateTime.Now;
            string phonenumber = MemPhone.Text.Trim();
            string email = MemEmail.Text.Trim();
            string username = MemUsername.Text.Trim();
            string password = MemPassword.Text.Trim();

            
            using (context = new KarateSchoolDataContext(connString))
            {
                NetUser user = new NetUser
                {
                    UserName = username,
                    UserPassword = password,
                    UserType = "Member"
                    
                };
                context.NetUsers.InsertOnSubmit(user);
                context.SubmitChanges();
                int generatedUserId = user.UserID;
                Member newMember = new Member
                {
                    Member_UserID = generatedUserId,
                    MemberFirstName = firstname,
                    MemberLastName = lastname,
                    MemberDateJoined = today,
                    MemberPhoneNumber = phonenumber,
                    MemberEmail = email,


                };
                
                context.Members.InsertOnSubmit(newMember);
                context.SubmitChanges();
                LoadMembers();
                ClearAddMember();
                string script = "alert('Member Added');";
                ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
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
                var entitiesToDelete1 = context.Sections.Where(x => x.Member_ID == primaryKeyValue).ToList();
                var entityToDelete2 = context.Members.SingleOrDefault(x => x.Member_UserID == primaryKeyValue);
                var entityToDelete3 = context.NetUsers.SingleOrDefault(x => x.UserID == primaryKeyValue);
                if (entitiesToDelete1.Count() > 0)
                {
                    context.Sections.DeleteAllOnSubmit(entitiesToDelete1);
                    context.SubmitChanges();
                }
                if (entityToDelete2 != null)
                {
                    // Remove the entity from the DataContext
                   
                    context.Members.DeleteOnSubmit(entityToDelete2);
                    context.NetUsers.DeleteOnSubmit(entityToDelete3);

                    // Save changes to the database
                    context.SubmitChanges();

                    // Rebind the GridView to refresh the data
                   
                    LoadMembers();
                    string script = "alert('Member Deleted');";
                    ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
                }
            }
        }


        protected void btnAssignMember_Click(object sender, EventArgs e)
        {
            using (context = new KarateSchoolDataContext(connString))
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
                    Member_ID = memid,
                    SectionFee = sectionfee

                };

                context.Sections.InsertOnSubmit(newSection);
                context.SubmitChanges();
                ClearAssign();
                string script = "alert('Section Assignment Completed');";
                ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);

            }
        }
    

        protected void btnAddInstructor_Click(object sender, EventArgs e)
        {

            using (context = new KarateSchoolDataContext(connString))
            {
                string firstname = InstFirst.Text.Trim();
                string lastname = InstLast.Text.Trim();
                DateTime today = DateTime.Now;
                string phonenumber = InstPhone.Text.Trim();
                string username = InstUsername.Text.Trim();
                string password = InstPassword.Text.Trim();
                NetUser user = new NetUser
                {
                    UserName = username,
                    UserPassword = password,
                    UserType = "Instructor"
                };
                context.NetUsers.InsertOnSubmit(user);
                context.SubmitChanges();
                int generatedUserId = user.UserID;
                Instructor newInstructor = new Instructor
                {
                    InstructorID = generatedUserId,
                    InstructorFirstName = firstname,
                    InstructorLastName = lastname,
                    InstructorPhoneNumber = phonenumber

                };

                context.Instructors.InsertOnSubmit(newInstructor);
                context.SubmitChanges();
                LoadInstructors();
                ClearAddInstructor();
                string script = "alert('Add Instructor Completed');";
                ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
            }
        }

       

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the primary key value of the row to be deleted
            int primaryKeyValue = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values["InstructorID"]);

            // Assuming you have a data context called YourDataContext
            using (context = new KarateSchoolDataContext(connString))
            {
                var entitiesToDelete1 = context.Sections.Where(x => x.Member_ID == primaryKeyValue).ToList();
                var entityToDelete2 = context.Instructors.SingleOrDefault(x => x.InstructorID == primaryKeyValue);
                var entityToDelete3 = context.NetUsers.SingleOrDefault(x => x.UserID == primaryKeyValue);

                if (entitiesToDelete1.Count() > 0 )
                {
                    context.Sections.DeleteAllOnSubmit(entitiesToDelete1);
                    context.SubmitChanges();
                }

                if (entityToDelete2 != null)
                {
                    // Remove the entity from the DataContext
                    
                    context.Instructors.DeleteOnSubmit(entityToDelete2);
                    context.NetUsers.DeleteOnSubmit(entityToDelete3);

                    // Save changes to the database
                    context.SubmitChanges();

                    // Rebind the GridView to refresh the data
                    
                    LoadInstructors();
                    string script = "alert('Instructor Deleted');";
                    ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
                }

            }
        }

        protected void btnAddMemClear_Click(object sender, EventArgs e)
        {
            ClearAddMember();
        }

        protected void btnAddInstructorClear_Click(object sender, EventArgs e)
        {
            ClearAddInstructor();
        }

        protected void btnAssignMemClear_Click(object sender, EventArgs e)
        {
            ClearAssign();
        }
    }
    
}