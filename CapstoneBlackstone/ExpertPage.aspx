<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="True" CodeBehind="ExpertPage.aspx.cs" Inherits="CapstoneBlackstone.ExpertPage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         #imgProfilePic{
            width: 270px;
            height: 270px;
            padding-bottom: 10px;
        }

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
        #BtnVenture{
             border-radius: 24px;
             width: 183px;
             color: #0098C3;
        }
        #lblDescription{
             border-radius: 24px;
             width: 183px;
             color: #bed600;
        } 
        
        .container{
            

        }
        .row{
            padding-top:10px;
        }
    </style>
        <div class="row">
            <div class="col-lg-6">
                <asp:Image id="imgProfilePic" runat="server" class="img-responsive" alt="Profile Picture" Style=" width: 270px;height: 270px;padding-bottom: 10px;"></asp:Image>
                <asp:button runat="server" text="Edit Profile" id="btnEditExpertPage" onclick="btnEditExpertPage_Click" class="btn btn-success" style="background-color: #ea2839; border-color: #ea2839; margin-bottom: 5px; border-radius: 30px;"/>
            </div>
            <div class="col-lg-6">
                <h2 id="h2Name" runat="server"></h2>
                <h3 id="h3SkillGroup" runat="server"></h3>
                College: <span id="spCollege" runat="server"></span><br />
                Major: <span id="spMajor" runat="server"></span><br />
                <span>Contact Information</span>
                <p>
                    <span class="glyphicon glyphicon-envelope"></span>&nbsp&nbsp <span id="spEmail" runat="server"></span>                   
                    <br />
                    <span class="glyphicon glyphicon-earphone"></span>&nbsp&nbsp <span id="spPhone" runat="server"></span>                   
                    <br />
                    <span class="fa fa-linkedin-square"></span>&nbsp&nbsp <span id="spLinkedIn" runat="server"></span>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <h1 style="text-align: left">About Me</h1>
                <p id="pAboutMe" runat="server"></p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4"><!--expert venture Names-->
                <h1 style="text-align: left">Ventures</h1>
                    <asp:Repeater ID="rptVentureNames" runat="server" ItemType="System.Tuple`2  [System.String, System.String]">
                         <ItemTemplate>
                             <div class="w3-container">
                                    <asp:Button ID="BtnVenture" class="btn btn-default" style="color: white; background-color: #00b0ca; border-color: #00b0ca; margin-bottom: 5px; border-radius: 24px" runat="server"  OnCommand="BtnVenture_Click" Text='<%# String.Format("{0} {1}", Item.Item1, Item.Item2)%>'
                                        CommandName="BtnVentureClick" CommandArgument='<%#Item.Item1%>' />  
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
            </div><!--end expert venture Names-->
            <div class="col-md-8"><!-- populate skills of expert here -->
                <h1 style="text-align: left">Expert Skills</h1><br />
                <asp:Repeater ID="rptExpertSkills" runat="server" ItemType="System.Tuple`2  [System.Int32, System.String]">
                    <ItemTemplate>
                        <div class="w3-container">
                            <asp:Button runat="server" ID="btnVentureSkill" Enabled="false" class="btn btn-default" style="color: white; background-color: #bed600; border-color: #bed600; margin-bottom: 5px; border-radius: 24px" text='<%#Item.Item2%>'/>  
                        </div>
                    </ItemTemplate>
                </asp:Repeater> <br />
            </div><!-- end skills-->
        </div><!--end row-->
</asp:Content>
