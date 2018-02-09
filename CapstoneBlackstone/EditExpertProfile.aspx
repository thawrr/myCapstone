<%@ Page Title="" Language="C#" MasterPageFile="~/Blackstone.Master" AutoEventWireup="true" CodeBehind="EditExpertProfile.aspx.cs" Inherits="CapstoneBlackstone.EditExpertProfile" %>
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
        .skillRp{
            border-radius: 24px; 
            color: white;
            background-color: #00b0ca; 
            width: 150px;
        }

                /*AutoComplete flyout */
        .autocomplete_completionListElement
        {
            margin : 0px!important ;
            background-color : inherit ;
            color : windowtext ;
            border : buttonshadow ;
            border-width : 1px ;
            border-style : solid ;
            cursor : 'default' ;
            overflow : auto ;
            height : 200px ;
            font-family : Tahoma ;
            font-size : small ;
            text-align : left ;
            list-style-type : none ;
            }
        /* AutoComplete highlighted item */
        .autocomplete_highlightedListItem
           {
            background-color : #ffff99 ;
            color : black ;
            padding : 1px ;
            }

            /* AutoComplete item */
        .autocomplete_listItem
            {
            background-color : window ;
            color : windowtext ;
            padding : 1px ;
           }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <p style="font-size: 36px">My Profile</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                 <!--edit First Name, Last Name, College, Major, Gender, Ethnicity-->
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <!--change profile pic here-->
            </div>
            <div class="col-md-3">
                <!--change contact info, phone number, email, linkedIn-->
            </div>
            <div class="col-md-3 col-md-offset-1">
                <p>
                    <!--about me big ass multi-lined textbox-->
                    <br />
                    <asp:Label ID="lblAddSkillTest" runat="server"  Text=""></asp:Label><br />
                    <asp:Label ID="lblDeleteTest" runat="server" Text=""></asp:Label><br />
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h1 style="text-align: left">About Me</h1>
               <!-- populate AboutMe here-->
            </div>
        </div><!-- end AboutMe -->
        <div class="row">
            <div class="col-md-3">
                <h1 style="text-align: left">Ventures</h1>
                <p>
                   <!--delete or leave a venture company here?-->
                    <!--perhaps this should only happen on the venture page-->
                </p>
            </div>
            <div class="col-md-8">
                <h2>Change Skill Group</h2>
                <p><!--select skill group--></p>

                <h1 style="text-align: left">Skills&nbsp<span class="glyphicon glyphicon-plus"  style="color: #bed600; width: 4px;" data-toggle="modal" data-target="#AddSkills"></span></h1>
                
                    <!--add Skills AJAX textBox here -->
                    <!--Add skill -->
                        <p style="text-align: left">Create New Skill:</p>
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                       
                        <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" 
                            EnableCaching="true"
                            BehaviorID="AutoCompleteEx" 
                            MinimumPrefixLength="2" 
                            TargetControlID="txtAddNewSkill"
                            ServicePath="AutoComplete.asmx.cs"
                            ServiceMethod="GetCompletionList" 
                            CompletionInterval="1000"  
                            CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            DelimiterCharacters=";, :"
                            ShowOnlyCurrentWordInCompletionListItem="true">
                              <Animations>
                                  <OnShow>
                                  <Sequence>
                                  <%-- Make the completion list transparent and then show it --%>
                                  <OpacityAction Opacity="0" /><HideAction Visible="true" />
                                      <%--Cache the original size of the completion list the first time
                                        the animation is played and then set it to zero --%>
                                      <ScriptAction Script="var behavior = $find('AutoCompleteEx');
                                                                    if (!behavior._height) {
                                                                        var target = behavior.get_completionList();
                                                                        behavior._height = target.offsetHeight - 2;
                                                                        target.style.height = '0px';}" />
                                          <%-- Expand from 0px to the appropriate size while fading in --%>
                                      <Parallel Duration=".4">
                                        <FadeIn /> <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                      </Parallel>
                                  </Sequence>
                                  </OnShow>
                                  <OnHide>
                                        <%-- Collapse down to 0px and fade out --%>
                                        <Parallel Duration=".4">
                                        <FadeOut /><Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                  </Parallel>
                                  </OnHide>
                              </Animations>
                         </ajaxToolkit:AutoCompleteExtender>
                        <asp:TextBox ID="txtAddNewSkill" runat="server" AutoComplete="off" class="form-control" aria-label="Skill" style="width: 270px;"></asp:TextBox>
                        <br /><br /><br />

                <p style="line-height: 3; text-align: left">Select Skill:
                    <br />
                        <!--delete Skills GridView Here-->
                        <asp:Label ID="lblGvDeleteTest" runat="server" Text=""></asp:Label>
                        <asp:GridView ID="gvDeleteSkills" runat="server" Width="277px">
                            <Columns>
                                <asp:BoundField HeaderText="Skills" ReadOnly="True" />
                                <asp:BoundField ReadOnly="True" Visible="False" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView><br />
                     <asp:Button ID="btnDeleteSkill" 
                    class="btn btn-outline-primary btn-xs"  data-toggle="modal" runat="server" Text="Delete Skil" 
                    style="border-radius: 24px; width: 100px" OnClick="btnDeleteSkill_Click" />
                </p>
            </div>
        </div>
            <div class="row">
                <!-- save changes button-->
                <!--close: don't save changes, return to expert profile-->
            </div>
    </div><!--end container-->
</asp:Content>
