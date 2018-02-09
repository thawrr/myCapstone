<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="true" CodeBehind="EditVenture.aspx.cs" Inherits="CapstoneBlackstone.EditVenture" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">


        <div class="row">
            <div class="col-md-4">
                <p style="font-size: 36px">Edit Venture</p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4 col-lg-4">
                <div class="form-group">
                    <asp:Label ID="lblRequired" runat="server" Text="Must fill in Required(*) Fields" ForeColor="Red" Visible="false" />
                    <asp:Label ID="lblVentureNameCheck" runat="server" Text="This Venture Name is already taken" ForeColor="Red" Visible="false" />
                    <label for="txtVentureName">VentureName: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtVentureName" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>

                    <label for="txtDescription">Description: <span style="color: red">*</span></label>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>

                    <label for="FileUpload1">Profile Picture:</label>
                    <p style="font-size: 14px">*If you don't choose a new file, your current profile picture will be used</p>
                    <asp:FileUpload ID="FileUpload1" runat="server" Style="width: 200px" />
                    <asp:Label ID="lblPicCheckSize" runat="server" Text="This Picture is too Big" ForeColor="Red" Visible="false" />
                    <asp:Label ID="lblPicCheckType" runat="server" Text="This Picture has an illegal file type" ForeColor="Red" Visible="false" />
                </div>
            </div>
            <div class="col-md-4 col-lg-4">
                <div class="form-group">
                    <label for="aboutUs">About Us:</label>
                    <asp:TextBox ID="txtAboutUs" class="span6" TextMode="multiline" Columns="50" Rows="3" runat="server" placeholder="About Us:" Style="width: 270px; height: 200px; border-radius: 5px" />
                </div>
            </div>
            <div class="col-md-4 col-lg-4">
                <label for="usr">Contact Information:</label>
                <br />
                <span class="glyphicon glyphicon-envelope pull-left" aria-hidden="true"></span>
                <label for="txtPrimaryEmail">&nbsp;Primary Contact Email: <span style="color: red">*</span></label>
                <asp:TextBox ID="txtPrimaryEmail" runat="server" class="form-control" Style="width: 200px"></asp:TextBox>
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
                <asp:Button ID="btnEditVenture" runat="server" class="btn btn-success" role="button" Style="background-color: #00b0ca; border-color: #00b0ca; margin-bottom: 5px;" Text="Update Venture" OnClick="btnEditVenture_Click" /><p style="display: inline; font-size: 14px">*Return to Venture Page</p>
            </div>
        </div>
        <div class="row">
            <hr style="height: 1px; width: 955px; border: none; color: #333; background-color: #333;" />
        </div>

        <!--end edit venture-->
        <div class="row">
            <div class="col-md-5">
                <br />
                <asp:Label ID="lblAddSkillTest" runat="server" Text=""></asp:Label>
                <div style="width: 940px">
                    <h4>Add Skills:</h4>
                    <p style="font-size: 14px">*Add a skill to show other users what skills your Venture needs</p>
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
            <div class="col-md-5">
                <h4>Delete Skills:</h4>
                <p style="font-size: 14px">*Select skills to delete, if any</p>
                <asp:Label ID="lblDeleteTest" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblGvDeleteTest" runat="server" Text=""></asp:Label>
                <!-- delete skills that are wanted for the Venture Company-->
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset style="border:none">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:GridView ID="gvDeleteSkills" class="table table-bordered table-hover tablesorter" runat="server" Width="402px" AutoGenerateColumns="False" OnRowCommand="gvDeleteSkills_RowCommand">
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
        <!--end add skills-->
        <!-- end delete skill -->

        <div class="row">
            <hr style="height: 1px; width: 955px; border: none; color: #333; background-color: #333;" />
        </div>

        <div class="row">
            <div class="col-md-5" id="AddExpert">
                <!-- add an expert to the venture company-->
                <h4>Add Existing Member</h4>
                <p style="font-size: 14px">*Add an existing member of the Blackstone LaunchPad Team Building Platform to your venture</p>
                <asp:Label ID="lblMemberRequired" runat="server" Text="Must fill in All Textboxes" ForeColor="Red" Visible="false" /><br />
                <asp:Label ID="lblEmailNotInSystem" runat="server" Text="Email entered is not in the system. Make sure the Expert you are adding is already set up with a Profile" ForeColor="Red" Visible="false" /><br />
                <asp:Label ID="lblMemberEmail" runat="server" Text="Member's Email:"></asp:Label>
                <asp:TextBox ID="txtExpertEmail" runat="server" class="form-control"></asp:TextBox><br />

                <asp:Label ID="lblMemberRole" runat="server" Text="Member's Role:"></asp:Label>
                <asp:TextBox ID="txtExpertRole" runat="server" class="form-control"></asp:TextBox><br />

                <asp:Button ID="btnSubmitExpert" runat="server" Text="Submit" Style="width: 100px; background-color: #bed600; border-color: #bed600; color: black" OnClick="btnSubmitExpert_Click" class="btn btn-success" />
            </div>
            <!--end add expert-->

            <div class="col-md-5" id="AddStaticMember">
                <!-- add an expert to the venture company-->
                <h4>Add Static Member</h4>
                <p style="font-size: 14px">*This member is neither a Temple student or alumni, therefore they will not have a user profile</p>
                <asp:Label ID="lblStaticRequired" runat="server" Text="Must fill in All Textboxes" ForeColor="Red" Visible="false" /><br />
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox><br />

                <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox><br />

                <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                <asp:TextBox ID="txtRole" runat="server" class="form-control"></asp:TextBox><br />

                <asp:Button ID="btnSubmitStaticMember" runat="server" Text="Submit" Style="width: 100px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnSubmitStaticMember_Click" />

            </div>
        </div>
        <div class="row">
            <hr style="height: 1px; width: 955px; border: none; color: #333; background-color: #333;" />
        </div>

        <div class="row">
            <div class="col-md-5" id="RemoveExistingMember">
                <!-- Remove an expert from the venture company-->
                <h4>Remove Existing Member</h4>
                <p style="font-size: 14px">*Remove an existing member from your venture</p>
                <asp:GridView ID="gvVentureMembers" runat="server" class="table table-bordered table-hover tablesorter" AutoGenerateColumns="False" OnRowCommand="gvDeleteSkills_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Item1" HeaderText="Username" ReadOnly="True" />
                        <asp:BoundField DataField="Item2" HeaderText="Full Name" ReadOnly="True" />
                        <asp:BoundField DataField="Item3" HeaderText="Role" ReadOnly="True" />
                    </Columns>
                </asp:GridView>

                <asp:Button ID="btnRemoveExpert" runat="server" Text="Remove" Style="width: 100px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnRemoveExpert_Click" />
            </div>
            <div class="col-md-5" id="RemoveStaticMember">
                <h4>Remove Static Member</h4>
                <p style="font-size: 14px">*Remove a static member from your venture</p>
                <asp:GridView ID="gvStaticMembers" runat="server" class="table table-bordered table-hover tablesorter" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Item1" HeaderText="Full Name" ReadOnly="True" SortExpression="Full Name" />
                        <asp:BoundField DataField="Item2" HeaderText="Role" ReadOnly="True" SortExpression="role" />
                        <asp:BoundField DataField="Item3" HeaderText="StaticMemberID" SortExpression="StaticMemberID" Visible="False" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnRemoveStaticMember" runat="server" Text="Remove" Style="width: 100px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnRemoveStaticMember_Click" />
            </div>
            <!--end add expert-->
        </div>
        <div class="row">
            <hr style="height: 1px; width: 955px; border: none; color: #333; background-color: #333;" />
        </div>
        <div class="row">
            <h4>Deactivate Venture</h4>
            <p style="font-size: 14px">*Deactivate/Remove this venture from the Team Building Platform</p>
            <asp:Button ID="btnDeactivateVenture" runat="server" Text="Deactivate" Style="width: 115px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" OnClick="btnDeactivateVenture_Click" />
            <br />
            <asp:Label ID="lblAreYouSure" runat="server" Text="Are you sure that you would like to deactivate your venture?" Visible="False"></asp:Label>
            <asp:Button ID="btnYes" runat="server" Text="Yes" Style="width: 115px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" Visible="False" OnClick="btnYes_Click" />
            <asp:Button ID="btnNo" runat="server" Text="No" Style="width: 115px; background-color: #bed600; border-color: #bed600; color: black" class="btn btn-success" Visible="False" OnClick="btnNo_Click" />
        </div>
        <br />
        <br />
    </div>
    <!--end container-->
</asp:Content>
