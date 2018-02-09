<%@ Page Title= "" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="true" CodeBehind="ExpertProfile.aspx.cs" Inherits="CapstoneBlackstone.ExpertProfile" %>

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
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <p style="font-size: 36px">Profile</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <img src="HarleyMeanMug.PNG" class="img-responsive" alt="Profile Picture" style="width: 175px; height: 175px;" />
            </div>
            <div class="col-md-4">
                <h2>Harley Baum</h2>
                <h3><strong>Graphic Designer</strong></h3>
                College: Tyler School of Art<br />
                Major: Graphic Design<br />
                <br />
                <span class="auto-style1"><em>Last Updated: 04/13/17
                </em></span>
            </div>
            <div class="col-md-4 col-md-offset-1">
                <h1>Contact Information</h1>
                <p>
                    <span class="glyphicon glyphicon-envelope"></span>&nbsp&nbsp tuf10315@temple.edu
                   
                    <br />
                    <span class="glyphicon glyphicon-earphone"></span>&nbsp&nbsp 167-884-6212
                   
                    <br />
                    <span class="fa fa-linkedin-square"></span>&nbsp&nbsp linkedin.com/in/harley-baum-20799713a/
               
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h1 style="text-align: left">About Me</h1>
                <p>
                    I am a Senior, Graphic Design student. I currently do freelance graphic design in my free time and am always looking for more projects to work on.
               
               
                </p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3">
                <h1 style="text-align: left">Ventures</h1>
                <p>
                    <asp:Button ID="Button2" class="btn venture" runat="server" Style="border-radius: 24px; width: 183px;" Text="Hooli, Graphic Designer" OnClick="Button2_Click"/>
                </p>
            </div>
            <div class="col-md-8">
                <h1 style="text-align: left">Skills&nbsp</h1>
                <p style="line-height: 3">
                    <button type="button" class="btn skill" style="border-radius: 24px">Visual Ideation</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Typography</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Photoshop</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Illustrator</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">InDesign</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Acrobat</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Color Theory</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Web Design</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">HTML</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">CSS</button>
                    <button type="button" class="btn skill" style="border-radius: 24px">Print Design</button>
                    <br />
                   
                </p>
            </div>
        </div>
        <!-- Modal -->
        <div id="EditProfile" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content modal-lg" style="margin-top:100px; margin-left:-25%">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" style="width:25px">&times;</button>
                        <h4 class="modal-title">Edit Profile...</h4>
                    </div>
                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-3">
                                <img src="default-profile.png" class="img-responsive" alt="Profile Picture" style="width: 198px; height: 188px;" />

                                <a href="#" class="btn btn-outline-primary btn-xs pull-right" role="button" style="color: #00b0ca; border-color: #00b0ca">Upload Photo</a>
                            </div>
                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label for="usr">First Name: <span style="color: red">*</span></label>
                                    <input type="text" class="form-control" id="usr" style="width: 200px" value="Kristie">
                                    <label for="usr">Last Name: <span style="color: red">*</span></label>
                                    <input type="text" class="form-control" id="usr2" style="width: 200px" value="Cunha">
                                    <label for="sel1">College: <span style="color: red">*</span></label>
                                    <select class="form-control" id="sel1" style="width: 240px">
                                        <option selected disabled>College of Science and Technology</option>
                                        <option>College of Science and Technology</option>
                                        <option>Tyler School of Art</option>
                                        <option>College of Education</option>
                                        <option>College of Engineering</option>
                                        <option>College of Public Health</option>
                                        <option>College of Liberal Arts</option>
                                        <option>Fox School of Business</option>
                                        <option>School of Social Work</option>
                                        <option>Maurice H. Kornberg School of Dentistry</option>
                                        <option>Beasley School of Law</option>
                                        <option>Klein College of Media and Communication</option>
                                        <option>Boyer College of Music and Dance</option>
                                        <option>Lewis Katz School of Medicine</option>
                                        <option>School of Pharmacy</option>
                                        <option>School of Podiatric Medicine</option>
                                        <option>School of Social Work</option>
                                        <option>School of Sport, Tourism and Hospitality Management</option>
                                        <option>School of Theater, Film and Media Arts</option>
                                    </select>
                                    <label for="sel2">Major: <span style="color: red">*</span></label>
                                    <select class="form-control" id="sel2" style="width: 240px">
                                        <option selected disabled>Information Science & Technology</option>
                                        <option>Applied Mathematics</option>
                                        <option>Biology</option>
                                        <option>Chemistry</option>
                                        <option>Computer Science</option>
                                        <option>Geology</option>
                                        <option>Information Science & Technology</option>
                                        <option>Mathematics</option>
                                        <option>Physics</option>
                                        <option>Biochemistry</option>
                                        <option>Biophysics</option>
                                        <option>Environmental Science</option>
                                        <option>Mathematics & Computer Science</option>
                                        <option>Mathematical Economics</option>
                                        <option>Mathematics & Physics</option>
                                        <option>Natural Science</option>
                                        <option>Neuroscience-Cellular & Molecular</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-4 col-lg-4">
                                <p class="auto-style4"><em>The following information will not be public</em></p>
                                <label for="sel3">Gender: <span style="color: red">*</span></label>
                                <select class="form-control" id="sel3" style="width: 200px">
                                    <option selected disabled>Female</option>
                                    <option>Male</option>
                                    <option>Female</option>
                                    <option>Other</option>
                                </select>
                                <label for="sel4">Ethnicity: <span style="color: red">*</span></label>
                                <select class="form-control" id="sel4" style="width: 200px">
                                    <option selected disabled>Caucasian</option>
                                    <option>Caucasian</option>
                                    <option>African American</option>
                                    <option>Hispanic/Latino</option>
                                    <option>Native Hawaiian/Pacific Islander</option>
                                    <option>Asian</option>
                                </select>
                            </div>

                        </div>
                        <div class="row">

                            <div class="col-md-4 col-md-offset-3">
                                <label for="sel5">Select Your Skill Group: <span style="color: red">*</span></label>
                                <select class="form-control" id="sel5" style="width: 170px">
                                    <option selected disabled>Programmer</option>
                                    <option>Programmer</option>
                                    <option>Graphic Designer</option>
                                    <option>Engineer</option>
                                    <option>Artist</option>
                                    <option>Musician</option>
                                    <option>Lawyer</option>
                                    <option>Marketer</option>
                                    <option>Biologist</option>
                                    <option>Chemist</option>
                                    <option>Physicist</option>
                                </select>
                                <br />
                                <p>About Me:</p>
                                <textarea class="span6" rows="3" placeholder="About Me:" style="width: 250px; height: 120px; border-radius: 5px">I am a Senior, Information Science & Technology student who someday hopes to make a difference in the world through the power of technology. In my free time I love to hangout with my friends, try new restaurants and watch European soccer.</textarea>
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <label for="usr">Contact Information:</label>
                                <br />
                                <span class="glyphicon glyphicon-envelope pull-left" aria-hidden="true"></span>
                                <span style="color: red">*</span>
                                <input type="text" class="form-control" id="usr" style="width: 200px" value="kristie.cunha@temple.edu">
                                <br />
                                <span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>
                                <input type="text" class="form-control" id="usr2" style="width: 150px" value="215-205-0650">
                                <br />
                                <i class="fa fa-linkedin-square" aria-hidden="true"></i>
                                <input type="text" class="form-control" id="usr3" style="width: 225px" value="linkedin.com/in/kristiecunha">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-success" data-dismiss="modal" style="width:150px; background-color:#bed600; border-color:#bed600">Save Changes</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width:150px; background-color:lightgray; border-color:lightgray; color:white">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <div id="DeleteSkill" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content" style="width: 300px; margin-top: 300px; margin-left: 120px">
                    <div class="modal-header">
                        <button type="button" class="close pull-right" data-dismiss="modal" style="width: 25px">&times;</button>
                        <h4 class="modal-title">Delete Skill</h4>
                    </div>
                    <div class="modal-body" style="text-align: center">
                        <p style="text-align: left">Select Skill:</p>
                        <select class="form-control" id="sel4" style="width: 270px">
                            <option selected disabled>Select Skill</option>
                            <option>Microsoft Office</option>
                            <option>Dreamweaver</option>
                            <option>Java</option>
                            <option>Python</option>
                            <option>C#</option>
                            <option>HTML</option>
                            <option>SQL</option>
                            <option>JavaScript</option>
                            <option>ASP.NET</option>
                            <option>AngularJS</option>
                        </select>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-success" data-dismiss="modal" style="width: 100px; background-color: #cb0044; border-color: #cb0044">Delete</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px; background-color: lightgrey; border-color: lightgray; color: white;">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <div id="AddSkills" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content" style="width: 300px; margin-top: 300px; margin-left: 120px">
                    <div class="modal-header">
                        <button type="button" class="close pull-right" data-dismiss="modal" style="width: 25px">&times;</button>
                        <h4 class="modal-title">Add Skill</h4>
                    </div>
                    <div class="modal-body" style="text-align: center">
                        <p style="text-align: left">Select Skill:</p>
                        <select class="form-control" id="sel4" style="width: 270px">
                            <option selected disabled>Select Skill </option>
                            <option>AI</option>
                            <option>Application Development</option>
                            <option>Databases</option>
                            <option>Finance</option>
                            <option>Graphic Design</option>
                            <option>Marketing</option>
                        </select>
                        <br />
                        OR 
                        <br />
                        <br />
                        <p style="text-align: left">Create New Skill:</p>
                        <input type="text" class="form-control" aria-label="Skill" style="width: 270px;" />

                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-success" data-dismiss="modal" style="width: 100px; background-color: #bed600; border-color: #bed600">Add</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px; background-color: lightgrey; border-color: lightgray; color: white;">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
