<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="CapstoneBlackstone.Admin1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            margin-top: 0px;
        }

        u {
            text-decoration: underline;
        }

        h1 {
            text-align: center;
        }

        h4 {
            text-align: center;
        }

        .container-narrow > hr {
            margin: 30px 0;
        }

        /* Supporting main content */
        .main {
            margin: 40px 0;
            max-width: 730px;
        }

            .main p + h4 {
                margin-top: 28px;
            }

        /* Responsive: Portrait tablets and up */
        @media screen and (min-width: 768px) {
            /* Remove the padding we set earlier */
            .header, .main, .footer {
                padding-right: 0;
                padding-left: 0;
            }
        }

        .table {
            text-align: center;
        }

        .auto-style1 {
            width: 102px;
        }

        .auto-style2 {
            width: 87px;
        }

        .auto-style3 {
            width: 134px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <h1><u>Admin Report Tables</u></h1>
            <h4>Click any header to sort that column by ascending or descending order.</h4>
            <br />
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <h2><u>Expert Profiles Table</u></h2>
        </div>
    </div>


    <div class="row">
        <div class="col-md-10">

            <asp:GridView ID="gvDisplayExperts" ClientIDMode="static" runat="server" AutoGenerateColumns="False" AllowSorting="False" class="table table-bordered table-hover tablesorter" CellPadding="4" ForeColor="#333333" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="LastName" HeaderText="LastName" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="DateJoined" HeaderText="DateJoined" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="College" HeaderText="College" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="Major" HeaderText="Major" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="GenderName" HeaderText="Gender" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="EthnicityName" HeaderText="Ethnicity" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" ReadOnly="true" SortExpression="True" />

                </Columns>

            </asp:GridView>

        </div>
    </div>

    <div class="row main">
        <div class="col-lg-8 col-lg-offset-12">
            <asp:Button ID="btnDownloadExperts" runat="server" Text="Download Experts" Style="background-color: #bed600; border-color: #bed600; color: black" OnClick="btnDownloadExperts_Click" Width="143px" class="btn btn-success" />
        </div>
    </div>



    <div class="row">
        <div class="col-md-3 col-md-offset-2">
            <h2><u>Venture Pages Table</u></h2>
        </div>
    </div>



    <div class="row">

        <div class="col-lg-8 col-lg-offset-2">

            <asp:GridView ID="gvDisplayVentures" ClientIDMode="static" runat="server" AutoGenerateColumns="False" AllowSorting="False" CssClass="table table-condensed table-bordered table-hover" CellPadding="4" ForeColor="#333333" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="VentureDesc" HeaderText="VentureDesc" ReadOnly="false" SortExpression="True" />
                    <asp:BoundField DataField="ContactEmail" HeaderText="ContactEmail" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="PrimaryContactEmail" HeaderText="PrimaryContactEmail" ReadOnly="true" SortExpression="True" />
                    <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" ReadOnly="true" SortExpression="True" />

                </Columns>

            </asp:GridView>

        </div>
    </div>

    <div class="row main">
        <div class="col-lg-8 col-lg-offset-12">
            <asp:Button ID="btnDownloadVentures" runat="server" Text="Download Ventures" Style="background-color: #bed600; border-color: #bed600; color: black" OnClick="btnDownloadVentures_Click" Width="143px" class="btn btn-success" />

        </div>
    </div>


    <!--container-->

</asp:Content>
