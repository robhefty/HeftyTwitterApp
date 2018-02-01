<%@ Page Title="Hefty Twitter App" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeftyTwitterApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Hefty Twitter App</h2>
        <p>Compare the popularity of two terms or phrases on Twitter</p>
    </div>

    First Search Term:<br>
    <asp:TextBox ID="search1" runat="server" required /><br />
    Second Search Term:<br>
    <asp:TextBox ID="search2" runat="server" required /><br />
    <br />
    <asp:Button ID="submitsearch" runat="server" Text="Search" OnClick="submitsearch_Click" />
    <br />
    <br />
    <div>
        <asp:HyperLink ID="historyLink" NavigateUrl="/history.aspx" Text="View History" runat="server" />
    </div>
    <div class="hidden">
        <asp:TextBox ID="tweets1" runat="server" /><br />
        <asp:TextBox ID="tweets2" runat="server" /><br />
        <asp:TextBox ID="seconds1" runat="server" /><br />
        <asp:TextBox ID="seconds2" runat="server" /><br />
    </div>


</asp:Content>
