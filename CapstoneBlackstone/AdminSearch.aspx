<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminSearch.aspx.cs" Inherits="CapstoneBlackstone.AdminSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .table {
            text-align: center;
        }

        h4 {
            text-align: center;
        }

        #expertProfilePic {
            width: 200px;
            height: 200px;
        }
        #ventureProfilePic{
            width: 200px;
            height: 200px;
        }
    </style>
    <div id="SearchDiv">
        <!--search div-->
        <br />
        <h1>Search</h1>
        <br />
        <p>If you are a Venture company and you want to look for people to join your company or need people to work on a project, please select the  "Ventures" Tab below to find Expert.</p>
        <br />
        <p>If you're someone who wants to checkout the companies on the website and/or want to join a company, please select the "Experts" Tab to find Venture Companies.</p>
        <br />        
        <div class="row">
            <div class="col-lg-4">    
                <asp:Label ID="lblDummy" runat="server" Text=""></asp:Label>
                <br />  
                <p>Please choose a skill group to search by. You can search by All Skills.</p>
                <label for="sel1">Skill Group: </label>          
                <asp:DropDownList ID="ddlSkillGroup" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlSkillGroup_SelectedIndexChanged" AppendDataBoundItems="true" class="form-control">
                    <asp:ListItem Value="">Please select a Skill Group</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-4">
                <label for="sel1">Full Name: </label>
                <asp:TextBox ID="txtFullName" runat="server" class="form-control"></asp:TextBox><br />
                <asp:Button ID="btnSearchExperts" OnClick="btnSearchExperts_Click" runat="server" Text="Search Experts"
                    Style="background-color: #00b0ca; border-color: #00b0ca;" class="form-control"/>
            </div>
            <div class="col-lg-4">
                <label for="sel1">Venture Description: </label>
                <asp:TextBox ID="txtPrefSearchVentureDesc" runat="server" class="form-control"></asp:TextBox>
                <br />
                <asp:Button ID="btnSearchVentures" OnClick="btnSearchVentures_Click" runat="server" Text="Search Ventures" class="form-control" Style="background-color: #00b0ca; border-color: #00b0ca;" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-6">
                <asp:GridView ID="gvShowSkillsBasedOnDdl" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvShowSkillsBasedOnDdl_PageIndexChanging" PageSize="25" class="table table-bordered table-hover tablesorter">
                    <Columns>
                        <asp:TemplateField HeaderText="Select Skills">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SkillName" Visible="true" HeaderText="Name of Skill" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#C2D69B" />
                    <PagerSettings PageButtonCount="25" Position="TopAndBottom" />
                </asp:GridView>
            </div>
        </div>
        <br />
        <div class="row"><!--results row-->
            <asp:Label ID="lblSearchError" runat="server" Text=""></asp:Label><br />
            <div class="col-md-8 align-self-end"><!--all repeaters-->
                <h3>Search Results</h3><br />
                <div class="w3-row-padding w3-margin-top"><!--venture's search -- using Expert Data-->
                        <asp:Repeater ID="rpExpertSearchResults" runat="server">
                        <ItemTemplate>
                            <div class="w3-half">
                                <div class="w3-card-4">
                                    <div class="w3-container">
                                        <div class="w3-third w3-container">
                                        <asp:Image class="card-img-top img-fluid" style="height:250px;max-width:200px;width: expression(this.width > 200 ? 200: true);" ID="expertProfilePic" runat="server" ImageUrl='<%# Eval("image") %>' />
                                            <h4><b><%# String.Format("{0} {1}", Eval("FirstName"), Eval("LastName")) %></b></h4>
                                            <h6><%# Eval("SkillGroupName")%></h6>
                                            <p><%# Eval("cdsAllExpertSkills")%></p>
                                            <asp:Button ID="btnExpertProfile" runat="server" Text="Profile" OnCommand="Expert_Profile_Click"
                                                CommandName="ExpertProfileClick" CommandArgument='<%# Eval("Username") %>' /><br />
                                        </div>
                                    </div><!--end container-->
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div><!--end venture Search div-->

                <!--expert's search -- using Venture Data-->
                <div class="w3-row-padding w3-margin-top">
                    <asp:Repeater ID="rpVentureSearchResults" runat="server">
                        <ItemTemplate>
                            <div class="w3-half">
                                <div class="w3-card-4">
                                    <div class="w3-container">
                                        <asp:Image class="card-img-top img-fluid" style="height:250px;max-width:200px;width: expression(this.width > 200 ? 200: true);" ID="ventureProfilePic" runat="server" ImageUrl='<%# Eval("image") %>' />
                                       
                                            <h4><b><asp:Label runat="server" ID="lblVname" Text='<%# Eval("Name") %>' /></b></h4>
                                            <p>
                                                <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("VentureDesc") %>' />
                                            </p>
                                            <asp:Button ID="btnViewVenture" runat="server" Style="background-color: lightgrey" Text="Profile" OnCommand="Venture_Profile_Click"
                                                CommandName="VentureProfileClick" CommandArgument='<%# Eval("Name") %>' />
                                    </div><!--end container-->
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div><!--end expert search div-->
            </div><!--end all repeaters-->
        </div><!--end results row-->
    </div><!--end search div-->
</asp:Content>
