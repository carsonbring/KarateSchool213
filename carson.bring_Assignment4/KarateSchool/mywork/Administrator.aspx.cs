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


        //Loading data into grid views
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
        //Clear add member text boxes
        private void ClearAddMember()
        {
            MemFirstName.Text = "";
            MemLastName.Text = "";
            MemPhone.Text = "";
            MemEmail.Text = "";
            MemUsername.Text = "";
            MemPassword.Text = "";

        }
        //Clearing add instructor text boxes
        private void ClearAddInstructor()
        {
            InstFirst.Text = "";
            InstLast.Text = "";
            InstPassword.Text = "";
            InstPhone.Text = "";
            InstUsername.Text = "";
            

        }
        //clear Section assignment text boxes
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


        //method that executes when the add member button is clicked
        protected void btnAddMember_Click(object sender, EventArgs e)
        {
           
            //setting local variabels using text box values
            string firstname = MemFirstName.Text.Trim();
            string lastname = MemLastName.Text.Trim();
            DateTime today = DateTime.Now;
            string phonenumber = MemPhone.Text.Trim();
            string email = MemEmail.Text.Trim();
            string username = MemUsername.Text.Trim();
            string password = MemPassword.Text.Trim();

            //using the data context
            using (context = new KarateSchoolDataContext(connString))
            {
                //creating a new NetUser to insert before inserting into Member table
                NetUser user = new NetUser
                {
                    UserName = username,
                    UserPassword = password,
                    UserType = "Member"
                    
                };
                context.NetUsers.InsertOnSubmit(user);
                context.SubmitChanges();
                //capturing the generated user id to use in the Member table since its a foreign key
                int generatedUserId = user.UserID;
                //creating member
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
                //alerting the user if it was successful
                string script = "alert('Member Added');";
                ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
            }
            

           
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Get the primary key value of the row to be deleted using the DataKey value
            int primaryKeyValue = Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Values["Member_UserID"]);
            
           //using the data context
            using (context = new KarateSchoolDataContext(connString))
            {
                //Retrieve the entities to be deleted - might be multiple sections so I store as a list
                var entitiesToDelete1 = context.Sections.Where(x => x.Member_ID == primaryKeyValue).ToList();
                var entityToDelete2 = context.Members.SingleOrDefault(x => x.Member_UserID == primaryKeyValue);
                var entityToDelete3 = context.NetUsers.SingleOrDefault(x => x.UserID == primaryKeyValue);
                //Delete the sections
                if (entitiesToDelete1.Count() > 0)
                {
                    context.Sections.DeleteAllOnSubmit(entitiesToDelete1);
                    context.SubmitChanges();
                }
                //Delete the rows from member table and net user for last due to foreign key restriction
                if (entityToDelete2 != null)
                {
                    context.Members.DeleteOnSubmit(entityToDelete2);
                    context.NetUsers.DeleteOnSubmit(entityToDelete3);
                    context.SubmitChanges();

                    LoadMembers();
                    //alerting if action was successful
                    string script = "alert('Member Deleted');";
                    ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
                }
            }
        }

        //Method that executes when the assign section button is clicked.
        protected void btnAssignMember_Click(object sender, EventArgs e)
        {
            //using the data context
            using (context = new KarateSchoolDataContext(connString))
            {
                //intiializing local variables based on text box field
                string sectionName = RadioButtonList1.SelectedValue;
                int memid = Convert.ToInt32(assignMemId.Text.Trim());
                int instructorid = Convert.ToInt32((assignInstructorId.Text.Trim()));
                decimal sectionfee = Convert.ToDecimal(SectionFee.Text.Trim());
                DateTime today = DateTime.Now;
                //Creating the new section
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
                //alerting user if the operation was successful
                string script = "alert('Section Assignment Completed');";
                ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);

            }
        }
    

        protected void btnAddInstructor_Click(object sender, EventArgs e)
        {
            //using the datacontext
            using (context = new KarateSchoolDataContext(connString))
            {
                //initializing local variables
                string firstname = InstFirst.Text.Trim();
                string lastname = InstLast.Text.Trim();
                DateTime today = DateTime.Now;
                string phonenumber = InstPhone.Text.Trim();
                string username = InstUsername.Text.Trim();
                string password = InstPassword.Text.Trim();
                //creating net user first due to foreign key restraint put on InstructorID
                NetUser user = new NetUser
                {
                    UserName = username,
                    UserPassword = password,
                    UserType = "Instructor"
                };
                context.NetUsers.InsertOnSubmit(user);
                context.SubmitChanges();
                //obtaining userId from created net user to use in the Instructor
                int generatedUserId = user.UserID;
                //creating instructor
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
                //alerting if successful
                string script = "alert('Add Instructor Completed');";
                ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
            }
        }

       

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Get the primary key value of the row to be deleted
            int primaryKeyValue = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values["InstructorID"]);

           //using data context
            using (context = new KarateSchoolDataContext(connString))
            {
                //Retrieve the entities to be deleted - might be multiple sections so I store as a list
                var entitiesToDelete1 = context.Sections.Where(x => x.Member_ID == primaryKeyValue).ToList();
                var entityToDelete2 = context.Instructors.SingleOrDefault(x => x.InstructorID == primaryKeyValue);
                var entityToDelete3 = context.NetUsers.SingleOrDefault(x => x.UserID == primaryKeyValue);
                //Delete Sections
                if (entitiesToDelete1.Count() > 0 )
                {
                    context.Sections.DeleteAllOnSubmit(entitiesToDelete1);
                    context.SubmitChanges();
                }
                //Delete Instructor and then NetUser due to foreign key relationship
                if (entityToDelete2 != null)
                {
                    context.Instructors.DeleteOnSubmit(entityToDelete2);
                    context.NetUsers.DeleteOnSubmit(entityToDelete3);
                    context.SubmitChanges();
                    LoadInstructors();
                    //alerting if the instructor was deleted
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