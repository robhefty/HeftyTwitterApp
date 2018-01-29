<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="HeftyTwitterApp.History" %>

    <div class="jumbotron">
        <h1>Hefty Twitter App History</h1>
        <p>All comparision results</p>
    </div>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>History | Hefty Twitter App</title>
</head>
<body>
    <asp:HyperLink id="home" NavigateUrl="/" Text="Search Again" runat="server"/> 
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
</body>
</html>
