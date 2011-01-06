<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMCampHome>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Garfielder camp
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
    <%this.Model.PageFlag = "camp_index"; %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="scSubhead">Summary of the site</h2>
    <div id="site-summary">
        <ul class="site-sm">
            <li class="clear"><span class="site-sm-t">Total Topics</span><div class="site-sm-n"><div class="site-sm-n_ site-sm-w<%:Model.Weight("CntTopic") %>" style="width:<%:Model.Percent(Model.CntTopic)%>%"><%:Model.CntTopic %></div></div></li>
            <li class="clear"><span class="site-sm-t">Total Topics today</span><div class="site-sm-n"><div class="site-sm-n_ site-sm-w<%:Model.Weight("CntTopicToday") %>" style="width:<%:Model.Percent(Model.CntTopicToday)%>%"><%:Model.CntTopicToday %></div></div></li>
            <li class="clear"><span class="site-sm-t">Total Groups</span><div class="site-sm-n"><div class="site-sm-n_ site-sm-w<%:Model.Weight("CntGroup") %>" style="width:<%:Model.Percent(Model.CntGroup)%>%"><%:Model.CntGroup%></div></div></li>
            <li class="clear"><span class="site-sm-t">Total Tags</span><div class="site-sm-n"><div class="site-sm-n_ site-sm-w<%:Model.Weight("CntTag") %>" style="width:<%:Model.Percent(Model.CntTag)%>%"><%:Model.CntTag%></div></div></li>
            <li class="clear"><span class="site-sm-t">Total Comments</span><div class="site-sm-n"><div class="site-sm-n_ site-sm-w<%:Model.Weight("CntComment") %>" style="width:<%:Model.Percent(Model.CntComment)%>%"><%:Model.CntComment%></div></div></li>
        </ul>
    </div>
</asp:Content>
