<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="errorPage.aspx.cs" Inherits="CapstoneBlackstone.errorPage" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    <div class="row">
            <div class="col-md-4 col-lg-4 col-md-offset-4 col-lg-offset-4">
                <h1 style="font-size: 80px;">Error:</h1>
                <p>Oops! Something went wrong on our end. Please re-login...</p>
                <asp:Button ID="btnReturnToLanding" CssClass="btn btn-success btn-lg;" runat="server" Text="Return to Landing Page" Style="font-size: 16px; background-color: #bed600; border-color: #bed600; color: black" Height="47px" Width="210px" OnClick="btnReturnToLanding_Click" />
            </div>
        </div>
   </div>
    </form>
</body>
</html>
