<%@ Page Title="Home Page" Language="C#" CodeBehind="Default.aspx.cs" Inherits="NewsAggregator._Default" Async="true" %>
<%@ Import Namespace="NewsAggregator" %>
<!DOCTYPE html/>
<head>
    <title> Home Page </title>
</head>
<html>
    <body>
    <form id="f1" action="Default.aspx" method="GET">
        <input type="text" name="searchkey" placeholder="Type keyword here" >
        <input type="submit" name="submit" value="Search"><br>
        <input type="radio" name="algochoice" value="regex" checked> Regex<br>
        <input type="radio" name="algochoice" value="kmp"> KMP<br>
        <input type="radio" name="algochoice" value="boyer"> Boyer-Moore<br>
    </form>
    <%
        //Parser a = new Parser("http://rss.detik.com/index.php/detikcom");
        Response.Write(Test.testing());
        string search_key = Request.QueryString["searchkey"];
        string algo_choice = Request.QueryString["algochoice"];
        //Response.Write(search_key + " " + algo_choice);
        //Response.Write(a.testing());
    %>
        </body>
</html>