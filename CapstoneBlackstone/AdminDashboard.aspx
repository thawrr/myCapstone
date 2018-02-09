<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="CapstoneBlackstone.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-5" id="addAdmin">
            <!--add a new admin-->
            <h4>Add New Admin</h4>
            <p style="font-size: 12px">*Add a new Admin for the system. They will have all the same capabilities as you.</p>
            <asp:label id="lblRequired" runat="server" Text="Must fill all textboxes" ForeColor="Red" Visible="false"/><br />
            <asp:Label ID="lblAdminTUID" runat="server" Text="Admin TUID:"></asp:Label>
            <asp:TextBox ID="txtAdminTUID" runat="server" class="form-control"></asp:TextBox><br />

            <asp:Label ID="lblAdminFirstName" runat="server" Text="Admin First Name:"></asp:Label>
            <asp:TextBox ID="txtAdminFirstName" runat="server" class="form-control"></asp:TextBox><br />

            <asp:Label ID="lblAdminLastName" runat="server" Text="Admin Last Name:"></asp:Label>
            <asp:TextBox ID="txtAdminLastName" runat="server" class="form-control"></asp:TextBox><br />

            <asp:Button ID="btnAddNewAdmin" runat="server" Text="Add New Admin" Style="width: 150px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnAddNewAdmin_Click" />
        </div>
        <!--end add admin-->

        <div class="col-md-5" id="removeAdmin">
            <!--Remove an Admin-->
            <h4>Remove Admin</h4>
            <p style="font-size: 12px">*Select a checkbox and hit "Remove Admin" to remove Admin Privileges from this person</p>
            <asp:label id="lblSuperAdmin" runat="server" Text="You cannot remove this Admin" ForeColor="Red" Visible="false"/><br />
            <asp:GridView ID="gvAdmins" runat="server" class="table table-bordered table-hover tablesorter" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TUID" HeaderText="TUID" ReadOnly="True"  />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="True"  />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True"  />
                </Columns>
            </asp:GridView>
            
            <asp:Button ID="btnRemoveAdmin" runat="server" Text="Remove Admin" Style="width: 150px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnRemoveAdmin_Click" />
        </div>
    </div>
    <br />
</asp:Content>
