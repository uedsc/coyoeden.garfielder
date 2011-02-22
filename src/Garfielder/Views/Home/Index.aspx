<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/nolayout.Master" Inherits="System.Web.Mvc.ViewPage<VMHome>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lingzhi's Emotion Studio - Life is Good.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section id="main">
		<h1>Lizishop.cn is under constructing...</h1>
		<h3>Redirect to the <a href="/Studio">Lingzhi emotion studio</a> in <strong id="second">5</strong> seconds</h3>
	</section>
	<script type="text/javascript">
	    (function () {
	        var obj = document.getElementById("second"),
				cnt = 5,
				timer = null,
				cd = function () {
				    if (cnt === 0) {
				        clearTimeout(timer);
				        window.location.href = "/Studio";
				        return;
				    };
				    obj.innerHTML = cnt--;
				    timer = setTimeout(function () {
				        cd();
				    }, 1000);
				};
	        cd();
	    })();
	</script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
html{background:#000;height:100%;width:100%;}
article, aside, figure, footer, header, hgroup,
menu, nav, section { display: block; }
#main{margin:50px auto;width:980px;height:480px;background:#f4f4f4;}
h1{font-size:16px;font-weight:bold;padding:20px;}
h3{font-size:14px;padding:10px 20px;}			
</style>  
<% Model.PageFlag = "home2011"; %>
</asp:Content>
