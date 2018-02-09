<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="True" CodeBehind="EditExpert.aspx.cs" Inherits="CapstoneBlackstone.EditExpertPage" ViewStateMode="Enabled" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        h1 {
            text-align: left;
            font-size: 24px;
        }

        h2 {
            text-align: left;
            font-size: 30px;
        }

        h3 {
            text-align: left;
            font-size: 14px;
        }

        button {
            width: 150px;
        }

        .skill {
            color: white;
            background-color: #bed600;
        }

        .venture {
            color: white;
            background-color: #00b0ca;
        }

        .auto-style1 {
            font-size: small;
        }
    </style>
        <div class="row">
            <div class="col-md-4">
                <p style="font-size: 36px">Edit Profile</p>
            </div>
        </div>

        <div class="row">
            <!--profile pic & personal info-->
            <div class="col-md-4 col-lg-4">
                <!--firstName, lastName, userName, college, Major, gender, ethnicity-->
                <div class="form-group">
                    <asp:Label ID="lblRequired" runat="server" Text="Must fill in Required(*) Fields" ForeColor="Red" Visible="false" />
                    <label for="txtFirstName">First Name: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>

                    <label for="txtLastName">Last Name: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtLastName" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>

                    <asp:Label ID="lblUsernameCheck" runat="server" Text="This username is already taken" ForeColor="Red" Visible="false" />
                    <label for="txtUsername">Username: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtUsername" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>

                    <label for="fileNew1">Profile Picture: <span style="color: red">*</span></label>
                    <p style="font-size: 14px">*If you don't choose a new file, your current profile picture will be used</p>
                    <asp:FileUpload ID="fileNew1" runat="server"/>
                    <asp:Label ID="lblPicCheckSize" runat="server" Text="This Picture is too Big" ForeColor="Red" Visible="false" />
                    <asp:Label ID="lblPicCheckType" runat="server" Text="This Picture has an illegal file type" ForeColor="Red" Visible="false" />


                </div>
                <!--end form-group-->
                <!--button: save changes-->
            </div>
            <div class="col-md-4 col-lg-4">
                <label for="sel5">Select Your Skill Group: <span style="color: red">*</span></label>
                <asp:DropDownList ID="SkillGroupDropdown" runat="server" class="form-control" Width="270px">
                </asp:DropDownList>
                <br />
                <p>About Me:</p>
                <asp:TextBox ID="aboutMe" class="span6" TextMode="multiline" Rows="3" runat="server" placeholder="About Me:" Style="width: 250px; height: 120px; border-radius: 5px" />
            </div>
            <div class="col-md-4 col-lg-4">
                <label for="usr">Contact Information:</label>
                <br />
                <span class="glyphicon glyphicon-envelope pull-left" aria-hidden="true"></span>
                <label for="txtEmail">&nbsp;Email: <span style="color: red">*</span></label>
                <asp:TextBox ID="txtEmail" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>
                <br />
                <span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>
                <label for="txtPhoneNumber">&nbsp;Phone Number:</label>
                <asp:TextBox ID="txtPhoneNumber" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>
                <br />
                <i class="fa fa-linkedin-square" aria-hidden="true"></i>
                <label for="txtLinkedIn">&nbsp;LinkedIn:</label>
                <asp:TextBox ID="txtLinkedIn" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>
                <br />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update Profile" class="btn btn-success" role="button" Style="background-color: #00b0ca; border-color: #00b0ca; margin-bottom: 5px;" />
            </div>
        </div>
        <div class="row">
            <hr style="height: 1px; border: none; color: #333; background-color: #333;" />
        </div>
        <br />
        <div class="row">
            <!--add skill group and skill-->
            <div class="col-md-5 col-lg-5">
                <br />
                <asp:Label ID="lblAddSkillTest" runat="server" Text=""></asp:Label>
                <div style="width: 940px">
                    <!--Add skill -->
                    <h4>Add Skills:</h4>
                    <p style="font-size: 14px">*Add a skill to show other users what skills you possess</p>
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

                    <asp:TextBox ID="txtAddNewSkill" runat="server" Style="width: 270px;" class="form-control"></asp:TextBox>

                    <cc1:AutoCompleteExtender ServiceMethod="SearchSkills"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtAddNewSkill"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                    </cc1:AutoCompleteExtender>
                    <br />
                    <asp:Button ID="BtnSubmitNewSkill" OnClick="BtnSubmitNewSkill_Click" runat="server" placeholder="Please enter a skill"
                        Style="width: 100px; background-color: #bed600; border-color: #bed600; color: black" Text="Add" class="btn btn-success"></asp:Button>
                </div>
            </div>
            <div class="col-md-5 col-lg-5">
                <!--delete skills-->
                <h4>Delete Skills:</h4>
                <p style="font-size: 14px">*Select skills to delete, if any</p>
                <asp:Label ID="lblDeleteTest" runat="server" Text=""></asp:Label>
                <!--delete Skills GridView Here-->
                <asp:Label ID="lblGvDeleteTest" runat="server" Text=""></asp:Label>
                <br />
                <!--allow paging later when this GV gets big-->
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset style="border: none">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:GridView ID="gvDeleteSkills" runat="server" Width="402px" AutoGenerateColumns="False" OnRowCommand="gvDeleteSkills_RowCommand" class="table table-bordered table-hover tablesorter">
                            <Columns>
                                <asp:BoundField DataField="Item2" Visible="true" HeaderText="Name of Skill" />
                                <asp:BoundField ReadOnly="True" Visible="false" DataField="SkillID" />
                                <asp:ButtonField ButtonType="Button" runat="server" HeaderText="Delete Skill" Text="Delete" CommandName="gvCommandDelete" CausesValidation="false" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#C2D69B" />
                        </asp:GridView>
                        <!--allow paging later when this GV gets big-->
                     </fieldset>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnSubmitNewSkill" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
        </div>
        <!--end add skill group and skill-->
        <!--end delete skills-->

        <div class="row">
            <hr style="height: 1px; border: none; color: #333; background-color: #333;" />
        </div>

        <div class="row">
            <h4>Deactivate Your Profile/Account</h4>
            <p style="font-size: 14px">*Deactivate/Remove your profile from the Team Building Platform (you may reactivate your account by simply logging back into the system)</p>
            <asp:Button ID="btnDeactivateExpert" runat="server" Text="Deactivate" Style="width: 115px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnDeactivateExpert_Click" />
            <br />
            <asp:Label ID="lblAreYouSure" runat="server" Text="Are you sure that you would like to deactivate your profile/account?" Visible="False"></asp:Label>
            <asp:Button ID="btnYes" runat="server" Text="Yes" Style="width: 115px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" Visible="False" OnClick="btnYes_Click" />
            <asp:Button ID="btnNo" runat="server" Text="No" Style="width: 115px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" Visible="False" OnClick="btnNo_Click" />
        </div>
        <br />
        <br />
</asp:Content>
