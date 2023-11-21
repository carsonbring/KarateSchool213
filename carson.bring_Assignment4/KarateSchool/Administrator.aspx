<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="KarateSchool.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Adjust the styling as needed */
        body {
            margin: 0;
            padding: 0;
            display: flex;
            height: 100vh; /* 100% viewport height */
        }

        .left-section, .right-section {
            flex: 1; /* Equal width for both sections */
            border: 1px solid #ccc; /* Border for visual separation */
            padding: 20px;
            box-sizing: border-box;
        }
    </style>
  <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2>Administrator Dashboard</h2>
                <div class="row">
                    <!-- display members -->
                    <div class="left-section">
                        <div class="col-md-6">
                            <h3>Members</h3>
                            <!-- GridView for members -->
                            <p>
                                <asp:GridView ID="GridView3" runat="server"  DataKeyNames="Member_UserID" OnRowDeleting="GridView3_RowDeleting" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </p>
                            <p>
                                Username :
                                <asp:TextBox ID="MemUsername" runat="server"></asp:TextBox>
                            &nbsp;</p>
                            <p>
                                Password :
                                <asp:TextBox ID="MemPassword" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                First Name: <asp:TextBox ID="MemFirstName" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                Last Name:
                                <asp:TextBox ID="MemLastName" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                Member Phone:
                                <asp:TextBox ID="MemPhone" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                Member Email:
                                <asp:TextBox ID="MemEmail" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                &nbsp;<asp:Button ID="btnAddMember" runat="server" Text="Add Member" CssClass="btn btn-success" OnClick="btnAddMember_Click" />
                                <asp:Button ID="btnAddMemClear" runat="server" OnClick="btnAddMemClear_Click" Text="Clear" />
                            </p>
                            <p>
                                -------------------------------------------------------------------------</p>
                            <p>
                                Section Name : <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                    <asp:ListItem>Karate Age-Uke</asp:ListItem>
                                    <asp:ListItem>Karate Chudan-Uke</asp:ListItem>
                                </asp:RadioButtonList>
                            </p>
                            Member ID: 
                            <asp:TextBox ID="assignMemId" runat="server"></asp:TextBox>
                            <br />
                            Instructor ID:
                            <asp:TextBox ID="assignInstructorId" runat="server"></asp:TextBox>
                            <br />
                            Section Fee :
                            <asp:TextBox ID="SectionFee" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <!-- add, delete, assign buttons -->
                            <asp:Button ID="btnAssignMember" runat="server" Text="Assign Member" CssClass="btn btn-primary" OnClick="btnAssignMember_Click" />
                            <asp:Button ID="btnAssignMemClear" runat="server" OnClick="btnAssignMemClear_Click" Text="Clear" />
                        </div>
                    </div>
                    <!-- display instructors -->
                    <div class="right-section">
                        <div class="col-md-6">
                            <h3>Instructors --> <p>
                                 <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" DataKeyNames="InstructorID" OnRowDeleting="GridView2_RowDeleting">
                                     <AlternatingRowStyle BackColor="#DCDCDC" />
                                     <Columns>
                                         <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                                     </Columns>
                                     <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                     <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                     <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                     <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                     <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                     <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                     <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                     <SortedDescendingHeaderStyle BackColor="#000065" />
                                 </asp:GridView>
                            </p>
                            <p>
                                 Username :
                                 <asp:TextBox ID="InstUsername" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                 Password :
                                 <asp:TextBox ID="InstPassword" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                 First Name:
                                 <asp:TextBox ID="InstFirst" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                 Last Name:
                                 <asp:TextBox ID="InstLast" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                 Phone Number:
                                 <asp:TextBox ID="InstPhone" runat="server"></asp:TextBox>
                            </p>
                            <!-- add, delete, assign buttons -->
                            <asp:Button ID="btnAddInstructor" runat="server" Text="Add Instructor" CssClass="btn btn-success" OnClick="btnAddInstructor_Click" />
                            <asp:Button ID="btnAddInstructorClear" runat="server" OnClick="btnAddInstructorClear_Click" Text="Clear" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
