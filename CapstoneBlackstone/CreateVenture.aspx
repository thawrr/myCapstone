<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="true" CodeBehind="CreateVenture.aspx.cs" Inherits="CapstoneBlackstone.CreateVenture" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row">
            <div class="col-md-4">
                <p style="font-size: 36px">Create Venture</p>
            </div>
        </div>
        <div class="form-group">
            <asp:label id="lblRequired" runat="server" Text="Must fill in Required(*) Fields" ForeColor="Red" Visible="false"/>
            <div class="row">
                <div class="col-lg-6">
                    <asp:label id="lblVentureNameCheck" runat="server" Text="This Venture Name is already taken" ForeColor="Red" Visible="false"/>
                    <label for="txtVentureName">Venture Name: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtVentureName" runat="server" class="form-control"></asp:TextBox>
                    <label for="txtDescription">Description: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                    <label for="FileUpload1">Logo: <span style="color: red">*</span></label>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                     <asp:Label ID="lblPicCheckSize" runat="server" Text="This Picture is too Big" ForeColor="Red" Visible="false" />
                    <asp:Label ID="lblPicCheckType" runat="server" Text="This Picture has an illegal file type" ForeColor="Red" Visible="false" />
                </div>
                <div class="col-lg-6">
                    <br />
                    <p>This should be straight-forward.</p>
                    <br />
                    <p>Give us your best two-word description. For example, Google would be "Technology".</p>
                    <br />
                    <p>What is the primary image you present your venture as.</p>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <label for="aboutUs">About Us:</label>
                    <asp:TextBox ID="aboutUs" class="span6" TextMode="multiline" Columns="50" Rows="3" style="border-radius: 5px;" runat="server" placeholder="About Us:"/>
                </div>
                <div class="col-lg-6">
                    <br />
                    <p>This is your venture's space to describe yourself and what you want out of this networking system. Use it as you see fit!</p>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <asp:label id="lblEmailsMust" runat="server" Text="Emails must be in correct format(johnDoe@temple.edu)" ForeColor="Red" Visible="false"/>
                    <label for="txtPrimaryContactEmail">Contact Information:</label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <label for="txtPrimaryContactEmail">Primary Contact Email: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtPrimaryContactEmail" runat="server" class="form-control"></asp:TextBox>
                    <br />
                    <span class="glyphicon glyphicon-envelope pull-left" aria-hidden="true"></span>
                    <label for="txtEmail">&nbsp;Email: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                    <br />
                    <span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>
                    <label for="txtPhoneNumber">&nbsp;Phone Number: </label>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" class="form-control"></asp:TextBox>
                    <br />
                    <i class="fa fa-linkedin-square" aria-hidden="true"></i>
                    <label for="txtLinkedIn">&nbsp;LinkedIn: </label>
                    <asp:TextBox ID="txtLinkedIn" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-6">
                    <br />
                    <p>Have someone in paticular you want interested experts to reach out to? Put their email here.</p>
                    <br />
                    <p>General contact email for your venture.</p>
                    <br />
                    <p>Optional field, but if your venture has a dedicated phone number please enter it!</p>
                    <br />
                    <p>Also an option field, but you're more than welcome to put your venture's LinkedIn here.</p>
                </div>
            </div>    
        </div>
        <div class="row">
            <div class="col-md-2 col-md-offset-10">
                <asp:Button ID="btnCreateVenture" runat="server" Text="Create Venture" type="button" class="btn btn-default" style="color: white; background-color: #00b0ca; border-color: #00b0ca; margin-bottom: 5px;" OnClick="btnCreateVenture_Click"/> 
            </div>
        </div>
</asp:Content>
