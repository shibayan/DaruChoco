<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DaruChoco.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>だるやなぎのチョコレート大作戦</title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery.signalR-1.0.0-rc2.min.js"></script>
    <script>
        $(function () {
            var connection = $.hubConnection();

            var choco = connection.createHubProxy("choco");
            
            choco.on("NotifyUserCount", function (user) {
                $("#user_count").text(user);
            });

            choco.on("NotifyCount", function (giri, honmei, homo) {
                $("#giri_count").text(giri);
                $("#honmei_count").text(honmei);
                $("#homo_count").text(homo);
            });

            connection.start(function () {
                $("#giri, #honmei, #homo").prop("disabled", false);
            });

            $("#giri").click(function () {
                choco.invoke("Present", 1);
            });

            $("#honmei").click(function () {
                choco.invoke("Present", 2);
            });
            
            $("#homo").click(function () {
                choco.invoke("Present", 3);
            });
        })
    </script>
    <style>
        #main {
            width: 640px;
            margin: 0 auto;
        }
        .center {
            text-align: center;
        }
    </style>
</head>
<body>
    <div id="main">
        <h1>だるやなぎのチョコレート大作戦</h1>
        <div class="center">
            <div>
                <span id="user_count"></span>人がだるやなぎにチョコをプレゼントしようとしています。
            </div>
            <br />
            <div>
                義理チョコ：<span id="giri_count"></span>個
                |
                本命チョコ：<span id="honmei_count"></span>個
                |
                ホモチョコ：<span id="homo_count"></span>個
            </div>
            <img src="Images/daruyanagi.png" alt="だるやなぎ" width="256" height="256" />
        </div>
        <hr />
        <div class="center">
            <input id="giri" type="button" value="義理チョコ" disabled="disabled" />
            |
            <input id="honmei" type="button" value="本命チョコ" disabled="disabled" />
            |
            <input id="homo" type="button" value="ホモチョコ" disabled="disabled" />
        </div>
    </div>
</body>
</html>
