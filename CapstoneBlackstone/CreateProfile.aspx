<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProfile.aspx.cs" Inherits="CapstoneBlackstone.CreateProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-compatible" content="IE-edgeg" />
    <!--tells browser to use latest rendering engine-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- set page width to size of device and zoom level to 1 -->

    <title>Blackstone LaunchPad Team Building Platform</title>
    <link rel="icon" href="https://i.imgur.com/tlkRTSY.jpg" />

    <link rel ="stylesheet" type="text/css" href="LoginStyleSheet.css"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto+Condensed" integrity="sha384-epMyz4hu1ASjjUDbf0V201n5v31rubA1aQx3RVU6DI5uvujzgHWUnQVN5cr8UY5p" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" integrity="sha384-CmLV3WR+cw/TcN50vJSYAs2EAzhDD77tQvGcmoZ1KEzxtpl2K5xkrpFz9N2H9ClN" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto+Condensed" integrity="sha384-epMyz4hu1ASjjUDbf0V201n5v31rubA1aQx3RVU6DI5uvujzgHWUnQVN5cr8UY5p" crossorigin="anonymous"/>
    <script src="https://use.fontawesome.com/95aa67ffa4.js"></script>

    <style type="text/css">
         html, body {
            
            font-family:'Times New Roman', Times, serif;
            background-color:white;
            background-image:url("Images/Red_DiamondPattern.jpg"); /*Hides the slither of white background beneath create profile*/
            background-size: 145% 145%; /* Height: auto is to keep aspect ratio */
            background-repeat: repeat;
        }

        #wrap {
            min-height: 100%;
        }

        #main {
            overflow: auto;
        }

        footer {
            position: relative;
            margin-bottom: -50px;
            height: 50px;
            clear: both;
            background-color: #000000;
            color: #ffffff;
        }
            .auto-style3 {
            display: inline-block;
            max-width: 100%;
            height: 195px;
            padding: 4px;
            line-height: 1.42857143;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 4px;
            -webkit-transition: all .2s ease-in-out;
            -o-transition: all .2s ease-in-out;
            transition: all .2s ease-in-out;
            width: 212px;
        }

        .wrapper {
            position: relative;
        }

            .wrapper .glyphicon {
                position: absolute;
                top: 169px;
                left: 161px;
                width: 9px;
            }

        .auto-style4 {
            font-size: x-small;
        }

        p {
            font-style: italic;
        }

        .resize-best-fit-in-repeat{
        /* Resize to best fit in a whole number of times in x-direction */ 
          background-size: round auto; /* Height: auto is to keep aspect ratio */
          background-repeat: repeat;
        }
        .container{
             background-color:#ffffff;
             z-index:1; 
             box-shadow: 7px 7px 7px #746262; 
            padding-top: 20px;
            padding-right: 30px;
            padding-bottom: 50px;
            padding-left: 30px;
             border:3px solid black;
        }
        .footer{
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: black;
            color: white;
            text-align: center;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">

    <div class="container" >
        <div class="row">
            <div class="col-md-12">
                <h1 style="font-size: 36px">Create Profile</h1>
                <div>
                    Here is where you will create your Expert Profile. Use this page to describe your professional 
                    skills and what you're looking for with the Team Building Platform. Also, please provide contact
                    information so people can reach out to you.
                </div>
            </div>
        </div>
        <div class="row" style="margin-right: 0">
            <div class="form-group">
                <asp:Label ID="lblRequired" runat="server" Text="Must fill in Required(*) Fields" ForeColor="Red" Visible="false" />
                <div class="row">
                    <div class="col-lg-6">
                        <div class="col-lg-6" style="padding-left: 0;">                            
                            <label for="txtFirstName">First Name: <span style="color: red">*</span></label>
                            <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-6" style="padding-left: 0; padding-right: 0;">
                            <label for="txtLastName">Last Name: <span style="color: red">*</span></label>
                            <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                        </div>  
                    </div>
                    <div class="col-lg-6">
                        <br />
                        <p>First name and last name is self-explanatory.</p>
                    </div>                                             
                </div>              
                <div class="row">
                    <div class="col-lg-6">
                        <asp:Label ID="lblUsernameCheck" runat="server" Text="This username is already taken" ForeColor="Red" Visible="false" />
                        <label for="txtUsername">Username: <span style="color: red">*</span></label>
                        <asp:TextBox ID="txtUsername" runat="server" class="form-control"></asp:TextBox>

                        <label for="FileUpload1">Profile Picture: <span style="color: red">*</span></label>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                         <asp:Label ID="lblPicCheckSize" runat="server" Text="This Picture is too Big" ForeColor="Red" Visible="false" />
                        <asp:Label ID="lblPicCheckType" runat="server" Text="This Picture has an illegal file type" ForeColor="Red" Visible="false" />
                    </div>
                    <div class="col-lg-6">
                        <br />
                        <p>
                            Usernames should be something unique to you and recognizable to others.
                        </p>
                        <br />
                        <p>
                            You look great! Upload a nice picture of yourself. 
                        </p>
                    </div>                        
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <label for="genderDropdown">Gender: <span style="color: red">*</span></label>
                        <asp:DropDownList ID="genderDropdown" runat="server" class="form-control">
                        </asp:DropDownList>
                        <label for="ethnicityDropdown">Ethnicity: <span style="color: red">*</span></label>
                        <asp:DropDownList ID="ethnicityDropdown" runat="server" class="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-6">
                        <br />
                        <p>
                            The following information will not be made public, and is for demographics reports only.
                        </p>
                    </div> 
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-6">
                        <label for="sel5">Select Your Skill Group: <span style="color: red">*</span></label>
                        <asp:DropDownList ID="SkillGroupDropdown" runat="server" class="form-control">
                        </asp:DropDownList>
                        <br />
                        <label for="aboutMe">About Me: </label>
                        <asp:TextBox ID="aboutMe" class="span6" TextMode="multiline" Columns="50" Rows="3" runat="server" placeholder="About Me:" Style="border-radius: 5px" />
                    </div>
                    <div class="col-lg-6">
                        <br />
                        <p>
                            Skill Groups are a way of identifying your primary skillset to other experts and ventures. If you could only pick one word to describe youself, what would it be?
                        </p>
                        <br />
                        <p>
                            This is your space. Define yourself however you see fit!
                        </p>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-6">
                        <span class="glyphicon glyphicon-envelope pull-left" aria-hidden="true"></span>
                        <label for="txtEmail">&nbsp;Email: <span style="color: red">*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                        <br />
                        <span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>
                        <label for="txtPhoneNumber">&nbsp;Phone Number:</label>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" class="form-control"></asp:TextBox>
                        <br />
                        <i class="fa fa-linkedin-square" aria-hidden="true"></i>
                        <label for="txtLinkedIn">&nbsp;LinkedIn:</label>
                        <asp:TextBox ID="txtLinkedIn" runat="server" class="form-control"></asp:TextBox>
                    </div> 
                    <div class="col-lg-6">
                        <br />
                        <p>
                            Only your email is required, although feel free to add your phone number and your LinkedIn profile!
                        </p>
                    </div> 
                </div>                                  
            </div>
        </div>
        <br />
        <div class="row">
            <!--add skill group and skill-->
            <div class="col-md-12">
                <br />
                <asp:Label ID="lblAddSkillTest" runat="server" Text=""></asp:Label>
                <div style="width: 940px">
                    <!--Add skill -->
                    <div style="text-align: left">To add skills to your profile, search for a skill you have in the text box and select the skill from the drop down below. If the skill doesn't exist, then that skill will be created:</div>
                    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server"></asp:ScriptManager>

                    <asp:TextBox ID="txtAddNewSkill" runat="server" Style="width: 270px;" class="form-control"></asp:TextBox>

                    <cc1:AutoCompleteExtender ServiceMethod="SearchSkills"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtAddNewSkill"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                    </cc1:AutoCompleteExtender>
                    <br />
                    <asp:Button ID="BtnSubmitNewSkill" OnClick="BtnSubmitNewSkill_Click" runat="server" placeholder="Please enter a skill" Style="width: 100px; background-color: #bed600; border-color: #bed600" Text="Add Skill" class="btn"/>
                   
                </div>
            </div>
        </div>
        <!--end add skill group and skill-->
        <div style="text-align: center">
            
            <!--delete skills-->
            <div style="text-align: left">Select Skills to Delete, if any:</div>
            <asp:Label ID="lblDeleteTest" runat="server" Text=""></asp:Label>
            <!--delete Skills GridView Here-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset style="border:none">
                        <asp:Label ID="lblGvDeleteTest" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:GridView ID="gvDeleteSkills" class="table table-bordered table-hover tablesorter" runat="server" Width="402px" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="SkillName" Visible="true" HeaderText="Name of Skill" />
                                <asp:BoundField ReadOnly="True" Visible="false" DataField="SkillID" />
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
        <!--end delete skills-->
        <div class="row">
            <div class="col-md-42 col-md-offset-10">
                <asp:Button ID="btnCreateProfile" runat="server" OnClick="btnCreateProfile_Click" class="btn btn-success" role="button" 
                    Style="background-color: #00b0ca; border-color: #00b0ca; margin-bottom: 5px;" Text="Create" />
            </div>
        </div>
    </div>

    <script>
        $("#myForm").hide();

        $('#uploadPic').click(function () {
            console.log("clicked")
            $("input[type=file]").click();
            return false;
        });

        document.getElementById("myfile").onchange = function () {
            var reader = new FileReader();

            reader.onload = function (e) {
                // get loaded data and render thumbnail.
                document.getElementById("imgProfilePic").src = e.target.result;
            };

            // read the image file as a data URL.
            reader.readAsDataURL(this.files[0]);
        };
    </script>
 <!--end container-->
                <br />
                <br />
                <br />
                <br />
                <!-- sticky footer -->
                <div class="footer">
                        <p style="text-align:center;">Blackstone LaunchPad Spring 2017</p>
                </div>
                <!-- end sticky footer -->

    </form>

</body>

</html>
