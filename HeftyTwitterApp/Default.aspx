<%@ Page Title="Hefty Twitter App" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeftyTwitterApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Hefty Twitter App</h1>
        <p>Compare the popularity of two terms or phrases on Twitter</p>
    </div>

  First Search Term:<br>
    <asp:TextBox ID="search1" runat="server" /><br />
  Second Search Term:<br>
    <asp:TextBox ID="search2" runat="server" /><br />
    <asp:Button ID="submitsearch" runat="server" Text="Search" onclick="submitsearch_Click" />
    <br />
    <div>
        <asp:HyperLink id="historyLink" NavigateUrl="/history.aspx" Text="View History" runat="server"/> 
    </div>
    

</asp:Content>
