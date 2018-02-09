<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="CapstoneBlackstone.LogIn" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login</title>
    <link runat="server" rel="shortcut icon" href="Resources/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="Resources/favicon.ico" type="image/ico" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js" integrity="sha384-3ceskX3iaEnIogmQchP8opvBy3Mi7Ce34nWjpBIwVTHfGYWQS9jwHDVRnpKKHJg7" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <style>
        body {
            background-image: url("Images/Red_DiamondPattern.jpg");
            font-family: "Segoe UI",Arial,sans-serifF;
        }

        .loginDiv {
            background-color: white;
            width: 35%;
            height: 403px;
            box-shadow: 7px 7px 7px #000000;
            align-self: center;
        }
    </style>

</head>
<body>
    <form id="Form_Login" runat="server">
        <div class="row" style="text-align: center; align-self: center; background-color: white">
            <div <%--class="col-xs-7 col-sm-8 col-md-8 col-lg-9 col-md-offset-5 col-lg-offset-5 col-sm-offset-5"--%> style="padding-top: 40px; text-align: center;">
                <img src="https://branding.temple.edu/sites/branding/files/Primary_2C_PMS201_0.png" style="background-color: white; padding: 10px; border-radius: 20px" /><br />
                <p><span style="font-weight: bold; font-size: 30px; color: black; background-color: white; padding: 5px; border-radius: 25px">Blackstone LaunchPad Team Building Platform</span></p>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="row" style="align-self: center;">
            <div class="loginDiv" style="margin: auto; border-radius: 20px">
                <br />
                <h1 class="text-center" style="margin-top: 25px; font-weight: bold">Login</h1>
                <br />

                <asp:TextBox ID="txb_TUID" CssClass="form-control input-lg" placeholder="AccessNet Username" runat="server" Style="width: 75%; margin: auto;" />
                <br />

                <asp:TextBox ID="txb_Password" CssClass="form-control input-lg" TextMode="Password" placeholder="AccessNet Password" runat="server" AutoComplete="False" Style="width: 75%; margin: auto;" />
                <p style="text-align: center;"><a href="https://accounts.temple.edu/selfcare/login.jsp">Forgot Password?</a></p>
                <br />
                <p style="text-align: center;">
                    <asp:Button ID="btn_login" CssClass="btn btn-success btn-lg;" runat="server" Text="Login" OnClick="btn_login_Click" Style="font-size: 16px; background-color: #bed600; border-color: #bed600; color: black" Height="47px" Width="147px" /></p>
                <br />
                <p style="text-align: center; margin-bottom: 10px;">
                    <asp:Label ID="lbl_Error" runat="server" />
                </p>
            </div>
        </div>

        <br />

    </form>
</body>
</html>
