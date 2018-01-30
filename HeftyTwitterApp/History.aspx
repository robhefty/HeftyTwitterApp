<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="HeftyTwitterApp.History" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Hefty Twitter App</h2>
        <p>All comparision results</p>
    </div>
    <div class="content-wrapper main-content clear-fix">
        <asp:Button ID="back" Text="Search Again" runat="server" OnClick="back_Click" />
        <br />
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
