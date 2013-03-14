<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_Default"
    EnableViewState="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HOLCIM Time and Pay System Manager</title>
    <!-- Apple iOS and Android stuff -->
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link rel="apple-touch-icon-precomposed" href="img/icon.png" />
    <link rel="apple-touch-startup-image" href="img/startup.png" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no,maximum-scale=1" />
    <link rel="stylesheet" href="css/css.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/light/theme.css" id="themestyle" />
    <!--[if lt IE 9]>
	<script src="js/html5.js"></script>
	<link rel="stylesheet" href="css/ie.css">
	<![endif]-->
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="js/functions.js" type="text/javascript"></script>
    <script src="js/plugins.js" type="text/javascript"></script>
    <script src="js/wl_Alert.js" type="text/javascript"></script>
    <script src="js/wl_Dialog.js" type="text/javascript"></script>
    <script src="js/wl_Form.js" type="text/javascript"></script>
    <script src="js/config.js" type="text/javascript"></script>
    <script src="js/login.js" type="text/javascript"></script>
</head>
<body id="login">
    
    <header style="margin-top:-40px;">
			<div id="logo">
				<a href="login.html"></a>
			</div>
	</header>
    <section id="content">
	<div style="text-align:left; padding-bottom:0px; margin-left:-15px; padding-left:0px; padding-top:0px; color:GrayText; font-size:10px;">
	    <%--<asp:Label ID="SystemTitle" runat="server" Text="" />--%>
	    <img src="css/images/SystemManagerLogo.png" />
	    <%--<span style='font-size:18px;'>TIME AND PAY</span><br/>
	    <span style="font-size:14px;">SYSTEM MANAGER <span style="color:#8C8C8C">LOGIN</span></span>--%>
	</div>
    <form id="loginform" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <fieldset>
			<section><label for="username">Username <asp:RequiredFieldValidator ID="RequiredFieldValidator1" EnableViewState="false" runat="server" ErrorMessage="*" ControlToValidate="username" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator></label>
				<div><asp:TextBox ID="username" runat="server" ></asp:TextBox></div>
			</section>
			<section><label for="password">Password <asp:RequiredFieldValidator ID="RequiredFieldValidator2" EnableViewState="false" runat="server" ErrorMessage="*" ControlToValidate="password" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator></label>
				<div><asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox><a href="ForgotPassword.aspx">Forgot Password?</a> </div>
			</section>
			        <div style="float:left; padding-left:15px;padding-top:10px;"><asp:Label ID="LoginMessage" runat="server"/></div>
					<div style="float:right"><asp:Button ID="LoginButton" runat="server" Text="Login"></asp:Button></div>
			</fieldset>
	</ContentTemplate>
    </asp:UpdatePanel>		
    </form>
    </section>
    
    <footer style="height: 100px">
    <div style="font-size:11px; text-align:center; mrgin-top:5px; color:#FFFFFF !important;">
        HOLCIM Time and Pay System Manager Verson 1.0<br /> Copyright &copy; 2012 All Rights Reserved.
    </div>
    </footer>
</body>
</html>
