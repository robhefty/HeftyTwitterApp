<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeftyTwitterApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Hefty Twitter App</h1>
        <p>This will compare the popularity of two terms or phrases in Twitter</p>
    </div>

  First Search Term:<br>
  <input type="text" name="search1"><br>
  Second Search Term:<br>
  <input type="text" name="search2">
    <br />
  <input type="submit" value="Search">

</asp:Content>
