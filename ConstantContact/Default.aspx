<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ConstantContact._Default" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 
    <div style="padding: 250px 20px;padding-left:500px;">
    
    <asp:Label ID="Label1" runat="server" style="font-size: 18px; font-weight: 500;color: black;" Text="Campaign Name"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br /><br /><br />
    <asp:ImageButton ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" style=" margin-left: 60px" ImageUrl="~/constantcontact.png"/>

       <br /><br />
    <asp:Button ID="btnCreateCampaign" runat="server" Text="Create Campaign"  OnClick="btnCreateCampaign_Click" Visible="false" />
    </div>
 

</asp:Content>
