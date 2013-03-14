<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Download.aspx.vb" Inherits="Download" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel id="UpdatePanel1" runat="server" >
   
        <contenttemplate>
        
    <div align="center">
    
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/MyCSVFolder/Exported/63000618-62011.xls">Download File</asp:HyperLink>
    
    </div>
   </contenttemplate>

    </asp:UpdatePanel>
</asp:Content>