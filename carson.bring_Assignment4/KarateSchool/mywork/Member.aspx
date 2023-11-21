<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="KarateSchool.mywork.Member" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <!-- Add Bootstrap CSS and JS links to the head -->
        <head>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet">
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
        </head>

        <h2>Member</h2>
        <div class="alert alert-primary" role="alert">
            Hello, <asp:Label ID="Name" runat="server" Text="Label" CssClass="fw-bold"></asp:Label>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered">
                <AlternatingRowStyle CssClass="table-success" />
                <FooterStyle CssClass="table-secondary" />
                <HeaderStyle CssClass="table-secondary" Font-Bold="True" />
                <PagerStyle CssClass="table-success" />
                <SelectedRowStyle CssClass="table-primary text-white" />
                <SortedAscendingCellStyle CssClass="table-warning" />
                <SortedAscendingHeaderStyle CssClass="table-warning" />
                <SortedDescendingCellStyle CssClass="table-warning" />
                <SortedDescendingHeaderStyle CssClass="table-warning" />
            </asp:GridView>
        </div>
        <p class="mt-3">Total Fees: <asp:Label ID="TotalFees" runat="server" Text="Label" CssClass="fw-bold"></asp:Label></p>
    </div>
</asp:Content>
