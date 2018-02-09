<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="true" CodeBehind="MyVentures.aspx.cs" Inherits="CapstoneBlackstone.MyVentures" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <h1>My Ventures</h1>
    <br />
    <div class="card-deck" style="height:100%;">
        <ul class="list-group list-group-flush" role="group" aria-label="basics">
            <li class="list-group-item">
                <div class="card">
                    <div class="card-block">
                        <h3 class="card-title">
                            Hooli</h3>
                            <div class="btn-toolbar pull-right">
                                <a href="VenturePage.aspx" class="btn btn-info" role="button" style="background-color: #bed600; border-color: #bed600">View</a>
                                <button type="button" class="btn btn-danger" style="background-color: #cb0044; border-color: #cb0044">Leave</button>
                            </div>
                            <div>
                                <img src="logos_hooli.png" class="img-responsive" alt="Profile Picture" style="width: 175px; height: 100px" />
                                <p style="font-size: 10px; font-style: italic">
                                    “What is Hooli? Excellent question. Hooli isn’t just another high-tech company. Hooli
                                    <br />
                                    isn’t just about software. Hooli … Hooli is about people. Hooli is about innovative<br />
                                    technology that makes a difference..."
                                </p>
                            </div>
                    </div>
                </div>
            </li>
            <li class="list-group-item">
                <div class="card">
                    <div class="card-block">
                        <h3 class="card-title">
                            <i>Create New Venture</i>
                            <div class="btn-toolbar pull-right">
                                <a href="CreateVenture.aspx" class="btn btn-success" style="background-color: #00b0ca; border-color: #00b0ca">Create</a>
                            </div>
                        </h3>
                    </div>
                </div>
            </li>
        </ul>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
