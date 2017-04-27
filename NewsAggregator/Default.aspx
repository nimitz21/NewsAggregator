<%@ Page Title="Home Page" Language="C#" CodeBehind="Default.aspx.cs" Inherits="NewsAggregator.About" async="true"%>

<%@ Import Namespace="NewsAggregator" %>
<!DOCTYPE html/>
<head>
    <title> CODAnews </title>
    <link rel="stylesheet" href="Search Page Style.css">
    <link rel="icon" href="asset/logo.png">
</head>
<html>
    <body>
    <div id = "head">
        <div id = "logoPlace">
            <img id = "logo" src="asset/logo and coda.png">
        </div>
        <form id = "searcher" method = "get" action = "Default.aspx">
            <input type = "radio" name = "algochoice" value = "kmp" checked> Knuth-Morris-Pratt &nbsp &nbsp
            <input type = "radio" name = "algochoice" value = "boyer"> Boyer-Moore &nbsp &nbsp
            <input type = "radio" name = "algochoice" value = "regex"> Regex
            <br>
            <input id = "inputWords" type = "text" name = "searchkey" placeholder = "Type keywords here">
            <button id="submitButton" type="submit" cursor = "pointer">
                <img id = "imgSearch" src="asset/search button.png">
            </button>
        </form>
        <div id = "credit">
            <center>
                <b>Powered by</b><br>
                <img id = "gocode" src="asset/GOCode.png">
            </center>
        </div>
        <div id = "separator"><hr></div>
    </div>
    <br><br><br><br><br>
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