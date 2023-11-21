<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="KarateSchool.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2>Administrator Dashboard</h2>
                <div class="row">
                    <!-- display members -->
                    <div class="col-md-6">
                        <h3>Members</h3>
                        <!-- GridView for members -->
                        <p>
                            <asp:GridView ID="GridView3" runat="server" OnRowDeleting="GridView3_RowDeleting">
                                <Columns>
                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                                </Columns>
                            </asp:GridView>
                        </p>
                        <p>
                            UserID : <asp:TextBox ID="MemID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MemID" ErrorMessage="Enter ID" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        </p>
                        <p>
                            -------------------------------------------------------------------------</p>
                        <p>
                            Section Name : <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                <asp:ListItem>Karate Age-Uke</asp:ListItem>
                                <asp:ListItem>Karate Chudan-Uke</asp:ListItem>
                            </asp:RadioButtonList>
                        </p>
                        Member ID: <asp:TextBox ID="TMember ID:
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
                    </div>
                    <!-- display instructors -->
                    <div class="col-md-6">
                        <h3>Instructorsuctors -->
                        <p>
                             <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" OnRowDeleting="GridView2_RowDeleting">
                                 <Columns>
                                     <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                                 </Columns>
                                 <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                 <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                 <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                 <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                 <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                 <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                 <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                 <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                 <SortedDescendingHeaderStyle BackColor="#33276A" />
                             </asp:GridView>
                        </p>
                        <p>
                             InstructorID:
                             <asp:TextBox ID="InstID" runat="server"></asp:TextBox>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
