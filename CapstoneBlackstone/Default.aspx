<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CapstoneBlackstone.Landing" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
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
        body {
            margin-top: 0px;
            /*background-color: #a41e35;*/
            background-image:url("Images/skyLine.jpg");
            /*background-size:contain, cover;       background-size:100% 100%;*/
           
            font-family:"Segoe UI",Arial,sans-serif;
             background-size: 105% 160%; /* Height: auto is to keep aspect ratio */
          background-repeat: repeat;
        }

        .auto-style4 {
            width: 37px;
            height: 33px;
            position: absolute;
            top: 115px;
            left: 579px;
            z-index: 1;
        }

        .auto-style5 {
            height: 52px;
        }

        .auto-style6 {
            margin-left: 560px;
        }

        .auto-style7 {
            margin-left: 600px;
        }

        .auto-style8 {
            margin-left: 440px;
        }

        .auto-style9 {
            margin-left: 560px;
            width: 730px;
            height: 38px;
            position: absolute;
            top: 356px;
            left: -163px;
            z-index: 1;
        }

        .auto-style10 {
            height: 82px;
        }

        .auto-style11 {
            width: 1290px;
            height: 19px;
            position: absolute;
            top: 408px;
            left: 10px;
            z-index: 1;
        }

        .button {
            background-color: #ff0000;
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }



        .buttonhi {
            background-color: #4cff00;
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }

        u {
            text-decoration: underline;
        }

        .footer {
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
        <div class="back" style="padding-top:80px;">
            <div class="container" style="background-color: white; z-index: 2; box-shadow: 5px 5px 5px #000000;border:4px solid black;">
                <br />
                <div class="row">
                    <div class="col-xs-7 col-sm-8 col-md-8 col-lg-9 col-md-offset-4 col-lg-offset-4 col-sm-offset-4">
                        <img src="https://branding.temple.edu/sites/branding/files/Primary_2C_PMS201_0.png" style="align-content:center;"/>
                        <%--<p style="font-weight: bold; font-size: 17px">Blackstone LaunchPad Team Building Platform</p>--%>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-xs-8 col-sm-9 col-md-10 col-lg-11 col-md-offset-1 col-lg-offset-1 col-sm-offset-1">
                        <p style="font-weight: bold; font-size: 35px; text-align:center; text-decoration:underline">Blackstone LaunchPad Team Building Platform</p>
                    </div>
                </div>

               
                <br />
                <br />
                <div class="row">
                    <div class="col-sm-1 col-md-6 col-lg-6">
                        <p style="text-align: center; font-size: 20px; font-weight:bold;">
                            About
               
                        </p>
                        <p>
                            Welcome to the Blackstone LaunchPad Team Builder! Use this site to promote your 
                skills and to find others with the skills you're looking for. Make connections 
                and trade services! This site is exclusive to Temple University students and alumni.
               
                        </p>
                    </div>
                    <div class="col-sm-1 col-md-6 col-lg-6">
                        <p style="text-align: center; font-size: 20px; font-weight:bold;">
                            General Information
               
                        </p>
                        <p>
                            Not yet familiar with the Blackstone LaunchPad? Temple University's LaunchPad is an 
                 innovative program that treats entrepreneurship as a viable career path. The Blackstone 
                 LaunchPad provides Temple University students and alumni with the skills, knowledge, and 
                 guidance to transform ideas into companies. To sign up for a 1:1 meeting or to get more 
                 info, click "More About Blackstone" button below.

               
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-1 col-md-3 col-lg-3 col-md-offset-1">

                        <a href="Login.aspx" class="btn btn-success" role="button" style="background-color: #bed600; border-color: #bed600">Login/Create Profile</a><br />
                        
                    </div>

                    <div class="col-sm-1 col-md-2 col-lg-2 col-md-offset-2">
                        <a href="http://research.temple.edu/TULaunchPad" class="btn btn-danger" role="button" style="background-color: #cb0044; border-color: #cb0044;">More About Blackstone</a>
                    </div>

                    <div class="col-md-2 col-md-offset-1">
                        <a href="https://calendar.google.com/calendar/selfsched?sstoken=UUpYZnU2dDlpbW9ffGRlZmF1bHR8NGUyYjZjMDlmY2NiZmY4NzFhNmM2ZWU2ZDU1ODE3NTk&pli=1"
                            class="btn btn-danger" role="button" style="background-color: #cb0044; border-color: #cb0044;">Schedule a Meeting</a>
                    </div>
                </div>

                <!--end container-->
                <br />
                <br />
                <br />
                <br />
                <!-- sticky footer -->
                <div class="navbar navbar-default navbar-fixed-bottom" style="background-color:black; position:fixed; color:white; align-content:center; padding-top:5px;">
                    <div class="container">
                        <p style="text-align:center;">Blackstone LaunchPad Spring 2017</p>
                    </div>
                </div>
                <!-- end sticky footer -->
            </div>
            <!--end container-->
        </div>

    </form>

</body>

</html>
