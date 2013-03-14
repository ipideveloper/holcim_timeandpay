<%@ Page Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="ConcurUpload.aspx.vb" Inherits="admin_ConcurUpload" Title="Concur Data Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .fileuploaduiko
        {
            border: 1px solid #919191 !important;
            -moz-border-radius: 8px !important;
            -webkit-border-radius: 8px !important;
            border-radius: 8px !important;
            text-shadow: #000 1px 1px 4px !important;
        }
        input.green {
	        background-color:#a2e8a2 !important;
        }

        input.green:hover {
	        background-color:#c5e8c5 !important;
        }
    </style>
    <script type="text/javascript">
        document.aspnetForm.ctl00_MainContentPlaceHolder_FileUpl.focus();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF; height: 60px;">
            <div style="float: left; width: 150px;">
                <img alt="Concur Logo" align="left" src="../images/Concur_logo.png" /></div>
            <div style="float: left; margin-top: 20px;">
                <span style="font-size: 24px;">Data Import</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">
            Browse excel file to import</p>
        <fieldset>
            
            <section>
	        <label for="input">Import Excel File
	        <button id="dialog" class="i_excel_document icon small" style="margin-left:-5px !important; margin-top:5px;">View Format</button>
	        </label>
			<div>
			<asp:FileUpload ID="FileUpload1" CssClass="fileuploaduiko" TabIndex="1" runat="server"/>
			
			</div>
		    </section>
		    
		    <section>
		    <label for="input">with headers?</label>
			<div>
			    <asp:CheckBox ID="CheckBox1" runat="server" TabIndex="2" Text="" Checked="true"></asp:CheckBox>
			</div>
		    </section>
            
            <section>
		        <div style="padding-top:5px; padding-bottom:5px;">
		        <asp:Button ID="btnUpload" TabIndex="300" runat="server" Text="  Load Data  " OnClick="btnUpload_Click" />
		        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnConfirmUpload" CssClass="btn green" runat="server" Text="  Import Concur Data  "></asp:Button>
		        </div>
		    </section>
            
            <section>
            <label for="input">
		        <asp:Label ID="MessageLabel" runat="server" Text="" Font-Size="Small"></asp:Label><br /><br />
		    </label>
		    <div style="padding-top:7px;">
		    <asp:Panel ID="MessagePanel" runat="server" CssClass="MessagePanel">
		    <asp:Label ID="FunctionLabel" Font-Size="small" runat="server" Text=""></asp:Label>
		    </asp:Panel>
		    <asp:GridView ID="GridView1" runat="server" Width="100%">
            </asp:GridView></div>
            </section>
        </fieldset>
    </div>
</asp:Content>
