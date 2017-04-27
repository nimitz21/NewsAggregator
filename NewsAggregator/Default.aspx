<%@ Page Title="Home Page" Language="C#" CodeBehind="Default.aspx.cs" Inherits="NewsAggregator.About" async="true"%>

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
        string search_key = Request.QueryString["searchkey"];
        string algo_choice = Request.QueryString["algochoice"];
        List<KeyValuePair<int, int>> listIndex = String_Matcher.getInstance().search(search_key, algo_choice);
        foreach (KeyValuePair<int, int> pair in listIndex)
        {
            Response.Write(String_Matcher.getInstance().printListItem(pair.Key, pair.Value));
        }
    %>
        </body>
</html>