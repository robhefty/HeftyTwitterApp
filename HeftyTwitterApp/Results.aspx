<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Results.aspx.cs" Inherits="HeftyTwitterApp.Results" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="jumbotron">
        <h2>Hefty Twitter App</h2>
        <p>Results of the compare</p>
    </div>

   First Search Term Entered:<br>
    <asp:TextBox ID="search1" runat="server" ReadOnly="true"/><br />
   Tweets Per Second:<br>
    <asp:TextBox ID="tmp1" runat="server" ReadOnly="true"/><br />
  Second Search Term Entered:<br>
    <asp:TextBox ID="search2" runat="server" ReadOnly="true"/><br />
  Tweets Per Second:<br>
    <asp:TextBox ID="tmp2" runat="server" ReadOnly="true"/><br />
        <br />
    <div>
        <asp:HyperLink id="home" NavigateUrl="/" Text="Search Again" runat="server"/> 
    </div>
    <br />
    <div>
        <asp:HyperLink id="historyLink" NavigateUrl="/history.aspx" Text="View History" runat="server"/> 
    </div>


    </asp:Content>
