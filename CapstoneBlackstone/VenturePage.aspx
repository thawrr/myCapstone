<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="true" CodeBehind="VenturePage.aspx.cs" Inherits="CapstoneBlackstone.VenturePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            width: 300px;
        }

        .member {
            color: white;
            background-color: #00b0ca;
        }

        .skill {
            width: 150px;
            color: white;
            background-color: #bed600;
        }

        .auto-style1 {
            font-size: large;
        }

        .add {
            background-color: blueviolet;
        }
        #imgLogo{
            width: 270px;
            height:270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row">
            <div class="col-md-4">
                <p style="font-size: 36px">Venture Page</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Image ID="imgLogo" runat="server" class="img-responsive" alt="Venture Logo" Style=" width: 270px;height: 270px;padding-bottom: 10px;"></asp:Image>
            </div>
            <div class="col-md-2">
                <h2 id="h2VentureName" runat="server"></h2>
                <h3 id="h3VentureDesc" runat="server"></h3>
            </div>
            <div class="col-md-5 col-md-offset-2">
                <h1>Contact Information</h1>
                <p>
                    <span class="glyphicon glyphicon-envelope"></span>&nbsp&nbsp <span id="spVentureContact" runat="server"></span>

                </p>
                <p>
                    <span class="glyphicon glyphicon-earphone"></span>&nbsp&nbsp <span id="spVenturePhone" runat="server"></span>
                </p>
                <p>
                    <span class="fa fa-linkedin-square"></span>&nbsp&nbsp <span id="spVentureLinkedIn" runat="server"></span>
                </p>
                <h1 style="font-size: 20px">Primary Contact Email</h1>
                <asp:Label ID="lblPrimaryContactEmail" runat="server" Text="primaryContactEmail"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button ID="editVenture" type="button" class="btn btn-success" style="background-color: #ea2839; border-color: #ea2839; margin-bottom: 5px; border-radius: 30px;" runat="server" Text="Edit Venture" OnClick="editVenture_Click" Visible="false" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h1 style="text-align: left">About Us</h1>
                <p id="pAboutUs" runat="server"></p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <h1 style="text-align: left">Members & Roles</h1>
                <asp:Repeater ID="rptMembersAndRoles" runat="server" ItemType="System.Tuple`3  [System.String,System.String,System.String]">
                    <ItemTemplate>
                        <div class="w3-container">
                            <asp:Button ID="btnMember" class="btn btn-default" Style="color: white; background-color: #00b0ca; border-color: #00b0ca; margin-bottom: 5px; border-radius: 24px" runat="server" OnCommand="btnMember_Click" Text='<%# String.Format("{0} {1}", Item.Item2, Item.Item3)%>'
                                CommandName="btnMemberClick" CommandArgument='<%# Item.Item1 %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <h1 style="text-align: left">Static Members & Roles</h1>
                <h3 style="text-align: left">*These members are not Temple University Students or Alumni</h3>
                <asp:Repeater ID="rptStaticMembersAndRoles" runat="server" ItemType="System.Tuple`3  [System.String,System.String,System.Int32]">
                    <ItemTemplate>
                        <div class="w3-container">
                            <asp:Button ID="btnStaticMember" class="btn btn-default" Style="color: white; background-color: #808080; border-color: #808080; margin-bottom: 5px; border-radius: 24px" runat="server"  Text='<%# String.Format("{0} {1}", Item.Item2, Item.Item1)%>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-md-6 col-md-offset-1">
                <h1 style="text-align: left">Skills We Need </h1>
                <asp:Repeater ID="rptVentureSkills" runat="server" ItemType="System.Tuple`2  [System.Int32, System.String]">
                    <ItemTemplate>
                        <div class="w3-container">
                            <asp:Button runat="server" ID="btnVentureSkill" Enabled="false" class="btn btn-default" style="color: white; background-color: #bed600; border-color: #bed600; margin-bottom: 5px; border-radius: 24px" text='<%#Item.Item2%>'/>  
                        </div>
                    </ItemTemplate>
                </asp:Repeater> <br />
            </div>
        </div>

        <!--leave this for Venture Page-->
        <div id="DeactivateVenture" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" style="width: 350px; margin-top: 300px; margin-left: 120px">
                    <div class="modal-header">
                        <button type="button" class="close pull-right" data-dismiss="modal" style="width: 25px">&times;</button>
                        <h4 class="modal-title">Delete Venture</h4>
                    </div>
                    <div class="modal-body" style="text-align: center">
                        <p style="text-align: left">Are you sure you want to deactivate <i>Hooli</i>?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-success" data-dismiss="modal" style="width: 100px; background-color: #cb0044; border-color: #cb0044">Deactivate</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px; background-color: lightgrey; border-color: lightgray; color: white;">Close</button>
                    </div>
                </div>
            </div>
        </div>

    
    <!--end container-->
</asp:Content>
